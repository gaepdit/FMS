using System;

namespace FMS.Domain.Dto
{
    public class GapsAssessmentCreateDto
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
            Description = Description?.Trim();
        }
    }
}
