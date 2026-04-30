using Microsoft.Playwright;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PlaywrightTests.Utilities;

/// <summary>
/// Helper class for capturing screenshots, traces, and diagnostics on test failures
/// </summary>
public class TestArtifactCapture
{
    private readonly IPage _page;
    private readonly IBrowserContext _context;
    private readonly string _artifactsDirectory;
    private readonly string _testName;

    public TestArtifactCapture(IPage page, IBrowserContext context, string testName, string artifactsDirectory = "test-artifacts")
    {
        _page = page;
        _context = context;
        _testName = testName;
        _artifactsDirectory = artifactsDirectory;
        
        // Create artifacts directory if it doesn't exist
        if (!Directory.Exists(_artifactsDirectory))
        {
            Directory.CreateDirectory(_artifactsDirectory);
        }
    }

    /// <summary>
    /// Capture a screenshot of the current page state
    /// </summary>
    public async Task CaptureScreenshotAsync(string suffix = "")
    {
        try
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
            var filename = string.IsNullOrEmpty(suffix) 
                ? $"{_testName}_{timestamp}.png"
                : $"{_testName}_{suffix}_{timestamp}.png";
            
            var filepath = Path.Combine(_artifactsDirectory, filename);
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = filepath, FullPage = true });
            Console.WriteLine($"📸 Screenshot captured: {filepath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to capture screenshot: {ex.Message}");
        }
    }

    /// <summary>
    /// Capture a video recording (requires video recording to be enabled)
    /// </summary>
    public async Task CaptureVideoAsync()
    {
        try
        {
            if (_page.Video != null)
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                var filename = $"{_testName}_video_{timestamp}.webm";
                var filepath = Path.Combine(_artifactsDirectory, filename);
                
                await _page.Video.SaveAsAsync(filepath);
                Console.WriteLine($"🎥 Video captured: {filepath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to capture video: {ex.Message}");
        }
    }

    /// <summary>
    /// Get the current page HTML content
    /// </summary>
    public async Task CapturePageHtmlAsync(string suffix = "failure")
    {
        try
        {
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
            var filename = $"{_testName}_{suffix}_{timestamp}.html";
            var filepath = Path.Combine(_artifactsDirectory, filename);
            
            var content = await _page.ContentAsync();
            await File.WriteAllTextAsync(filepath, content);
            Console.WriteLine($"📄 Page HTML captured: {filepath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to capture page HTML: {ex.Message}");
        }
    }

    /// <summary>
    /// Capture and stop Playwright trace recording
    /// </summary>
    public async Task CaptureTraceAsync(string suffix = "failure")
    {
        try
        {
            if (_context?.Tracing != null)
            {
                var timestamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                var filename = $"{_testName}_{suffix}_{timestamp}.zip";
                var filepath = Path.Combine(_artifactsDirectory, filename);
                
                await _context.Tracing.StopAsync(new TracingStopOptions { Path = filepath });
                Console.WriteLine($"🔍 Trace captured: {filepath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to capture trace: {ex.Message}");
        }
    }

    /// <summary>
    /// Start Playwright trace recording (call this at the beginning of a test)
    /// </summary>
    public async Task StartTraceAsync()
    {
        try
        {
            if (_context?.Tracing != null)
            {
                await _context.Tracing.StartAsync(new TracingStartOptions
                {
                    Screenshots = true,
                    Snapshots = true,
                    Sources = true
                });
                Console.WriteLine("🔴 Trace recording started");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to start trace: {ex.Message}");
        }
    }

    /// <summary>
    /// Capture page console logs
    /// </summary>
    public void LogPageDiagnostics()
    {
        try
        {
            var url = _page.Url;
            var title = _page.TitleAsync().Result;
            
            Console.WriteLine($"📋 Page Diagnostics:");
            Console.WriteLine($"   URL: {url}");
            Console.WriteLine($"   Title: {title}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ Failed to capture diagnostics: {ex.Message}");
        }
    }
}
