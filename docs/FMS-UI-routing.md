# File Management System (FMS) Site Map

## Landing page

- `/` (Redirects to Login Page or Facilities Search Page)

| Redirect  | To  |
|---|---|
| `/`  | `/Account/Login?` (To Login) |
| `/`  | `/Facilities` (If Logged in) |

## Menu Bar

- `Facility Search` (Links to `/Facilities`)
- `Location Search` (Links to `/Facilities/Map/`)
- `Add Facility` (Links to `/Facilities/Add/`)
- `More (Drop-Down)` (links to `/Files`, `/Cabinets/`, `/Users`, `/Maintenance`)

## Facilities Pages

- `/Facilities` (Facilities search)
- `/Facilities/Add/` (Add a new facility/file)
- `/Facilities/Delete/` (Delete a facility)
- `/Facilities/Details/?` (Facility details)
- `/Facilities/Details/?` **POST** (Add retention record)
- `/Facilities/Edit/?` (Edit facility details)
- `/Facilities/EditRetentionRecord/?` (Edit retention record)
- `/Facilities/Map/` (Facilities location search)
- `/Facilities/Undelete/` (Restore a deleted facility)

## Account Pages

- `/Account` (View account profile or Sign out)
- `/Account/AccessDenied` (Access denied alert)
- `/Account/Lockout` (Locked out alert)
- `/Account/Login` (Log in)

## Cabinet Pages
- `/Cabinets/` (List cabinets)
- `/Cabinets/Add/` (Add a new cabinet)
- `/Cabinets/Details/{Cabinet Number}` (Cabinet details by Cabinet Number)
- `/Cabinets/Edit/{id}}` (Edit cabinet details by id)

## Files Pages 

- `/Files` (Files search)
- `/Files/Details/{File Number}` (File details by File Number. ***List of Facilities within a File***)

## Maintenance Pages

- `/Maintenance` (List of Drop-Down Menues to Edit)

>#### Budget Code Pages

>- `/Maintenance/BudgetCode` (List of all Budget Codes, Includes remove from and restore to list)
>- `/Maintenance/BudgetCode/Add` (Add New Budget Code)
>- `/Maintenance/BudgetCode/Edit/{id}` (Edit Budget Code by id)

>#### Compliance Officer Pages

>- `/Maintenance/ComplianceOfficer` (List of all Users that have ever logged in. Includes removed and restored to List)

>#### Facility Status Pages

>- `/Maintenance/FacilityStatus` (List of all Facility Statuses. Includes remove from and restore to List)
>- `/Maintenance/FacilityStatus/Add` (Add New Facility Status)
>- `/Maintenance/FacilityStatus/Edit/{id}` (Edit New Facility Status by id)

>#### Type/Environmental Interest Pages

>- `/Maintenance/FacilityType` (List of Facility Types. Includes remove from and restore to List)
>- `/Maintenance/FacilityType/Add` (Add New Facility Type)
>- `/Maintenance/FacilityType/Edit{id}` (Edit Facility Type by id)

>#### Organizational Unit Pages

>- `/Maintenance/OrganizationalUnit` (List of Organizational Units. Includes remove from and restore to List)
>- `/Maintenance/OrganizationalUnit/Add` (Add New Organizational Unit)
>- `/Maintenance/OrganizationalUnit/Edit/{id}` (Edit Organizational Unit by id)

>#### Status Pages

>- `/Maintenance/Status` (List of Statuses. Includes remove from and restore to List)
>- `/Maintenance/Status/Add` (Add New Status)
>- `/Maintenance/Status/Edit/{id}` (Edit Status by id)

>#### Substances Pages

>- `/Maintenance/Substances` (List of Substances. Includes remove from and restore to List)
>- `/Maintenance/Substances/Add` (Add New Substance)
>- `/Maintenance/Substances/Edit/{id}` (Edit Substances by id)

>#### Chemicals Pages

>- `/Maintenance/Chemicals` (List of Chemicals. Includes remove from and restore to List)
>- `/Maintenance/Chemicals/Add` (Add New Chemicals)
>- `/Maintenance/Chemicals/Edit/{id}` (Edit Chemicals by id)

>#### MajorCode Pages

>- `/Maintenance/MajorCode` (List of MajorCodes. Includes remove from and restore to List)
>- `/Maintenance/MajorCode/Add` (Add New MajorCode)
>- `/Maintenance/MajorCode/Edit/{id}` (Edit MajorCode by id)
>- `/Maintenance/MajorCode/PermittedCodes/{id}` (Add and remove Permitted MinorCodes Associated with the MajorCode)

>#### MinorCode Pages

>- `/Maintenance/MinorCode` (List of MinorCodes. Includes remove from and restore to List)
>- `/Maintenance/MinorCode/Add` (Add New MinorCode)
>- `/Maintenance/MinorCode/Edit/{id}` (Edit MinorCode by id)

## Report Pages (Under Construction)

- `/Reports/` (View Report Preview. Allow Export to MS Excel) 
-  ***Todo - Additional Report Pages***

## User Pages

- `/Users` (Search Users)
- `/Users/Details/{id}` (View user profile by id)
- `/Users/Edit/{id}` (Edit user roles by id)

