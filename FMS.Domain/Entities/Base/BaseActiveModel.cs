namespace FMS.Domain.Entities.Base
{
    public abstract class BaseActiveModel : BaseModel
    {
        public bool Active { get; set; } = true;
    }
}
