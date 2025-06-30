using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class AllowedActionTakenSummaryDto
    {
        public AllowedActionTakenSummaryDto(AllowedActionTaken allowedActionTaken)
        {
            Id = allowedActionTaken.Id;
            Active = allowedActionTaken.Active;
            EventTypeId = allowedActionTaken.EventTypeId;
            ActionTakenId = allowedActionTaken.ActionTakenId;
        }

        public Guid Id { get; }

        public bool Active { get; }

        public Guid EventTypeId { get; }
        public EventType EventType { get; }

        public Guid ActionTakenId { get; }
        public ActionTaken ActionTaken { get; }
    }
}
