
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
      new Permission { Id = 1, Name = "All Access", Description = "Full access to all features" },
      new Permission { Id = 2, Name = "Moderate Access", Description = "Access to moderate features" },
      new Permission { Id = 3, Name = "Minimal Access", Description = "Limited access to basic features" }
  );

        modelBuilder.Entity<Group>().HasData(
            new Group { Id = 1, Name = "Admin Group" },
            new Group { Id = 2, Name = "Manager Group" },
            new Group { Id = 3, Name = "User Group" }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Name = "Admin",
                Surname = "Dude",
                IdNumber = "123456789",
                ContactDetail = null
            }
        );

    }
}