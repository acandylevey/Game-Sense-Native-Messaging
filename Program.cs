using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using NativeMessaging;
using System.Linq;


namespace GameSenseNativeMessaging
{
    //TODO: REWRITE THIS AND SEE IF WE CAN CHECK THE USERNAME / DOMAIN
    //IF: The extension detects a JDLF username, then set cookie (Logged in to JDLF account = true)
    //IF: Disconnect happens or 
    class Program
    {
        static public string AssemblyLoadDirectory
        {
            get
            {
                string codeBase = Assembly.GetEntryAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

		static public string AssemblyExecuteablePath
		{
			get
			{
				string codeBase = Assembly.GetEntryAssembly().CodeBase;
				UriBuilder uri = new UriBuilder(codeBase);
				return Uri.UnescapeDataString(uri.Path);
			}
		}

        static Host Host;
        static GameSenseClient Client;


        static string[] AllowedOrigins = new string[] { "chrome-extension://knldjmfmopnpolahpmmgbagdohdnhkik/" };

        static string Description = "Connects Chrome to the Steel Series GameSense Enginge, allowing websites to control hardware.";
        static void Main(string[] args)
        {
            Client = new GameSenseClient();
            Host = new ChromeHost(Client);

            if (args.Contains("--register"))
            {
                Host.GenerateManifest(Description, AllowedOrigins);
                Host.Register();
                Client.Register();

            } else if(args.Contains("--unregister"))
            {
                Host.UnRegister();
                Client.UnRegister();
            } else
            {
                Host.Listen();
            }
        }
    }
}
