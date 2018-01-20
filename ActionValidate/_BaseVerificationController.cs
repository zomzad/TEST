//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.ComponentModel;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//using LightSpeed.B2C.Utility;
//using Newtonsoft.Json;

//namespace LightSpeed.B2C.Travel.Web
//{
//    public enum EnumSessionDirectUrl
//    {
//        [Description("Search")]
//        Controller,
//        [Description("Index")]
//        Action,
//        [Description("Account")]
//        MemberController,
//        [Description("Login")]
//        MemberAction
//    }

//    public enum EnumReleaseAction
//    {
//        Choose,
//        Passenger,
//        Emergency
//    }

//    public enum EnumPdfConvertAction
//    {
//        Completed
//    }
//}

//namespace LightSpeed.B2C.Travel.Web.Controllers
//{
//    public class _BaseVerificationController : Controller
//    {
        
//    }

//    public class TimeOutActionFilter : ActionFilterAttribute
//    {
//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            #region - 檢驗訂單編號 -
//            bool orderInvalid = true;
//            bool isPdfCinvert = true;
//            var requestFormDic = ToDictionary(filterContext.HttpContext.Request.Form);

//            if (Enum.IsDefined(typeof(EnumPdfConvertAction), filterContext.ActionDescriptor.ActionName) &&
//                filterContext.ActionParameters["pdfFlag"] != null)
//            {
//                isPdfCinvert = filterContext.ActionParameters["pdfFlag"].ToString().Equals("Y", StringComparison.CurrentCultureIgnoreCase) == false;
//            }

//            if (requestFormDic.ContainsKey("OrderID") ||
//                (requestFormDic.ContainsKey("sOrdr") && requestFormDic.ContainsKey("sYear")))
//            {
//                orderInvalid = requestFormDic.ContainsKey("OrderID") 
//                    ? string.IsNullOrWhiteSpace(requestFormDic["OrderID"]) 
//                    : string.IsNullOrWhiteSpace(requestFormDic["sOrdr"]) && string.IsNullOrWhiteSpace(requestFormDic["sYear"]);
//            }
//            #endregion

//            HttpContext httpContext = HttpContext.Current;

//            #region - 判斷是否有登入資訊 -
//            if (isPdfCinvert &&
//                httpContext.Session != null &&
//                httpContext.Session["LionUID"] == null)
//            {
//                if (Enum.IsDefined(typeof(EnumReleaseAction), filterContext.ActionDescriptor.ActionName))
//                {
//                    //view中的js加上下面這段，就可以執行這邊的語法
//                    //var loginBoxPopUp = @Html.Raw(ViewBag.LoginBoxPopUp);
//                    //if (loginBoxPopUp !== '')
//                    //{
//                    //    @Html.Raw(ViewBag.LoginBoxPopUp);
//                    //}

//                    filterContext.Controller.ViewBag.LoginBoxPopUp =
//                        "setTimeout(function () { $('#lionLogin').trigger('open', ['login']); }, 1000);" +
//                        "\n$('div#lionLogin').attr({ 'data-backdrop': 'static' })";
//                }
//                else
//                {
//                    RedirecteLogIn(filterContext, orderInvalid);
//                }
//            }
//            #endregion

//            base.OnActionExecuting(filterContext);
//        }

//        private void RedirecteLogIn(ActionExecutingContext filterContext, bool orderInvalid)
//        {
//            if (orderInvalid)
//            {
//                RouteValueDictionary routeValues = new RouteValueDictionary
//                    (new
//                    {
//                        controller = EnumHelper.GetEnumDescription(EnumSessionDirectUrl.Controller),
//                        action = EnumHelper.GetEnumDescription(EnumSessionDirectUrl.Action)
//                    });
//                filterContext.Result = new RedirectToRouteResult(routeValues);
//            }
//            else
//            {
//                filterContext.Result = new RedirectToRouteResult(
//                    new RouteValueDictionary(
//                        new
//                        {
//                            controller = "Order",
//                            action = "RedirectLoggedInPage"
//                        }));
//            }
//        }

//        private static IDictionary<string, string> ToDictionary(NameValueCollection data)
//        {
//            IDictionary<string, string> dict = new Dictionary<string, string>();
//            foreach (var k in data.AllKeys)
//            {
//                dict.Add(k, data[k]);
//            }
//            return dict;
//        }
//    }
//}
