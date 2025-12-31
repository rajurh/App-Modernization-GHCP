using eShopLite.StoreFx.Data;
using eShopLite.StoreFx.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure services
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        // Configure System.Text.Json for modern JSON serialization
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.WriteIndented = true;
    });

// Configure database with SQLite
var connectionString = builder.Configuration.GetConnectionString("StoreDbContext")
    ?? throw new InvalidOperationException("Connection string 'StoreDbContext' not found.");

builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseSqlite(connectionString));

// Register application services using scoped lifetime for per-request instances
builder.Services.AddScoped<IStoreService, StoreService>();

// Add response compression for better performance
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
});

// Configure HTTP strict transport security (HSTS)
builder.Services.AddHsts(options =>
{
    options.MaxAge = TimeSpan.FromDays(365);
    options.IncludeSubDomains = true;
    options.Preload = true;
});

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
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/StatusErrorCode", "?code={0}");
app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Map controller routes
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

await app.RunAsync();