using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace RealEstate.API.Middleware
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode == (int)HttpStatusCode.BadRequest && context.Items.ContainsKey("ValidationProblemDetails"))
                {
                    var validationProblemDetails = context.Items["ValidationProblemDetails"] as ValidationProblemDetails;
                    await HandleModelStateValidationErrorAsync(context, validationProblemDetails);
                }
            }
            catch (ValidationException ex)
            {
                await HandleValidationExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleModelStateValidationErrorAsync(HttpContext context, ValidationProblemDetails validationProblemDetails)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            var result = JsonSerializer.Serialize(new
            {
                message = "Errores de validación",
                errors = validationProblemDetails.Errors
            });

            return response.WriteAsync(result);
        }

        private static Task HandleValidationExceptionAsync(HttpContext context, ValidationException exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.BadRequest;

            var errors = exception.Errors.Select(err => new { err.PropertyName, err.ErrorMessage });
            var result = JsonSerializer.Serialize(new { message = "Errores de validación", errors });

            return response.WriteAsync(result);
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorMessage = exception.InnerException != null ? exception.InnerException.Message : exception.Message;

            var result = JsonSerializer.Serialize(new { message = errorMessage });
            return response.WriteAsync(result);
        }

    }
}
