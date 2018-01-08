using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.SimpleFactory
{
    /// <summary>
    /// 工厂类
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// 实例化对象
        /// </summary>
        /// <param name="oparate"></param>
        /// <returns></returns>
        public static Opration CreateOprate(string oparate)
        {
            Opration opration = null;
            switch (oparate)
            {
                case "+":; opration = new OprationAdd(); break;
                case "-": opration = new OprationSub(); break;
                case "*": opration = new OprationMul(); break;
                default:
                    break;
            }
            return opration;
        }
    }
}
