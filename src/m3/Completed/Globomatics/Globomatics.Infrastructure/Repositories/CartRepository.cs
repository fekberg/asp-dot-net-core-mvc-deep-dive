namespace Globomatics.Infrastructure.Repositories;

using Globomantics.Domain.Models;
using Globomantics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class CartRepository : GenericRepository<Cart>, ICartRepository
{
    public CartRepository(GlobomanticsContext context)
        : base(context)
    {
    }

    public Cart CreateOrUpdate(Guid? cartId, Guid productId, int quantity = 1)
    {
        (var cart, var isNewCart) = GetCart(cartId);

        AddProductToCart(cart, productId, quantity);

        if (isNewCart)
        {
            context.Add(cart);
        }
        else
        {
            context.Update(cart);
        }

        return cart;
    }

    private (Cart cart, bool isNewCart) GetCart(Guid? cartId)
    {
        Cart? cart = null;
        bool isNewCart = false;

        if (cartId is not null || cartId == Guid.Empty)
        {
            cart = context.Carts
                .Include(x => x.LineItems)
                .FirstOrDefault(x => x.CartId == cartId);
        }

        if (cart is null)
        {
            isNewCart = true;
            cart = new();
        }

        return (cart, isNewCart);
    }

    private void AddProductToCart(Cart cart, Guid productId, int quantity)
    {
        var lineItem = cart.LineItems.FirstOrDefault(x => x.ProductId == productId);

        if (lineItem is not null && quantity == 0)
        {
            cart.LineItems.Remove(lineItem);
        }
        else if (lineItem is not null)
        {
            lineItem.Quantity = quantity;
        }
        else
        {
            lineItem = new() { ProductId = productId, Quantity = quantity };

            context.LineItems.Add(lineItem);

            cart.LineItems.Add(lineItem);
        }
    }
}