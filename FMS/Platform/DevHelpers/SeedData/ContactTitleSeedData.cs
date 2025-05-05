using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<ContactTitle> GetContactTitles()
        {
            return new List<ContactTitle>()
            {
                new()
                {
                    Id = new Guid("19A1359B-B4A0-4721-83FD-24BEAF7CBAD1"),
                    Active = true,
                    Name = "Mr. ",
                },
                new()
                {
                    Id = new Guid("2D38E2D5-A188-4ACF-BE6C-492A85AF1521"),
                    Active = true,
                    Name = "Mrs. ",
                },
                new()
                {
                    Id = new Guid("7A086539-66CF-4EE1-BFDC-72E24C66B49C"),
                    Active = true,
                    Name = "Miss ",
                },
                new()
                {
                    Id = new Guid("5A4251DE-4DE4-4A55-860F-CD021350EE24"),
                    Active = true,
                    Name = "Ms. ",
                },
                new()
                {
                    Id = new Guid("CF88D1F6-BCA2-4E69-83B0-C0C1C1E4866E"),
                    Active = true,
                    Name = "Dr. ",
                }
            };
        }
    }
}