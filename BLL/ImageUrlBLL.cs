using SZHome.DAL;
using SZHome.Entity;
using System.Collections.Generic;
namespace SZHome.BLL
{
    /// <summary>
    /// 数据表ImageUrl的业务逻辑类
    /// </summary>
    public class ImageUrlBLL
    {
        /// <summary>
        /// 向数据表中插入一条新记录
        /// </summary>
        /// <param name="entity">Entity.ImageImageUrlEntity实体类</param>
        /// <remarks>如果表存在自增长字段，插入记录成功后自增长字段值会更新至实体</remarks>
        public static void Insert(Entity.ImageUrlEntity entity)
        {
            ImageUrlDAL.Insert(entity);
        }

        /// <summary>
        /// 获取数据库一条记录实体(根据主键条件)
        /// </summary>
        /// <param name="id">主键字段id</param>
        /// <returns>Entity.ImageUrlEntity实体类</returns>
        public static Entity.ImageUrlEntity GetById(int id)
        {
            return ImageUrlDAL.GetById(id);
        }
        /// <summary>
        /// 获取未使用链接列表
        /// </summary>
        /// <param name="host"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static List<ImageUrlEntity> GetList(string host)
        {
            string where = "[IsUser]=0";
            if (!string.IsNullOrEmpty(host))
            {
                where += string.Format(" and [Host]='{0}' ", host);
            }
            return ImageUrlDAL.Search(where, "Id", 10);
        }
        /// <summary>
        /// 判断图片是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsExistImageUrl(string imageUrl)
        {
            return ImageUrlDAL.IsExistImageUrl(imageUrl) ;
        }

        /// <summary>
        /// 修改图片为已使用
        /// </summary>
        /// <param name="id"></param>
        public static void UpdateUsered(int id)
        {
            ImageUrlDAL.UpdateUsered(id);
        }

    }
}
