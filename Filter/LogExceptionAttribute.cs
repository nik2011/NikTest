using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace SZHome.Filter
{
    /// <summary>
    /// 统一处理异常错误过滤器
    /// </summary>   
    ///[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class LogExceptionAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {

            if (!filterContext.ExceptionHandled)
            {
                HttpRequest Request = System.Web.HttpContext.Current.Request;
                string strRef = "";  //错误发生的action
                if (Request.UrlReferrer != null)
                {
                    strRef = Request.UrlReferrer.ToString();
                }

                //记录错误日志
                //BLL.ErrorLogBLL.SaveErrorLog(filterContext.Exception, Request.RawUrl, "", strRef);
                Common.LogHelper.LogTrace(Request.Url + filterContext.Exception.ToString()+filterContext.Exception.Message);
            }

            if (filterContext.Result is JsonResult)
            {
                //当结果为json时，设置异常已处理 （500系统错误）
                filterContext.ExceptionHandled = true;
            }
            else
            {
                //否则调用原始设置（页面初始化错误）
                base.OnException(filterContext);
            }

        }
    }
}
