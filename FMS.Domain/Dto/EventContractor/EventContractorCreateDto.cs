using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class EventContractorCreateDto
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
