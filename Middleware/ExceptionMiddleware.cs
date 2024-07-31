using project.Errors;
using System.Net;
using System.Text.Json;

namespace project.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate request;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment eve;

        public ExceptionMiddleware(RequestDelegate request,ILogger<ExceptionMiddleware> logger,IHostEnvironment
            eve)
        {
            this.request = request;
            this.logger = logger;
            this.eve = eve;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await request(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode=(int)HttpStatusCode.InternalServerError;
                var response =
                    new ApiExtension((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace.ToString());
                   // new ApiResponse((int)HttpStatusCode.InternalServerError);

                var options=new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json=JsonSerializer.Serialize<ApiExtension>(response,options);
                await context.Response.WriteAsync(json);

            }
        }
    }
}
