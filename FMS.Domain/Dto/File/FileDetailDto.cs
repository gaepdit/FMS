using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class FileDetailDto
    {
        public FileDetailDto(File file)
        {
            Id = file.Id;
            Active = file.Active;
            FileLabel = file.FileLabel;
            Facilities = file.Facilities?
                    .Where(e => e.Active)
                    .Select(e => new FacilitySummaryDto(e)).ToList()
                ?? new List<FacilitySummaryDto>();
            Cabinets = new List<string>();
        }

        public Guid Id { get; }
        public bool Active { get; }

        [Display(Name = "File Label")]
        public string FileLabel { get; }

        [Display(Name = "Cabinets")]
        public List<string> Cabinets { get; set; }

        [Display(Name = "Facilities")]
        public List<FacilitySummaryDto> Facilities { get; }
    }
}