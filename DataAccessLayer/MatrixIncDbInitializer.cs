using DataAccessLayer.Models;

namespace DataAccessLayer
{
    public static class MatrixIncDbInitializer
    {
        public static void Initialize(MatrixIncDbContext context)
        {
            if (context.Customers.Any())
            {
                return;
            }

            var customers = new Customer[]
            {
                new Customer
                {
                    Name = "Neo",
                    Address = "123 Elm Street",
                    Active = true
                },

                new Customer
                {
                    Name = "Morpheus",
                    Address = "456 Oak Street",
                    Active = true
                },

                new Customer
                {
                    Name = "Trinity",
                    Address = "789 Pine Street",
                    Active = true
                }
            };

            context.Customers.AddRange(customers);

            var products = new Product[]
            {
                new Product
                {
                    Name = "Nebuchadnezzar",
                    Description = "Hovercraft schip van Morpheus.",
                    Price = 10000m
                },

                new Product
                {
                    Name = "Jack-in Chair",
                    Description = "Stoel om te verbinden met de Matrix.",
                    Price = 500m
                },

                new Product
                {
                    Name = "EMP Device",
                    Description = "Wapen tegen machines.",
                    Price = 129m
                }
            };

            context.Products.AddRange(products);

            var parts = new Part[]
            {
                new Part
                {
                    Name = "Hydraulische Cilinder",
                    Description = "Onderdeel voor mechanische systemen."
                },

                new Part
                {
                    Name = "M5 Boutje",
                    Description = "Bevestigingsmateriaal."
                }
            };

            context.Parts.AddRange(parts);

            context.SaveChanges();
        }
    }
}