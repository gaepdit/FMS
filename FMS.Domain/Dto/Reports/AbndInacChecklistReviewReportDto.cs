using ClosedXML.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AbndInacChecklistReviewReportDto
    {
        public AbndInacChecklistReviewReportDto() { }

        public AbndInacChecklistReviewReportDto(AbndInacChecklistReviewDto aiclrtDto) 
        {
            HSINumber = aiclrtDto.HSINumber;
            FacilityName = aiclrtDto.FacilityName;
            City = aiclrtDto.City;
            County = aiclrtDto.County;
            AbndInac = aiclrtDto.AbndInac;
            ActionTakenName = aiclrtDto.ActionTaken?.Name;
            StartDate = aiclrtDto.StartDate;
            DueDate = aiclrtDto.DueDate;
            CompletionDate = aiclrtDto.CompletionDate;
            ComplianceOfficerName = aiclrtDto.ComplianceOfficer?.Name;
            Comment = aiclrtDto.Comment;
        }

        [XLColumn(Header = "HSI ID")]
        [Display(Name = "HSI ID")]
        public string HSINumber { get; set; }

        [XLColumn(Header = "Facility Name")]
        [Display(Name = "Facility Name")]
        public string FacilityName { get; set; }

        [XLColumn(Header = "City")]
        [Display(Name = "City")]
        public string City { get; set; }

        [XLColumn(Header = "County")]
        [Display(Name = "County")]
        public string County { get; set; }

        [XLColumn(Header = "Abnd/Inac")]
        [Display(Name = "Abnd/Inac")]
        public string AbndInac { get; set; }

        [XLColumn(Header = "Action Taken")]
        [Display(Name = "Action Taken")]
        public string ActionTakenName { get; set; }

        [XLColumn(Header = "Start Date")]
        [Display(Name = "Start Date")]
        public DateOnly? StartDate { get; set; }

        [XLColumn(Header = "Due Date")]
        [Display(Name = "Due Date")]
        public DateOnly? DueDate { get; set; }

        [XLColumn(Header = "Completion Date")]
        [Display(Name = "Completion Date")]
        public DateOnly? CompletionDate { get; set; }

        [XLColumn(Header = "Done By (CO)")]
        [Display(Name = "Done By (CO)")]
        public string ComplianceOfficerName { get; set; }

        [XLColumn(Header = "Comment")]
        [Display(Name = "Comment")]
        public string Comment { get; set; }
    }
}
