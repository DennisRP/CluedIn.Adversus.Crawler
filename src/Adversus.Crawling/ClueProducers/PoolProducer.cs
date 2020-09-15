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
    public class PoolProducer : BaseClueProducer<Pool>
    {
        private readonly IClueFactory _factory;

        public PoolProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Pool input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create("/Pool", input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.Name))
            {
                data.Name = input.Name.ToString();
            }
            var vocab = new PoolVocabulary();

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.Active] = input.Active.PrintIfAvailable();

            DateTimeOffset createdDate;
            if (DateTimeOffset.TryParse(input.Created, out createdDate))
            {
                data.CreatedDate = createdDate;
            }

            data.Properties[vocab.Created] = input.Created.PrintIfAvailable();
            data.Properties[vocab.Name] = input.Name.PrintIfAvailable();

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
