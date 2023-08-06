
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MiniProject_UserManagement.Models;


public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<Permission> Permissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Set up your database connection string here
        string connectionString = "Server=localhost\\MINIPROJECT;Database=master;Trusted_Connection=True;User Id=sa;Password=Pass@word123;TrustServerCertificate=true;";
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Permission>().HasData(
            new Permission { Id = 1, Name = "Level 1" },
            new Permission { Id = 2, Name = "Level 2" },
            new Permission { Id = 3, Name = "Level 3" }
        );

        // Seed Groups table with initial data
        modelBuilder.Entity<Group>().HasData(
            new Group { Id = 1, Name = "Minors" },
            new Group { Id = 2, Name = "Legues" },
            new Group { Id = 3, Name = "Pros" }
        );

        // Seed Users table with initial data
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Name = "James", GroupId = 1 },
            new User { Id = 2, Name = "Susan", GroupId = 1 },
            new User { Id = 3, Name = "Jan", GroupId = 2 },
            new User { Id = 4, Name = "uston", GroupId = 2 },
            new User { Id = 5, Name = "Sarel", GroupId = 3 },
            new User { Id = 6, Name = "Franciska", GroupId = 3 }
        );

        // Set up the many-to-many relationship between Groups and Permissions
        modelBuilder.Entity<GroupPermission>()
            .HasKey(gp => new { gp.GroupId, gp.PermissionId });

        modelBuilder.Entity<GroupPermission>()
            .HasOne(gp => gp.Group)
            .WithMany(g => g.GroupPermissions)
            .HasForeignKey(gp => gp.GroupId);

        modelBuilder.Entity<GroupPermission>()
            .HasOne(gp => gp.Permission)
            .WithMany(p => p.GroupPermissions)
            .HasForeignKey(gp => gp.PermissionId);

}
}