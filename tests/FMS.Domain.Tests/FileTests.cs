using System;
using FluentAssertions;
using FMS.Domain.Entities;
using Xunit;

// ReSharper disable ObjectCreationAsStatement

namespace FMS.Domain.Tests
{
    public class FileTests
    {
        [Fact]
        public void NewFile_Succeeds()
        {
            var file = new File(131, 1);
            file.FileLabel.Should().Be("131-0001");
        }

        [Fact]
        public void NewFile_WithTwoDigitCounty_Succeeds()
        {
            var file = new File(99, 1);
            file.FileLabel.Should().Be("099-0001");
        }

        [Fact]
        public void File_InvalidCounty_ThrowsException()
        {
            const int countyNum = 999;
            Action action = () => new File(999, 1);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(10000)]
        public void File_InvalidSequence_ThrowsException(int sequence)
        {
            Action action = () => new File(131, sequence);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Input sequence was out of range (Parameter 'sequence')");
        }

        [Fact]
        public void CountyString_Succeeds()
        {
            var result = File.CountyString(99);
            result.Should().Be("099");
        }

        [Fact]
        public void CountyString_InvalidCounty_ThrowsException()
        {
            int countyNum = 999;
            Action action = () => File.CountyString(countyNum);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        [Fact]
        public void SequenceString_Succeeds()
        {
            var result = File.SequenceString(99);
            result.Should().Be("0099");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(10000)]
        public void SequenceString_InvalidSequence_ThrowsException(int sequence)
        {
            Action action = () => File.SequenceString(sequence);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Input sequence was out of range (Parameter 'sequence')");
        }
    }
}