using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;

namespace PublicApiIntegrationTests;

[TestClass]
public class ProgramTest
{
    private static readonly WebApplicationFactory<Program> _application = new();

    public static HttpClient NewClient => _application.CreateClient();
}
