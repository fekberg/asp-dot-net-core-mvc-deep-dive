using System.Globalization;
using System.Text.RegularExpressions;

namespace Globomatics.Web.Constraints;

public class SlugConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, 
        IRouter? route, 
        string routeKey,
        RouteValueDictionary values, 
        RouteDirection routeDirection)
    {
        if(!values.TryGetValue("slug", out var slug))
        {
            return false;
        }

        var slugAsString = Convert.ToString(slug,
            CultureInfo.InvariantCulture);

        if(string.IsNullOrWhiteSpace(slugAsString))
        {
            return false;
        }

        return Regex.IsMatch(slugAsString, "^[a-zA-Z0-9- ]+$");
    }
}
