using FMS.Domain.Dto;
using FMS.Domain.Repositories;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FMS.Helpers
{
    public class SiteSummaryHelper
    {
        private readonly IReportingRepository _repository;
        public SiteSummaryHelper(IReportingRepository repository) => _repository = repository;

        public enum reportBatchType
        {
            Single,
            ComplianceOfficer,
            County,
            All
        }

        public async Task<IList<SiteSummaryReportDto>> GetSiteSummaryReports(reportBatchType batchType, SiteSummaryQuerySpec spec)
        {
            var ssReport = new List<SiteSummaryReportDto>();

            switch (batchType)
            {
                case reportBatchType.Single:
                    {

                        break;
                    }
                case reportBatchType.ComplianceOfficer:
                    {

                        break;
                    }
                case reportBatchType.County:
                    {
                        break;
                    }
                case reportBatchType.All:
                    {
                        break;
                    }
            }

            return ssReport;
        }

        public static string GetCleanupStatusParameter(SiteSummaryReportDto facility)
        {
            var cleanupStatusParam = "";
            var SESTAT = "";
            var SLSTAT = "";
            var GWSTAT = "";
            var Source_Stat = facility.StatusDetails.SourceStatus.Name;
            var Soil_stat = facility.StatusDetails.SoilStatus.Name;
            var GW_Stat = facility.StatusDetails.GroundwaterStatus.Name;

            if (Source_Stat == "RRS5")
            {
                SESTAT = "5";
            }
            else
            {
                SESTAT = Strings.Left(Source_Stat, 3);
            }
            if (Soil_stat == "RRS5")
            {
                SLSTAT = "5";
            }
            else
            {
                SLSTAT = Strings.Left(Soil_stat, 3);
            }

            if (GW_Stat == "RRS5")
            {
                GWSTAT = "5";
            }
            else
            {
                GWSTAT = Strings.Left(GW_Stat, 3);
            }

            cleanupStatusParam = string.Concat(SESTAT, SLSTAT, GWSTAT);

            return cleanupStatusParam;
        }

        public static string GetCleanupStatusLanguage(SiteSummaryReportDto facility)
        {
            var cleanupstat = GetCleanupStatusParameter(facility);
            var landFill = facility.StatusDetails.LandFill;
            var mrrs = "Hazardous Site Response Act cleanup levels have been met for ";
            var mrrs5_1 = "Hazardous Site Response Act cleanup levels have been met for ";
            var mrrs5_2 = " through institutional and engineering controls to eliminate or reduce present and future threats to human health and the environment.";
            var mcip = "Cleanup activities are being conducted for ";
            var minv = "Investigations are being conducted to determine how much cleanup is necessary for ";
            var mnat = "EPD has not yet directed the responsible parties to begin investigation or cleanup under the Hazardous Site Response Act for ";
            var msource = "source materials";
            var msoil = "soil";
            var mgwater = "groundwater";
            var mssgall = "source materials, soil, and groundwater";
            var mssl = "source materials and soil";
            var msgw = "source materials and groundwater";
            var mslgw = "soil and groundwater";
            var mper = ".";
            var minac1 = "Although directed to by EPD, identified responsible parties are not currently performing the required investigation and/or cleanup under the Hazardous Site Response Act for ";
            var minac2 = ". Enforcement is pending.";
            var mabnd1 = "Identified responsible parties are unable or unwilling to conduct the required investigation and/or cleanup under the Hazardous Site Response Act for ";
            var mabnd2 = ". EPD may perform these actions using state funding from the Hazardous Waste Trust Fund and may lien the property or pursue cost recovery.";

            string clup;
            switch (cleanupstat)
            {
                case "555":
                    {
                        // & Same Code for all 3 Status
                        clup = mrrs5_1 + (landFill ? msgw : mssgall) + mrrs5_2;
                        break;
                    }
                case "RRSRRSRRS":
                    {
                        clup = mrrs + (landFill ? msgw : mssgall) + mper;
                        break;
                    }
                case "CIPCIPCIP":
                    {
                        clup = mcip + (landFill ? msgw : mssgall) + mper;
                        break;
                    }
                case "INVINVINV":
                    {
                        clup = minv + (landFill ? msgw : mssgall) + mper;
                        break;
                    }
                case "NATNATNAT":
                    {
                        clup = mnat + (landFill ? msgw : mssgall) + mper;
                        break;
                    }
                case "55RRS":
                    {
                        clup = mrrs5_1 + (landFill ? msource : mssl) + mrrs5_2 + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "5RRS5":
                    {
                        clup = mrrs5_1 + msgw + mrrs5_2 + "  " + (landFill ? "" : mrrs + msoil + mper);
                        break;
                    }
                case "RRS55":
                    {
                        clup = mrrs5_1 + (landFill ? mgwater : mslgw) + mrrs5_2 + "  " + mrrs + msource + mper;
                        break;
                    }
                case "55CIP":
                    {
                        clup = mrrs5_1 + (landFill ? msource : mssl) + mrrs5_2 + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "5CIP5":
                    {
                        clup = mrrs5_1 + msgw + mrrs5_2 + "  " + (landFill ? "" : mcip + msoil + mper);
                        break;
                    }
                case "CIP55":
                    {
                        clup = mrrs5_1 + (landFill ? mgwater : mslgw) + mrrs5_2 + "  " + mcip + msource + mper;
                        break;
                    }
                case "55INV":
                    {
                        clup = mrrs5_1 + (landFill ? msource : mssl) + mrrs5_2 + "  " + minv + mgwater + mper;
                        break;
                    }
                case "5INV5":
                    {
                        clup = mrrs5_1 + msgw + mrrs5_2 + "  " + (landFill ? "" : minv + msoil + mper);
                        break;
                    }
                case "INV55":
                    {
                        clup = mrrs5_1 + (landFill ? msource : mslgw) + mrrs5_2 + "  " + minv + msource + mper;
                        break;
                    }
                case "55NAT":
                    {
                        clup = mrrs5_1 + (landFill ? msource : mssl) + mrrs5_2 + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "5NAT5":
                    {
                        clup = mrrs5_1 + msgw + mrrs5_2 + "  " + (landFill ? "" : mnat + msoil + mper);
                        break;
                    }
                case "NAT55":
                    {
                        clup = mrrs5_1 + (landFill ? msource : mslgw) + mrrs5_2 + "  " + mnat + msource + mper;
                        break;
                    }
                case "RRSRRS5":
                    {
                        clup = mrrs + (landFill ? msource : mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "RRS5RRS":
                    {
                        clup = mrrs + msgw + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2);
                        break;
                    }
                case "5RRSRRS":
                    {
                        clup = mrrs + (landFill ? msource : mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                        break;
                    }
                case "RRSRRSCIP":
                    {
                        clup = mrrs + (landFill ? msource : mssl) + mper + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "RRSCIPRRS":
                    {
                        clup = mrrs + msgw + mper + "  " + (landFill ? "" : mcip + msoil + mper);
                        break;
                    }
                case "CIPRRSRRS":
                    {
                        clup = mrrs + (landFill ? msource : mslgw) + mper + "  " + mcip + msource + mper;
                        break;
                    }
                case "RRSRRSINV":
                    {
                        clup = mrrs + (landFill ? msource : mssl) + mper + "  " + minv + mgwater + mper;
                        break;
                    }
                case "RRSINVRRS":
                    {
                        clup = mrrs + msgw + mper + "  " + (landFill ? "" : minv + msoil + mper);
                        break;
                    }
                case "INVRRSRRS":
                    {
                        clup = mrrs + (landFill ? mgwater : mslgw) + mper + "  " + minv + msource + mper;
                        break;
                    }
                case "RRSRRSNAT":
                    {
                        clup = mrrs + (landFill ? msource : mssl) + mper + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "RRSNATRRS":
                    {
                        clup = mrrs + msgw + mper + "  " + (landFill ? "" : mnat + msoil + mper);
                        break;
                    }
                case "NATRRSRRS":
                    {
                        clup = mrrs + (landFill ? mgwater : mslgw) + mper + "  " + mnat + msource + mper;
                        break;
                    }
                case "CIPCIP5":
                    {
                        clup = mcip + (landFill ? msource : mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "CIP5CIP":
                    {
                        clup = mcip + msgw + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2);
                        break;
                    }
                case "5CIPCIP":
                    {
                        clup = mcip + (landFill ? mgwater : mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                        break;
                    }
                case "CIPCIPRRS":
                    {
                        clup = mcip + (landFill ? msource : mssl) + mper + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "CIPRRSCIP":
                    {
                        clup = mcip + msgw + mper + "  " + (landFill ? "" : mrrs + msoil + mper);
                        break;
                    }
                case "RRSCIPCIP":
                    {
                        clup = mcip + (landFill ? mgwater : mslgw) + mper + "  " + mrrs + msource + mper;
                        break;
                    }
                case "CIPCIPINV":
                    {
                        clup = mcip + (landFill ? msource : mssl) + mper + "  " + minv + mgwater + mper;
                        break;
                    }
                case "CIPINVCIP":
                    {
                        clup = mcip + msgw + mper + "  " + (landFill ? "" : minv + msoil + mper);
                        break;
                    }
                case "INVCIPCIP":
                    {
                        clup = mcip + (landFill ? mgwater : mslgw) + mper + "  " + minv + msource + mper;
                        break;
                    }
                case "CIPCIPNAT":
                    {
                        clup = mcip + (landFill ? msource : mssl) + mper + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "CIPNATCIP":
                    {
                        clup = mcip + msgw + mper + "  " + (landFill ? "" : mnat + msoil + mper);
                        break;
                    }
                case "NATCIPCIP":
                    {
                        clup = mcip + (landFill ? mgwater : mslgw) + mper + "  " + mnat + msource + mper;
                        break;
                    }
                case "INVINV5":
                    {
                        clup = minv + (landFill ? msource : mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "INV5INV":
                    {
                        clup = minv + msgw + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2);
                        break;
                    }
                case "5INVINV":
                    {
                        clup = minv + (landFill ? mgwater : mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                        break;
                    }
                case "INVINVRRS":
                    {
                        clup = minv + (landFill ? msource : mssl) + mper + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "INVRRSINV":
                    {
                        clup = minv + msgw + mper + "  " + (landFill ? "" : mrrs + msoil + mper);
                        break;
                    }
                case "RRSINVINV":
                    {
                        clup = minv + (landFill ? mgwater : mslgw) + mper + "  " + mrrs + msource + mper;
                        break;
                    }
                case "INVINVCIP":
                    {
                        clup = minv + (landFill ? msource : mssl) + mper + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "INVCIPINV":
                    {
                        clup = minv + msgw + mper + "  " + (landFill ? "" : mcip + msoil + mper);
                        break;
                    }
                case "CIPINVINV":
                    {
                        clup = minv + (landFill ? mgwater : mslgw) + mper + "  " + mcip + msource + mper;
                        break;
                    }
                case "INVINVNAT":
                    {
                        clup = minv + (landFill ? msource : mssl) + mper + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "INVNATINV":
                    {
                        clup = minv + msgw + mper + "  " + (landFill ? "" : mnat + msoil + mper);
                        break;
                    }
                case "NATINVINV":
                    {
                        clup = minv + (landFill ? mgwater : mslgw) + mper + "  " + mnat + msource + mper;
                        break;
                    }
                case "NATNAT5":
                    {
                        clup = mnat + (landFill ? msource : mssl) + mper + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "NAT5NAT":
                    {
                        clup = mnat + msgw + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2);
                        break;
                    }
                case "5NATNAT":
                    {
                        clup = mnat + (landFill ? mgwater : mslgw) + mper + "  " + mrrs5_1 + msource + mrrs5_2;
                        break;
                    }
                case "NATNATRRS":
                    {
                        clup = mnat + (landFill ? msource : mssl) + mper + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "NATRRSNAT":
                    {
                        clup = mnat + msgw + mper + "  " + (landFill ? "" : mrrs + msoil + mper);
                        break;
                    }
                case "RRSNATNAT":
                    {
                        clup = mnat + (landFill ? mgwater : mslgw) + mper + "  " + mrrs + msource + mper;
                        break;
                    }
                case "NATNATCIP":
                    {
                        clup = mnat + (landFill ? msource : mssl) + mper + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "NATCIPNAT":
                    {
                        clup = mnat + msgw + mper + "  " + (landFill ? "" : mcip + msoil + mper);
                        break;
                    }
                case "CIPNATNAT":
                    {
                        clup = mnat + (landFill ? mgwater : mslgw) + mper + "  " + mcip + msource + mper;
                        break;
                    }
                case "NATNATINV":
                    {
                        clup = mnat + (landFill ? msource : mssl) + mper + "  " + minv + mgwater + mper;
                        break;
                    }
                case "NATINVNAT":
                    {
                        clup = mnat + msgw + mper + "  " + (landFill ? "" : minv + msoil + mper);
                        break;
                    }
                case "INVNATNAT":
                    {
                        clup = mnat + (landFill ? mgwater : mslgw) + mper + "  " + minv + msource + mper;
                        break;
                    }
                case "RRS5CIP":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "RRSCIP5":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "RRS5INV":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "RRSINV5":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "RRS5NAT":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "RRSNAT5":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "RRSCIPINV":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "RRSINVCIP":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "RRSCIPNAT":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "RRSNATCIP":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "RRSINVNAT":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "RRSNATINV":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "5RRSCIP":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                // northside
                case var @case when @case == "5RRSRRS":
                    {
                        clup = mrrs + msource + mrrs5_2 + "  " + (landFill ? "" : mrrs) + mslgw + mper;
                        break;
                    }
                case "5CIPRRS":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "5RRSINV":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "5INVRRS":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "5RRSNAT":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "5NATRRS":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "5CIPINV":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "5INVCIP":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "5CIPNAT":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "5NATCIP":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "5INVNAT":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "5NATINV":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "CIPRRS5":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "CIP5RRS":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "CIPRRSINV":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "CIPINVRRS":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "CIPRRSNAT":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "CIPNATRRS":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "CIP5INV":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "CIPINV5":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "CIP5NAT":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "CIPNAT5":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "CIPINVNAT":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "CIPNATINV":
                    {
                        clup = mcip + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "INVRRS5":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "INV5RRS":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "INVRRSCIP":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "INVCIPRRS":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "INVRRSNAT":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "INVNATRRS":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "INV5CIP":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "INVCIP5":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "INV5NAT":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "INVNAT5":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "INVCIPNAT":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mnat + mgwater + mper;
                        break;
                    }
                case "INVNATCIP":
                    {
                        clup = minv + msource + mper + "  " + (landFill ? "" : mnat + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "NATRRS5":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "NAT5RRS":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "NATRRSCIP":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "NATCIPRRS":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "NATRRSINV":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "NATINVRRS":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "NAT5CIP":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "NATCIP5":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "NAT5INV":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "NATINV5":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "NATCIPINV":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : mcip + msoil + mper) + "  " + minv + mgwater + mper;
                        break;
                    }
                case "NATINVCIP":
                    {
                        clup = mnat + msource + mper + "  " + (landFill ? "" : minv + msoil + mper) + "  " + mcip + mgwater + mper;
                        break;
                    }
                case "RRS5ABN":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mabnd1 + mgwater + mabnd2;
                        break;
                    }
                case "RRSABN5":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mabnd1 + msoil + mabnd2) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "5RRSABN":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mabnd1 + mgwater + mabnd2;
                        break;
                    }
                case "5ABNRRS":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mabnd1 + msoil + mabnd2) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "ABN5RRS":
                    {
                        clup = mabnd1 + msource + mabnd2 + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "ABNRRS5":
                    {
                        clup = mabnd1 + msource + mabnd2 + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "RRS5INA":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + minac1 + mgwater + minac2;
                        break;
                    }
                case "RRSINA5":
                    {
                        clup = mrrs + msource + mper + "  " + (landFill ? "" : minac1 + msoil + minac2) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                case "5RRSINA":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + minac1 + mgwater + minac2;
                        break;
                    }
                case "5INARRS":
                    {
                        clup = mrrs5_1 + msource + mrrs5_2 + "  " + (landFill ? "" : minac1 + msoil + minac2) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "INA5RRS":
                    {
                        clup = minac1 + msource + minac2 + "  " + (landFill ? "" : mrrs5_1 + msoil + mrrs5_2) + "  " + mrrs + mgwater + mper;
                        break;
                    }
                case "INARRS5":
                    {
                        clup = minac1 + msource + minac2 + "  " + (landFill ? "" : mrrs + msoil + mper) + "  " + mrrs5_1 + mgwater + mrrs5_2;
                        break;
                    }
                default:
                    {
                        clup = cleanupstat + "  DATA IS INCOMPLETE - PLEASE CHECK!!!!!!!!!";
                        break;
                    }
            }
            return clup;
        }

        public static string GetLanguageForGWScore(SiteSummaryReportDto facility, string exLang = "")
        {
            var gwLang = "";
            var gw_score = facility.GroundwaterScoreDetails.GWScore;
            var gw_a = facility.GroundwaterScoreDetails.A;
            var gwa = "";
            var gw_1d = facility.GroundwaterScoreDetails.ChemName;


            if (gw_score > 10)
            {

                switch (gw_a)
                {
                    case 45:
                        {
                            gwa = "This site has a known release of " + gw_1d + " in groundwater at levels exceeding the reportable quantity.  ";
                            break;
                        }
                    case 10:
                        {
                            gwa = "This site has a suspected release of " + gw_1d + " in groundwater at levels exceeding the reportable quantity.  ";
                            break;
                        }
                    case 5:
                        {
                            gwa = "This site has a release of " + gw_1d + " that exceeds a reportable quantity because it has the potential to contaminate groundwater. ";
                            break;
                        }

                    default:
                        {
                            gwa = "!!!!! INVALID VALUE FOR GW_A !!!!!";
                            break;
                        }
                }

                var gw_1e = facility.GroundwaterScoreDetails.E1;
                var gw1e = "";

                switch (gw_1e)
                {
                    case 25:
                        {
                            gw1e = "This release has resulted in known human exposure greater than or equal to the MCL for " + gw_1d + ".  ";
                            break;
                        }
                    case 20:
                        {
                            gw1e = "This release has resulted in suspected human exposure.  ";
                            break;
                        }
                    case 18:
                        {
                            gw1e = "This release has resulted in known human exposure, with no MCL having been established for " + gw_1d + ".  ";
                            break;
                        }
                    case 15:
                        {
                            gw1e = "This release has resulted in known human exposure less than the MCL for " + gw_1d + ".  ";
                            break;
                        }
                    case 12:
                        {
                            gw1e = "This release has resulted in suspected human exposure.  ";
                            break;
                        }
                    case 8:
                        {
                            gw1e = "This release has resulted in suspected human exposure.  ";
                            break;
                        }
                    case 4:
                    case 3:
                    case 2:
                        {
                            gw1e = "No human exposure via drinking water is suspected from this release.  ";
                            break;
                        }
                    case 1:
                    case 0:
                        {
                            gw1e = "";
                            break;
                        }

                    default:
                        {
                            gw1e = "!!!!! INVALID VALUE FOR GW_1E !!!!!";
                            break;
                        }
                }

                var gw_2e = facility.GroundwaterScoreDetails.E2;
                var gw2em = "";
                string gw2estrt = "The nearest drinking water well is ";
                string gw2eend = " from the area affected by the release.  ";

                if (gw_1e < 8)
                {
                    switch (gw_2e)
                    {
                        case 16:
                            {
                                gw2em = "less than 0.5 miles";
                                break;
                            }
                        case 9:
                            {
                                gw2em = "between 0.5 and 1 miles";
                                break;
                            }
                        case 4:
                            {
                                gw2em = "between 1 and 2 miles";
                                break;
                            }
                        case 1:
                            {
                                gw2em = "between 2 and 3 miles";
                                break;
                            }
                        case 0:
                            {
                                gw2em = "greater than 3 miles";
                                break;
                            }

                        default:
                            {
                                gw2em = "!!!!! INVALID VALUE FOR GW_2E !!!!!";
                                break;
                            }
                    }
                }
                gwLang = gwa + gw1e + gw2estrt + gw2em + gw2eend;
            }
            return gwLang;
        }

        public static string GetLanguageForOSScore(SiteSummaryReportDto facility)
        {
            var osLang = "";
            var osa = "";
            var osb = "";
            var os1em = "";
            var os_score = facility.OnsiteScoreDetails.OnsiteScoreValue;
            var os_a = facility.OnsiteScoreDetails.A;
            var os_b = facility.OnsiteScoreDetails.B;
            var os_1d = facility.OnsiteScoreDetails.ChemName1D;
            var os_1e = facility.OnsiteScoreDetails.E1;

            if (os_score <= 20)
            {
                osLang = "";
            }
            else
            {
                switch (os_b)
                {
                    case 25:
                        {
                            osb = "This site has a known release of " + os_1d + " in soil at levels exceeding the reportable quantity.  ";
                            break;
                        }
                    case 15:
                        {
                            osb = "This site has a suspected release of " + os_1d + " in soil at levels exceeding the reportable quantity.  ";
                            break;
                        }

                    default:
                        {
                            osb = "!!!!! INVALID VALUE FOR OS_B !!!!!";
                            break;
                        }

                }

                switch (os_a)
                {
                    case 2:
                        {
                            osa = "This site has limited access.  ";
                            break;
                        }
                    case 4:
                        {
                            osa = "This site has unlimited access.  ";
                            break;
                        }

                    default:
                        {
                            osa = "!!!!! INVALID VALUE FOR OS_A !!!!!";
                            break;
                        }

                }

                switch (os_1e)
                {
                    case 8:
                        {
                            os1em = "less than 300 feet";
                            break;
                        }
                    case 6:
                        {
                            os1em = "between 301 and 1000 feet";
                            break;
                        }
                    case 4:
                        {
                            os1em = "between 1001 and 3000 feet";
                            break;
                        }
                    case 2:
                        {
                            os1em = "between 3001 and 5280 feet";
                            break;
                        }

                    default:
                        {
                            os1em = "!!!!! INVALID VALUE FOR OS-1E !!!!!";
                            break;
                        }
                }

                osLang = osb + osa + "The nearest resident individual is " + os1em + " from the area affected by the release.  ";
            }
            return osLang;
        }

        public static string GetLanguageForExceptions(SiteSummaryReportDto facility) => facility.ScoreDetails.Comments.ToString();
    }
}
