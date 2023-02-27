using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;

namespace Api.Data;

public class DataSeeder
{
    private readonly DataContext _context;

    public DataSeeder(DataContext context)
    {
        _context = context;
    }

    public void Drop()
    {
        _context.Database.EnsureDeleted();
    }

    public void Seed()
    {
        _context.Database.EnsureCreated();

        _context.ProviderOrders.RemoveRange(_context.ProviderOrders);
        _context.Orders.RemoveRange(_context.Orders);
        _context.Comments.RemoveRange(_context.Comments);
        _context.Carts.RemoveRange(_context.Carts);
        _context.Images.RemoveRange(_context.Images);
        _context.Articles.RemoveRange(_context.Articles);
        _context.Categories.RemoveRange(_context.Categories);
        _context.Providers.RemoveRange(_context.Providers);
        _context.Statuses.RemoveRange(_context.Statuses);
        _context.Addresses.RemoveRange(_context.Addresses);
        _context.Users.RemoveRange(_context.Users);
        _context.Roles.RemoveRange(_context.Roles);
        _context.Permissions.RemoveRange(_context.Permissions);


        _context.SaveChanges();

        if (!_context.Permissions.Any())
        {
            _context.Permissions.Add(new Permission
            {
                Id = 1,
                Name = "Create",
                Description = "Create"
            });

            _context.Permissions.Add(new Permission
            {
                Id = 2,
                Name = "Read",
                Description = "Read"
            });

            _context.Permissions.Add(new Permission
            {
                Id = 3,
                Name = "Update",
                Description = "Update"
            });

            _context.Permissions.Add(new Permission
            {
                Id = 4,
                Name = "Delete",
                Description = "Delete"
            });

            _context.SaveChanges();
        }

        if (!_context.Roles.Any())
        {
            _context.Roles.Add(new Role
            {
                Id = 1,
                Name = "Admin",
                Permissions = new List<Permission>
                {
                    _context.Permissions.FirstOrDefault(x => x.Id == 1),
                    _context.Permissions.FirstOrDefault(x => x.Id == 2),
                    _context.Permissions.FirstOrDefault(x => x.Id == 3),
                    _context.Permissions.FirstOrDefault(x => x.Id == 4)
                }
            });

            _context.Roles.Add(new Role
            {
                Id = 2,
                Name = "Provider",
                Permissions = new List<Permission>
                {
                    _context.Permissions.FirstOrDefault(x => x.Id == 1),
                    _context.Permissions.FirstOrDefault(x => x.Id == 2),
                    _context.Permissions.FirstOrDefault(x => x.Id == 3),
                    _context.Permissions.FirstOrDefault(x => x.Id == 4)
                }
            });

            _context.Roles.Add(new Role
            {
                Id = 3,
                Name = "User",
                Permissions = new List<Permission>
                {
                    _context.Permissions.FirstOrDefault(x => x.Id == 1),
                    _context.Permissions.FirstOrDefault(x => x.Id == 2),
                    _context.Permissions.FirstOrDefault(x => x.Id == 3),
                    _context.Permissions.FirstOrDefault(x => x.Id == 4)
                }
            });

            _context.SaveChanges();
        }

        if (!_context.Users.Any())
        {
            var password = "Password";
            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var hash = BCrypt.Net.BCrypt.HashPassword(password, salt);

            _context.Users.Add(new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Email = "john.doe@gmail.com",
                Phone = 0631409799,
                Password = hash,
                RoleId = 1,
                Role = _context.Roles.FirstOrDefault(x => x.Id == 1)
            });

            _context.Users.Add(new User
            {
                Id = 2,
                Name = "Jane",
                Surname = "Doe",
                Email = "jane.doe@gmail.com",
                Phone = 0631409799,
                Password = hash,
                RoleId = 3,
                Role = _context.Roles.FirstOrDefault(x => x.Id == 3)
            });

            _context.SaveChanges();
        }

