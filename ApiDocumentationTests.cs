using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightTests.Pages;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class ApiDocumentationTests : BasePlaywrightTest
{
    private HomePage _homePage;
    private SearchPage _searchPage;
    private ApiPage _apiPage;

    [SetUp]
    public async Task Setup()
    {
        await BaseSetUp();
        _homePage = new HomePage(Page);
        _searchPage = new SearchPage(Page);
        _apiPage = new ApiPage(Page);
        await _homePage.NavigateToHomeAsync();
    }

    [Test]
    public async Task VerifyApiPageLoads()
    {
        try
        {
            Console.WriteLine("📖 Testing API page navigation...");
            await _apiPage.NavigateToApiPageAsync();
            
            var headerText = await _apiPage.GetPageHeaderTextAsync();
            if (string.IsNullOrEmpty(headerText))
            {
                throw new Exception("API page header is empty");
            }

            Console.WriteLine($"✓ API page header: {headerText}");
            Console.WriteLine($"✓ Page URL: {_apiPage.PageUrl}");
            Console.WriteLine("✅ TEST PASSED: API page loaded successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ TEST FAILED: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task SearchApiDocumentation()
    {
        try
        {
            Console.WriteLine("🔍 Testing API documentation search...");
            await _homePage.VerifyHomepageTitleAsync("Playwright");

            await _searchPage.OpenSearchAsync();
            await _searchPage.SearchForAsync("API");
            
            var resultCount = await _searchPage.GetSearchResultCountAsync("API");
            if (resultCount == 0)
            {
                throw new Exception("No search results found for 'API'");
            }

            Console.WriteLine($"✓ Found {resultCount} API search result(s)");
            Console.WriteLine("✅ TEST PASSED: API search completed successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ TEST FAILED: {ex.Message}");
            throw;
        }
    }

    [Test]
    public async Task VerifyApiSections()
    {
        try
        {
            Console.WriteLine("📋 Verifying API sections...");
            await _apiPage.NavigateToApiPageAsync();
            await _apiPage.VerifyNavigationMenuExistsAsync();
            
            Console.WriteLine("✓ Navigation menu verified");

            await _apiPage.VerifyApiSectionsExistAsync();
            
            Console.WriteLine("✓ API sections verified");
            Console.WriteLine("✅ TEST PASSED: API sections verified successfully");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ TEST FAILED: {ex.Message}");
            throw;
        }
    }
}
