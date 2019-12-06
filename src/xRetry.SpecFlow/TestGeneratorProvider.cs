using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using TechTalk.SpecFlow.Generator;
using TechTalk.SpecFlow.Generator.CodeDom;
using xRetry.SpecFlow.NUnit.Parsers;

namespace xRetry.SpecFlow.NUnit
{
    public class TestGeneratorProvider : CustomNUnit3TestGeneratorProvider
    {
        private readonly IRetryTagParser _retryTagParser;

        public TestGeneratorProvider(CodeDomHelper codeDomHelper, IRetryTagParser retryTagParser) 
            : base(codeDomHelper)
        {
            _retryTagParser = retryTagParser;
        }

        public override void SetTestMethodCategories(TestClassGenerationContext generationContext, CodeMemberMethod testMethod, IEnumerable<string> scenarioCategories)
        {
            // Prevent multiple enumerations
            scenarioCategories = scenarioCategories.ToList();

            base.SetTestMethodCategories(generationContext, testMethod, scenarioCategories);

            string strRetryTag = getRetryTag(scenarioCategories);
            if (strRetryTag != null)
            {
                RetryTag retryTag = _retryTagParser.Parse(strRetryTag);

                // Add the Retry attribute
                CodeDomHelper.AddAttribute(testMethod, "NUnit.Framework.RetryAttribute", retryTag?.MaxRetries ?? 3);

            }
        }

        private string getRetryTag(IEnumerable<string> tags) =>
            tags.FirstOrDefault(t =>
                t.StartsWith(Constants.RETRY_TAG, StringComparison.OrdinalIgnoreCase) &&
                // Is just "retry", or is "retry("... for params
                (t.Length == Constants.RETRY_TAG.Length || t[Constants.RETRY_TAG.Length] == '('));
    }
}
