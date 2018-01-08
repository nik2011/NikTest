using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using SZHome.Entity;

namespace SZHome.Filter
{
    /// <summary>
    /// Ajax的过滤器 多发点
    /// </summary>
    public class AjaxFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string sheader = filterContext.HttpContext.Request.Headers["X-Requested-With"];
            bool isAjaxRequest = (sheader != null && sheader == "XMLHttpRequest") ? true : false;
            if (!isAjaxRequest)   //判断请求是否为ajax
            {
                ContentResult contentResult = new ContentResult();
                HandleResult handleResult = new HandleResult(500, "非法请求");
                contentResult.Content = JsonConvert.SerializeObject(handleResult);
                // contentResult.ContentType = "application/json";
                filterContext.Result = contentResult;
            }
        }
    }
}
