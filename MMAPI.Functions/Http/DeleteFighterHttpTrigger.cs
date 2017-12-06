using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Services.Interfaces;
using MMAPI.Services;
using System;

namespace MMAPI.Functions.Http
{
    public static class DeleteFighterHttpTrigger
    {
        [FunctionName("DeleteFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "fighter/{id:guid}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            log.Info($"Requesting fighter with Id: {id}");

            IFighterService fighterService = new FighterService();

            try
            {
                await fighterService.DeleteAsync(id);
                return req.CreateResponse(HttpStatusCode.NoContent);
            }
            catch(Exception ex)
            {
                log.Error($"Unable to delete resource with id: `{id}`", ex);
                return req.ServerError();
            }
        }
    }
}
