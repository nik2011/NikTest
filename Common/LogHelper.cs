using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SZHome.Common
{
    public class LogHelper
    {

        private static object obj = new object();

        /// <summary>        
        /// 记录日志文件        
        /// </summary>        
        /// <param name="message">日志内容</param>        
        public static void LogInfo(string message)
        {
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileDic = currentPath + "\\Log";
            string filePath = fileDic + "\\" + file_name;
            lock (obj)
            {
                if (!Directory.Exists(fileDic))
                {
                    Directory.CreateDirectory(fileDic);
                }
                using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    writer.Write("\r\n"+DateTime.Now+"："+message);
                }
            }
        }

        /// <summary>
        /// 记录异常日志
        /// </summary>
        /// <param name="message">异常内容</param>
        public static void LogTrace(string message)
        {
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileDic = currentPath + "\\Trace";
            string filePath = fileDic + "\\" + file_name;

            lock (obj)
            {

                if (!Directory.Exists(fileDic))
                {
                    Directory.CreateDirectory(fileDic);
                }
                using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    writer.Write("\r\n" + DateTime.Now + "：" + message);
                }

            }

        }
        /// <summary>
        /// 写入信息
        /// </summary>
        /// <param name="foldName">文件夹</param>
        /// <param name="message"></param>
        public static void LogMessage(string foldName, string message)
        {
            string file_name = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            string currentPath = AppDomain.CurrentDomain.BaseDirectory;
            string fileDic = currentPath + "\\" + foldName;
            string filePath = fileDic + "\\" + file_name;

            lock (obj)
            {

                if (!Directory.Exists(fileDic))
                {
                    Directory.CreateDirectory(fileDic);
                }
                using (StreamWriter writer = new StreamWriter(filePath, true, Encoding.UTF8))
                {
                    writer.Write("\r\n" + DateTime.Now + "：" + message);
                }

            }

        }
    }
}
