using BootstrapAdmin.DataAccess.Models;

namespace BootstrapAdmin.DataAccess.EFCore.Models;

public class EFUser : User
{
    /// <summary>
    /// 
    /// </summary>
    public ICollection<EFRole>? Roles { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public List<UserRole>? UserRoles { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ICollection<EFGroup>? Groups { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public ICollection<UserGroup>? UserGroup { get; set; }
}
