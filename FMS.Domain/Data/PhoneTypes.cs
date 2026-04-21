namespace FMS.Domain.Data
{
    public static partial class Data
    {
        public static List<string> PhoneTypes => new List<string>
        {
            "Cell",
            "Office",
            "Fax",
            "Home",
            "Other"
        };
    }
}
