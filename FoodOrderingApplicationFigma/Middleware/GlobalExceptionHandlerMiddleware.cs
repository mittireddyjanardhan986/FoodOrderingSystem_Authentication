using System.Net;
using System.Text.Json;

namespace FoodOrderingApplicationFigma.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            int statusCode;
            object response;

            switch (exception.Message)
            {
                case "User Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "User not found." };
                    break;
                case "Address Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "Address not found." };
                    break;
                case "Admin Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "Admin not found." };
                    break;
                case "City Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "City not found." };
                    break;
                case "State Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "State not found." };
                    break;
                case "Customer Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "Customer not found." };
                    break;
                case "Restaurant Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "Restaurant not found." };
                    break;
                case "Role Not Found":
                    statusCode = (int)HttpStatusCode.NotFound;
                    response = new { statusCode, message = "Role not found." };
                    break;
                case "Invalid Credentials":
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    response = new { statusCode, message = "Invalid email or password." };
                    break;
                case "Email Already Exists":
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new { statusCode, message = "Email already exists." };
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new { statusCode, message = "An error occurred while processing your request.", details = exception.Message };
                    break;
            }

            context.Response.StatusCode = statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
