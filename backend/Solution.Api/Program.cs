Log.Logger = new LoggerConfiguration()
   .WriteTo.Console()
   .MinimumLevel.Debug()
   .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
   .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
   .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonSerializatonSettings();

builder.Services.AddResponseCompression(options => options.EnableForHttps = true);
builder.ConfigureEnviorementVariables();
builder.ConfigureSettings();
builder.ConfigureSerilog();
builder.ConfigureDatabase();
builder.ConfigureFluentValidation();
builder.ConfigureAuthentication();
builder.ConfigureDI();
builder.ConfigureSwagger();
builder.ConfigureCORS();
builder.ConfigureHttpContext();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddMemoryCache();
builder.Services.AddOutputCache();

var app = builder.Build();
app.UseResponseCompression();
app.ConfigureSwagger();
app.UseMigrationsEndPoint();
app.UseHttpsRedirection();
app.ConfigureCORS();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseOutputCache();


var cultureInfo = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


//Auto migration
using var serviceScope = (app as IApplicationBuilder).ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
using var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
await context!.Database.MigrateAsync();

app.Run();
