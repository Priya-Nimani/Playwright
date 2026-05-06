using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

/// <summary>
/// Page object for search functionality on Playwright documentation
/// </summary>
public class SearchPage : BasePage
{
    public SearchPage(IPage page) : base(page) { }

    // Locators
    private ILocator SearchInput => Page.GetByPlaceholder("Search");
    private ILocator SearchButton => Page.GetByRole(AriaRole.Button, new() { Name = "Search" });

    // Methods
    public async Task OpenSearchAsync()
    {
        await SearchButton.ClickAsync();
        await SearchInput.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5000
        });
    }

    public async Task SearchForAsync(string keyword)
    {
        await SearchInput.FillAsync(keyword);

        var searchResult = await GetSearchResultAsync(keyword);
        await searchResult.First.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Visible,
            Timeout = 5000
        });
    }

    public async Task ClearSearchAsync()
    {
        await SearchInput.ClearAsync();
    }

    public async Task<ILocator> GetSearchResultAsync(string keyword)
    {
        return Page.Locator("a", new() { Has = Page.Locator($"text={keyword}") });
    }

    public async Task<int> GetSearchResultCountAsync(string keyword)
    {
        var results = Page.Locator("a").Filter(new() { HasText = keyword });
        return await results.CountAsync();
    }

    public async Task ClickSearchResultAsync(string keyword)
    {
        var result = await GetSearchResultAsync(keyword);
        await result.First.ClickAsync();
        await WaitForNavigationAsync();
    }

    public async Task CloseSearchAsync()
    {
        await Page.Keyboard.PressAsync("Escape");
        await SearchInput.WaitForAsync(new LocatorWaitForOptions
        {
            State = WaitForSelectorState.Hidden,
            Timeout = 5000
        });
    }
}
