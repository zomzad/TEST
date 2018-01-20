using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleCode.Models
{
    /// <summary>
    /// 公車基本資料
    /// </summary>
    public class BusStation
    {
        /// <summary>
        /// 車牌號碼
        /// </summary>
        public string PlateNumb { get; set; }
        /// <summary>
        /// 站牌唯一識別代碼
        /// </summary>
        public string StopUID { get; set; }
        /// <summary>
        /// 地區既用中之站牌代碼
        /// </summary>
        public string StopID { get; set; }
        /// <summary>
        /// 站牌名
        /// </summary>
        public NameType StopName { get; set; }
        /// <summary>
        /// 路線唯一識別代碼
        /// </summary>
        public string RouteUID { get; set; }
        /// <summary>
        /// 地區既用中之路線代碼
        /// </summary>
        public string RouteID { get; set; }
        /// <summary>
        /// 路線名稱
        /// </summary>
        public NameType RouteName { get; set; }
        /// <summary>
        /// 子路線唯一識別代碼
        /// </summary>
        public string SubRouteUID { get; set; }
        /// <summary>
        /// 地區既用中之子路線代碼
        /// </summary>
        public string SubRouteID { get; set; }
        /// <summary>
        /// 子路線名稱 
        /// </summary>
        public NameType SubRouteName { get; set; }
        /// <summary>
        /// 去返程['0: 去程', '1: 返程']
        /// </summary>
        public string Direction { get; set; }
        /// <summary>
        /// 到站時間預估(秒)
        /// </summary>
        public int EstimateTime { get; set; }
        /// <summary>
        /// 車輛距離本站站數 
        /// </summary>
        public string StopCountDown { get; set; }
        /// <summary>
        /// 車輛目前所在站牌代碼 
        /// </summary>
        public string CurrentStop { get; set; }
        /// <summary>
        /// 車輛目的站牌代碼
        /// </summary>
        public string DestinationStop { get; set; }
        /// <summary>
        /// 路線經過站牌之順序
        /// </summary>
        public int StopSequence { get; set; }
        /// <summary>
        /// 車輛狀態備註 = ['1: 尚未發車', '2: 交管不停靠', '3: 末班車已過', '4: 今日未營運']
        /// </summary>
        public string StopStatus { get; set; }
        /// <summary>
        /// 資料型態種類 = ['0: 未知', '1: 定期', '2: 非定期']
        /// </summary>
        public string MessageType { get; set; }
        /// <summary>
        /// 下一班公車到達時間(ISO8601格式:yyyy-MM-ddTHH:mm:sszzz) 
        /// </summary>
        public string NextBusTime { get; set; }
        /// <summary>
        /// 是否為末班車
        /// </summary>
        public bool IsLastBus { get; set; }
        /// <summary>
        /// 車機資料傳輸時間(ISO8601格式:yyyy-MM-ddTHH:mm:sszzz) 
        /// </summary>
        public DateTime TransTime { get; set; }
        /// <summary>
        /// 來源端平台接收時間(ISO8601格式:yyyy-MM-ddTHH:mm:sszzz) 
        /// </summary>
        public DateTime SrcRecTime { get; set; }
        /// <summary>
        /// 來源端平台接收時間(ISO8601格式:yyyy-MM-ddTHH:mm:sszzz) 
        /// </summary>
        public DateTime SrcUpdateTime { get; set; }
        /// <summary>
        /// 本平台資料更新時間(ISO8601格式:yyyy-MM-ddTHH:mm:sszzz)
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }

    public class NameType
    {
        public string Zh_tw { get; set; }
        public string En { get; set; }
    }
}