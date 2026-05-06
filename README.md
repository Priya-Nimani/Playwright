# Playwright Tests - Playwright Documentation

[![Playwright Tests](https://github.com/Priya-Nimani/Playwright/actions/workflows/playwright.yml/badge.svg)](https://github.com/Priya-Nimani/Playwright/actions/workflows/playwright.yml)

Comprehensive automated test suite for Playwright documentation using Page Object Model (POM) pattern with NUnit and C#.

## Overview

This project contains automated tests for the [Playwright documentation website](https://playwright.dev), covering navigation, search functionality, and documentation page verification.

## Architecture

### Page Object Model (POM)
Tests are organized using the Page Object Model pattern for improved maintainability and reusability:

- **BasePage.cs** - Abstract base class with common page functionality
- **HomePage.cs** - Homepage interactions and navigation
- **SearchPage.cs** - Search functionality
- **DocumentationPages.cs** - Individual documentation page objects

## Test Coverage

### Basic Tests (UnitTest1.cs) - 2 Tests
1. **HasTitle** - Verifies Playwright documentation page loads with correct title
2. **GetStartedLink** - Tests navigation via "Get Started" link to Installation page

### Documentation Tests (PlaywrightDocumentationTests.cs) - 10 Tests

#### Search Tests
- **NavigateToFramesDocumentation** - Search and navigate to Frames page
- **SearchForInstallationDocumentation** - Search and navigate to Installation page
- **SearchForBrowserContextDocumentation** - Search and navigate to BrowserContext API page
- **SearchForLocatorsDocumentation** - Search and navigate to Locators page

#### Navigation Tests
- **NavigateToIntroductionPage** - Navigate to Introduction page via search
- **NavigateViaGetStartedLink** - Navigate from Get Started link to Installation
- **VerifyMultipleNavigationPaths** - Test multiple navigation flows (Installation → Frames)

#### Verification Tests
- **VerifyDocumentationNavigation** - Verify key documentation pages exist
- **VerifyPageTitleOnHomepage** - Verify homepage title contains "Playwright"
- **VerifyHomepagePerformanceMetrics** - Capture homepage load performance metrics

**Total: 13 Tests**
- ✅ All tests passing
- Average duration: ~32 seconds

## Technologies & Dependencies

- **Framework**: NUnit 4.3.2
- **Playwright**: Microsoft.Playwright.NUnit 1.59.0
- **Language**: C# (.NET 10.0)
- **Architecture**: Page Object Model

## Project Structure

```
PlaywrightTests/
├── UnitTest1.cs                    # Basic tests
├── PlaywrightDocumentationTests.cs # Documentation tests
├── Pages/
│   ├── BasePage.cs                 # Base page class
│   ├── HomePage.cs                 # Homepage page object
│   ├── SearchPage.cs               # Search page object
│   └── DocumentationPages.cs        # Documentation pages (Frames, Installation, etc.)
├── PlaywrightTests.csproj          # Project configuration
└── README.md                        # This file
```

## Key Features

✅ **Page Object Model** - Centralized locators and methods  
✅ **Comprehensive Coverage** - 12 tests covering search, navigation, and verification  
✅ **Robust Locators** - Specific and non-brittle element selectors  
✅ **Detailed Logging** - Console output for test progress and debugging  
✅ **Error Handling** - Explicit error messages for failures  
✅ **Automatic Failure Capture** - Screenshots and HTML snapshots on test failures  

## Test Artifacts & Failure Diagnostics

When tests fail, the framework automatically captures comprehensive diagnostic artifacts:

### Automatic Artifact Capture
- **Playwright Trace** - Complete trace recording (`.zip` file with screenshots, DOM snapshots, network activity, console logs)
- **Screenshots** - Full-page PNG screenshots on test failure
- **Page HTML** - HTML content of the failed page for DOM inspection
- **Diagnostics** - Current URL and page title logged to console
- **Auto-Directory Creation** - Artifacts automatically saved to `test-artifacts/` directory
- **Timestamped Files** - Unique filenames prevent overwriting (format: `{TestName}_{timestamp}.{ext}`)

### Access Captured Artifacts
Artifacts are automatically captured in the `test-artifacts/` directory:
```
test-artifacts/
├── TestName_failure_20240120_143025.zip     # Playwright trace (viewable in Inspector)
├── TestName_failure_20240120_143025.png     # Screenshot
├── TestName_failure_20240120_143025.html    # HTML snapshot
└── VerifyHomepagePerformanceMetrics_homepage_performance_20240120_143025.json # Performance metrics report
```

### Viewing Trace Files
Trace files can be viewed using Playwright Inspector:
```bash
npx playwright show-trace test-artifacts/TestName_failure_20240120_143025.zip
```

Or via Playwright's online trace viewer: https://trace.playwright.dev

### How It Works
1. `BasePlaywrightTest` automatically initializes `TestArtifactCapture` in setup
2. Trace recording starts automatically with `StartTraceAsync()` 
3. When a test fails, the `TearDown` method captures:
   - Trace (stopped and saved)
   - Screenshot
   - HTML snapshot
   - Page diagnostics
4. Console output confirms artifact capture with indicators (🔍, 📸, 📄)
5. Artifacts are available for review immediately after test completion

### Implementation Details
The artifact capture system is fully integrated into the base test class:
- All test classes inherit from `BasePlaywrightTest`
- Trace recording runs for every test automatically
- `TearDown` method checks test failure status and captures if needed
- `TestArtifactCapture` utility handles file operations, tracing, and naming
- No manual configuration required - works automatically with rich diagnostics

## Running Tests

### Run all tests
```bash
dotnet test
```

### Run specific test class
```bash
dotnet test --filter "PlaywrightDocumentationTests"
```

### Run specific test
```bash
dotnet test --filter "NavigateToFramesDocumentation"
```

### Build project
```bash
dotnet build
```

## Continuous Integration

This repository is configured to run on GitHub Actions using the workflow in `.github/workflows/playwright.yml`.

### CI behavior
- Runs on every push and pull request across all branches
- Uses `.NET 10.0` and restores dependencies before building
- Installs Playwright browsers before executing tests
- Uploads `test-artifacts/**` and `TestResults/**` after every run for failure diagnostics

### How to use
Push your branch or open a pull request and the workflow will execute automatically.


## Test Locators

All tests use specific, robust locators to avoid strict mode violations:

- **GetByRole()** - Accessible element locators (buttons, links, headings)
- **GetByPlaceholder()** - Input field locators
- **Locator.Filter()** - Filtering with text matching
- **Page.Keyboard** - Keyboard interaction (Escape to close search)

## Recent Changes

### Page Object Model Refactoring (Latest)
- Implemented BasePage abstract class
- Created HomePage, SearchPage, and DocumentationPages objects
- Refactored all tests to use page objects
- Improved code reusability and maintainability

### Test Suite Organization
- Split documentation tests into separate test file
- Organized tests by functionality (search, navigation, verification)
- Added comprehensive logging for better debugging

## Future Enhancements

- [ ] Implement data-driven testing for multiple keywords
- [ ] Add performance testing
- [ ] Extend tests to other Playwright documentation sections
- [ ] Add CI/CD integration
- [ ] Enable video recording during test execution

## Notes

- All test classes inherit from `BasePlaywrightTest` for automatic failure capture and trace recording
- Trace recording starts automatically for every test - includes screenshots, DOM snapshots, network activity
- Tests use `.First` property to select first matching element when multiple exist
- Navigation waits for `LoadState.NetworkIdle` to ensure page fully loads
- Search results handled with timeout for dynamic content
- Console logging provides detailed test progress information
- Artifacts are captured automatically on failure - no manual code needed in test classes
- Trace files are captured in `.zip` format and can be viewed with `npx playwright show-trace`

## License

MIT


