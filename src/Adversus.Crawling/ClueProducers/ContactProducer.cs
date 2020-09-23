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

            var vocab = new ContactVocabulary();

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.ExternalId] = input.ExternalId.PrintIfAvailable();
            data.Properties[vocab.PoolId] = input.PoolId.PrintIfAvailable();

            if (input.MappedData.ContainsKey("Fornavn") && input.MappedData.ContainsKey("Efternavn"))
                data.Name = $"{input.MappedData["Fornavn"]} {input.MappedData["Efternavn"]}";
            else
                data.Name = input.Id.PrintIfAvailable();


            foreach (var contactProperty in input.MappedData)
            {
                if (contactProperty.Key.Equals("Fornavn"))
                    data.Properties[vocab.FirstName] = contactProperty.Value;
                else if (contactProperty.Key.Equals("Efternavn"))
                    data.Properties[vocab.LastName] = contactProperty.Value;
                else if (contactProperty.Key.Equals("Adresse"))
                    data.Properties[vocab.Address] = contactProperty.Value;
                else if (contactProperty.Key.Equals("Postnummer"))
                    data.Properties[vocab.ZipCode] = contactProperty.Value;
                else if (contactProperty.Key.Equals("Bynavn"))
                    data.Properties[vocab.City] = contactProperty.Value;
                else if (contactProperty.Key.Equals("Email"))
                    data.Properties[vocab.Email] = contactProperty.Value;
                else if (contactProperty.Key.Equals("Stilling"))
                    data.Properties[vocab.JobTitle] = contactProperty.Value;
                else if (contactProperty.Key.Equals("Firma"))
                    data.Properties[vocab.Company] = contactProperty.Value;
                else  if (contactProperty.Key.Equals("CVR"))
                    data.Properties[vocab.CVR] = contactProperty.Value;
                else
                    data.Properties[$"{contactProperty.Key}-dynamic"] = contactProperty.Value;
            }

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
