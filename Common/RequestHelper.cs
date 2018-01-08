using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.IO.Compression;
using SZHome.Entity;
using Newtonsoft.Json;
using System.Drawing;
using SZHomeDLL;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace SZHome.Common
{
    public class RequestHelper
    {

        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="entity">请求实体</param>
        /// <returns></returns>
        public static CookieContainer GetCookie(RequestEntity entity)
        {
            CookieContainer cookie = new CookieContainer();
            entity.cookie = cookie;
            HttpWebRequest httpRequest = InitWebRequest(entity);
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();//获得 服务端响应
            cookie.Add(httpResponse.Cookies);
            return cookie;
        }
        /// <summary>
        /// 获取cookie
        /// </summary>
        /// <param name="entity">请求实体</param>
        /// <returns></returns>
        public static CookieContainer GetCookie(RequestEntity entity, ref string msg)
        {
            CookieContainer cookie = new CookieContainer();
            entity.cookie = cookie;
            HttpWebRequest httpRequest = InitWebRequest(entity);
            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();//获得 服务端响应
            cookie.Add(httpResponse.Cookies);
            using (Stream stream = httpResponse.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream, System.Text.Encoding.UTF8);
                msg = sr.ReadToEnd();
                sr.Dispose();
            }
            return cookie;
        }

        /// <summary>
        /// 执行操作
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="postUrl"></param>
        /// <param name="postString"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static string ImplementOprater(RequestEntity entity)
        {
            string str = "";
            HttpWebRequest httpRequest = InitWebRequest(entity);
            try
            {
                ServicePointManager.ServerCertificateValidationCallback =new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);
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
            }
            catch (Exception ex)
            {
                LogHelper.LogTrace(entity.postUrl + " 获取报错：" + ex.Message);
            }

            return str;

        }

        /// <summary>
        /// 重写通信通道连接验证回调
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        public static bool CheckValidationResult(object sender, X509Certificate certificate,X509Chain chain, SslPolicyErrors errors)
        {  // 总是接受  
            return true;
        }

        /// <summary>
        /// 初始化web请求
        /// </summary>
        /// <param name="postString"></param>
        /// <param name="postUrl"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static HttpWebRequest InitWebRequest(RequestEntity entity)
        {
            HttpWebRequest httpRequest = null;
            try
            {
                httpRequest = (HttpWebRequest)HttpWebRequest.Create(entity.postUrl);//创建http 请求
                httpRequest.CookieContainer = entity.cookie;//设置cookie
                entity.method = string.IsNullOrEmpty(entity.method) ? "get" : entity.method;//默认get请求
                httpRequest.Method = entity.method;//POST 提交
                WebHeaderCollection header = new WebHeaderCollection();
                header.Add("X-Requested-With: XMLHttpRequest");
                if (!string.IsNullOrEmpty(entity.strCookie))
                {
                    header["Cookie"] = entity.strCookie;
                }
                httpRequest.Headers = header;

                httpRequest.KeepAlive = true;
                httpRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:38.0) Gecko/20100101 Firefox/38.0";
                httpRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                if (!string.IsNullOrEmpty(entity.host))
                {
                    httpRequest.Host = entity.host;
                }
                //httpRequest.AllowAutoRedirect =true;
                if (!string.IsNullOrEmpty(entity.referer))
                {
                    httpRequest.Referer = entity.referer;
                }
                if (entity.method.ToUpper() == "POST")
                {
                    entity.postString = entity.postString == null ? string.Empty : entity.postString;
                    byte[] bytes = System.Text.Encoding.UTF8.GetBytes(entity.postString);
                    httpRequest.ContentLength = bytes.Length;
                    Stream stream = httpRequest.GetRequestStream();
                    stream.Write(bytes, 0, bytes.Length);
                    stream.Close();//以上是POST数据的写入
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogTrace(entity.postUrl + " 请求报错：" + ex.Message);
            }

            return httpRequest;

        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="cookie"></param>
        /// <param name="postUrl"></param>
        /// <param name="postString"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static void LoadPic(RequestEntity entity)
        {
            HttpWebRequest httpRequest = InitWebRequest(entity);
            try
            {
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
                string webUrl = entity.referer.Replace("http://", "").Replace(".", "_").Replace("/", "_");
                string fileName = entity.postUrl.Substring(entity.postUrl.LastIndexOf('/') + 1);
                string root = ConfigHelper.GetConfigString("path");// AppDomain.CurrentDomain.BaseDirectory;
                string p_path = root + "\\image\\" + webUrl + "\\" + fileName;
                // LogHelper.LogInfo("保存图片路径：" + root);
                // string writepath = ConfigHelper.GetConfigString("path") + "log.txt";
                // CommonTools.WriteTxt(writepath, "保存图片路径：" + root);
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
            catch (Exception ex)
            {

                LogHelper.LogTrace(entity.postUrl + " 下载图片报错：" + ex.Message);
            }


        }

        /// <summary>
        /// 把字符串转化为对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static T ConvertObject<T>(string msg) where T : class
        {
            return JsonConvert.DeserializeObject<T>(msg);
        }



    }
}
