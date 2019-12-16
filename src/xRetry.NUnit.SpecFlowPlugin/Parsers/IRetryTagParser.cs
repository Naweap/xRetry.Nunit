namespace xRetry.NUnit.SpecFlowPlugin.Parsers
{
    public interface IRetryTagParser
    {
        RetryTag Parse(string tag);
    }
}