        if (!_context.Carts.Any())
        {
            _context.Carts.Add(new Cart
            {
                Id = 1,
                UserId = 1,
                User = _context.Users.FirstOrDefault(x => x.Id == 1),
                CartItems = new List<CartItem>
                {
                    // new CartItem
                    // {
                    //     Id = 1,
                    //     ArticleId = 1,
                    //     Article = _context.Articles.FirstOrDefault(x => x.Id == 1),
                    //     CartId = 1,
                    //     Cart = _context.Carts.FirstOrDefault(x => x.Id == 1),
                    //     Quantity = 1
                    // },
                    // new CartItem
                    // {
                    //     Id = 2,
                    //     ArticleId = 2,
                    //     Article = _context.Articles.FirstOrDefault(x => x.Id == 2),
                    //     CartId = 1,
                    //     Cart = _context.Carts.FirstOrDefault(x => x.Id == 1),
                    //     Quantity = 1
                    // }
                }
            });

            _context.SaveChanges();
        }

        if (!_context.Addresses.Any())
        {
            _context.Addresses.Add(new Address()
            {
                Id = 1,
                Street = "Kralja Petra I Karađorđevića",
                City = "Novi Sad",
                Country = "Srbija",
                ZipCode = 27000,
                UserId = 1,
                User = _context.Users.FirstOrDefault(x => x.Id == 1)
            });

            _context.Addresses.Add(new Address()
            {
                Id = 2,
                Street = "Address Provider",
                City = "Novi Sad",
                Country = "Srbija",
                ZipCode = 21000,
            });

            _context.Addresses.Add(new Address()
            {
                Id = 3,
                Street = "Address Provider 2",
                City = "Paris",
                Country = "France",
                ZipCode = 75000,
            });
            
            _context.Addresses.Add(new Address()
            {
                Id = 4,
                Street = "Address Provider 4",
                City = "Paris",
                Country = "France",
                ZipCode = 75000,
            });
            
            _context.Addresses.Add(new Address()
            {
                Id = 5,
                Street = "Address Provider 5",
                City = "Paris",
                Country = "France",
                ZipCode = 75000,
            });
            
            _context.Addresses.Add(new Address()
            {
                Id = 6,
                Street = "Address Provider 6",
                City = "Paris",
                Country = "France",
                ZipCode = 75000,
            });

            _context.SaveChanges();
        }

        if (!_context.Statuses.Any())
        {
            _context.Statuses.Add(new Status
            {
                Id = 1,
                Message = "Pending"
            });

            _context.Statuses.Add(new Status
            {
                Id = 2,
                Message = "In progress"
            });

            _context.Statuses.Add(new Status
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
                Name = "Domaine de Joy",
                Description =
                    "Domaine de Joy est un domaine viticole situé à Saint-Christophe-des-Bardes, dans le département de l'Aude, en région Occitanie. Il est dirigé par la famille Joy depuis 1972. Le domaine est spécialisé dans la production de vins rouges et rosés.",
                Email = "domaine.joy@gmail.com",
                AddressId = 2,
            });

            _context.Providers.Add(new Provider
            {
                Id = 2,
                Name = "Domaine de Tariquet",
                Description =
                    "Domaine de Tariquet est un domaine viticole situé à Montardon, dans le département des Landes, en région Nouvelle-Aquitaine. Il est dirigé par la famille Tariquet depuis 1972. Le domaine est spécialisé dans la production de vins blancs et rosés.",
                Email = "domaine.tariquet@gmail.com",
                AddressId = 3,
            });
            
            _context.Providers.Add(new Provider
            {
                Id = 3,
                Name = "Domaine de Pelleheaut",
                Description =
                    "Domaine de Pelleheaut est un domaine viticole situé à Besançon, dans le département du Doubs, en région Bourgogne-Franche-Comté. Il est dirigé par la famille Pelleheaut depuis 1972. Le domaine est spécialisé dans la production de vins blancs et rosés.",
                Email = "domaine.pelleheaut@gmail.com",
                AddressId = 4,
            });
            
            _context.Providers.Add(new Provider
            {
                Id = 4,
                Name = "Vignoble Fontan",
                Description =
                    "Vignoble Fontan est un domaine viticole situé à Besançon, dans le département du Doubs, en région Bourgogne-Franche-Comté. Il est dirigé par la famille Fontan depuis 1972. Le domaine est spécialisé dans la production de vins blancs et rosés.",
                Email = "vignoble.fontan@gmail.com",
                AddressId = 5,
            });
            
