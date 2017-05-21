using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameSenseNativeMessaging.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameSenseNativeMessaging.Events
{
    internal class GameRegistration
    {
        [JsonProperty("game")]
        public string Name { get; set; }

        [JsonProperty("game_display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("icon_color_id")]
        public int ColourId { get; set; }


        public GameRegistration(string name, string displayName, IconColour colour )
        {
            Name = name;
            DisplayName = displayName;
            ColourId = (int)colour;
        }
    }
}
