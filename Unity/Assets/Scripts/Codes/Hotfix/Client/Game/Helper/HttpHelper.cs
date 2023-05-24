using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using MongoDB.Bson;

namespace ET.Client
{
    public static class HttpHelper
    {
        public static async ETTask<string> SendPost(string url, Dictionary<string, string> body)
        {
            string json = body.ToJson();
            HttpContent data = new StringContent(json, Encoding.UTF8, "application/json");
            using HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync(url, data);
            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }

        public static async ETTask<string> SendGet(string url, Dictionary<string, string> body)
        {
            string param = body.Aggregate("",
                (current, item) => current + $"{item.Key}={item.Value}&");
            using HttpClient client = new HttpClient();
            StringBuilder urlSb = new StringBuilder(url);
            HttpResponseMessage response = await client.GetAsync(urlSb.Append(param.TrimEnd('&')).ToString());
            string result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
        
    }
}