namespace Search.Extensions;

public static class ArgumentExtensions
{
    public static string? GetNameArgument(this string[] args)
    {
        return args
            .Select((arg, i) => new { arg, i })
            .Where(x => x.arg.StartsWith("--"))
            .ToDictionary(
                x => x.arg.TrimStart('-').ToLower(),
                x => args.Length > x.i + 1 ? args[x.i + 1] : ""
            )
            .TryGetValue("name", out var name) && !string.IsNullOrWhiteSpace(name)
                ? name
                : null;
    }
}
