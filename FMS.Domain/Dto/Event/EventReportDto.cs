using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class EventReportDto
    {
        public EventReportDto() { }

        public Guid Id { get; set; }

        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }

        public string FacilityNumber { get; set; }

        public string FacilityName { get; set; }

        public FacilityType FacilityType { get; set; }

        public EventType EventType { get; set; }

        public ActionTaken ActionTaken { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? CompletionDate { get; set; }

        public ComplianceOfficer ComplianceOfficer { get; set; }

        public ComplianceOfficer DoneBy { get; set; }

        public OrganizationalUnit OrganizationalUnit { get; set; }

        [DataType(DataType.Currency)]
        public decimal? EventAmount { get; set; }

        public EventContractor EventContractor { get; set; }

        public string Comment { get; set; }

        public OverallStatus OverallStatus { get; set; }

        public DateOnly? ListDate { get; set; }
    }
}
