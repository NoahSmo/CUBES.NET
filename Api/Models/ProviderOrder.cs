namespace Api.Models;

public class ProviderOrder : Auditable
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
        
    public int ProviderId { get; set; }
    public Provider? Provider { get; set; }
    
    public int StatusId { get; set; }
    public Status? Status { get; set; }
        
    public virtual List<ArticleOrder>? ArticleOrders { get; set; }
}