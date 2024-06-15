namespace Solution.Configurations.ConfigurationExtensions;

public static class DatabaseConfiguration
{
    public static void ConfigureDatabase(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContextPool<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies()
                .UseSqlServer(connectionString, options =>
                {
                    options.MigrationsAssembly("Solution.Database");
                    options.EnableRetryOnFailure();
                    options.CommandTimeout(300);
                })
        //.LogTo(Console.WriteLine) //please let it here for debugging purposes
        );

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
    }
}
