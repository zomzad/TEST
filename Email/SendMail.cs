using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;

namespace TEST
{
    public class MailInfo
    {
        private string _compPhone = "(02)8793-9000";
        private string _compAddress = "台北市石潭路151號";

        public MailInfo()
        {
            
        }

        public MailInfo(MailTemplateInfo info)
        {
            SetTemplateInfo(info);
        }

        /// <summary>
        /// 畫面LOGO
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 信件樣板
        /// </summary>
        public string EmailTemplateHtml { get; set; }

        /// <summary>
        /// 寄件者
        /// </summary>
        public string MailSender { get; set; }

        /// <summary>
        /// 主旨
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 是否採用HTML格式
        /// </summary>
        public bool IsBodyHtml { get; set; }

        /// <summary>
        /// 附件檔案路徑
        /// </summary>
        public List<string> FilePaths { get; set; }

        /// <summary>
        /// 寄件副本
        /// </summary>
        public List<string> Ccs { get; set; }

        /// <summary>
        /// 收件者
        /// </summary>
        public List<string> MailReceiver { get; set; }

        /// <summary>
        /// EMAIL Server
        /// </summary>
        internal SmtpClient SmtpClient { get; set; }

        private void SetTemplateInfo(MailTemplateInfo info)
        {
            EmailTemplateHtml = Encoding.UTF8.GetString((new WebClient()).DownloadData(info.EmailTemplateUrl));//取得html碼

            if (info.TemplateItems != null)
            {
                Type type = info.TemplateItems.GetType();
                PropertyInfo[] propertyInfos = type.GetProperties();

                foreach (var item in propertyInfos)
                {
                    if (item.PropertyType == typeof(DateTime))
                    {
                        DateTime dateTimeVal = Convert.ToDateTime(item.GetValue(info.TemplateItems, null));
                        EmailTemplateHtml = EmailTemplateHtml.Replace($"[{item.Name}]", dateTimeVal.ToString());
                    }
                    else
                    {
                        var val = item.GetValue(info.TemplateItems, null).ToString();
                        EmailTemplateHtml = EmailTemplateHtml.Replace($"[{item.Name}]", val);
                    }
                }
            }
        }
    }

    public class MailTemplateInfo
    {
        /// <summary>
        /// 信件HTML樣板
        /// </summary>
        public string EmailTemplateUrl { get; set; }

        /// <summary>
        /// 公司代碼
        /// </summary>
        public string CompID { get; set; }

        /// <summary>
        /// 信件HTML樣板元素
        /// </summary>
        public object TemplateItems { get; set; }
    }

    public class SendMail
    {
        public bool Mail_Send(MailInfo mailInfo)
        {
            mailInfo.SmtpClient = new SmtpClient(ConfigurationManager.AppSettings["EmailServer"], 25);

            try
            {
                MailMessage message = new MailMessage
                {
                    From = new MailAddress(mailInfo.MailSender),
                    Subject = mailInfo.Subject,
                    Body = mailInfo.EmailTemplateHtml,
                    BodyEncoding = Encoding.Default,
                    IsBodyHtml = mailInfo.IsBodyHtml
                };

                #region - 收件者 -
                foreach (var receiver in mailInfo.MailReceiver)
                {
                    message.To.Add(receiver);
                }
                #endregion

                #region - 寄件副本 -
                if (mailInfo.Ccs != null &&
                    mailInfo.Ccs.Any())
                {
                    foreach (var cc in mailInfo.Ccs)
                    {
                        message.CC.Add(new MailAddress(cc.Trim()));
                    }
                }
                #endregion

                #region - 附加檔案 -
                if (mailInfo.FilePaths != null &&
                    mailInfo.FilePaths.Any())
                {
                    foreach (var filePath in mailInfo.FilePaths)
                    {
                        if (File.Exists(filePath))
                        {
                            Attachment file = new Attachment(filePath);
                            message.Attachments.Add(file);
                        }
                    }
                }
                #endregion

                mailInfo.SmtpClient.Send(message);

                if (message.Attachments.Count > 0)
                {
                    foreach (Attachment t in message.Attachments)
                    {
                        t.Dispose();
                    }
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
