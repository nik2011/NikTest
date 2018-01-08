using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JiSuanQi
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("请输入数字A：");
            string numA= Console.ReadLine();
            Console.Write("请输入运算符：");
            string oparate = Console.ReadLine();
            Console.Write("请输入数字B：");
            string numB = Console.ReadLine();
            double result =0.0;
            switch (oparate)
            {
                case "+": result = Convert.ToDouble(numA) + Convert.ToDouble(numB); break;
                case "-": result = Convert.ToDouble(numA) - Convert.ToDouble(numB); break;
                case "*": result = Convert.ToDouble(numA) * Convert.ToDouble(numB); break;
                case "/": result = Convert.ToDouble(numA) / Convert.ToDouble(numB); break;
                default:
                    break;
            }
            Console.WriteLine("结果："+result);
            Console.ReadKey();

        }


    }
}
