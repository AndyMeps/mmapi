using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using MMAPI.Helpers;
using MMAPI.Models;
using MMAPI.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;


/*
 * https://docs.microsoft.com/en-us/azure/azure-functions/functions-bindings-http-webhook
 * https://docs.microsoft.com/en-us/aspnet/web-api/overview/web-api-routing-and-actions/attribute-routing-in-web-api-2#route-constraints
 */

namespace MMAPI.Http
{
    public static class GetFighterHttpTrigger
    {
        [FunctionName("GetFighterHttpTrigger")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "fighter/{id:guid}")]HttpRequestMessage req, string id, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");
            log.Info($"Requesting fighter with Id: {id}");

            return await Task.Run(() =>
            {
                return req.CreateResponse(HttpStatusCode.OK, new Fighter
                {
                    Id = new Guid(id),
                    FirstName = "Connor",
                    LastName = "McGreggor",
                    Nickname = "The Notorious",
                    Gender = Gender.Male,
                    DateOfBirth = new DateTimeOffset(new DateTime(1988, 7, 14, 0, 0, 0, DateTimeKind.Utc)),
                    Height = 69,
                    Reach = 25,
                    WeightClasses = new List<WeightSummary>
                    {
                        new WeightSummary { Id = Guid.NewGuid(), Name = "Featherweight" },
                        new WeightSummary { Id = Guid.NewGuid(), Name = "Lightweight" },
                        new WeightSummary { Id = Guid.NewGuid(), Name = "Welterweight" }
                    },
                    Record = new FighterRecord
                    {
                        Wins = 21,
                        Losses = 3
                    }
                }, JsonHelper.StandardFormatter);
            });
        }
    }
}
