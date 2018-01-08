using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHome.Common;
using SZHome.Entity;
using SZHomeDLL;

namespace web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            string coins = ConfigHelper.GetConfigString("coins");
            List<string> coinList = coins.ToStringList();
            ViewBag.coinList = coinList;
            return View();
        }

        /// <summary>
        /// 获取币数据详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetCoinList(string coins,int count,decimal percent)
        {
            HandleResult hr = new HandleResult();
            List<BitcoinEntity> resultList = new List<BitcoinEntity>();
            if (string.IsNullOrWhiteSpace(coins))
            {
               coins = ConfigHelper.GetConfigString("coins");
            }
            // int count =ConvertHelper.StringToInt(ConfigHelper.GetConfigString("coinsCount"));
            //decimal percent =Convert.ToDecimal(ConfigHelper.GetConfigString("coinsPercent"));
            List<string> coinList = coins.ToStringList();
            foreach (var item in coinList)
            {
                RequestEntity requestEnt = new RequestEntity();
                requestEnt.postUrl = "https://www.feixiaohao.com/currencies/"+item;
                requestEnt.method = "get";
                requestEnt.host = "www.feixiaohao.com";
                string htmlDetail = RequestHelper.ImplementOprater(requestEnt);
                string msg = "";
                List<BitcoinEntity> list = new List<BitcoinEntity>();
                string summry = "";
                bool matchResult = HtmlDetailHelper.HandleCoinHtml(item, htmlDetail, ref list,ref summry,ref msg,count,percent);
                if (!matchResult)
                {
                    LogHelper.LogInfo(item +": "+msg);
                    continue;
                }
                hr.Other += summry+"  ";
                resultList.AddRange(list);
            }
            hr.StatsCode = 200;
            hr.Message = "成功";
            hr.Data = resultList;
            return Json(hr);
        }

        /// <summary>
        /// 平台数据
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PlatForm()
        {
            string platForms = ConfigHelper.GetConfigString("ComparePlatForms");
            List<string> platList = platForms.ToStringList();
            ViewBag.platList = platList;
            return View();
        }

        /// <summary>
        /// 获取平台数据详情
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetPlatFormList(string platForms,decimal percent)
        {
            HandleResult hr = new HandleResult();
            List<Dictionary<string,object>> resultList = new List<Dictionary<string, object>>();

            List<BitcoinEntity> platCoinlist = new List<BitcoinEntity>();
            List<string> platFormList = platForms.ToStringList();
            foreach (var item in platFormList)
            {
                RequestEntity requestEnt = new RequestEntity();
                requestEnt.postUrl = "https://www.feixiaohao.com/exchange/" + item;
                requestEnt.method = "get";
                requestEnt.host = "www.feixiaohao.com";
                string htmlDetail = RequestHelper.ImplementOprater(requestEnt);
                string msg = "";
                List<BitcoinEntity> list = new List<BitcoinEntity>();
                bool matchResult = HtmlDetailHelper.HandlePlatFornHtml(item, htmlDetail,ref list,ref msg);
                if (!matchResult)
                {
                    LogHelper.LogInfo(item + ": " + msg);
                    continue;
                }
                platCoinlist.AddRange(list);
            }
            resultList = HtmlDetailHelper.CompareCoinData(platCoinlist, percent);
            hr.StatsCode = 200;
            hr.Message = "成功";
            hr.Data = resultList;
            return Json(hr);
        }

        /// <summary>
        /// 实现简单的实时消息推送
        /// </summary>
        /// <returns></returns>
        public ActionResult ClientChat()
        {
            return View();
        }


    }
}