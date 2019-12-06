using System;
using NUnit.Framework;
using xRetry.SpecFlow.NUnit.Parsers;

namespace Tests.SpecFlow.Parsers
{
    public class RetryTagParserTests
    {
        [Test]
        public void Parse_Null_ThrowsArgumentNullException() =>
            Assert.Throws<ArgumentNullException>(() => getParser().Parse(null));

        [Test]
        public void Parse_NoParams_CorrectResult()
        {
            // Arrange
            RetryTagParser parser = getParser();
            RetryTag expected = new RetryTag(null, null);

            // Act
            RetryTag actual = parser.Parse("retry");

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("retry(5)", 5)]
        [TestCase("Retry(5)", 5)]
        [TestCase("RETRY(5)", 5)]
        [TestCase("ReTrY(5)", 5)]
        public void Parse_MaxRetries_ReturnsCorrectResult(string tag, int maxRetries)
        {
            // Arrange
            RetryTagParser parser = getParser();
            RetryTag expected = new RetryTag(maxRetries, null);

            // Act
            RetryTag actual = parser.Parse(tag);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestCase("retry(5,100)", 5, 100)]
        [TestCase("Retry(5,100)", 5, 100)]
        [TestCase("RETRY(5,100)", 5, 100)]
        [TestCase("rEtRy(5,100)", 5, 100)]
        [TestCase("retry(765,87)", 765, 87)]
        public void Parse_MaxRetriesAndDelayBetweenRetriesMs_ReturnsCorrectResult(string tag, int maxRetries,
            int delayBetweenRetriesMs)
        {
            // Arrange
            RetryTagParser parser = getParser();
            RetryTag expected = new RetryTag(maxRetries, delayBetweenRetriesMs);

            // Act
            RetryTag actual = parser.Parse(tag);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        private RetryTagParser getParser() => new RetryTagParser();
    }
}
