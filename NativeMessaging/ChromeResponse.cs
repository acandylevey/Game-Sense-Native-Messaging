using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameSenseNativeMessaging.NativeMessaging
{
    public class ChromeResponse
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        public ChromeResponse(string msg)
        {
            Message = msg;
        }
            
    }
}
