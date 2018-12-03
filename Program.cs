using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Caching;
using System.Web.Script.Serialization;
using ConsoleApplication1;
using LionTech.APIService.Message;
using LionTech.APIService.SMS;
using LionTech.Utility;
using LionTech.Utility.Sockets;
using System.Web.Mvc;
using LionTech.APIService.AppService.LionTravel;
using Newtonsoft.Json;
using SampleCode.Models;
using SampleCode.Security;

namespace TEST
{
    internal class Program
    {
        #region - Definition -
        public class UserData
        {
            public string UserID { get; set; }
        }

        public enum EnumDomainType
        {
            [Description("LDAP://lionmail.com")]
            LionMail,
            [Description("LDAP://liontech.com.tw")]
            LionTech
        }

        public enum EnumUserJob
        {
            SGM = 10,
            PERSINNEL = 11,
            COMPUTER = 14
        }

        #region - 繼承測試 -
        private class ObjectA
        {
            protected ObjectA(string str)
            {
                Console.WriteLine("A 建構子" + str);
            }
        }

        private class ObjectB : ObjectA
        {
            public ObjectB() : this("One")
            {
                Console.WriteLine("B無參數建構子");
            }

            private ObjectB(string aa) : base("gg")
            {
                Console.WriteLine("B 建構子" + aa);
            }
        }
        #endregion

        public class UserInfo
        {
            public string UserID { get; set; }
            public string UserNM { get; set; }
        }
        #endregion

        #region - Property -
        public delegate int MyDelegate(int a, int b);

        //存取子測試
        public static string testStr
        {
            get
            {
                return name == "姓名:方道筌" ? name : "AAA";
            }
            set
            {
                name = $"姓名:{value}";
            }
        }

        public static List<string> CountryList { get; set; }
        #endregion

        #region - Private -
        //存取子測試用
        private static string name = string.Empty;
        #endregion

        private class Country
        {
            public string CountryName { get; set; }
            public string CountryId { get; set; }
        }

        private static void Main(string[] args)
        {
            //Exec run = new Exec();
            //run.ExecTestFun();

            #region - B2C推播測試 -

            #region - 推播 -
            B2CAppMessage message = new B2CAppMessage();
            // 資料類型
            /*
              OrderPayment 訂單付款
              TicketInvoicingSuccess 機票開票成功
              BookingVoucherSuccess 住宿券開票成功
              GroupNotification 成團通知
              CustomerServiceReply 客服回覆
            */
            //message.DataType = EnumDataType.BookingVoucherSuccess;
            //message.UserList = new List<string> // 推播發送對象
            //{
            //    "69d40c4f-25c8-4a1e-94a7-38c23b1b2d6f" // 會員代碼
            //};

            //// 指定訊息推播時間2016/12/29 10:50 PM，若不指定推播時間，則無需此行程式。推播訊息會立即發送
            //message.AddPushDateTime(new DateTime(2018, 11, 26, 15, 03, 0));
            //message.Title = "機票開票成功";// 推播標題
            //message.Body = "2018/11/26 12:00 機票開票z成功";// 推播內容

            //B2CAppClient client = B2CAppClient.Create();
            //client.ClientSysID = "ERPAP";// 推播發送平台
            //client.ClientUserID = "008605";// 推播發送人員
            //var response = client.PushMessage(message);
            #endregion

            #region - 推播主題 -
            //B2CAppTopicMessage message = new B2CAppTopicMessage();
            //// 指定訊息推播時間2018/12/29 10:50 PM，若不指定推播時間，則無需此行程式。推播訊息會立即發送
            //message.AddPushDateTime(new DateTime(2018, 11, 26, 14, 57, 0));
            //message.Title = "澎湖福朋喜來登2日只要$1,999起";// 推播標題
            //message.Body = "出發日期：即日起~2019/2/25止(台北/台中/高雄出發)";// 推播內容

            //B2CAppClient client = B2CAppClient.Create();
            //client.ClientSysID = "ERPAP";// 推播發送平台
            //client.ClientUserID = "008605";// 推播發送人員
            //PushTopicMessageResponse response = client.PushMessageByAll(message);
            #endregion

            #region - 取消推播 -
            //Guid messageID = new Guid("73680267-e6bf-46d7-b3e4-6b165a958608"); // 推播訊息時，回傳messageID;
            //B2CAppClient client = B2CAppClient.Create();
            //client.ClientSysID = "ERPAP";// 取消推播平台;
            //client.ClientUserID = "008605";// 取消推播人員;
            //bool result = client.CancelPushMessage(messageID); // 取消推播
            #endregion

            #region - 取消主題推播 -
            //Guid messageID = new Guid("C725DD04-BAD8-471B-9CBB-4619E32569D1"); // 推播訊息時，回傳messageID;
            //B2CAppClient client = B2CAppClient.Create();
            //client.ClientSysID = "ERPAP";// 取消推播平台
            //client.ClientUserID = "008605";// 取消推播人員
            //bool result = client.CancelPushTopicMessage(messageID); // 取消主題推播
            #endregion

            #endregion

            #region - API測試 -

            #region - B2C推播訊息已讀測試 -
            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/ReadMessage?ClientUserID=00D223&ClientSysID=ERPAP";
            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "MessageID", "0BC77DC0-A891-4109-8610-693419EFA583" },
            //        { "UserID", "00D223"}
            //    });

            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/ReadMessage?ClientUserID=00D223&ClientSysID=ERPAP";
            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "MessageID", "C8FDDB55-4B3A-46FA-851A-0C3141353038" },
            //        { "UserID", "69d40c4f-25c8-4a1e-94a7-38c23b1b2d6f"}
            //    });
            #endregion

