# Тестовая работа для ЭкоНива-АПК-холдинг.

RestApi для простенького багтрекера.

## Развертывание
 
* Инициализировать базу данных для айдентити сервера   ~/Identity> dotnet run /seed
* Запустить айдентити сервер ~/Identity>  'dotnet run --urls="http://localhost:4000"'  - порт обязательно такой, либо его нужно поменять в настройках ~/BugTracker.Api/Startup.cs(60 строчка) на тот который будет свободен.
  
### Запуск АПИ

* Запустить апи ~/BugTracker.Api> 'dotnet run --urls="https://localhost:5001"'
* Документация к АПИ - https://localhost:5001/swagger/index.html
* Набор запросов для проверки в постмане в файле ~/test.postman_collection.json. (не забываем обновлять маркер, в коллекции сохранен уже устаревший маркер).

### Запуск тестов
 
*  Запуск ~/BugTracker.Api.Tests> 'dotnet test'

#### Ссылки

[IdentityServer 4.0](https://identityserver4.readthedocs.io/en/latest/)
[dotnet core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)





