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
    var services = scope.ServiceProvider;


    var dbContext = services.GetRequiredService<DatabaseContext>();
    for (var attempt = 1; attempt <= 5; attempt++)
    {
        try
        {
            dbContext.Database.EnsureCreated();
            break;
        }
        catch when (attempt < 5)
        {
            Thread.Sleep(2000);
        }
    }

    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);

    var productsRepository = services.GetRequiredService<IProductDBsRepository>();
    ProductInitializer.Initialize(productsRepository);
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
