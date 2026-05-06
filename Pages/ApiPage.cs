using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

/// <summary>
/// Page object for Playwright API documentation
/// </summary>
public class ApiPage : BasePage
{
    public ApiPage(IPage page) : base(page) { }

    // Locators
    private ILocator PageHeader => Page.Locator("h1");
    private ILocator SearchButton => Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
    private ILocator NavigationMenu => Page.Locator("nav");
    private ILocator ApiSections => Page.Locator("//a[contains(@href, '/docs/api/')]");

    // Methods
    public async Task NavigateToApiPageAsync()
    {
        await Page.GotoAsync("https://playwright.dev/docs/api");
        await WaitForNavigationAsync();
    }

    public async Task VerifyApiHeaderAsync(string expectedText)
    {
        await PageHeader.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5000
        });
        
        var headerText = await PageHeader.TextContentAsync() ?? string.Empty;
        if (!headerText.Contains(expectedText))
        {
            throw new Exception($"Expected header to contain '{expectedText}', but got '{headerText}'");
        }
    }

    public async Task ClickSearchButtonAsync()
    {
        await SearchButton.ClickAsync();

        var searchInput = Page.GetByPlaceholder("Search");
        await searchInput.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5000
        });
    }

    public async Task SearchForApiAsync(string searchTerm)
    {
        var searchInput = Page.GetByPlaceholder("Search");
        await searchInput.FillAsync(searchTerm);
        await searchInput.PressAsync("Enter");
        await WaitForNavigationAsync();
    }

    public async Task VerifyNavigationMenuExistsAsync()
    {
        await NavigationMenu.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5000
        });
    }

    public async Task VerifyApiSectionsExistAsync()
    {
        var count = await ApiSections.CountAsync();
        if (count == 0)
        {
            throw new Exception("No API sections found on the page");
        }
    }

    public async Task<string> GetPageHeaderTextAsync()
    {
        return await PageHeader.TextContentAsync() ?? string.Empty;
    }
}
