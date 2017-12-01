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
    using MMAPI.Repository;
    using MMAPI.Repository.Data;
    using Newtonsoft.Json.Serialization;

    public static class CreateFighterHttpTrigger
    {
        [FunctionName("CreateFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "fighter")]HttpRequestMessage req,
            TraceWriter log)
        {
            var fighter = await req.Content.ReadAsAsync<Fighter>();

            log.Info($"Received a new fighter: {JsonConvert.SerializeObject(fighter)}");

            var fighterRepo = GetFighterRepository();

            var existingFighter = await fighterRepo.ExistsAsync<Fighter>(d =>
                d.FirstName == fighter.FirstName &&
                d.LastName == fighter.LastName &&
                d.DateOfBirth == fighter.DateOfBirth);

            if (existingFighter)
            {
                return req.CreateResponse(HttpStatusCode.Conflict);
            }
                
            var id = await fighterRepo.CreateDocumentAsync(fighter);

            return req.CreateResponse(new { id = id });
            
        }

        private static string Uri {  get { return Environment.GetEnvironmentVariable("DocumentDbUri");  } }
        private static string AuthKey {  get { return Environment.GetEnvironmentVariable("DocumentDbAuthKey"); } }

        private static DocumentRepository<Fighter> GetFighterRepository()
        {
            return new DocumentRepository<Fighter>(
                new DocumentRepositoryConfiguration(Uri, AuthKey, "mmapidb", "fighters"),
                new ConnectionPolicy { ConnectionMode = ConnectionMode.Gateway },
                new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }
    }
}
