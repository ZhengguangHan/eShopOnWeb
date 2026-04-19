using System.Net.Http;

namespace PublicApiTests.Support;

public class ApiContext
{
    public HttpClient Client { get; set; } = default!;
    public HttpResponseMessage Response { get; set; } = default!;
    public string ResponseBody { get; set; } = string.Empty;
}
