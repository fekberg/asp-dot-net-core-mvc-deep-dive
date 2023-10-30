using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Globomatics.Web.Attributes;

public class SessionValueProvider : BindingSourceValueProvider
{
    private readonly ISession session;

    public SessionValueProvider(BindingSource bindingSource, ISession session) : base(bindingSource)
    {
        this.session = session;
    }

    public override bool ContainsPrefix(string prefix)
    {
        return session.Get(prefix)?.Any() ?? false;
    }

    public override ValueProviderResult GetValue(string key)
    {
        if(string.IsNullOrWhiteSpace(key))
        {
            return ValueProviderResult.None;
        }

        if(session.Keys.Contains(key))
        {
            return new ValueProviderResult(session.GetString(key));
        }

        return ValueProviderResult.None;
    }
}
