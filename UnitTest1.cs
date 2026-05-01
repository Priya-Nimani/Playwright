using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System.Text.RegularExpressions;
using PlaywrightTests.Pages;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : BasePlaywrightTest
{
    private HomePage _homePage;

    [SetUp]
    public async Task Setup()
    {
        await BaseSetUp();
        _homePage = new HomePage(Page);
        await _homePage.NavigateToHomeAsync();
    }

    [Test]
    public async Task HasTitle()
    {
        await _homePage.VerifyHomepageTitleAsync("Playwright");
        Console.WriteLine("✅ Homepage title verification passed");
    }

    [Test]
    public async Task GetStartedLink()
    {
        await _homePage.VerifyHomepageTitleAsync("Playwright");
        await _homePage.ClickGetStartedLinkAsync();
        var installationPage = new InstallationPage(Page);
        await installationPage.VerifyInstallationPageAsync();
        Console.WriteLine("✅ Get Started link navigation passed");
    }
}
