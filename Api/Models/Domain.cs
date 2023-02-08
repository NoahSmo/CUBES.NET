using Api.ViewModels;

namespace Api.Models;

public class Domain : Auditable
{
   public int Id { get; set; }
   
   public string Name { get; set; }
   public string Description { get; set; }
   public string Email { get; set; }
   
   public virtual List<Address>? Addresses { get; set; }

   public virtual List<Provider>? Providers { get; set; }
   
   public virtual List<Article>? Articles { get; set; }
}