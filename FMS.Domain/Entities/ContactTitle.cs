using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class ContactTitle : BaseActiveModel
    {
        public ContactTitle() { }

        public ContactTitle(ContactTitleCreateDto contactTitle)
        {
            Name = contactTitle.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
