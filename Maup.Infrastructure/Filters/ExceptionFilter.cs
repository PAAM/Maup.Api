using Maup.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Maup.Infrastructure.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception.GetType() == typeof(CustomException))
            {
                var exception = (CustomException)context.Exception;
                var validation = new
                {
                    status = 400,
                    Title = "Bad Request",
                    Detail = exception.Message
                };

                var result = new
                {
                    errors = new[] { validation },
                };

                context.Result = new BadRequestObjectResult(result);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                context.ExceptionHandled = true;
            }
        }
    }
}
