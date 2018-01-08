using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.SimpleFactory;


namespace SimpleFactory
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Console.Write("请输入数字A：");
            string numA = Console.ReadLine();
            Console.Write("请输入运算符：");
            string oparate = Console.ReadLine();
            Console.Write("请输入数字B：");
            string numB = Console.ReadLine();
            double result = 0.0;
            Opration opration = Factory.CreateOprate(oparate);
            opration.NumberA =Convert.ToDouble(numA);
            opration.NumberB =Convert.ToDouble(numB);
            result = opration.GetResult();

            Console.WriteLine("结果：" + result);
            Console.ReadKey();
        }
    }
}
