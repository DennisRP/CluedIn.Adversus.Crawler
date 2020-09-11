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
    public class AppointmentProducer : BaseClueProducer<Appointment>
    {
        private readonly IClueFactory _factory;

        public AppointmentProducer([NotNull] IClueFactory factory)
        {
            if (factory == null)
                throw new ArgumentNullException(nameof(factory));

            _factory = factory;
        }

        protected override Clue MakeClueImpl([NotNull] Appointment input, Guid accountId)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var clue = _factory.Create(EntityType.Calendar.Event, input.Id.ToString(), accountId);

            var data = clue.Data.EntityData;

            data.Name = input.Id.ToString();

            var vocab = new AppointmentVocabulary();      

            data.Properties[vocab.Id] = input.Id.PrintIfAvailable();
            data.Properties[vocab.ConsultantId] = input.ConsultantId.PrintIfAvailable();
            data.Properties[vocab.End] = input.End.PrintIfAvailable();
            data.Properties[vocab.LeadId] = input.LeadId.PrintIfAvailable();
            data.Properties[vocab.Start] = input.Start.PrintIfAvailable();
            data.Properties[vocab.Status] = input.Status.PrintIfAvailable();

            if (!string.IsNullOrWhiteSpace(input.Status))
                data.Tags.Add(new Tag(input.Status));

            if (input.LeadId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Sales.Lead, EntityEdgeType.PartOf, input, input.LeadId.ToString());

            if (input.ConsultantId != default)
                _factory.CreateOutgoingEntityReference(clue, EntityType.Person, EntityEdgeType.PartOf, input, input.ConsultantId.ToString());

            if (!data.OutgoingEdges.Any())
                _factory.CreateEntityRootReference(clue, EntityEdgeType.PartOf);

            return clue;
        }
    }
}
