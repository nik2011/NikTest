using System;
using System.Data;
using System.Collections.Generic;
using System.Data.SqlClient;
using YiTu.DBUtility;
namespace SZHome.DAL
{
    /// <summary>
    /// 数据表Url的数据操作类
    /// </summary>
    public partial class UrlDAL
    {
        #region ConstVariables
        private const string C_TABLE_NAME = "Url";
        private const string C_SP_URL_FIELDS = "[Id],[WebUrl],[Host],[Url],[IsUser],[Depth],[AddDate]";
        private const string C_SP_URL_INSERT = "INSERT INTO [Url]([WebUrl],[Host],[Url],[IsUser],[Depth],[AddDate]) VALUES(@WebUrl,@Host,@Url,@IsUser,@Depth,@AddDate);SET @Id = SCOPE_IDENTITY();";
        private const string C_SP_URL_UPDATE = "UPDATE [Url] SET [WebUrl]=@WebUrl,[Host]=@Host,[Url]=@Url,[IsUser]=@IsUser,[Depth]=@Depth,[AddDate]=@AddDate WHERE [Id] = @Id";
        private const string C_SP_URL_DELETE = "DELETE [Url] WHERE [Id] = @Id";
        private const string C_SP_URL_GET = "SELECT [Id],[WebUrl],[Host],[Url],[IsUser],[Depth],[AddDate] FROM [Url] WHERE [Id] = @Id";
        #endregion

