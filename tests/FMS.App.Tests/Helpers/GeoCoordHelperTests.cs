<<<<<<< HEAD
﻿using FMS.Helpers;
using FluentAssertions;
using NUnit.Framework;
=======
using NUnit.Framework;
using FMS;
>>>>>>> origin/274-build-unit-testing-for-repositories-in-the-infrastructure-project

namespace FMS.App.Tests.Helpers
{
    public class GeoCoordHelperTests
    {
        [TestCase(0)]
        [TestCase(32)]
        public void ValidLat_ReturnsTrue_GiveValidLatitude(decimal lat)
        {
            GeoCoordHelper.ValidLat(lat).Should().BeTrue();
        }

        [TestCase(40)]
        [TestCase(20)]
        public void ValidLat_ReturnsFalse_GiveInvalidLatitude(decimal lat)
        {
            GeoCoordHelper.ValidLat(lat).Should().BeFalse();
        }

        [TestCase(0)]
        [TestCase(-82)]
        public void ValidLon_ReturnsTrue_GiveValidLongitude(decimal lng)
        {
            GeoCoordHelper.ValidLong(lng).Should().BeTrue();
        }

        [TestCase(-70)]
        [TestCase(-90)]
        [TestCase(82)]
        public void ValidLong_ReturnsFalse_GiveInvalidLongitude(decimal lng)
        {
            GeoCoordHelper.ValidLong(lng).Should().BeFalse();
        }

        [Test]
        public void BothZeroOrBothNonzero_ReturnsTrue_GivenTwoZeros()
        {
            GeoCoordHelper.BothZeroOrBothNonzero(0, 0).Should().BeTrue();
        }

        [Test]
        public void BothZeroOrBothNonzero_ReturnsTrue_GivenTwoNonZeros()
        {
            GeoCoordHelper.BothZeroOrBothNonzero(1, 1).Should().BeTrue();
        }

        [TestCase(0, 1)]
        [TestCase(1, 0)]
        public void BothZeroOrBothNonzero_ReturnsFalse_GivenOneZero(decimal lat, decimal lng)
        {
            GeoCoordHelper.BothZeroOrBothNonzero(lat, lng).Should().BeFalse();
        }
    }
}
