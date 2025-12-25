namespace TP2.Models;

public class UserCart
{
    public Guid Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public Guid ProductId { get; set; }
    public List<Product> Products { get; set; } = new List<Product>();
}
