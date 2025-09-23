using FMS.Domain.Dto;
using FMS.Domain.Entities.Base;

namespace FMS.Domain.Entities
{
    public class Chemical : BaseActiveModel, INamedModel
    {
        public Chemical() { }

        public Chemical(ChemicalCreateDto chemical)
        {
            CasNo = chemical.CasNo;
            ChemicalName = chemical.ChemicalName;
            CommonName = chemical.CommonName;
            ToxValue = chemical.ToxValue;
            MCLs = chemical.MCLs;
            FinalRc = chemical.FinalRc;
            RQ = chemical.RQ;
        }

        public string CasNo { get; set; }

        public string ChemicalName { get; set; }

        public string CommonName { get; set; }

        public string ToxValue { get; set; }

        public string MCLs { get; set; }

        public string FinalRc { get; set; }

        public string RQ { get; set; }

        public string Name => CasNo;

        public string DisplayName => $"{CasNo} ({ChemicalName})";

        public string FullName => $"{CasNo}, {ChemicalName}, {CommonName}";

        public void TrimAll()
        {
            CasNo = CasNo?.Trim();
            ChemicalName = ChemicalName?.Trim();
            CommonName = CommonName?.Trim();
            ToxValue = ToxValue?.Trim();
            MCLs = MCLs?.Trim();
        }
    }   
}
