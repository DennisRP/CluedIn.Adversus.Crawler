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
    public class ProductProducer : BaseClueProducer<Product>
    {
        private readonly IClueFactory _factory;

        public ProductProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Product input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Product, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.Title))
                data.Name = input.Title;

            if (!string.IsNullOrWhiteSpace(input.Description))
                data.Description = input.Description;

            var vocab = new ProductVocabulary();

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.Max] = input.Max.PrintIfAvailable();
            data.Properties[vocab.Min] = input.Min.PrintIfAvailable();
            data.Properties[vocab.Unit] = input.Unit.PrintIfAvailable();
            data.Properties[vocab.UnitPrice] = input.UnitPrice.PrintIfAvailable();
       
            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
