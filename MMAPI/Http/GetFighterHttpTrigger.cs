using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Helpers;
using MMAPI.Models;
using MMAPI.Models.Enumerations;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


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
            log.Info($"Requesting fighter with Id: {id}");

            return await Task.Run(() =>
            {
                return req.CreateResponse(HttpStatusCode.OK, new Fighter
                {
                    Id = id,
                    FirstName = "Connor",
                    LastName = "McGreggor",
                    Gender = Gender.Female,
                    DateOfBirth = DateTimeOffset.UtcNow
                }, JsonHelper.StandardFormatter);
            });
        }
    }
}
