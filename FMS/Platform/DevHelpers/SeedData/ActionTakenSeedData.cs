using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
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
                    Id = new Guid(""),
                    Active = true,
                    Name = "Amendment",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Appeal",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Call In Letter",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Cancel/Terminate",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Concur Engineering and Institutional  Type 5 RRS",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Concur Non-Residential Type 3 and 4 RRS",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Concur Residential Type 1 and 2 RRS",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Draft Received",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Executed",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Hearing",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Issued",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Notice of Deficiency",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Pending",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Received",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Referral",
                },
            };
        }
    }
}
 