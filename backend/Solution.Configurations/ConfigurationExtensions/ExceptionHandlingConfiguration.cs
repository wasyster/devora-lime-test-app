namespace Solution.Configurations.ConfigurationExtensions;

public static class ExceptionHandlingConfiguration
{
    public static void AddExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(exceptionHandlerApp =>
        {
            exceptionHandlerApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                context.Response.ContentType = new MediaTypeWithQualityHeaderValue("application/json").MediaType;

                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exceptionInfo = ExceptionDispatchInfo.Capture(exceptionHandlerPathFeature.Error);
                var exceptionMessage = exceptionInfo?.SourceException?.Message ?? string.Empty;

                var message = JsonSerializer.Serialize(new GlobalErrorResponse(exceptionInfo?.SourceException?.Message ?? string.Empty));
                await context.Response.WriteAsync(message);

                var logger = app.ApplicationServices.GetService<ILogger<IApplicationBuilder>>();
                logger.LogError($"exceptionInfo source: {exceptionInfo?.SourceException?.Source}\n\texceptionInfo message: {exceptionMessage}");
            });
        });
    }

}
