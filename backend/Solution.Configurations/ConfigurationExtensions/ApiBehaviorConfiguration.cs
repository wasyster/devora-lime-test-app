namespace Solution.Configurations.ConfigurationExtensions;

public static class ApiBehaviorConfiguration
{
    public static IMvcBuilder ConfigureApiBehaviorOptions(this IMvcBuilder builder)
    {
        builder.ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                if (!context.ModelState.IsValid)
                {
                    var errorInfos = context.ModelState.ToDictionary(k => k.Key, v => v.Value);

                    foreach (var errorInfo in errorInfos)
                        Log.Logger.Information($"validation error for: {errorInfo.Key} => {errorInfo.Value?.Errors.First().ErrorMessage}");

                    return new BadRequestObjectResult(context.ModelState);
                }
                return new OkResult();
            };
        });

        return builder;
    }
}
