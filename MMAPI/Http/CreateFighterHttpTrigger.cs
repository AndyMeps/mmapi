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
    [StorageAccount("AzureWebJobsStorage")]
    public static class CreateFighterHttpTrigger
    {
        [FunctionName("CreateFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "fighter")]HttpRequestMessage req,
            [Queue("outputQueue")]IAsyncCollector<Fighter> outputQueueMessage,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            var newFighter = await req.Content.ReadAsAsync<Fighter>();

            if (newFighter != null)
            {
                await outputQueueMessage.AddAsync(newFighter);
                return req.CreateResponse(HttpStatusCode.Accepted, newFighter);
            }

            return req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a valid fighter");
        }
    }
}
