using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [SetUp]
    public async Task Setup()
    {
            await Page.GotoAsync("https://playwright.dev");
    }

    [Test]
    public async Task HasTitle()
    {
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
    }

    [Test]
    public async Task GetStartedLink()
    {
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
        await Page.ClickAsync("text=Get Started");
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" })).ToBeVisibleAsync();
        await Page.ClickAsync("text=Installation");
    }
}
