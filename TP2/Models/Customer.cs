namespace TP2.Models;

public class Customer
{
    public int Id { get; set; }
    public required string Name { get; set; }

    public int MembershipTypeId { get; set; }

    public MembershipType? MembershipType { get; set; }

    // Newsletter subscription flag
    public bool IsSubscribed { get; set; }
}
