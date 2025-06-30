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
            EventTypeName = allowedActionTaken.EventType?.Name ?? string.Empty;
            ActionTakenId = allowedActionTaken.ActionTakenId;
            ActionTakenName = allowedActionTaken.ActionTaken?.Name ?? string.Empty;
        }

        public Guid Id { get; }

        public bool Active { get; }

        public Guid EventTypeId { get; }
        public string EventTypeName { get; set; }

        public Guid ActionTakenId { get; }
        public string ActionTakenName { get; set; }
    }
}
