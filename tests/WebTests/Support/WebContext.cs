namespace WebTests.Support;

public class WebContext
{
    public HttpClient Client { get; set; } = default!;
    public HttpResponseMessage? LastResponse { get; set; }
    public string LastBody { get; set; } = string.Empty;
}
