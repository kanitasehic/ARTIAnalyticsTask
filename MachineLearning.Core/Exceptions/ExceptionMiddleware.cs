using MachineLearning.Core.Constants;
using MachineLearning.Core.DTOs;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace MachineLearning.Core.Exceptions;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (BadRequestException ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            await httpContext.Response.WriteAsJsonAsync((ErrorResponse)ex);
        }
        catch (Exception ex)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(new ErrorResponse
            {
                Title = ErrorMessages.INTERNAL_SERVER_ERROR,
                Description = ex.Message
            });
        }
    }
}
