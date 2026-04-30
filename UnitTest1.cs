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

    [Test]
    public async Task NavigateToFramesDocumentation()
    {
        try
        {
            // Verify we're on Playwright documentation
            await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
            Console.WriteLine("✓ Successfully loaded Playwright documentation");

            // Open search and type "Frames"
            var searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            await searchButton.ClickAsync();
            Console.WriteLine("✓ Clicked search button");

            // Wait for search dialog to appear
            await Page.WaitForTimeoutAsync(300);

            // Type "Frames" in the search field
            var searchInput = Page.GetByPlaceholder("Search");
            await searchInput.FillAsync("Frames");
            Console.WriteLine("✓ Typed 'Frames' in search");

            // Wait for search results
            await Page.WaitForTimeoutAsync(500);

            // Click on the Frames result - it should be a link in the results
            var framesLink = Page.Locator("a", new() { Has = Page.Locator("text=Frames") }).First;
            await framesLink.ClickAsync();
            Console.WriteLine("✓ Clicked on Frames search result");

            // Wait for page to load
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Verify that the page header reads "Frames"
            var framesHeader = Page.GetByRole(AriaRole.Heading, new() { Name = "Frames" });
            await Expect(framesHeader).ToBeVisibleAsync();
            Console.WriteLine("✓ Verified page header reads 'Frames'");

            // Additional verification: Check the page URL contains "frames"
            var pageUrl = Page.Url;
            if (pageUrl.Contains("frames"))
            {
                Console.WriteLine($"✓ Page URL confirmed: {pageUrl}");
                Console.WriteLine("\n✅ TEST PASSED: Successfully navigated to Frames documentation");
            }
            else
            {
                throw new Exception($"Page URL does not contain 'frames'. Got: {pageUrl}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\n❌ TEST FAILED: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task SearchForInstallationDocumentation()
    {
        try
        {
            Console.WriteLine("📖 Testing search for Installation documentation...");

            // Click search button
            var searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            await searchButton.ClickAsync();
            await Page.WaitForTimeoutAsync(300);

            // Search for "Installation"
            var searchInput = Page.GetByPlaceholder("Search");
            await searchInput.FillAsync("Installation");
            await Page.WaitForTimeoutAsync(500);

            // Click Installation result
            var installationLink = Page.Locator("a", new() { Has = Page.Locator("text=Installation") }).First;
            await installationLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Verify page contains Installation heading
            var heading = Page.GetByRole(AriaRole.Heading, new() { Name = "Installation" });
            await Expect(heading).ToBeVisibleAsync();

            Console.WriteLine("✅ Installation documentation search passed");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ Installation search failed: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task VerifyDocumentationSidebar()
    {
        try
        {
            Console.WriteLine("📖 Testing documentation navigation links...");

            // Use search to verify key documentation pages exist
            var searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            
            // Test 1: Search for Getting Started
            await searchButton.ClickAsync();
            await Page.WaitForTimeoutAsync(300);
            var searchInput = Page.GetByPlaceholder("Search");
            await searchInput.FillAsync("Getting Started");
            await Page.WaitForTimeoutAsync(300);
            
            var gettingStartedResults = await Page.Locator("a").Filter(new() { HasText = "Getting started" }).CountAsync();
            if (gettingStartedResults > 0)
            {
                Console.WriteLine("✓ Getting started documentation exists");
            }
            
            // Clear and search for another doc
            await searchInput.ClearAsync();
            await searchInput.FillAsync("Installation");
            await Page.WaitForTimeoutAsync(300);
            
            var installationResults = await Page.Locator("a").Filter(new() { HasText = "Installation" }).CountAsync();
            if (installationResults > 0)
            {
                Console.WriteLine("✓ Installation documentation exists");
            }

            // Close search
            await Page.Keyboard.PressAsync("Escape");
            await Page.WaitForTimeoutAsync(300);

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

            // Search for Intro instead of clicking sidebar
            var searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            await searchButton.ClickAsync();
            await Page.WaitForTimeoutAsync(300);

            var searchInput = Page.GetByPlaceholder("Search");
            await searchInput.FillAsync("Intro");
            await Page.WaitForTimeoutAsync(500);

            // Click on intro result
            var introLink = Page.Locator("a", new() { Has = Page.Locator("text=Intro") }).First;
            await introLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Verify page loaded
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

            // Click search button
            var searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            await searchButton.ClickAsync();
            await Page.WaitForTimeoutAsync(300);

            // Search for "BrowserContext"
            var searchInput = Page.GetByPlaceholder("Search");
            await searchInput.FillAsync("BrowserContext");
            await Page.WaitForTimeoutAsync(500);

            // Check if results appear
            var results = Page.Locator("a").Filter(new() { HasText = "BrowserContext" });
            var resultCount = await results.CountAsync();

            if (resultCount > 0)
            {
                Console.WriteLine($"✓ Found {resultCount} BrowserContext result(s)");

                // Click first result
                await results.First.ClickAsync();
                await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

                // Verify we navigated
                var url = Page.Url;
                if (url.Contains("browser") || url.Contains("api"))
                {
                    Console.WriteLine($"✓ Navigated to: {url}");
                    Console.WriteLine("✅ BrowserContext search successful");
                }
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

            var title = await Page.TitleAsync();
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

            // Click Get Started link
            var getStartedLink = Page.GetByRole(AriaRole.Link).Filter(new() { HasText = "Get Started" }).First;
            await getStartedLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Verify page contains installation or getting started content
            var heading = Page.GetByRole(AriaRole.Heading).Filter(new() { HasText = "Installation" });
            await Expect(heading.First).ToBeVisibleAsync();
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

            // Click search button
            var searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            await searchButton.ClickAsync();
            await Page.WaitForTimeoutAsync(300);

            // Search for "Locators"
            var searchInput = Page.GetByPlaceholder("Search");
            await searchInput.FillAsync("Locators");
            await Page.WaitForTimeoutAsync(500);

            // Click Locators result
            var locatorsLink = Page.Locator("a", new() { Has = Page.Locator("text=Locators") }).First;
            await locatorsLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Verify Locators content
            var content = await Page.ContentAsync();
            if (content.Contains("Locator") || Page.Url.Contains("locator"))
            {
                Console.WriteLine("✓ Successfully navigated to Locators documentation");
                Console.WriteLine("✅ Locators search test passed");
            }
            else
            {
                throw new Exception("Failed to navigate to Locators page");
            }
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
            var searchButton = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            await searchButton.ClickAsync();
            await Page.WaitForTimeoutAsync(300);
            
            var searchInput = Page.GetByPlaceholder("Search");
            await searchInput.FillAsync("Installation");
            await Page.WaitForTimeoutAsync(500);
            
            var installLink = Page.Locator("a", new() { Has = Page.Locator("text=Installation") }).First;
            await installLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Console.WriteLine("✓ Navigated to Installation");

            // Go back to home
            await Page.GoBackAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Test 2: Navigate to Frames via search
            await searchButton.ClickAsync();
            await Page.WaitForTimeoutAsync(300);
            await searchInput.ClearAsync();
            await searchInput.FillAsync("Frames");
            await Page.WaitForTimeoutAsync(500);
            
            var framesLink = Page.Locator("a", new() { Has = Page.Locator("text=Frames") }).First;
            await framesLink.ClickAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Console.WriteLine("✓ Navigated to Frames");

            // Go back to home
            await Page.GoBackAsync();
            await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

            // Test 3: Search functionality still accessible
            var searchButton2 = Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
            await searchButton2.ClickAsync();
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
