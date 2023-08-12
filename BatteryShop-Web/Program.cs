using CodeYad_Blog.CoreLayer.Services.Comments;
using CoreLayer.Services;
using CoreLayer.Services.Categories;
using CoreLayer.Services.FileManager;
using CoreLayer.Services.Orders;
using CoreLayer.Services.Products;
using CoreLayer.Services.Products.ProductImages;
using CoreLayer.Services.Users;
using CoreLayer.Services.Users.UserAddress;
using DataLayer.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ShopContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IUserService, UserService>(); 
builder.Services.AddTransient<IProductService, ProductService>(); 
builder.Services.AddTransient<IOrderService, OrderService>(); 
builder.Services.AddTransient<IFileManager, FileManager>(); 
builder.Services.AddTransient<ICategoryService, CategoryService>(); 
builder.Services.AddTransient<IAddressService, AddressService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IProductImageService, ProductImageService>();
builder.Services.AddTransient<IOrderFinallyService, OrderFinallyService>();


builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    option.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
}).AddCookie(option =>
{
    option.LoginPath = "/Auth/Login";
    option.LogoutPath = "/Auth/Logout";
    option.ExpireTimeSpan = TimeSpan.FromDays(30);
    option.AccessDeniedPath = "/";
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "Default",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
app.MapRazorPages();

app.Run();
