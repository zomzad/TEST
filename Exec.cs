using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using LionTech.Utility;

namespace TEST
{
    class Exec
    {
        public void ExecTestFun()
        {
            //string filePath = $@"//iis-utour/UTOUR/WebApp/TKTAP/App_GlobalResources/Booking/BookingBooking.resx";
            IPHostEntry hostinfo = Dns.GetHostEntry("google.com.tw");
            string ipAddress = hostinfo.AddressList[0].ToString();

            string filePath = $@"192.168.1.233/App_GlobalResources/Booking/BookingBooking.resx";
            ResXResourceReader resxReader = new ResXResourceReader(filePath);
            Directory.GetDirectories(@"//iis-utour");
            var resourceDic = File.Exists(filePath) ?
                resxReader
                    .Cast<DictionaryEntry>()
                    .ToDictionary(dic => dic.Key.ToString(), dic => dic.Value.ToString()) :
                new Dictionary<string, string>();

            var resourceInfoList = (from s in resourceDic
                                join o in resourceDic on s.Key equals o.Key into resource
                                from o in resource.DefaultIfEmpty()
                                select new
                                {
                                    ResourceNM = s.Key,
                                    ValueZHTW = s.Value,
                                    o.Value
                                }).ToList();





            string uploadPath = @"C:\TEST";
            byte[] file = new WebClient().DownloadData(filePath);

            if (Directory.Exists(uploadPath) == false)
            {
                Directory.CreateDirectory(uploadPath);
            }

            string pdfFilePath = $@"{uploadPath}\{DateTime.Now.ToString("yyyymmmmddhhmmss")}.jpg";
            FileStream fs = new FileStream(pdfFilePath, FileMode.Create, FileAccess.Write);
            fs.Write(file, 0, file.Length);
            fs.Close();
        }
    }
}
