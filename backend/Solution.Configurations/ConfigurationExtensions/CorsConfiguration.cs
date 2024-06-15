namespace Solution.Configurations.ConfigurationExtensions;

public static class CorsConfiguration
{
    public static void ConfigureCORS(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(Constants.CorsPolicy, policy =>
            {
                policy.AllowAnyHeader()
                      .AllowAnyOrigin()
                      .AllowAnyMethod();
            });
        });
    }

    public static void ConfigureCORS(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseCors(options => 
            options.SetIsOriginAllowed(x => _ = true)
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials()
        );
    }
}
