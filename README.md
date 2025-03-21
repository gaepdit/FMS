# FMS

File Management System for GAEPD Hazardous Waste

[![.NET Test](https://github.com/gaepdit/fms/actions/workflows/dotnet-test.yml/badge.svg)](https://github.com/gaepdit/fms/actions/workflows/dotnet-test.yml)
[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=gaepdit_FMS&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=gaepdit_FMS)
[![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=gaepdit_FMS&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=gaepdit_FMS)


## Background and Phases

### Phase I

The File Management System (FMS) was created to replace a legacy system to house the physical location of Files, Folders and File Cabinets used for Hazardous Waste Program documents. The original Database data was migrated into a new MS SQL Server database with a .NET Core C# backend and a C# Razorpage Web-Based Front-End. It is a Facility-Based system that has entensive search functionality in text or map form, and is greatly customizable.

### Phase II

This phase was done to upgrade the existing platform and database to also hold Release Notification information. This required modification of the UI and addition of more facility information and migration of the Legacy MS Access database housing Release Notification data. 

### Phase III (In Progress)

This phase is a major upgrade in functionality, adding Hazardous Waste information for Facilities and Project Tracking. This modification will migrate the existing HSRP MS Access Database into FMS. HSI Facilities will then be able to be tracked and all data will be able to be displayed, as well as reports run. 

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
