using System;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Adversus.Core;
using RuleConstants = CluedIn.Core.Constants.Validation.Rules;

namespace CluedIn.Crawling.Adversus.Factories
{
    public class AdversusClueFactory : ClueFactory
    {
        public AdversusClueFactory()
            : base(AdversusConstants.CodeOrigin, AdversusConstants.ProviderRootCodeValue)
        {
        }

        protected override Clue ConfigureProviderRoot([NotNull] Clue clue)
        {
            if (clue == null)
            {
                throw new ArgumentNullException(nameof(clue));
            }

            var data = clue.Data.EntityData;
            data.Name = AdversusConstants.CrawlerName;
            data.Uri = new Uri(AdversusConstants.Uri);
            data.Description = AdversusConstants.CrawlerDescription;

            clue.ValidationRuleSuppressions.AddRange(new[] {RuleConstants.PROPERTIES_001_MustExist,});

            clue.ValidationRuleSuppressions.AddRange(new[]
            {
                RuleConstants.METADATA_002_Uri_MustBeSet, RuleConstants.PROPERTIES_002_Unknown_VocabularyKey_Used
            });

            return clue;
        }
    }
}
