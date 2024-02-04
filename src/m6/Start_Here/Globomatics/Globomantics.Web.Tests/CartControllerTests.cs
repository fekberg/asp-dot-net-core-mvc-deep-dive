using Globomantics.Domain.Models;
using Globomantics.Infrastructure.Data;
using Globomantics.Web.Tests.Repositories;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Controllers;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Globomantics.Web.Tests;

[TestClass]
public class CartControllerTests
{
    private GlobomanticsContext context = default!;
    private ILogger<CartController> logger = default!;

    [TestInitialize]
    public void Setup()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging(options => options.AddDebug())
            .BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<ILoggerFactory>();

        logger = factory.CreateLogger<CartController>();

        context = new GlobomanticsContext();

        context.Database.EnsureDeleted();

        context.Database.Migrate();

        context.Products.Add(new Product { ProductId = Guid.Parse("4bc34cb4-c16e-4172-97af-4f90d2c494ec"), Name = "Alexander Lemtov Live", Price = 65m });
        context.Products.Add(new Product { ProductId = Guid.Parse("cda496ae-ec4d-410f-8bcd-26aaca5ba9da"), Name = "To The Moon And Back", Price = 135m });
        context.Products.Add(new Product { ProductId = Guid.Parse("92bc5f1c-0851-4fbb-931a-d6f807aae99a"), Name = "The State Of Affairs: Mariam Live!", Price = 85m });

        context.SaveChanges();
    }

    [TestMethod]
    public void Cart_Update_With_Empty_CartId_Should_Create_New_Cart()
    {
        var cartController = new CartController(
            new CartRepository(context),
            new CustomerRepository(context),
            new OrderRepository(context),
            // new InMemoryStateRepository(),
            logger);

        var model = new AddToCartModel
        {
            CartId = null,
            Product = new ProductModel
            {
                ProductId = Guid.Parse("4bc34cb4-c16e-4172-97af-4f90d2c494ec"),
                Quantity = 99
            }
        };

        var result = cartController.AddToCart(model);

        Assert.AreEqual(99, context.Carts.Sum(x => x.LineItems.Sum(x => x.Quantity)));

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
    }
}