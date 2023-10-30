using Xunit;
using FMS;

namespace FMS.App.Tests.Helpers
{
    public class GeoCoordHelperTests
    {
        // Null values are filtered out by the form validation

        [Theory]
        [InlineData(0)]
        [InlineData(32)]
        public void ValidLat_ReturnsTrue_GiveValidLatitude(decimal lat)
        {
            Assert.True(GeoCoordHelper.ValidLat(lat));
        }

        [Theory]
        [InlineData(40)]
        [InlineData(20)]
        public void ValidLat_ReturnsFalse_GiveInvalidLatitude(decimal lat)
        {
            Assert.False(GeoCoordHelper.ValidLat(lat));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-82)]
        public void ValidLon_ReturnsTrue_GiveValidLongitude(decimal lng)
        {
            Assert.True(GeoCoordHelper.ValidLong(lng));
        }

        [Theory]
        [InlineData(-70)]
        [InlineData(-90)]
        [InlineData(82)]
        public void ValidLong_ReturnsFalse_GiveInvalidLongitude(decimal lng)
        {
            Assert.False(GeoCoordHelper.ValidLong(lng));
        }

        [Fact]
        public void BothZeroOrBothNonzero_ReturnsTrue_GivenTwoZeros()
        {
            Assert.True(GeoCoordHelper.BothZeroOrBothNonzero(0, 0));
        }

        [Fact]
        public void BothZeroOrBothNonzero_ReturnsTrue_GivenTwoNonZeros()
        {
            Assert.True(GeoCoordHelper.BothZeroOrBothNonzero(1, 1));
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        public void BothZeroOrBothNonzero_ReturnsFalse_GivenOneZero(decimal lat, decimal lng)
        {
            Assert.False(GeoCoordHelper.BothZeroOrBothNonzero(lat, lng));
        }
    }
}
