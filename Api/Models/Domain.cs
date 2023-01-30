using Api.ViewModels;

namespace Api.Models;

public class Domain : Auditable
{
   public int Id { get; set; }
   
   public string Name { get; set; }
   public string Description { get; set; }
   
   public virtual List<AddressViewModel>? Addresses { get; set; }
}