using FMS.Domain.Entities;

namespace FMS.Helpers
{
    public class ScoreToolTipHelper
    {
        public const string Score = "<a href='https://gets.sharepoint.com/:x:/r/sites/RRP-Templates/Shared%20Documents/Release_Notifications%20and%20HSI%20Listing/RQSM%20Scoresheet%20Calculator.xls?d=w40774f5702564932b0fd2b78055b9b6e&csf=1&web=1&e=AdKkR3' target='_blank'>Please see RQSM Score Sheet Calculator</a>";
    }
    public class GWToolTipHelper
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
}
