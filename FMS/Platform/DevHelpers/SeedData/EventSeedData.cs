using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<Event> GetEvents()
        {
            return new List<Event>()
            {
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ParentId = new Guid(""),
                    EventTypeId = new Guid(""),
                    ActionTakenId = new Guid(""),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid(""),
                    EventAmount = 0,
                    EntityNameOrNumber = "",
                    Comment = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ParentId = new Guid(""),
                    EventTypeId = new Guid(""),
                    ActionTakenId = new Guid(""),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid(""),
                    EventAmount = 0,
                    EntityNameOrNumber = "",
                    Comment = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ParentId = new Guid(""),
                    EventTypeId = new Guid(""),
                    ActionTakenId = new Guid(""),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid(""),
                    EventAmount = 0,
                    EntityNameOrNumber = "",
                    Comment = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ParentId = new Guid(""),
                    EventTypeId = new Guid(""),
                    ActionTakenId = new Guid(""),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid(""),
                    EventAmount = 0,
                    EntityNameOrNumber = "",
                    Comment = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ParentId = new Guid(""),
                    EventTypeId = new Guid(""),
                    ActionTakenId = new Guid(""),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid(""),
                    EventAmount = 0,
                    EntityNameOrNumber = "",
                    Comment = ""
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    ParentId = new Guid(""),
                    EventTypeId = new Guid(""),
                    ActionTakenId = new Guid(""),
                    StartDate = new(2018, 2, 13),
                    DueDate = new(2018, 2, 13),
                    CompletionDate = new(2018, 2, 13),
                    ComplianceOfficerId = new Guid(""),
                    EventAmount = 0,
                    EntityNameOrNumber = "",
                    Comment = ""
                }
            };
        }
    }
}