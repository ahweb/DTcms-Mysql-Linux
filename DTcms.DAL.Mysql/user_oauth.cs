using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// OAuth授权用户信息
    /// </summary>
    public partial class user_oauth
    {
        private string databaseprefix; //数据库表名前缀
        public user_oauth(string _databaseprefix)
        {
            databaseprefix = _databaseprefix; 
        }

        #region 基本方法===========================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_oauth order by id desc limit 1";
            object obj = DbHelperMySql.GetSingle(conn, trans, strSql);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_oauth");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;
            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_oauth model)
        {
            int newId;
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "user_oauth(");
                        strSql.Append("user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@user_id,@user_name,@oauth_name,@oauth_access_token,@oauth_openid,@add_time)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@oauth_name", MySqlDbType.VarChar,50),
					            new MySqlParameter("@oauth_access_token", MySqlDbType.VarChar,500),
					            new MySqlParameter("@oauth_openid", MySqlDbType.VarChar,255),
					            new MySqlParameter("@add_time", MySqlDbType.Date)};
                        parameters[0].Value = model.user_id;
                        parameters[1].Value = model.user_name;
                        parameters[2].Value = model.oauth_name;
                        parameters[3].Value = model.oauth_access_token;
                        parameters[4].Value = model.oauth_openid;
                        parameters[5].Value = model.add_time;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        newId = GetMaxId(conn, trans);
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return newId;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_oauth model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_oauth set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("oauth_name=@oauth_name,");
            strSql.Append("oauth_access_token=@oauth_access_token,");
            strSql.Append("oauth_openid=@oauth_openid,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					new MySqlParameter("@oauth_name", MySqlDbType.VarChar,50),
					new MySqlParameter("@oauth_access_token", MySqlDbType.VarChar,500),
					new MySqlParameter("@oauth_openid", MySqlDbType.VarChar,255),
					new MySqlParameter("@add_time", MySqlDbType.Date),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.oauth_name;
            parameters[3].Value = model.oauth_access_token;
            parameters[4].Value = model.oauth_openid;
            parameters[5].Value = model.add_time;
            parameters[6].Value = model.id;

            int rows = DbHelperMySql.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_oauth ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            int rows = DbHelperMySql.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_oauth GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time");
            strSql.Append(" from " + databaseprefix + "user_oauth ");
            strSql.Append(" where id=@id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据开放平台和openid返回一个实体
        /// </summary>
        public Model.user_oauth GetModel(string oauth_name, string oauth_openid)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time");
            strSql.Append(" from " + databaseprefix + "user_oauth");
            strSql.Append(" where oauth_name=@oauth_name and oauth_openid=@oauth_openid limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@oauth_name", MySqlDbType.VarChar,100),
                    new MySqlParameter("@oauth_openid", MySqlDbType.VarChar,100)};
            parameters[0].Value = oauth_name;
            parameters[1].Value = oauth_openid;

            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            
            strSql.Append(" id,user_id,user_name,oauth_name,oauth_access_token,oauth_openid,add_time ");
            strSql.Append(" FROM " + databaseprefix + "user_oauth ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            if (Top > 0)
            {
                strSql.Append(" limit " + Top.ToString());
            }
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "user_oauth");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法===========================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_oauth DataRowToModel(DataRow row)
        {
            Model.user_oauth model = new Model.user_oauth();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(row["user_id"].ToString());
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["oauth_name"] != null)
                {
                    model.oauth_name = row["oauth_name"].ToString();
                }
                if (row["oauth_access_token"] != null)
                {
                    model.oauth_access_token = row["oauth_access_token"].ToString();
                }
                if (row["oauth_openid"] != null)
                {
                    model.oauth_openid = row["oauth_openid"].ToString();
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
            }
            return model;
        }

        #endregion
    }
}