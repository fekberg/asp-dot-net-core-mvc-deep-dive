using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Globomatics.Web.Attributes;

public class TimerFilterAttribute : ActionFilterAttribute
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<TimerFilterAttribute>>();

        var stopwatch = new Stopwatch();

        stopwatch.Start();

        logger.LogInformation($"Action {context.ActionDescriptor} started");

        await next();

        stopwatch.Stop();

        logger.LogInformation($"Action {context.ActionDescriptor} completed");
        logger.LogInformation($"{context.ActionDescriptor} ran for {stopwatch.ElapsedMilliseconds}");
    }
}
