using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace OrderAggregator
{
    public static class OrderAggregatorFunction
    {
        [FunctionName("OrderAggregatorFunction")]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("OrderAggregatorFunction_Hello", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("OrderAggregatorFunction_Hello", "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>("OrderAggregatorFunction_Hello", "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }

        [FunctionName("OrderAggregatorFunction_Hello")]
        public static string SayHello([ActivityTrigger] string name, ILogger log)
        {
            log.LogInformation($"Saying hello to {name}.");
            return $"Hello {name}!";
        }

        [FunctionName("OrderAggregatorFunction_HttpStart")]
        public static async Task<HttpResponseMessage> HttpStart(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log)
        {
            // Function input comes from the request content.
            string instanceId = await starter.StartNewAsync("OrderAggregatorFunction", null);


            //SourceFiles.Add(new KeyValuePair<string, KeyValuePair<string, string>>("testKey"), )

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");

            return starter.CreateCheckStatusResponse(req, instanceId);
        }



        [FunctionName("HttpAccumulationTest")]
        public static async Task HttpAccumulate(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestMessage req,
            [DurableClient] IDurableEntityClient entityClient,
            ILogger log)
        {
            // The "Counter/{metricType}" entity is created on-demand.
            //TODO: Check EventGrid filename
            //TODO: thisshouldbeDatePrefix
            //TODO: Event JSON Body
            //TODO: SignalEntityAsync should have documenttype
            //TODO: Change OrderHead
            var datePrefixEntityId = new EntityId("OrderAccumulator", "thisshouldbeDatePrefix");

            await entityClient.SignalEntityAsync(datePrefixEntityId, DocType.OrderHead.ToString(), "Event JSON Body");

        }

        [FunctionName("OrderAccumulator")]
        public static void OrderAccumulator([EntityTrigger] IDurableEntityContext ctx)
        {
            var order = ctx.GetState<OrderEntity>() ?? new OrderEntity();

            var operationName = Enum.Parse(typeof(DocType), ctx.OperationName);

            switch (operationName)
            {
                case DocType.OrderHead:
                    order.OrderHeader = DateTime.Now.ToString();
                    ctx.SetState(order);
                    break;
            }
        }
    }
}