using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SZHome.Entity
{
   [Serializable]
   public class msmqEntity
    {
       /// <summary>
       /// 消息
       /// </summary>
       public string message { get; set; }

       /// <summary>
       /// 时间
       /// </summary>
       public DateTime  date { get; set; }
    }
}
