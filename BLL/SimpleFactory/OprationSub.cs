using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SimpleFactory
{
   /// <summary>
   /// 减法
   /// </summary>
   public class OprationSub:Opration
    {
        public override double GetResult()
        {
            return NumberA - NumberB;
        }
    }
}
