using System.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using YiTu.DBUtility;

namespace SZHome.DAL
{
    public partial class UrlDAL
    {
        /// <summary>
        /// 修改链接为已使用
        /// </summary>
        /// <param name="id"></param>
        public static void UpdateUsered(int id)
        {
            string sql = string.Format("UPDATE [dbo].[Url] SET [IsUser]=1 WHERE Id={0}",id);
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, sql, null);
        }


         /// <summary>
        /// 判断链接是否存在
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsExistUrl(string url)
        {
            string sql = "SELECT COUNT(Id) FROM [dbo].[Url] WHERE url=@url";
            List<SqlParameter> paraList = new List<SqlParameter>();
            paraList.Add(SqlHelper.CreateParam("@url", SqlDbType.NVarChar, 200, ParameterDirection.Input, url));
            return (int)SqlHelper.ExecuteScalar(ConnectionString, CommandType.Text, sql, paraList)>0;
        }
    }
}
