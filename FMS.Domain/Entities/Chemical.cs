using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Chemical : BaseActiveNamedModel
    {
        public Chemical() { }
        public Chemical(ChemicalCreateDto chemical)
        {
            CasNo = chemical.CasNo;
            ChemicalName = chemical.ChemicalName;
            ToxValue = chemical.ToxValue;
            MCLs = chemical.MCLs;
        }
        public string CasNo { get; set; }
        public string ChemicalName { get; set; }
        public string ToxValue { get; set; }
        public string MCLs { get; set; }
        public void TrimAll()
        {
            CasNo = CasNo?.Trim();
            ChemicalName = ChemicalName?.Trim();
            ToxValue = ToxValue?.Trim();
            MCLs = MCLs?.Trim();
        }
    }   
}
