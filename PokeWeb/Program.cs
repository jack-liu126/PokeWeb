using PokeWeb;
using PokeWeb.Extensions;
using PokeWeb.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//���]�w�s���r�ꪺ�I�s�覡
builder.Services.AddDbContext<PokeContext>();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Pokemon}/{action=Index}/{id?}");

app.Run();
