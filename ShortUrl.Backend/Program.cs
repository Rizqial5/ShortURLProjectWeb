
using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Backend.Models;
using ShortUrl.Backend.Controllers;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddSqlServer<ApplicationDbContext>(connectionString);
builder.Services.AddControllers();
builder.Services.AddScoped<ShorteningController>();
builder.Services.AddHttpClient();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    
    app.MapScalarApiReference(options => 
    {
        options.Theme = ScalarTheme.BluePlanet;
    });
    
    
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

// app.MapGet("/", () => "Hello world!");

app.Run();


