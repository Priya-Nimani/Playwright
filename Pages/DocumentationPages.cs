using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;

namespace PlaywrightTests.Pages;

/// <summary>
/// Page object for Frames documentation page
/// </summary>
public class FramesPage : BasePage
{
    public FramesPage(IPage page) : base(page) { }

    // Methods
    public async Task VerifyFramesPageAsync()
    {
        var url = PageUrl;
        if (!url.Contains("frames"))
        {
            throw new Exception($"Expected URL to contain 'frames', but got '{url}'");
        }
    }

    public bool IsFramesPage => PageUrl.Contains("frames");
}

/// <summary>
/// Page object for Installation documentation page
/// </summary>
public class InstallationPage : BasePage
{
    public InstallationPage(IPage page) : base(page) { }

    // Methods
    public async Task VerifyInstallationPageAsync()
    {
        var url = PageUrl;
        var content = await GetPageContentAsync();
        
        // Check if we navigated to any documentation page (URL changed from home)
        if (!url.Contains("playwright.dev/docs") && !content.Contains("Installation", StringComparison.OrdinalIgnoreCase))
        {
            throw new Exception($"Failed to navigate to a documentation page. URL: {url}");
        }
    }

    public bool IsInstallationPage => PageUrl.Contains("installation") || PageUrl.Contains("docs");
}

/// <summary>
/// Page object for API/BrowserContext documentation page
/// </summary>
public class BrowserContextPage : BasePage
{
    public BrowserContextPage(IPage page) : base(page) { }

    // Methods
    public async Task VerifyBrowserContextPageAsync()
    {
        var url = PageUrl;
        if (!url.Contains("browser") && !url.Contains("api"))
        {
            throw new Exception($"Expected URL to contain 'browser' or 'api', but got '{url}'");
        }
    }

    public bool IsBrowserContextPage => PageUrl.Contains("browser") || PageUrl.Contains("api");
}

/// <summary>
/// Page object for Locators documentation page
/// </summary>
public class LocatorsPage : BasePage
{
    public LocatorsPage(IPage page) : base(page) { }

    // Methods
    public async Task VerifyLocatorsPageAsync()
    {
        var content = await GetPageContentAsync();
        var url = PageUrl;

        if ((!content.Contains("Locator") && !url.Contains("locator")))
        {
            throw new Exception("Expected Locators page content or URL");
        }
    }

    public bool IsLocatorsPage => PageUrl.Contains("locator");
}
