using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;
using System.Xml.Linq;

namespace FMS.Domain.Entities
{
    public class Chemical : BaseActiveModel
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

        public string Name => CasNo;

        public string DisplayName => $"{CasNo} ({ChemicalName})";

        public void TrimAll()
        {
            CasNo = CasNo?.Trim();
            ChemicalName = ChemicalName?.Trim();
            ToxValue = ToxValue?.Trim();
            MCLs = MCLs?.Trim();
        }
    }   
}
