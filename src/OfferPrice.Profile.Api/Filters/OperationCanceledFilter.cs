using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using OfferPrice.Profile.Application.Exceptions;

namespace OfferPrice.Profile.Api.Filters
{
    public class OperationCanceledFilter : IExceptionFilter
    {
        private readonly ProblemDetailsFactory problemDetailsFactory;

        public OperationCanceledFilter(ProblemDetailsFactory problemDetailsFactory)
        {
            this.problemDetailsFactory = problemDetailsFactory;
        }

        public void OnException(ExceptionContext context)
        {
            context.Result = context.Exception switch
            {
                OperationCanceledException =>
                    new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 400, context.Exception.Message)),

                UserNotFoundException=>
                    new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 404, context.Exception.Message)),

                UserEmailException =>
                    new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 409, context.Exception.Message)),

                _ =>
                    new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 500))
            };
        }
    }
}
