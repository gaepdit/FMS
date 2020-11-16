using System;
using System.Collections.Generic;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Extensions;
using Xunit;

namespace FMS.Domain.Tests
{
    public class CabinetExtensionTests
    {
        private static readonly IReadOnlyList<CabinetSummaryDto> CabinetList = new List<CabinetSummaryDto>
        {
            new CabinetSummaryDto(new Cabinet {Id = Guid.NewGuid(), Name = "C001", FirstFileLabel = "000-0000"}),
            new CabinetSummaryDto(new Cabinet {Id = Guid.NewGuid(), Name = "C002", FirstFileLabel = "103-0001"}),
            new CabinetSummaryDto(new Cabinet {Id = Guid.NewGuid(), Name = "C003", FirstFileLabel = "110-0001"}),
            new CabinetSummaryDto(new Cabinet {Id = Guid.NewGuid(), Name = "C004", FirstFileLabel = "111-0001"}),
            new CabinetSummaryDto(new Cabinet {Id = Guid.NewGuid(), Name = "C005", FirstFileLabel = "111-0001"}),
            new CabinetSummaryDto(new Cabinet
            {
                Id = Guid.NewGuid(), Name = "C006", FirstFileLabel = "150-0001", Active = false
            }),
            new CabinetSummaryDto(new Cabinet {Id = Guid.NewGuid(), Name = "C007", FirstFileLabel = "150-0001"}),
        };

        // GetCabinetsForFile

        [Theory]
        [InlineData("001-0001", "C001")]
        [InlineData("103-0001", "C002")]
        [InlineData("109-9999", "C003")]
        [InlineData("111-0001", "C004,C005")]
        [InlineData("111-0002", "C005")]
        [InlineData("150-0001", "C007")]
        [InlineData("160-0001", "C007")]
        public void GetCabinetsForFile_ReturnsCorrectList(string fileLabel, string cabinets)
        {
            var result = CabinetList.GetCabinetsForFile(fileLabel).ConcatNonEmpty(",");
            result.Should().Be(cabinets);
        }

        [Fact]
        public void GetCabinetsForFile_InvalidFileLabel_ThrowsException()
        {
            const string fileLabel = "01-23";
            Action action = () => CabinetList.GetCabinetsForFile(fileLabel);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"File label '{fileLabel}' is invalid.");
        }

        [Fact]
        public void GetCabinetsForFileNumber_NullFileLabel_ThrowsException()
        {
            Action action = () => CabinetList.GetCabinetsForFile(null);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'fileLabel')");
        }
    }
}