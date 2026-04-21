using FMS.Domain.Entities;

namespace FMS.Domain.Dto
{
    public class AbndInacChecklistReviewDto
    {
        public AbndInacChecklistReviewDto() { }

        public AbndInacChecklistReviewDto(AbndInacChecklistReviewDto aiclrtDto)
        {
            HSINumber = aiclrtDto.HSINumber;
            FacilityName = aiclrtDto.FacilityName;
            City = aiclrtDto.City;
            County = aiclrtDto.County;
            AbndInac = aiclrtDto.AbndInac;
            ActionTaken = aiclrtDto.ActionTaken;
            StartDate = aiclrtDto.StartDate;
            DueDate = aiclrtDto.DueDate;
            CompletionDate = aiclrtDto.CompletionDate;
            ComplianceOfficer = aiclrtDto.ComplianceOfficer;
            Comment = aiclrtDto.Comment;
        }

        public string HSINumber { get; set; }

        public string FacilityName { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string AbndInac { get; set; }

        public Guid? EventTypeId { get; set; }
        public EventType EventType { get; set; }

        public Guid? ActionTakenId { get; set; }
        public ActionTaken ActionTaken { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? CompletionDate { get; set; }

        public Guid? ComplianceOfficerId { get; set; }
        public ComplianceOfficer ComplianceOfficer { get; set; }

        public string Comment { get; set; }
    }
}
