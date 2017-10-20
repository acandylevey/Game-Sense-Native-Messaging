using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using GameSenseNativeMessaging.Enums;
using GameSenseNativeMessaging.Events;

namespace GameSenseNativeMessaging
{
    public class GameSenseClient
    {
        string Destination = "http://localhost:49682"; //Temp for now, will need to read this from : C:\ProgramData\SteelSeries\SteelSeries Engine 3\coreProps.json later.

        public string Name = "CHROMECONNECT";

        string DisplayName = "Chrome Connect";

        IconColour Colour = IconColour.Yellow;

        public void Register()
        {
            //First we register the application
            var metaData = new GameRegistration(Name, DisplayName, Colour);
            PostData(Destination + "/game_metadata", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(metaData)));
        }

        public void RegisterEvent(EventRegistration gameEvent)
        {
            gameEvent.ApplicationName = Name;
            var jObject = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(gameEvent));
            PostData(Destination + "/register_game_event", jObject);
        }

        public void UnRegister()
        {
            var metaData = new GameRegistration(Name, DisplayName, Colour);
            PostData(Destination + "/remove_game", JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(metaData)));
        }

        public void UnRegisterEvent(EventRegistration gameEvent)
        {
            gameEvent.ApplicationName = Name;
            var jObject = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(gameEvent));
            PostData(Destination + "/remove_game_event", jObject);
        }

        public void SendEvent(GameEvent gameEvent)
        {
            gameEvent.ApplicationName = Name;
            var jObject = JsonConvert.DeserializeObject<JObject>(JsonConvert.SerializeObject(gameEvent));
            PostData(Destination + "/game_event", jObject);
        }


        public void PostData(string url, JObject json)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);

            var postBytes = System.Text.Encoding.UTF8.GetBytes(json.ToString(Formatting.Indented));



            request.Method = "POST";
            request.ContentType = "application/json; charset=UTF-8";
            request.Accept = "application/json";

            request.ContentLength = postBytes.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(postBytes, 0, postBytes.Length);
            }
            try
            {
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            }
            catch (WebException e)
            {
                var responseString = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
            }

        }
    }
}
