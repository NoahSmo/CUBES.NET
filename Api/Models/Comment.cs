namespace Api.Models;

public class Comment
{
    public int Id { get; set; }
    public int Rating { get; set; }
    public string Message { get; set; }
    
    public int ArticleId { get; set; }
    public Article Article { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }
}