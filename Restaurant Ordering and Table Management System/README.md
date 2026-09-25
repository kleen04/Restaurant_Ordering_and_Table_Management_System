# Restaurant Ordering and Table Management System — team setup

## What changed from the original upload

The project only had UI forms with hardcoded sample data and no data layer.
This wires up a skeleton for the whole team to build on:

```
Restaurant Ordering and Table Management System/
├── DBContext/       -> DatabaseConnection.cs (opens the MySQL connection)
├── Helper/          -> DbHelper.cs, MessageHelper.cs, ValidationHelper.cs
├── Interfaces/      -> one interface per feature area (I*Service.cs)
├── Models/          -> plain data classes (Staff, RestaurantTable, MenuItem, InventoryItem, Order, OrderItem)
├── Service/         -> one class per interface, calling stored procedures only
├── Forms/           -> all the existing forms, moved here as asked
├── Database/        -> schema.sql + stored_procedures.sql
├── Program.cs
└── App.config       -> now has a <connectionStrings> section
```

Each Service class has **one method already implemented as a working
example** (usually the "get all" read), and the rest are `throw new
NotImplementedException()` with a `// TODO(<module> owner): ...` comment
saying exactly which stored procedure to call and what it should do. Copy
the pattern from the finished method.

## One-time setup 

1. Install MySQL locally (or point at a shared instance).
2. Run `Database/schema.sql`, then `Database/stored_procedures.sql`, in that
   order.
3. Edit the `<connectionStrings>` entry in `App.config` with your own
   `Uid`/`Pwd`.
4. Open the solution in Visual Studio, then **Tools → NuGet Package
   Manager → Package Manager Console** and run:
   ```
   Install-Package MySql.Data -Version 8.0.33
   ```
   The `.csproj` already has a `packages.config` and a `MySql.Data`
   `<Reference>` pointing at where NuGet *should* put the DLL — if the
   HintPath doesn't match after restore, right-click the reference in
   Solution Explorer → Remove, then re-add it from
   `packages\MySql.Data.<version>\lib\...\MySql.Data.dll`.

## How the pieces fit together (SOLID/DRY, in plain terms)

- **Single Responsibility** — `DatabaseConnection` only knows how to open a
  connection. `DbHelper` only knows how to run a stored procedure. Each
  `*Service` only knows one feature area. Forms only know how to display
  things and react to clicks.
- **Dependency Inversion** — Forms depend on `IStaffService` /
  `ITableService` / etc., not on `StaffService` directly. That's what makes
  it possible for four people to work on different Service classes at once
  without touching each other's Form code, and to swap in a fake
  implementation later if you ever want to test a form without a real
  database.
- **DRY** — `DbHelper` is the one place that opens a `MySqlConnection` and
  runs a command; `MessageHelper` is the one place that formats a
  `MessageBox`. No Service or Form should contain its own
  `new MySqlConnection(...)` — if you find yourself typing that, use
  `DbHelper` instead.
