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
    public class ContactProducer : BaseClueProducer<Contact>
    {
        private readonly IClueFactory _factory;

        public ContactProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Contact input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Infrastructure.Contact, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            data.Name = input.Id.ToString();

            var vocab = new ContactVocabulary();

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.Data] = input.Data.PrintIfAvailable();
            data.Properties[vocab.ExternalId] = input.ExternalId.PrintIfAvailable();
            data.Properties[vocab.PoolId] = input.PoolId.PrintIfAvailable();

            if (input.PoolId != default)
                _factory.CreateOutgoingEntityReference(clue, "/Pool", EntityEdgeType.PartOf, input, input.PoolId.ToString());

            if (!string.IsNullOrWhiteSpace(input.ExternalId))
                data.Codes.Add(new EntityCode(EntityType.Infrastructure.Contact, CodeOrigin.CluedIn, input.ExternalId));

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
