using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FMS.Domain.Dto;
using FMS.Domain.Entities;
using FMS.Domain.Extensions;
using NUnit.Framework;

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

        [TestCase("001-0001", "C001")]
        [TestCase("103-0001", "C002")]
        [TestCase("109-9999", "C002")]
        [TestCase("111-0001", "C004,C005")]
        [TestCase("111-0002", "C005")]
        [TestCase("150-0001", "C007")]
        [TestCase("160-0001", "C007")]
        public void GetCabinetsForFile_ReturnsCorrectList(string fileLabel, string cabinets)
        {
            var result = GetCabinetList().GetCabinetsForFile(fileLabel).ConcatNonEmpty(",");
            result.Should().Be(cabinets);
        }

        [Test]
        public void GetCabinetsForFile_InvalidFileLabel_ThrowsException()
        {
            const string fileLabel = "01-23";
            Action action = () => GetCabinetList().GetCabinetsForFile(fileLabel);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"File label '{fileLabel}' is invalid. (Parameter 'fileLabel')");
        }

        [Test]
        public void GetCabinetsForFileNumber_NullFileLabel_ThrowsException()
        {
            Action action = () => GetCabinetList().GetCabinetsForFile(null);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Value cannot be null. (Parameter 'fileLabel')");
        }
    }
}