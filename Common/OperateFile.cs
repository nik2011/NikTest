using Microsoft.VisualBasic;
using System.IO;
using System.Xml;
using System.Data;
using System;
using System.Collections.Generic;
namespace SZHome.Common
{

    public class OperateFile
    {
        #region xml操作

        /// <summary>
        /// 读取xml数据到dataset
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static DataTable ReadXmlToDataTable(string filepath,string []struct_arr)
        {
            DataTable dt = new DataTable();
            if (!System.IO.File.Exists(filepath))
            {
                return dt;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filepath);
            XmlNodeList xnl = xmlDoc.SelectSingleNode("//NewDataSet").ChildNodes;

            foreach ( string item in struct_arr)
            {
                dt.Columns.Add(item);
            }
           
            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.Name == "UrlInfo")
                {
                    DataRow _row = dt.NewRow();
                    foreach (string item in struct_arr)
                    {
                        _row[item] = xe[item].InnerText;
                    }

                    dt.Rows.Add(_row);
                }
            }
           // DataView dv = dt.DefaultView;
            //dv.Sort = "id";
           //return dv.ToTable() ;
            return dt;
        }

        /// <summary>
        /// 读取最大ID
        /// </summary>
        public static int GetMaxIdByTypeid(string xmlpath)
        {
            if (!System.IO.File.Exists(xmlpath))
            {
                return 0;
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNode root = xmlDoc.SelectSingleNode("//NewDataSet"); //查找<NewDataSet>
            XmlElement xe = (XmlElement)root;
            return Convert.ToInt32(xe.GetAttribute("maxid").ToString());
        }

        /// <summary>
        /// 增加节点后更新最大节点标记MaxId
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <remarks></remarks>
        public static void EditMaxIdByTypeid(string xmlpath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNode root = xmlDoc.SelectSingleNode("//NewDataSet"); //查找<NewDataSet>
            XmlElement xe = (XmlElement)root;
            xe.SetAttribute("id", (Convert.ToInt32(xe.GetAttribute("maxid").ToString()) + 1).ToString());
        }
        /// <summary>
        /// 新建xml文档
        /// </summary>
        /// <param name="xmlpath"></param>
        /// <param name="domain"></param>
        /// <param name="host"></param>
        /// <param name="imgUrl"></param>
        /// <param name="title"></param>
        public static void CreateXmlFile(string xmlpath,Dictionary<string,string> dic)
        {
            string dire = Path.GetDirectoryName(xmlpath);
            if (!Directory.Exists(dire))
            {
                Directory.CreateDirectory(dire);
            }
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement root = xmlDoc.CreateElement("NewDataSet"); //新建根节点
            XmlElement xe = (XmlElement)root;
            xe.SetAttribute("maxid", "1");
            xmlDoc.AppendChild(root);

            XmlElement xe1 = xmlDoc.CreateElement("UrlInfo"); //创建一个子节点

            foreach (KeyValuePair<string,string> item in dic)
            {
                AddNode(xmlDoc, xe1,item.Key,item.Value, false);
            }
          
            root.AppendChild(xe1); //添加到<DataNode>节点中
            xmlDoc.Save(xmlpath);
        }

        /// <summary>
        /// 插入xml新节点,并更新最大节点标记MaxId
        /// </summary>
        public static void InsertXmlNode(string xmlpath, Dictionary<string, string> dic)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNode root = xmlDoc.SelectSingleNode("//NewDataSet"); //查找根节点
            XmlElement xe = (XmlElement)root;
            xe.SetAttribute("maxid",dic["id"]);

            XmlElement xe1 = xmlDoc.CreateElement("UrlInfo"); //创建一个子节点
            foreach (KeyValuePair<string, string> item in dic)
            {
                AddNode(xmlDoc, xe1, item.Key, item.Value, false);
            }

            root.AppendChild(xe1); //添加到<DataNode>节点中
            xmlDoc.Save(xmlpath);
        }

        /// <summary>
        /// 添加节点
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="title"></param>
        /// <param name="value"></param>
        /// <param name="isCData"></param>
        public static void AddNode(XmlDocument xmlDoc, XmlNode parentNode, string title, string value, bool isCData)
        {
            XmlElement ele = xmlDoc.CreateElement(title);
            //是否要添加cdata
            if (isCData)
            {
                XmlCDataSection xcs = xmlDoc.CreateCDataSection(title);
                xcs.InnerText = value;
                ele.AppendChild(xcs);
            }
            else {
                ele.InnerText = value;
            }
            parentNode.AppendChild(ele);
        }

        /// <summary>
        /// 修改xml节点值
        /// </summary>
        /// <remarks></remarks>
        public static void UpdateXmlNode(string filepath, int id,string domain,string host, string url, string title)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            XmlNode node = doc.SelectSingleNode("//NewDataSet/BbsTop[@id=\'" + id.ToString() + "\']");
            XmlNodeList xnl = node.ChildNodes;
            foreach (XmlNode xn in xnl)
            {

                XmlElement xe = (XmlElement)xn;
                if ((string)xe.Name == "link")
                {
                    XmlCDataSection xcs = (XmlCDataSection)(xe.ChildNodes[0]);
                    xcs.Data = url;
                }
                else if ((string)xe.Name == "title")
                {
                    xe.InnerXml = title;
                }
            }
            doc.Save(filepath);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public static void DeleteXmlNode(string xmlpath, int id)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlpath);
            XmlNodeList xnl = xmlDoc.SelectSingleNode("//NewDataSet").ChildNodes;

            foreach (XmlNode xn in xnl)
            {
                XmlElement xe = (XmlElement)xn;
                if (xe.Name == "BbsTop" && xe.GetAttribute("id") == id.ToString())
                {
                  
                    xe.ParentNode.RemoveChild(xe); //删除该节点的全部内容
                }
            }
            xmlDoc.Save(xmlpath);
        }

        /// <summary>
        /// 更换xml id（修改顺序时）
        /// </summary>
        /// <remarks></remarks>
        public static void ChangeXmlId(string filepath, int id1, int id2)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);
            XmlNode node1 = doc.SelectSingleNode("//NewDataSet/BbsTop[@id=\'" + id1.ToString() + "\']");
            XmlElement xe1 = (XmlElement)node1;
            XmlNode node2 = doc.SelectSingleNode("//NewDataSet/BbsTop[@id=\'" + id2.ToString() + "\']");
            XmlElement xe2 = (XmlElement)node2;
            xe1.SetAttribute("id", id2.ToString());
            xe2.SetAttribute("id", id1.ToString());
            doc.Save(filepath);
        }

        #endregion



        /// <summary>
        /// 写入数据到文件
        /// </summary>
        /// <remarks></remarks>
        public void StreamWrite(string filePath, string strContent)
        {
            if (filePath.Length > 0)
            {
                StreamWriter sw = null;
                if (!Directory.Exists(System.IO.Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                try
                {
                    sw = new StreamWriter(filePath, false, System.Text.Encoding.UTF8);
                    sw.Write(strContent);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    sw.Close();
                    sw.Dispose();
                }
            }
        }
    }
}