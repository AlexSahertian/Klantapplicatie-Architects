using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;

using Microsoft.EntityFrameworkCore;

namespace KE03_INTDEV_SE_1_Base
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // =========================
            // DATABASE
            // =========================

            builder.Services.AddDbContext<MatrixIncDbContext>(
                options => options.UseSqlite(
                    "Data Source=MatrixInc.db"));

            // =========================
            // REPOSITORIES
            // =========================

            builder.Services.AddScoped<ICustomerRepository,
                CustomerRepository>();

            builder.Services.AddScoped<IOrderRepository,
                OrderRepository>();

            builder.Services.AddScoped<IProductRepository,
                ProductRepository>();

            builder.Services.AddScoped<IPartRepository,
                PartRepository>();

            // =========================
            // SESSION / WINKELWAGEN
            // =========================

            builder.Services.AddDistributedMemoryCache();

            builder.Services.AddSession(options =>
            {
                options.IdleTimeout =
                    TimeSpan.FromMinutes(30);

                options.Cookie.HttpOnly = true;

                options.Cookie.IsEssential = true;
            });

            // =========================
            // RAZOR PAGES
            // =========================

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // =========================
            // ERROR HANDLING
            // =========================

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");

                app.UseHsts();
            }

            // =========================
            // DATABASE INITIALIZE
            // =========================

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var context =
                    services.GetRequiredService<MatrixIncDbContext>();

                context.Database.EnsureCreated();

                MatrixIncDbInitializer.Initialize(context);
            }

            // =========================
            // MIDDLEWARE
            // =========================

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            // BELANGRIJK:
            // Session MOET vóór Authorization staan

            app.UseSession();

            app.UseAuthorization();

            // =========================
            // RAZOR PAGES
            // =========================

            app.MapRazorPages();

            app.Run();
        }
    }
}