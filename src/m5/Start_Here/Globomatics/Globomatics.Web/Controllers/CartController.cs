using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Globomatics.Web.Controllers;

[Route("[controller]")]
public class CartController : Controller
{
    private readonly ILogger<CartController> logger;
    private readonly ICartRepository cartRepository;

    public CartController(ILogger<CartController> logger,
        ICartRepository cartRepository)
    {
        this.logger = logger;
        this.cartRepository = cartRepository;
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

        return RedirectToAction("Index", "Cart");
    }

    [HttpPost]
    public IActionResult Update(UpdateQuantitiesModel updateQuantitiesModel)
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult Create(CreateOrderModel createOrderModel)
    {
        throw new NotImplementedException();
    }

    [Route("ThankYou")]
    public IActionResult ThankYou()
    {
        return View();
    }
}
