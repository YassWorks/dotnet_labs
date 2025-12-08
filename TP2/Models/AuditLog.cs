namespace TP2.Models;

public class AuditLog
{
    public int Id { get; set; }
    public required string TableName { get; set; }
    public required string Action { get; set; } // "Added", "Modified", "Deleted"
    public required string EntityKey { get; set; }
    public string? Changes { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
