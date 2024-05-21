using denemeKnowling.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("baglanti")));

builder.Services.AddSession();

//builder.Services.AddAuthentication(
//	CookieAuthenticationDefaults.AuthenticationScheme).
//	AddCookie(x =>
//	{
//	x.LoginPath = "/Kurslar/Login";
//	});

//builder.Services.AddMvc(config =>
//{
//	var policy = new AuthorizationPolicyBuilder()
//	.RequireAuthenticatedUser()
//	.Build();

//	config.Filters.Add(new AuthorizeFilter(policy));
//});


var app = builder.Build();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

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
	pattern: "{controller=Hesap}/{action=Register}/{id?}");

app.Run();
