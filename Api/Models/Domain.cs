namespace Api.Models;

public class Domain
{
   public int Id { get; set; }
   
   public string Name { get; set; }
   public string Description { get; set; }
   
   public virtual List<Address>? Addresses { get; set; }
}