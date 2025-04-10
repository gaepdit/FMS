using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class LocationCreateDto
    {
        [Display(Name = "Score")]
        public string Score { get; set; }

        public void TrimAll()
        {
            Score = Score?.Trim();
        }
    }
}
