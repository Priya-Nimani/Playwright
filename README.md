# Playwright Tests - Playwright Documentation

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

**Total: 12 Tests**
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

- [ ] Add screenshot capture on test failures
- [ ] Implement data-driven testing for multiple keywords
- [ ] Add performance testing
- [ ] Extend tests to other Playwright documentation sections
- [ ] Add CI/CD integration

## Notes

- Tests use `.First` property to select first matching element when multiple exist
- Navigation waits for `LoadState.NetworkIdle` to ensure page fully loads
- Search results handled with timeout for dynamic content
- Console logging provides detailed test progress information

## License

MIT


