using Application.Commands;
using Application.Interfaces;
using Application.Validators;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(CreateProductCommandValidator).Assembly);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();  
app.UseAuthorization();   

app.MapControllers();

// dynamically create database in case of pending migrations

//using var scope = app.Services.CreateScope();
//var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
//var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
//if (pendingMigrations?.Any() == true)
//    await context.Database.MigrateAsync();

app.Run();