            #region - B2C登出測試 -
            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/Logout?ClientUserID=00D223&ClientSysID=ERPAP";
            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/Logout?ClientUserID=00D223&ClientSysID=ERPAP";
            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "UUID", "5956DED2-249B-4424-A48F-000B8834C8BA" }
            //    });
            #endregion

            #region - B2C取得推播紀錄測試 -
            ////20171025021015999, 20160225021015000
            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/LogPushMessage?ClientUserID=00D223&ClientSysID=ERPAP" +
            //          "&UserID=69d40c4f-25c8-4a1e-94a7-38c23b1b2d6f" +
            //          "&StartDateTime=20181203091111222" +
            //"&EndDateTime=20181203091111222";

            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/LogPushMessage?ClientSysID=ERPAP" +
            //          "&UserID=69d40c4f-25c8-4a1e-94a7-38c23b1b2d6f" +
            //          "& StartDateTime=20181203091111222" +
            //          "& EndDateTime=20181203091111222";
            #endregion

            #region - B2C登入測試 -
            //00D223裝置資料
            /*
                    { "UUID", "22495241-7342-4A33-905B-1A65E6498CD6" },
                    { "AppID", "B2CAPP" },
                    { "UserID", "69d40c4f-25c8-4a1e-94a7-38c23b1b2d6f" },
                    { "DeviceToken", "ebMsFq3EYQo:APA91bGr6du3CVVj8-4JmnhfLzMzvsUUV48Z0Eo4SUB3Wb5CK8QVKz7IiUkbQW7FgVnsUSSa7i9YzpbifmBq9qd24TnFrV7rvH-EyC2NtH_i4n0lsBvRXeSW6kI9Sd6b0hQTi8kO94Om" },
                    { "DeviceTokenType", "Firebase" },
                    { "OS", "Android" },
                    { "MobileType", "U Ultra" },
                    { "IsOpenPush", "Y" }
             */


            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/Login?ClientUserID=00D223&ClientSysID=ERPAP";
            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/Login?ClientUserID=00D223&ClientSysID=ERPAP";
            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        //C2ECB70B-F50B-4D5D-A81A-0012B610D6DC
            //        //A4EBC9F4 -28BD-4AFF-A8DC-F01F53A440A3
            //        //7CE1BF60-808F-4EE3-AFF7-14E1F7FD3E5F
            //        //C2ECB70B-F50B-4D5D-A81A-0012B610D6DC
            //        //9B494ADB-7342-4A33-905B-1A65E6498CD6
            //        { "UUID", "6056DED2-249B-4424-A48F-000B8834C8BA" },
            //        { "AppID", "B2CAPP" },
            //        { "UserID", "123457" },
            //        { "DeviceToken", "35168" },
            //        { "DeviceTokenType", "abc" },
            //        { "OS", "windows" },
            //        { "MobileType", "android" },
            //        { "IsOpenPush", "Y" }
            //    });
            #endregion

            #region - B2C開啟推播測試 -
            //開啟推播測試
            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/OpenPush?ClientUserID=00D223&ClientSysID=ERPAP";
            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/OpenPush?ClientUserID=00D223&ClientSysID=ERPAP";
            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "UUID", "22495241-7342-4A33-905B-1A65E6498CD6" },
            //        { "IsOpenPush", "Y" },
            //        { "DeviceToken", "ebMsFq3EYQo:APA91bGr6du3CVVj8-4JmnhfLzMzvsUUV48Z0Eo4SUB3Wb5CK8QVKz7IiUkbQW7FgVnsUSSa7i9YzpbifmBq9qd24TnFrV7rvH-EyC2NtH_i4n0lsBvRXeSW6kI9Sd6b0hQTi8kO94Om" },
            //        //eePmFsFHTcs:APA91bHVyn6lL4uWK6hGKowZz55kc7UA-HhU_XuC2Ik3_3dMpORxMsJH0g3idWACBj2BAc-MLpzbFshcnS7jG4Z2167xM5wB9jLFPs3CXA8YibFRjuWLIuEJYknUzQPtLZG0gkH2KawH
            //        //dD5rSf4sjk4:APA91bEeOyyCx7bzkD_NzgOFi3cZJmU5-iVbLZnb4uVMPOl0hqvGs4LXLlz5vp2SRffAPm1Q1fnY0h6BXZilF4u2g8iA0fSc2yh2g5GVs8IenqXPm_knozaFj_aI1Rd__pBs9LW3oj7n
            //    });
            #endregion

            #region - B2C推播訊息測試 -
            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/PushMessage?ClientUserID=00D223&ClientSysID=ERPAP";
            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/PushMessage?ClientUserID=00D223&ClientSysID=ERPAP";

            //var data = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //                {"SourceID","ee47afe8-f81b-4cfb-8988-b670a2710727"},
            //                { "SourceType","Metting"}
            //    });

            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "Body", "測試推播內容" },
            //        { "Title", "測試推播標題" },
            //        { "DataType", "OrderPayment" },
            //        //{ "PushDateTime", Common.GetDateTimeString(DateTime.Now) },
            //        { "PushDateTime", "20181122112600123" },
            //        {
            //            "UserList", new List<string>()
            //            {
            //                "00D223", "000101", "00zzzz"
            //            }
            //        },
            //        {
            //            "Data",
            //            new JavaScriptSerializer().Serialize(new Dictionary<string, object>
            //            {
            //                { "data", "testdata" }
            //            })
            //        }
            //    });
            #endregion

            #region - B2C推播主題訊息測試 -
            //////var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/PushTopicMessage?ClientUserID=00D223&ClientSysID=ERPAP";
            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/PushTopicMessage?ClientUserID=00D223&ClientSysID=ERPAP";

            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "Topic", "B2CAPP_ALL" },
            //        { "Title", "測試主題推播" },
            //        { "Body", "主題的推播" },
            //        { "PushDateTime", "20181122115500123" }, //Common.GetDateTimeString(DateTime.Now)
            //        {
            //            "UserList", new List<string> { "00D223", "000101", "00zzzz" }
            //        },
            //        {
            //            "Data",
            //            new JavaScriptSerializer().Serialize(new Dictionary<string, object>
            //            {
            //                { "data", "testdata" }
            //            })
            //        }
            //    });
            #endregion

            #region - B2C取消TOPIC推播測試 -
            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/CancelPushTopicMessage?ClientUserID=00D223&ClientSysID=ERPAP&MessageID=176E5218-42AF-4B8E-BBE4-87E0364C60B6";
            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/CancelPushTopicMessage?ClientUserID=00D223&ClientSysID=ERPAP";

            //var userIDList = new List<string> { "00D223" };

            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "UserIDList", userIDList },
            //        { "MessageID" , "81C86680-7D0C-4816-B151-1571E8BCD216"},
            //        { "ClientUserID","00D223"}
            //    });
            #endregion

            #region - B2C取消推播測試 -
            //model.UserIDList = new List<string> { "00D223", "00D470" };

            //var url = "http://127.0.0.1:6734/v1/LionTravelB2CApp/CancelPushMessage?ClientUserID=00D223&ClientSysID=ERPAP&MessageID=C59F7E6D-6200-4C57-8580-954BCAA270A9";
            //var url = "http://upush.inapi.liontravel.com.tw/v1/LionTravelB2CApp/CancelPushMessage?ClientUserID=00D223&ClientSysID=ERPAP";

            //var userIDList = new List<string> { "00D223" };

            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "UserIDList", userIDList },
            //        { "MessageID" , "C8FDDB55-4B3A-46FA-851A-0C3141353038"},
            //        { "ClientUserID","00D223"}
            //    });
            #endregion

            #region - 事件訂閱API -

            #region - ERPAPIscpm00 -
            //SELECT TOP 5 * FROM RAW_CM_ORG_COM
            //WHERE COM_ID = '!!'

            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "comp_comp", "!!" },
            //        { "comp_sts", "Y"},
            //        { "comp_bu","J"},
            //        { "comp_dname","測試公司"},
            //        { "comp_order","999"},
            //        { "comp_country","YY"},
            //        { "comp_ps","N"}
            //    });

            //var url = "http://127.0.0.1:6666/Subscriber/ERPAPIscpm00EditEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            //var url = "http://127.0.0.1:6666/Subscriber/ERPAPIscpm00DeleteEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            #endregion

            #region - ERPAPOptbm00 -
            //SELECT TOP 5 * FROM RAW_CM_COUNTRY
            //WHERE COUNTRY_ID = 'XX'

            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "cnty_country", "XX" },
            //        { "cnty_sts", "N" },
            //        { "cnty_dname", "測試國家" },
            //        { "cnty_cname", "測試國家中文" },
            //        { "cnty_ename", "TESTEN" }
            //    });

            //var url = "http://127.0.0.1:6666/Subscriber/ERPAPOptbm00EditEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            //var url = "http://127.0.0.1:6666/Subscriber/ERPAPOptbm00DeleteEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            #endregion

            #region - ERPAPPsppm00 -
            //SELECT TOP 5 * FROM RAW_CM_USER_DETAIL
            //WHERE USER_ID = '00AAAA'

            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "pp00_stfn", "00AAAA" },
            //        { "pp00_idno", "A123456898" },
            //        { "pp00_bdate", "19870101" },
            //        { "pp00_scmp", "T" }
            //    });

            ////var url = "http://127.0.0.1:6666/Subscriber/ERPAPPsppm00EditEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            //var url = "http://127.0.0.1:6666/Subscriber/ERPAPPsppm00DeleteEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            #endregion

            #region - ERPAPOpagm20 -
            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "stfn_stfn", "00ZZZZ" },
            //        { "stfn_cname", "TESTMAN" },
            //        { "stfn_prof", "87" },
            //        { "stfn_sts", "N" },
            //        { "stfn_comp", "T" },
            //        { "stfn_team", "S990" },
            //        { "stfn_job1", "14" },
            //        { "stfn_job2", "27" },
            //        { "stfn_email", "zomzad@gmail.com" },
            //    });

            //var url = "http://127.0.0.1:6666/Subscriber/ERPAPOpagm20EditEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            ////var url = "http://127.0.0.1:6666/Subscriber/ERPAPPsppm00DeleteEvent?ClientUserID=00D223&ClientSysID=ERPAP&EventPara=" + apiParaJsonStr;
            #endregion

            #endregion

            #region - Authorization -

            #region - ERPUserAccount & ERPUserRoleReset -
            //var apiParaJsonStr = new JavaScriptSerializer().Serialize(
            //    new Dictionary<string, object>()
            //    {
            //        { "UserID", "00ZZZZ" },
            //        { "UserNM", "TESTMAN" },
            //        { "IsLeft", "N" },
            //        { "JoinDate", "20181010" },
            //        { "UserOrgArea", "A001" },//區域
            //        { "UserOrgBIZTitle", "O030" },//業務職稱
            //        { "UserOrgDept", "D582" },//部門
            //        { "UserOrgGroup", "A" },//事業群
            //        { "UserOrgJobTitle", "T075" },//職稱
            //        { "UserOrgLevel", "020" },//職等
            //        { "UserOrgPlace", "B029" },//事業處
            //        { "UserOrgTeam", "S999" },//組別
            //        { "UserOrgTitle", "G010" },//ERP-職稱
            //        { "UserOrgWorkCom", "T" },//公司編號
            //        { "UserComID", "T" },//公司編號
            //        { "UserSalaryComID", "T" },//薪資公司編號
            //        { "UserTeamID", "S999" },//ERP-組別
            //        { "UserTitleID", "27" },//ERP-職稱
            //        { "UserUnitID", "87" },//單位編號
            //        { "UserWorkID", "14" }//ERP-工作
            //    });

            //var url = "http://127.0.0.1:6666/Authorization/ERPUserAccountCreateEvent?ClientUserID=00D223&ClientSysID=ERPAP&APIPara=" + apiParaJsonStr;
            //var url = "http://127.0.0.1:6666/Authorization/ERPUserRoleResetEvent?ClientUserID=00D223&ClientSysID=ERPAP&APIPara=" + apiParaJsonStr;
            #endregion

            #endregion

            #region - POST -
            //WebClient client = new WebClient { Encoding = Encoding.UTF8 };
            //client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            //var responseWebClient = client.UploadData(url, "POST", Encoding.UTF8.GetBytes(apiParaJsonStr));
            //var apiResult = Encoding.UTF8.GetString(responseWebClient);
            #endregion

            #region - GET -
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            //request.Method = WebRequestMethods.Http.Get;
            //request.KeepAlive = false;
            //request.ContentType = "application/json";
            //HttpWebResponse response = request.GetResponse() as HttpWebResponse; //取得API回傳結果
            //if (response != null)
            //{
            //    Stream responseStream = response.GetResponseStream();
            //    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            //    string srcString = reader.ReadToEnd(); //如果是網頁 可以抓到網頁原始碼
            //}
            #endregion

            #endregion

            #region - LDAP -
            //string domainNM = string.Empty;
            //string ldapPath = "LionMail";
            //string propFieldName = "mail";
            //string DomainAccount = @"lionmail\hjiunliau";
            //string DomainPWD = "D223d223";
            //string DomainGroupNM = "HCM_HCMAP組";

            //EnumDomainType domainType = (from s in Enum.GetNames(typeof(EnumDomainType))
            //                             let type = (EnumDomainType)Enum.Parse(typeof(EnumDomainType), s)
            //                             let path = Common.GetEnumDesc(type)
            //                             where ldapPath.ToLower().IndexOf(path.ToLower(), StringComparison.Ordinal) > -1
            //                             select type).SingleOrDefault();
            //domainNM = domainType.ToString();

            //DirectoryEntry directoryEntry = new DirectoryEntry(ldapPath);

            //DirectorySearcher searcher = new DirectorySearcher(directoryEntry);
            //searcher.SearchScope = SearchScope.Subtree;

            //if (domainType == EnumDomainType.LionTech &&
            //    string.IsNullOrWhiteSpace(DomainPWD) == false)
            //{
            //    propFieldName = "samaccountname";
            //    directoryEntry.Username = DomainAccount.Split('@')[0];
            //    directoryEntry.Password = Security.Decrypt(DomainPWD);
            //}

            //searcher.Filter = $"(&(objectCategory=group)(CN={DomainGroupNM}))";
            //searcher.PropertiesToLoad.Add("samaccountname");
            //searcher.PropertiesToLoad.Add("name");
            //searcher.PropertiesToLoad.Add("mail");
            //SearchResult searchResult = searcher.FindOne();

            //if (searchResult != null)
            //{
            //    searcher.Filter = $"(&(objectCategory=person)(memberOf={searchResult.Path.Substring(searchResult.Path.LastIndexOf("/", StringComparison.Ordinal) + 1)}))";

            //    var searchResultCollection = searcher.FindAll();
            //}
            #endregion

            #region - 委派 -
            MyDelegate myDelegate = MethodA;
            MyDelegate myDelegate2;
            int a = myDelegate(2, 1); //MethodA已事先宣告

            //或用匿名方法
            myDelegate2 = (x, y) => x * y; //(x,y)表示傳入的兩個參數,=>後面是回傳值
            var ass = myDelegate2(2, 5);
            #endregion

            #region - 各種換行 -
            //Model
            //errMsgList.AddRange(ModifyProgramInfoList
            //    .Where(n => string.IsNullOrWhiteSpace(n.Lmm00Aspid) == false &&
            //                string.IsNullOrWhiteSpace(n.Lmm00Add) &&
            //                (string.IsNullOrWhiteSpace(n.Lmm00Title) || string.IsNullOrWhiteSpace(n.Lmm00Desc)))
            //    .Select(n => string.Format(PubSysRequiredFormModifyProgram.SystemMsg_TheItem, int.Parse(n.Order) + 1, $"{PubSysRequiredFormModifyProgram.SystemMsg_Hint_Required}<br/>{PubSysRequiredFormModifyProgram.SystemMsg_Desc_Required}")));

            //return string.Join("<br/>", errMsgList);
            #endregion

            #region - List相關處理 -
            //移除符合條件的項目
            //var userList = new List<UserInfo>
            //{
            //    new UserInfo
            //    {
            //        UserID = "A",
            //        UserNM = "AName"
            //    },
            //    new UserInfo
            //    {
            //        UserID = "B",
            //        UserNM = "BName"
            //    },
            //    new UserInfo
            //    {
            //        UserID = "C",
            //        UserNM = "CName"
            //    }
            //};

            //userList.RemoveAll(e => e.UserID.Equals("B"));
            //var resultList = userList;
            #endregion

            #region - 匿名型別取值 -
            //List<UserInfo> userInfoList = new List<UserInfo>
            //{
            //    new UserInfo
            //    {
            //        UserID = "00D223",
            //        UserNM = "LIAU"
            //    },
            //    new UserInfo
            //    {
            //        UserID = "00D123",
            //        UserNM = "REX"
            //    }
            //};

            ////取得class成員
            //Type myTypeA = typeof(UserInfo);

            ////取得class內欄位 成員是{get set} 才抓的到
            ////不是屬性的話 用DeclaredFields 檔案下方有寫
            //var userInfoProperty = myTypeA.GetProperties(); 

            ////正常class取值
            //foreach (var item in userInfoList)
            //{
            //    Type type = item.GetType();
            //    PropertyInfo[] propertyInfos = type.GetProperties();
            //    foreach (var p in propertyInfos)
            //    {
            //        var val = p.GetValue(item, null);
            //    }
            //}

            ////匿名List
            //var anonymousListA = userInfoList.Select(n => new
            //{
            //    n.UserID,
            //    n.UserNM
            //});

            //List<object> anonymousListB = new List<object>
            //{
            //    new
            //    {
            //        people = "ABC",
            //        NM = "A"
            //    },
            //    new
            //    {
            //        people = "DDD",
            //        NM = "X"
            //    }
            //};

            ////匿名List取值
            //var aList = (from s in anonymousListA
            //             let property = TypeDescriptor.GetProperties(s)
            //             let value = property.Find("UserID", true).GetValue(s)
            //             where value != null
            //             select value.ToString()).ToList();

            ////匿名List取值
            //foreach (var one in anonymousListA)
            //{
            //    PropertyDescriptorCollection property = TypeDescriptor.GetProperties(one);
            //    PropertyDescriptor pdID = property.Find("UserID", true);
            //    PropertyDescriptor pdNM = property.Find("UserNM", true);
            //    string userIDVal = pdID.GetValue(one).ToString();
            //    string userNMVal = pdNM.GetValue(one).ToString();
            //}

            //List<string> resutList = GetValue(anonymousListB);//匿名List取值

            ////class取值
            //var setting = new
            //{
            //    people = 1,
            //    date = DateTime.Now
            //};

            //var propertyInfo = setting.GetType().GetProperty("people");
            //if (propertyInfo != null)
            //{
            //    var peopleVal = (int)(propertyInfo.GetValue(setting, null));
            //}
            #endregion

            #region - 分頁 -
            //string input = "123ABCDE456FGHIJKL789MNOPQ012";
            //var arrSize = Convert.ToInt32(Math.Ceiling(input.Length / (double)5));

            //for (int i = 0; i < input.Length; i = i + 5)
            //{
            //    var result = input.Substring(i, 5);
            //}
            #endregion

            #region - 正規表達式 -
            ////正規表達式直接寫(24小時制時間)
            //bool matchResult = Regex.IsMatch("1935", @"^(([0-1][0-9])|([2][0-3]))([0-5][0-9])$"); //回傳boolean
            //var matchResult2 = Regex.Match("起床時間0630，晚上2200入睡", @"(([0-1][0-9])|([2][0-3]))([0-5][0-9])"); //回傳第一個符合的項目
            //var matchResult3 = Regex.Matches("起床時間0630，晚上2225入睡", @"(([0-1][0-9])|([2][0-3]))([0-5][0-9])"); //回傳所有符合的項目
            //var item = matchResult2.ToString(); //item = 0630
            //List<string> matchList = matchResult3.Cast<Match>()
            //    .Select(m => m.Value).ToList(); //matchList = {"0630","2225":}

            ////正規表達式也可宣告在變數中(24小時制時間)
            //var reegx = @"^(([0-1]?[0-9])|([2][0-3]))([0-5]?[0-9])?$";
            //bool matchResult4 = Regex.IsMatch("1935", reegx);

            ////將字串中符合規則的部分，透過ChangeText方法做處理並替換(英數4碼)
            //Regex reg = new Regex(@"[A-Za-z0-9]{4}");
            //string result = reg.Replace("編號2013至編號8048貨品請回收", ChangeText);

            //string pattern = @"(\s[c][\w]*)"; //前面空白字元 + c 後面接0到多個字母
            //string input = "The men's soft tennis team captured the first gold medal for Taiwan "
            // + "yesterday in the 2010 Asian Games in Guangzhou, China, while their "
            // + "female counterparts garnered a silver for Taiwan, which is competing "
            // + "as Chinese Taipei at the regional sport games.";

            //Regex regIgnoreCase = new Regex(pattern, RegexOptions.IgnoreCase); //不區分大小寫的比對
            //Regex regNotIgnoreCase = new Regex(pattern);

            //MatchCollection matches = regIgnoreCase.Matches(input); //matches = {"captured","China","counterparts","competing","Chinese"}
            //MatchCollection matches2 = regNotIgnoreCase.Matches(input);//matches2 = {"captured","counterparts","competing"}
            #endregion

            #region - 各種日期時間 -
            //var abc = Common.GetDateTimeFormattedText(DateTime.Now, Common.EnumDateTimeFormatted.ShortDateNumber);
            //var formateDateTime = Common.FormatDateString("20171109"); // 格式 : 2017/11/09
            //DateTime dateTime = Convert.ToDateTime(formateDateTime); //{2017/11/9 上午 12:00:00}
            //DateTime y = Common.GetFormatDate("20171109");//{2017/11/9 上午 12:00:00}
            //DateTime dt = Common.GetFormatDate("20180311").Add(TimeSpan.Parse("15:30")); //{2018/3/11 下午 03:30:00}
            //string h = Common.GetDateTimeFormattedText(DateTime.Now.AddDays(1 - DateTime.Now.Day), Common.EnumDateTimeFormatted.ShortDateNumber);//20180301
            //string d = Common.GetDateTimeFormattedText(DateTime.Now.AddDays(1 - DateTime.Now.Day).AddMonths(1).AddDays(-1), Common.EnumDateTimeFormatted.ShortDateNumber);//20180331
            //string dtStr1 = DateTime.Now.ToLongTimeString(); //下午 09:44:04
            //string dtStr2 = Common.GetDateTimeFormattedText(DateTime.Now.AddDays(-10), Common.EnumDateTimeFormatted.ShortDateNumber); //20180301
            //string dtStr3 = Common.GetDateString(); //20180311
            //string dtStr4 = "20180305".Substring(4, "20180305".Length - 4);//0305

            #region - mm:dd時間比大小 -
            //TimeSpan timeA = TimeSpan.Parse("16:20");
            //TimeSpan timeB = TimeSpan.Parse("10:30");
            //bool timeResult = timeA > timeB;
            #endregion

            #endregion

            #region - 底層JS驗證加入訊息 -
            //if ($("#" + _JsErrMessageBox).find("label[" + _display + " ='']").length > 0) {
            //    $('div#JsOtherErrMessageBoxLabel label:eq(0)', "#" + _JsErrMessageBox)
            //        .prepend($(document.createElement("label"))
            //        .attr('jsothererrmessageboxlabel', 'JsOtherErrMessageBoxLabel')
            //        .html(JsMsg_CheckRuleSetNO + (idx + 1) + JsMsg_CheckRuleItem));
            //}
            #endregion

            #region - 字串 或 int相關 -

            #region - 字串移除指定位置某段文字 -
            //string testStr = "aaaa|bbbb|ccc";
            //var result = testStr.Remove(0, testStr.IndexOf('|'));
            #endregion

            #region - 字串 or 數字 轉boolean -
            //bool boolResultA = Convert.ToBoolean(1); //true 0以外所有數字都是true
            //bool boolResultB = Convert.ToBoolean(bool.TrueString); //true
            //bool boolResultC = Convert.ToBoolean(bool.FalseString); //false
            //bool a = Convert.ToBoolean("1"); //會error
            #endregion

            #endregion

            #region - Aggregate用法 -
            //int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            ////每次可暫存結果(return),和下次迴圈做運算
            //int sum = num.Aggregate((start, next) =>
            //{
            //    Console.WriteLine("start:" + start);
            //    Console.WriteLine("nxt:" + next);
            //    return start + next;
            //});
            #endregion

            #region - 列舉操作 -
            //var enumItem = (EnumUserJob)14; //數字代碼取得enum成員
            //var itemList = Enum.GetValues(typeof(EnumUserJob)); //取得enum所有項目

            //string item = "11";
            //var itemToEnum = (EnumUserJob)Enum.Parse(typeof(EnumUserJob), item);
            //bool result = Enum.IsDefined(typeof(EnumUserJob), itemToEnum);

            //switch ((EnumUserJob)Enum.Parse(typeof(EnumUserJob), item))
            //{
            //    case EnumUserJob.SGM:
            //        break;
            //    case EnumUserJob.PERSINNEL:
            //        break;
            //    case EnumUserJob.COMPUTER:
            //        break;
            //    default:
            //        item = item + 'A';
            //        break;
            //}
            #endregion

            #region - 動態塞值到宣告的物件中 -
            //var result = new EntityOrderContactInfo.OrderContactInfo();
            //var fieldName = ((TypeInfo)(originaldata.GetType())).DeclaredFields.ToList().Select(s => s.Name);
            //var modelPropertyList = GetType().GetProperties().Where(m => fieldName.Contains(m.Name)).ToList();

            //foreach (var item in modelPropertyList)
            //{
            //    result.GetType().GetField(item.Name).SetValue(result, new DBVarChar(item.GetValue(this, null).ToString()));
            //}


            //model.WorkFlowAction = new WorkFlowAction();
            //NameValueCollection para = Request.QueryString;
            //var wfParaDic = para.AllKeys.Where(k => k != null).ToDictionary(k => k, v => para[v]);

            //foreach (var item in wfParaDic)
            //{
            //    var propertyInfo = model.WorkFlowAction.GetType().GetProperty(item.Key);
            //    if (propertyInfo != null)
            //    {
            //        var type = propertyInfo.PropertyType;
            //        if (type == typeof(bool))
            //        {
            //            propertyInfo.SetValue(model.WorkFlowAction, Convert.ToBoolean(item.Value));
            //            ;
            //        }
            //        else if (type == typeof(WorkFlowAction.EnumExecActionType))
            //        {
            //            propertyInfo.SetValue(model.WorkFlowAction, (WorkFlowAction.EnumExecActionType)Enum.Parse(typeof(WorkFlowAction.EnumExecActionType), item.Value));
            //        }
            //        else
            //        {
            //            propertyInfo.SetValue(model.WorkFlowAction, item.Value);
            //        }
            //    }
            //}
            #endregion

            #region - 遇A就換B 字串替代 -
            //List<string> tsetList = new List<string> { "2", "4", "4", "3", "4", "5" };
            //string sig = "F";
            //var sigSeq =
            //    (Regex.IsMatch(sig, @"[0-9]$"))
            //        ? sig.PadLeft(3, '0')
            //        : new List<string> { "2", "4" }[new List<string> { "B", "F" }.IndexOf(sig)].PadLeft(3, '0');
            #endregion

            #region - GroupBy 分組後轉dictionary字典 -
            //var addRemarkParaDic
            //    = AddRemarkParaList.GroupBy(o => o.WFNo)
            //                       .ToDictionary(o => o.Key, o => o.ToList());
            #endregion

            #region - 檔案讀寫 -

            #region - URI抓取檔案並上傳 -
            //string fileNM = "/17/17120018_1.04-wallpaper-contest3.jpg".Split(new[] { "/17/" }, StringSplitOptions.RemoveEmptyEntries).Last();
            //string filePath = $"{@"http://uerp.liontravel.com.tw/html2/form"}{@"/17/17120018_1.04-wallpaper-contest3.jpg"}";
            //string docEncodeNM = filePath.Split(new[] { "\\" }, StringSplitOptions.RemoveEmptyEntries).Last();

            //byte[] file = new WebClient().DownloadData(filePath);
            //string serverDir = @"\\localhost\APData\WFAP\WorkFlow\Document\";

            //if (Directory.Exists(serverDir) == false)
            //{
            //    Directory.CreateDirectory(serverDir);
            //}

            //string encodeName = $"{Guid.NewGuid().ToString("N")}{Guid.NewGuid().ToString("N").Substring(0, 16)}";
            //string pdfFilePath = $@"{serverDir}\{"20170000000003"}.{encodeName}";
            //FileStream fs = new FileStream(pdfFilePath, FileMode.Create, FileAccess.Write);
            //fs.Write(file, 0, file.Length);
            //fs.Close();
            #endregion

            #region - FileStream & StreamWriter寫入檔案-
            //string filePath = $@"C:\Log\{DateTime.Now.ToString("yyyyMMdd")}.log";
            //string writeStr = $"[{DateTime.Now:yyyy/MM/dd HH:mm:ss}] {"127.0.0.1"}" + Environment.NewLine;

            #region - 檢查是否有此目錄，若無就會創建(會抓檔案的父層目錄) -
            //FileInfo finfo = new FileInfo(filePath);
            //if (finfo.Directory.Exists == false)
            //{
            //    finfo.Directory.Create();
            //}
            #endregion

            //FileStream fs = File.Open(filePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
            //FileMode.OpenOrCreate 只會針對檔案，不會建立目錄
            //FileMode.Append 才不會覆蓋檔案

            #region - 透過StreamWriter寫入字串 -
            //StreamWriter sw = new StreamWriter(fs);
            //sw.Write(writeStr);
            //sw.Dispose();
            //fs.Dispose();
            #endregion

            #region - 寫入byte資料 -
            //byte[] data = Encoding.Default.GetBytes(writeStr);
            //fs.Write(data, 0, data.Length);
            //fs.Flush();
            //fs.Close();
            #endregion

            #endregion

            #region - FileStream & StreamReader讀取檔案 -
            try
            {
                //FileStream fsRead = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                #region - 透過StreamReader -
                //StreamReader sr = new StreamReader(fsRead);
                //string str = sr.ReadToEnd();
                //sr.Dispose();
                //fsRead.Dispose();
                #endregion

                #region - 讀取資料流 -
                //byte[] byData = new byte[100];
                //char[] charData = new char[1000];
                //fsRead.Seek(0, SeekOrigin.Begin);
                //fsRead.Read(byData, 0, 100);
                //Decoder d = Encoding.Default.GetDecoder();
                //d.GetChars(byData, 0, byData.Length, charData, 0);
                //Console.WriteLine(charData);
                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            #endregion

            #endregion

            #region - Foreach 找 Index -
            //List<string> testList = new List<string> {"A","B","C"};
            //foreach (var istbm in testList.Select((value, index) => new { Value = value, Index = index }))
            //{
            //    int h = istbm.Index; //index起始值為0
            //}
            #endregion

            #region - 存取資料庫使用SqlDataReader 或 SqlDataAdapter -
            //DataTable tableReader = new DataTable();
            //DataTable tableAdapter = new DataTable();
            //DataSet dataSet = new DataSet();

            //Stopwatch swDataReader = new Stopwatch();//計算運行時間
            //Stopwatch swDataAdapter = new Stopwatch();//計算運行時間
            //string connStr = ConfigurationManager.ConnectionStrings["SERPConnection"].ConnectionString;

            #region - SqlDataReader -
            //using (SqlConnection con = new SqlConnection(connStr))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT * FROM ZD223_istbm00", con))
            //    {
            //        con.Open();
            //        swDataReader.Start();//計算運行時間
            //        SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SequentialAccess);//資料抓取到SqlDataReader中
            //        tableReader.BeginLoadData();

            //        for (int i = 0; i < reader.FieldCount; i++)
            //            tableReader.Columns.Add(reader.GetName(i), reader.GetFieldType(i)); //建立資料表的欄位名稱和型態

            //        while (reader.Read())//一次讀取一筆紀錄
            //        {
            //            object[] items = new object[reader.FieldCount];
            //            var name = reader.GetName(0);//此筆記錄第一個欄位的名稱
            //            var val = reader.GetValue(0);//此筆記錄第一個欄位的值//名稱(用指定欄位名稱的寫法)
            //            //var val2 = reader["tabl_type"]; //值(用指定欄位名稱的寫法)

            //            reader.GetValues(items);//取出值放入items中
            //            tableReader.LoadDataRow(items, true);//存入DataTable
            //        }

            //        tableReader.EndLoadData();
            //        var value = tableReader.Rows[0]["tabl_type"]; //抓DataTable中的資料
            //        reader.Close();
            //        swDataReader.Stop();//計算運行時間
            //    }
            //}
            #endregion

            //public DataSet GetERPLogList(ERPLogPara para)
            //{
            //    DataSet dataSet = new DataSet();
            //    DataTable[] dts = new DataTable[] { new DataTable("Tabel1"), new DataTable("Tabel2"), };

            //    StringBuilder commandText = new StringBuilder();

            //    foreach (string form in para.FormList)
            //    {
            //        commandText.AppendLine(string.Join(Environment.NewLine, new object[]
            //        {
            //            "SELECT rec93_stfn",
            //            "     , stfn_stfn",
            //            "     , lrec93_form",
            //            "     , lrec93_date",
            //            "     , lrec93_fsts",
            //            "     , lrec93_hidden",
            //            "     , lrec93_bgcolor",
            //            "     , lrec93_mstfn",
            //            "     , lrec93_mdate",
            //            "     , lrec93_desc",
            //            "  FROM logrecm93",
            //            "  JOIN recm93",
            //            "    ON lrec93_form = rec93_form",
            //            "  LEFT JOIN (",
            //            "           SELECT * FROM opagm20",
            //            "            WHERE stfn_stfn IN (",
            //            "                                   SELECT rec94_stfn FROM recm94",
            //            "                                    WHERE rec94_form = '" + form + "' )) AS OPGM20",
            //            "    ON CHARINDEX(stfn_cname,lrec93_mstfn) > 0",
            //            " WHERE lrec93_form = '" + form + "';",
            //        }));
            //    }

            //    using (SqlConnection connection = new SqlConnection(_conn))
            //    {
            //        using (SqlCommand cmd = new SqlCommand(commandText.ToString(), connection))
            //        {
            //            connection.Open();
            //            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //            adapter.Fill(0, 0, dts);
            //            adapter.Fill(dataSet);
            //        }
            //    }
            //    //var h = dts[0].ToList<ERPSigLog>().ToList();
            //    return dataSet;
            //}

            #region - SqlDataAdapter -
            //swDataAdapter.Start();//計算運行時間
            //using (SqlConnection con = new SqlConnection(connStr))
            //{
            //    using (SqlCommand cmd = new SqlCommand("SELECT * FROM ZD223_istbm00", con))
            //    {
            //        con.Open();
            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(tableAdapter);//放入DataTable
            //        adapter.Fill(dataSet);//放入DataSet
            //        swDataAdapter.Stop();//計算運行時間

            //        var value = tableAdapter.Rows[0]["tabl_type"]; //抓DataTable中的資料
            //        var value2 = dataSet.Tables[0].Rows[0]["tabl_type"].ToString();
            //        //因為語法只有一個SELECT結果，所以只有一個Table，實際上DataSet可存放多個DateTable
            //        //DataSet是存放在記憶體中  
            //    }
            //}
            #endregion

            #region - 一次多筆存入DATASET -
            //DataSet dataSet = new DataSet();
            //DataTable[] dts = new DataTable[] { new DataTable("Tabel1"), new DataTable("Tabel2"), };

            //StringBuilder commandText = new StringBuilder();

            //foreach (string form in FormList)
            //{
            //    commandText.AppendLine(string.Join(Environment.NewLine, new object[]
            //    {
            //        "SELECT rec93_stfn",
            //        "     , stfn_stfn",
            //        "     , lrec93_form",
            //        "     , lrec93_date",
            //        "     , lrec93_fsts",
            //        "     , lrec93_hidden",
            //        "     , lrec93_bgcolor",
            //        "     , lrec93_mstfn",
            //        "     , lrec93_mdate",
            //        "     , lrec93_desc",
            //        "  FROM logrecm93",
            //        "  JOIN recm93",
            //        "    ON lrec93_form = rec93_form",
            //        "  LEFT JOIN (",
            //        "           SELECT * FROM opagm20",
            //        "            WHERE stfn_stfn IN (",
            //        "                                   SELECT rec94_stfn FROM recm94",
            //        "                                    WHERE rec94_form = '"+ form +"' )) AS OPGM20",
            //        "    ON CHARINDEX(stfn_cname,lrec93_mstfn) > 0",
            //        " WHERE lrec93_form = '"+ form +"';",
            //    }));
            //}

            //using (SqlConnection connection = new SqlConnection(_conn))
            //{
            //    using (SqlCommand cmd = new SqlCommand(commandText.ToString(), connection))
            //    {
            //        connection.Open();
            //        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //        adapter.Fill(0, 0, dts);
            //        adapter.Fill(dataSet);
            //    }
            //}

            //var erpLogDS = _connStr.GetERPLogList(logPara);
            //var eRrpLogList = erpLogDS.Tables[0].ToList<Entity.ERPSigLog>();
            //var lrec93_form = erpLogDS.Tables[0].Rows[0]["lrec93_form"];
            //var lrec93_mstfn = erpLogDS.Tables[0].Rows[0]["lrec93_mstfn"];
            #endregion

            #region - DataTable轉List -
            ////迴圈作法
            //List<ZD223_istbm00> istbm00List = (from DataRow dr in tableAdapter.Rows
            //                                   select new ZD223_istbm00
            //                                   {
            //                                       tabl_type = dr.Field<string>("tabl_type")
            //                                   }).ToList();

            //List<ZD223_istbm00> istbm00List = tableAdapter.ToList<ZD223_istbm00>().ToList();
            #endregion

            #region - UPADTE語法 -
            //string commandUpdateText = string.Join(Environment.NewLine,
            //    "UPDATE ZD223_istbm00",
            //    "   SET tabl_order = @tabl_order",
            //    " WHERE tabl_type = @tabl_type",
            //    "   AND tabl_code = @tabl_code");

            //using (SqlConnection con = new SqlConnection(connStr))
            //{
            //    using (SqlCommand cmd = new SqlCommand(commandUpdateText, con))
            //    {
            //        con.Open();
            //        cmd.Parameters.AddWithValue("@tabl_order", 1);
            //        cmd.Parameters.AddWithValue("@tabl_type", "ACCHECK");
            //        cmd.Parameters.AddWithValue("@tabl_code", "1");
            //        var oo = cmd.ExecuteNonQuery();
            //    }
            //}
            #endregion

            //如不用using SqlCommand和sqlConnection 資源要釋放
            //mySqlCmd.Cancel();
            //dataConnection.Close();
            //dataConnection.Dispose();
            #endregion

            #region - 發送Email測試 -
            //MailTemplateInfo templateInfo = new MailTemplateInfo
            //{
            //    EmailTemplateUrl = "~/_Shared/LightSpeed/SubSiteBundles/Common/PartialViews/_OrderCheck.html",//Email的樣板HTML
            //    CompID = "TW",
            //    TemplateItems = new
            //    {//樣板中要替換的文字
            //        OrderID = "123",
            //        CustomerNM = "HI",
            //        OrderCreateTime = DateTime.Now,
            //        OrderUrl = "http://aa.com"
            //    }
            //};

            //MailInfo mailInfo = new MailInfo(templateInfo)
            //{
            //    Subject = "雄獅旅遊 - 團體需求單確認信 123",//主旨
            //    MailSender = "hjiunliau@liontravel.com",//寄件者
            //    IsBodyHtml = true,
            //    MailReceiver = new List<string> { "zomzad@gmail.com" },//收件者
            //    FilePaths = new List<string> { $"{"~/_Shared/LightSpeed/SubSiteBundles/Common/StaticViews/PDF/"}aa.pdf" }//附件檔案
            //};
            //new SendMail().Mail_Send(mailInfo);
            #endregion

            #region - 呼叫API GET (網頁的話可抓取網頁原始碼) -
            //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://ptx.transportdata.tw/MOTC/v2/Bus/EstimatedTimeOfArrival/City/Taipei/280?$top=30&$format=JSON");
            //request.Method = WebRequestMethods.Http.Get;
            //request.KeepAlive = false;
            //request.ContentType = "application/json";
            //HttpWebResponse response = request.GetResponse() as HttpWebResponse; //取得API回傳結果
            //if (response != null)
            //{
            //    Stream responseStream = response.GetResponseStream();
            //    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
            //    string srcString = reader.ReadToEnd(); //如果是網頁 可以抓到網頁原始碼
            //}
            #endregion

            #region - 呼叫API POST -
            ////回傳結果
            //string result = string.Empty;

            //HttpWebRequest requestPost = WebRequest.Create("https://uinc.api.liontravel.com/api/V2/CompInfo?CompID=TW") as HttpWebRequest;
            //if (requestPost != null)
            //{
            //    requestPost.Method = "POST";
            //    requestPost.KeepAlive = true;
            //    requestPost.ContentType = "application/x-www-form-urlencoded";

            //    var jsonPara = new JavaScriptSerializer().Serialize(
            //        new Dictionary<string, object>()
            //        {
            //            { "MSG_STFN", "D223" },
            //            { "MSG_MESSAGE", "HELLO你好" }
            //        });

            //    byte[] bs = Encoding.UTF8.GetBytes(jsonPara);

            //    using (Stream reqStream = request.GetRequestStream())
            //    {
            //        reqStream.Write(bs, 0, bs.Length);
            //        reqStream.Flush();
            //    }

            //    using (WebResponse responsePost = requestPost.GetResponse())
            //    {
            //        StreamReader sr = new StreamReader(responsePost.GetResponseStream());
            //        result = sr.ReadToEnd();
            //        sr.Close();
            //    }
            //}
            #endregion

            #region - 讀取html檔案的原始碼 -
            //string htmlFileDir = @"E:\TFS\LionTravel\LionLightSpeed\SourceCode\LightSpeed.B2C.Travel.Web\1234567\1234567.html";
            //string htmlStrA = Encoding.UTF8.GetString((new WebClient()).DownloadData(htmlFileDir));
            //string htmlStrB = File.ReadAllText(htmlFileDir, Encoding.UTF8);
            #endregion

            #region - [action]允許跨網域 -
            //string sCorsProtocol = Request.IsSecureConnection ? "https://" : "http://";
            //var clientOrigin = System.Web.HttpContext.Current.Request.Headers.Get("Origin");
            //if (string.IsNullOrEmpty(clientOrigin) == false &&
            //    clientOrigin.Contains(".liontravel.com"))
            //{
            //    System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", clientOrigin);
            //}
            //else
            //{
            //    System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", sCorsProtocol + Utility.Appl.CorsWebsite);
            //}
            #endregion

            #region - NReco.PdfGenerator轉PDF -

            #region - 直接讀取檔案轉檔 -
            //var nrecoPdfGenerator = new NReco.PdfGenerator.HtmlToPdfConverter
            //{
            //    Size = NReco.PdfGenerator.PageSize.A3
            //};

            //const string urlpdf = "C:/產品比較.html";
            //nrecoPdfGenerator.GeneratePdfFromFile(urlpdf, null, "C:/OutputNReco.pdf");
            #endregion

            #region - 用在網站action進入時 -
            //string protocol = Request.IsSecureConnection ? "https://" : "http://";
            //orderID = string.IsNullOrWhiteSpace(orderID)
            //    ? (new Random(Guid.NewGuid().GetHashCode()).Next()).ToString()
            //    : orderID;

            //htmlSourceCode = HttpUtility.UrlDecode(htmlSourceCode);
            //byte[] pdfBytes;
            //var nrecoPdfGenerator = new NReco.PdfGenerator.HtmlToPdfConverter
            //{
            //    Size = NReco.PdfGenerator.PageSize.A3
            //};

            //if (string.IsNullOrWhiteSpace(htmlSourceCode))
            //{
            //    string urlpdf = (string.IsNullOrWhiteSpace(sourceUrl))
            //        ? Request.UrlReferrer?.ToString()
            //        : $@"{protocol}{Request.Url?.Authority}{HttpUtility.UrlDecode(sourceUrl)}";
            //    pdfBytes = nrecoPdfGenerator.GeneratePdfFromFile(urlpdf, null);
            //}
            //else
            //{
            //    var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();
            //    pdfBytes = htmlToPdf.GeneratePdf(HttpUtility.UrlDecode(htmlSourceCode));
            //}

            //if (isShowPdf)
            //{
            //    #region - 將PDF檔案顯示在瀏覽器上 -
            //    Response.ContentType = "application/pdf";
            //    Response.ContentEncoding = System.Text.Encoding.Default;
            //    Response.Clear();
            //    Response.AddHeader("Content-Disposition", $"Inline; filename={orderID}.pdf");
            //    Response.BinaryWrite(pdfBytes);
            //    Response.Flush();
            //    Response.End();
            //    #endregion
            //}
            #endregion

            #region - 儲存在Server端資料夾 -
            //string serverDir = Server.MapPath($"{"~/_Shared/LightSpeed/SubSiteBundles/Common/StaticViews/PDF/"}{orderID}");

            //if (Directory.Exists(serverDir) == false)
            //{
            //    Directory.CreateDirectory(serverDir);
            //}

            //string pdfFilePath = $@"{serverDir}\{orderID}.pdf";
            //int size = pdfBytes.Length;
            //FileStream fs = new FileStream(pdfFilePath, FileMode.Create, FileAccess.Write);
            //fs.Write(pdfBytes, 0, size);
            //fs.Close();
            #endregion

            #endregion

            #region - iTextSharp轉PDF -
            //MemoryStream outputStream = new MemoryStream();
            //WebClient wc = new WebClient();
            //string htmlText = wc.DownloadString(url);
            //if (string.IsNullOrEmpty(htmlText))
            //{
            //    return null;
            //}

            //htmlText = "<p>" + htmlText + "</p>";

            //#region - 註冊字體標楷體 -
            //string fontPath = "C:/kaiu.ttf";
            //FontFactory.Register(fontPath);
            //#endregion

            //byte[] data = Encoding.UTF8.GetBytes(htmlText);
            //MemoryStream msInput = new MemoryStream(data);
            //Document doc = new Document();
            //PdfWriter writer = PdfWriter.GetInstance(doc, outputStream);
            //PdfDestination pdfDest = new PdfDestination(PdfDestination.XYZ, 0, doc.PageSize.Height, 1f);

            //doc.Open();

            //XMLWorkerHelper.GetInstance().ParseXHtml(writer, doc, msInput, null, Encoding.UTF8);
            //PdfAction action = PdfAction.GotoLocalPage(1, pdfDest, writer);

            //writer.SetOpenAction(action);
            //doc.Close();
            //msInput.Close();
            //outputStream.Close();

            //byte[] pdfFile = outputStream.ToArray();
            //return File(pdfFile, "application/pdf", "OutputiTextSharp.pdf");
            #endregion

            #region - 留言測試 -
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = true; //標記此留言是否為系統發送
            //message.MSG_HDATE = "20170808"; //格式 : YYYYMMDD ，留言接收者會在指定提醒日期收到留言
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "HI 系統發送 但給留言者"; //留言內容(必填)
            //message.MSG_TIMEOUT = false; //是否藍字提醒，留言會以藍色顯示
            //client.Send(message);

            //系統發送MSG_SYS = false ，留言發送者MSG_MSTFN 不給值
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_HDATE = "20170808"; //格式 : YYYYMMDD ，留言接收者會在指定提醒日期收到留言
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "系統發送MSG_SYS = false ，留言發送者MSG_MSTFN 不給值"; //留言內容(必填)
            //message.MSG_TIMEOUT = false; //是否藍字提醒，留言會以藍色顯示
            //client.Send(message);

            //藍字提醒 = true
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "藍字提醒"; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //message.MSG_TIMEOUT = true; //是否藍字提醒，留言會以藍色顯示
            //client.Send(message);

            //MSG_HDATE日期打錯
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_HDATE = "00170832"; //格式 : YYYYMMDD ，留言接收者會在指定提醒日期收到留言
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "MSG_HDATE日期打錯"; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //message.MSG_TIMEOUT = false; //是否藍字提醒，留言會以藍色顯示
            //client.Send(message);

            //查無訂單業務資料
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "HI"; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //message.MSG_YEAR = "2017";
            //message.MSG_ORDR = 2173390;
            //client.Send(message);

            //帶入團號&團號公司
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "HI"; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //message.MSG_PROD = "13XI121CXK";
            //message.MSG_PRODCOMP = "T";
            //client.Send(message);

            //不設定MSG_TIMEOUT值
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "無藍字提醒"; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //client.Send(message);

            //增加MSG_URL網址
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = "出勤2"; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //message.MSG_TIMEOUT = false;
            //message.MSG_URL = "http://userp.liontravel.com.tw/Home/ERPLogin?userID=D223&targetUrl=http://uhcm.liontravel.com.tw/Pub/PageAgent?MsgID=00000017WH";
            //client.Send(message);

            //MSG_MESSAGE留言內容空字串，跳出必填驗證
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_MESSAGE = ""; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //message.MSG_TIMEOUT = false;
            //client.Send(message);
            #endregion

            #region - 留言測試 -
            //UserMessageClient client = UserMessageClient.Create(url);
            //client.ClientSysID = "ERPAP"; //留言發送平台
            //client.ClientUserID = "8605"; //留言發送人員
            //Message message = new Message();
            //message.MSG_HDATE = "20170809"; //格式 : YYYYMMDD ，留言接收者會在指定提醒日期收到留言
            //message.MSG_STFN = "D223"; //留言接收者員編
            //message.MSG_SYS = false; //標記此留言是否為系統發送
            //message.MSG_MESSAGE = "HI"; //留言內容(必填)
            //message.MSG_MSTFN = "8605"; //留言發送者員編
            //message.MSG_TIMEOUT = false; //是否藍字提醒，留言會以藍色顯示
            //client.Send(message);
            #endregion

            #region - 簡訊發送綜合測試 -
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.Country = "886";//電話國碼 
            //messagesms.PhoneNumber = "0912106296";//簡訊發送對象手機號碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.ProjectName = "說明";//專案名稱註記
            //var actual = clientsms.Send(messagesms);

            ////手機號碼打9碼，秀錯誤訊息
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.Country = "886";//電話國碼 
            //messagesms.PhoneNumber = "912106296";//簡訊發送對象手機號碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.ProjectName = "說明";//專案名稱註記
            //var actual = clientsms.Send(messagesms);

            ////國碼改香港852，手機號碼打9碼
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.Country = "852";//電話國碼 
            //messagesms.PhoneNumber = "912106296";//簡訊發送對象手機號碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.ProjectName = "說明";//專案名稱註記
            //var actual = clientsms.Send(messagesms);

            //預約時間發送
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.Country = "886";//電話國碼 
            //messagesms.PhoneNumber = "0912106296";//簡訊發送對象手機號碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.ProjectName = "說明";//專案名稱註記
            //messagesms.BookingDateTime = DateTime.Now.AddDays(1);
            //var actual = clientsms.Send(messagesms);

            //預約時間小於30分鐘,秀錯誤訊息
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.Country = "886";//電話國碼 
            //messagesms.PhoneNumber = "0912106296";//簡訊發送對象手機號碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.ProjectName = "說明";//專案名稱註記
            //messagesms.BookingDateTime = DateTime.Now.AddMinutes(15);
            //var actual = clientsms.Send(messagesms);

            ////發送對象員編隨便打
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.SendUser = "X010";//簡訊發送對象員編
            //messagesms.Country = "886";//電話國碼 
            //messagesms.PhoneNumber = "0912106296";//簡訊發送對象手機號碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.ProjectName = "說明";//專案名稱註記
            //messagesms.BookingDateTime = DateTime.Now.AddMinutes(45);
            //var actual = clientsms.Send(messagesms);

            //輸入訂單年分編號
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.Country = "886";//電話國碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.OrderNumber = 55;
            //messagesms.OrderYear = "2018";
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.PhoneNumber = "0912106296";//簡訊發送對象手機號碼
            //messagesms.ProjectName = "說明";//專案名稱註記
            //messagesms.BookingDateTime = DateTime.Now.AddMinutes(45);
            //var actual = clientsms.Send(messagesms);

            //傳State和StateDesc
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.Country = "886";//電話國碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.State = "V";
            //messagesms.StateDesc = "DESC";
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.PhoneNumber = "0912106296";//簡訊發送對象手機號碼
            //messagesms.ProjectName = "說明";//專案名稱註記
            //messagesms.BookingDateTime = DateTime.Now.AddMinutes(45);
            //var actual = clientsms.Send(messagesms); 
            #endregion

            #region - 簡訊發送測試 -
            //SMSClient clientsms = SMSClient.Create(url);
            //clientsms.ClientSysID = "ERPAP";//簡訊發送平台
            //clientsms.ClientUserID = "8605";//簡訊發送人員
            //SMSMessage messagesms = new SMSMessage();
            //messagesms.SendUser = "D223";//簡訊發送對象員編
            //messagesms.Country = "886";//電話國碼 
            //messagesms.PhoneNumber = "0912106296";//簡訊發送對象手機號碼
            //messagesms.Message = "請領取包裹";//簡訊內容
            //messagesms.ProjectName = "說明";//專案名稱註記
            //messagesms.BookingDateTime = DateTime.Now.AddDays(1);

            //var actual = clientsms.Send(messagesms);
            #endregion

            #region - 簡訊查詢測試 -
            //SMSClient clientSms2 = SMSClient.Create(url);
            //clientSms2.ClientSysID = "ERPAP";//查詢簡訊平台
            //clientSms2.ClientUserID = "8605";//查詢簡訊人員
            //var actualSms2 = clientSms2.Query(actual.SMSYear, actual.SMSSeq);//查詢簡訊。傳入發送簡訊時取得的SMSYear和SMSSeq。得到回傳物件
            #endregion

            #region - 取消簡訊測試 -
            //SMSClient clientSms3 = SMSClient.Create(url);
            //clientSms3.ClientSysID = "ERPAP";// 取消簡訊平台
            //clientSms3.ClientUserID = "8605";// 取消簡訊人員
            //SMSCancel messageSms3 = new SMSCancel();
            //messageSms3.SMSYear = actual.SMSYear;// 發送簡訊時取得的回傳值SMSYear
            //messageSms3.SMSSeq = actual.SMSSeq;// 發送簡訊時取得的回傳值SMSSeq
            //messageSms3.PhoneNumber = "0912106296";// 取消簡訊對象的手機號碼(非必填)
            //var actualSms3 = clientSms3.Cancel(messageSms3);// 回傳true或false
            #endregion

            #region - CMS行政區API -

            #region - 取得Token -
            //string cmsTestUrl = "https://ucms.api.liontravel.com/API/";
            //string cmsProductionUrl = "https://cms.api.liontravel.com/API/";

            //WebClient cmsToken = new WebClient();
            //cmsToken.Encoding = System.Text.Encoding.UTF8;
            //cmsToken.Headers.Add(HttpRequestHeader.ContentType, "application/json;charset=UTF-8");
            //cmsToken.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
            //cmsToken.Headers.Add(HttpRequestHeader.Accept, "json");
            //LoginAPIViewModel accountInfo = new LoginAPIViewModel
            //{
            //    #region - 測試機帳密
            //    login_id = "Serp",
            //    password = "N56ser89P"
            //    #endregion

            //    #region - 正式機帳密 -
            //    //m.login_id = "Serp";
            //    //m.password = "nKrt45r89P";
            //    #endregion
            //};

            //string strSendData = new JavaScriptSerializer().Serialize(accountInfo);
            //string strReurn = cmsToken.UploadString(cmsTestUrl + "GetToken", strSendData);
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //ToKenViewModel toKen = js.Deserialize<ToKenViewModel>(strReurn);
            #endregion

            #region - 取得行政區資料 -
            //if (toKen.ResultID == 1)
            //{
            //    string tokenInfo = toKen.ToKen;

            //    WebClient cmsArea = new WebClient();
            //    cmsArea.Encoding = System.Text.Encoding.UTF8;
            //    cmsArea.Headers.Add(HttpRequestHeader.ContentType, "application/json;charset=UTF-8");
            //    cmsArea.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-cn,zh;q=0.8,en-us;q=0.5,en;q=0.3");
            //    cmsArea.Headers.Add("Authorization", $"basic {tokenInfo}");
            //    cmsArea.Headers.Add(HttpRequestHeader.Accept, "json");

            //    var jsonPara = new JavaScriptSerializer().Serialize(
            //        new Dictionary<string, object>()
            //        {
            //            { "Key", "2" },
            //            { "LastUpdateDate", "" }
            //        });

            //    var cmsAreaResult = cmsArea.UploadString(cmsTestUrl + "GetAreaData", jsonPara);
            //}
            #endregion

            #endregion

            #region - 不固定類別Json字串轉字典 -
            //string json = "{\"Result\":\"Y\",\"Message\":null,\"Data\":{\"WFNo\":\"20170000000150\",\"NodeNo\":\"001\",\"SysID\":\"PUBAP\",\"FlowID\":\"SignForm\",\"FlowVer\":\"001\",\"NodeID\":\"ApplySignForm\",\"NodeType\":\"P\",\"FunSysID\":\"PUBAP\",\"SubSysID\":\"PUBAP\",\"FunControllerID\":\"WorkFlow\",\"FunActionName\":\"SignFormDetail\",\"NodeUrl\":\"http://127.0.0.1:8906/WorkFlow/SignFormDetail\"}}";
            //var apiResult = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(json);
            //var wfno = (apiResult["Data"] as Dictionary<string, object>)?["WFNo"].ToString();
            #endregion

            #region - Json轉不固定類別List -
            //LogPushMessageList = (from s in result
            //                      let data = jsonConvert.Deserialize<Dictionary<string, string>>(s.Data.GetValue())
            //                      where data.ContainsKey("SourceType") == false
            //                      select new APIDataResult
            //                      {
            //                          MessageID = s.MessageID.GetValue(),
            //                          Title = s.Title.GetValue(),
            //                          Body = s.Body.GetValue(),
            //                          UpdDT = Common.GetDateTimeString(s.UpdDT.GetValue().ToLocalTime())
            //                      }).ToList();
            #endregion
        }

        #region - 自行擴充ENUM屬性 -
        /// <summary>
        /// 自行擴充ENUM可掛的屬性
        /// </summary>
        /// 可像這樣掛在列舉成員上
        /// [ReportDescription("全部發送清單")]
        ///  All = 310,
        private class ReportDescriptionAttribute : Attribute
        {
            public string Description { get; set; }

            public ReportDescriptionAttribute(string Description)
            {
                this.Description = Description;
            }

            public override string ToString()
            {
                return Description;
            }
        }
        #endregion

        #region - CMS行政區Token回傳類別 -
        public class ToKenViewModel
        {
            /// <summary>
            /// 1:獲取ToKen成功 0:失敗
            /// </summary>
            public int ResultID { get; set; }

            /// <summary>
            /// 返回信息
            /// </summary>
            public string Message { get; set; }

            /// <summary>
            /// ToKen 值
            /// </summary>
            public string ToKen { get; set; }
        }
        #endregion

        #region - 對符合正規表示法規則的字串做處理 -
        /// <summary>
        /// 對符合正規表示法規則的字串做處理
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        private static string ChangeText(Match m)
        {
            string x = m.ToString();
            return string.IsNullOrWhiteSpace(x) ? x : (x[0] == 'Z' ? "ZZ" + x : "00" + x);
        }
        #endregion

        #region - 系統列舉 -
        /// <summary>
        /// 系統列舉
        /// </summary>
        public enum EnumSystemID
        {
            [Category("Developing")]
            [Description("127.0.0.1")]
            Domain,

            [Category("ERPAP")]
            [Description("http://127.0.0.1:8888")]
            ERPAP,

            [Category("PUBAP")]
            [Description("http://127.0.0.1:9999")]
            PUBAP
        }
        #endregion

        #region - 取得列舉描述 -
        /// <summary>
        /// 取得列舉描述
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetEnumDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }
        #endregion

        #region - 由列舉描述取得列舉成員 -
        /// <summary>
        /// 由列舉描述取得列舉成員
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="description"></param>
        /// <returns></returns>
        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum)
            {
                throw new InvalidOperationException();
            }
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (T)field.GetValue(null);
                    }
                }
            }
            throw new ArgumentException(@"無相對應的列舉值", nameof(description));
        }
        #endregion

        #region - 寫LOG -
        /// <summary>
        /// 寫LOG
        /// </summary>
        public static class EventLog
        {
            public static string FilePath { get; set; }

            public static void Write(string format, params object[] arg)
            {
                Write(string.Format(format, arg));
            }

            public static void Write(Exception message)
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    FilePath = Directory.GetCurrentDirectory();
                }
                string filename = FilePath + string.Format("\\{0:yyyy}\\{0:MM}\\{0:yyyy-MM-dd}.txt", DateTime.Now);
                FileInfo finfo = new FileInfo(filename);
                if (finfo.Directory.Exists == false)
                {
                    finfo.Directory.Create();
                }
                string writeString = string.Format("{0:yyyy/MM/dd HH:mm:ss} {1}", DateTime.Now, message) + Environment.NewLine;
                File.AppendAllText(filename, writeString, Encoding.Unicode);
            }
        }
        #endregion

        #region - 取得公車資訊 -
        /// <summary>
        /// 取得公車資訊
        /// </summary>
        /// <param name="queryBusStr"></param>
        /// queryBusStr = bus/645
        /// <returns></returns>
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

                    busMinResult = ooo.Select((val, idx) => new { Index = idx, Value = val }).Aggregate(busMinResult, (current, row) => current + string.Join(Environment.NewLine, $"第{row.Index + 1}班公車{busInfo[1]}到內湖行政大樓還有{row.Value.EstimateTime / 60}分鐘"));
                }

                return busMinResult;
            }
            catch (Exception)
            {
                throw new InvalidOperationException("取得公車訊息失敗");
            }
        }
        #endregion

        #region - 取得員工編號新舊碼清單 TypeCode用法 -
        /// <summary>
        /// 取得員工編號新舊碼清單
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        //public static List<DBVarChar> GetUserIDList<T>(T userInfo)
        //{
        //    var userIDInfoList = new List<DBVarChar>();
        //    var type = Type.GetTypeCode(typeof(T));

        //    switch (type)
        //    {
        //        case TypeCode.String:
        //            string userID = Convert.ToString(userInfo);
        //            if (string.IsNullOrWhiteSpace(userID) == false)
        //            {
        //                userIDInfoList.Add(new DBVarChar(userID));

        //                if (userID.Substring(0, 2) == Common.GetEnumDesc(EnumNewUserIDStartStrType.StartWithZ) ||
        //                    userID.Substring(0, 2) == Common.GetEnumDesc(EnumNewUserIDStartStrType.StartWithZero))
        //                {
        //                    userIDInfoList.Add(new DBVarChar(userID.Remove(0, 2)));
        //                }
        //            }
        //            break;
        //        case TypeCode.Object:
        //            var userIDList = userInfo as List<DBVarChar>;
        //            if (userIDList != null &&
        //                userIDList.Any())
        //            {
        //                userIDInfoList.AddRange(userIDList);
        //                userIDInfoList.AddRange(from id in userIDList
        //                                        where id.GetValue().Substring(0, 2) == Common.GetEnumDesc(EnumNewUserIDStartStrType.StartWithZ)
        //                                              || id.GetValue().Substring(0, 2) == Common.GetEnumDesc(EnumNewUserIDStartStrType.StartWithZero)
        //                                        select new DBVarChar(id.GetValue().Remove(0, 2)));
        //            }
        //            break;
        //    }

        //    return userIDInfoList;
        //}
        #endregion

        #region - CLASS轉DATATABLE -
        private static DataTable CreateEmptyDataTable(Type myType, string tableNM)
        {
            DataTable dt = new DataTable(tableNM);

            foreach (PropertyInfo info in myType.GetProperties())
            {
                dt.Columns.Add(new DataColumn(info.Name, info.PropertyType));
            }

            return dt;
        }
        #endregion

        #region - 取得異動欄位 -
        /// <summary>
        /// 取得異動欄位
        /// </summary>
        //public bool GetModifyField()
        //{
        //    try
        //    {
        //        ModifyField = new List<string>();
        //        EntityOrderContactInfo.OrderContactInfoPara para = new EntityOrderContactInfo.OrderContactInfoPara
        //        {
        //            OrderSN = new DBInt(string.IsNullOrWhiteSpace(OrderSN) ? null : OrderSN),
        //            OrderYear = new DBChar(string.IsNullOrWhiteSpace(OrderYear) ? null : OrderYear)
        //        };
        //        var oldOrderContactInfo = _entity.SelectOrderContactInfo(para);

        //        var oldOrderContactFieldInfo = ((TypeInfo)(oldOrderContactInfo.GetType())).DeclaredFields.ToList();
        //        var fieldNM = oldOrderContactFieldInfo.Select(s => s.Name);
        //        var propertyInfoDic = GetType().GetProperties().Where(m => fieldNM.Contains(m.Name))
        //                                       .ToDictionary(k => k.Name, v => v.GetValue(this, null)?.ToString());

        //        ModifyField.AddRange(oldOrderContactFieldInfo
        //            .Where(contactInfoField => propertyInfoDic[contactInfoField.Name]
        //                                       != ((DBStringType)(oldOrderContactInfo.GetType().GetField(contactInfoField.Name).GetValue(oldOrderContactInfo))).GetValue())
        //            .Select(m => m.Name));

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        OnException(ex);
        //    }

        //    return false;
        //}
        #endregion

        #region - LIST轉DATATABLE -
        public void ListTranDataTable()
        {
            #region - List轉DataTable -
            //var props = typeof(EntityBatch.AddRemarkPara).GetProperties();
            //var remarkDT = new DataTable();
            //remarkDT.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            //AddRemarkParaList.ForEach(
            //    remark => remarkDT.LoadDataRow
            //        (
            //            props.Select(pi => pi.GetValue(remark, null)).ToArray(),
            //            true
            //        ));
            #endregion

            //方法2
            //var props = typeof(EntityBatch.AddRemarkPara).GetProperties();
            //var remarkDT = new DataTable();
            //remarkDT.Columns.AddRange(props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray());

            //foreach (EntityBatch.AddRemarkPara remark in AddRemarkParaList)
            //{
            //    var array = props.Select(pi => pi.GetValue(remark, null)).ToArray();
            //    remarkDT.LoadDataRow(array, true);
            //}

            //AddRemarkParaList.ForEach(remark => remarkDT.LoadDataRow(props.Select(pi => pi.GetValue(remark, null)).ToArray(),true));
        }
        #endregion

        #region - List轉DataTable 通用版本 -
        //private DataTable ListToDatatable<T>(IEnumerable<T> dataList)
        //{
        //    var dt = new DataTable();

        //    var props = typeof(T).GetProperties();
        //    dt.Columns.AddRange(props.Select(p =>
        //        new DataColumn(p.Name,
        //            (p.PropertyType.IsGenericType && p.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
        //                ? p.PropertyType.GetGenericArguments()[0]
        //                : p.PropertyType)).ToArray());

        //    dataList.ToList().ForEach(remark => dt.LoadDataRow(props.Select(pi => pi.GetValue(remark, null)).ToArray(), true));

        //    return dt;
        //}
        #endregion

        #region - 匿名型別取值 -
        public static List<string> GetValue(List<object> oList)
        {
            return (from s in oList
                    let property = TypeDescriptor.GetProperties(s)
                    let value = property.Find("people", true).GetValue(s)
                    where value != null
                    select value.ToString()).ToList();
        }
        #endregion

        #region - 委派測試用方法 -
        public static int MethodA(int a, int b)
        {
            return a + b;
        }

        public static int MethodB(int a, int b)
        {
            return a - b;
        }
        #endregion
    }
}
