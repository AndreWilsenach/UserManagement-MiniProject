namespace MiniProject_UserManagement.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation property to represent the relationship between Permission and Group
        public ICollection<GroupPermission> GroupPermissions { get; set; } = new List<GroupPermission>();
    }
}
