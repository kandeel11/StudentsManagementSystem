using MVC01.Middleware;
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
            builder.Host.UseSerilog((hostingContext, configuration) =>
            {
                configuration.MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day);
            });
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
            app.UseHandelExpection();

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
