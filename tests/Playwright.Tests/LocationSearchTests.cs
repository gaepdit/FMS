using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests
{
    [TestFixture]
    public class LocationSearchTests
    {
        [Test]
        public async Task LocationSearch_WithExistingCoordinates_ReturnsTable()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Go to Location Search page and sign in
            await page.GotoAsync("https://localhost:44362/Facilities/Map");
            await page.ClickAsync("button:has-text(\"Sign in\")");
            (await page.TitleAsync()).Should().EndWith("Location Search");

            // Fill in form
            await page.FillAsync("input[name=\"Spec.Latitude\"]", "34.1");
            await page.FillAsync("input[name=\"Spec.Longitude\"]", "-84.5");
            await page.SelectOptionAsync("select[name=\"Spec.Output\"]",
                new[] { "2" });
            await page.ClickAsync("button:has-text(\"Search\")");

            page.Url.Should().EndWith("#SearchResults");
            (await page.TextContentAsync("tbody td a")).Should().Contain("ADD1");
        }

        [Test]
        public async Task LocationSearch_WithDangerousCoordinates_ReturnsTable()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();
            var page = await context.NewPageAsync();

            // Go to Location Search page and sign in
            await page.GotoAsync("https://localhost:44362/Facilities/Map");
            await page.ClickAsync("button:has-text(\"Sign in\")");
            (await page.TitleAsync()).Should().EndWith("Location Search");

            // Fill in form
            await page.FillAsync("input[name=\"Spec.Latitude\"]", "33.1");
            await page.FillAsync("input[name=\"Spec.Longitude\"]", "-83.5");
            await page.SelectOptionAsync("select[name=\"Spec.Output\"]",
                new[] { "2" });
            await page.ClickAsync("button:has-text(\"Search\")");

            page.Url.Should().EndWith("#SearchResults");
            (await page.TextContentAsync("tbody td a")).Should().Contain("FMS-183");
        }
    }
}