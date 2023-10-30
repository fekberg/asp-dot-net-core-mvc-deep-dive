using Globomatics.Infrastructure.Repositories;

namespace Globomatics.Web.Repositories;

public class SessionStateRepository : IStateRepository
{
    private readonly IHttpContextAccessor httpContextAccessor;

    public SessionStateRepository(IHttpContextAccessor httpContextAccessor)
    {
        this.httpContextAccessor = httpContextAccessor;
    }
    public string GetValue(string key)
    {
        return httpContextAccessor.HttpContext?.Session?.GetString(key) 
            ?? string.Empty;
    }

    public void SetValue(string key, string value)
    {
        httpContextAccessor.HttpContext?.Session?.SetString(key, value);
    }

    public void Remove(string key)
    {
        httpContextAccessor.HttpContext?.Session?.Remove(key);
    }
}
