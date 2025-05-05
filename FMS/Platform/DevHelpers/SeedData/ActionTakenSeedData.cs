using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<ActionTaken> GetActionTakens()
        {
            return new List<ActionTaken>()
            {
                new()
                {
                    Id = new Guid("a7a3f7b3-8cd3-421c-a041-1f6a29cba42c"),
                    Active = true,
                    Name = "Amendment",
                },
                new()
                {
                    Id = new Guid("45830c35-6d19-4d12-b22a-3a02dd126f0f"),
                    Active = true,
                    Name = "Appeal",
                },
                new()
                {
                    Id = new Guid("d3562fc2-d606-4bc2-ba7e-5d096eb33cf7"),
                    Active = true,
                    Name = "Call In Letter",
                },
                new()
                {
                    Id = new Guid("90522ff8-de86-44fd-9a8b-386a54dfd7da"),
                    Active = true,
                    Name = "Cancel/Terminate",
                },
                new()
                {
                    Id = new Guid("c53c8451-bec9-45e1-8db1-0b4f04cd4395"),
                    Active = true,
                    Name = "Concur Engineering and Institutional  Type 5 RRS",
                },
                new()
                {
                    Id = new Guid("661846da-db76-48f0-abfa-4c4a42db474e"),
                    Active = true,
                    Name = "Concur Non-Residential Type 3 and 4 RRS",
                },
                new()
                {
                    Id = new Guid("526ddb34-2f7d-4369-8ed4-ced51bc23fb2"),
                    Active = true,
                    Name = "Concur Residential Type 1 and 2 RRS",
                },
                new()
                {
                    Id = new Guid("6aae6fa2-17ca-4edd-9c59-87c6af3885c3"),
                    Active = true,
                    Name = "Draft Received",
                },
                new()
                {
                    Id = new Guid("634f379a-90b1-4762-9268-c0c278040723"),
                    Active = true,
                    Name = "Executed",
                },
                new()
                {
                    Id = new Guid("d727e1b8-da93-41e9-acdc-696635209575"),
                    Active = true,
                    Name = "Hearing",
                },
                new()
                {
                    Id = new Guid("51e5d358-f991-4574-a21b-79ccfa31ac9d"),
                    Active = true,
                    Name = "Issued",
                },
                new()
                {
                    Id = new Guid("4e7c1163-59eb-43cc-8cb6-62f004a0a7ce"),
                    Active = true,
                    Name = "Notice of Deficiency",
                },
                new()
                {
                    Id = new Guid("591780b5-0614-4066-88e0-f9a92238cba6"),
                    Active = true,
                    Name = "Pending",
                },
                new()
                {
                    Id = new Guid("0d7ee6cd-f975-40cf-94ff-406fe71075ff"),
                    Active = true,
                    Name = "Received",
                },
                new()
                {
                    Id = new Guid("a2719b60-650b-4dac-83fb-1399cf5b4dfe"),
                    Active = true,
                    Name = "Referral",
                },
            };
        }
    }
}
 