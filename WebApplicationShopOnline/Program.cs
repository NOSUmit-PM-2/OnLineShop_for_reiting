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
    var user = await userManager.FindByNameAsync("test@test.com");
    if (user == null)
    {
        user = new User { UserName = "test@test.com", Email = "test@test.com" };
        await userManager.CreateAsync(user, "Password123!");
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

/*var user = await userManager.FindByNameAsync("test@test.com");
if (user == null)
{
    user = new User { UserName = "test@test.com", Email = "test@test.com" };
    await userManager.CreateAsync(user, "Password123!");
}*/
/*var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    IdentityInitializer.Initialize(userManager, roleManager);*/