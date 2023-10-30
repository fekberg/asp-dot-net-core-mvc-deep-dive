using Globomantics.Infrastructure.Data;

namespace Globomatics.Infrastructure.Repositories;

public abstract class GenericRepository<T>
    : IRepository<T> where T : class
{
    protected GlobomanticsContext context;

    public GenericRepository(GlobomanticsContext context)
    {
        this.context = context;
    }

    public virtual T Add(T entity)
    {
        var addedEntity = context.Add(entity).Entity;

        return addedEntity;
    }

    public virtual IEnumerable<T> All()
    {
        var all = context.Set<T>().ToList();

        return all;
    }

    public virtual T? Get(Guid id)
    {
        return context.Find<T>(id);
    }

    public virtual void SaveChanges()
    {
        context.SaveChanges();
    }

    public virtual T Update(T entity)
    {
        return context.Update(entity).Entity;
    }
}

