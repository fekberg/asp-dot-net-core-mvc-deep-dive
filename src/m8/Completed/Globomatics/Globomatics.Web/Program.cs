using Globomantics.Domain.Models;
using Globomantics.Infrastructure.Data;
using Globomatics.Infrastructure.Repositories;
using Globomatics.Web.Constraints;
using Globomatics.Web.Transformers;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("MyRedisConStr");
    options.InstanceName = "SampleInstance";
});

builder.Services.AddSession();

builder.Services.AddRouting(options => {
    options.ConstraintMap["validateSlug"] = typeof(SlugConstraint);
    options.ConstraintMap["slugTransform"] = typeof(SlugParameterTransformer);
});

builder.Services.AddDbContext<GlobomanticsContext>(ServiceLifetime.Scoped);

builder.Services.AddTransient<IRepository<Customer>, CustomerRepository>();
builder.Services.AddTransient<IRepository<Product>, ProductRepository>();
builder.Services.AddTransient<IRepository<Order>, OrderRepository>();
builder.Services.AddTransient<IRepository<Cart>, CartRepository>();
builder.Services.AddTransient<ICartRepository, CartRepository>();

var app = builder.Build();

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

app.UseAuthentication();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "administrationDefault",
    defaults: new { controller = "Home" },
    pattern: "{area:exists}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "administration",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GlobomanticsContext>();

    GlobomanticsContext.CreateInitialDatabase(context);
}

app.Run();