using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using OnlineShopp.DB;
using WebApplicationShopOnline.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DBonlineShop");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<DatabaseContext>();

builder.Services.AddScoped<IProductDBsRepository, ProductsDBRepository>();

builder.Services.AddScoped<ICartDBsRepository, CartDBsRepository>();


var app = builder.Build();


// Вызов инициализации БД с пользователями
using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);
    var productsRepo = scope.ServiceProvider.GetRequiredService<IProductDBsRepository>();
    if (productsRepo.GetAll().Count == 0) // Только если товаров нет
    {
        productsRepo.Add(new ProductDB
        {
            Id = Guid.NewGuid(),
            Name = "Тестовый товар 1",
            Description = "Описание тестового товара 1",
            Cost = 100,
            PathPicture = "https://via.placeholder.com/150",
            Length = 0,
            Width = 0,
            Height = 0,
            Weight = 0
        });
        productsRepo.Add(new ProductDB
        {
            Id = Guid.NewGuid(),
            Name = "Тестовый товар 2",
            Description = "Описание тестового товара 2",
            Cost = 200,
            PathPicture = "https://via.placeholder.com/150",
            Length = 0,
            Width = 0,
            Height = 0,
            Weight = 0
        });
    }

}

//Инициализпция DB с товарами
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
    DbInitializer.Initialize(dbContext);
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
