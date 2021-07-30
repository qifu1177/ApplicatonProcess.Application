using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Data
{
    public class HTTPDataAccess : Singleton<HTTPDataAccess>
    {
        public async Task<List<string>> LoadAssetNames(string url)
        {
            List<string> list = new List<string>();
            string response = await this.GetResponse(url);
            dynamic data = JsonConvert.DeserializeObject<ExpandoObject>(response, new ExpandoObjectConverter());
            //dynamic data = JObject.Parse(response);
            if(data.data!=null && data.data.Count>0)
            {
                foreach(dynamic item in data.data)
                {
                    if (!list.Contains(item.name))
                        list.Add(item.name);
                }
            }
            return list;
        }

        private async Task<string> GetResponse(string url)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync(url);
            response = response.EnsureSuccessStatusCode();
            string responseMessage = await response.Content.ReadAsStringAsync();
            return responseMessage;
        }
    }
}
