using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class ContactType : BaseActiveModel
    {
        public ContactType() { }
        public ContactType(ContactTypeCreateDto contactType)
        {
            Name = contactType.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
