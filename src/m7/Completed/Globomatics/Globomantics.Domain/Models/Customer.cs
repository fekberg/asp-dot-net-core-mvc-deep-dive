namespace Globomantics.Domain.Models;

public class Customer
{
    public Guid CustomerId { get; set; }

    public required string Name { get; set; }
    public required string ShippingAddress { get; set; }
    public required string Email { get; set; }
    public required string City { get; set; }
    public required string PostalCode { get; set; }
    public required string Country { get; set; }

    public Customer()
    {
        CustomerId = Guid.NewGuid();
    }
}
