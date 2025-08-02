using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class ActionTaken : BaseActiveModel, INamedModel
    {
        public ActionTaken() { }
        public ActionTaken(ActionTakenCreateDto actionTaken)
        {
            Name = actionTaken.Name;
        }
        public ActionTaken(ActionTakenSummaryDto actionTaken)
        {
            Id = actionTaken.Id;
            Name = actionTaken.Name;
            Active = actionTaken.Active;
        }
        public string Name { get; set; }
        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
