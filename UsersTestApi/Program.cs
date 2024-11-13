using UsersTestApi;
using UsersTestApi.Models;
using UsersTestApi.Repositories;
using UsersTestApi.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

// Add services to the container.
builder.Services.AddControllers();

builder.Services.Configure<UserDatabaseSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();  // Detailed error page in development
}

app.UseHttpsRedirection();

app.UseRouting();  // Explicit routing middleware

app.MapControllers();  // Map controller routes

app.Run();