using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace EventHubAppFunction.Application
{
    public class EventHubAppFunction
    {
        private readonly ILogger logger;
        private readonly IEventProcessor eventProcessor;

        public EventHubAppFunction(
            ILogger<EventHubAppFunction> l,
            IEventProcessor p)
        {
            logger = l ?? throw new ArgumentNullException(nameof(l));
            eventProcessor = p ?? throw new ArgumentNullException(nameof(p));
        }



        [FunctionName("EventHubAppFunction")]
        public async Task Run([EventHubTrigger("sample-event-hub", Connection = "EventHubConnectionString")] EventData[] events, ILogger log)
        {
            var exceptions = new List<Exception>();

            logger.LogInformation("EventHubAppFunction - Run function invoked");
            foreach (EventData eventData in events)
            {
                try
                {
                    //
                    // you can check the message properties here for something specific
                    // to help identify this message as something that can be processed by this function
                    // for example
                    if (eventData.SystemProperties["my-custom-property"]?.ToString().Contains("my-expected-value") == false)
                        continue;

                    //
                    // Assuming this event is interesting to this function
                    string messageId = eventData.SystemProperties["my-message-id"].ToString();
                    string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);


                    // Replace these two lines with your processing logic.
                    log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");
                    await Task.Yield();
                }
                catch (Exception e)
                {
                    // We need to keep processing the rest of the batch - capture this exception and continue.
                    // Also, consider capturing details of the message that failed processing so it can be processed again later.
                    logger.LogError(e, "An exception occurred");
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }
    }
}
