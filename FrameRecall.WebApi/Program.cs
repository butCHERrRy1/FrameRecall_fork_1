using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Добавление сервисов
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IMongoClient>(
    _ => new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));

builder.Services.AddScoped<IMongoDatabase>(
    sp => sp.GetRequiredService<IMongoClient>().GetDatabase("framerecall"));

builder.Services.AddScoped<IRepository<DataFilm>, FilmRepository>();

builder.Services.AddScoped<IFilmService, FilmService>();

WebApplication app = builder.Build();

// Настройка middleware для development окружения
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();


