# RiverBooks

RiverBooks is a modular monolith sample built with ASP.NET Core, FastEndpoints, and Entity Framework Core. The solution currently contains a Books module and a web API host.

## Projects

- `RiverBooks.Web` - ASP.NET Core API host.
- `RiverBooks.Books` - Books module, EF Core `BookDbContext`, repository, service, endpoint, and migrations.

## Requirements

- .NET 10 SDK
- SQL Server instance accessible from the application

The projects target `net10.0`. Verify the installed SDK with:

```bash
dotnet --version
```

## Configuration

The Books module reads the `BooksConnectionString` connection string. It is not committed to `appsettings.json`, so configure it locally before calling the API. User Secrets are recommended for development:

```bash
dotnet user-secrets --project RiverBooks.Web init
# dotnet user-secrets --project RiverBooks.Web set \
#   "ConnectionStrings:BooksConnectionString" \
#   "Server=localhost;Database=RiverBooks;User Id=sa;Password=your-password;TrustServerCertificate=True"

# dotnet user-secrets --project RiverBooks.Web init
dotnet user-secrets --project RiverBooks.Web set \
  "ConnectionStrings:BooksConnectionString" \
  "Server=tcp:aaqib-dev-sql.database.windows.net,1433;Initial Catalog=Wiki-Azure-SQL;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

dotnet user-secrets --project RiverBooks.Web set \
  "ConnectionStrings:UsersConnectionString" \
  "Server=tcp:aaqib-dev-sql.database.windows.net,1433;Initial Catalog=Wiki-Azure-SQL;Persist Security Info=False;User ID=sqladmin;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

dotnet user-secrets --project RiverBooks.Web set \
  "ConnectionStrings:BooksConnectionString" \
  "Server=tcp:192.168.1.95,1433;Initial Catalog=BookDb-Windows-PC;User Id=sa;
  Password={your_password}; TrustServerCertificate=True;"

dotnet user-secrets set "ConnectionStrings:BooksConnectionString:Password" "{your_password}"

"BooksConnectionString": "don't recommend putting the real password in appsettings.json -use dotnet user-secrets "

```

Use a connection string appropriate for your SQL Server installation. Do not commit credentials to the repository.

## Run locally

From this directory:

```bash
dotnet restore RiverBooks.slnx
dotnet build RiverBooks.slnx
dotnet run --project RiverBooks.Web --launch-profile http
```

The HTTP profile listens on `http://localhost:5104`. The HTTPS profile listens on `https://localhost:7094` and `http://localhost:5104`:

```bash
dotnet run --project RiverBooks.Web --launch-profile https
```

## Database migrations

Apply the existing migration after configuring the connection string:

```bash
dotnet ef database update \
  --project RiverBooks.Books \
  --startup-project RiverBooks.Web
```

The initial migration creates the `Books` schema and seeds three J.R.R. Tolkien books.

To create a new migration after changing the Books model:

```bash
dotnet ef migrations add <MigrationName> \
  --project RiverBooks.Books \
  --startup-project RiverBooks.Web
```

```bash
dotnet ef migrations add 'Initial-Migration' -c BookDbContext -p ../RiverBooks.Books/RiverBooks.Books.csproj -s ./RiverBooks.Web.csproj -o Data/Migrations
```

```bash
dotnet ef migrations add 'Initial-Users' -c UsersDbContext -p ../RiverBooks.Users/RiverBooks.Users.csproj -s ./RiverBooks.Web.csproj -o Data/Migrations
```

```bash
dotnet ef database update
```

```bash
dotnet ef database update  -c UsersDbContext
```

```bash
 dotnet ef database update -- --environemnt Testing
```

## API

In Development, OpenAPI is available at:

```text
http://localhost:5104/openapi/v1.json
```

List all books:

```bash
curl http://localhost:5104/books
```

The response contains a `books` collection with each book's `id`, `title`, `author`, and `price`.

A ready-to-run request is also available in `HttpFiles/RiverBooks.Web.http`. Bruno requests are in `BrunoFiles/RiverBooks.Web - v1/`.

## Solution commands

```bash
dotnet build RiverBooks.slnx
dotnet test RiverBooks.slnx
```

There are currently no test projects in the solution, so `dotnet test` has no tests to run.
