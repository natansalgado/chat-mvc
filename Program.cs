using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using mvc.Context;
using mvc.Hubs;
using mvc.Repositories;
using mvc.Repositories.Interfaces;
using mvc.Services;
using mvc.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddDbContext<ChatContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// builder.Services.AddSession(opt => 
// {
//     opt.IdleTimeout = TimeSpan.FromSeconds(30);
//     opt.Cookie.HttpOnly = true;
//     opt.Cookie.IsEssential = true;
// });
builder.Services.AddSession();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Chat}/{action=Index}/{id?}");

app.MapHub<ChatHub>("/Chat");

app.UseSession();

app.Run();
