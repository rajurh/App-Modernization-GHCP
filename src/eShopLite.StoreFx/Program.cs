using eShopLite.StoreFx.Components;
using eShopLite.StoreFx.Data;
using eShopLite.StoreFx.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add Blazor services
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure database with SQLite
var connectionString = builder.Configuration.GetConnectionString("StoreDbContext")
    ?? "Data Source=store.db";

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlite(connectionString));

// Register application services using scoped lifetime for per-request instances
builder.Services.AddScoped<IStoreService, StoreService>();

// Add response compression for better performance
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// Configure HTTP strict transport security (HSTS) for production
if (!builder.Environment.IsDevelopment())
{
    builder.Services.AddHsts(options =>
    {
        options.MaxAge = TimeSpan.FromDays(365);
        options.IncludeSubDomains = true;
        options.Preload = true;
    });
}

var app = builder.Build();

// Database initialization and seeding
await using (var scope = app.Services.CreateAsyncScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StoreDbContext>();
    StoreDbContextSeed.Seed(context);
}

// Configure middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseResponseCompression();

// Only redirect to HTTPS in development (Azure Container Apps handles TLS termination)
if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseStaticFiles();
app.UseAntiforgery();

// Map Blazor components
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

await app.RunAsync();