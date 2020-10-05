using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileSpec
    {
        [StringLength(9)]
        [Display(Name = "File Label")]
        public string FileLabel { get; set; }

        [Display(Name = "County")]
        public int? CountyId { get; set; }

        [Display(Name = "Show deleted")]
        public bool ShowInactive { get; set; }

        public IDictionary<string, string> AsRouteValues =>
            new Dictionary<string, string>
            {
                {nameof(ShowInactive), ShowInactive.ToString()},
                {nameof(FileLabel), FileLabel},
                {nameof(CountyId), CountyId?.ToString()},
            };
    }
}