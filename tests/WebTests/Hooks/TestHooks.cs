using Reqnroll;
using WebTests.Support;

namespace WebTests.Hooks;

[Binding]
public class TestHooks
{
    private static TestApplication _application = default!;

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        _application = new TestApplication();
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        _application?.Dispose();
    }

    [BeforeScenario]
    public void BeforeScenario(WebContext context)
    {
        context.Client = _application.CreateClient(new Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = true
        });
    }

    [AfterScenario]
    public void AfterScenario(WebContext context)
    {
        context.LastResponse?.Dispose();
        context.Client?.Dispose();
    }
}
