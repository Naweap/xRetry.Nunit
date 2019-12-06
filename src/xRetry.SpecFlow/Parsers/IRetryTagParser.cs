namespace xRetry.SpecFlow.NUnit.Parsers
{
    public interface IRetryTagParser
    {
        RetryTag Parse(string tag);
    }
}
