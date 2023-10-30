namespace Globomatics.Web.Models;

public class UpdateQuantitiesModel
{
    public Guid? CartId { get; set; }

    public IEnumerable<ProductModel>? Products { get; set; }
}