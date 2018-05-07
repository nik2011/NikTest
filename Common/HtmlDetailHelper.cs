using SZHome.Entity;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SZHome.Common;
using SZHomeDLL;
using System.Text.RegularExpressions;

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

            HtmlNode tableNode = doc.DocumentNode.SelectSingleNode("//table[@id='markets']//tbody");
            if (tableNode == null)
            {
                msg = "获取不到数据";
                return false;
            }
            Regex trRegex = new Regex(@"<tr[\s\S]*?添加自选</div>", RegexOptions.IgnoreCase);
            Regex tdRegex = new Regex("<tr><td>[\\d\\*]+<td>([\\s\\S]+?)<td>([^<]+?)<td class=\"price\"[^>]+?data-btc=\"([\\d\\.]+)\"[^>]+?>([^<]+?)<td>([^<]+?)<td class=\"volume\"[^>]+?>([^<]+?)<td>([^<]+?)<td>([\\d\u4e00-\u9fa5]+?)<td>", RegexOptions.IgnoreCase);
            Regex platRegex = new Regex(@"<img[^>]+?>([^>]+?)</a>", RegexOptions.IgnoreCase);
            MatchCollection matchs = trRegex.Matches(tableNode.InnerHtml);
            int i = 0;
            foreach (Match item in matchs)
            {
                string trDetail = item.Value;
                Match tdmatchs = tdRegex.Match(trDetail);
                if (tdmatchs.Success)
                {
                    i++;
                    BitcoinEntity coinEnt = new BitcoinEntity();
                    coinEnt.Id = i + 1;
                    coinEnt.Name = coin;
                    coinEnt.PlatFormHtml = tdmatchs.Groups[1].Value.Replace("/exchange", "https://www.feixiaohao.com/exchange");
                    Match platMatch = platRegex.Match(coinEnt.PlatFormHtml);
                    if (platMatch.Success)
                    {
                        coinEnt.PlatForm = platMatch.Groups[1].Value;
                    }
                    coinEnt.ExchangeType = tdmatchs.Groups[2].Value;
                    string aPrice = tdmatchs.Groups[4].Value;
                    decimal price = 0;
                    decimal btcPrice = 0;
                    if ((aPrice.Contains("¥")))
                    {
                        price = Convert.ToDecimal(aPrice.Replace("¥", "").Replace(",", ""));
                        btcPrice = CommonTools.ChangeDataToD(tdmatchs.Groups[3].Value);
                    }
                    if (price == 0)
                    {
                        msg = "价格为0";
                        continue;
                    }
                    coinEnt.Amout = tdmatchs.Groups[5].Value;
                    coinEnt.TotalPrice = tdmatchs.Groups[6].Value;
                    coinEnt.Percent = Convert.ToDecimal(tdmatchs.Groups[7].Value.Replace("%", ""));
                    coinEnt.Time = tdmatchs.Groups[8].Value;
                    coinEnt.Price = price;
                    coinEnt.BtcPrice = btcPrice;
                    if (platFormList.Contains(coinEnt.PlatForm))
                    {
                        continue;
                    }
                    //该平台已存在
                    if (list.Exists(x => x.PlatForm == coinEnt.PlatForm))
                    {
                        continue;
                    }
                    list.Add(coinEnt);

                }
            }
            //if (trNodeList.Count < 2)
            //{
            //    msg = "数据量不足";
            //    return false;
            //}
            //for (int i = 0; i < trNodeList.Count; i++)
            //{
            //    BitcoinEntity coinEnt = new BitcoinEntity();
            //    HtmlNode trNode= HtmlNode.CreateNode(trNodeList[i].OuterHtml);
            //    HtmlNodeCollection tdNodeList = trNode.SelectNodes("./td");
            //    if (tdNodeList == null || tdNodeList.Count < 9)
            //    {
            //        msg = "html结构有变化";
            //        return false;
            //    }
            //    decimal price =0;
            //    decimal btcPrice =0;
            //    if ((tdNodeList[3].InnerText.Contains("¥")))
            //    {
            //        price = Convert.ToDecimal(tdNodeList[3].InnerText.Replace("¥", "").Replace(",", ""));
            //        HtmlAttributeCollection attrColl = tdNodeList[3].Attributes;
            //        btcPrice = CommonTools.ChangeDataToD(attrColl["data-btc"].Value);
            //    }
            //    if (price==0)
            //    {
            //        msg = "价格为0";
            //        continue;
            //    }
            //    coinEnt.Id = i+1;
            //    coinEnt.Name = coin;
            //    coinEnt.PlatForm = tdNodeList[1].InnerText.ToLower().Trim();
            //    coinEnt.PlatFormHtml = tdNodeList[1].InnerHtml.Replace("/exchange", "https://www.feixiaohao.com/exchange");
            //    coinEnt.ExchangeType = tdNodeList[2].InnerHtml.Replace("/exchange", "https://www.feixiaohao.com/exchange");
            //    coinEnt.Price = price;
            //    coinEnt.BtcPrice = btcPrice;
            //    coinEnt.Amout = tdNodeList[4].InnerText;
            //    coinEnt.TotalPrice = tdNodeList[5].InnerText;
            //    coinEnt.Percent = Convert.ToDecimal(tdNodeList[6].InnerText.Replace("%", ""));
            //    coinEnt.Time = tdNodeList[7].InnerText;
            //    if (platFormList.Contains(coinEnt.PlatForm))
            //    {
            //        continue;
            //    }
            //    //该平台已存在
            //    if (list.Exists(x=>x.PlatForm == coinEnt.PlatForm))
            //    {
            //        continue;
            //    }
            //    list.Add(coinEnt);
            //}
            if (list.Count < 2)
            {
                msg = "数据量不足";
                return false;
            }
            //先按占比排序取前n个，然后按价格排序
            list = list.OrderByDescending(x => x.Percent).Take(count).ToList().OrderByDescending(x => x.BtcPrice).ToList();
            BitcoinEntity firstEnt = list.First();
            BitcoinEntity lastEnt = list.Last();
            decimal margin = firstEnt.BtcPrice - lastEnt.BtcPrice;
            if (firstEnt.BtcPrice>0)
            {
                decimal percent = margin / firstEnt.BtcPrice;
                if (percent >= defaultPrecent)
                {
                    resultList = list;
                    coinResultList.Add(new CoinResultEntity()
                    {
                        Name = coin,
                        Margin = CommonTools.ChangeDataToD(margin.ToString()),
                        Proportion = Convert.ToDecimal((percent * 100).ToString("f2"))
                    });
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// coin排序
        /// </summary>
        /// <param name="list"></param>
        /// <param name="coinList"></param>
        /// <returns></returns>
        public static List<BitcoinEntity> OrderCoinList(List<BitcoinEntity> list, List<CoinResultEntity> coinList)
        {
            List<BitcoinEntity> resultList = new List<BitcoinEntity>();
            foreach (var item in coinList)
            {
                resultList.AddRange(list.Where(x => x.Name == item.Name));
            }
            return resultList;
        }

        /// <summary>
        /// 处理平台html数据
        /// </summary>
        /// <param name="htmlDetail"></param>
        /// <param name="sourceId"></param>
        public static bool HandlePlatFornHtml(string platForm, string htmlDetail, ref List<BitcoinEntity> resultList, ref string msg)
        {
            htmlDetail = CommonTools.Compress(htmlDetail);
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(htmlDetail);

            HtmlNode tableNode = doc.DocumentNode.SelectSingleNode("//table[@class='table noBg']//tbody");
            if (tableNode == null)
            {
                msg = "获取不到数据";
                return false;
            }
            Regex trRegex = new Regex(@"<tr[\s\S]*?添加自选</div>", RegexOptions.IgnoreCase);
            Regex tdRegex = new Regex("<tr><td>[\\d\\*]+<td>([\\s\\S]+?)<td>([^<]+?)<td class=\"price\"[^>]+?data-btc=\"([\\d\\.]+)\"[^>]+?>([^<]+?)<td>([^<]+?)<td class=\"volume\"[^>]+?>([^<]+?)<td>([^<]+?)<td>([\\d\u4e00-\u9fa5]+?)<td>", RegexOptions.IgnoreCase);
            Regex platRegex = new Regex(@"<img[^>]+?>([^>]+?)</a>", RegexOptions.IgnoreCase);
            MatchCollection matchs = trRegex.Matches(tableNode.InnerHtml);
            int i = 0;
            foreach (Match item in matchs)
            {
                string trDetail = item.Value;
                Match tdmatchs = tdRegex.Match(trDetail);
                if (tdmatchs.Success)
                {
                    i++;
                    BitcoinEntity coinEnt = new BitcoinEntity();
                    coinEnt.Id = i + 1;
                    coinEnt.PlatForm =platForm;
                    coinEnt.NameHtml = tdmatchs.Groups[1].Value.Replace("/exchange", "https://www.feixiaohao.com/exchange");
                    Match platMatch = platRegex.Match(coinEnt.NameHtml);
                    if (platMatch.Success)
                    {
                        coinEnt.Name = platMatch.Groups[1].Value;
                    }
                    coinEnt.ExchangeType = tdmatchs.Groups[2].Value;
                    string aPrice = tdmatchs.Groups[4].Value;
                    decimal price = 0;
                    decimal btcPrice = 0;
                    if ((aPrice.Contains("¥")))
                    {
                        price = Convert.ToDecimal(aPrice.Replace("¥", "").Replace(",", ""));
                        btcPrice = CommonTools.ChangeDataToD(tdmatchs.Groups[3].Value);
                    }
                    if (price == 0)
                    {
                        msg = "价格为0";
                        continue;
                    }
                    coinEnt.Amout = tdmatchs.Groups[5].Value;
                    coinEnt.TotalPrice = tdmatchs.Groups[6].Value;
                    coinEnt.Percent = Convert.ToDecimal(tdmatchs.Groups[7].Value.Replace("%", ""));
                    coinEnt.Time = tdmatchs.Groups[8].Value;
                    coinEnt.Price = price;
                    coinEnt.BtcPrice = btcPrice;
                    string nameEnglish = "";
                    string[] arr = coinEnt.ExchangeType.Split('/');
                    if (arr.Length == 2)
                    {
                        nameEnglish = arr[0].ToUpper();
                    }
                    coinEnt.NameEnglish = nameEnglish;
                    if (resultList.Exists(x => x.NameEnglish.ToLower() == coinEnt.NameEnglish.ToLower()))
                    {
                        continue;
                    }
                    resultList.Add(coinEnt);
                }
            }

            //for (int i = 0; i < trNodeList.Count; i++)
            //{
            //    BitcoinEntity coinEnt = new BitcoinEntity();
            //    HtmlNodeCollection tdNodeList = trNodeList[i].ChildNodes;
            //    if (tdNodeList == null || tdNodeList.Count < 9)
            //    {
            //        msg = "html结构有变化";
            //        return false;
            //    }
            //    decimal price = 0;
            //    decimal btcPrice = 0;
            //    if ((tdNodeList[3].InnerText.Contains("¥")))
            //    {
            //        price = Convert.ToDecimal(tdNodeList[3].InnerText.Replace("¥", "").Replace(",", ""));
            //        HtmlAttributeCollection attrColl = tdNodeList[3].Attributes;
            //        btcPrice = CommonTools.ChangeDataToD(attrColl["data-btc"].Value);
            //    }
            //    string name = tdNodeList[1].InnerText.ToLower().Trim();

            //    coinEnt.Id = i + 1;
            //    coinEnt.PlatForm = platForm;
            //    coinEnt.Name = name;
            //    coinEnt.NameHtml = tdNodeList[1].InnerHtml.Replace("/currencies", "https://www.feixiaohao.com/currencies");
            //    coinEnt.ExchangeType = tdNodeList[2].InnerText.Trim();
            //    coinEnt.Price = price;
            //    coinEnt.BtcPrice = btcPrice;
            //    coinEnt.Amout = tdNodeList[4].InnerText;
            //    coinEnt.TotalPrice = tdNodeList[5].InnerText;
            //    coinEnt.Percent = Convert.ToDecimal(tdNodeList[6].InnerText.Replace("%", ""));
            //    coinEnt.Time = tdNodeList[7].InnerText;
            //    string nameEnglish = "";
            //    string[] arr = coinEnt.ExchangeType.Split('/');
            //    if (arr.Length == 2)
            //    {
            //        nameEnglish = arr[0].ToUpper();
            //    }
            //    coinEnt.NameEnglish = nameEnglish;
            //    //if (!coinEnt.ExchangeType.ToUpper().Contains("BTC"))
            //    //{
            //    //    continue;
            //    //}
            //    if (resultList.Exists(x => x.NameEnglish.ToLower() == coinEnt.NameEnglish.ToLower()))
            //    {
            //        continue;
            //    }
            //    resultList.Add(coinEnt);
            //}
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
            var nameList = platCoinlist.GroupBy(x => x.NameEnglish).Select(x => (new { nameEnglish = x.Key, count = x.Count() })).ToList();
            foreach (var item in nameList)
            {
                if (item.count < 2)
                {
                    continue;
                }
                PlatResultEntity result = new PlatResultEntity();
                List<BitcoinEntity> selectList = platCoinlist.Where(x => x.NameEnglish == item.nameEnglish).ToList();
                if (selectList.Count < 2)
                {
                    continue;
                }
                decimal price1 = selectList[0].BtcPrice;
                decimal price2 = selectList[1].BtcPrice;
                if (selectList[0].NameEnglish.ToLower() == "btc")
                {
                    price1 = selectList[0].Price;
                    price2 = selectList[1].Price;
                }
                if (price1 == 0 || price2 == 0)
                {
                    continue;
                }
                decimal margin = price1 - price2;
                decimal percent = margin / price1;
                if (Math.Abs(percent) < defaultPrecent)
                {
                    continue;
                }
                decimal p1 = (selectList[1].Price / selectList[1].BtcPrice) * selectList[0].BtcPrice;
                if (selectList[0].NameEnglish.ToLower() == "btc")
                {
                    p1 = selectList[0].Price;
                }
                result.NameHtml = selectList[0].NameHtml;
                result.ExchangeType = $"{selectList[0].ExchangeType}({selectList[0].PlatForm})-{selectList[1].ExchangeType}({selectList[1].PlatForm})";
                result.Price = $"{selectList[0].BtcPrice}(¥{p1.ToString("f2")})/{selectList[1].BtcPrice}(¥{selectList[1].Price})";
                result.Margin = CommonTools.ChangeDataToD(margin.ToString());
                result.Percent = Convert.ToDecimal((percent * 100).ToString("f2"));
                result.Amout = selectList[0].Amout + "/" + selectList[1].Amout;
                result.TotalPrice = selectList[0].TotalPrice + "/" + selectList[1].TotalPrice;
                result.Time = selectList[0].Time + "/" + selectList[1].Time;

                result.Proportion = selectList[0].Percent + "/" + selectList[1].Percent;
                resultList.Add(result);
                if (result.Percent >= 5 && coinsSmsList.Contains(selectList[0].NameEnglish.ToLower()))
                {
                    string phone = "13751131731";
                    string msg = $"恭喜您，报名成功：{selectList[0].NameEnglish.Substring(0, 1)}-{result.Percent}";
                    //SmsService.SendSMS(phone,msg);
                }
            }
            resultList = resultList.OrderByDescending(x => x.Percent).ToList();

            return resultList;
        }

    }
}
