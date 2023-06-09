Структура

Примерные "слои" приложения 
- Presentation
- Business
- Persistence
- Database

Подготовка и структура проекта

```
dotnet new gitignore

mkdir Communiko
cd Communiko
dotnet new sln
dotnet new webapi -n PresentationAPI
dotnet new classlib -n Application
dotnet new classlib -n BusinessDomain
dotnet new classlib -n Persistence

```

```
dotnet sln add PresentationAPI/PresentationAPI.csproj
dotnet sln add Application/Application.csproj
dotnet sln add BusinessDomain/BusinessDomain.csproj
dotnet sln add Persistence/Persistence.csproj
```

```
cd PresentationAPI
dotnet add reference ../Application/Application.csproj
cd ../Application
dotnet add reference ../BusinessDomain/BusinessDomain.csproj
dotnet add reference ../Persistence/Persistence.csproj
cd ../Persistence
dotnet add reference ../BusinessDomain/BusinessDomain.csproj
cd ..
```

```
dotnet restore
```

# Проект Persistence

## Начало работы с базой

### Установить и настроить PostgreSQL или [развернуть Docker-контейнер](https://t.me/iksergeyru/176)

Библиотека EF для PostgreSQL:
- `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL`

## Проект PresentationAPI

Библиотеки для миграций 
- `dotnet add package Microsoft.EntityFrameworkCore.Design`
- `dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design`


Обновить файл _Communiko/PresentationAPI/appsettings.json_ 
добавив строку подключения к бд

