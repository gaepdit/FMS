using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Status> GetStatuses()
        {
            return new List<Status>()
            {
                new()
                {
                    Id = new Guid("C464ADD6-326E-4D72-B377-64BDF04E7B50"),
                    Active = true,
                    FacilityId = new Guid("3FF8B38C-B2A0-4A32-B703-BEAB9138B7F0"),
                    SourceStatusId = new Guid("E7ED53FA-2FC8-4662-9C83-8484F159A2B8"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("013E4ABB-F6A5-4B95-BA5A-438BEDD0BECA"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("39A33A21-400C-47EB-9114-E5C1F7BDA5EA"),
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
                    Id = new Guid("C45AEE20-46E3-484B-8425-ED0B4ED2E0A8"),
                    Active = true,
                    FacilityId = new Guid("AB44F9C7-C2EC-47BC-8886-60D72B5BD5EB"),
                    SourceStatusId = new Guid("7CFF0840-7790-4E38-A9AE-BAC62368CA11"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("013E4ABB-F6A5-4B95-BA5A-438BEDD0BECA"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("1186BFF4-4DCA-4F8F-A0D6-CDB0157FD7A1"),
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
                    Id = new Guid("A01DB517-763C-4BE8-BE0F-04998747248B"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    SourceStatusId = new Guid("F2127F74-0B6F-4577-8D51-0AC8ADA71426"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("12345678-90AB-CDEF-1234-567890ABCDEF"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("EE88D813-2199-4E6F-81EF-3F3837258B2E"),
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
                    Id = new Guid("F9FE1C35-684E-4004-8E08-0740A9C7CE6A"),
                    Active = true,
                    FacilityId = new Guid("7B20DE98-4726-4789-9AEA-2D995FF6839A"),
                    SourceStatusId = new Guid("D1969522-9E72-4AF0-9CFD-54F6E60D5720"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("12345678-90AB-CDEF-1234-567890ABCDEF"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("E708CC7C-9362-4932-A6C0-3E101E74B5D9"),
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
                    Id = new Guid("F7A7DFA2-3A96-44CC-B37E-080EC3F07DC1"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    SourceStatusId = new Guid("DA6FE988-4A8B-4173-A8BD-ECCCF1A1D115"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("FEDCBA98-7654-3210-FEDC-BA9876543210"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("E4644119-683C-4793-9462-0B25C18797B5"),
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
                    Id = new Guid("182C1BCA-A977-417B-B693-AB053468192B"),
                    Active = true,
                    FacilityId = new Guid("E697F074-9C1C-4CEF-93F0-FCD9610ECCD3"),
                    SourceStatusId = new Guid("F2127F74-0B6F-4577-8D51-0AC8ADA71426"),
                    SourceDate = new(2018, 2, 13),
                    SourceProjected = "",
                    SoilStatusId = new Guid("C0AB6B37-F700-4DAE-8801-D9DC8E03453E"),
                    SoilDate = new(2018, 2, 13),
                    SoilProjected = "",
                    GroundwaterStatusId = new Guid("E708CC7C-9362-4932-A6C0-3E101E74B5D9"),
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
