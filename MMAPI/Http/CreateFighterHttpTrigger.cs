using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Models;

namespace MMAPI.Http
{
    public static class CreateFighterHttpTrigger
    {
        [FunctionName("CreateFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "fighter")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // Get request body
            var newFighter = await req.Content.ReadAsAsync<Fighter>();

            return newFighter == null
                ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid fighter")
                : req.CreateResponse(HttpStatusCode.OK, "Hello " + newFighter.FirstName + " " + newFighter.LastName);
        }
    }
}
