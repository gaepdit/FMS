using FMS.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMS.Domain.Dto
{
    public class ActionTakenSummaryDto
    {
        public ActionTakenSummaryDto(ActionTaken actionTaken)
        {
            Id = actionTaken.Id;
            Active = actionTaken.Active;
            Name = actionTaken.Name;
        }

        public Guid Id { get; }

        public bool Active { get; }

        [Display(Name = "Action Taken")]
        public string Name { get; }
    }
}
