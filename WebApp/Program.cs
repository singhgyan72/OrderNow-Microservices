using Microsoft.AspNetCore.Authentication.Cookies;
using OrderNow.WebApp.Service;
using OrderNow.WebApp.Service.IService;
using OrderNow.WebApp.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Configure HttpClient
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<ICouponService, CouponService>();
builder.Services.AddHttpClient<IAuthService, AuthService>();
builder.Services.AddHttpClient<IProductService, ProductService>();
builder.Services.AddHttpClient<ICartService, CartService>();
builder.Services.AddHttpClient<IOrderService, OrderService>();
#endregion

Helpers.CouponAPIBase = builder.Configuration["ServiceUrls:CouponAPI"];
Helpers.AuthAPIBase = builder.Configuration["ServiceUrls:AuthAPI"];
Helpers.ProductAPIBase = builder.Configuration["ServiceUrls:ProductAPI"];
Helpers.ShoppingCartAPIBase = builder.Configuration["ServiceUrls:ShoppingCartAPI"];
Helpers.OrderAPIBase = builder.Configuration["ServiceUrls:OrderAPI"];

builder.Services
    .AddScoped<IHttpClientService, HttpClientService>()
    .AddScoped<ICouponService, CouponService>()
    .AddScoped<IAuthService, AuthService>()
    .AddScoped<ITokenProvider, TokenProvider>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<ICartService, CartService>()
    .AddScoped<IOrderService, OrderService>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromHours(10);
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
