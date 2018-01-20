using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using Newtonsoft.Json;
using SampleCode.Models;
using SampleCode.Security;

namespace LineBotZom.Models
{
    public class LineBotChatModel
    {
        public string GetBusInfo(string queryBusStr)
        {
            string busMinResult = string.Empty;
            string apiResult = string.Empty;

            try
            {
                string APPID = "429abe57aad34cc1bc5ba3cc261ada0f";
                string APPKey = "0qs0e0LeozfeFVNR0qRXtqP75Es";
                string[] busInfo = queryBusStr.Split('/');
                List<BusStation> data = new List<BusStation>();

                string xdate = DateTime.Now.ToUniversalTime().ToString("r");
                string signDate = "x-date: " + xdate;
                string signature = HMAC_SHA1.Signature(signDate, APPKey);
                string sAuth = "hmac username=\"" + APPID + "\", algorithm=\"hmac-sha1\", headers=\"x-date\", signature=\"" + signature + "\"";
                var apiUrl = $"http://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/City/Taipei/{busInfo[1]}?$filter=StopName%2FZh_tw%20eq%20'%E5%85%A7%E6%B9%96%E8%A1%8C%E6%94%BF%E5%A4%A7%E6%A8%93'&$top=30&$format=JSON";

                using (HttpClient Client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip }))
                {
                    Client.DefaultRequestHeaders.Add("Authorization", sAuth);
                    Client.DefaultRequestHeaders.Add("x-date", xdate);
                    apiResult = Client.GetStringAsync(apiUrl).Result;
                }

                if (string.IsNullOrWhiteSpace(apiResult) == false)
                {
                    data = JsonConvert.DeserializeObject<List<BusStation>>(apiResult);
                    var ooo = data.Where(b => b.Direction == "0").ToList();

                    busMinResult = ooo.Select((val, idx) => new { Index = idx, Value = val })
                                .Aggregate(busMinResult, (current, row) => current + string.Join(Environment.NewLine, $"第{row.Index + 1}班公車{busInfo[1]}到內湖行政大樓還有{row.Value.EstimateTime / 60}分鐘"));
                }

                return busMinResult;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("取得公車訊息失敗");
            }
        }
    }
}