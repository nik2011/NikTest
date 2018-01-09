using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZHome.Common
{
   public class SmsService
    {
        private static int appId = 12;
        private static string token = "aaO4LOc4zDhxv4dqG81o2uh8N0w2R3vw";

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static int SendSMS(string phone, string message)
        {
            com.szhome.inside.smsservice.sms_ws ws = new com.szhome.inside.smsservice.sms_ws();
            message += "【咚咚找房】";// 短信签名
            return ws.SendMessage(appId, token, phone, message);
        }
    }
}
