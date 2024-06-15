namespace Solution.Configurations.ConfigurationExtensions;

public static class DependencyInjectionConfiguration
{
    public static void ConfigureDI(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpClient();

        // Add useful interface for accessing the ActionContext outside a controller.
        builder.Services.TryAddSingleton<IActionContextAccessor, ActionContextAccessor>();

        builder.Services.AddTransient<IValidatorInterceptor, FluentvalidationInterceptor>();
        builder.Services.AddTransient<IHeroService, HeroService>();
        builder.Services.AddTransient<IArenaService, ArenaService>();
    }
}
