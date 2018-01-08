using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;

namespace SZHome.Entity
{
    public class RequestEntity
    {
       public string postUrl { set; get; }
       public string postString { set; get; }
       public string method { set; get; }
       public string strCookie { set; get; }
       public string referer { set; get; }
       public string host { set; get; }
       public CookieContainer cookie { set; get; }

    }
}
