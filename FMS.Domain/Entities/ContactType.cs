using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class ContactType : BaseActiveNamedModel
    {
        public ContactType() { }

        public ContactType(ContactTypeCreateDto contactType)
        {
            Name = contactType.Name;
        }
    }
}
