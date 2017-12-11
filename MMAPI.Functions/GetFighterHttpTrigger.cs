using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Models;
using MMAPI.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


/*
 * https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook
 * https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#route-constraints
 */

namespace MMAPI.Functions.Http
{
    public static class GetFighterHttpTrigger
    {
        [FunctionName("GetFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fighter/{id:guid}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            log.Info($"Requesting fighter with Id: {id}");

            var fighterService = new FighterService();

            var result = await fighterService.FindByIdAsync(new Guid(id));

            if (result == null) req.CreateErrorResponse(HttpStatusCode.NotFound, "Fighter not found.");

            return req.CreateResponse(HttpStatusCode.OK, result);
        }
    }
}
