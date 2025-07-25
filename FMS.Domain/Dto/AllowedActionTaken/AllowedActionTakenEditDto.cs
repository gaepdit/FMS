using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AllowedActionTakenEditDto
    {
        public AllowedActionTakenEditDto()
        {
            // Required for EditAllowedActionTaken page
        }

        public AllowedActionTakenEditDto(AllowedActionTaken allowedActionTaken)
        {
            Active = allowedActionTaken.Active;
            EventTypeId = allowedActionTaken.EventTypeId;
            ActionTakenId = allowedActionTaken.ActionTakenId;
        }

        public bool Active { get; set; }

        public Guid EventTypeId { get; set; }
        public EventType EventType { get; set; }

        public Guid ActionTakenId { get; set; }
        public ActionTaken ActionTaken { get; set; }
    }
}
