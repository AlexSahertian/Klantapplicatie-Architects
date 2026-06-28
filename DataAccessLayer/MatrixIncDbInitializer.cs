using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            // Database creation/migration is handled in Program.cs via Migrate().
            if (context.Customers.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
                new Customer { Name = "Neo", Address = "123 Elm St", Username = "neo", PasswordHash = "o7gE7TfX6Fm7I9lqYvL8XW6p8b4bCw9N8z5E3Y1u0o8=", Email = "neo@example.com", Active = true },
                new Customer { Name = "Morpheus", Address = "456 Oak St", Username = "morpheus", PasswordHash = "o7gE7TfX6Fm7I9lqYvL8XW6p8b4bCw9N8z5E3Y1u0o8=", Email = "morpheus@example.com", Active = true },
                new Customer { Name = "Trinity", Address = "789 Pine St", Username = "trinity", PasswordHash = "o7gE7TfX6Fm7I9lqYvL8XW6p8b4bCw9N8z5E3Y1u0o8=", Email = "trinity@example.com", Active = true },
                new Customer { Name = "Admin", Address = "789 Pine St", Username = "admin", PasswordHash = "o7gE7TfX6Fm7I9lqYvL8XW6p8b4bCw9N8z5E3Y1u0o8=", Email = "admin@example.com", Active = true, IsAdmin = true }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges(); // persist customers so related entities have FK ids if needed

            var orders = new Order[]
            {
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-01-01")},
                new Order { Customer = customers[0], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[1], OrderDate = DateTime.Parse("2021-02-01")},
                new Order { Customer = customers[2], OrderDate = DateTime.Parse("2021-03-01")}
            };
            context.Orders.AddRange(orders);

            var products = new Product[]
            {
                new Product { Name = "Hydraulic Actuator H-200", Description = "Heavy-duty hydraulic cylinder designed for automating airlocks and bulkhead doors.", Price = 450.00m, ImageUrl = "actuator.jpg" },
                new Product { Name = "Titanium Servo Joint", Description = "High-performance mechanical articulation joint featuring an integrated precision servo motor.", Price = 899.95m, ImageUrl = "servo.png" },
                new Product { Name = "Liquid Nitrogen Cooling Pump", Description = "Specialized cryogenic cooling pump optimized for circulating liquid nitrogen in high-thermal systems.", Price = 295.50m, ImageUrl = "pump.jpg" },
                new Product { Name = "Heavy Duty Carbon Gearbox", Description = "Futuristic power transmission gearbox enclosed in a reinforced carbon-fiber casing.", Price = 620.00m, ImageUrl = "gearbox.png" },
                new Product { Name = "EMP (Electro-Magnetic Pulse) Device", Description = "Tactical defensive defense system utilized on Zion vessels for emergency shutdown protocols.", Price = 129.99m, ImageUrl = "EMP.png" }
            };
            context.Products.AddRange(products);

            var parts = new Part[]
            {
                new Part { Name = "Gear Wheel", Description = "Transmission of rotation in engine or hatch mechanisms.", Stock = 12 },
                new Part { Name = "M5 Bolt", Description = "Securing of panels, pipes, or internal modules.", Stock = 4 },
                new Part { Name = "Hydraulic Cylinder", Description = "Opening and closing of heavy airlocks or moving components.", Stock = 8 },
                new Part { Name = "Coolant Pump", Description = "Cooling systems for engines or electronic modules.", Stock = 15 }
            };
            context.Parts.AddRange(parts);

            context.SaveChanges();
        }
    }
}