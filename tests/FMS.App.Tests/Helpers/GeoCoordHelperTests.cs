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
    public class GeoCoordHelperTests
    {
        // Null values are filtered out by the form validation

        [Theory]
        [InlineData(0.00)]
        [InlineData(32.456)]
        public void InvalidLatIsFalse(decimal lat)
        {
            Assert.False(GeoCoordHelper.InvalidLat(lat));
        }

        [Theory]
        [InlineData(37.23)]
        [InlineData(38.65)]
        [InlineData(27.3)]
        public void InvalidLatIsTrue(decimal lat)
        {
            Assert.True(GeoCoordHelper.InvalidLat(lat));
        }

        [Theory]
        [InlineData(0.00)]
        [InlineData(-82.876)]
        public void InvalidLongIsFalse(decimal lng)
        {
            Assert.False(GeoCoordHelper.InvalidLong(lng));
        }

        [Theory]
        [InlineData(-79.86)]
        [InlineData(-91.23)]
        [InlineData(83.765)]
        public void InvalidLongIsTrue(decimal lng)
        {
            Assert.True(GeoCoordHelper.InvalidLong(lng));
        }

        [Theory]
        [InlineData(0.00, 0.00)]
        [InlineData(32.876, -84.432)]
        [InlineData(45.876, 86.54)]
        public void BothZeroIsTrue(decimal lat, decimal lng)
        {
            Assert.True(GeoCoordHelper.BothZeros(lat, lng));
        }

        [Theory]
        [InlineData(0.00, 23.356)]
        [InlineData(23.543, 0.00)]
        public void BothZeroIsFalse(decimal lat, decimal lng)
        {
            Assert.False(GeoCoordHelper.BothZeros(lat, lng));
        }
    }
}