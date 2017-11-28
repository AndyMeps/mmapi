using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Models;
using Newtonsoft.Json;

namespace MMAPI.Http
{
    [StorageAccount("AzureWebJobsStorage")]
    public static class CreateFighterHttpTrigger
    {
        [FunctionName("CreateFighterHttpTrigger")]
        [return: Queue("create-fighter")]
        public static Fighter Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "fighter")]Fighter fighter,
            TraceWriter log)
        {
            log.Info($"Received a new fighter: {JsonConvert.SerializeObject(fighter)}");

            return fighter;
        }
    }
}
