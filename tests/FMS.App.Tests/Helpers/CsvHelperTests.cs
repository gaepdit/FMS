using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using FluentAssertions;
using JetBrains.Annotations;
using Xunit;

namespace FMS.App.Tests.Helpers
{
    public class CsvHelperTests
    {
        private class MyClass
        {
            public MyClass(int i, decimal m, double d, float f, string s)
            {
                MyInt = i;
                MyDecimal = m;
                MyDouble = d;
                MyFloat = f;
                MyString = s;
            }

            [UsedImplicitly]
            public int MyInt { get; }

            [UsedImplicitly]
            public decimal MyDecimal { get; }

            [UsedImplicitly]
            public double MyDouble { get; }

            [UsedImplicitly]
            public float MyFloat { get; }

            [UsedImplicitly]
            public string MyString { get; }
        }

        private static readonly List<MyClass> MyItems = new()
        {
            new MyClass(1, 2, 3, 4, "abc"),
            new MyClass(1, 2.0m, 3.0d, 4.0f, "🐛👍😜"),
            new MyClass(0, 0, 0, 0, ""),
            new MyClass(0, 0, 0, 0, null),
        };

        [UsedImplicitly]
        private sealed class MyClassMap : ClassMap<MyClass>
        {
            public MyClassMap() => AutoMap(CultureInfo.InvariantCulture);
        }

        /// U+FEFF BYTE ORDER MARK
        /// The BOM is used at the start of a text stream to indicate that the stream's encoding is Unicode.
        private const char Bom = (char) 0xFEFF;

        /// NewLine characters are explicitly defined because CSV Helper uses CRLF by default regardless
        /// of environment. Leaving it unspecified (using "Environment.NewLine" or a multi-line verbatim
        /// string literal) would cause the unit tests to fail in non-Windows environments.
        private static readonly string CsvOutput = $"{Bom}MyInt,MyDecimal,MyDouble,MyFloat,MyString\r\n" +
            "1,2,3,4,abc\r\n" +
            "1,2.0,3,4,🐛👍😜\r\n" +
            "0,0,0,0,\r\n" +
            "0,0,0,0,\r\n";

        [Fact]
        public async Task GetCsv_Succeeds()
        {
            var csvByteArray = await MyItems.GetCsvByteArrayAsync<MyClassMap>();
            var result = Encoding.UTF8.GetString(csvByteArray);
            result.Should().BeEquivalentTo(CsvOutput);
        }

        private static readonly List<MyClass> MyMultilineItems = new()
        {
            new MyClass(1, 0, 0, 0, "abc"),
            new MyClass(2, 0, 0, 0, "abc" + Environment.NewLine + "def"),
            new MyClass(3, 0, 0, 0, "abc"),
        };

        private static readonly string MultilineCsvOutput = $"{Bom}MyInt,MyDecimal,MyDouble,MyFloat,MyString\r\n" +
            "1,0,0,0,abc\r\n" +
            "2,0,0,0,\"abc" + Environment.NewLine + "def\"\r\n" +
            "3,0,0,0,abc\r\n";

        [Fact]
        public async Task GetCsv_Multiline_Succeeds()
        {
            var csvByteArray = await MyMultilineItems.GetCsvByteArrayAsync<MyClassMap>();
            var result = Encoding.UTF8.GetString(csvByteArray);
            result.Should().BeEquivalentTo(MultilineCsvOutput);
        }
    }
}