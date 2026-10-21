# NuGet Package maintenance

The following NuGet packages have been added directly to work around vulnerable dependencies in other packages.

- `System.Security.Cryptography.Xml` was added to `FMS.App.Tests` to avoid a vulnerable version referenced in
  `Microsoft.Identity.Web`.

- `System.Security.Cryptography.Xml` was added to `FMS.Infrastructure.Tests` to avoid a vulnerable version referenced in
  `EfCore.TestSupport`.

- `SQLitePCLRaw.lib.e_sqlite3` was added to `FMS.Infrastructure.Tests` to avoid a vulnerable version referenced in
  `EfCore.TestSupport` (via `Microsoft.EntityFrameworkCore.Sqlite`).

- `System.Security.Cryptography.Pkcs` was added to `FMS.Infrastructure` to avoid a vulnerable version referenced in
  `Microsoft.Data.SqlClient`.
