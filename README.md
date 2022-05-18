# FMS

File Management System for GAEPD Hazardous Waste

[![.NET Test](https://github.com/gaepdit/fms/actions/workflows/dotnet-test.yml/badge.svg)](https://github.com/gaepdit/fms/actions/workflows/dotnet-test.yml)
[![CodeQL](https://github.com/gaepdit/fms/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/gaepdit/fms/actions/workflows/codeql-analysis.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=gaepdit_FMS&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=gaepdit_FMS)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=gaepdit_FMS&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=gaepdit_FMS)

## Development

There are two launch profiles:

* "FMS Local" -- Does not connect to any external server and no VPN access is needed (or even internet access except to load Google Maps). Uses LocalDB for storage with seed data and creates a local user account.

* "FMS Dev Server" -- Connects to the dev database server so it requires a VPN connection. Uses your SOG account to log in. *To use this profile, you must add the "appsettings.Development.json" file from the "app-config" repo.*

Most development should be done using the Local profile. The Dev Server profile is only needed when specifically troubleshooting issues with the database server or SOG account.

**NOTE:** In order to load Google Maps on the Location Search page, you must add a Google API key in the "appsettings.json" file.
