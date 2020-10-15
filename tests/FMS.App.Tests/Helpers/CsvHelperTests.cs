using System;
using System.Globalization;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using FluentAssertions;
using Xunit;
using Xunit.Extensions.AssertExtensions;

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

            public int MyInt { get; }
            public decimal MyDecimal { get; }
            public double MyDouble { get; }
            public float MyFloat { get; }
            public string MyString { get; }
        }

        private static readonly List<MyClass> MyItems = new List<MyClass>()
        {
            new MyClass(1, 2, 3, 4, "abc"),
            new MyClass(1, 2.0m, 3.0d, 4.0f, "🐛👍😜"),
            new MyClass(0, 0, 0, 0, ""),
            new MyClass(0, 0, 0, 0, null),
        };

        private sealed class MyClassMap : ClassMap<MyClass>
        {
            public MyClassMap()
            {
                AutoMap(CultureInfo.InvariantCulture);
            }
        }

        private static readonly string Zwnbsp = ((char) 0xFEFF).ToString();

        private static readonly string CsvOutput = $"{Zwnbsp}MyInt,MyDecimal,MyDouble,MyFloat,MyString\r\n" +
            "1,2,3,4,abc\r\n" +
            "1,2.0,3,4,🐛👍😜\r\n" +
            "0,0,0,0,\r\n" +
            "0,0,0,0,\r\n";

        [Fact]
        public async Task GetCsv_Succeeds()
        {
            var csvByteArray = await MyItems.GetCsvByteArrayAsync<MyClassMap>();
            var result = System.Text.Encoding.UTF8.GetString(csvByteArray);
            result.Should().BeEquivalentTo(CsvOutput);
        }
    }
}