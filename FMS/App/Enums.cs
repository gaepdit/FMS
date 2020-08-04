using System.ComponentModel.DataAnnotations;

namespace FMS
{
    public enum SecurityRole
    {
        [Display(Name = "Admin")] Admin,
        [Display(Name = "Division Manager")] DivisionManager,
        [Display(Name = "Manager")] Manager,
        [Display(Name = "User Account Admin")] UserAdmin,
        [Display(Name = "Data Export")] DataExport,
    }

    internal enum ServerEnvironment
    {
        Development,
        Staging,
        Production,
    }

    public enum SortOrder
    {
        Ascending,
        Descending,
    }

    //     Represents an enumeration of the types of phone numbers.
    public enum PhoneType
    {
        Cell = 0,
        Fax = 1,
        Home = 2,
        Office = 3
    }
}
