namespace Solution.Configurations.ConfigurationExtensions;

public static class SettingsConfiguration
{
    public static void ConfigureSettings(this WebApplicationBuilder builder)
    {
        Configure(builder.Services, builder.Configuration);
    }
    private static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JWTSettings>(configuration.GetSection("JWT"));
    }
}
