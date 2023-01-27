
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

using System.Net;

namespace TestProject.API.Exceptions;

public class HttpResponseExceptionFilter : IExceptionFilter
{
    public int Order => int.MaxValue - 10;


    public void OnException(ExceptionContext context)
    {
        Exception exception = context.Exception;
        var errorResult = new ErrorResult
        {
            Source = exception.TargetSite?.DeclaringType?.FullName,
            Exception = exception.Message.Trim(),
        };
        errorResult.Messages.Add(exception.Message);

        if (exception.InnerException != null)
        {
            while (exception.InnerException != null)
            {
                exception = exception.InnerException;
            }
        }
        if (context.Exception is HttpRequestException httpRequestException)
        {
            errorResult.StatusCode = (int)httpRequestException.StatusCode;
        }

        context.Result = new JsonResult(errorResult)
        {
            StatusCode = errorResult.StatusCode,
        };
        //throw new NotImplementedException();
    }
}