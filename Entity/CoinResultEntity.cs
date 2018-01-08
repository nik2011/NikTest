using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZHome.Entity
{
   public class CoinResultEntity
    {

        /// <summary>
        /// 币种
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 差价
        /// </summary>
        public decimal Margin { get; set; }

        /// <summary>
        /// 占比
        /// </summary>
        public decimal Proportion { get; set; }

    }
}
