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

        //public EventReportDto(EventReportDto eventEntity)
        //{
        //    Id = eventEntity.Id;
        //    FacilityId = eventEntity.FacilityId;
        //    ParentId = eventEntity.ParentId;
        //    FacilityNumber = eventEntity.FacilityNumber;
        //    FacilityName = eventEntity.FacilityName;
        //    FacilityType = eventEntity.FacilityType;
        //    EventType = eventEntity.EventType;
        //    ActionTaken = eventEntity.ActionTaken;
        //    StartDate = eventEntity.StartDate;
        //    DueDate = eventEntity.DueDate;
        //    CompletionDate = eventEntity.CompletionDate;
        //    ComplianceOfficer = eventEntity.ComplianceOfficer;
        //    DoneBy = eventEntity.DoneBy;
        //    OrganizationalUnit = eventEntity.OrganizationalUnit;
        //    EventAmount = eventEntity.EventAmount;
        //    EventContractor = eventEntity.EventContractor;
        //    Comment = eventEntity.Comment;
        //    OverallStatus = eventEntity.OverallStatus;
        //    ListDate = eventEntity.ListDate;
        //}

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
