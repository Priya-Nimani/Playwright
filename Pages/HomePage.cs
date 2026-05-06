using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

/// <summary>
/// Page object for Playwright documentation homepage
/// </summary>
public class HomePage : BasePage
{
    public HomePage(IPage page) : base(page) { }

    // Locators
    private ILocator SearchButton => Page.GetByRole(AriaRole.Button, new() { Name = "Search" });
    private ILocator GetStartedLink => Page.GetByRole(AriaRole.Link).Filter(new() { HasText = "Get Started" });

    // Methods
    public async Task NavigateToHomeAsync()
    {
        await Page.GotoAsync("https://playwright.dev");
        await WaitForNavigationAsync();
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

    public async Task ClickGetStartedLinkAsync()
    {
        await GetStartedLink.First.ClickAsync();
        await WaitForNavigationAsync();
    }

    public async Task VerifyHomepageTitleAsync(string expectedText)
    {
        var title = await GetPageTitleAsync();
        if (!title.Contains(expectedText))
        {
            throw new Exception($"Expected title to contain '{expectedText}', but got '{title}'");
        }
    }
}
