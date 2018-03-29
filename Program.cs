using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
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
using Newtonsoft.Json;
using SampleCode.Models;
using SampleCode.Security;

namespace TEST
{
    class Program
    {
        public enum EnumUserJob
        {
            SGM = 10,
            PERSINNEL = 11,
            COMPUTER = 14
        }

        private static void Main(string[] args)
        {
            #region - 臨時測試 -
            var tt = new DateTime?();
            var testStr = "預設008056".Substring(0, 2);
            string A = "1";
            string B = "3";

            bool aaaaa = string.CompareOrdinal(A, B) >= 0;

            var aaList = new List<string> { "228", "235", "218", "219", "220", "227", "229", "230", "232", "171", "233", "234", "237" };
            #endregion

            //字串移除指定位置某段文字
            //string testStr = "aaaa|bbbb|ccc";
            //var result = testStr.Remove(0, testStr.IndexOf('|'));

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

            #region - 字串 or 數字 轉boolean -
            bool boolResultA = Convert.ToBoolean(1); //true 0以外所有數字都是true
            bool boolResultB = Convert.ToBoolean(bool.TrueString); //true
            bool boolResultC = Convert.ToBoolean(bool.FalseString); //false
                                                                    //bool a = Convert.ToBoolean("1"); //會error
            #endregion

            #region - Aggregate用法 -
            //int[] num = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //每次可暫存結果(return),和下次迴圈做運算
            //int sum = num.Aggregate((start, next) => {
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

            #region URI抓取檔案並上傳
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

            #region - 正規表達示替換字串 & 取符合字串集合 -
            //string input = "002013起只13筆, 已1年沒有資料, 故而作廢 預設008048";
            ////"blue 8120 FG52王淑芬,A911 楊琇茲ZA28 0123 red"
            //Regex rx = new Regex(@"[A-Za-z0-9]{4}");
            //string result = rx.Replace(input, ChangeText);

            //string pattern = @"(\d{4})";
            //Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
            //MatchCollection matches = regex.Matches(input);
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
    }
}
