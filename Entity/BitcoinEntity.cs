using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZHome.Entity
{
   public class BitcoinEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 币种
        /// </summary>
        public string NameEnglish { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string NameHtml { get; set; }

        /// <summary>
        /// 交易所
        /// </summary>
        public string PlatForm { get; set; }

        /// <summary>
        /// 交易所
        /// </summary>
        public string PlatFormHtml { get; set; }

        /// <summary>
        /// 交易对
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        /// 当前人民币价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// btc价格
        /// </summary>
        public decimal BtcPrice { get; set; }

        /// <summary>
        /// 成交量
        /// </summary>
        public string Amout { get; set; }


        /// <summary>
        /// 成交额
        /// </summary>
        public string TotalPrice { get; set; }


        /// <summary>
        /// 占比
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string Time { get; set; }

    }
}
