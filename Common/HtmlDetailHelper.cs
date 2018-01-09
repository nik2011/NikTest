using SZHome.Entity;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZHome.Common;
using SZHomeDLL;

namespace SZHome.Common
{
    public class HtmlDetailHelper
    {
        /// <summary>
        /// 处理币html数据
        /// </summary>
        /// <param name="htmlDetail"></param>
        /// <param name="sourceId"></param>
        public static bool HandleCoinHtml(string coin, string htmlDetail, ref List<BitcoinEntity> resultList, ref List<CoinResultEntity> coinResultList, ref string msg
            , int count = 5, decimal defaultPrecent = (decimal)0.1)
        {
            htmlDetail = CommonTools.Compress(htmlDetail);
            List<BitcoinEntity> list = new List<BitcoinEntity>();
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlDetail);
            List<string> platFormList = ConfigHelper.GetConfigString("PlatForms").ToLower().ToStringList();

            HtmlNodeCollection trNodeList = doc.DocumentNode.SelectNodes("//table[@id='markets']//tbody//tr");
            if (trNodeList == null || trNodeList.Count == 0)
            {
                msg = "获取不到数据";
                return false;
            }
            if (trNodeList.Count < 2)
            {
                msg = "数据量不足";
                return false;
            }
            for (int i = 0; i < trNodeList.Count; i++)
            {
                BitcoinEntity coinEnt = new BitcoinEntity();
                HtmlNodeCollection tdNodeList = trNodeList[i].ChildNodes;//SelectNodes("//td");
                if (tdNodeList == null || tdNodeList.Count < 9)
                {
                    msg = "html结构有变化";
                    return false;
                }
                decimal price = (decimal)0;
                if ((tdNodeList[3].InnerText.Contains("¥")))
                {
                    price = Convert.ToDecimal(tdNodeList[3].InnerText.Replace("¥", "").Replace(",", ""));
                }
                if (price==0)
                {
                    msg = "价格为0";
                    return false;
                }
                coinEnt.Id = i+1;
                coinEnt.Name = coin;
                coinEnt.PlatForm = tdNodeList[1].InnerText.ToLower().Trim();
                coinEnt.PlatFormHtml = tdNodeList[1].InnerHtml.Replace("/exchange", "https://www.feixiaohao.com/exchange");
                coinEnt.ExchangeType = tdNodeList[2].InnerHtml.Replace("/exchange", "https://www.feixiaohao.com/exchange");
                coinEnt.Price = price;
                coinEnt.Amout = tdNodeList[4].InnerText;
                coinEnt.TotalPrice = tdNodeList[5].InnerText;
                coinEnt.Percent = Convert.ToDecimal(tdNodeList[6].InnerText.Replace("%", ""));
                coinEnt.Time = tdNodeList[7].InnerText;
                if (platFormList.Contains(coinEnt.PlatForm))
                {
                    continue;
                }
                //该平台已存在
                if (list.Exists(x=>x.PlatForm == coinEnt.PlatForm))
                {
                    continue;
                }
                list.Add(coinEnt);
            }
            if (list.Count < 2)
            {
                msg = "数据量不足";
                return false;
            }
            //先按占比排序取前n个，然后按价格排序
            list = list.OrderByDescending(x => x.Percent).Take(count).ToList().OrderByDescending(x => x.Price).ToList();
            BitcoinEntity firstEnt = list.First();
            BitcoinEntity lastEnt = list.Last();
            decimal margin=Convert.ToDecimal((firstEnt.Price - lastEnt.Price).ToString("f2"));
            decimal percent =margin / firstEnt.Price;
            if (percent >= defaultPrecent)
            {
                resultList = list;
                coinResultList.Add(new CoinResultEntity() {
                  Name=coin,
                  Margin=margin,
                  Proportion=Convert.ToDecimal((percent * 100).ToString("f2"))
                });
               // summry =$"{coin}: 最大差价{margin}（{(percent*100).ToString("f2")}%）";
                return true;
            }
            return false;
        }

        /// <summary>
        /// coin排序
        /// </summary>
        /// <param name="list"></param>
        /// <param name="coinList"></param>
        /// <returns></returns>
        public static List<BitcoinEntity> OrderCoinList(List<BitcoinEntity> list,List<CoinResultEntity> coinList)
        {
            List<BitcoinEntity> resultList = new List<BitcoinEntity>();
            foreach (var item in coinList)
            {
              resultList.AddRange(list.Where(x=>x.Name==item.Name));
            }
            return resultList;
        }


        /// <summary>
        /// 处理平台html数据
        /// </summary>
        /// <param name="htmlDetail"></param>
        /// <param name="sourceId"></param>
        public static bool HandlePlatFornHtml(string platForm, string htmlDetail, ref List<BitcoinEntity> resultList,ref string msg)
        {
            htmlDetail = CommonTools.Compress(htmlDetail);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlDetail);

