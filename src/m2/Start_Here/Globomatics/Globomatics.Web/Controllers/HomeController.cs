using Globomantics.Domain.Models;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Globomatics.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult TicketDetails(Guid productId, string slug)
    {
        throw new NotImplementedException();
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