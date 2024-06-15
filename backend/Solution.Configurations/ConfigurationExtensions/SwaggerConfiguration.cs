namespace Solution.Configurations.ConfigurationExtensions;

public static class SwaggerConfiguration
{
    public static void ConfigureSwagger(this WebApplicationBuilder builder)
    {
        string path = "version.txt";
        string version = $"{path} does not exists";

        if (File.Exists(path))
            version = File.ReadAllText(path);

        builder.Services.AddSwaggerGen(x =>
        {
            x.EnableAnnotations();
            x.UseInlineDefinitionsForEnums();
            x.SwaggerDoc("v1", new() { Title = $"Arena API - {version}", Version = version, Description = "API endpoints for Devora Lime Test App" });
            

            x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\""
            });;

            x.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }

    public static void ConfigureSwagger(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseSwagger();

        applicationBuilder.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/v1/swagger.json", "API endpoints for Devora Lime Test App");
            options.DocExpansion(DocExpansion.None);
        });
    }
}
