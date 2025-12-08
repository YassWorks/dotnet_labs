namespace TP2.Models;

public class MembershipType
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public short SignUpFee { get; set; }
    public byte DurationInMonth { get; set; }
    public byte DiscountRate { get; set; }

    public ICollection<Customer>? Customers { get; set; }
}
