using System.Collections.Generic;

namespace Api.Models;

public class Cart
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
    
    public virtual List<CartItem?> CartItems { get; set; }
}