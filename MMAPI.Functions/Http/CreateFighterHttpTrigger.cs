using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Models;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MMAPI.Functions.Http
{
    using Helpers;
    public static class CreateFighterHttpTrigger
    {
        [FunctionName("CreateFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "fighter")]HttpRequestMessage req,
            TraceWriter log)
        {
            var fighter = await req.Content.ReadAsAsync<Fighter>();

            log.Info($"Received a new fighter: {JsonConvert.SerializeObject(fighter)}");

            if (await HasExistingFighter(fighter))
            {
                return req.CreateResponse(HttpStatusCode.Conflict);
            }

            var result = await CreateFighter(fighter);
            return req.CreateResponse(new { Id = result });
        }

        private static async Task<string> CreateFighter(Fighter fighter)
        {
            var uri = Environment.GetEnvironmentVariable("DocumentDbUri");
            var authKey = Environment.GetEnvironmentVariable("DocumentDbAuthKey");

            using (var client = GetClient())
            {
                Database db = await client.CreateDatabaseIfNotExistsAsync(new Database { Id = "mmapidb" });
                var collection = await client.CreateDocumentCollectionIfNotExistsAsync(db.SelfLink, new DocumentCollection { Id = "fighters" });
                Document result = await client.CreateDocumentAsync(FightersCollection, fighter);               

                return result.Id;
            }
        }

        private static Uri FightersCollection
        {
            get
            {
                return UriFactory.CreateDocumentCollectionUri("mmapidb", "fighters");
            }
        }

        /// <summary>
        /// New up an instance of DocumentClient
        /// </summary>
        private static DocumentClient GetClient()
        {
            var uri = Environment.GetEnvironmentVariable("DocumentDbUri");
            var authKey = Environment.GetEnvironmentVariable("DocumentDbAuthKey");

            return new DocumentClient(
                serviceEndpoint: new Uri(uri),
                authKeyOrResourceToken: authKey,
                connectionPolicy: new ConnectionPolicy { ConnectionMode = ConnectionMode.Direct });
        }

        /// <summary>
        /// Determine whether a fighter already exists.
        /// </summary>
        /// <param name="fighter">Potential new fighter.</param>
        private static async Task<bool> HasExistingFighter(Fighter fighter)
        {
            using (var client = GetClient())
            {
                Expression<Func<Fighter, bool>> nameAndDobMatch = d =>
                    d.FirstName == fighter.FirstName &&
                    d.LastName == fighter.LastName &&
                    d.DateOfBirth == fighter.DateOfBirth;

                var result = await client
                    .CreateDocumentQuery<Fighter>(FightersCollection)
                    .Where(nameAndDobMatch)
                    .AsDocumentQuery()
                    .GetAllResultsAsync();

                return result.Any();
            }          
        }
    }
}
