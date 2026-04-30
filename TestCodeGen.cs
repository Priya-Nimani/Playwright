using Microsoft.Playwright.NUnit;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

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
    public async Task MyTest()
    {
        //await Page.GetByLabel("Breadcrumbs").GetByText("Getting Started").ClickAsync();
        await Expect(Page).ToHaveTitleAsync(new Regex("Playwright"));
        await Page.ClickAsync("text=Get Started");
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Installation" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Installation" })).ToBeVisibleAsync();
        await Expect(Page.GetByRole(AriaRole.Link, new() { Name = "Writing tests", Exact = true })).ToBeVisibleAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Installation" }).ClickAsync();
        await Page.GetByRole(AriaRole.Link, new() { Name = "Generating tests" }).ClickAsync();
        await Expect(Page.Locator("h1")).ToContainTextAsync("Generating tests");
        await Expect(Page.GetByRole(AriaRole.Article)).ToMatchAriaSnapshotAsync("- paragraph: Playwright can generate tests automatically, providing a quick way to get started with testing. Codegen opens a browser window for interaction and the Playwright Inspector for recording, copying, and managing your generated tests.");
        await Page.GetByRole(AriaRole.Link, new() { Name = "How to record a test" }).ClickAsync();
        await Expect(Page.GetByRole(AriaRole.Heading, new() { Name = "Recording a testDirect link" })).ToBeVisibleAsync();
    }
}
