using Microsoft.Playwright;

namespace PlaywrightTests.Pages;

/// <summary>
/// Base page class containing common functionality for all pages
/// </summary>
public abstract class BasePage
{
    protected readonly IPage Page;

    protected BasePage(IPage page)
    {
        Page = page;
    }

    public string PageUrl => Page.Url;

    public async Task<string> GetPageTitleAsync()
    {
        return await Page.TitleAsync();
    }

    public async Task<string> GetPageContentAsync()
    {
        return await Page.ContentAsync();
    }

    public async Task GoBackAsync()
    {
        await Page.GoBackAsync();
    }

    public async Task WaitForNavigationAsync()
    {
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
    }

    public async Task WaitForTimeoutAsync(int milliseconds)
    {
        await Page.WaitForTimeoutAsync(milliseconds);
    }
}
