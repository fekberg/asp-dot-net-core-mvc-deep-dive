namespace Globomatics.Web.Models;

public class CreateOrderModel
{
    public Guid? CartId { get; set; }
    public required CustomerModel Customer { get; init; } 
}
