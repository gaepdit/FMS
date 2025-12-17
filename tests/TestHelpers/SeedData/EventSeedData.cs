using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<Event> GetEvents()
        {
            return new List<Event>()
            {
                new()
                {
                    Id = new Guid("1105FB87-229E-417C-B0F5-3F1E70F1C66D"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),   //WOODSTOCK CROSSING
                    ParentId = Guid.Empty,   //Self
                    EventTypeId = new Guid("F1A8BCAD-B09E-42BB-ABFD-74E230567A65"),   // HWTF Master Project
                    ActionTakenId = new Guid("0d7ee6cd-f975-40cf-94ff-406fe71075ff"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2019, 10, 18),
                    CompletionDate = null,
                    ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"),
                    EventAmount = 0,
                    EventContractorId = new Guid("0DBEDF10-14FA-4FE9-A241-40FD5374ACFC"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("E50B57D5-1929-4167-8ABD-22B82D7E8294"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),  //WOODSTOCK CROSSING
                    ParentId = Guid.Empty,   //Self
                    EventTypeId = new Guid("F1A8BCAD-B09E-42BB-ABFD-74E230567A65"),   //HWTF Master Project
                    ActionTakenId = new Guid("634f379a-90b1-4762-9268-c0c278040723"),
                    StartDate = new(2016, 5, 13),
                    DueDate = new(2019, 4, 18),
                    CompletionDate = new(2021, 6, 13),
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),
                    EventAmount = 0,
                    EventContractorId = new Guid("0DBEDF10-14FA-4FE9-A241-40FD5374ACFC"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("271BA527-2624-49EB-9751-6E4E646A684E"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"), //WOODSTOCK CROSSING
                    ParentId = new Guid("1105FB87-229E-417C-B0F5-3F1E70F1C66D"),   //HWTF Master Project
                    EventTypeId = new Guid("7AF4D45F-0C17-4231-9D0B-3971051B75E6"),   //PAF
                    ActionTakenId = new Guid("0d7ee6cd-f975-40cf-94ff-406fe71075ff"),
                    StartDate = new(2017, 1, 23),
                    DueDate = new(2018, 12, 14),
                    CompletionDate = new(2020, 7, 13),
                    ComplianceOfficerId = new Guid("FCE1195E-BF17-4513-B617-029EE8766A6E"),
                    EventAmount = 0,
                    EventContractorId = new Guid("9E4EAF2E-251C-48EE-8A1E-5F34BE80CD27"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("261F8502-8382-4885-B53F-426A49655B17"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),  //WOODSTOCK CROSSING
                    ParentId = new Guid("E50B57D5-1929-4167-8ABD-22B82D7E8294"),  //HWTF Master Project
                    EventTypeId = new Guid("7AF4D45F-0C17-4231-9D0B-3971051B75E6"),   //PAF
                    ActionTakenId = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2021, 12, 13),
                    CompletionDate = null,
                    ComplianceOfficerId = new Guid("B87CADC7-AD43-40CD-A1B6-C906883E386B"),
                    EventAmount = 0,
                    EventContractorId = new Guid("CF994588-4B71-4188-9A66-369D437E91B8"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("027D8105-4F7E-42BB-8F12-B81AC790279B"),
                    Active = true,
                    FacilityId = new Guid("3A7457EC-E4A4-47D2-B47C-35078C3F5BF7"),  //WOODSTOCK CROSSING
                    ParentId = Guid.Empty,   //HWTF Master Project
                    EventTypeId = new Guid("867CAD56-D354-493A-A3CB-985101A5ACB7"),   //Finacial Assurance
                    ActionTakenId = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2020, 8, 20),
                    CompletionDate = new(2022, 3, 18),
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    EventAmount = 0,
                    EventContractorId = new Guid("3E2E125D-7CA7-45CA-8070-CDB6F3623E18"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("55AA3F67-1E02-4331-9850-4BD1964058D2"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParentId = Guid.Empty,   //Self
                    EventTypeId = new Guid("867CAD56-D354-493A-A3CB-985101A5ACB7"),   //Financial Assurance
                    ActionTakenId = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    EventAmount = 0,
                    EventContractorId = new Guid("0DBEDF10-14FA-4FE9-A241-40FD5374ACFC"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("A4D1E5C2-0F7B-4F8A-9C3D-6E5B0A2E1F7A"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParentId = Guid.Empty,
                    EventTypeId = new Guid("766262E9-38BD-4819-B9FB-546FA193EDEF"),
                    ActionTakenId = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid("C505460A-1AFF-4A9C-9637-3FF5CC09878D"),
                    EventAmount = 0,
                    EventContractorId = new Guid("CF994588-4B71-4188-9A66-369D437E91B8"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("1826FBAA-6ABD-4E36-B85A-FD0B5D1243EB"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParentId = Guid.Empty,
                    EventTypeId = new Guid("766262E9-38BD-4819-B9FB-546FA193EDEF"),
                    ActionTakenId = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid("C505460A-1AFF-4A9C-9637-3FF5CC09878D"),
                    EventAmount = 0,
                    EventContractorId = new Guid("9E4EAF2E-251C-48EE-8A1E-5F34BE80CD27"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("D11DE329-98B7-492A-B56B-93822A109F16"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParentId = Guid.Empty,
                    EventTypeId = new Guid("6898B627-5E0C-48D1-9520-6A8428D9D7F3"),
                    ActionTakenId = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid("468F746A-270F-4584-8B04-71CD5271A40F"),
                    EventAmount = 0,
                    EventContractorId = new Guid("3E2E125D-7CA7-45CA-8070-CDB6F3623E18"),
                    Comment = ""
                },
                new()
                {
                    Id = new Guid("79B3C0FD-A06A-4766-A973-F60A86D8AE2C"),
                    Active = true,
                    FacilityId = new Guid("50AEC751-D2FA-42D1-BE02-3EDF721787CA"),
                    ParentId = Guid.Empty,
                    EventTypeId = new Guid("C1A6B598-9502-4310-8D43-7030EF3A44FA"),
                    ActionTakenId = new Guid("6aae6fa2-17ca-4edd-9c59-87c6af3885c3"),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid("C505460A-1AFF-4A9C-9637-3FF5CC09878D"),
                    EventAmount = 0,
                    EventContractorId = new Guid("3E2E125D-7CA7-45CA-8070-CDB6F3623E18"),
                    Comment = ""
                }
            };
        }
    }
}