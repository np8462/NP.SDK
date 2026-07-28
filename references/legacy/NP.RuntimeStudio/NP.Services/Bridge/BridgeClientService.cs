using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using NP.Core.Models;
using System;
using System.Windows.Forms;

namespace NP.Services.Bridge
{
    public class BridgeClientService
    {
        private readonly HttpClient _client;

        public BridgeClientService()
        {
            _client = new HttpClient();
            _client.BaseAddress =
                new Uri("http://localhost:5050/");
        }

        public bool SendContext(AiContext context)
        {
            try
            {
                string json =
                    JsonConvert.SerializeObject(context);

                StringContent content =
                    new StringContent(
                        json,
                        Encoding.UTF8,
                        "application/json");

                HttpResponseMessage response =
                    _client.PostAsync(
                        "bridge/context",
                        content).Result;

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

                return false;
            }
        }
    }
}