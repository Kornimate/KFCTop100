using KFCTop100.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using KFCSharedData;
using KFCSharedData.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<KFCDbContext>(options =>
{
    IConfigurationRoot configuration = builder.Configuration;
    options.UseSqlServer(configuration.GetConnectionString("SqlServerConnection"));
    options.UseLazyLoadingProxies();
});

// Add services to the container.
builder.Services.AddTransient<IKFCService, KFCService>();

builder.Services.AddControllersWithViews();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Records/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseSession();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Records}/{action=Index}/{id?}");

using (var serviceScope = app.Services.CreateScope())
using (var context = serviceScope.ServiceProvider.GetRequiredService<KFCDbContext>())
{
    DbInitializer.Initialize(context);
}


app.Run();
