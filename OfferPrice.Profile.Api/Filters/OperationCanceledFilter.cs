using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace OfferPrice.Profile.Api.Filters
{
    public class OperationCanceledFilter : IExceptionFilter
    {

        public ProblemDetailsFactory ProblemDetailsFactory { get; }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is OperationCanceledException)
            {
                context.HttpContext.Response.StatusCode = 409;
                context.Result = (IActionResult)ProblemDetailsFactory.CreateProblemDetails(context.HttpContext, 409);
            }
            else
            {
                context.HttpContext.Response.StatusCode = 500;
                context.Result = (IActionResult)ProblemDetailsFactory.CreateProblemDetails(context.HttpContext, 500);
            }
        }
    }
}
