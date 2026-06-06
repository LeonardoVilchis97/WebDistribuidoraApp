using WebApplicationDistribuidoraApp.Repositories;
using WebApplicationDistribuidoraApp.Services;
using System.Globalization;

var supportedCultures = new[] { "es-MX" };
var localizationOptions = new RequestLocalizationOptions()
    .SetDefaultCulture("es-MX")
    .AddSupportedCultures(supportedCultures)
    .AddSupportedUICultures(supportedCultures);



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ProductoRepository>();
builder.Services.AddScoped<IProductoService, ProductoService>(); 
builder.Services.AddScoped<TiposProductoRepository>();
builder.Services.AddScoped<ProductoProveedorRepository>();
builder.Services.AddScoped<ProductoProveedorService>();
builder.Services.AddScoped<ProveedorRepository>();
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();
app.UseRequestLocalization(localizationOptions);
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Prodctos}/{action=Index}/{id?}");

app.Run();

