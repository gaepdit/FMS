using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class LocationEditDto
    {
        public LocationEditDto() { }

        public LocationEditDto(string score)
        {
            Score = score;
        }
        public string Score { get; set; } = string.Empty;
    }
}
