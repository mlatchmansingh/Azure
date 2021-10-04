using System;
using System.Threading.Tasks;
using EventHubAppFunction.Application;
using EventHubAppFunction.Domain;

namespace EventHubAppFunction.UI.Services
{
    public class EventProcessor : IEventProcessor
    {
        public EventProcessor()
        {
        }

        public Task Process(CustomEvent customEvent)
        {
            //
            // this is where you can process the event from the event hub
            switch (customEvent.EventType.ToLower())
            {
                case "newevent":
                    break;

                case "deleteevent":
                    break;
            }

            return Task.FromResult<int>(0);
        }
    }
}
