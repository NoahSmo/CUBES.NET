namespace Api.Models;

public class DataSeeder
{
    private readonly DataContext _context;
    
    public DataSeeder(DataContext context)
    {
        _context = context;
    }
    
    public void Seed()
    {
        if (!_context.Users.Any())
        {
            _context.Users.Add(new User
            {
                Id = 1,
                Username = "JohnDoe",
                Name = "John",
                Surname = "Doe",
                Email = "John.doe@gmail.com",
                IsAdmin = true,
                Password = "Test",
            });
            _context.SaveChanges();
        }

        if (!_context.Providers.Any())
        {
            _context.Providers.Add(new Provider
            {
                Id = 1,
                Name = "Provider 1",
                Email = "Provider1@gmail.com",
                Phone = "123456789",
            });
        }
        
        if (!_context.Categories.Any())
        {
            _context.Categories.Add(new Category
            {
                Id = 1,
                Name = "Provider 1",
                Description = "Random description for a category"
            });
        }
        
        if (!_context.Articles.Any())
        {
            _context.Articles.Add(new Article
            {
                Id = 1,
                Name = "Product 1",
                Description = "Product 1 description",
                Year = "2001",
                Price = "10",
                ProviderId = 1,
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
                Date = DateTime.Now,
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
                    }
                }
            });
        }
        
        _context.SaveChanges();
    }
}