using System;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests
{
    [TestFixture]
    public class AddFacilitiesTests
    {
        [Test]
        public async Task FacilityWithUniqueCoordinatesCanBeAdded()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Go to Add Facility page and sign in
            await page.GotoAsync("https://localhost:44362/Facilities/Add");
            await page.ClickAsync("button:has-text(\"Sign in\")");
            (await page.TitleAsync()).Should().EndWith("Add Facility");

            // Fill in form
            var facilityNumber = Guid.NewGuid().ToString();
            await page.ClickAsync("input[name=\"Facility.FacilityNumber\"]");
            await page.FillAsync("input[name=\"Facility.FacilityNumber\"]", facilityNumber);
            await page.PressAsync("select[name=\"Facility.CountyId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.FacilityTypeId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.OrganizationalUnitId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.BudgetCodeId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.FacilityStatusId\"]", "ArrowDown");
            await page.FillAsync("input[name=\"Facility.Name\"]", "A");
            await page.FillAsync("input[name=\"Facility.Address\"]", "2");
            await page.FillAsync("input[name=\"Facility.City\"]", "B");
            await page.FillAsync("input[name=\"Facility.PostalCode\"]", "12345");
            await page.FillAsync("input[name=\"Facility.Latitude\"]", "30");
            await page.FillAsync("input[name=\"Facility.Longitude\"]", "-80");

            // Submit form
            await page.ClickAsync("text=Create New Facility");
            (await page.TitleAsync()).Should().EndWith(facilityNumber);
            (await page.TextContentAsync("h1")).Should().Contain(facilityNumber);

            // Clean up so test can be re-run
            await page.ClickAsync("text=Delete");
            await page.ClickAsync("button:has-text(\"Delete Facility\")");
        }

        [Test]
        public async Task FacilityWithDuplicateCoordinatesRequiresConfirmation()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Go to Add Facility page and sign in
            await page.GotoAsync("https://localhost:44362/Facilities/Add");
            await page.ClickAsync("button:has-text(\"Sign in\")");
            (await page.TitleAsync()).Should().EndWith("Add Facility");

            // Fill in form
            var facilityNumber = Guid.NewGuid().ToString();
            await page.ClickAsync("input[name=\"Facility.FacilityNumber\"]");
            await page.FillAsync("input[name=\"Facility.FacilityNumber\"]", facilityNumber);
            await page.PressAsync("select[name=\"Facility.CountyId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.FacilityTypeId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.OrganizationalUnitId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.BudgetCodeId\"]", "ArrowDown");
            await page.PressAsync("select[name=\"Facility.FacilityStatusId\"]", "ArrowDown");
            await page.FillAsync("input[name=\"Facility.Name\"]", "A");
            await page.FillAsync("input[name=\"Facility.Address\"]", "2");
            await page.FillAsync("input[name=\"Facility.City\"]", "B");
            await page.FillAsync("input[name=\"Facility.PostalCode\"]", "12345");
            await page.FillAsync("input[name=\"Facility.Latitude\"]", "33.1");
            await page.FillAsync("input[name=\"Facility.Longitude\"]", "-83.5");

            // Submit form
            await page.ClickAsync("text=Create New Facility");
            (await page.TitleAsync()).Should().EndWith("Add Facility");
            page.Url.Should().EndWith("/Facilities/Add#ConfirmFacility");
            (await page.TextContentAsync("h2")).Should().Be("Confirm New Facility");
        }
    }
}