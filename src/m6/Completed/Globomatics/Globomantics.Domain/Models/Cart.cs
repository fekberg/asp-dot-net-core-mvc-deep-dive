namespace Globomantics.Domain.Models;

public class Cart
{
    public Guid CartId { get; init; }
    public bool Finalized { get; set; }
    public string? CustomerEmail { get; set; }
    public DateTime CreatedAt { get; set; }
    public virtual ICollection<LineItem> LineItems { get; set; }

    public Cart()
    {
        CartId = Guid.NewGuid();

        LineItems = new List<LineItem>();

        CreatedAt = DateTime.UtcNow;
    }
}