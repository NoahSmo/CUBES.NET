using System.Collections.Generic;

namespace Api.Models;

public class Status : Auditable
{
    public int Id { get; set; }
    public string Message { get; set; }
    public virtual List<Order> Orders { get; set; }
}