using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace FMS_API.Models
{
    public class County
    {
        public int Id { get; set; }

        [StringLength(20)]
        [DisplayFormat(
            NullDisplayText = FMS.NotEnteredDisplayText,
            ConvertEmptyStringToNull = true)]
        public string Name { get; set; }
    }
}