            HtmlNodeCollection trNodeList = doc.DocumentNode.SelectNodes("//table[@class='table noBg']//tbody//tr");
            if (trNodeList == null || trNodeList.Count == 0)
            {
                //LogHelper.LogInfo(coin+ " 获取不到数据");
                msg = "获取不到数据";
                return false;
            }
            for (int i = 0; i < trNodeList.Count; i++)
            {
                BitcoinEntity coinEnt = new BitcoinEntity();
                HtmlNodeCollection tdNodeList = trNodeList[i].ChildNodes;//SelectNodes("//td");
                if (tdNodeList == null || tdNodeList.Count < 9)
                {
                    msg = "html结构有变化";
                    return false;
                }
                decimal  price=(decimal)0;
                if ((tdNodeList[3].InnerText.Contains("¥")))
                {
                    price= Convert.ToDecimal(tdNodeList[3].InnerText.Replace("¥", "").Replace(",", ""));
                }
                string name= tdNodeList[1].InnerText.ToLower().Trim();
               
                coinEnt.Id = i + 1;
                coinEnt.PlatForm = platForm;
                coinEnt.Name = name;
                coinEnt.NameHtml = tdNodeList[1].InnerHtml.Replace("/currencies", "https://www.feixiaohao.com/currencies");
                coinEnt.ExchangeType = tdNodeList[2].InnerText.Trim();
                coinEnt.Price = price;
                coinEnt.Amout = tdNodeList[4].InnerText;
                coinEnt.TotalPrice = tdNodeList[5].InnerText;
                coinEnt.Percent = Convert.ToDecimal(tdNodeList[6].InnerText.Replace("%", ""));
                coinEnt.Time = tdNodeList[7].InnerText;
                string nameEnglish = "";
                string[] arr = coinEnt.ExchangeType.Split('/');
                if (arr.Length == 2)
                {
                    nameEnglish = arr[0].ToUpper();
                }
                coinEnt.NameEnglish = nameEnglish;
                //if (!coinEnt.ExchangeType.ToUpper().Contains("BTC"))
                //{
                //    continue;
                //}
                if (resultList.Exists(x => x.NameEnglish.ToLower() == coinEnt.NameEnglish.ToLower()))
                {
                    continue;
                }
                resultList.Add(coinEnt);
            }
            return true;
        }

        /// <summary>
        /// 比较币数据
        /// </summary>
        /// <param name="platCoinlist"></param>
        /// <returns></returns>
        public static List<PlatResultEntity> CompareCoinData(List<BitcoinEntity> platCoinlist, decimal defaultPrecent = (decimal)0.1)
        {
            List<PlatResultEntity> resultList = new List<PlatResultEntity>();
            //发短信的币
            List<string> coinsSmsList = ConfigHelper.GetConfigString("CoinsSms").ToLower().ToStringList();
            var nameList=platCoinlist.GroupBy(x=>x.NameEnglish).Select(x=>(new { nameEnglish = x.Key,count=x.Count()})).ToList();
            foreach (var item in nameList)
            {
                if (item.count<2) {
                    continue;
                }
                PlatResultEntity result = new PlatResultEntity();
                List<BitcoinEntity> selectList= platCoinlist.Where(x=>x.NameEnglish == item.nameEnglish).ToList();
                if (selectList.Count < 2)
                {
                    continue;
                }
                decimal price1 = selectList[0].Price;
                decimal price2= selectList[1].Price;
                if (price1==0||price2==0)
                {
                    continue;
                }
                decimal margin = Convert.ToDecimal((price1 - price2).ToString("f2"));
                decimal percent = margin / price1;
                if (Math.Abs(percent) < defaultPrecent)
                {
                    continue;
                }
                result.NameHtml = selectList[0].NameHtml;
                result.ExchangeType = selectList[0].ExchangeType+"-"+ selectList[1].ExchangeType;
                result.Price = selectList[0].Price + "/" + selectList[1].Price;
                result.Margin = margin;
                result.Percent=Convert.ToDecimal((percent * 100).ToString("f2"));
                result.Amout= selectList[0].Amout+"/"+ selectList[1].Amout;
                result.TotalPrice= selectList[0].TotalPrice+"/"+ selectList[1].TotalPrice;
                result.Time = selectList[0].Time+"/"+ selectList[1].Time;
                result.Proportion = selectList[0].Percent+"/"+selectList[1].Percent;
                resultList.Add(result);
                if (percent>=10&&coinsSmsList.Contains(selectList[0].NameEnglish.ToLower()))
                {
                    string phone = "13751131731";
                    string msg = $"恭喜您，报名成功：{selectList[0].NameEnglish.Substring(0, 1)}-{percent.ToString("f0")}" ;
                    SmsService.SendSMS(phone,msg);
                }
            }
            resultList = resultList.OrderByDescending(x=>x.Percent).ToList();

            return resultList;
        }

    }
}
