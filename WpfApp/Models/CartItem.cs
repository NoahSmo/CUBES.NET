namespace Api.Models;

public class CartItem
{
    public int Id { get; set; }
    
    public int Quantity { get; set; }
    
    public int ArticleId { get; set; }
    public Article? Article { get; set; }
    
    public int CartId { get; set; }
    public Cart? Cart { get; set; }
}