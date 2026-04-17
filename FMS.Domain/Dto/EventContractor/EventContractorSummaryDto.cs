using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class EventContractorSummaryDto
    {
        public EventContractorSummaryDto() { }

        public EventContractorSummaryDto(EventContractor contractor)
        {
            Id = contractor.Id;
            Name = contractor.Name;
            Description = contractor.Description;
            Active = contractor.Active;
        }

        public Guid Id { get; set; }

        public bool Active { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
