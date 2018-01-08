using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SZHome.Entity;
using SZHome.Common;
using SZHomeDLL;
using System.Text;

namespace web.Controllers
{
    public class PictureRecogniseController : Controller
    {
        // GET: PictureRecognise
        public ActionResult Index()
        {
            //string str = SZHomeDLL.StringHelper.ConvertUnicodeToChn("\u5927\u5e45\u5ea6");

            return View();
        }
        /// <summary>
        /// 图片识别
        /// </summary>
        /// <returns></returns>
        //public ActionResult PictureRecognise()
        //{
        //    HandleResult hr = new HandleResult();
        //    HttpPostedFileBase file = Request.Files["file"];
        //    if (file == null || file.ContentLength <= 0)
        //    {
        //        hr.Message = "请选择图片";
        //        return Json(hr);
        //    }
        //    byte[] bytes = CommonTools.StreamToByte(file.InputStream);
        //    string extend = System.IO.Path.GetExtension(file.FileName).ToUpper();//扩展名
        //    Random random = new Random(DateTime.Now.Millisecond);//随机数对象
        //    //if (extend != ".JPG" && extend != ".PNG")
        //    //{
        //    //    hr.Message = "图片类型只能是.jpg .png";
        //    //    return Json(hr);
        //    //}
        //    string newDic = DateTime.Now.ToString("yyyy") + "\\" + DateTime.Now.ToString("MM") + "\\";//新目录
        //    string newName = DateTime.Now.ToString("ddHHmmssffff") + random.Next(10000).ToString() + extend;//新文件名称
        //    string rootDir = ConfigHelper.GetConfigString("ImageFileUploadPath");
        //    string filePath = rootDir + newName;
        //    string message = "";
        //    int result = ImageHelper.UploadImage(bytes, filePath, ref message);
        //    if (result != 1)
        //    {
        //        hr.Message = message;
        //        return Json(hr);
        //    }
        //    try
        //    {
        //        MODI.Document doc = new MODI.Document();
        //        doc.Create(filePath);
        //        MODI.Image image;
        //        MODI.Layout layout;
        //        string language = Request.Params["language"];
        //        doc.OCR(GetLanuageType(language), true, true);  // 识别文字类型
        //        StringBuilder sb = new StringBuilder();
        //        for (int i = 0; i < doc.Images.Count; i++)
        //        {
        //            image = (MODI.Image)doc.Images[i];
        //            layout = image.Layout;
        //            sb.Append(layout.Text);
        //        }
        //        hr.Other = sb.ToString();
        //    }
        //    catch (Exception ex)
        //    {
        //        hr.Message = "转换失败！详情：" + ex.Message;
        //        return Json(hr);
        //    }
        //    hr.StatsCode = 200;
        //    hr.Message = "识别成功";
        //    return Json(hr);
        //}

        //private MODI.MiLANGUAGES GetLanuageType(string sValue)
        //{
        //    switch (sValue)
        //    {
        //        case "2052":
        //            return MODI.MiLANGUAGES.miLANG_CHINESE_SIMPLIFIED;
        //        case "5":
        //            return MODI.MiLANGUAGES.miLANG_CZECH;
        //        case "6":
        //            return MODI.MiLANGUAGES.miLANG_DANISH;
        //        case "7":
        //            return MODI.MiLANGUAGES.miLANG_GERMAN;
        //        case "8":
        //            return MODI.MiLANGUAGES.miLANG_GREEK;
        //        case "9":
        //            return MODI.MiLANGUAGES.miLANG_ENGLISH;
        //        case "10":
        //            return MODI.MiLANGUAGES.miLANG_SPANISH;
        //        case "11":
        //            return MODI.MiLANGUAGES.miLANG_FINNISH;
        //        case "12":
        //            return MODI.MiLANGUAGES.miLANG_FRENCH;
        //        case "14":
        //            return MODI.MiLANGUAGES.miLANG_HUNGARIAN;
        //        case "16":
        //            return MODI.MiLANGUAGES.miLANG_ITALIAN;
        //        case "17":
        //            return MODI.MiLANGUAGES.miLANG_JAPANESE;
        //        case "18":
        //            return MODI.MiLANGUAGES.miLANG_KOREAN;
        //        case "19":
        //            return MODI.MiLANGUAGES.miLANG_DUTCH;
        //        case "20":
        //            return MODI.MiLANGUAGES.miLANG_NORWEGIAN;
        //        case "21":
        //            return MODI.MiLANGUAGES.miLANG_POLISH;
        //        case "22":
        //            return MODI.MiLANGUAGES.miLANG_PORTUGUESE;
        //        case "25":
        //            return MODI.MiLANGUAGES.miLANG_RUSSIAN;
        //        case "29":
        //            return MODI.MiLANGUAGES.miLANG_SWEDISH;
        //        case "31":
        //            return MODI.MiLANGUAGES.miLANG_TURKISH;
        //        case "1028":
        //            return MODI.MiLANGUAGES.miLANG_CHINESE_TRADITIONAL;
        //        default:
        //            return MODI.MiLANGUAGES.miLANG_ENGLISH;
        //    }
        //}
    }
}