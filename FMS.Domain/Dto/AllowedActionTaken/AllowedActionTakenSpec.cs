using FMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMS.Domain.Dto
{
    public class AllowedActionTakenSpec
    {
        public AllowedActionTakenSpec() { }

        //public AllowedActionTakenSpec(AllowedActionTaken allowedActionTaken)
        //{
        //    Id = allowedActionTaken.Id;
        //    Active = allowedActionTaken.Active;
        //    EventTypeId = allowedActionTaken.EventType.Id;
        //    EventTypeName = allowedActionTaken.EventType.Name;
        //    EventTypeActive = allowedActionTaken.EventType.Active;
        //    ActionTakenId = allowedActionTaken.ActionTaken.Id;
        //    ActionTakenName = allowedActionTaken.ActionTaken.Name;
        //    ActionTakenActive = allowedActionTaken.ActionTaken.Active;
        //}
        public Guid Id { get; set; }
        public bool Active { get; set; }
        public Guid EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        public bool EventTypeActive { get; set; }
        public Guid ActionTakenId { get; set; }
        public string ActionTakenName { get; set; } 
        public bool ActionTakenActive { get; set; }

        //public IDictionary<string, string> AsRouteValues =>
        //    new Dictionary<string, string>
        //    {
        //        {nameof(Id), Id.ToString()},
        //        {nameof(Active), Active.ToString()},
        //        {nameof(EventTypeId), EventTypeId.ToString()},
        //        {nameof(EventTypeName), EventTypeName},
        //        {nameof(EventTypeActive), EventTypeActive.ToString()},
        //        {nameof(ActionTakenId), ActionTakenId.ToString()},
        //        {nameof(ActionTakenName), ActionTakenName},
        //        {nameof(ActionTakenActive), ActionTakenActive.ToString()}
        //    };
    }
}
