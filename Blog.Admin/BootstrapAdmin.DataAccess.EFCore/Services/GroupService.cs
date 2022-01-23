using AutoMapper;
using BootstrapAdmin.DataAccess.Models;
using BootstrapAdmin.Web.Core;
using Microsoft.EntityFrameworkCore;

namespace BootstrapAdmin.DataAccess.EFCore.Services;

public class GroupService : IGroup
{
    private IDbContextFactory<BootstrapAdminContext> DbFactory;
    private IMapper _mapper;

    public GroupService(IDbContextFactory<BootstrapAdminContext> dbFactory, IMapper mapper)
    {
        DbFactory = dbFactory;
        _mapper = mapper;
    }

    public List<Group> GetAll()
    {
        using var dbcontext = DbFactory.CreateDbContext();

        return _mapper.Map<List<Group>>(dbcontext.Groups.ToList());
    }

    public List<string> GetGroupsByRoleId(string? roleId)
    {
        using var dbcontext = DbFactory.CreateDbContext();

        return dbcontext.RoleGroup.Where(s => s.RoleId == roleId).Select(s => s.GroupId!).ToList();
    }

    public List<string> GetGroupsByUserId(string? userId)
    {
        using var dbcontext = DbFactory.CreateDbContext();

        return dbcontext.UserGroup.Where(s => s.UserId == userId).Select(s => s.GroupId!).ToList();
    }

    public bool SaveGroupsByRoleId(string? roleId, IEnumerable<string> groupIds)
    {
        using var dbcontext = DbFactory.CreateDbContext();
        var role = dbcontext.Roles.FirstOrDefault(x => x.Id == roleId);
        if (role != null)
        {
            var transaction = dbcontext.Database.BeginTransaction();
            try
            {
                var roleGroups = dbcontext.RoleGroup.Where(x => x.RoleId == roleId);
                dbcontext.RoleGroup.RemoveRange(roleGroups);
                dbcontext.RoleGroup.AddRange(groupIds.Select(groupId => new Models.RoleGroup
                {
                    GroupId = groupId,
                    RoleId = roleId
                }));
                transaction.Commit();
            }
            catch
            {
                if (transaction != null)
                    transaction.Rollback();
                return false;
            }
            return dbcontext.SaveChanges() > 0;
        }
        else
        {
            return false;
        }
    }

    public bool SaveGroupsByUserId(string? userId, IEnumerable<string> groupIds)
    {
        using var dbcontext = DbFactory.CreateDbContext();
        var user = dbcontext.Users.First(x => x.Id == userId);
        if (user != null)
        {
            var userRoles = dbcontext.UserRole.Where(x => x.UserId == userId).ToList();
            var transaction = dbcontext.Database.BeginTransaction();
            try
            {
                dbcontext.UserRole.RemoveRange(userRoles);
                groupIds.ToList().ForEach(groupId =>
                {
                    dbcontext.UserGroup.Add(new Models.UserGroup
                    {
                        GroupId = groupId,
                        UserId = userId
                    });
                });
            }
            catch
            {
                if (transaction != null)
                    transaction.Rollback();
                return false;
            }
            return dbcontext.SaveChanges() > 0;
        }
        else
        {
            return false;
        }
    }
}
