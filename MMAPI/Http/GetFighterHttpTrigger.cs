using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Models;
using System;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using MMAPI.Helpers;


/*
 * https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook
 */

namespace MMAPI.Http
{
    public static class GetFighterHttpTrigger
    {
        [FunctionName("GetFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fighter/{id:int}")]HttpRequestMessage req, int id, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            log.Info($"{id}");

            return req.CreateResponse(HttpStatusCode.OK, new Fighter
            {
                Id = id,
                FirstName = "Connor",
                LastName = "McGreggor",
                Gender = Models.Enumerations.Gender.Female,
                DateOfBirth = DateTimeOffset.UtcNow
            }, JsonHelper.StandardFormatter);
        }
    }
}
