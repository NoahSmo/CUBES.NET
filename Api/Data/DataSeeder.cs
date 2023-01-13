using Api.Models;

namespace Api.Data;

    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            _context.Orders.RemoveRange(_context.Orders);
            _context.Articles.RemoveRange(_context.Articles);
            _context.Categories.RemoveRange(_context.Categories);
            _context.Providers.RemoveRange(_context.Providers);
            _context.Users.RemoveRange(_context.Users);

            _context.SaveChanges();
            
            if (!_context.Users.Any())
            {
                var password = "Password";
                var salt = BCrypt.Net.BCrypt.GenerateSalt();
                var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);
                
                
                
                _context.Users.Add(new User
                {
                    Id = 1,
                    Username = "JohnDoe",
                    Name = "John",
                    Surname = "Doe",
                    Email = "john.doe@gmail.com",
                    Phone = "0631409799",
                    Address = "1 Rue de la liberté",
                    City = "Rouen",
                    Country = "FR",
                    PostCode = "76000",
                    IsAdmin = true,
                    Password = hash,
                });
            }

            if (!_context.Providers.Any())
            {
                _context.Providers.Add(new Provider
                {
                    Id = 1,
                    Name = "Domaine de Tariquet",
                    Email = "Tariquet.dom@gmail.com",
                    Phone = "0631409799",
                });
                _context.Providers.Add(new Provider
                {
                    Id = 2,
                    Name = "Domaine de Joy",
                    Email = "Joy.dom@gmail.com",
                    Phone = "0631409799",
                });
                _context.Providers.Add(new Provider
                {
                    Id = 3,
                    Name = "Vignoble Fontan",
                    Email = "Vignoble.Fontan@gmail.com",
                    Phone = "0631409799",
                });
                _context.Providers.Add(new Provider
                {
                    Id = 4,
                    Name = "Domaine d'Uby",
                    Email = "Uby.dom@gmail.com",
                    Phone = "0631409799",
                });
            }

            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category
                {
                    Id = 1,
                    Name = "Vin Rouge",
                    Description = "Vin Rouge pour les amateurs",
                });
                _context.Categories.Add(new Category
                {
                    Id = 2,
                    Name = "Vin Blanc",
                    Description = "Vin Blanc pour les amateurs",
                });
                _context.Categories.Add(new Category
                {
                    Id = 3,
                    Name = "Vin Rosé",
                    Description = "Vin Rosé pour les amateurs",
                });
                _context.Categories.Add(new Category
                {
                    Id = 4,
                    Name = "Champagne",
                    Description = "Champagne pour les amateurs",
                });
            }

            if (!_context.Articles.Any())
            {
                _context.Articles.Add(new Article
                {
                    Id = 1,
                    Name = "Les Darons",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Les Darons (les Pères en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = "2020",
                    Price = "6.50",
                    ProviderId = 1,
                    CategoryId = 1,
                    Stock = 150
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 2,
                    Name = "YLIRUM",
                    Description = "Ylirum est un vin rouge facile à boire, juteux, avec beaucoup de fraîcheur et un délicieux fruité. Nommé MEILLEUR VIN de son appellation, avec ses saveurs vanillés lui apportant cette sucrosité, voilà une belle gourmandise à partager. Appelez vite les copains : c'est l'occasion rêvée de les inviter sans vous ruiner ! Le petit vin plaisir par excellence...",
                    Year = "2020",
                    Price = "4.50",
                    ProviderId = 2,
                    CategoryId = 1,
                    Stock = 150
                });
            }

            if (!_context.Orders.Any())
            {
                _context.Orders.Add(new Order
                {
                    Id = 1,
                    UserId = 1,
                    Date = DateTime.UtcNow,
                    Status = "In progress",
                    Serial = "123456789",
                    ArticleOrders = new List<ArticleOrder>
                    {
                        new ArticleOrder
                        {
                            Id = 1,
                            ArticleId = 1,
                            OrderId = 1,
                            Quantity = 10
                        },
                        new ArticleOrder
                        {
                            Id = 2,
                            ArticleId = 2,
                            OrderId = 1,
                            Quantity = 10
                        }
                    }
                });
            }

            _context.SaveChanges();
        }
    }