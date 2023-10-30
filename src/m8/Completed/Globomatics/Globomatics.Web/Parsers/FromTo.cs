namespace Globomatics.Web.Parsers;

public class FromTo : IParsable<FromTo>
{
    public int From { get; set; }
    public int To { get; set; }

    public static FromTo Parse(string input, IFormatProvider? provider)
    {
        TryParse(input, provider, out var result);

        return result;
    }

    public static bool TryParse(string? input, 
        IFormatProvider? provider, out FromTo result)
    {
        var segments = input.Split('|');

        result = new FromTo // Default value
        {
            From = 0,
            To = 0
        };

        if (segments.Length > 2 ||
            !int.TryParse(segments[0], out var from) ||
            !int.TryParse(segments[1], out var to))
        {
            return false;
        }

        result.From = from;
        result.To = to;

        return true;
    }
}
