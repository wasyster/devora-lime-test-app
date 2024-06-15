namespace Solution.Configurations.ConfigurationExtensions;

public static class FluentValidationConfiguration
{
    public static void ConfigureFluentValidation(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        builder.Services.AddFluentValidationAutoValidation();
        builder.RegisterFluentValidators();
    }
}
