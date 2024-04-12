using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;

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
            //context.Result = context.Exception switch
            //{
            //    context.Exception is OperationCanceledException =>
            //    new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 400));
            //};

            if (context.Exception is OperationCanceledException)
            {
                context.Result = new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 400));
            }
            else
            {
                context.Result = new ObjectResult(problemDetailsFactory.CreateProblemDetails(context.HttpContext, 500));
            }
        }
    }
}
