using System.Diagnostics.CodeAnalysis;

// ReSharper disable ObjectCreationAsStatement

namespace FMS.Domain.Tests
{
    public class FileTests
    {
        [Test]
        public void NewFile_Succeeds()
        {
            var file = new File(131, 1);
            file.FileLabel.Should().Be("131-0001");
        }

        [Test]
        public void NewFile_WithTwoDigitCounty_Succeeds()
        {
            var file = new File(99, 1);
            file.FileLabel.Should().Be("099-0001");
        }

        [Test]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results")]
        public void File_InvalidCounty_ThrowsException()
        {
            const int countyNum = 999;
            Action action = () => new File(999, 1);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(10000)]
        [SuppressMessage("Performance", "CA1806:Do not ignore method results")]
        public void File_InvalidSequence_ThrowsException(int sequence)
        {
            Action action = () => new File(131, sequence);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Input sequence was out of range (Parameter 'sequence')");
        }

        [Test]
        public void CountyString_Succeeds()
        {
            var result = File.CountyString(99);
            result.Should().Be("099");
        }

        [Test]
        public void CountyString_InvalidCounty_ThrowsException()
        {
            int countyNum = 999;
            Action action = () => File.CountyString(countyNum);
            action.Should().Throw<ArgumentException>()
                .WithMessage($"County ID {countyNum} does not exist. (Parameter 'countyNum')");
        }

        [Test]
        public void SequenceString_Succeeds()
        {
            var result = File.SequenceString(99);
            result.Should().Be("0099");
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(10000)]
        public void SequenceString_InvalidSequence_ThrowsException(int sequence)
        {
            Action action = () => File.SequenceString(sequence);
            action.Should().Throw<ArgumentException>()
                .WithMessage("Input sequence was out of range (Parameter 'sequence')");
        }

        [TestCase("000-0000")]
        [TestCase("999-9999")]
        public void FileLabelString_Valid_IsValid(string fileLabel)
        {
            File.IsValidFileLabelFormat(fileLabel).Should().BeTrue();
        }

        [TestCase("0000000")]
        [TestCase("00000000")]
        [TestCase("9999-999")]
        [TestCase("NOT-GOOD")]
        public void FileLabelString_Invalid_IsInValid(string fileLabel)
        {
            File.IsValidFileLabelFormat(fileLabel).Should().BeFalse();
        }
    }
}
