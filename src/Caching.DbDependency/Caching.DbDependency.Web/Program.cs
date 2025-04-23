using Caching.DbDependency.Web.GetProductsFeature;
using Caching.DbDependency.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddRepositoryServices(builder.Configuration);
builder.Services.AddScoped<IProductsManager, ProductsManager>();
builder.Services.AddSingleton<IProductsChangeTokenProvider, ProductsChangeTokenProvider>();
builder.Services.AddHostedService<ProductChangedListener>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
