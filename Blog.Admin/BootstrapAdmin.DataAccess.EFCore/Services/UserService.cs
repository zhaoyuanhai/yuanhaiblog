using AutoMapper;
using BootstrapAdmin.DataAccess.EFCore.Models;
using BootstrapAdmin.DataAccess.Models;
using BootstrapAdmin.Web.Core;
using Longbow.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapAdmin.DataAccess.EFCore.Services;

public class UserService : IUser
{
    private IDbContextFactory<BootstrapAdminContext> DbFactory { get; set; }
    private IMapper _mapper;

    public UserService(IDbContextFactory<BootstrapAdminContext> factory, IMapper mapper)
    {
        DbFactory = factory;
        _mapper = mapper;
    }

    public IEnumerable<User> GetAll()
    {
        using var context = DbFactory.CreateDbContext();
        return context.Users.ToList();
    }

    public bool Authenticate(string userName, string password)
    {
        using var context = DbFactory.CreateDbContext();

        var user = context.Users.Where(s => s.ApprovedTime != null).FirstOrDefault(x => x.UserName == userName);

        var isAuth = false;
        if (user != null && !string.IsNullOrEmpty(user.PassSalt))
        {
            isAuth = user.Password == LgbCryptography.ComputeHash(password, user.PassSalt);
        }
        return isAuth;
    }

    public List<string> GetApps(string userName)
    {
        return new List<string> { "BA" };
    }

    public string? GetDisplayName(string? userName)
    {
        using var context = DbFactory.CreateDbContext();
        return string.IsNullOrEmpty(userName) ? "" : context.Users.FirstOrDefault(s => s.UserName == userName)?.DisplayName;
    }

    public List<string> GetRoles(string userName)
    {
        using var context = DbFactory.CreateDbContext();
        var roleNames = from user in context.Users
                        join userRole in context.UserRole
                        on user.Id equals userRole.UserId
                        join role in context.Roles
                        on userRole.RoleId equals role.Id
                        select role.RoleName;
        return roleNames.ToList();
    }

    public List<string> GetUsersByGroupId(string? groupId)
    {
        using var context = DbFactory.CreateDbContext();

        return context.UserGroup.Where(s => s.GroupId == groupId).Select(s => s.UserId!).ToList();
    }

    public List<string> GetUsersByRoleId(string? roleId)
    {
        using var context = DbFactory.CreateDbContext();

        return context.UserRole.Where(s => s.RoleId == roleId).Select(s => s.UserId!).ToList();
    }

    public bool SaveUsersByGroupId(string? groupId, IEnumerable<string> userIds)
    {
        using var dbcontext = DbFactory.CreateDbContext();
        var transaction = dbcontext.Database.BeginTransaction();
        try
        {
            var userGroups = dbcontext.UserGroup.Where(s => s.GroupId == groupId).ToList();
            dbcontext.UserGroup.RemoveRange(userGroups);
            dbcontext.UserGroup.AddRange(userIds.Select(userId => new UserGroup
            {
                GroupId = groupId,
                UserId = userId
            }));
            var count = dbcontext.SaveChanges();
            transaction.Commit();
            return count > 0;
        }
        catch
        {
            if (transaction != null)
                transaction.Rollback();
            throw;
        }
    }

    public bool SaveUsersByRoleId(string? roleId, IEnumerable<string> userIds)
    {
        using var dbcontext = DbFactory.CreateDbContext();
        var transaction = dbcontext.Database.BeginTransaction();
        try
        {
            var userRoles = dbcontext.UserRole.Where(x => x.RoleId == roleId).ToList();
            dbcontext.UserRole.RemoveRange(userRoles);
            dbcontext.UserRole.AddRange(userIds.Select(userId => new UserRole
            {
                UserId = userId,
                RoleId = roleId
            }));
            var count = dbcontext.SaveChanges();
            transaction.Commit();
            return count > 0;
        }
        catch
        {
            if (transaction != null)
                transaction.Rollback();
            throw;
        }
    }

    public bool TryCreateUserByPhone(string phone, string appId, ICollection<string> roles)
    {
        var ret = false;
        using var dbcontext = DbFactory.CreateDbContext();
        try
        {
            var user = GetAll().FirstOrDefault(user => user.UserName == phone);
            if (user == null)
            {
                dbcontext.Database.BeginTransaction();
                user = new User()
                {
                    ApprovedBy = "Mobile",
                    ApprovedTime = DateTime.Now,
                    DisplayName = "手机用户",
                    UserName = phone,
                    Icon = "default.jpg",
                    Description = "手机用户",
                    App = appId
                };
                dbcontext.Add(user);

                // Authorization
                var roleIds = dbcontext.Roles.Where(s => roles.Contains(s.RoleName)).Select(s => s.Id).ToList();
                dbcontext.AddRange(roleIds.Select(g => new { RoleID = g, UserID = user.Id }));
                ret = dbcontext.SaveChanges() > 0;
            }
            ret = true;
        }
        catch (Exception)
        {

            throw;
        }
        return ret;
    }

