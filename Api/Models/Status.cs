namespace Api.Models;

public class Status : Auditable
{
    public int Id { get; set; }
    public string Message { get; set; }
}