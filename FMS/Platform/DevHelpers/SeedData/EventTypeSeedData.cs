using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<EventType> GetEventTypes()
        {
            return new List<EventType>()
            {
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "(Legacy) Annual Cert / FA",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Abandoned/Inactive Site Review",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Administrative Order",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Annual Certification",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "Compliance Status Report",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                },
                new()
                {
                    Id = new Guid(""),
                    Active = true,
                    Name = "",
                }
            };
        }
    }
}