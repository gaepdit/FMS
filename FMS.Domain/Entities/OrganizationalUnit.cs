using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class OrganizationalUnit : BaseActiveNamedModel
    {
        public OrganizationalUnit() { }

        public OrganizationalUnit(OrganizationalUnitCreateDto newOrganizationalUnit)
        {
            Name = newOrganizationalUnit.Name;
        }
    }
}
