using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Status> GetStatuses()
        {
            return new List<Status>()
            {
                new()
                {
                    Id = new Guid("E7ED53FA-2FC8-4662-9C83-8484F159A2B8"),
                    Active = true,
                    FacilityId = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    SourceStatusId = new Guid("E7ED53FA-2FC8-4662-9C83-8484F159A2B8"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("E7ED53FA-2FC8-4662-9C83-8484F159A2B8"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("E7ED53FA-2FC8-4662-9C83-8484F159A2B8"),
                    GroundwaterDate = new(2018, 2, 13),
                    GroundwaterHWTF = "",
                    OverallStatusId = new Guid("E134A363-CC33-4F76-B4F3-DC457144186F"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = "",
                    FundingSourceId = new Guid("97E9B7AF-5A60-4426-812A-8F6B5B2CB635"),
                    LandFill = false,
                    SolidWastePermitNumber = "",
                    HSPMScore = 0,
                    Comments = "",
                    Lien = false,
                    FinancialAssurance = true
                },
                new()
                {
                    Id = new Guid("7CFF0840-7790-4E38-A9AE-BAC62368CA11"),
                    Active = true,
                    FacilityId = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    SourceStatusId = new Guid("7CFF0840-7790-4E38-A9AE-BAC62368CA11"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("7CFF0840-7790-4E38-A9AE-BAC62368CA11"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("7CFF0840-7790-4E38-A9AE-BAC62368CA11"),
                    GroundwaterDate = new(2018, 2, 13),
                    GroundwaterHWTF = "",
                    OverallStatusId = new Guid("808FA441-4CC8-4DC2-84EA-C5C2825CC9AC"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = "",
                    FundingSourceId = new Guid("DEACC0E4-AB9F-4A16-8B66-FB7512731AB5"),
                    LandFill = false,
                    SolidWastePermitNumber = "",
                    HSPMScore = 0,
                    Comments = "",
                    Lien = true,
                    FinancialAssurance = false
                },
                new()
                {
                    Id = new Guid("89197E7A-A079-48BA-BAF2-F15657F750A7"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    SourceStatusId = new Guid("A1B2C3D4-E5F6-7890-ABCD-EF1234567890"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("F2127F74-0B6F-4577-8D51-0AC8ADA71426"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("F2127F74-0B6F-4577-8D51-0AC8ADA71426"),
                    GroundwaterDate = new(2018, 2, 13),
                    GroundwaterHWTF = "",
                    OverallStatusId = new Guid("113C16FD-FB0D-478D-8287-FDE112A2FFA1"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = "",
                    FundingSourceId = new Guid("65A97123-4BCA-478C-B110-E0F149DFCCD1"),
                    LandFill = false,
                    SolidWastePermitNumber = "",
                    HSPMScore = 0,
                    Comments = "",
                    Lien = false,
                    FinancialAssurance = true
                },
                new()
                {
                    Id = new Guid("4172EBAE-8E4A-47DE-8191-718C8D575CF8"),
                    Active = true,
                    FacilityId = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    SourceStatusId = new Guid("12345678-90AB-CDEF-1234-567890ABCDEF"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("D1969522-9E72-4AF0-9CFD-54F6E60D5720"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("F2127F74-0B6F-4577-8D51-0AC8ADA71426"),
                    GroundwaterDate = new(2018, 2, 13),
                    GroundwaterHWTF = "",
                    OverallStatusId = new Guid("113C16FD-FB0D-478D-8287-FDE112A2FFA1"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = "",
                    FundingSourceId = new Guid("5DD80C99-665C-475C-8799-203915D6E668"),
                    LandFill = true,
                    SolidWastePermitNumber = "SW12345",
                    HSPMScore = 0,
                    Comments = "",
                    Lien = true,
                    FinancialAssurance = true
                },
                new()
                {
                    Id = new Guid("9BD1C8AE-2E95-49A3-99E3-B2D74FEF8984"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    SourceStatusId = new Guid("12345678-90AB-CDEF-1234-567890ABCDEF"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("DA6FE988-4A8B-4173-A8BD-ECCCF1A1D115"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("D1969522-9E72-4AF0-9CFD-54F6E60D5720"),
                    GroundwaterDate = new(2018, 2, 13),
                    GroundwaterHWTF = "",
                    OverallStatusId = new Guid("E79D213C-226D-499E-B64B-D3355B34EB9F"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = "",
                    FundingSourceId = new Guid("5F857A62-65B0-475D-B31C-A1F685C5C002"),
                    LandFill = false,
                    SolidWastePermitNumber = "",
                    HSPMScore = 0,
                    Comments = "",
                    Lien = true,
                    FinancialAssurance = false
                },
                new()
                {
                    Id = new Guid("233CEB52-278A-46EB-A066-AF69D25BE64A"),
                    Active = true,
                    FacilityId = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    SourceStatusId = new Guid("FEDCBA98-7654-3210-FEDC-BA9876543210"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("DA6FE988-4A8B-4173-A8BD-ECCCF1A1D115"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("DA6FE988-4A8B-4173-A8BD-ECCCF1A1D115"),
                    GroundwaterDate = new(2018, 2, 13),
                    GroundwaterHWTF = "",
                    OverallStatusId = new Guid("3E1E3ADE-08AC-4C25-9839-2AE47BEE3896"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = "",
                    FundingSourceId = new Guid("CEDC5C85-E355-4458-A283-323D8F7D075A"),
                    LandFill = false,
                    SolidWastePermitNumber = "",
                    HSPMScore = 0,
                    Comments = "",
                    Lien = false,
                    FinancialAssurance = false
                }
            };
        }
    }
}
