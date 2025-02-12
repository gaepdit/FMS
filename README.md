# FMS

File Management System for GAEPD Hazardous Waste

[![.NET Test](https://github.com/gaepdit/fms/actions/workflows/dotnet-test.yml/badge.svg)](https://github.com/gaepdit/fms/actions/workflows/dotnet-test.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=gaepdit_FMS&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=gaepdit_FMS)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=gaepdit_FMS&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=gaepdit_FMS)

## Development

There are two launch profiles:

* "FMS Local" -- Does not connect to any external server and no VPN access is needed (or even internet access except to load Google Maps). Uses LocalDB for storage with seed data and creates a local user account.

* "FMS Dev Server" -- Connects to the dev database server, so it requires a VPN connection. Uses your SOG account to log in. *To use this profile, you must add the "appsettings.Development.json" file from the "app-config" repo.*

Most development should be done using the Local profile. The Dev Server profile is only needed when specifically troubleshooting issues with the database server or SOG account.

**NOTE:** In order to load Google Maps on the Location Search page, you must add a Google API key in the "appsettings.Local.json" or "appsettings.Development.json" file.

### Entity Framework database migrations

Instructions for adding a new Entity Framework database migration:

1. Open a command prompt to the `FMS.Infrastructure` project folder.

2. Run the following command with an appropriate migration name:

   `dotnet ef migrations add NAME_OF_MIGRATION --msbuildprojectextensionspath ..\artifacts\FMS.Infrastructure\obj\`

Example to show exact path:

   `TK's path: D:\source\repos\FMS\fms.infrastructure> dotnet ef migrations add NAME_OF_MIGRATION --msbuildprojectextensionspath ..\artifacts\FMS.Infrastructure\obj\`
