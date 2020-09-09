using CluedIn.Core;
using CluedIn.Core.Messages.Processing;
using System;
using System.Collections.Generic;
using CluedIn.Core.Data;
using CluedIn.Core.Mesh;
using CluedIn.Core.Messages.WebApp;
using CluedIn.Crawling.Adversus.Core;
using System.Linq;
using RestSharp;

namespace CluedIn.Providers.Mesh
{
    public abstract class AdversusCreateMeshProcessor : BaseMeshProcessor
    {
        public EntityType[] EntityType { get; }
        public string EditUrl { get; }

        protected AdversusCreateMeshProcessor(ApplicationContext appContext, string editUrl, params EntityType[] entityType)
            : base(appContext)
        {
            EntityType = entityType;
            EditUrl = editUrl;
        }

        public override bool Accept(MeshDataCommand command, MeshQuery query, IEntity entity)
        {
            return command.ProviderId == this.GetProviderId() && query.Action == ActionType.CREATE && EntityType.Contains(entity.EntityType);
        }

        public override void DoProcess(CluedIn.Core.ExecutionContext context, MeshDataCommand command, IDictionary<string, object> jobData, MeshQuery query)
        {
            return;
        }

        public override List<RawQuery> GetRawQueries(IDictionary<string, object> config, IEntity entity, Core.Mesh.Properties properties)
        {
            var adversusCrawlJobData = new AdversusCrawlJobData();

            return new List<Core.Messages.WebApp.RawQuery>()
            {
                new Core.Messages.WebApp.RawQuery()
                {
                    Query = string.Format("curl -X PUT https://api.adversus.dk" + EditUrl + "{1}?hapikey={0} "  + "--header \"Content-Type: application/json\"" + " --data '{2}'", adversusCrawlJobData.Username, this.GetLookupId(entity), JsonUtility.Serialize(properties)),
                    Source = "cUrl"
                }
            };
        }

        public override Guid GetProviderId()
        {
            return AdversusConstants.ProviderId;
        }

        public override string GetVocabularyProviderKey()
        {
            return "adversus";
        }

        public override string GetLookupId(IEntity entity)
        {
            var code = entity.Codes.ToList().FirstOrDefault(d => d.Origin.Code == "Adversus");
            long id;
            if (!long.TryParse(code.Value, out id))
            {
                //It does not match the id I need.
            }

            return code.Value;
        }

        public override List<QueryResponse> RunQueries(IDictionary<string, object> config, string id, Core.Mesh.Properties properties)
        {
            var adversusCrawlJobData = new AdversusCrawlJobData();
            var client = new RestClient("https://api.adversus.dk/v1/contacts/{id}");
            var request = new RestRequest(string.Format(EditUrl + "{0}", id), Method.PUT);
            request.AddQueryParameter("username", adversusCrawlJobData.Username); // adds to POST or URL querystring based on Method
            request.AddQueryParameter("password", adversusCrawlJobData.Password);
            request.AddJsonBody(properties);

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }

        public override List<QueryResponse> Validate(ExecutionContext context, MeshDataCommand command, IDictionary<string, object> config, string id, MeshQuery query)
        {
            var adversusCrawlJobData = new AdversusCrawlJobData();

            var client = new RestClient("https://api.adversus.dk/v1/contacts/{id}");
            var request = new RestRequest(string.Format(EditUrl + "{0}", id), Method.GET);
            request.AddQueryParameter("username", adversusCrawlJobData.Username); // adds to POST or URL querystring based on Method
            request.AddQueryParameter("password", adversusCrawlJobData.Password);

            var result = client.ExecuteTaskAsync(request).Result;

            return new List<QueryResponse>() { new QueryResponse() { Content = result.Content, StatusCode = result.StatusCode } };
        }
    }
}
