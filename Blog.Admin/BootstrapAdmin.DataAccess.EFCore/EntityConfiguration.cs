using BootstrapAdmin.DataAccess.EFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace BootstrapAdmin.DataAccess.EFCore;

public static class EntityConfiguration
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void Configure(this ModelBuilder builder)
    {
        var converter = new ValueConverter<string?, int>(
            v => Convert.ToInt32(v),
            v => v.ToString(),
              new ConverterMappingHints(valueGeneratorFactory: (p, t) => new GuidStringGenerator()));

        builder.Entity<EFUser>().ToTable("Users");
        builder.Entity<EFUser>().Ignore(u => u.Period);
        builder.Entity<EFUser>().Ignore(u => u.NewPassword);
        builder.Entity<EFUser>().Ignore(u => u.ConfirmPassword);
        builder.Entity<EFUser>().Ignore(u => u.IsReset);
        builder.Entity<EFUser>().Property(s => s.Id).HasConversion(converter).ValueGeneratedOnAdd();
        builder.Entity<EFUser>().HasMany(s => s.Roles).WithMany(s => s.Users).UsingEntity<UserRole>(s =>
        {
            s.HasOne(s => s.User).WithMany(s => s.UserRoles).HasForeignKey(s => s.UserId);
            s.HasOne(s => s.Role).WithMany(s => s.UserRoles).HasForeignKey(s => s.RoleId);
        });
        builder.Entity<EFUser>().HasMany(s => s.Groups).WithMany(s => s.Users).UsingEntity<UserGroup>(s =>
        {
            s.HasOne(s => s.User).WithMany(s => s.UserGroup).HasForeignKey(s => s.UserId);
            s.HasOne(s => s.Group).WithMany(s => s.UserGroup).HasForeignKey(s => s.GroupId);
        });

        builder.Entity<UserRole>().Property(s => s.Id).HasConversion(converter).ValueGeneratedOnAdd();

        builder.Entity<EFRole>().ToTable("Roles");
        builder.Entity<EFRole>().Property(s => s.Id).HasConversion(converter).ValueGeneratedOnAdd();
        builder.Entity<EFRole>().HasMany(s => s.Navigations).WithMany(s => s.Roles).UsingEntity<NavigationRole>(s =>
        {
            s.HasOne(s => s.Navigation).WithMany(s => s.NavigationRoles).HasForeignKey(s => s.NavigationId);
            s.HasOne(s => s.Role).WithMany(s => s.NavigationRoles).HasForeignKey(s => s.RoleId);
        });
        builder.Entity<EFRole>().HasMany(s => s.Groups).WithMany(s => s.Roles).UsingEntity<RoleGroup>(s =>
        {
            s.HasOne(s => s.Group).WithMany(s => s.RoleGroup).HasForeignKey(s => s.GroupId);
            s.HasOne(s => s.Role).WithMany(s => s.RoleGroup).HasForeignKey(s => s.RoleId);
        });

        builder.Entity<EFNavigation>().ToTable("Navigations");
        builder.Entity<EFNavigation>().Property(s => s.Id).HasConversion(converter).ValueGeneratedOnAdd();
        builder.Entity<EFNavigation>().Ignore(s => s.HasChildren);

        //builder.Entity<Dict>().Property(s => s.Id).HasConversion(converter).ValueGeneratedOnAdd();

        //builder.Entity<Group>().Property(s => s.Id).HasConversion(converter).ValueGeneratedOnAdd();
    }
}

internal class GuidStringGenerator : ValueGenerator
{

    public override bool GeneratesTemporaryValues => false;

    protected override object? NextValue(EntityEntry entry) => "0";

}
