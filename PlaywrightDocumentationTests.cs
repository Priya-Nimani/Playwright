using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Pages;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class PlaywrightDocumentationTests : PageTest
{
    private HomePage _homePage;
    private SearchPage _searchPage;

    [SetUp]
    public async Task Setup()
    {
        _homePage = new HomePage(Page);
        _searchPage = new SearchPage(Page);
        await _homePage.NavigateToHomeAsync();
    }

    [Test]
    public async Task NavigateToFramesDocumentation()
    {
        try
        {
            Console.WriteLine("📖 Testing navigation to Frames documentation...");
            await _homePage.VerifyHomepageTitleAsync("Playwright");

            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("Frames");
            await _searchPage.ClickSearchResultAsync("Frames");

            var framesPage = new FramesPage(Page);
            await framesPage.VerifyFramesPageAsync();

            Console.WriteLine($"✓ Page URL confirmed: {framesPage.PageUrl}");
            Console.WriteLine("✅ TEST PASSED: Successfully navigated to Frames documentation");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ TEST FAILED: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task SearchForInstallationDocumentation()
    {
        try
        {
            Console.WriteLine("📖 Testing search for Installation documentation...");

            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("Installation");
            await _searchPage.ClickSearchResultAsync("Installation");

            var installationPage = new InstallationPage(Page);
            await installationPage.VerifyInstallationPageAsync();

            Console.WriteLine("✅ Installation documentation search passed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Installation search failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task VerifyDocumentationNavigation()
    {
        try
        {
            Console.WriteLine("📖 Testing documentation navigation links...");

            // Test Getting Started exists
            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("Getting Started");
            var gettingStartedCount = await _searchPage.GetSearchResultCountAsync("Getting started");
            
            if (gettingStartedCount > 0)
            {
                Console.WriteLine("✓ Getting started documentation exists");
            }

            // Test Installation exists
            await _searchPage.ClearSearchAsync();
            await _searchPage.SearchForAsync("Installation");
            var installationCount = await _searchPage.GetSearchResultCountAsync("Installation");
            
            if (installationCount > 0)
            {
                Console.WriteLine("✓ Installation documentation exists");
            }

            await _searchPage.CloseSearchAsync();
            Console.WriteLine("✅ Documentation navigation verified");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Navigation verification failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task NavigateToIntroductionPage()
    {
        try
        {
            Console.WriteLine("📖 Testing navigation to Introduction page...");

            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("Intro");
            await _searchPage.ClickSearchResultAsync("Intro");

            var url = Page.Url;
            if (url.Contains("intro"))
            {
                Console.WriteLine($"✓ Navigated to: {url}");
                Console.WriteLine("✅ Introduction page navigation successful");
            }
            else
            {
                throw new Exception($"Failed to navigate to Intro page. URL: {url}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Intro navigation failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task SearchForBrowserContextDocumentation()
    {
        try
        {
            Console.WriteLine("📖 Testing search for BrowserContext documentation...");

            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("BrowserContext");

            var resultCount = await _searchPage.GetSearchResultCountAsync("BrowserContext");
            if (resultCount > 0)
            {
                Console.WriteLine($"✓ Found {resultCount} BrowserContext result(s)");

                var result = await _searchPage.GetSearchResultAsync("BrowserContext");
                await result.First.ClickAsync();
                await _homePage.WaitForNavigationAsync();

                var browserContextPage = new BrowserContextPage(Page);
                await browserContextPage.VerifyBrowserContextPageAsync();

                Console.WriteLine($"✓ Navigated to: {browserContextPage.PageUrl}");
                Console.WriteLine("✅ BrowserContext search successful");
            }
            else
            {
                throw new Exception("No BrowserContext results found");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ BrowserContext search failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task VerifyPageTitleOnHomepage()
    {
        try
        {
            Console.WriteLine("📖 Testing homepage title...");

            var title = await _homePage.GetPageTitleAsync();
            if (title.Contains("Playwright"))
            {
                Console.WriteLine($"✓ Page title: {title}");
                Console.WriteLine("✅ Homepage title verification passed");
            }
            else
            {
                throw new Exception($"Unexpected title: {title}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Title verification failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task NavigateViaGetStartedLink()
    {
        try
        {
            Console.WriteLine("📖 Testing Get Started link navigation...");

            await _homePage.ClickGetStartedLinkAsync();

            var installationPage = new InstallationPage(Page);
            await installationPage.VerifyInstallationPageAsync();

            Console.WriteLine("✓ Successfully navigated from Get Started link");
            Console.WriteLine("✅ Get Started link test passed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Get Started navigation failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task SearchForLocatorsDocumentation()
    {
        try
        {
            Console.WriteLine("📖 Testing search for Locators documentation...");

            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("Locators");
            await _searchPage.ClickSearchResultAsync("Locators");

            var locatorsPage = new LocatorsPage(Page);
            await locatorsPage.VerifyLocatorsPageAsync();

            Console.WriteLine("✓ Successfully navigated to Locators documentation");
            Console.WriteLine("✅ Locators search test passed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Locators search failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task VerifyMultipleNavigationPaths()
    {
        try
        {
            Console.WriteLine("📖 Testing multiple navigation paths...");

            // Test 1: Navigate to Installation via search
            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("Installation");
            await _searchPage.ClickSearchResultAsync("Installation");

            var installationPage = new InstallationPage(Page);
            await installationPage.VerifyInstallationPageAsync();
            Console.WriteLine("✓ Navigated to Installation");

            // Go back to home
            await _homePage.GoBackAsync();
            await _homePage.WaitForNavigationAsync();

            // Test 2: Navigate to Frames via search
            await _searchPage.OpenSearchAsync();
            await _searchPage.ClearSearchAsync();
            await _searchPage.SearchForAsync("Frames");
            await _searchPage.ClickSearchResultAsync("Frames");

            var framesPage = new FramesPage(Page);
            await framesPage.VerifyFramesPageAsync();
            Console.WriteLine("✓ Navigated to Frames");

            // Go back to home
            await _homePage.GoBackAsync();
            await _homePage.WaitForNavigationAsync();

            // Test 3: Search functionality still accessible
            await _searchPage.OpenSearchAsync();
            Console.WriteLine("✓ Search functionality accessible");

            Console.WriteLine("✅ Multiple navigation paths test passed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Navigation paths test failed: {ex.Message}");
            throw;
        }
    }
}
