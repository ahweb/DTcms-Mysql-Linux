using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// 邮件模板
    /// </summary>
    public partial class mail_template
    {
        private string databaseprefix; //数据库表名前缀
        public mail_template(string _databaseprefix)
        { 
            databaseprefix = _databaseprefix; 
        }

        #region 基本方法============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "mail_template order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "mail_template");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(0) from " + databaseprefix + "mail_template");
            strSql.Append(" where call_index=@call_index ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@call_index", MySqlDbType.VarChar,50)};
            parameters[0].Value = call_index;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.mail_template model)
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
                        strSql.Append("insert into " + databaseprefix + "mail_template(");
                        strSql.Append("title,call_index,maill_title,content)");
                        strSql.Append(" values (");
                        strSql.Append("@title,@call_index,@maill_title,@content)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@call_index", MySqlDbType.VarChar,50),
					            new MySqlParameter("@maill_title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@content", MySqlDbType.VarChar)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.call_index;
                        parameters[2].Value = model.maill_title;
                        parameters[3].Value = model.content;
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
        public bool Update(Model.mail_template model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "mail_template set ");
            strSql.Append("title=@title,");
            strSql.Append("call_index=@call_index,");
            strSql.Append("maill_title=@maill_title,");
            strSql.Append("content=@content");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@call_index", MySqlDbType.VarChar,50),
					new MySqlParameter("@maill_title", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.VarChar),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.call_index;
            parameters[2].Value = model.maill_title;
            parameters[3].Value = model.content;
            parameters[4].Value = model.id;

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
            strSql.Append("delete from " + databaseprefix + "mail_template ");
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
        public Model.mail_template GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,call_index,maill_title,content,is_sys");
            strSql.Append(" from " + databaseprefix + "mail_template");
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
        /// 得到一个对象实体
        /// </summary>
        public Model.mail_template GetModel(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,call_index,maill_title,content,is_sys");
            strSql.Append(" from " + databaseprefix + "mail_template");
            strSql.Append(" where call_index=@call_index limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@call_index", MySqlDbType.VarChar,50)};
            parameters[0].Value = call_index;

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
            
            strSql.Append(" id,title,call_index,maill_title,content,is_sys ");
            strSql.Append(" FROM " + databaseprefix + "mail_template ");
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
            strSql.Append("select * FROM " + databaseprefix + "mail_template");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法============================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.mail_template DataRowToModel(DataRow row)
        {
            Model.mail_template model = new Model.mail_template();
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
                if (row["maill_title"] != null)
                {
                    model.maill_title = row["maill_title"].ToString();
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