    List<User> IUser.GetAll()
    {
        var dbContext = DbFactory.CreateDbContext();
        return _mapper.Map<List<User>>(dbContext.Users.ToList());
    }

    public bool TryCreateUserByPhone(string phone, string code, string appId, ICollection<string> roles)
    {
        var dbContext = DbFactory.CreateDbContext();

        var ret = false;
        try
        {
            var salt = LgbCryptography.GenerateSalt();
            var pwd = LgbCryptography.ComputeHash(code, salt);
            var user = dbContext.Users.FirstOrDefault(x => x.UserName == phone);
            if (user == null)
            {
                dbContext.Database.BeginTransaction();
                // 插入用户
                user = new User()
                {
                    ApprovedBy = "Mobile",
                    ApprovedTime = DateTime.Now,
                    DisplayName = "手机用户",
                    UserName = phone,
                    Icon = "default.jpg",
                    Description = "手机用户",
                    PassSalt = salt,
                    Password = LgbCryptography.ComputeHash(code, salt),
                    App = appId
                };
                dbContext.SaveChanges();
                // Authorization
                dbContext.Roles.Where(x => roles.Contains(x.RoleName)).ToList().ForEach(x =>
                {
                    dbContext.UserRole.Add(new UserRole
                    {
                        RoleId = x.Id,
                        UserId = user.Id
                    });
                });
                dbContext.Database.CommitTransaction();
            }
            else
            {
                user.PassSalt = salt;
                user.Password = pwd;
                dbContext.Update(user);
            }
            ret = true;
        }
        catch (Exception)
        {
            throw;
        }
        return ret;
    }

    public User? GetUserByUserName(string? userName)
    {
        var dbContext = DbFactory.CreateDbContext();
        return string.IsNullOrEmpty(userName) ? null : dbContext.Users.FirstOrDefault(x => x.UserName == userName);
    }

    public string? GetAppIdByUserName(string userName)
    {
        var dbContext = DbFactory.CreateDbContext();
        return dbContext.Users.FirstOrDefault(x => x.UserName == userName)?.App;
    }

    public bool SaveApp(string userName, string app)
    {
        var dbContext = DbFactory.CreateDbContext();
        var user = dbContext.Users.FirstOrDefault(u => u.UserName == userName);
        if (user != null)
        {
            user.App = app;
            return dbContext.SaveChanges() >= 1;
        }
        return false;
    }

    public bool SaveUser(string userName, string displayName, string password)
    {
        var salt = LgbCryptography.GenerateSalt();
        var pwd = LgbCryptography.ComputeHash(password, salt);
        var dbContext = DbFactory.CreateDbContext();

        var user = dbContext.Users.FirstOrDefault(x => x.UserName == userName);
        bool ret;
        if (user == null)
        {
            IDbContextTransaction? transaction = null;
            try
            {
                // 开始事务
                transaction = dbContext.Database.BeginTransaction();
                user = new User()
                {
                    ApprovedBy = "System",
                    ApprovedTime = DateTime.Now,
                    DisplayName = "手机用户",
                    UserName = userName,
                    Icon = "default.jpg",
                    Description = "系统默认创建",
                    PassSalt = salt,
                    Password = pwd
                };
                dbContext.SaveChanges();
                // 授权 Default 角色
                var roleId = dbContext.Roles.FirstOrDefault(x => x.RoleName == "Default")?.Id;
                dbContext.Users.Where(x => x.UserName == userName).ToList().ForEach(x =>
                {
                    dbContext.UserRole.Add(new UserRole
                    {
                        UserId = x.Id,
                        RoleId = roleId
                    });
                });
                dbContext.SaveChanges();
                // 结束事务
                transaction.Rollback();
                ret = true;
            }
            catch (Exception)
            {
                transaction?.Rollback();
                throw;
            }
        }
        else
        {
            user.DisplayName = displayName;
            user.PassSalt = salt;
            user.Password = pwd;
            dbContext.SaveChanges();
            ret = true;
        }
        return ret;
    }
}
