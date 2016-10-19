using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.Web.Plugin.submits.DAL
{
    /// <summary>
    /// 数据访问类:category
    /// </summary>
    public partial class submits_category
    {
        private string databaseprefix; //数据库表名前缀
        public submits_category(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法=========================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "submits_category");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询生成目录名是否存在
        /// </summary>
        public bool Exists(string build_path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "submits_category");
            strSql.Append(" where build_path=@build_path ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@build_path", MySqlDbType.VarChar,100)};
            parameters[0].Value = build_path;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "submits_category");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetIds(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 field_ids from " + databaseprefix + "submits_category");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 返回频道分类的生成目录名
        /// </summary>
        public string GetBuildPath(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 build_path from " + databaseprefix + "submits_category");
            strSql.Append(" where id=" + id);
            string build_path = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(build_path))
            {
                return "";
            }
            return build_path;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.submits_category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "submits_category(");
            strSql.Append("title,field_ids,is_default,sort_id,add_time)");
            strSql.Append(" values (");
            strSql.Append("@title,@field_ids,@is_default,@sort_id,@add_time)");
            strSql.Append(";select @@IDENTITY");
            MySqlParameter[] parameters = {
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@field_ids", MySqlDbType.VarChar,255),
					new MySqlParameter("@is_default", MySqlDbType.Int32,4),
					new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
                    new MySqlParameter("@add_time", MySqlDbType.Date)
            };
            parameters[0].Value = model.title;
            parameters[1].Value = model.field_ids;
            parameters[2].Value = model.is_default;
            parameters[3].Value = model.sort_id;
            parameters[4].Value = model.add_time;

            object obj = DbHelperMySql.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "submits_category set " + strValue);
            strSql.Append(" where id=" + id);
            int rows = DbHelperMySql.ExecuteSql(strSql.ToString());
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
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "submits_category set " + strValue);
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@id", MySqlDbType.VarChar,100)};
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.submits_category model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "submits_category set ");
            strSql.Append("title=@title,");

            strSql.Append("field_ids=@field_ids,");
            strSql.Append("is_default=@is_default,");
            strSql.Append("sort_id=@sort_id");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4),
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@field_ids", MySqlDbType.VarChar,255),
					new MySqlParameter("@is_default", MySqlDbType.Int32,4),
					new MySqlParameter("@sort_id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.title;
            parameters[2].Value = model.field_ids;
            parameters[3].Value = model.is_default;
            parameters[4].Value = model.sort_id;

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
            //取得要删除频道记录
            Model.submits_category model = GetModel(id);
            if (model == null)
            {
                return false;
            }
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //删除频道分类表
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from " + databaseprefix + "submits_category ");
                        strSql.Append(" where id=@id");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
                        parameters[0].Value = id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.submits_category GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,field_ids,is_default,sort_id,add_time from " + databaseprefix + "submits_category ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.submits_category model = new Model.submits_category();
            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.field_ids = ds.Tables[0].Rows[0]["field_ids"].ToString();
                if (ds.Tables[0].Rows[0]["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(ds.Tables[0].Rows[0]["is_default"].ToString());
                }
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                return model;
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
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,title,field_ids,is_default,sort_id,add_time ");
            strSql.Append(" FROM " + databaseprefix + "submits_category ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "submits_category");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion
    }
}