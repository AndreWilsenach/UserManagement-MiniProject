using System.Security;

namespace MiniProject_UserManagement.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> UserList { get; set; } = new List<User>();
        public ICollection<Permission> PermissionList { get; set; } = new List<Permission>();
    }
}
