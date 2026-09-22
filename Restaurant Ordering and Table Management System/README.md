# Restaurant Ordering and Table Management System — team setup

## What changed from the original upload

The project only had UI forms with hardcoded sample data and no data layer.
This pass adds the instructor's required folder structure and wires up a
skeleton for the whole team to build on:

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

## Mistakes found and fixed

- **AssemblyName didn't match the project name.** The `.csproj` built an
  executable called `Restaurant Ordering and Management System.exe` — missing
  the word "Table" — while the solution, folder, and project title all say
  "...Table Management System". Fixed the `<AssemblyName>` so the built .exe
  name matches. (Left the internal C# namespace,
  `Restaurant_Ordering_and_Management_System`, alone on purpose — renaming
  that would touch every file for a cosmetic reason and risks merge conflicts
  for no real benefit.)
- **No data layer at all.** Every form's grid was filled with hardcoded
  sample rows (`dgvTableStatus.Rows.Add("1", "Available", ...)`, etc.) and
  `InventoryForm`'s Add/Edit/Delete buttons just show a "to be implemented"
  MessageBox. That's expected for a UI-first prototype, but it means none of
  it survives a restart. That's what this skeleton is for.
- **Model naming:** named the table entity `RestaurantTable`, not `Table` —
  the forms already `using System.Data;`, and a class named `Table` sitting
  next to `DataTable`/`DataSet` code invites confusion later.
- **Repeated code that DRY should catch:** the `MessageBox.Show(...,
  MessageBoxButtons.OK, MessageBoxIcon.X)` pattern was repeated across
  `InventoryForm.cs`. `Helper/MessageHelper.cs` wraps it so it's written once.
  As forms get wired to the Service layer, replace direct `MessageBox.Show(...)`
  calls with `MessageHelper.ShowInfo(...)` / `.ShowWarning(...)` / `.Confirm(...)`.
- **Stored procedures, not raw SQL.** Every read/write in `Service/` goes
  through `Helper/DbHelper.cs`, which always sets
  `CommandType.StoredProcedure` — there's no path in this skeleton for
  writing a raw `SELECT`/`INSERT` string, since the instructor specifically
  asked for stored procedures.

## One-time setup (each teammate)

1. Install MySQL locally (or point at a shared instance).
2. Run `Database/schema.sql`, then `Database/stored_procedures.sql`, in that
   order.
3. Edit the `<connectionStrings>` entry in `App.config` with your own
   `Uid`/`Pwd` — don't commit real passwords, this is a school project but
   still good habit.
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

## Suggested task split (4 members)

Each module = one Form + one Model (or two) + one Service + its stored
procedures. Whoever owns a module can work start-to-finish on it without
waiting on anyone else, since all the cross-module contracts are already
defined as interfaces.

| Member | Owns | Files |
|---|---|---|
| 1 — Orders | `FormAddOrder`, the dashboard's recent-orders grid | `Order.cs`, `OrderItem.cs`, `IOrderService.cs`, `OrderService.cs`, the Order/OrderItem/MenuItem procedures |
| 2 — Tables | `FormTables`, the dashboard's table-status grid | `RestaurantTable.cs`, `ITableService.cs`, `TableService.cs`, the Table procedures |
| 3 — Staff | `FormStaff` | `Staff.cs`, `IStaffService.cs`, `StaffService.cs`, the Staff procedures |
| 4 — Inventory & Reports | `InventoryForm`, `FormReports` | `InventoryItem.cs`, `IInventoryService.cs` / `IReportService.cs`, `InventoryService.cs` / `ReportService.cs`, the Inventory/Report procedures |

Whoever sets up the shared foundation (`DBContext`, `Helper`, wiring
`Program.cs`/`Form1` to construct the services) should do that first so the
other three aren't blocked — that part is already done here.

## Still to do (not started)

- `Form1`, `FormTables`, `FormStaff`, `InventoryForm`, `FormReports` still
  need their sample data replaced with real calls into the Service layer
  (construct a `DatabaseConnection` → `DbHelper` → the specific service,
  probably in `Program.cs` or each form's constructor).
- `FormReports` has no date-range controls yet — add them before wiring up
  `IReportService`.
- No login/authentication exists yet (`lblUser` on the dashboard is static
  text) — not mentioned in the task, flagging in case it's expected later.
