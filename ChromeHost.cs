using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NativeMessaging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using GameSenseNativeMessaging.Enums;
using GameSenseNativeMessaging.Events;
using GameSenseNativeMessaging.NativeMessaging;

namespace GameSenseNativeMessaging
{
    public class ChromeHost : Host
    {
        private const bool SendConfirmationReceipt = false;

        static GameSenseClient Client;

        public override string Hostname
        {
            get { return "com.anewtonlevey.gamesense"; }
        }

        public ChromeHost(GameSenseClient client) : base(SendConfirmationReceipt)
        {
            Client = client;
        }

        protected override void ProcessReceivedMessage(JObject data)
        {
            if(data.Property("type") != null)
            {
                NativeMessageType type = (NativeMessageType)Enum.Parse(typeof(NativeMessageType), data.Property("type").Value.ToString());
                switch (type)
                {
                    case NativeMessageType.RegisterEvent:
                        Client.RegisterEvent(data.ToObject<EventRegistration>());
                        break;
                    case NativeMessageType.UnregisterEvent:
                        Client.UnRegisterEvent(data.ToObject<EventRegistration>());
                        break;
                    case NativeMessageType.SendEvent:
                        Client.SendEvent(data.ToObject<GameEvent>());
                        break;
                    default:
                        SendMessage(JObject.FromObject(new ChromeResponse("Unidentified message type")));
                        break;
                }
            } else
            {
                SendMessage(JObject.FromObject(new ChromeResponse("Unidentified message type")));
            }
        }

       


    }
}
