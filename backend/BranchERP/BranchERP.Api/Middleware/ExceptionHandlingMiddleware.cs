using System.Net;
using System.Text.Json;
using BranchERP.Application.DTOs.Common;

namespace BranchERP.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception");

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";

                var response = ApiResponse<object>.Fail(
                    "An unexpected error occurred.",
                    new List<ErrorItem>
                    {
                        new ErrorItem
                        {
                            Code = "ServerError",
                            Message = ex.Message // ممكن تخليها رسالة عامة في الإنتاج
                        }
                    });

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }
    }
}
