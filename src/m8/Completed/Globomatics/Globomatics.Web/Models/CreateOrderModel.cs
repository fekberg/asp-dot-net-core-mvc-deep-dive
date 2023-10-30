using System.Diagnostics.CodeAnalysis;

namespace Globomatics.Web.Models;

public class CartModel
{
    public Guid? CartId { get; set; }
    public Guid? CustomerId { get; set; }
}
public class Product2
{
    public Guid? ProductId { get; set; }
    public decimal Price { get; set; }
}

public class CreateOrderModel
{
    public Guid? CartId { get; set; }
    public required CustomerModel Customer { get; init; } 
}