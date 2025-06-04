using System;
using System.Collections.Generic;
using FMS.Domain.Entities;

// ReSharper disable StringLiteralTypo

namespace FMS.Platform.Extensions.DevHelpers.SeedData
{
    public static partial class SeedData
    {
        private static IEnumerable<OverallStatus> GetOverallStatuses()
        {
            return new List<OverallStatus>
            {
                new OverallStatus()
                {
                    Id = new Guid("E134A363-CC33-4F76-B4F3-DC457144186F"),
                    Active = true,
                    Name = "Status Comment 1",
                    Description = "Initial overall status for testing purposes."
                },
                new OverallStatus()
                {
                    Id = new Guid("808FA441-4CC8-4DC2-84EA-C5C2825CC9AC"),
                    Active = true,
                    Name = "Status Comment 2",
                    Description = "Secondary overall status for testing purposes."
                },
                new OverallStatus()
                {
                    Id = new Guid("113C16FD-FB0D-478D-8287-FDE112A2FFA1"),
                    Active = true,
                    Name = "Status Comment 3",
                    Description = "Tertiary overall status for testing purposes."
                },
                new OverallStatus()
                {
                    Id = new Guid("E79D213C-226D-499E-B64B-D3355B34EB9F"),
                    Active = true,
                    Name = "Status Comment 4",
                    Description = "Quaternary overall status for testing purposes."
                },
                new OverallStatus()
                {
                    Id = new Guid("3E1E3ADE-08AC-4C25-9839-2AE47BEE3896"),
                    Active = true,
                    Name = "Status Comment 5",
                    Description = "Quinary overall status for testing purposes."
                },
                new OverallStatus()
                {
                    Id = new Guid("9E9FF1B4-4068-4074-B73A-842868AE9B87"),
                    Active = true,
                    Name = "Status Comment 6",
                    Description = "Senary overall status for testing purposes."
                },
                new OverallStatus()
                {
                    Id = new Guid("C8409B65-699D-4D58-B9B6-46B4BC651808"),
                    Active = true,
                    Name = "Status Comment 7",
                    Description = "Septenary overall status for testing purposes."
                },
            };
        }
    }
}
