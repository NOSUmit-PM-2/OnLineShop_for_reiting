using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using OnlineShop.DB.Repositories;  // ← ДОБАВИТЬ ЭТУ СТРОКУ!

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

string connection = builder.Configuration.GetConnectionString("DBonlineShop");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseContext>()
    .AddDefaultTokenProviders();

// Регистрация репозиториев (С ПРАВИЛЬНЫМИ ПРОСТРАНСТВАМИ ИМЁН)
builder.Services.AddTransient<IProductDBsRepository, ProductsDBRepository>();
builder.Services.AddTransient<ICartDBsRepository, CartDBsRepository>();

var app = builder.Build();

// ... остальной код