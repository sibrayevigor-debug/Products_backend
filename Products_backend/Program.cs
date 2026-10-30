
using Microsoft.EntityFrameworkCore;
using Products_backend;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 1. РЕГИСТРАЦИЯ СЛУЖБ SWAGGER
builder.Services.AddControllers();

// Добавляет генератор Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 2. ПОДКЛЮЧЕНИЕ MIDDLEWARE ДЛЯ SWAGGER
// Рекомендуется включать Swagger только в среде разработки (Development)
if (app.Environment.IsDevelopment())
{
    // Включает генерацию документации в формате JSON
    app.UseSwagger();

    // Включает интерактивный UI-интерфейс Swagger
    app.UseSwaggerUI(c =>
    {
        // Указывает конечную точку для JSON-документа
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


// НАЧАЛО БЛОКА АВТОМАТИЧЕСКИХ МИГРАЦИЙ
using (var scope = app.Services.CreateScope())
{
    // Безопасно достаем контекст базы данных из сервис-провайдера
    var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();
