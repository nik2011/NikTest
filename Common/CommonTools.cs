using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.IO;
using SZHomeDLL;
using Newtonsoft.Json;
using System.Net;
using System.Text.RegularExpressions;
using System.IO.Compression;
using System.Drawing;


namespace SZHome.Common
{
    public class CommonTools
    {
        private static string writepath = ConfigHelper.GetConfigString("path") + "log.txt";
        private static object objLock = new object();

        /// <summary>
        /// 压缩HTML
        /// </summary>
        /// <returns></returns>
        public static string Compress(string strHTML)
        {
            strHTML = strHTML.Replace("\r\n", "").Replace("\r", "").Replace("\n", "").Replace("&nbsp;", "");
            strHTML = Regex.Replace(strHTML, @">\s+\r", ">");
            strHTML = Regex.Replace(strHTML, @">\s+\n", ">");
            strHTML = Regex.Replace(strHTML, @">\s+<", "><");
            return strHTML;
        }

        /// <summary>
        /// 去除HTML标签和前后空白
        /// </summary>
        /// <returns></returns>
        public static string NoHTMLTrim(string strHTML)
        {
            strHTML = SZHomeDLL.HTMLHelper.NoHTML(strHTML).TrimEnd().TrimStart();
            return strHTML;
        }

        /// <summary>
        /// 验证扩展名
        /// </summary>
        /// <param name="imgName"></param>
        /// <returns></returns>
        public static bool ValidateImg(string imgName)
        {
            string[] imgType = new string[] { ".gif", ".jpg", ".png", ".bmp" };
            int i = 0;
            bool blean = false;
            string message = string.Empty;

            //判断是否为Image类型文件
            while (i < imgType.Length)
            {
                if (imgName.Equals(imgType[i].ToString(), StringComparison.OrdinalIgnoreCase))
                {
                    blean = true;
                    break;
                }
                else if (i == (imgType.Length - 1))
                {
                    break;
                }
                else
                {
                    i++;
                }
            }
            return blean;
        }

