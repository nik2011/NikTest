using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZHome.Entity
{
   public class PlatResultEntity
    {
        /// <summary>
        /// 币种
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string NameHtml { get; set; }

        /// <summary>
        /// 交易对
        /// </summary>
        public string ExchangeType { get; set; }

        /// <summary>
        /// 当前价格
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// 差价
        /// </summary>
        public decimal Margin { get; set; }

        /// <summary>
        /// 幅度
        /// </summary>
        public decimal Percent { get; set; }

        /// <summary>
        /// 成交量
        /// </summary>
        public string Amout { get; set; }

        /// <summary>
        /// 成交额
        /// </summary>
        public string TotalPrice { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public string Proportion
        { get; set; }
      
    }
}
