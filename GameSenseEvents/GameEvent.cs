using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameSenseNativeMessaging.Events
{
    public class GameEvent
    {
        [JsonProperty("game")]
        public string ApplicationName { get; set; }

        [JsonProperty("event")]
        public string EventName { get; set; }

        [JsonProperty("data")]
        public JObject Data { get; set; }

        public GameEvent(string applicationName, string eventName, JObject data)
        {
            ApplicationName = applicationName;
            EventName = eventName.ToUpper();
            Data = data;
        }
    }
}