        public static string JsonSerializable(object value)
        {
            return JsonConvert.SerializeObject(value);
        }
        public static T JsonDeserializeObject<T>(string jsonStr)
        {
            return JsonConvert.DeserializeObject<T>(jsonStr);
        }

        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="inputString">要处理的字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns>返回处理后的字符串</returns>
        public static string ClipString(string inputString, int len)
        {

            bool isShowFix = false;
            if (len % 2 == 1)
            {
                isShowFix = true;
                len--;
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {

                if (tempLen >= len)
                    break;
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

            }

            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (isShowFix && mybyte.Length > len)
                tempString += "…";
            return tempString;
        }

        /// <summary> 
        /// 转换人民币大小金额 
        /// </summary> 
        /// <param name="num">金额</param> 
        /// <returns>返回大写形式</returns> 
        public static string CmycurD(decimal num)
        {
            string str1 = "零壹贰叁肆伍陆柒捌玖";            //0-9所对应的汉字 
            string str2 = "万仟佰拾亿仟佰拾万仟佰拾元角分"; //数字位所对应的汉字 
            string str3 = "";    //从原num值中取出的值 
            string str4 = "";    //数字的字符串形式 
            string str5 = "";  //人民币大写金额形式 
            int i;    //循环变量 
            int j;    //num的值乘以100的字符串长度 
            string ch1 = "";    //数字的汉语读法 
            string ch2 = "";    //数字位的汉字读法 
            int nzero = 0;  //用来计算连续的零值是几个 
            int temp;            //从原num值中取出的值 

            num = Math.Round(Math.Abs(num), 2);    //将num取绝对值并四舍五入取2位小数 
            str4 = ((long)(num * 100)).ToString();        //将num乘100并转换成字符串形式 
            j = str4.Length;      //找出最高位 
            if (j > 15) { return "溢出"; }
            str2 = str2.Substring(15 - j);   //取出对应位数的str2的值。如：200.55,j为5所以str2=佰拾元角分 

            //循环取出每一位需要转换的值 
            for (i = 0; i < j; i++)
            {
                str3 = str4.Substring(i, 1);          //取出需转换的某一位的值 
                temp = Convert.ToInt32(str3);      //转换为数字 
                if (i != (j - 3) && i != (j - 7) && i != (j - 11) && i != (j - 15))
                {
                    //当所取位数不为元、万、亿、万亿上的数字时 
                    if (str3 == "0")
                    {
                        ch1 = "";
                        ch2 = "";
                        nzero = nzero + 1;
                    }
                    else
                    {
                        if (str3 != "0" && nzero != 0)
                        {
                            ch1 = "零" + str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                    }
                }
                else
                {
                    //该位是万亿，亿，万，元位等关键位 
                    if (str3 != "0" && nzero != 0)
                    {
                        ch1 = "零" + str1.Substring(temp * 1, 1);
                        ch2 = str2.Substring(i, 1);
                        nzero = 0;
                    }
                    else
                    {
                        if (str3 != "0" && nzero == 0)
                        {
                            ch1 = str1.Substring(temp * 1, 1);
                            ch2 = str2.Substring(i, 1);
                            nzero = 0;
                        }
                        else
                        {
                            if (str3 == "0" && nzero >= 3)
                            {
                                ch1 = "";
                                ch2 = "";
                                nzero = nzero + 1;
                            }
                            else
                            {
                                if (j >= 11)
                                {
                                    ch1 = "";
                                    nzero = nzero + 1;
                                }
                                else
                                {
                                    ch1 = "";
                                    ch2 = str2.Substring(i, 1);
                                    nzero = nzero + 1;
                                }
                            }
                        }
                    }
                }
                if (i == (j - 11) || i == (j - 3))
                {
                    //如果该位是亿位或元位，则必须写上 
                    ch2 = str2.Substring(i, 1);
                }
                str5 = str5 + ch1 + ch2;

                if (i == j - 1 && str3 == "0")
                {
                    //最后一位（分）为0时，加上“整” 
                    str5 = str5 + '整';
                }
            }
            if (num == 0)
            {
                str5 = "零元整";
            }
            return str5;
        }

        /// <summary>
        /// 读取txt文件 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void ReaderTxt(string path, ref string userName, ref string pwd)
        {
            string strResult = string.Empty;
            StreamReader reader = File.OpenText(path);
            strResult = reader.ReadToEnd();
            reader.Close();
            //string[] arr_1 = strResult.Split('*');

            string[] arr_1 = strResult.Split('\n');
            foreach (string item in arr_1)
            {
                string[] arr_2 = item.Split(':');
                if (arr_2.Length == 2)
                {
                    if (arr_2[0].Trim() == "userName")
                    {
                        userName = arr_2[1].Replace("\r", "").Trim();
                    }
                    else
                    {
                        pwd = arr_2[1].Replace("\r", "").Trim();
                    }
                }
            }

        }
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void WriteTxt(string path, string content)
        {
            lock (objLock)
            {
                string dictory = Path.GetDirectoryName(path);
                if (!Directory.Exists(dictory))
                {
                    Directory.CreateDirectory(dictory);
                }
                StreamWriter writer = new StreamWriter(path, true, Encoding.UTF8);
                writer.Write(content);
                writer.Close();
            }
        }

        /// <summary>
        /// 把内存流转化为字节数据
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToByte(Stream stream)
        {
            Byte[] bytes = null;
            //转化为二进制流
            using (BinaryReader br = new BinaryReader(stream))
            {
                //转化为字节流
                bytes = br.ReadBytes((int)stream.Length);
            }
            return bytes;
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="postString">请求的数据</param>
        /// <param name="postUrl">请求的路径</param>
        /// <param name="method">请求的方式</param>
        /// <returns></returns>
        public static CookieContainer GetCookie(string postString, string postUrl, string method, string referer = "", string host = "")
        {
            CookieContainer cookie = new CookieContainer();
            HttpWebRequest httpRequest = CommonTools.InitWebRequest(postString, postUrl, method, cookie, "", referer, host);
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();//获得 服务端响应
            //string strCookie= httpRequest.CookieContainer.GetCookieHeader(new Uri(postUrl));
            cookie.Add(httpResponse.Cookies);
            string content = "";
            using (Stream responseStream = httpResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.UTF8);
                content = sr.ReadToEnd();
            }
            if (cookie != null && cookie.Count > 0)
            {
                content = "登录成功";
            }
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string hang = "\r\n" + "-----------------------------------\r\n";
            content = hang + date + "：" + content;
            CommonTools.WriteTxt(writepath, content);
            return cookie;//拿到cookie
        }

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="postString">请求的数据</param>
        /// <param name="postUrl">请求的路径</param>
        /// <param name="method">请求的方式</param>
        /// <returns></returns>
        public static CookieContainer GetCookie(string postString, string postUrl, string method, ref string content, int i)
        {
            CookieContainer cookie = new CookieContainer();
            HttpWebRequest httpRequest = InitWebRequest(postString, postUrl, method, cookie);

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();//获得 服务端响应

            content = "登录成功";
            using (Stream responseStream = httpResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.UTF8);

                string returnStr = sr.ReadToEnd();
                if (returnStr.Contains("用户名或者密码不正确"))
                {
                    content = "用户名或者密码不正确";
                }

            }
            string date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string hang = "\r\n" + "-----------------------------------\r\n";
            content = hang + "第" + i + "登录：" + date + "：" + content;
            CommonTools.WriteTxt(writepath, content);
            return cookie;//拿到cookie
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="postUrl"></param>
        /// <param name="postString"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string ImplementOprater(CookieContainer cookie, string postUrl, string postString, string method, string strCookie = "", string referer = "", string host = "")
        {
            string str = "";
            HttpWebRequest httpRequest = InitWebRequest(postString, postUrl, method, cookie, strCookie, referer, host);

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();//获得 服务端响应
            Stream responseStream = httpResponse.GetResponseStream();
            //如果http头中接受gzip的话，这里就要判断是否为有压缩，有的话，直接解压缩即可  
            if (httpResponse.Headers["Content-Encoding"] != null && httpResponse.Headers["Content-Encoding"].ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }

            StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.UTF8);
            str = sr.ReadToEnd();
            responseStream.Close();
            responseStream.Dispose();
            sr.Close();
            sr.Dispose();

            return str;

        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="postUrl"></param>
        /// <param name="postString"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static void LoadPic(CookieContainer cookie, string webUrl, string postUrl, string postString, string method, string fileName, string strCookie = "", string referer = "", string host = "")
        {
            HttpWebRequest httpRequest = InitWebRequest(postString, postUrl, method, cookie, strCookie, referer, host);

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();//获得 服务端响应
            Stream responseStream = httpResponse.GetResponseStream();
            //如果http头中接受gzip的话，这里就要判断是否为有压缩，有的话，直接解压缩即可  
            if (httpResponse.Headers["Content-Encoding"] != null && httpResponse.Headers["Content-Encoding"].ToLower().Contains("gzip"))
            {
                responseStream = new GZipStream(responseStream, CompressionMode.Decompress);
            }

            Image img = Image.FromStream(responseStream, true, false);//把流转化为图片
            responseStream.Close();
            responseStream.Dispose();
            webUrl = webUrl.Replace("http://", "").Replace(".", "_").Replace("/", "_");
            string p_path = ConfigHelper.GetConfigString("path") + "image\\" + webUrl + "\\" + fileName;
            string dire = Path.GetDirectoryName(p_path);
            if (!Directory.Exists(dire))
            {
                Directory.CreateDirectory(dire);
            }
            Image newImg = new Bitmap(img.Width, img.Height);//新建图片对象
            Graphics graghics = Graphics.FromImage(newImg);
            graghics.DrawImage(img, 0, 0);//绘制新图片
            img.Dispose();
            newImg.Save(p_path);
            newImg.Dispose();

        }
        /// <summary>
        /// 初始化web请求
        /// </summary>
        /// <param name="postString"></param>
        /// <param name="postUrl"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HttpWebRequest InitWebRequest(string postString, string postUrl, string method, CookieContainer cookie, string strCookie = "", string referer = "", string host = "")
        {

            HttpWebRequest httpRequest = (HttpWebRequest)HttpWebRequest.Create(postUrl);//创建http 请求
            //if (cookie != null&&cookie.Count>0)
            //{
            //    httpRequest.CookieContainer = cookie;//设置cookie
            //}
            httpRequest.CookieContainer = cookie;//设置cookie
            httpRequest.Method = method;//POST 提交
            WebHeaderCollection header = new WebHeaderCollection();
            header.Add("X-Requested-With: XMLHttpRequest");

            //httpRequest.Headers.Add(HttpRequestHeader.Authorization, "");
            if (!string.IsNullOrEmpty(strCookie))
            {
                header["Cookie"] = strCookie;
            }
            httpRequest.Headers = header;
            httpRequest.KeepAlive = true;
            httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";
            httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            if (!string.IsNullOrEmpty(host))
            {
                httpRequest.Host = host;
            }
            //httpRequest.AllowAutoRedirect =true;
            if (!string.IsNullOrEmpty(referer))
            {
                httpRequest.Referer = referer;
            }
            if (method.ToUpper() == "POST")
            {
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(postString);
                httpRequest.ContentLength = bytes.Length;
                Stream stream = httpRequest.GetRequestStream();
                stream.Write(bytes, 0, bytes.Length);
                stream.Close();//以上是POST数据的写入
            }
            return httpRequest;
        }
    }
}
