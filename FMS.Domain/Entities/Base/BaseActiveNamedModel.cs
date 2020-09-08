namespace FMS.Domain.Entities.Base
{
    public abstract class BaseActiveNamedModel : BaseActiveModel, INamedModel
    {
        // Common name for display purposes
        public string Name { get; set; }
    }
}
