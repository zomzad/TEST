using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text;
using System.Web.Script.Serialization;
using LionTech.Utility;

namespace TEST
{
    class Exec
    {
        public void ExecTestFun()
        {
            var apiParaJsonStr = new JavaScriptSerializer().Serialize(
                new Dictionary<string, object>()
                {
                    { "EDINo", "20191017007296" },
                    { "EDIFlowID", "RWLINKM" }
                });

            var url = @"http://127.0.0.1:6666/EDIService/FlowManagerSelectEvent?ClientSysID=RWAP&UserID=181037&=&FlowPara=" + apiParaJsonStr;

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            request.Method = WebRequestMethods.Http.Get;
            request.KeepAlive = false;
            request.ContentType = "application/json";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse; //取得API回傳結果
            if (response != null)
            {
                Stream responseStream = response.GetResponseStream();
                StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                string srcString = reader.ReadToEnd(); //如果是網頁 可以抓到網頁原始碼
            }
        }
    }
}
