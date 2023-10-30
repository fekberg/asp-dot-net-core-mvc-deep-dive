using Globomantics.Domain.Models;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Globomatics.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;

    private readonly IRepository<Product> productRepository;

    public HomeController(IRepository<Product> productRepository, 
        ILogger<HomeController> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Route("/details/{productId:guid}/{slug:slugTransform}")]
    public IActionResult TicketDetails(Guid productId, 
        [RegularExpression("^[a-zA-Z0-9- ]+$")] string slug)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest();
        }

        var product = productRepository.Get(productId);

        return View(product);
    }

    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}