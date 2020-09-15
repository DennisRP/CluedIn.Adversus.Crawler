using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CluedIn.Core;
using CluedIn.Core.Data;
using CluedIn.Crawling.Adversus.Core.Models;
using CluedIn.Crawling.Adversus.Vocabularies;
using CluedIn.Crawling.Factories;
using CluedIn.Crawling.Helpers;

namespace CluedIn.Crawling.Adversus.ClueProducers
{
    public class SaleProducer : BaseClueProducer<Sale>
    {
        private readonly IClueFactory _factory;

        public SaleProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Sale input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Sales.Sale, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.Id.ToString()))
                data.Name = input.Id.ToString();        

            var vocab = new SaleVocabulary();

            data.Properties[vocab.CampaignId] = input.Campaignid.PrintIfAvailable();
            data.Properties[vocab.Currency] = input.Currency.PrintIfAvailable();
            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.LeadId] = input.Leadid.PrintIfAvailable();
            data.Properties[vocab.Lines] = input.Lines.PrintIfAvailable();
            data.Properties[vocab.State] = input.State.PrintIfAvailable();

            if (input.Leadid != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Sales.Lead, EntityEdgeType.PartOf, input, input.Leadid.ToString());

            if (input.Campaignid != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Marketing.Campaign, EntityEdgeType.PartOf, input, input.Campaignid.ToString());

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
