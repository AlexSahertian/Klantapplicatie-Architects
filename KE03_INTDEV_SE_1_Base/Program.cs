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

            // Database
            builder.Services.AddDbContext<MatrixIncDbContext>(options =>
                options.UseSqlite("Data Source=MatrixInc.db"));

            // Repositories
            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<IPartRepository, PartRepository>();

            // Razor Pages
            builder.Services.AddRazorPages();

            // Session
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            var app = builder.Build();

            // Error handling
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            // Database aanmaken + seeden
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<MatrixIncDbContext>();

                    // Voor schoolproject veiliger dan Migrate()
                    context.Database.EnsureCreated();

                    MatrixIncDbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<MatrixIncDbContext>>();
                    logger.LogError(ex, "Er is een fout opgetreden bij het aanmaken of vullen van de database.");
                    throw;
                }
            }

            app.MapRazorPages();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseAuthorization();

            app.Run();
        }
    }
}