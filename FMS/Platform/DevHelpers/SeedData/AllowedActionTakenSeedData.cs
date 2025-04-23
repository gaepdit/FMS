using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<AllowedActionTaken> GetAllowedActionTakens()
        {
            return new List<AllowedActionTaken>()
            {
                new()
                {
                    Id = new Guid("40f5e2ad-e885-4391-b1b9-f19dbdf16528"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid("")
                },
                new()
                {
                    Id = new Guid("9fd2909e-9505-4b7b-a54c-e4e8025dd69a"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid("")
                },
                new()
                {
                    Id = new Guid("48bdc27e-626f-4ea6-90ee-1150cc8486e7"),
                    Active = true,
                    ActionTakenId = new Guid(""), 
                    EventTypeId = new Guid(""),
                },
                new()
                {
                    Id = new Guid("7bdb95b3-0d40-482d-ae7b-69518838b44c"),
                    Active = true,
                    ActionTakenId = new Guid(""), 
                    EventTypeId = new Guid(""),
                },
                new()
                {
                    Id = new Guid("2653ab03-6c0b-419b-98c7-2bc514b77878"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid(""),
                },
                new()
                {
                    Id = new Guid("994165da-74cb-475e-a4ba-b8749e374131"),
                    Active = true,
                    ActionTakenId = new Guid(""), 
                    EventTypeId = new Guid(""),
                },
                new()
                {
                    Id = new Guid("cb3a2cf9-0336-4a3f-8b97-37999ccf84ba"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid("")
                },
                new()
                {
                    Id = new Guid("879fb9e0-7f7b-4c6b-89fd-68a219cea4bc"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid("")
                },
                new()
                {
                    Id = new Guid("d9bfa013-c777-43fe-bd05-c4da4f53e99e"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid(""),
                },
                new()
                {
                    Id = new Guid("efcb6ac9-ffab-434a-a968-656e9dace53d"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid(""),
                },
                new()
                {
                    Id = new Guid("d403f85c-e4be-40e7-bd4b-1a46c5715fbe"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid(""),
                },
                new()
                {
                    Id = new Guid("16194bfe-a5b6-4ffa-8b44-bab915ce1523"),
                    Active = true,
                    ActionTakenId = new Guid(""),
                    EventTypeId = new Guid(""),
                }
            };
        }
    }
}
