using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class DocumentType : BaseActiveModel
    {
        public DocumentType() { }

        public DocumentType(DocumentTypeCreateDto documentType)
        {
            Name = documentType.Name;
        }

        public string Name { get; set; }

        public void TrimAll()
        {
            Name = Name?.Trim();
        }
    }
}
