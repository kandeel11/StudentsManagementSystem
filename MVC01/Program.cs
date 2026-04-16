using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MVC01.BL;
using MVC01.Context;
using MVC01.Middleware;
using MVC01.Models;
using MVC01.Repositry;
using MVC01.ViewModels;
using Serilog;

namespace MVC01
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger=new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.Console().WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
                .CreateBootstrapLogger();

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();
            builder.Services.AddDbContext<MVCContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
            
            builder.Host.UseSerilog((hostingContext, configuration) =>
            {
                configuration.MinimumLevel.Debug()
                    
                    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
            });
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(op =>
            {
                op.Password.RequireNonAlphanumeric = false;
                op.Password.RequiredLength = 4;
                op.Password.RequireUppercase= false;
                op.Password.RequireLowercase= false;
            }) 
                .AddEntityFrameworkStores<MVCContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddScoped<IStudentRepositry, StudentRepositry>();
            builder.Services.AddScoped<IDepartmentRepositry, DepartmentRepositry>();
            builder.Services.AddScoped<ICourseRepositry, CourseRepositry>();
            builder.Services.AddScoped<IInstructorRepositry, InstructorRepositry>();
            builder.Services.AddScoped<IRepositry<StudentCourseEnrollmentVM>, StudentCourseEnrollmentRepositry>();
            var app = builder.Build();


            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            //app.UseHandelExpection();

            app.UseAuthorization();
            app.UseSession();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            Configuration.MapsterConfiguration.Register();
            app.Run();
        }
    }
}
