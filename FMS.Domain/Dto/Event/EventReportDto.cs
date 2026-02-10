using DocumentFormat.OpenXml.Office.Y2022.FeaturePropertyBag;
using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class EventReportDto
    {
        public EventReportDto() { }

        public EventReportDto(Event eventNew)
        {
            Id = eventNew.Id;
            FacilityId = eventNew.FacilityId;
            ParentId = eventNew.ParentId;
            EventType = eventNew.EventType;
            ActionTaken = eventNew.ActionTaken;
            StartDate = eventNew.StartDate;
            DueDate = eventNew.DueDate;
            CompletionDate = eventNew.CompletionDate;
            ComplianceOfficer = eventNew.ComplianceOfficer;
            EventAmount = eventNew.EventAmount;
            EventContractor = eventNew.EventContractor;
            Comment = eventNew.Comment;
        }

        public Guid Id { get; set; }

        public Guid FacilityId { get; set; }

        public Guid? ParentId { get; set; }

        public string FacilityNumber { get; set; } 

        public string FacilityName { get; set; } 

        public EventType EventType { get; set; }

        public ActionTaken ActionTaken { get; set; }

        public DateOnly? StartDate { get; set; }

        public DateOnly? DueDate { get; set; }

        public DateOnly? CompletionDate { get; set; }

        public ComplianceOfficer ComplianceOfficer { get; set; }

        public OrganizationalUnit OrganizationalUnit { get; set; }

        [DataType(DataType.Currency)]
        public decimal? EventAmount { get; set; }

        public EventContractor EventContractor { get; set; }

        public string Comment { get; set; }
    }
}
