
using Microsoft.EntityFrameworkCore;
using Products_backend;
using Products_backend.Services;


var builder = WebApplication.CreateBuilder(args);

//DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// РЕГИСТРАЦИЯ СЛУЖБ SWAGGER
builder.Services.AddControllers();

// Добавляет генератор Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Добавляет сервиса
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// НАЧАЛО БЛОКА АВТОМАТИЧЕСКИХ МИГРАЦИЙ
using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<AppDbContext>();
dbContext?.Database.Migrate();


// ПОДКЛЮЧЕНИЕ MIDDLEWARE ДЛЯ SWAGGER
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

app.Run();