            _context.Providers.Add(new Provider
            {
                Id = 5,
                Name = "Vignoble Uby",
                Description =                 
                    "Vignoble Uby est un domaine viticole situé à Besançon, dans le département du Doubs, en région Bourgogne-Franche-Comté. Il est dirigé par la famille Uby depuis 1972. Le domaine est spécialisé dans la production de vins blancs et rosés.",
                Email = "vignoble.uby@gmail.com",
                AddressId = 6,
            });
            
            _context.SaveChanges();
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
            
            _context.SaveChanges();
        }

        if (!_context.Articles.Any())
        {
            _context.Articles.Add(new Article
            {
                Id = 1,
                Name = "Les Darons",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Les Darons (les Pères en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 10,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 1,
                        Url = "https://picsum.photos/200",
                        ArticleId = 1,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 1)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 2,
                Name = "YLIRUM",
                Description =
                    "Ylirum est un vin rouge facile à boire, juteux, avec beaucoup de fraîcheur et un délicieux fruité. Nommé MEILLEUR VIN de son appellation, avec ses saveurs vanillés lui apportant cette sucrosité, voilà une belle gourmandise à partager. Appelez vite les copains : c'est l'occasion rêvée de les inviter sans vous ruiner ! Le petit vin plaisir par excellence...",
                Year = 2015,
                Price = 8,
                Alcohol = 19.1,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 2,
                        Url = "https://picsum.photos/200",
                        ArticleId = 2,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 2)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 3,
                Name = "Le Vin de Gascogne",
                Description =
                    "Le Vin de Gascogne est un vin blanc sec, fruité et frais, à la robe jaune pâle. Il est élaboré à partir de cépages locaux, le colombard et le gros manseng. Il est issu de raisins issus de vignes de plus de quarante ans, Les Darons (les Pères en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 3,
                        Url = "https://picsum.photos/200",
                        ArticleId = 3,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 3)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 4,
                Name = "Les Daronnes",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Les Daronnes (les Mères en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 4,
                        Url = "https://picsum.photos/200",
                        ArticleId = 4,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 4)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 5,
                Name = "Los Muchachos",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Los Muchachos (les Enfants en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 5,
                        Url = "https://picsum.photos/200",
                        ArticleId = 5,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 5)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 6,
                Name = "Le Canard",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Canard porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 6,
                        Url = "https://picsum.photos/200",
                        ArticleId = 6,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 6)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 7,
                Name = "Le Chien",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Chien porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 7,
                        Url = "https://picsum.photos/200",
                        ArticleId = 7,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 7)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 8,
                Name = "Le Chat",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Chat porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 8,
                        Url = "https://picsum.photos/200",
                        ArticleId = 8,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 8)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 9,
                Name = "Le Cheval",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Cheval porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 9,
                        Url = "https://picsum.photos/200",
                        ArticleId = 9,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 9)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 10,
                Name = "Le Cochon",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Cochon porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 10,
                        Url = "https://picsum.photos/200",
                        ArticleId = 10,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 10)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 11,
                Name = "La Chaise",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, La Chaise porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 11,
                        Url = "https://picsum.photos/200",
                        ArticleId = 11,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 11)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 12,
                Name = "Le Lit",
                Description =
                    "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Lit porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                Year = 2020,
                Price = 6.50,
                Alcohol = 25.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 12,
                        Url = "https://picsum.photos/200",
                        ArticleId = 12,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 12)
                    },
                },
            });

            _context.SaveChanges();
        }

        if (!_context.Images.Any())
        {
            _context.Images.Add(new Image()
            {
                Id = 1,
                Url = "https://picsum.photos/200",
                ArticleId = 1,
                Article = _context.Articles.FirstOrDefault(x => x.Id == 1)
            });

            _context.SaveChanges();
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

            _context.SaveChanges();
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
            
            _context.SaveChanges();
        }

        if (!_context.ProviderOrders.Any())
        {
            _context.ProviderOrders.Add(new ProviderOrder()
            {
                Id = 1,
                ProviderId = 1,
                Date = DateTime.UtcNow,
                StatusId = 1,
            });
        }

        _context.SaveChanges();
    }
}