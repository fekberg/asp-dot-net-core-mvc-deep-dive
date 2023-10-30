using Globomatics.Web.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Globomatics.Web.Controllers;

public class LoginController : Controller
{
    [Route("/login")]
    public IActionResult Index()
    {
        return View();
    }

    [AllowAnonymous]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Route("/login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        if (loginModel.Username == "filip@ekberg.dev")
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, loginModel.Username),
                new Claim(ClaimTypes.Role, "Administrator"),
            };

            var claimsIdentity = new ClaimsIdentity(claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Customers");
        }

        return View();
    }

    [AllowAnonymous]
    [Route("/logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        return RedirectToAction("Index");
    }
}
