using System.Threading.Tasks;
using AwesomeAssertions;
using Microsoft.Playwright;
using NUnit.Framework;

namespace PlaywrightTests
{
    [TestFixture]
    public class AuthTests
    {
        [Test]
        public async Task CanSignInAndSignOut()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();

            // Open new page
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://localhost:44362");
            (await page.TitleAsync()).Should().EndWith("Login");
            (await page.IsVisibleAsync("button:has-text(\"Sign in\")")).Should().BeTrue();

            // Sign in
            await page.ClickAsync("button:has-text(\"Sign in\")");
            page.Url.Should().Be("https://localhost:44362/Facilities");
            (await page.TitleAsync()).Should().EndWith("Facility Search");

            // Check profile
            await page.ClickAsync("text=Account");
            await page.ClickAsync("text=Your profile");
            (await page.TextContentAsync("h2")).Should().Be("Test User");
            (await page.TitleAsync()).Should().EndWith("Account Profile");

            // Sign out
            await page.ClickAsync("text=Account");
            await page.ClickAsync("text=Sign out");
            page.Url.Should().StartWith("https://localhost:44362/Account/Login");
            (await page.TitleAsync()).Should().EndWith("Login");
        }

        [Test]
        public async Task AuthIsPreservedBetweenSessions()
        {
            using var playwright = await Playwright.CreateAsync();
            await using var browser = await playwright.Chromium.LaunchAsync();
            var context = await browser.NewContextAsync();

            // Sign in and save authentication state
            var page = await context.NewPageAsync();
            await page.GotoAsync("https://localhost:44362");
            await page.ClickAsync("button:has-text(\"Sign in\")");
            await context.StorageStateAsync(
                new BrowserContextStorageStateOptions { Path = "temp-auth-state.json" });
            await context.CloseAsync();

            // Open a new browser context
            var newContext = await browser.NewContextAsync(
                new BrowserNewContextOptions { StorageStatePath = "temp-auth-state.json" });

            // Root page should redirect to Facilities page
            var newPage = await newContext.NewPageAsync();
            await newPage.GotoAsync("https://localhost:44362");
            newPage.Url.Should().Be("https://localhost:44362/Facilities");
        }
    }
}