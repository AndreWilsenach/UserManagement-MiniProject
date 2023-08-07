namespace MiniProject_UserManagement.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        // Navigation property to represent the relationship between Permission and Group
        public ICollection<Group> GroupList { get; set; } = new List<Group>();
    }
}
