using System.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using YiTu.DBUtility;

namespace SZHome.DAL
{
    public partial class ImageUrlDAL
    {
        /// <summary>
        /// 修改图片为已使用
        /// </summary>
        /// <param name="id"></param>
        public static void UpdateUsered(int id)
        {
            string sql = string.Format("UPDATE [dbo].[ImageUrl] SET [IsUser]=1 WHERE Id={0}", id);
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql, null);
        }

         /// <summary>
        /// 判断图片是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsExistImageUrl(string imageUrl)
        {
            string sql = "SELECT COUNT(Id) FROM [dbo].[ImageUrl] WHERE [ImageUrl]=@imageUrl";
            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(SqlHelper.CreateParam("@imageUrl", SqlDbType.NVarChar, 200, ParameterDirection.Input, imageUrl));
            return (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql, paraList) > 0;
        }
    }
}
