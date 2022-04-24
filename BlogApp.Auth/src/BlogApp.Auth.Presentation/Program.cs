using BlogApp.Auth.Application;
using BlogApp.Auth.Domain.Entities;
using BlogApp.Auth.Infrastructure;
using BlogApp.Auth.Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.ConfigureApplicationCookie(config =>
{
    config.Cookie.Name = "BlogApp.Identity.Cookie";
    config.LoginPath = "/Auth/Login";
    config.LogoutPath = "/Auth/Logout";
});

builder.Services.AddIdentity<AppUser, AppRole>(config =>
{
    config.Password.RequiredLength = 6;
    config.Password.RequireDigit = true;
    config.Password.RequireLowercase = true;
    config.Password.RequireUppercase = true;
    config.Password.RequireNonAlphanumeric = false;
})
                .AddEntityFrameworkStores<BlogAuthDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<AppUser, AppRole, BlogAuthDbContext, Guid>>()
                .AddRoleStore<RoleStore<AppRole, BlogAuthDbContext, Guid>>();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(configuration);

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
