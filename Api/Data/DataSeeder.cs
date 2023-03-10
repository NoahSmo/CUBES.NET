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
                Street = "12 Route de la Libert??",
                City = "Paris",
                Country = "France",
                ZipCode = 75000,
            });

            _context.Addresses.Add(new Address()
            {
                Id = 2,
                Street = "21 Rue du Grand Prieur??",
                City = "Bapeaume",
                Country = "France",
                ZipCode = 62170,
            });

            _context.Addresses.Add(new Address()
            {
                Id = 3,
                Street = "116 Avenue de la R??publique",
                City = "Paris",
                Country = "France",
                ZipCode = 75011,
            });
            
            _context.Addresses.Add(new Address()
            {
                Id = 4,
                Street = "1 Avenue Paul Vaillant Couturier",
                City = "Tourcoing",
                Country = "France",
                ZipCode = 59200,
            });
            
            _context.Addresses.Add(new Address()
            {
                Id = 5,
                Street = "1 Rue de la R??publique",
                City = "Lille",
                Country = "France",
                ZipCode = 59000,
            });
            
            _context.Addresses.Add(new Address()
            {
                Id = 6,
                Street = "83 Chemin Vert",
                City = "Saint Sa??ns",
                Country = "France",
                ZipCode = 76680,
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
                    "Domaine de Joy est un domaine viticole situ?? ?? Saint-Christophe-des-Bardes, dans le d??partement de l'Aude, en r??gion Occitanie. Il est dirig?? par la famille Joy depuis 1972. Le domaine est sp??cialis?? dans la production de vins rouges et ros??s.",
                Email = "domaine.joy@gmail.com",
                AddressId = 2,
            });

            _context.Providers.Add(new Provider
            {
                Id = 2,
                Name = "Domaine de Tariquet",
                Description =
                    "Domaine de Tariquet est un domaine viticole situ?? ?? Montardon, dans le d??partement des Landes, en r??gion Nouvelle-Aquitaine. Il est dirig?? par la famille Tariquet depuis 1972. Le domaine est sp??cialis?? dans la production de vins blancs et ros??s.",
                Email = "domaine.tariquet@gmail.com",
                AddressId = 3,
            });
            
            _context.Providers.Add(new Provider
            {
                Id = 3,
                Name = "Domaine de Pelleheaut",
                Description =
                    "Domaine de Pelleheaut est un domaine viticole situ?? ?? Besan??on, dans le d??partement du Doubs, en r??gion Bourgogne-Franche-Comt??. Il est dirig?? par la famille Pelleheaut depuis 1972. Le domaine est sp??cialis?? dans la production de vins blancs et ros??s.",
                Email = "domaine.pelleheaut@gmail.com",
                AddressId = 4,
            });
            
            _context.Providers.Add(new Provider
            {
                Id = 4,
                Name = "Vignoble Fontan",
                Description =
                    "Vignoble Fontan est un domaine viticole situ?? en Gascogne, dans le d??partement des Landes, en r??gion Nouvelle-Aquitaine. Il est dirig?? par la famille Fontan depuis 1972. Le domaine est sp??cialis?? dans la production de vins blancs et ros??s.",
                Email = "vignoble.fontan@gmail.com",
                AddressId = 5,
            });
            
            _context.Providers.Add(new Provider
            {
                Id = 5,
                Name = "Vignoble Uby",
                Description =                 
                    "Vignoble Uby est un domaine viticole situ?? ?? Saint-Sa??ns, dans le d??partement du Calvados, en r??gion Normandie. Il est dirig?? par la famille Uby depuis 1972. Le domaine est sp??cialis?? dans la production de vins blancs et ros??s.",
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
                Name = "Vin Ros??",
                Description = "Vin Ros?? pour les amateurs",
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
                    "Compos?? en majorit?? de raisins issus de vignes de plus de quarante ans, Les Darons (les P??res en argot) porte bien son nom ! Charmeur avec son nez fruit?? et ??pic??, ??quilibr?? et puissant, il poss??de des nuances l??g??rement toast??es (bien qu?????lev?? en f??t) qui apportent un relief et une g??n??rosit?? des plus appr??ciables. Un vin solide et s??r de lui qui s???adresse ?? ceux qui ont suffisamment de bouteille pour appr??cier les bonnes choses de la vie !",
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
                        Url = "https://imgs.search.brave.com/7yw15rrliTP5Wl2_zsM2pGhEMlk4DZg3PGQicQGq2qM/rs:fit:800:1200:1/g:ce/aHR0cHM6Ly9wdXJl/cG5nLmNvbS9wdWJs/aWMvdXBsb2Fkcy9t/ZWRpdW0vcHVyZXBu/Zy5jb20td2luZS1i/b3R0bGVib3R0bGUt/Zm9vZC13aW5lLW9i/amVjdC1kcmluay1h/bGNvaG9sLWJldmVy/YWdlLWxpcXVvci05/NDE1MjQ2MjAwNjls/Y2tlYy5wbmc",
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
                    "Ylirum est un vin rouge facile ?? boire, juteux, avec beaucoup de fra??cheur et un d??licieux fruit??. Nomm?? MEILLEUR VIN de son appellation, avec ses saveurs vanill??s lui apportant cette sucrosit??, voil?? une belle gourmandise ?? partager. Appelez vite les copains : c'est l'occasion r??v??e de les inviter sans vous ruiner ! Le petit vin plaisir par excellence...",
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
                        Url = "https://imgs.search.brave.com/Hu0XVcJ68JRT14T6Z59Wp2t2lnr8W-lrFllky-F4NlQ/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly9sYXlt/YW5uZXdtZWRpYS5j/b20vaW1hZ2VzL3Bo/b3RvL2dyZWVuaGls/bC13aW5lcnktdmlu/ZXlhcmRzLWJvdHRs/ZS13aGl0ZS1iYWNr/Z3JvdW5kLWV0ZXJu/aXR5LTE0NDAuanBn",
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
                    "Le Vin de Gascogne est un vin blanc sec, fruit?? et frais, ?? la robe jaune p??le. Il est ??labor?? ?? partir de c??pages locaux, le colombard et le gros manseng. Il est issu de raisins issus de vignes de plus de quarante ans, Les Darons (les P??res en argot) porte bien son nom ! Charmeur avec son nez fruit?? et ??pic??, ??quilibr?? et puissant, il poss??de des nuances l??g??rement toast??es (bien qu?????lev?? en f??t) qui apportent un relief et une g??n??rosit?? des plus appr??ciables. Un vin solide et s??r de lui qui s???adresse ?? ceux qui ont suffisamment de bouteille pour appr??cier les bonnes choses de la vie !",
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
                        Url = "https://imgs.search.brave.com/JLYoxtXWgta_YNgEhXpLSX81WjmCHDIooTPTwkuAbpA/rs:fit:271:631:1/g:ce/aHR0cHM6Ly9tZWRp/YS5pc3RvY2twaG90/by5jb20vdmVjdG9y/cy9ib3R0bGUtb2Yt/d2hpdGUtd2luZS13/aXRoLWEtYmxhY2st/bGFiZWwtb24td2hp/dGUtYmFja2dyb3Vu/ZC12ZWN0b3ItaWQx/NjU2NDIxNjk_az02/Jm09MTY1NjQyMTY5/JnM9MTcwNjY3YSZ3/PTAmaD1QQlAzOWJ2/eXFKVlZGNm5ZeHFr/UGNYaE1MUnFZSGgx/VEJMUnhuc0tSRklZ/PQ",
                        ArticleId = 3,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 3)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 4,
                Name = "Andreola Col del Forno Prosecco",
                Description = "Des vignes cultiv??es ?? plus de 240 m??tres d'altitude produisent les raisins utilis??s pour l'??laboration de ce vin p??tillant limpide aux notes d??licates de fruits croquants, d'acacia et de fleurs de glycine. Il accompagne tous les plats, y compris le poisson. En bouche, il r??v??le une ??l??gance peu commune, pleine et gourmande.",
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
                        Url = "https://imgs.search.brave.com/zpuAqO7OA5YyBtlmcQa_1drEr2p34SukQ7kVlfJTsvQ/rs:fit:860:1076:1/g:ce/aHR0cHM6Ly93d3cu/a2luZHBuZy5jb20v/cGljYy9tLzQ4MC00/ODA5MTQwX3dpbmUt/Ym90dGxlLXBuZy1p/bWFnZS13aW5lLWJv/dHRsZS10cmFuc3Bh/cmVudC1iYWNrZ3Jv/dW5kLnBuZw",
                        ArticleId = 4,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 4)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 5,
                Name = "Cantine Astroni Colle Imperatrice Falanghina Campi Flegrei",
                Description =
                    "Un vin limpide et consistant, de couleur jaune paille avec des reflets dor??s et des reflets verts. Le nez est intense, fin et complexe et pr??sente des notes florales et fruit??es. Sec chaud et onctueux en bouche. Une bonne fra??cheur et sapidit??. Un vin ??quilibr?? et cors?? avec une belle persistance et intensit??.",
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
                        Url = "https://imgs.search.brave.com/2vLTCrFZgEWKcwaMnFG-s8vidGL9fHfWNRW7W26_S7c/rs:fit:960:1200:1/g:ce/aHR0cHM6Ly9wdXJl/cG5nLmNvbS9wdWJs/aWMvdXBsb2Fkcy9t/ZWRpdW0vcHVyZXBu/Zy5jb20tZ3JlZW4t/d2luZS1ib3R0bGVi/b3R0bGVuYXJyb3dl/cmphcmV4dGVybmFs/aW5uZXJzZWFsZ3Jl/ZW4tMTQyMTUyNjQ1/OTA0MGV0eXQ0LnBu/Zw",
                        ArticleId = 5,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 5)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 6,
                Name = "LES HAUTS DE SMITH",
                Description =
                    "Les Hauts de Smith est un vin rouge facile ?? boire, juteux, avec beaucoup de fra??cheur et un d??licieux fruit??. Nomm?? MEILLEUR VIN de son appellation, avec ses saveurs vanill??s lui apportant cette sucrosit??, voil?? une belle gourmandise ?? partager. Appelez vite les copains : c'est l'occasion r??v??e de les inviter sans vous ruiner ! Le petit vin plaisir par excellence...",
                Year = 2016,
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
                        Url = "https://imgs.search.brave.com/VA0aElWkPa5NK1gAdGRsG7cLjUMIpQEVweFdcGNBCqo/rs:fit:800:1200:1/g:ce/aHR0cHM6Ly9wdXJl/cG5nLmNvbS9wdWJs/aWMvdXBsb2Fkcy9t/ZWRpdW0vcHVyZXBu/Zy5jb20tc3Bhcmts/aW5nLXdpbmUtZnJv/bS1hLWJvdHRsZWFs/Y29ob2xkcmlua3Nw/YXJsaW5nLXdpbmVz/cGFybGluZy13aW4t/aW4tYS1ib3R0bGVi/b3R0bGUtMjMxNTE5/MzM5ODAzeXF3YWku/cG5n",
                        ArticleId = 6,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 6)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 7,
                Name = "LES FIEFS DE LAGRANGE",
                Description =
                    "M??lange subtil de fruits rouges et noirs, relev?? par quelques notes ??pic??es, le nez d??gage une ??l??gance et une pr??cision impressionnantes. La bouche est gourmande, onctueuse. Une fra??cheur agr??able assure un bon ??quilibre et une accessibilit?? ?? la d??gustation quasi imm??diate. Mais ne vous m??prenez pas, ce vin continuera de vous ??merveiller pendant les 20 prochaines ann??es !", 
                Year = 2019,
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
                        Url = "https://imgs.search.brave.com/yexHBw1EHKhYniwf9J_2NqhjDmVYg3fyDeUHRvxx0mQ/rs:fit:1170:1200:1/g:ce/aHR0cDovL2NsaXBh/cnQtbGlicmFyeS5j/b20vaW1hZ2VzX2sv/d2luZS1ib3R0bGUt/dHJhbnNwYXJlbnQt/YmFja2dyb3VuZC93/aW5lLWJvdHRsZS10/cmFuc3BhcmVudC1i/YWNrZ3JvdW5kLTE3/LnBuZw",
                        ArticleId = 7,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 7)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 8,
                Name = "CHATEAU TALBOT",
                Description =
                    "V??ritable trait d???union entre la finesse des Margaux et la puissance des Pauillac, cette propri??t?? parmi les plus justement populaires du M??doc vient de produire une s??rie de grands mill??simes dont le 2020 fait largement parti. D'une belle robe violet-noir profond, le nez d??gage des parfums de cassis, de framboise, de prunes cuites, d'herbes m??diterran??ennes s??ch??es et de feuille de tabac, avec une touche de graphite. En bouche on retrouve un Saint-Julien d'une belle fra??cheur, avec de la m??che, des fruits noirs, des notes d'herbes s??ch??es ainsi qu'une finale savoureuse.",
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
                        Url = "https://imgs.search.brave.com/--zUszdsdfBA7m1CEE6qilBKxxBCpt01n2pHBRmEk3o/rs:fit:720:1200:1/g:ce/aHR0cHM6Ly9sYXlt/YW5uZXdtZWRpYS5j/b20vaW1hZ2VzL3Bo/b3RvL2dyZWVuaGls/bC13aW5lcnktdmlu/ZXlhcmRzLWJvdHRs/ZS13aGl0ZS1iYWNr/Z3JvdW5kLWNoYXJk/b25uYXktNzIwLmpw/Zw",
                        ArticleId = 8,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 8)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 9,
                Name = "RESERVE DE LA COMTESSE",
                Description =
                    "Cet illustre cru class?? de Pauillac a depuis quelques ann??es d??marr?? une nouvelle vie, s???illustrant d??sormais sous le giron de la maison champenoise Roederer. Issu du m??me terroir et b??n??ficiant de la m??me technologie et de la m??me r??putation que le grand vin, la R??serve de la Comtesse figure parmi les seconds vins les plus r??put??s et les plus fiables de la rive gauche de la Gironde. Bien que moins charpent?? et moins apte au vieillissement que son a??n??, il promet cependant une fra??cheur, une complexit?? aromatique et un volume tout ?? fait remarquable. Le compromis id??al pour d??couvrir la grandeur des vins de Pauillac !",
                Year = 2019,
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
                        Url = "https://imgs.search.brave.com/YbW1d9hIkWVNqNpfVK_RKdA9zYdQu4aE60acI5RQG_o/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly9zbWFy/dHltb2NrdXBzLmNv/bS93cC1jb250ZW50/L3VwbG9hZHMvMjAx/Ni8wNi9XaGl0ZV9X/aW5lX0JvdHRsZV9N/b2NrdXBfMWEuanBn",
                        ArticleId = 9,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 9)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 10,
                Name = "Ch??teauneuf-du-Pape",
                Description =
                    "Ce vin rouge est produit dans la r??gion du Rh??ne m??ridional en France. Il est riche et cors??, avec des ar??mes de fruits rouges m??rs, d'??pices et de cuir. Pouilly-Fuiss?? : Ce vin blanc est produit dans la r??gion de la Bourgogne en France. Il est ??l??gant et fruit??, avec des notes de pomme verte, de citron et de noisette.",
                Year = 2020,
                Price = 6.50,
                Alcohol = 21.7,
                Stock = 150,
                ProviderId = 1,
                CategoryId = 1,
                Images = new List<Image>()
                {
                    new Image()
                    {
                        Id = 10,
                        Url = "https://imgs.search.brave.com/Bvikr3v0Bi4zYMD3cdtKXwBtlcjPkbojQs34cloZ9YY/rs:fit:800:1200:1/g:ce/aHR0cHM6Ly9wdXJl/cG5nLmNvbS9wdWJs/aWMvdXBsb2Fkcy9t/ZWRpdW0vcHVyZXBu/Zy5jb20td2luZS1i/b3R0bGVib3R0bGVu/YXJyb3dlcmphcmV4/dGVybmFsaW5uZXJz/ZWFsLTE0MjE1MjY0/NTg5OTR4dGV0ci5w/bmc",
                        ArticleId = 10,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 10)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 11,
                Name = "Margaux",
                Description = " Ce vin rouge est produit dans la r??gion de Bordeaux en France. Il est raffin?? et ??l??gant, avec des ar??mes de fruits rouges et de cassis, ainsi que des notes de ch??ne et de vanille.",
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
                        Url = "https://imgs.search.brave.com/J-IwvESq40rvK8Txy6MIFfmF-ngz5yeyVFYVpGE8_Ew/rs:fit:920:1200:1/g:ce/aHR0cDovL2NsaXBh/cnQtbGlicmFyeS5j/b20vbmV3X2dhbGxl/cnkvMjY2LTI2Njc4/MzhfYm90dGxlLW9m/LXNlcnJhbm8tMjAx/NC13aGl0ZS1ibGVu/ZC1saXF1aWQtbHVj/ay5wbmc",
                        ArticleId = 11,
                        Article = _context.Articles.FirstOrDefault(x => x.Id == 11)
                    },
                },
            });

            _context.Articles.Add(new Article
            {
                Id = 12,
                Name = "Sancerre",
                Description = "Ce vin blanc est produit dans la r??gion de la Loire en France. Il est frais et vif, avec des notes d'agrumes, de fruits de la passion et de fleurs blanches.",
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
                        Url = "https://imgs.search.brave.com/USemhQ7bMDBlOV3peEXkkIY5Nndw10aV_L2WsI8oVCA/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly9pNS53/YWxtYXJ0aW1hZ2Vz/LmNvbS9hc3IvYjUz/ZjY5ZWItMzRhOS00/ODJkLWJiOGQtZWZi/YWZhNDg3MDYzXzUu/NDY5YmJkNzU4ZjZh/NTc0NWUzNThlZWZl/ZmU3MDEwZWQuanBl/Zw",
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
                Url = "https://imgs.search.brave.com/9CgMMD4M-8_FyuBi7_rO5p5HDO3r4mla7RA_ElIXfDU/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly93d3cu/YXBlbW9ja3Vwcy5j/b20vd3AtY29udGVu/dC91cGxvYWRzL2Vk/ZC8yMDIwLzA5L0Zy/ZWUtV2hpdGUtV2lu/ZS1Cb3R0bGUtTW9j/a3VwLTEucG5n",
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