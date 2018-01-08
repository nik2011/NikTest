using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SZHomeDLL;
using SZHome.BLL;
using SZHome.Entity;
using SZHome.Common;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;

namespace SmallSpider_v2
{
    public partial class Url : Form
    {
        public Url()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 获取登录cookie
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        private CookieContainer GetCookie(ref string message)
        {
            CookieContainer cookie = new CookieContainer();
            try
            {
                string loginUrl = this.txtLoginUrl.Text;
                if (string.IsNullOrEmpty(loginUrl))
                {
                    message = "请输入登录路径";
                    return cookie;
                }
                string postData = this.txtPostData.Text;
                if (string.IsNullOrEmpty(postData))
                {
                    message = "请输入登录路径";
                    return cookie;
                }
                RequestEntity requestEnt = new RequestEntity();
                requestEnt.postUrl = loginUrl;
                requestEnt.method = "post";
                requestEnt.postString = postData;
                string msg = string.Empty;
                cookie = RequestHelper.GetCookie(requestEnt, ref msg);
                LogHelper.LogInfo(msg);
                if (cookie == null || cookie.Count == 0)
                {
                    message = "登录失败";
                    return cookie;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogTrace("登录报错：" + ex.Message);
            }
            return cookie;
        }
        /// <summary>
        /// 获取url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void confirm_Click(object sender, EventArgs e)
        {
            try
            {

                string url = this.txtUrl.Text;
                if (string.IsNullOrEmpty(url))
                {
                    MessageBox.Show("请输入首页链接");
                    return;
                }
                CookieContainer cookie = new CookieContainer();
                if (this.ckbLogin.Checked)
                {
                    string msg = "";
                    cookie = this.GetCookie(ref msg);
                    if (msg != "")
                    {
                        MessageBox.Show(msg);
                        return;
                    }
                }

                string host = string.Empty;
                if (url.Contains("http"))
                {
                    string[] arr = url.Split('/');
                    if (arr.Length >= 3)
                    {
                        host = arr[2];
                    }

                }
                this.confirm.Text = "执行中...";
                int depth = 1;
                RequestEntity requestEnt = new RequestEntity();
                requestEnt.postUrl = url;
                requestEnt.method = "get";
                requestEnt.host = host;
                requestEnt.cookie = cookie;
                string result = RequestHelper.ImplementOprater(requestEnt);
                MatchWebUrl(result, host, url, depth);
                for (int i = 1; i <= 3; i++)
                {
                    depth = i + 1;
                    List<UrlEntity> urlList = UrlBLL.GetList(host, i);
                    foreach (var item in urlList)
                    {
                        requestEnt = new RequestEntity();
                        requestEnt.postUrl = item.Url;
                        requestEnt.method = "get";
                        requestEnt.host = item.Host;
                        if (this.ckbLogin.Checked&&cookie.Count==0)
                        {
                            string msg = "";
                            cookie = this.GetCookie(ref msg);
                            if (msg != "")
                            {
                                MessageBox.Show(msg);
                                return;
                            }
                        }
                        requestEnt.cookie = cookie;
                        result = RequestHelper.ImplementOprater(requestEnt);
                        Thread thread = new Thread(MatchWebUrl);
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        dic["Result"] = result;
                        dic["Host"] = item.Host;
                        dic["Url"] = item.Url;
                        dic["Depth"] = depth;
                        thread.Start(dic);
                        //MatchWebUrl(result, item.Host, item.Url, depth);
                    }
                }
                this.confirm.Text = "获取url";
            }
            catch (Exception ex)
            {
                LogHelper.LogTrace("报错：" + ex.Message);
                Application.Exit();
            }
        }

        private void MatchWebUrl(object obj)
        {
            try
            {
                Dictionary<string, object> dic = (Dictionary<string, object>)obj;
                string html = dic["Result"].ToString();
                string host = dic["Host"].ToString();
                string webUrl = dic["Url"].ToString();
                int depth = (int)dic["Depth"];
                string matchStr = "<a[\\s^\\s]+href=\"(?<href>\\S+)\"";//命名分组
                MatchCollection collection = Regex.Matches(html, matchStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
                string content = string.Empty;
                foreach (Match item in collection)
                {
                    string href = item.Groups["href"].Value;
                    //无效链接
                    if (string.IsNullOrEmpty(href) || href == "#" || href.ToLower().Contains("javascript"))
                    {
                        continue;
                    }
                    //没有域名的链接
                    if (!href.Contains("http") && !href.Contains("ftp"))
                    {
                        href = "http://" + host + href;
                    }
                    //外链接
                    if (!href.Contains("http://" + host))
                    {
                        continue;
                    }
                    //是否已保存
                    if (UrlBLL.IsExistUrl(href))
                    {
                        continue;
                    }
                    UrlEntity urlEnt = new UrlEntity();
                    urlEnt.AddDate = DateTime.Now;
                    urlEnt.Depth = depth;
                    urlEnt.Host = host;
                    urlEnt.Url = href;
                    urlEnt.WebUrl = webUrl;
                    UrlBLL.Insert(urlEnt);
                }
            }
            catch (Exception ex)
            {

                LogHelper.LogTrace(ex.Message);
            }
            
        }

        /// <summary>
        /// 匹配网页链接
        /// </summary>
        /// <param name="html"></param>
        private void MatchWebUrl(string html, string host, string webUrl, int depth)
        {
            string matchStr = "<a[\\s^\\s]+href=\"(?<href>\\S+)\"";//命名分组
            MatchCollection collection = Regex.Matches(html, matchStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            string content = string.Empty;
            foreach (Match item in collection)
            {
                string href = item.Groups["href"].Value;
                //无效链接
                if (string.IsNullOrEmpty(href) || href == "#" || href.ToLower().Contains("javascript"))
                {
                    continue;
                }
                //没有域名的链接
                if (!href.Contains("http") && !href.Contains("ftp"))
                {
                    href = "http://" + host + href;
                }
                //外链接
                if (!href.Contains("http://" + host))
                {
                    continue;
                }
                //是否已保存
                if (UrlBLL.IsExistUrl(href))
                {
                    continue;
                }
                UrlEntity urlEnt = new UrlEntity();
                urlEnt.AddDate = DateTime.Now;
                urlEnt.Depth = depth;
                urlEnt.Host = host;
                urlEnt.Url = href;
                urlEnt.WebUrl = webUrl;
                UrlBLL.Insert(urlEnt);

            }


        }


        /// <summary>
        /// 获取图片url
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_Click(object sender, EventArgs e)
        {
            try
            {
                string host = this.txtHost.Text;
                if (string.IsNullOrEmpty(host))
                {
                    MessageBox.Show("请输入主机");
                    return;
                }
                CookieContainer cookie = new CookieContainer();
                if (this.ckbLogin.Checked)
                {
                    string msg = "";
                    cookie = this.GetCookie(ref msg);
                    if (msg != "")
                    {
                        MessageBox.Show(msg);
                        return;
                    }
                }

                this.image.Text = "执行中...";
                List<UrlEntity> urlList = UrlBLL.GetList(host);
                foreach (var item in urlList)
                {
                    RequestEntity requestEnt = new RequestEntity();
                    requestEnt.postUrl = item.Url;
                    requestEnt.method = "get";
                    requestEnt.host = item.Host;
                    requestEnt.cookie = cookie;
                    string result = RequestHelper.ImplementOprater(requestEnt);
                    MatchImageUrl(result, item.Host, item.WebUrl);
                    UrlBLL.UpdateUsered(item.Id);
                }
                this.image.Text = "获取图片url";
            }
            catch (Exception ex)
            {

                LogHelper.LogTrace("报错：" + ex.Message);
                Application.Exit();
            }

        }

        /// <summary>
        /// 获取图片url
        /// </summary>
        /// <param name="html"></param>
        private void MatchImageUrl(string html, string host, string webUrl)
        {
            string matchStr = "<img[\\s^\\s]+src=\"(?<src>\\S+)\"";//分组命名
            string imgUrl = string.Empty;
            Regex regex = new Regex(matchStr, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            MatchCollection collection = regex.Matches(html);
            foreach (Match item in collection)
            {
                imgUrl = item.Groups["src"].Value;

                //无效链接
                if (string.IsNullOrEmpty(imgUrl))
                {
                    continue;
                }

                if (!imgUrl.Contains("http"))
                {
                    imgUrl = "http://" + host + imgUrl;
                }
                //是否存在
                if (ImageUrlBLL.IsExistImageUrl(imgUrl))
                {
                    continue;
                }

                ImageUrlEntity imageUrlEnt = new ImageUrlEntity();
                imageUrlEnt.AddDate = DateTime.Now;
                imageUrlEnt.Host = host;
                imageUrlEnt.ImageUrl = imgUrl;
                imageUrlEnt.WebUrl = webUrl;
                ImageUrlBLL.Insert(imageUrlEnt);
            }
        }


        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loadImage_Click(object sender, EventArgs e)
        {
            try
            {
                string host = this.txtLoad.Text;
                if (string.IsNullOrEmpty(host))
                {
                    MessageBox.Show("请输入主机");
                    return;
                }
                CookieContainer cookie = new CookieContainer();
                if (this.ckbLogin.Checked)
                {
                    string msg = "";
                    cookie = this.GetCookie(ref msg);
                    if (msg != "")
                    {
                        MessageBox.Show(msg);
                        return;
                    }
                }

                this.loadImage.Text = "执行中...";
                List<ImageUrlEntity> imageList = ImageUrlBLL.GetList(host);
                foreach (var item in imageList)
                {
                    RequestEntity requestEnt = new RequestEntity();
                    requestEnt.postUrl = item.ImageUrl;
                    requestEnt.method = "get";
                    requestEnt.host = item.Host;
                    requestEnt.referer = item.WebUrl;
                    requestEnt.cookie = cookie;
                    RequestHelper.LoadPic(requestEnt);

                    ImageUrlBLL.UpdateUsered(item.Id);

                }
                this.loadImage.Text = "下载图片";
            }
            catch (Exception ex)
            {

                LogHelper.LogTrace("报错：" + ex.Message);
                Application.Exit();
            }

        }




    }
}
