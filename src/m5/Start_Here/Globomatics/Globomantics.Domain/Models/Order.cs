namespace Globomantics.Domain.Models;

public class Order
{
    public Guid OrderId { get; init; }

    public virtual ICollection<LineItem> LineItems { get; set; }

    public virtual Customer? Customer { get; set; }
    public required Guid CustomerId { get; set; }

    // SQLite doesn't support DateTimeOffset, we have to ensure dates are always UTC!
    public DateTime CreatedAt { get; set; }

    public decimal OrderTotal => LineItems?.Sum(item => item.Product?.Price * item.Quantity) ?? 0;

    public Order()
    {
        OrderId = Guid.NewGuid();

        CreatedAt = DateTime.UtcNow;

        LineItems = new List<LineItem>();
    }
}