using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using WebApplicationShopOnline.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DBonlineShop");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddTransient<IProductDBsRepository, ProductsDBRepository>();

builder.Services.AddTransient<ICartDBsRepository, CartDBsRepository>();


var app = builder.Build();


// Вызов инициализации БД 
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);
    var productsRepo = scope.ServiceProvider.GetRequiredService<IProductDBsRepository>();
    if (productsRepo.GetAll().Count == 0) 
    {
        productsRepo.Add(new ProductDB
        {
            Id = Guid.NewGuid(),
            Name = "132",
            Description = "123213",
            Cost = 100,
            PathPicture = "https://via.placeholder.com/150"
        });
        productsRepo.Add(new ProductDB
        {
            Id = Guid.NewGuid(),
            Name = "1111",
            Description = "wre",
            Cost = 200,
            PathPicture = "https://via.placeholder.com/150"
        });
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Catalog}/{id?}");

app.Run();
