using System.Security;

namespace MiniProject_UserManagement.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property to represent the relationship between Group and Permission
        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }
}
