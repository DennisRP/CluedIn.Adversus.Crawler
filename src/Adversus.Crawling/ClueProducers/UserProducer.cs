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
    public class UserProducer : BaseClueProducer<User>
    {
        private readonly IClueFactory _factory;

        public UserProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] User input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Infrastructure.User, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            if (!string.IsNullOrWhiteSpace(input.Name.ToString()))
                data.Name = input.Name.ToString();        

            var vocab = new UserVocabulary();

            data.Properties[vocab.Active] = input.Active.PrintIfAvailable();
            data.Properties[vocab.Admin] = input.Admin.PrintIfAvailable();
            data.Properties[vocab.Email] = input.Email.PrintIfAvailable();

            if (!string.IsNullOrWhiteSpace(input.Email))
                data.Codes.Add(new EntityCode(EntityType.Infrastructure.User, CodeOrigin.CluedIn, input.Email));

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.Locale] = input.Locale.PrintIfAvailable();
            data.Properties[vocab.Name] = input.Name.PrintIfAvailable();
            data.Properties[vocab.Phone] = input.Phone.PrintIfAvailable();

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
