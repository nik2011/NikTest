using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SimpleFactory
{
    /// <summary>
    /// 加法
    /// </summary>
   public class OprationAdd:Opration
    {
        
        public override double GetResult()
        {
            return NumberA + NumberB;
        }
    }
}
