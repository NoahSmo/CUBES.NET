namespace Api.Models;

public class Cart
{
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public int ArticleId { get; set; }
    public Article? Article { get; set; }
    
    public int Quantity { get; set; }
}