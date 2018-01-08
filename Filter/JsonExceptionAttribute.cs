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
    /// 异步错误的过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class JsonExceptionAttribute : HandleErrorAttribute
    {

        public override void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                //返回异常JSON
                filterContext.Result = new JsonResult
                {
                    Data = new HandleResult(500, "500系统错误：" + filterContext.Exception.Message)
                    //Message可以自定义内容 
                };
            }
        }
    }
}
