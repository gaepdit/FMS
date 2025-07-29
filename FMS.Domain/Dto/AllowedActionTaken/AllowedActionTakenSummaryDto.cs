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
            EventType = allowedActionTaken.EventType;
            ActionTaken = allowedActionTaken.ActionTaken;
        }

        public Guid Id { get; }

        public bool Active { get; }

        public EventType EventType { get; }

        public ActionTaken ActionTaken { get; }
    }
}
