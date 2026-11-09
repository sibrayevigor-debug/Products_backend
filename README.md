# Product Catalog REST API

  Тестовое задание. Бэкэнд ASP.NET Core + SQL Разработка REST API системы учёта товаров

#Технические особенности

  Фреймворк: ASP.NET Core 9.0 (Web API)  
  База данных: SQL Server (LocalDB)  
  ORM: Entity Framework Core (Code First)  

#Архитектура

  Разделение слоёв: Контроллер→ Сервис→ EF Core   
  Асинхронные методы, CancellationToken в контроллерах и сервисах  
  Миграции применяются при старте (Database.Migrate())  
  Настройка базы данных через Fluent API    

#Требования

  Установленный \[.NET SDK 9.0]  
  Установленный компонент SQL Server Express LocalDB   

 

#Клонирование репозитория и переход в папку  


git clone https://github.com/sibrayevigor-debug/Products_backend.git  
cd Products_backend  
  dotnet build  
  dotnet run --project Products_backend 

#Адрес для Swagger  

  https://localhost:7152/swagger  
  https://localhost:5062/swagger  

#Основные эндпоинты  

  GET /api/products — список для каталога  
  GET /api/products/{id} — детальная карточка  
  GET /api/warehouses — список складов (для выпадающего списка на фронте)  
  POST /api/products — создание записи продукта  

#Примеры curl для всех эндпоинтов

GET /api/products:

curl -X 'GET' \
  'https://localhost:7152/api/products' \
  -H 'accept: */*'

POST /api/products:

curl -X 'POST' \
  'https://localhost:7152/api/products' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "name": "Мышь",
  "price": 1450.50,
  "quantity": 3,
  "length_mm": 120,
  "width_mm": 65,
  "height_mm": 38,
  "weight_kg": 1,
  "warehouse_id": 1
  }'

GET /api/products/{id}:

curl -X 'GET' \
  'https://localhost:7152/api/products/11' \
  -H 'accept: */*'

GET /api/warehouses:

curl -X 'GET' \
  'https://localhost:7152/api/warehouse' \
  -H 'accept: */*'

