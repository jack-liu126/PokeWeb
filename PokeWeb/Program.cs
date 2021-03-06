using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Authorization;
using PokeWeb;
using PokeWeb.Extensions;
using PokeWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(option =>
{
    option.Filters.Add(new AuthorizeFilter());
});
//不設定連結字串的呼叫方式
builder.Services.AddDbContext<PokeContext>();

#region ForSignIn
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
{
    //未登入時會自動導到這個網址
    option.LoginPath = new PathString("/Auth/Login");
    //沒權限會自動導到該網址
    //option.AccessDeniedPath = new PathString("");
    //登入期限
    option.ExpireTimeSpan = TimeSpan.FromHours(12);
});
#endregion

builder.Services.AddSingleton<IFileExt, FileExt>();

builder.Services.AddTransient<ITypeCompareSet_Website, TypeCompareSet_Website>();

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

#region ForSignIn
app.UseCookiePolicy();
app.UseAuthentication();
#endregion

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
