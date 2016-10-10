using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.SqlServer
{
    /// <summary>
    /// 数据访问类:手机短信模板
    /// </summary>
    public partial class sms_template
    {
        private string databaseprefix; //数据库表名前缀
        public sms_template(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "sms_template");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) from " + databaseprefix + "sms_template");
            strSql.Append(" where call_index=@call_index ");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.sms_template model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "sms_template(");
            strSql.Append("title,call_index,content,is_sys)");
            strSql.Append(" values (");
            strSql.Append("@title,@call_index,@content,@is_sys)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.call_index;
            parameters[2].Value = model.content;
            parameters[3].Value = model.is_sys;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.sms_template model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "sms_template set ");
            strSql.Append("title=@title,");
            strSql.Append("call_index=@call_index,");
            strSql.Append("content=@content,");
            strSql.Append("is_sys=@is_sys");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.call_index;
            parameters[2].Value = model.content;
            parameters[3].Value = model.is_sys;
            parameters[4].Value = model.id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
            strSql.Append("delete from " + databaseprefix + "sms_template ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
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
        public Model.sms_template GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,call_index,content,is_sys from " + databaseprefix + "sms_template ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
        /// 得到一个对象实体
        /// </summary>
        public Model.sms_template GetModel(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,call_index,content,is_sys from " + databaseprefix + "sms_template");
            strSql.Append(" where call_index=@call_index");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
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
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,title,call_index,content,is_sys ");
            strSql.Append(" FROM " + databaseprefix + "sms_template ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "sms_template");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法============================
        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        public Model.sms_template DataRowToModel(DataRow row)
        {
            Model.sms_template model = new Model.sms_template();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["call_index"] != null)
                {
                    model.call_index = row["call_index"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["is_sys"] != null && row["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(row["is_sys"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}