        private static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["SmallSpider"].ConnectionString;
            }
        }

        /// <summary>
        /// 默认构造函数
        /// </summary>
        private UrlDAL() { }

        /// <summary>
        /// 向数据表中插入一条新记录
        /// </summary>
        /// <param name="entity">Entity.UrlEntity实体类</param>
        /// <remarks>如果表存在自增长字段，插入记录成功后自增长字段值会更新至实体</remarks>
        public static void Insert(Entity.UrlEntity entity)
        {
            List<SqlParameter> commandParms = new List<SqlParameter>();
            SqlParameter id_Id = SqlHelper.CreateParam("@Id", SqlDbType.Int, 0, ParameterDirection.Output, null);
            commandParms.Add(id_Id);
            commandParms.Add(SqlHelper.CreateParam("@WebUrl", SqlDbType.NVarChar, 200, ParameterDirection.Input, entity.WebUrl));
            commandParms.Add(SqlHelper.CreateParam("@Host", SqlDbType.VarChar, 50, ParameterDirection.Input, entity.Host));
            commandParms.Add(SqlHelper.CreateParam("@Url", SqlDbType.NVarChar, 200, ParameterDirection.Input, entity.Url));
            commandParms.Add(SqlHelper.CreateParam("@IsUser", SqlDbType.Bit, 0, ParameterDirection.Input, entity.IsUser));
            commandParms.Add(SqlHelper.CreateParam("@Depth", SqlDbType.Int, 0, ParameterDirection.Input, entity.Depth));
            commandParms.Add(SqlHelper.CreateParam("@AddDate", SqlDbType.DateTime, 0, ParameterDirection.Input, entity.AddDate));

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, C_SP_URL_INSERT, commandParms);
            entity.Id = Convert.ToInt32(id_Id.Value);
        }

        /// <summary>
        /// 获取数据库一条记录实体(根据主键条件)
        /// </summary>
        /// <param name="id">主键字段id</param>
        /// <returns>Entity.UrlEntity实体类</returns>
        public static Entity.UrlEntity GetById(int id)
        {
            Entity.UrlEntity entity = null;
            List<SqlParameter> commandParms = new List<SqlParameter>();
            commandParms.Add(SqlHelper.CreateParam("@Id", SqlDbType.Int, 0, ParameterDirection.Input, id));

            using (SqlDataReader reader = SqlHelper.ExecuteReader(ConnectionString, CommandType.Text, C_SP_URL_GET, commandParms))
            {
                if (reader.Read())
                {
                    entity = ConvertToEntityFromDataReader(reader);
                }
            }

            return entity;
        }

        /// <summary>
        /// 更新数据库中一条记录(根据主键条件)
        /// </summary>
        /// <param name="entity">Entity.UrlEntity实体类</param>
        public static void Update(Entity.UrlEntity entity)
        {
            List<SqlParameter> commandParms = new List<SqlParameter>();

            commandParms.Add(SqlHelper.CreateParam("@Id", SqlDbType.Int, 0, ParameterDirection.Input, entity.Id));
            commandParms.Add(SqlHelper.CreateParam("@WebUrl", SqlDbType.NVarChar, 200, ParameterDirection.Input, entity.WebUrl));
            commandParms.Add(SqlHelper.CreateParam("@Host", SqlDbType.VarChar, 50, ParameterDirection.Input, entity.Host));
            commandParms.Add(SqlHelper.CreateParam("@Url", SqlDbType.NVarChar, 200, ParameterDirection.Input, entity.Url));
            commandParms.Add(SqlHelper.CreateParam("@IsUser", SqlDbType.Bit, 0, ParameterDirection.Input, entity.IsUser));
            commandParms.Add(SqlHelper.CreateParam("@Depth", SqlDbType.Int, 0, ParameterDirection.Input, entity.Depth));
            commandParms.Add(SqlHelper.CreateParam("@AddDate", SqlDbType.DateTime, 0, ParameterDirection.Input, entity.AddDate));

            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, C_SP_URL_UPDATE, commandParms);
        }

        /// <summary>
        /// 删除数据库中一条记录(根据主键条件)
        /// </summary>
        /// <param name="id">主键字段id</param>
        public static void Delete(int id)
        {
            List<SqlParameter> commandParms = new List<SqlParameter>();
            commandParms.Add(SqlHelper.CreateParam("@Id", SqlDbType.Int, 0, ParameterDirection.Input, id));
            SqlHelper.ExecuteNonQuery(ConnectionString, CommandType.Text, C_SP_URL_DELETE, commandParms);
        }

        /// <summary>
        /// 按条件查询数据表,返回DataTable类型数据
        /// </summary>
        /// <param name="whereClause">SQL条件语句(可为空)，不须带"Where"关键字</param>
        /// <param name="dataFields">需返回字段(不能为空,为"*"则返回所有字段)</param>
        /// <param name="orderBy">SQL排序语句(不能为空)，不须带"Order By"关键字</param>
        /// <param name="startRowIndex">记录开始索引，从0开始</param>
        /// <param name="maximumRows">返回记录数量</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchDT(string whereClause, string dataFields, string orderBy, int startRowIndex, int maximumRows)
        {
            if (dataFields.Trim() == "*")
            {
                dataFields = C_SP_URL_FIELDS;
            }
            return SqlListHepler.Search(ConnectionString, C_TABLE_NAME, dataFields, whereClause, orderBy, startRowIndex, maximumRows);
        }


        /// <summary>
        /// 按条件查询数据表,返回 Entity.UrlEntity 数据集
        /// </summary>
        /// <param name="whereClause">SQL条件语句(可为空)，不须带"Where"关键字</param>
        /// <param name="orderBy">SQL排序语句(不能为空)，不须带"Order By"关键字</param>
        /// <param name="startRowIndex">记录开始索引，从0开始</param>
        /// <param name="maximumRows">返回记录数量</param>
        public static List<Entity.UrlEntity> Search(string whereClause, string orderBy, int startRowIndex, int maximumRows)
        {
            List<Entity.UrlEntity> list = new List<Entity.UrlEntity>();
            using (SqlDataReader reader = SqlListHepler.SearchDataReader(ConnectionString, C_TABLE_NAME, C_SP_URL_FIELDS, whereClause, orderBy, startRowIndex, maximumRows))
            {
                while (reader.Read())
                {
                    list.Add(ConvertToEntityFromDataReader(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 按条件查询数据表,返回DataTable类型数据
        /// </summary>
        /// <param name="whereClause">SQL条件语句(可为空)，不须带"Where"关键字</param>
        /// <param name="dataFields">需返回字段(不能为空,为"*"则返回所有字段)</param>
        /// <param name="orderBy">SQL排序语句(可为空)，不须带"Order By"关键字</param>
        /// <param name="rowsToReturn">返回记录数量</param>
        /// <returns>DataTable</returns>
        public static DataTable SearchDT(string whereClause, string dataFields, string orderBy, int rowsToReturn)
        {
            if (dataFields.Trim() == "*")
            {
                dataFields = C_SP_URL_FIELDS;
            }
            return SqlListHepler.Search(ConnectionString, C_TABLE_NAME, dataFields, whereClause, orderBy, rowsToReturn);
        }

        /// <summary>
        /// 按条件查询数据表,返回 Entity.UrlEntity 数据集
        /// </summary>
        /// <param name="whereClause">SQL条件语句(可为空)，不须带"Where"关键字</param>
        /// <param name="orderBy">SQL排序语句(可为空)，不须带"Order By"关键字</param>
        /// <param name="rowsToReturn">返回记录数量</param>
        public static List<Entity.UrlEntity> Search(string whereClause, string orderBy, int rowsToReturn)
        {
            List<Entity.UrlEntity> list = new List<Entity.UrlEntity>();
            using (SqlDataReader reader = SqlListHepler.SearchDataReader(ConnectionString, C_TABLE_NAME, C_SP_URL_FIELDS, whereClause, orderBy, rowsToReturn))
            {
                while (reader.Read())
                {
                    list.Add(ConvertToEntityFromDataReader(reader));
                }
            }
            return list;
        }

        /// <summary>
        /// 按条件获取记录数量
        /// </summary>
        /// <param name="whereClause">SQL条件语句(可为空)，不须带"Where"关键字</param>
        /// <returns>int整型数据</returns>
        public static int SearchCount(string whereClause)
        {
            return SqlListHepler.SearchCount(ConnectionString, C_TABLE_NAME, whereClause);
        }

        /// <summary>
        /// 转换DataRow类型数据记录为实体
        /// </summary>
        private static Entity.UrlEntity ConvertToEntityFromDataRow(DataRow row)
        {
            Entity.UrlEntity entity = new Entity.UrlEntity();
            entity.Id = Convert.ToInt32(row["Id"]);
            entity.WebUrl = row["WebUrl"].ToString();
            entity.Host = row["Host"].ToString();
            entity.Url = row["Url"].ToString();
            entity.IsUser = Convert.ToBoolean(row["IsUser"]);
            entity.Depth = Convert.ToInt32(row["Depth"]);
            entity.AddDate = Convert.ToDateTime(row["AddDate"]);

            return entity;
        }

        /// <summary>
        /// 转换SqlDataReader类型数据记录为实体
        /// </summary>
        private static Entity.UrlEntity ConvertToEntityFromDataReader(SqlDataReader reader)
        {
            Entity.UrlEntity entity = new Entity.UrlEntity();
            entity.Id = Convert.ToInt32(reader["Id"]);
            entity.WebUrl = reader["WebUrl"].ToString();
            entity.Host = reader["Host"].ToString();
            entity.Url = reader["Url"].ToString();
            entity.IsUser = Convert.ToBoolean(reader["IsUser"]);
            entity.Depth = Convert.ToInt32(reader["Depth"]);
            entity.AddDate = Convert.ToDateTime(reader["AddDate"]);

            return entity;
        }

    }
}