```
{
  "ConnectionStrings": {
    "PostgreSQLConnection": "Host=$HOST:PORT$; Username=$Username$; Password=$Password$;Database=$db_name$"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

Пример строки подключения: 
```
"PostgreSQLConnection": "Host=localhost:8888; Username=postgres; Password=12345678;Database=Communiko"
```

Конфигурация контекста:

```
builder.Services.AddDbContext<DataContext>(op =>
{
op.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection"));
});
```

Для проверки инструментов EF в терминале выполнить `dotnet ef`

*у меня: `Entity Framework Core .NET Command-line Tools 7.0.4`

Если нет `dotnet-ef` нужно выполнить 
`dotnet tool install --global dotnet-ef`
если старая версия – выполнить `dotnet tool update --global dotnet-ef`

_!Будьте внимательны: версия EF должна соответствовать версии .NET_

Выполняем `dotnet restore` для обновления зависимостей 

Выполняем из корневого каталога с sln файлом для первой миграции

`dotnet ef migrations add initial-migration -s PresentationAPI -p Persistence`

И обновления базы данных:

`dotnet ef database update -s PresentationAPI`


## Создание клиента 

Настроить и подключить необходимые компоненты
- http://reactjs.org [doc](https://react.dev/learn) | [new react app](https://react.dev/learn/start-a-new-react-project) ([можно в качестве теста создать пустое приложение и посмотреть основы](#)) [+](https://create-react-app.dev)
- https://nodejs.org/en
- https://www.typescriptlang.org

- Версия node `node --version`
- Версия mpm `npm --version `
- Проверка npx `which npx` если нет - `npm install -g npx` [подробнее](https://dev-gang.ru/article/npm-protiv-npx-v-czem-raznica-hsvo0oxvqs/)
- Создание клиентского приложения `npx create-react-app client --use-npm --template typescript` ([подробнее](https://create-react-app.dev/docs/adding-typescript/))
- Многие верстальщики рекомендуют использовать плагин [Prettier - Code formatter](https://marketplace.visualstudio.com/items?itemName=esbenp.prettier-vscode)

Потребуется библиотека [axios](https://www.npmjs.com/package/axios) `npm i axios`
Failed to load resource: Origin is not allowed by Access-Control-Allow-Origin. Status code: 200 [Про CORS на Habr](https://habr.com/ru/companies/macloud/articles/553826/)

# Обновление CORS-политик

Обновить файл _appsettings.json_, добавить строку

```
...
},
"Client-host": "http://localhost:3000",
"Logging": {
...
```

## Наведение красоты

- Библиотека [Semantic-UI](https://github.com/semantic-org/semantic-ui)
- Документация [для react](https://react.semantic-ui.com/usage/)
Установка `npm install semantic-ui-react semantic-ui-css`

## Библиотека для генерации UUID

- `npm install uuid` [url](https://www.npmjs.com/package/uuid)
- `npm i --save-dev @types/uuid` добавление поддержки ts

## Библиотека для управления состоянием в приложениях на JS/TS

- `npm install mobx`
- `npm install mobx-react-lite`

### Проблема 

Решение - Обновление состояния с помощью действий  [runInAction](https://mobx.js.org/actions.html#updating-state-using-actions) 

## Маршрутизация 

Библиотека [React Router](https://reactrouter.com/) | [npm](https://www.npmjs.com/package/react-router)

[create-browser-router](https://reactrouter.com/en/main/routers/create-browser-router)

- `npm i react-router`

Библиотека [React Router Dom](https://www.npmjs.com/package/react-router-dom)

- `npm i react-router-dom`

_[решение проблемы рендеринга компонентов для действий создания и обновления](https://react.dev/learn/preserving-and-resetting-state#option-2-resetting-state-with-a-key)_

## Красивый календарь

Библиотека – [react-calendar](https://www.npmjs.com/package/react-calendar) | [github](https://github.com/wojtekmaj/react-calendar)
- `npm i react-calendar`
- `npm i @types/react-calendar` явное указание на ts

## Валидация 

[FluentValidation.AspNetCore](https://www.nuget.org/packages/FluentValidation.AspNetCore/)
- `dotnet add package FluentValidation.AspNetCore`

## Нотификация

[React Toastify](https://www.npmjs.com/package/react-toastify)

- `npm i react-toastify`

## Формы ввода 

[Formik](https://formik.org/docs/overview#npm)
- `npm install formik`

## Базовая валидация yup

Конструктор схем для анализа и проверки значений во время выполнения.
- `npm i yup`
- `npm i @types/yup`

## Работа с датами

[date-fns](https://date-fns.org/v2.30.0/docs/format)
[React Datepicker](https://reactdatepicker.com/#example-default)
- `npm i react-datepicker`

## Авторизация пользователей

Identity
- `dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore`
- `dotnet ef migrations add identity-added -s PresentationAPI -p Persistence`

## Jwt

1. [JSON Web Tokens](https://jwt.io)
2. [Timestamp в обычную дату](https://i-leon.ru/tools/time)
- `dotnet add package System.IdentityModel.Tokens.Jwt`
- `dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer`

Дополнительная информация по [ASP.NET Core security](https://learn.microsoft.com/en-us/aspnet/core/security/?view=aspnetcore-7.0)

Новая миграция. Отношения много активностей <=> много людей
- `dotnet ef migrations add AppUserActiveness -s PresentationAPI -p Persistence`

## Загрузка связанных сущностей
[Loading Related Entities](https://learn.microsoft.com/en-us/ef/ef6/querying/related-data)

На этом этапе требуется миграция 
- `dotnet ef migrations add add-props-comments -s PresentationAPI -p Persistence`

## Комментарии, библиотека SignalR
- [Introduction to SignalR](https://learn.microsoft.com/en-us/aspnet/signalr/overview/getting-started/introduction-to-signalr)
- [ASP.NET Core SignalR supported platforms](https://learn.microsoft.com/en-us/aspnet/core/signalr/supported-platforms?view=aspnetcore-7.0)
- [Базовый пример](https://github.com/iksergey/SignalR/)

В случае проблем с SSL-сертификатом
(Ошибка: `Unhandled exception. System.Net.Http.HttpRequestException: The SSL connection could not be established, see inner exception.`) 

В терминале выполнить `dotnet dev-certs https --trust` | [подробнее](https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs)

Для JS / TS
- [@microsoft/signalr](https://www.npmjs.com/package/@microsoft/signalr)
- `npm i @microsoft/signalr`


<details><summary>Пример конфигурации</summary>
  
```
{
  "ConnectionStrings": {
    "PostgreSQLConnection": "Host=10.0.0.0:1234; Username=admin; Password=password; Database=test"
  },
  "Secret-key": "A1FCF778S2809K377517D2A1FDE4B2A1FCF75EE1D744",
  "Client-host": "http://127.0.0.1:80",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

</details>
