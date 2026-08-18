using FMS.Domain.Entities;

namespace FMS.Helpers
{
    public static class ScoreToolTipHelper
    {
        public const string Score = "<a href='https://gets.sharepoint.com/:x:/r/sites/RRP-Templates/Shared%20Documents/Release_Notifications%20and%20HSI%20Listing/RQSM%20Scoresheet%20Calculator.xls?d=w40774f5702564932b0fd2b78055b9b6e&csf=1&web=1&e=AdKkR3' target='_blank'>Please see RQSM Score Sheet Calculator</a>";
    }
    public static class GWToolTipHelper
    {
        public const string GWScore = "Score > 10 Site listed to HSI for impacts to groundwater";
        public const string AReleaseType = "(45) Known Release, (10) Suspected Release, (5) Potential Future Release";
        public const string B1Susceptibility = "Pollution susceptibility of area according to Hydrologic Atlas 20 - (6) Higher, (3) Average, (0) Lower ";
        public const string B2PhysicalState = "Physical state of the released substance during initial release - (0) Stable Solid, (1) Unstable Solid, (2) Powder/Ash, (3) Liquid/Gas/Sludge";
        public const string CContainment = "Measure of physical barriers preventing regulated substance from migrating - (0) Very Good, (1) Good, (2) Fair, (3) Poor";
        public const string ChemicalName = "Regulated substance used for groundwater evaluation";
        public const string D2ToxVal = "Toxicity Value of regulated substance - (1) None/low, (2), (4), (8), (16) High";
        public const string D3Quantity = "Mass of the regulated substance released - (1) low, (2), (3), (4) default, (5), (6), (7), (8) high";
        public const string E1Exposure = "<a href='https://gets.sharepoint.com/:x:/r/sites/RRP-Templates/Shared%20Documents/Release_Notifications%20and%20HSI%20Listing/RQSM%20Scoresheet%20Calculator.xls?d=w40774f5702564932b0fd2b78055b9b6e&csf=1&web=1&e=AdKkR3' target='_blank'>Please see RQSM Score Sheet Calculator</a>";
        public const string E2DistanceToWell = "Distance from known location of regulated substance to drinking water well in presumed flowpath - (16) < 1/2 Mile, (9) 1/2-1 mile, (4) 1-2 miles, (1) 2-3 miles, (0) > 3 miles";
    }

    public static class OnsiteToolTipHelper
    {
        public const string OSScore = "Score > 20 Site listed to HSI for impacts to soil";
        public const string AAccessToSite = "(0) Inaccessible, (2) Limited access, (4) Unlimited access";
        public const string BReleaseType = "(25) Known Release, (15) Suspected Release, (0) No Release";
        public const string CContainment = "(0) Covered by permanent non-earthern material, (1) Covered by engineered material, (2) Covered by loose earthen fill or native soil, (3) No Cover";
        public const string ChemicalName = "Regulated substance used for on-site evaluation";
        public const string D2ToxVal = "Toxicity Value of regulated substance - (1) None/low, (2), (4), (8), (16) High";
        public const string D3Quantity = "Mass of the regulated substance released - (1) low, (2), (3), (4) default, (5), (6), (7), (8) high";
        public const string E1DistanceToResidence = "Measured from outer edge of affected area to nearest residence, day care, school, playground - (8) < 300ft, (6) 301-1000ft,  (4) 1001-3000ft, (2) 3001-5280ft, (1) > 1 mile";
        public const string E2SensitiveEnvironment = "Sensitive area must lie within the area of a regulated substance and be likely to be affected by the release - (1) Yes, (0) No";
    }

    public static class StatusToolTipHelper
    {
        public const string ISWQS = "Above In-Stream Water Quality Standards?";
        public const string SourceStatus = "ABND - Abandoned, CIP - Cleanup in Progress, INAC - Inactive Site, INV - Investigation Phase, NAT - No Action Taken, NFA - No Further Action, RRS# - Meets Type # RRS";
        public const string SoilStatus = "ABND - Abandoned, CIP - Cleanup in Progress, INAC - Inactive Site, INV - Investigation Phase, NAT - No Action Taken, NFA - No Further Action, RRS# - Meets Type # RRS";
        public const string GroundwaterStatus = "ABND - Abandoned, CIP - Cleanup in Progress, INAC - Inactive Site, INV - Investigation Phase, NAT - No Action Taken, NFA - No Further Action, RRS# - Meets Type # RRS";
        public const string OverallStatus = "ABND - Abandoned, CIP - Cleanup in Progress, INAC - Inactive Site, INV - Investigation Phase, NAT - No Action Taken, NFA - No Further Action, RRS# - Meets Type # RRS";
        public const string GAPSModelScore = "110 or above = High Priority, 70-109 = Medium Priority, 30-69 = Low Priority, 0-30 = Discuss with Unit Coordinator";
        public const string CostEstimate = "RACER estimate";
        public const string FundingSource = "A- Abandoned, LE - Local Government Eligible, LI - Local Government Ineligible, P - Private Party, SE - State Government Eligible, SI - State Government Ineligible";
        public const string GAPSAssessment = "Threat Site may pose to human health or the environment";
    }
}
