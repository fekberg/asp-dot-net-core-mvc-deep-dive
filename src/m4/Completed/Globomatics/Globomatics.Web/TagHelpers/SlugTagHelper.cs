using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text.RegularExpressions;

namespace Globomatics.Web.TagHelpers;

[HtmlTargetElement("url-with-slug")]
public class SlugTagHelper : AnchorTagHelper
{
    public SlugTagHelper(IHtmlGenerator generator) : base(generator) { }

    [HtmlAttributeName("for-product-id")]
    public Guid ProductId { get; set; }

    [HtmlAttributeName("for-ticket-name")]
    public required string TicketTitle { get; set; }

    public override void Process(TagHelperContext context, 
        TagHelperOutput output)
    {
        output.TagName = "a";
        output.TagMode = TagMode.StartTagAndEndTag;

        var slug = Regex.Replace(TicketTitle, @"[^a-zA-Z0-9-]+", " ");
        slug = slug.Trim().Replace(" ", "-").ToLower();

        RouteValues.Add("slug", slug);

        RouteValues.Add("productId", ProductId.ToString());

        base.Process(context, output);
    }
}