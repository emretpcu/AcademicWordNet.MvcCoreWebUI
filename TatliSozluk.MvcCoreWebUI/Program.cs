using BusinessLayer.Abstract;
using BusinessLayer.Concrate;
using DataAccessLayer.Abstract;
using DataAccessLayer.Concrate;
using DataAccessLayer.Concrate.Repositories;
using EntityLayer.Concreate;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using TatliSozluk.MvcCoreWebUI.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IMailService, MailService>();

var configuration = new ConfigurationBuilder()
	.SetBasePath(builder.Environment.ContentRootPath)
	.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
	.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
	.AddEnvironmentVariables()
.Build();

builder.Services.AddDbContext<Context>();
builder.Services.AddAuthentication(
	CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(x=>x.LoginPath="/Account/Login");

builder.Services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<Context>()
	.AddDefaultTokenProviders();

var serviceProvider = builder.Services.BuildServiceProvider();
var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

if (!await roleManager.RoleExistsAsync("User")&& !await roleManager.RoleExistsAsync("Admin"))
{
	await roleManager.CreateAsync(new IdentityRole("User"));
	await roleManager.CreateAsync(new IdentityRole("Admin"));
}

var adminUser = new User
{
	UserName = "adminuser",
	Email = "admin@hakin.com",
	NameSurname = "adminuser"
};
var adminUserExists = await userManager.FindByEmailAsync(adminUser.Email);
if (adminUserExists == null)
{
	var result = await userManager.CreateAsync(adminUser, "Admin1-");
	if (result.Succeeded)
	{
		await userManager.AddToRoleAsync(adminUser, "Admin");
	}
}

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

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
