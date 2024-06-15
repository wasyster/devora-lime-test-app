namespace Solution.Configurations.ConfigurationExtensions;

public static class JsonSerializeConfiguration
{
    public static IMvcBuilder AddJsonSerializatonSettings(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });

        return builder;
    }
}
