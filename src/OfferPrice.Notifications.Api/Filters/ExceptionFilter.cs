using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OfferPrice.Notifications.Application.Exceptions;

namespace OfferPrice.Notifications.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ProblemDetailsFactory problemDetailsFactory;

    public ExceptionFilter(ProblemDetailsFactory problemDetailsFactory)
    {
        this.problemDetailsFactory = problemDetailsFactory;
    }

    public void OnException(ExceptionContext context)
    {
        context.Result = context.Exception switch
        {
            OperationCanceledException =>
                new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 400, context.Exception.Message)),

            EntityNotFoundException =>
                new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 404, context.Exception.Message)),

            _ =>
                new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 500, context.Exception.Message))
        };
    }
}
