using FMS.Domain.Entities;
using System;
using System.Collections.Generic;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Domain.Entities.Status> GetStatuses()
        {
            return new List<Domain.Entities.Status>()
            {
                new()
                {
                    Id = new Guid("A01DB517-763C-4BE8-BE0F-04998747248B"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    SourceStatusId = new Guid("F2127F74-0B6F-4577-8D51-0AC8ADA71426"),
                    SourceDate = new(2018, 2, 13),
                    //SourceProjected = 20000,
                    SoilStatusId = new Guid("12345678-90AB-CDEF-1234-567890ABCDEF"),
                    SoilDate = new(2018, 2, 13),
                    //SoilProjected = 50000,
                    GroundwaterStatusId = new Guid("EE88D813-2199-4E6F-81EF-3F3837258B2E"),
                    GroundwaterDate = new(2018, 2, 13),
                    //GroundwaterHWTF = 12000,
                    OverallStatusId = new Guid("113C16FD-FB0D-478D-8287-FDE112A2FFA1"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = false,
                    FundingSourceId = new Guid("65A97123-4BCA-478C-B110-E0F149DFCCD1"),
                    LandFill = false,
                    SolidWastePermitNumber = "",
                    GAPSScore = 4,
                    Comments = "comments for this status",
                    Lien = false,
                    FinancialAssurance = true,
                    GAPSModelDate = new(2018, 2, 13),
                    GAPSNoOfUnknowns = 14,
                    GAPSAssessmentId = new Guid("DC9738A8-7C88-421A-8E1A-36D0B518C582"),
                    CostEstimate = 62000,
                    CostEstimateDate = new(2018, 2, 13),
                    AbandonedInactiveId = new Guid("9c9582b2-c065-4640-933c-eeff05626b04"),
                    ReportComments = "Report comments for this status"
                },
                new()
                {
                    Id = new Guid("F7A7DFA2-3A96-44CC-B37E-080EC3F07DC1"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),
                    SourceStatusId = new Guid("DA6FE988-4A8B-4173-A8BD-ECCCF1A1D115"),
                    SourceDate = new(2018, 2, 13),
                    //SourceProjected = 50000,
                    SoilStatusId = new Guid("FEDCBA98-7654-3210-FEDC-BA9876543210"),
                    SoilDate = new(2018, 2, 13),
                    //SoilProjected = null,
                    GroundwaterStatusId = new Guid("E4644119-683C-4793-9462-0B25C18797B5"),
                    GroundwaterDate = new(2018, 2, 13),
                    //GroundwaterHWTF = 27000,
                    OverallStatusId = new Guid("E79D213C-226D-499E-B64B-D3355B34EB9F"),
                    OverallDate = new(2018, 2, 13),
                    ISWQS = true,
                    FundingSourceId = new Guid("5F857A62-65B0-475D-B31C-A1F685C5C002"),
                    LandFill = true,
                    SolidWastePermitNumber = "SW046758",
                    GAPSScore = 0,
                    Comments = "More silly comments" + Environment.NewLine +
                    "More stuff and more stuff" + Environment.NewLine +
                    "lots of comments" + Environment.NewLine +
                    "and more lines" + Environment.NewLine +
                    "and more and more",
                    Lien = true,
                    FinancialAssurance = false,
                    GAPSModelDate = new(2018, 2, 13),
                    GAPSNoOfUnknowns = 14,
                    GAPSAssessmentId = new Guid("E1F11C25-43EE-4C87-8461-1308E935451F"),
                    CostEstimate = 62000,
                    CostEstimateDate = new(2018, 2, 13),
                    AbandonedInactiveId = new Guid("74f1dbdd-cd4a-463d-b426-3ccc98fc0ab6"),
                    ReportComments = "Another report comment for this status"
                }
            };
        }
    }
}
