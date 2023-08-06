using Microsoft.EntityFrameworkCore;
using MiniProject_UserManagement;

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDbContext>(options =>
{
    string connectionString = "Server=localhost\\MINIPROJECT;Database=master;Trusted_Connection=True;User Id=sa;Password=Pass@word123;TrustServerCertificate=true;";
    options.UseSqlServer();
});


startup.ConfigureServices(builder.Services); // calling ConfigureServices method
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
