using Globomantics.Domain.Models;
using Globomantics.Infrastructure.Data;

namespace Globomatics.Infrastructure.Repositories;

public class OrderRepository : GenericRepository<Order>
{
    public OrderRepository(GlobomanticsContext context) : base(context)
    {
    }
}
