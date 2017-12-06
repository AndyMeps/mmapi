using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Models;
using MMAPI.Services;
using MMAPI.Services.Interfaces;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MMAPI.Functions.Http
{
    public static class CreateFighterHttpTrigger
    {
        [FunctionName("CreateFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "fighter")]HttpRequestMessage req,
            TraceWriter log)
        {
            var fighter = await req.Content.ReadAsAsync<Fighter>();

            IFighterService fighterService = new FighterService();

            if (await fighterService.ExistsAsync(fighter))
            {
                log.Info($"Ignored fighter that already exists: `{fighter.FirstName} {fighter.LastName} | {fighter.DateOfBirth}`");

                return req.CreateResponse(HttpStatusCode.Conflict);
            }

            var id = await fighterService.CreateAsync(fighter);

            log.Info($"Created a new fighter with Id: `{id}`");

            return req.CreateResponse(new { id = id });            
        }
    }
}
