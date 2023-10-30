using System.Text.RegularExpressions;

namespace Globomatics.Web.Transformers;

public class SlugParameterTransformer : IOutboundParameterTransformer
{
    public string? TransformOutbound(object? value)
    {
        if(value is not string)
        {
            return null;
        }

        return Regex.Replace(value.ToString()!, @"[^a-zA-Z0-9]+", "-",
            RegexOptions.CultureInvariant,
            TimeSpan.FromMilliseconds(200)).ToLowerInvariant().Trim('-');
    }
}
