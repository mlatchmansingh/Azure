using System;
using Newtonsoft.Json;

namespace EventHubAppFunction.Domain
{
    public class Header
    {
        [JsonProperty("eventType")]
        public string EventType { get; set; }
    }

    public class CustomEvent : Header
    {
        [JsonProperty("data1")]
        public string Data1 { get; set; }
        [JsonProperty("data2")]
        public string Data2 { get; set; }
    }
}
