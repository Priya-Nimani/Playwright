using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using System;
using System.Threading.Tasks;
using NUnit.Framework;
using PlaywrightTests.Utilities;

namespace PlaywrightTests;

/// <summary>
/// Base test class that automatically captures screenshots, traces, and artifacts on test failure
/// </summary>
public abstract class BasePlaywrightTest : PageTest
{
    protected TestArtifactCapture ArtifactCapture;

    [SetUp]
    public virtual async Task BaseSetUp()
    {
        // Initialize artifact capture with current test name
        var testName = TestContext.CurrentContext.Test.Name;
        ArtifactCapture = new TestArtifactCapture(Page, Context, testName);
        
        // Start trace recording for debugging
        await ArtifactCapture.StartTraceAsync();
    }

    [TearDown]
    public async Task BaseTearDown()
    {
        // Capture artifacts if test failed
        if (TestContext.CurrentContext.Result.Outcome.Status == NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            Console.WriteLine("\n❌ Test failed - capturing artifacts...");
            
            // Capture trace first (should be stopped before screenshot)
            await ArtifactCapture.CaptureTraceAsync("failure");
            
            // Capture screenshot
            await ArtifactCapture.CaptureScreenshotAsync("failure");
            
            // Capture page HTML
            await ArtifactCapture.CapturePageHtmlAsync("failure");
            
            // Log diagnostics
            ArtifactCapture.LogPageDiagnostics();
            
            Console.WriteLine("✅ Artifacts captured successfully");
        }
    }
}
