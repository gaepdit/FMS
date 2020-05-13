using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;

namespace FMS_API.Models
{
    public class User
    {
        public Guid UID { get; set; }

        [Display(Name = "UserName")]
        [StringLength(25)]
        public string UserName { get; set; }

        [Display(Name = "First Name")]
        [StringLength(25)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [StringLength(25)]
        public string LastName { get; set; }

        [Display(Name = "Company Name")]
        [StringLength(50)]
        public string CompanyName { get; set; }

        [Display(Name = "E-mail Address")]
        [EmailAddress]
        [StringLength(150)]
        [DataType(DataType.EmailAddress)]
        public string LoginEmail { get; set; }

        [Display(Name = "EPD Program Name")]
        [StringLength(25)]
        public string ProgramName { get; set; }

        [Display(Name = "Security Level")]
        public int SecurityRole { get; set; }

        [Display(Name = "Is user active?")]
        public bool Active { get; set; }
    }
}
