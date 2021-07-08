# Testing with Playwright

[Playwright](https://playwright.dev/dotnet/) is an end-to-end testing framework using browser automation.

## Setup

Quick setup as described in the [Getting Started](https://playwright.dev/dotnet/docs/intro/) docs.

Start with installing Playwright dotnet tool globally. This only needs to be done once.

```
dotnet tool install --global Microsoft.Playwright.CLI
```

Install the Playwright dependency for this project.

```
dotnet build
playwright install
```

## Running tests

Playwright requires that the application already be running on localhost in order to run the tests. The quickest way to do this is to open a command prompt in the FMS application folder and run the app with `dotnet run`. Then open a new command prompt in the Playwright.Tests folder and run the tests with:

```
dotnet test
```

If desired, either the application or the tests (or both) can be run from an IDE to enable debugging tools.
