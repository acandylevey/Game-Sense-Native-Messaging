using GameSenseNativeMessaging.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSenseNativeMessaging.Events
{
    public class EventRegistration
    {
        [JsonProperty("game")]
        public string ApplicationName { get; set; }

        [JsonProperty("event")]
        public string EventName { get; set; }

        [JsonProperty("icon_id")]
        public int IconId { get; set; }

        [JsonProperty("min_value", NullValueHandling = NullValueHandling.Ignore)]
        public int? Min { get; set; }

        [JsonProperty("max_value", NullValueHandling = NullValueHandling.Ignore)]
        public int? Max { get; set; }

        public EventRegistration(string applicationName, string eventName, int? min = null, int? max = null)
        {
            ApplicationName = applicationName;
            EventName = eventName.ToUpper();
            IconId = (int)Icon.None;
            Min = min;
            Max = max;
        }
    }
}
