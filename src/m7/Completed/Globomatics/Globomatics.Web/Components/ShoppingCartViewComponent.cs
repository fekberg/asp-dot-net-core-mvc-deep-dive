using Globomantics.Infrastructure.Data;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Globomatics.Web.Components;

public class ShoppingCartViewComponent : ViewComponent
{
    private readonly GlobomanticsContext context;
    private readonly IStateRepository stateRepository;

    public ShoppingCartViewComponent(GlobomanticsContext context, 
        IStateRepository stateRepository)
    {
        this.context = context;
        this.stateRepository = stateRepository;
    }

    public async Task<IViewComponentResult> InvokeAsync(string cartId, 
        bool isCompact)
    {
        if (!Guid.TryParse(cartId, out var id))
        {
            return View(new ShoppingCartModel { IsCompact = isCompact });
        }

        var cart = await context.Carts
            .Include(x => x.LineItems)
            .ThenInclude(x => x.Product)
            .FirstOrDefaultAsync(x => x.CartId == id);

        if (cart is not null)
        {
            stateRepository.SetValue("NumberOfItems", 
                cart.LineItems.Sum(x => x.Quantity).ToString());

            stateRepository.SetValue("CartId", 
                cart.CartId.ToString());
        }

        return View(new ShoppingCartModel { Cart = cart, IsCompact = isCompact });
    }
}
