using Globomantics.Domain.Models;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Globomatics.Web.Controllers;

[Route("[controller]")]
public class CartController : Controller
{
    private readonly IStateRepository stateRepository;
    private readonly ICartRepository cartRepository;
    private readonly IRepository<Customer> customerRepository;
    private readonly IRepository<Order> orderRepository;
    private readonly ILogger<CartController> logger;

    public CartController(ICartRepository cartRepository,
        IRepository<Customer> customerRepository,
        IRepository<Order> orderRepository,
        ILogger<CartController> logger,
        IStateRepository stateRepository)
    {
        this.cartRepository = cartRepository;
        this.customerRepository = customerRepository;
        this.orderRepository = orderRepository;
        this.logger = logger;
        this.stateRepository = stateRepository;
    }

    public IActionResult Index(Guid? id)
    {
        return View();

    }

    [HttpPost]
    [Route("Add")]
    public IActionResult AddToCart(AddToCartModel addToCartModel)
    {
        if (addToCartModel.Product is null)
        {
            return BadRequest();
        }

        logger.LogInformation($"Adding products " +
            $"{addToCartModel.Product.ProductId} to cart " +
        $"{addToCartModel.CartId}");

        var cart = cartRepository.CreateOrUpdate(addToCartModel.CartId, 
            addToCartModel.Product.ProductId, 
            addToCartModel.Product.Quantity);

        cartRepository.SaveChanges();

        stateRepository.SetValue("NumberOfItems", 
            cart.LineItems.Sum(x => x.Quantity).ToString());

        stateRepository.SetValue("CartId", 
            cart.CartId.ToString()!);

        return RedirectToAction("Index", "Cart");
    }

    [HttpPost]
    [Route("Update")]
    // [ValidateAntiForgeryToken]
    public IActionResult Update([FromBody]UpdateQuantitiesModel updateQuantitiesModel)
    {
        if (updateQuantitiesModel.Products is null)
        {
            return BadRequest();
        }

        Cart cart = null!;

        foreach (var product in updateQuantitiesModel.Products)
        {
            logger.LogInformation($"Adding products {product.ProductId} to cart {updateQuantitiesModel.CartId}");

            cart = cartRepository.CreateOrUpdate(updateQuantitiesModel.CartId,
                product.ProductId, product.Quantity);
        }

        cartRepository.SaveChanges();

        stateRepository.SetValue("NumberOfItems",
            cart.LineItems.Sum(x => x.Quantity).ToString());

        stateRepository.SetValue("CartId",
            cart.CartId.ToString()!);

        return RedirectToAction("Index", "Cart");
    }

    [HttpPost]
    [Route("Finalize")]
    [ValidateAntiForgeryToken]
    public IActionResult Create([FromBody]CreateOrderModel createOrderModel)
    {
        if (createOrderModel.Customer is null)
        {
            ModelState.AddModelError("Customer", 
                "Customer data is not set correctly");

            return View("Index");
        }

        if (createOrderModel.Customer.Name.Length <= 2)
        {
            ModelState.AddModelError(nameof(createOrderModel.Customer.Name), 
                "Too short name.");

            return View("Index");
        }

        if(!ModelState.IsValid)
        {
            return View("Index");
        }

        var customer = new Customer
        {
            Email = createOrderModel.Customer.Email,
            Name = createOrderModel.Customer.Name,
            City = createOrderModel.Customer.City,
            Country = createOrderModel.Customer.Country,
            ShippingAddress = createOrderModel.Customer.ShippingAddress,
            PostalCode = createOrderModel.Customer.PostalCode,
        };

        logger.LogInformation($"Creating a new order for {customer.CustomerId}");

        customerRepository.Add(customer);

        var order = new Order
        {
            CustomerId = customer.CustomerId
        };

        if (createOrderModel.CartId is null || createOrderModel.CartId == Guid.Empty)
        {
            ModelState.AddModelError("Cart", "Cart has been deleted");

            return View("Index");
        }

        var cart = cartRepository.Get(createOrderModel.CartId.Value);

        if (cart is null)
        {
            ModelState.AddModelError("Cart", "Cart has been deleted");

            return View("Index");
        }

        foreach (var lineItem in cart.LineItems)
        {
            order.LineItems.Add(lineItem);
        }

        orderRepository.Add(order);

        cartRepository.Update(cart);

        cartRepository.SaveChanges();

        logger.LogInformation($"Order placed for {customer.CustomerId}");

        stateRepository.Remove("NumberOfItems");
        stateRepository.Remove("CartId");

        return RedirectToAction("ThankYou");
    }

    [Route("ThankYou")]
    public IActionResult ThankYou()
    {
        return View();
    }
}
