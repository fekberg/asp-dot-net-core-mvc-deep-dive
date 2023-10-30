using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace Globomatics.Web.Controllers;

public class CartController : Controller
{
    public IActionResult Index(Guid? id)
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddToCart(AddToCartModel addToCartModel)
    {
        throw new NotImplementedException();
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

    public IActionResult ThankYou()
    {
        return View();
    }
}
