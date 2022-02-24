using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YosioluwaOsibemekun.Email
{
    public static class StringUtility
    {
        public async static Task<IRestResponse> MakeApiRequest(RestSharp.Method method, string url, string parameter = null, string clientSecret = null,
            bool wallet = false, string authorization = "", bool admin = false)
        {
            var client = new RestClient(url);
            client.Timeout = -1;
            var request = method == Method.POST ? new RestRequest(Method.POST) : new RestRequest(Method.GET);
            request.AddHeader("Content-Type", "application/json");
            if (clientSecret != null)
                request.AddHeader("X-ClientSecret", clientSecret);

            //if (wallet)
            //    request.AddHeader("x-skarpa-client", Constants.SKARPA_WALLET_SECRET);

            //if (admin)
            //    request.AddHeader("x-skarpa-client", Constants.SKARPA_ADMIN_SECRET);

            if (!string.IsNullOrEmpty(authorization))
                request.AddHeader("Authorization", authorization);

            if (method == Method.POST)
                request.AddParameter("application/json", parameter, ParameterType.RequestBody);

            var r = await client.ExecuteAsync(request);
            //Log.Info(r.Content);
            return r;
        }


        public static string SerializeData(object postdata)
        {
            var resp = "";
            string contentType = "application/json";
            switch (contentType)
            {
                case "application/json":
                    resp = JsonConvert.SerializeObject(postdata);
                    break;
                case "application/x-www-form-urlencoded":
                    var properties = from p in postdata.GetType().GetProperties()
                                     where p.GetValue(postdata, null) != null
                                     select p.Name + "=" + p.GetValue(postdata, null).ToString();

                    resp = String.Join("&", properties.ToArray());
                    break;
            }
            return resp;
        }
    }
}
