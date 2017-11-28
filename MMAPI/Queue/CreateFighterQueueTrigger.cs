using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage.Queue;
using MMAPI.Models;
using Newtonsoft.Json;
// using Microsoft.Azure.Documents.Client;
using System.Threading.Tasks;

namespace MMAPI.Queue
{
    [StorageAccount("AzureWebJobsStorage")]
    public static class CreateFighterQueueTrigger
    {
        [FunctionName("CreateFighterQueueTrigger")]
        public static async Task Run([QueueTrigger("create-fighter")]Fighter myQueueItem, TraceWriter log)
        {
            var documentDbUri = Environment.GetEnvironmentVariable("DocumentDbUri");
            var documentDbAuthKey = Environment.GetEnvironmentVariable("DocumentDbAuthKey");

            log.Info($"Env variables: {documentDbUri}, {documentDbAuthKey}");

            log.Info($"C# Queue trigger function processed: {JsonConvert.SerializeObject(myQueueItem)}");
            /*
            using (var client = new DocumentClient(new Uri(documentDbUri), documentDbAuthKey))
            {
                await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("mmapidb", "fighters"), myQueueItem);
            }           
            */
        }
    }
}
