namespace Globomantics.Domain.Models;

public class LineItem
{
    public Guid LineItemId { get; set; }
    
    public required int Quantity { get; set; }

    public virtual Product? Product { get; set; }
    public required Guid ProductId { get; set; }

    public LineItem()
    {
        LineItemId = Guid.NewGuid();
    }
}