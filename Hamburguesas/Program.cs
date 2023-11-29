using Hamburguesas.Models.Entities;
using Hamburguesas.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Repositorios
builder.Services.AddTransient<Repository<Menu>>();
builder.Services.AddTransient<ClasificacionRepository>();
builder.Services.AddTransient<MenuRepository>();
builder.Services.AddTransient<Repository<Clasificacion>>();
builder.Services.AddDbContext<NeatContext>(x => x.UseMySql("server=localhost;user=root;password=root;database=neat",
    Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql")));

builder.Services.AddMvc();

var app = builder.Build();
app.UseFileServer();
app.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
app.MapDefaultControllerRoute();

app.Run();
