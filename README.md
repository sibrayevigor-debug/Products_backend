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

#Запуск 

git clone https://github.com/sibrayevigor-debug/Products_backend.git  
cd Products_backend  
dotnet build  
dotnet run --project Products_backend --launch-profile https  

#После запуска приложение будет доступно по адресам 

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
  "name": "Игровая мышь Logitech G102",  
  "price": 2450.50,  
  "quantity": 12,  
  "length_mm": 116,  
  "width_mm": 62,  
  "height_mm": 38,  
  "weight_kg": 0.085,  
  "warehouse_id": 1  
}'  

GET /api/products/{id}:  

curl -X 'GET' \  
  'https://localhost:7152/api/products/15' \  
  -H 'accept: */*'  

GET /api/warehouses:  

curl -X 'GET' \  
  'https://localhost:7152/api/warehouses' \  
  -H 'accept: */*'  

