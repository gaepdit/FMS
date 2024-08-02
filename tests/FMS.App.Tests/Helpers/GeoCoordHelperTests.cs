using NUnit.Framework;
using FMS;

namespace FMS.App.Tests.Helpers
{
    public class GeoCoordHelperTests
    {
        // Null values are filtered out by the form validation

        [TestCase(0)]
        [TestCase(32)]
        public void ValidLat_ReturnsTrue_GiveValidLatitude(decimal lat)
        {
            Assert.True(GeoCoordHelper.ValidLat(lat));
        }

        [TestCase(40)]
        [TestCase(20)]
        public void ValidLat_ReturnsFalse_GiveInvalidLatitude(decimal lat)
        {
            Assert.False(GeoCoordHelper.ValidLat(lat));
        }

        [TestCase(0)]
        [TestCase(-82)]
        public void ValidLon_ReturnsTrue_GiveValidLongitude(decimal lng)
        {
            Assert.True(GeoCoordHelper.ValidLong(lng));
        }

        [TestCase(-70)]
        [TestCase(-90)]
        [TestCase(82)]
        public void ValidLong_ReturnsFalse_GiveInvalidLongitude(decimal lng)
        {
            Assert.False(GeoCoordHelper.ValidLong(lng));
        }

        [Test]
        public void BothZeroOrBothNonzero_ReturnsTrue_GivenTwoZeros()
        {
            Assert.True(GeoCoordHelper.BothZeroOrBothNonzero(0, 0));
        }

        [Test]
        public void BothZeroOrBothNonzero_ReturnsTrue_GivenTwoNonZeros()
        {
            Assert.True(GeoCoordHelper.BothZeroOrBothNonzero(1, 1));
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void BothZeroOrBothNonzero_ReturnsFalse_GivenOneZero(decimal lat, decimal lng)
        {
            Assert.False(GeoCoordHelper.BothZeroOrBothNonzero(lat, lng));
        }
    }
}
