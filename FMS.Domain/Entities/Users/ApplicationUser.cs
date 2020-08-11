using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMS.Domain.Entities.Users
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [ProtectedPersonalData]
        [StringLength(150)]
        public string GivenName { get; set; }

        [ProtectedPersonalData]
        [StringLength(150)]
        public string Surname { get; set; }

        public string FullName
        {
            get
            {
                return string.Join(" ", new string[] { GivenName, Surname }.Where(s => !string.IsNullOrEmpty(s)));
            }
        }
        public string SortableFullName
        {
            get
            {
                return string.Join(", ", new string[] { Surname, GivenName }.Where(s => !string.IsNullOrEmpty(s)));
            }
        }
    }
}
