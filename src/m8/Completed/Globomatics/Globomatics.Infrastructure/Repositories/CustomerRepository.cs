using Globomantics.Domain.Models;
using Globomantics.Infrastructure.Data;

namespace Globomatics.Infrastructure.Repositories;

public class CustomerRepository : GenericRepository<Customer>
{
    public CustomerRepository(GlobomanticsContext context) : base(context)
    {
    }
}
