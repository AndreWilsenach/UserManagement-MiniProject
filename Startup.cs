using Microsoft.EntityFrameworkCore;

namespace MiniProject_UserManagement
{
    public class Startup
    {
        public IConfiguration configRoot
        {
            get;
        }
        public Startup(IConfiguration configuration)
        {
            configRoot = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                {
                    builder.WithOrigins("https://localhost:7246", "https://localhost:7246","*")
           .AllowAnyHeader()
           .AllowAnyMethod()
                           ;
                });
            });

            services.AddDbContext<MyDbContext>(options =>
            {
                string connectionString = "Server=localhost\\MINIPROJECT;Database=master;Trusted_Connection=True;User Id=sa;Password=Pass@word123;TrustServerCertificate=true;";
                options.UseSqlServer(connectionString);
            });
           
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
         
            app.Run();
        }
    }
}
