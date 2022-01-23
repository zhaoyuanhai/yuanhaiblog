using BootstrapAdmin.DataAccess.Models;

namespace BootstrapAdmin.DataAccess.EFCore.Models;

public class EFRole : Role
{
    /// <summary>
    /// 
    /// </summary>
    public ICollection<EFUser>? Users { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<UserRole>? UserRoles { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public ICollection<EFNavigation>? Navigations { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<NavigationRole>? NavigationRoles { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ICollection<EFGroup>? Groups { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<RoleGroup>? RoleGroup { get; set; }
}
