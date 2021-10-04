using System;
using System.Threading.Tasks;
using EventHubAppFunction.Domain;

namespace EventHubAppFunction.Application
{
    public interface IEventProcessor
    {
        Task Process(CustomEvent customEvent);
    }
}
