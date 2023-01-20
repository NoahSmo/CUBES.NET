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
            _context.ProviderOrders.RemoveRange(_context.ProviderOrders);
            _context.Orders.RemoveRange(_context.Orders);
            _context.Comments.RemoveRange(_context.Comments);
            _context.Images.RemoveRange(_context.Images);
            _context.Articles.RemoveRange(_context.Articles);
            _context.Categories.RemoveRange(_context.Categories);
            _context.Providers.RemoveRange(_context.Providers);
            _context.Status.RemoveRange(_context.Status);
            _context.Domains.RemoveRange(_context.Domains);
            _context.Addresses.RemoveRange(_context.Addresses);
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
                    Phone = 0631409799,
                    Role = "Admin",
                    Password = hash,
                });
                
                _context.SaveChanges();
            }
            
            if (!_context.Addresses.Any())
            {
                _context.Addresses.Add(new Address()
                {
                    Id = 1,
                    Street = "Kralja Petra I Karađorđevića",    
                    City =  "Novi Sad",
                    Country = "Srbija",
                    ZipCode = 27000,
                    UserId = 1
                });
                
                _context.Addresses.Add(new Address()
                {
                    Id = 2,
                    Street = "Sdqsdazqsdqsqwxc",    
                    City =  "Novi Sad",
                    Country = "Srbija",
                    ZipCode = 21000
                });
                
                _context.SaveChanges();
            }
            
            if (!_context.Domains.Any())
            {
                _context.Domains.Add(new Domain
                {
                    Id = 1,
                    Name = "John",
                    Description = "John Doe"
                });
                
                _context.SaveChanges();
            }
            
            if (!_context.Status.Any())
            {
                _context.Status.Add(new Status
                {
                    Id = 1,
                    Message = "Pending"
                });
                
                _context.Status.Add(new Status
                {
                    Id = 2,
                    Message = "In progress"
                });
                
                _context.Status.Add(new Status
                {
                    Id = 3,
                    Message = "Completed"
                });
                
                _context.SaveChanges();
            }
            
            if (!_context.Providers.Any())
            {
                _context.Providers.Add(new Provider
                {
                    Id = 1,
                    Name = "Domaine de Tariquet",
                    Email = "Tariquet.dom@gmail.com",
                    Phone = 0631409799,
                });
                _context.Providers.Add(new Provider
                {
                    Id = 2,
                    Name = "Domaine de Joy",
                    Email = "Joy.dom@gmail.com",
                    Phone = 0631409799,
                });
                _context.Providers.Add(new Provider
                {
                    Id = 3,
                    Name = "Vignoble Fontan",
                    Email = "Vignoble.Fontan@gmail.com",
                    Phone = 0631409799,
                });
                _context.Providers.Add(new Provider
                {
                    Id = 4,
                    Name = "Domaine d'Uby",
                    Email = "Uby.dom@gmail.com",
                    Phone = 0631409799,
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
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1, 
                    CategoryId = 1
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 2,
                    Name = "YLIRUM",
                    Description = "Ylirum est un vin rouge facile à boire, juteux, avec beaucoup de fraîcheur et un délicieux fruité. Nommé MEILLEUR VIN de son appellation, avec ses saveurs vanillés lui apportant cette sucrosité, voilà une belle gourmandise à partager. Appelez vite les copains : c'est l'occasion rêvée de les inviter sans vous ruiner ! Le petit vin plaisir par excellence...",
                    Year = 2015,
                    Price = 8,
                    Alcohol = 19.1,
                    Stock = 150,
                    DomainId = 1, 
                    CategoryId = 1
                });
            }
            
            if (!_context.Images.Any())
            {
                _context.Images.Add(new Image
                {
                    Id = 1,
                    Url = "TEST URL",
                    ArticleId = 1
                });
            }
            
            if (!_context.Comments.Any())
            {
                _context.Comments.Add(new Comment()
                {
                    Id = 1,
                    Rating = 5,
                    Message = "Super vin",
                    UserId = 1,
                    ArticleId = 1
                });
            }

            if (!_context.Orders.Any())
            {
                _context.Orders.Add(new Order
                {
                    Id = 1,
                    UserId = 1,
                    AddressId = 1,
                    Date = DateTime.UtcNow,
                    StatusId = 1,
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
            
            if (!_context.ProviderOrders.Any())
            {
                _context.ProviderOrders.Add(new ProviderOrder()
                {
                    Id = 1,
                    ProviderId = 1,
                    Date = DateTime.UtcNow,
                    StatusId = 1,
                    // ArticleOrders = new List<ArticleOrder>
                    // {
                    //     new ArticleOrder
                    //     {
                    //         Id = 3,
                    //         ArticleId = 1,
                    //         OrderId = 1,
                    //         Quantity = 10
                    //     },
                    //     new ArticleOrder
                    //     {
                    //         Id = 4,
                    //         ArticleId = 2,
                    //         OrderId = 1,
                    //         Quantity = 10
                    //     }
                    // }
                });
            }


            _context.SaveChanges();
        }
    }