# File Management System - FMS UI Routing

## Publicly available pages

### Public User

All public pages are also available to authenticated users.
Priviledged Users include all previous user priviledges.

- `/` (Login)
- `/Search` (File Search - Initial Landing Page - Text and Map, including Results)
  - `/ShowFacility/?` (Facility Details)
  - `/ShowFile/?` (Show File Contents)
  - `/ShowCabinet/?` (Show File Cabinet Contents)

### Standard Priviledge User

- `/Reports` (Future Reports)

### Edit Priviledge User

- `/FacilityAdd` (Add a new Facility)
- `/FacilityEdit/?` (Edit Facility Details or Delete Facility)

### Admin Priviledge User

- `/AddUser` (Add Users)
- `/EditUsers` (Edit/Delete Users)
- `/EditDropDown` (Edit Drop-Down Menu Items, including Adding Cabinets)

### Login Pages

- `/Account/ConfirmEmail`
- `/Account/SetPassword`
- `/Account/ForgotPassword`
- `/Account/ForgotPasswordConfirmation`
- `/Account/ResetPassword`
- `/Account/AccessDenied`

### Misc

- `/Error`
