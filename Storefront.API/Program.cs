using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Storefront.API.Classes;
using Storefront.API.Data;
using Storefront.API.Data.Models;
using Storefront.API.Extensions;
using Storefront.API.Services;

var builder = WebApplication.CreateBuilder(args);

ConnectionStrings? connectionStrings = builder.Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>();

if (connectionStrings is null)
{
    throw new KeyNotFoundException("ConnectionStrings");
}

// Add services to the container.

builder.Services.AddDbContext<StorefrontContext>(options =>
    options.UseSqlServer(connectionStrings.Storefront));

builder.Services.AddControllers();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
})
    .AddRoles<ApplicationRole>()
    .AddEntityFrameworkStores<StorefrontContext>()
    .AddDefaultTokenProviders();

builder.Services.AddUnitOfWork();
builder.Services.AddAppServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<ApplicationUser>();

app.MapControllers();



app.Run();

//void SeedRoles(WebApplication? app)
//{
//    using (var scope = app.Services.CreateScope())
//    {
//        var roleManager = scope.ServiceProvider.GetRequiredService();
//        string[] roleNames = { "Admin", "User", "Manager" };

//        foreach (var roleName in roleNames)
//        {
//            if (!await roleManager.RoleExistsAsync(roleName))
//            {
//                await roleManager.CreateAsync(new IdentityRole(roleName));
//            }
//        }
//    }
//}