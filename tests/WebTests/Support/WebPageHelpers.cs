using System.Text.RegularExpressions;

namespace WebTests.Support;

public static class WebPageHelpers
{
    public const string TokenTag = "__RequestVerificationToken";

    public static string GetRequestVerificationToken(string body)
    {
        const string pattern = @"name=""__RequestVerificationToken"" type=""hidden"" value=""([-A-Za-z0-9+=/\\_]+?)""";
        var match = Regex.Match(body, pattern);
        return match.Groups[1].Value;
    }

    public static string GetFirstItemId(string body)
    {
        const string pattern = @"name=""Items\[0\].Id"" value=""(\d+)""";
        var match = Regex.Match(body, pattern);
        return match.Groups[1].Value;
    }
}
