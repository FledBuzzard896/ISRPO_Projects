using API.Models;
using Microsoft.EntityFrameworkCore;

//Билдер, для инициализации API. К нему происходит подключение сервисов. (БД, Авторизация)
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Создание экземпляра API
var app = builder.Build();


// GET: All
// URL: /api/drugs
app.MapGet("/api/drugs", async (AppDbContext context) =>
{
    var drugs = await context.Drugs.ToListAsync();
    return Results.Json(drugs);
});


// GET: One
// URL: /api/drugs/5
app.MapGet("/api/drugs/{id}", async (AppDbContext context, int id) =>
{
    var drug = await context.Drugs.FirstOrDefaultAsync(d => d.Id == id);

    if (drug is null) 
    {
        return Results.NotFound(new { message = "Запись не найдена" });
    }

    return Results.Json(drug);
});


// POST: One
// URL: /api/drugs
app.MapPost("/api/drugs", async (AppDbContext context, Drug newDrug) =>
{
    if (string.IsNullOrWhiteSpace(newDrug.Name) || string.IsNullOrWhiteSpace(newDrug.StreetName)) 
    {
        return Results.BadRequest(new { message = "Название (Name) и Уличное название (StreetName) не могут быть пустыми" });
    }

    if (newDrug.PricePerGram <= 0 || newDrug.WeightInGrams <= 0) 
    {
        return Results.BadRequest(new { message = "Цена за грамм (PricePerGram) и Вес в граммах (WeightInGrams) должны быть больше нуля" });
    }

    if (newDrug.PurityPercentage < 0 || newDrug.PurityPercentage > 100) 
    {
        return Results.BadRequest(new { message = "Процент чистоты (PurityPercentage) должен быть от 0 до 100." });
    }
});


app.Run();
