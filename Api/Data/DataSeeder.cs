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
            _context.Domains.RemoveRange(_context.Domains);
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
                    City =  "Novi Sad",
                    Country = "Srbija",
                    ZipCode = 27000,
                    UserId = 1,
                    User = _context.Users.FirstOrDefault(x => x.Id == 1)
                });
                
                _context.Addresses.Add(new Address()
                {
                    Id = 2,
                    Street = "Sdqsdazqsdqsqwxc",    
                    City =  "Novi Sad",
                    Country = "Srbija",
                    ZipCode = 21000,
                });
                
                _context.SaveChanges();
            }
            
            if (!_context.Domains.Any())
            {
                _context.Domains.Add(new Domain
                {
                    Id = 1,
                    Name = "Domaine de Tariquet",
                    Description = "Le domaine du tariquet est producteur de vin blanc",
                    Email = "Tariquet.dom@gmail.com",
                });
                _context.Domains.Add(new Domain
                {
                    Id = 2,
                    Name = "Domaine de Joy",
                    Description = "Le domaine de Joy est producteur de vin rouge",
                    Email = "Joy.dom@gmail.com",
                });
                _context.Domains.Add(new Domain
                {
                    Id = 3,
                    Name = "Vignoble Fontan",
                    Description = "Le Vignoble Fontan est producteur de vin rosé",
                    Email = "Vignoble.Fontan@gmail.com",
                });
                _context.Domains.Add(new Domain
                {
                    Id = 4,
                    Name = "Domaine d'Uby",
                    Description = "Le domaine d'uby est producteur de vin bio",
                    Email = "Uby.dom@gmail.com",
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
                    Name = "Provider 1",
                    Email = "EmailProvider@gmail.com",
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
                    CategoryId = 1,
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
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 3,
                    Name = "Le Vin de Gascogne",
                    Description =   "Le Vin de Gascogne est un vin blanc sec, fruité et frais, à la robe jaune pâle. Il est élaboré à partir de cépages locaux, le colombard et le gros manseng. Il est issu de raisins issus de vignes de plus de quarante ans, Les Darons (les Pères en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1, 
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 4,
                    Name = "Les Daronnes",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Les Daronnes (les Mères en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1, 
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 5,
                    Name = "Los Muchachos",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Los Muchachos (les Enfants en argot) porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 6,
                    Name = "Le Canard",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Canard porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 7,
                    Name = "Le Chien",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Chien porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 8,
                    Name = "Le Chat",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Chat porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 9,
                    Name = "Le Cheval",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Cheval porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 10,
                    Name = "Le Cochon",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Cochon porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 11,
                    Name = "La Chaise",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, La Chaise porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });
                
                _context.Articles.Add(new Article
                {
                    Id = 12,
                    Name = "Le Lit",
                    Description = "Composé en majorité de raisins issus de vignes de plus de quarante ans, Le Lit porte bien son nom ! Charmeur avec son nez fruité et épicé, équilibré et puissant, il possède des nuances légèrement toastées (bien qu’élevé en fût) qui apportent un relief et une générosité des plus appréciables. Un vin solide et sûr de lui qui s’adresse à ceux qui ont suffisamment de bouteille pour apprécier les bonnes choses de la vie !",
                    Year = 2020,
                    Price = 6.50,
                    Alcohol = 25.7,
                    Stock = 150,
                    DomainId = 1,
                    CategoryId = 1,
                });

                _context.SaveChanges();
            }
            
            if (!_context.Images.Any())
            {
                _context.Images.Add(new Image
                {
                    Id = 1,
                    Url = "https://imgs.search.brave.com/Xvh4r9QThnPOf01buIUk4mbNPEskdReXoNARsBrx5WM/rs:fit:1200:1200:1/g:ce/aHR0cDovL3d3dy5u/YXBhd2luZXByb2pl/Y3QuY29tL3dwLWJs/b2cvd3AtY29udGVu/dC91cGxvYWRzLzIw/MTgvMDcvVHJlZWJv/dHRvbS1XaW5lcy0x/LmpwZw",
                    ArticleId = 1
                });
                
                _context.Images.Add(new Image
                {
                    Id = 2,
                    Url = "https://imgs.search.brave.com/Ev-HhePgVX2L08lDPd3wu-DV9vD-R3DzvEBcDqWvx38/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly93d3cu/b2phbGF3aW5lLmNv/bS93cC1jb250ZW50/L3VwbG9hZHMvMjAx/Ny8wMS9PamFsYVdp/bmVfQ2FiZXJuZXQt/U2F1dmlnbm9uLmpw/Zw",
                    ArticleId = 2
                });
                
                _context.Images.Add(new Image
                {
                    Id = 3,
                    Url = "https://imgs.search.brave.com/tuOZgUejfKOKqihFFfORYdN3YJOq0jBhTOOB4UOiykY/rs:fit:1130:1200:1/g:ce/aHR0cHM6Ly9zMy5h/bWF6b25hd3MuY29t/L2ltYWdlcy5lY3dp/ZC5jb20vaW1hZ2Vz/LzMyMjc3MDI0LzE1/MDE2OTU2MjQuanBn",
                    ArticleId = 3
                });
                
                _context.Images.Add(new Image
                {
                    Id = 4,
                    Url = "https://imgs.search.brave.com/nTEQnm1AYF41JEzaXnvV4X6keiwnnxK2ugUw0BPg1D8/rs:fit:1000:667:1/g:ce/aHR0cHM6Ly9kMmhn/OGN0eDh0aHpqaS5j/bG91ZGZyb250Lm5l/dC9oZWFsdGhhY2Nl/c3MuY29tL3dwLWNv/bnRlbnQvdXBsb2Fk/cy8yMDE4LzEwLzdf/TXVzdC1Lbm93X0hl/YWx0aF9CZW5lZml0/c19PZl9XaW5lLmpw/Zw",
                    ArticleId = 4
                });
                
                _context.Images.Add(new Image
                {
                    Id = 5,
                    Url = "https://imgs.search.brave.com/N9trEeJv3KKJT3bmwb7MqtrHscn8yebZPvCQrg2VAes/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly93d3cu/ZHJpbmtoYWNrZXIu/Y29tL3dwLWNvbnRl/bnQvdXBsb2Fkcy8y/MDE5LzA1L2NvcHBv/bGEtZWxlYW5vci5q/cGVn",
                    ArticleId = 5
                });
                
                _context.Images.Add(new Image
                {
                    Id = 6,
                    Url = "https://imgs.search.brave.com/1yLaVVbOb2lLFiOV1LaZEG19Cx_FHgm6StA625dst58/rs:fit:800:1200:1/g:ce/aHR0cHM6Ly9pbWFn/ZXMuY3RmYXNzZXRz/Lm5ldC8zczVpbzZt/bnhmcXovNlprcjky/MU9jUVE1YnpkVGRi/ZDA0aC9jZGVjNGZl/MTliYjZiZDFmZGU1/ZDQ3NjhhMTliZjZk/NS9BZG9iZVN0b2Nr/XzE0MzA3NzA3Lmpw/ZWc_Zm09anBnJnc9/ODAwJmZsPXByb2dy/ZXNzaXZl",
                    ArticleId = 6
                });
                
                _context.Images.Add(new Image
                {
                    Id = 7,
                    Url = "https://imgs.search.brave.com/3FAKN3QwoOSrytjzZcqFFRtKoT67jt1KxkXmzGky8js/rs:fit:1152:1200:1/g:ce/aHR0cHM6Ly9sdXh1/cnl5YWNodGNoYXJ0/ZXJzLmNvbS93cC1j/b250ZW50L3VwbG9h/ZHMvMjAyMC8xMS9t/YXJzYWxhLXdpbmUt/Mi0xMTUyeDE1MzYu/anBn",
                    ArticleId = 7
                });
                
                _context.Images.Add(new Image
                {
                    Id = 8,
                    Url = "https://imgs.search.brave.com/6dkhKLLZLsYgJrLFlUV8t_qqzjYVSAPU2SIy83SIhEg/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly9tZWRp/YS5iYnIuY29tL3Mv/YmJyLzIwMTY4MDE4/OTc5LW1zP2ltZzQw/ND1EZWZhdWx0X1dp/bmU",
                    ArticleId = 8
                });
                
                _context.Images.Add(new Image
                {
                    Id = 9,
                    Url = "https://imgs.search.brave.com/4aCM4HOOiSLVxkClRu40_7hyUrupGUwJdGLxZaUZ6hc/rs:fit:600:1200:1/g:ce/aHR0cHM6Ly93d3cu/dGFzdGluZ3MuY29t/L1Byb2R1Y3QtSW1h/Z2VzL1dpbmUvMjAx/OC82XzRfMjAxOC8y/MjQ3NjNfZnMuanBn",
                    ArticleId = 9
                });
                
                _context.Images.Add(new Image
                {
                    Id = 10,
                    Url = "https://imgs.search.brave.com/C7ADzLRMg5ToGJaH3feMF2qQoqxmEgF8pQdm55KIvsc/rs:fit:1200:1200:1/g:ce/aHR0cHM6Ly93d3cu/bmFwYXdpbmVwcm9q/ZWN0LmNvbS93cC1i/bG9nL3dwLWNvbnRl/bnQvdXBsb2Fkcy8y/MDEzLzEwL05hcGEt/Q2VsbGFycy0xLmpw/Zw",
                    ArticleId = 10
                });
                
                _context.Images.Add(new Image
                {
                    Id = 11,
                    Url = "https://imgs.search.brave.com/11g8IJDp7UcSN12isMShNixUSmk2wxyK6-CmRXOn4Tk/rs:fit:1080:628:1/g:ce/aHR0cHM6Ly93d3cu/b25seWZvb2RzLm5l/dC93cC1jb250ZW50/L3VwbG9hZHMvMjAy/MC8wNy9Nb3NjYXRv/LmpwZw",
                    ArticleId = 11
                });
                
                _context.Images.Add(new Image
                {
                    Id = 12,
                    Url = "https://imgs.search.brave.com/55F4oHvViDUIMRsvKmfOV71DSZfdSvWz5YwxcwvVcok/rs:fit:776:1176:1/g:ce/aHR0cDovL3d3dy5j/ZWx0aWN3aGlza2V5/c2hvcC5jb20vaW1h/Z2UvSXJpc2gtQmxh/Y2tiZXJyeS13aW5l/LmpwZw",
                    ArticleId = 12
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