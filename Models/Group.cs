using System.Security;

namespace MiniProject_UserManagement.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserGroup>? UserGroups { get; set; }
        public ICollection<GroupPermission>? GroupPermissions { get; set; } = new List<GroupPermission>();
    }
}
