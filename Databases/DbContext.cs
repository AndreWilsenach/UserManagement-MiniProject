
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
    }
}