using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Extensions;
using Xunit;

namespace FMS.Domain.Tests
{
    public class CabinetExtensionTests
    {
        private static readonly List<Cabinet> Cabinets = new List<Cabinet>
        {
            new Cabinet {Id = Guid.NewGuid(), Name = "C001", FirstFileLabel = "000-0000"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C002", FirstFileLabel = "103-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C003", FirstFileLabel = "110-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C004", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C005", FirstFileLabel = "111-0001"},
            new Cabinet {Id = Guid.NewGuid(), Name = "C007", FirstFileLabel = "150-0001"},
        };

        private static IEnumerable<CabinetSummaryDto> GetCabinetList()
        {
            var cabinets = Cabinets.Select(e => new CabinetSummaryDto(e)).ToList();

            // loop through all the cabinets except the last one and set last file label
            for (var i = 0; i < cabinets.Count - 1; i++)
            {
                cabinets[i].LastFileLabel = cabinets[i + 1].FirstFileLabel;
            }

            return cabinets;
        }


        // GetCabinetsForFile

        [Theory]
        [InlineData("001-0001", "C001")]
        [InlineData("103-0001", "C002")]
        [InlineData("109-9999", "C002")]
        [InlineData("111-0001", "C004,C005")]
        [InlineData("111-0002", "C005")]
        [InlineData("150-0001", "C007")]
        [InlineData("160-0001", "C007")]
        public void GetCabinetsForFile_ReturnsCorrectList(string fileLabel, string cabinets)
        {
            var result = GetCabinetList().GetCabinetsForFile(fileLabel).ConcatNonEmpty(",");
            result.Should().Be(cabinets);
        }

        [Fact]
        public void GetCabinetsForFile_InvalidFileLabel_ThrowsException()
        {
            const string fileLabel = "01-23";
            Action action = () => GetCabinetList().GetCabinetsForFile(fileLabel);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"File label '{fileLabel}' is invalid. (Parameter 'fileLabel')");
        }

        [Fact]
        public void GetCabinetsForFileNumber_NullFileLabel_ThrowsException()
        {
            Action action = () => GetCabinetList().GetCabinetsForFile(null);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'fileLabel')");
        }
    }
}