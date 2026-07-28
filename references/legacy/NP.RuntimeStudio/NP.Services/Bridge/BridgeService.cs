using System;
using System.IO;
using Newtonsoft.Json;
using NP.Core.Models;

namespace NP.Services.Bridge
{
    public class BridgeService
    {
        private string GetBridgeFile()
        {
            return Path.Combine(
                Path.GetTempPath(),
                "NP_BridgeRequest.json");
        }

        public BridgeRequest Load()
        {
            try
            {
                string file = GetBridgeFile();

                if (!File.Exists(file))
                    return null;

                string json =
                    File.ReadAllText(file);

                return JsonConvert
                    .DeserializeObject<BridgeRequest>(json);
            }
            catch
            {
                return null;
            }
        }
    }
}