using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class ContactTitle : BaseActiveNamedModel
    {
        public ContactTitle() { }

        public ContactTitle(ContactTitleCreateDto contactTitle)
        {
            Name = contactTitle.Name;
        }
    }
}
