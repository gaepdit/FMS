# File Management System Routing

## FMS pages

- `/` (Redirects to Login or Facilities)
- `/Cabinets` (List cabinets)
- `/Cabinets/Details/?` (Cabinet details)
- `/Facilities` (Facilities search)
- `/Facilities/Details/?` (Facility details)
- `/Facilities/Map` (Facilities location search)
- `/Files` (Files search)
- `/Files/Details/?` (File details)
- `/Reports` (View reports)
- `/Users` (Search Users)
- `/Users/Details/?` (View user profile)

## FMS admin pages

- `/Cabinets` **POST** (Add a new cabinet)
- `/Cabinets/Edit/?` (Edit cabinet details)
- `/Facilities/Add` (Add a new facility/file)
- `/Facilities/Edit/?` (Edit facility details)
- `/Files/Edit/?` (Edit file details)
- `/Maintenance` (Edit lists, etc.)

## User account pages

- `/Account` (View account profile)
- `/Account/AccessDenied` (Access denied alert)
- `/Account/Login` (Log in)
- `/Account/Logout` (Log out)
- `/Account/Lockout` (Locked out alert)

## User admin pages

- `/Users/Edit/?` (Edit user roles)
