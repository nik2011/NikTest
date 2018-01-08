using SZHome.DAL;
using SZHome.Entity;
using System.Collections.Generic;
namespace SZHome.BLL
{
    /// <summary>
    /// 数据表Url的业务逻辑类
    /// </summary>
    public class UrlBLL
    {
        /// <summary>
        /// 向数据表中插入一条新记录
        /// </summary>
        /// <param name="entity">Entity.UrlEntity实体类</param>
        /// <remarks>如果表存在自增长字段，插入记录成功后自增长字段值会更新至实体</remarks>
        public static void Insert(Entity.UrlEntity entity)
        {
            UrlDAL.Insert(entity);
        }

           /// <summary>
        /// 获取数据库一条记录实体(根据主键条件)
        /// </summary>
        /// <param name="id">主键字段id</param>
        /// <returns>Entity.UrlEntity实体类</returns>
        public static Entity.UrlEntity GetById(int id)
        {
            return UrlDAL.GetById(id);
        }
        /// <summary>
        /// 获取链接列表
        /// </summary>
        /// <param name="host"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static List<UrlEntity> GetList(string host,int depth)
        {
            string where = string.Empty;
            if (!string.IsNullOrEmpty(host))
            {
                where += string.Format("[Host]='{0}' and ",host);
            }
            if (depth > 0)
            {
                where += string.Format("[Depth]={0} and ", depth);
            }
            if (!string.IsNullOrEmpty(where))
            {
                where = where.Remove(where.Length - 4, 4);
            }
            return UrlDAL.Search(where,"Id",0);
        }
        /// <summary>
        /// 获取未使用链接列表
        /// </summary>
        /// <param name="host"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static List<UrlEntity> GetList(string host)
        {
            string where = "isUser=0";
            if (!string.IsNullOrEmpty(host))
            {
                where += string.Format(" and [Host]='{0}' ", host);
            }
          
            return UrlDAL.Search(where, "Id",0);
        }
        /// <summary>
        /// 判断链接是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsExistUrl(string url)
        {
            return UrlDAL.IsExistUrl(url);
        }

         /// <summary>
        /// 修改链接为已使用
        /// </summary>
        /// <param name="id"></param>
        public static void UpdateUsered(int id)
        {
            UrlDAL.UpdateUsered(id);
        }


    }
}
