using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.TestData.SeedData
{
    public static partial class SeedData
    {
        public static List<AllowedActionTaken> GetAllowedActionsTaken()
        {
            return new List<AllowedActionTaken>()
            {
                new()
                {
                    Id = new Guid("40f5e2ad-e885-4391-b1b9-f19dbdf16528"),
                    Active = true,
                    ActionTakenId = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),  //Amendment
                    EventTypeId = new Guid("A80FA804-3A37-4E5A-BC1C-3E4B30EC8D79")   //Administrative Order
                },
                new()
                {
                    Id = new Guid("9fd2909e-9505-4b7b-a54c-e4e8025dd69a"),
                    Active = true,
                    ActionTakenId = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),   //Amendment
                    EventTypeId = new Guid("AB83B36A-FCB2-4C97-918C-FD38421F9F41")   //Annual Certification
                },
                new()
                {
                    Id = new Guid("48bdc27e-626f-4ea6-90ee-1150cc8486e7"),
                    Active = true,
                    ActionTakenId = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),    //Amendment
                    EventTypeId = new Guid("1791A93F-C9AC-43F1-BCD5-F687802DFE6D"),   //Consent Order
                },
                new()
                {
                    Id = new Guid("7bdb95b3-0d40-482d-ae7b-69518838b44c"),
                    Active = true,
                    ActionTakenId = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),    //Amendment
                    EventTypeId = new Guid("C1A6B598-9502-4310-8D43-7030EF3A44FA"),   //Geologist Hydrogologic review
                },
                new()
                {
                    Id = new Guid("2653ab03-6c0b-419b-98c7-2bc514b77878"),
                    Active = true,
                    ActionTakenId = new Guid("45830c35-6d19-4d12-b22a-3a02dd126f0f"),   //Appeal
                    EventTypeId = new Guid("C1A6B598-9502-4310-8D43-7030EF3A44FA"),   //Geologist Hydrogologic review
                },
                new()
                {
                    Id = new Guid("994165da-74cb-475e-a4ba-b8749e374131"),
                    Active = true,
                    ActionTakenId = new Guid("45830c35-6d19-4d12-b22a-3a02dd126f0f"),    //Appeal
                    EventTypeId = new Guid("4CF16FE7-B240-49DE-AB17-0595DDD45F4E"),   //HWTF Request for Advance
                },
                new()
                {
                    Id = new Guid("cb3a2cf9-0336-4a3f-8b97-37999ccf84ba"),
                    Active = true,
                    ActionTakenId = new Guid("45830c35-6d19-4d12-b22a-3a02dd126f0f"),   //Appeal
                    EventTypeId = new Guid("6898B627-5E0C-48D1-9520-6A8428D9D7F3")   //Environmental Covenant
                },
                new()
                {
                    Id = new Guid("879fb9e0-7f7b-4c6b-89fd-68a219cea4bc"),
                    Active = true,
                    ActionTakenId = new Guid("6aae6fa2-17ca-4edd-9c59-87c6af3885c3"),   //Draft Received
                    EventTypeId = new Guid("458A7D80-8E8C-4476-961C-CE823281EFAB")   //Corrective Action Plan
                },
                new()
                {
                    Id = new Guid("d9bfa013-c777-43fe-bd05-c4da4f53e99e"),
                    Active = true,
                    ActionTakenId = new Guid("6aae6fa2-17ca-4edd-9c59-87c6af3885c3"),   //Draft Received
                    EventTypeId = new Guid("C1A6B598-9502-4310-8D43-7030EF3A44FA"),   //Geologist Hydrogologic review
                },
                new()
                {
                    Id = new Guid("efcb6ac9-ffab-434a-a968-656e9dace53d"),
                    Active = true,
                    ActionTakenId = new Guid("6aae6fa2-17ca-4edd-9c59-87c6af3885c3"),   //Draft Received
                    EventTypeId = new Guid("F1A8BCAD-B09E-42BB-ABFD-74E230567A65"),   //HWTF Master Project
                },
                new()
                {
                    Id = new Guid("d403f85c-e4be-40e7-bd4b-1a46c5715fbe"),
                    Active = true,
                    ActionTakenId = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),   //Issued
                    EventTypeId = new Guid("C1A6B598-9502-4310-8D43-7030EF3A44FA"),   //Geologist Hydrogologic review
                },
                new()
                {
                    Id = new Guid("16194bfe-a5b6-4ffa-8b44-bab915ce1523"),
                    Active = true,
                    ActionTakenId = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),   //Issued
                    EventTypeId = new Guid("F1A8BCAD-B09E-42BB-ABFD-74E230567A65"),   //HWTF Master Project
                },
                new()
                {
                    Id = new Guid("86ED497B-7DA7-4961-B0A4-FEE0A066A1CB"),
                    Active = true,
                    ActionTakenId = new Guid("6aae6fa2-17ca-4edd-9c59-87c6af3885c3"),   //Draft Received
                    EventTypeId = new Guid("4CF16FE7-B240-49DE-AB17-0595DDD45F4E"),   //HWTF Request for Advance
                },
                new()
                {
                    Id = new Guid("24A52FDD-3D94-437F-92B6-BDD52507E49B"),
                    Active = true,
                    ActionTakenId = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),   //Issued
                    EventTypeId = new Guid("7AF4D45F-0C17-4231-9D0B-3971051B75E6"),   //PAF
                },
                new()
                {
                    Id = new Guid("A8A2C814-8F89-4726-B88F-40BBF205501B"),
                    Active = true,
                    ActionTakenId = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),   //Issued
                    EventTypeId = new Guid("43F184E5-0CD9-4C4B-B2C1-1F093076C60F"),   //PAF Invoice
                }
            };
        }
    }
}
