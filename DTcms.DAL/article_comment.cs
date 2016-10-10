using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.SqlServer
{
    /// <summary>
    /// 数据访问类:内容评论
    /// </summary>
    public partial class article_comment
    {
        private string databaseprefix; //数据库表名前缀
        public article_comment(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法===========================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_comment");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数(AJAX分页用到)
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "article_comment ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "article_comment(");
            strSql.Append("channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time)");
            strSql.Append(" values (");
            strSql.Append("@channel_id,@article_id,@parent_id,@user_id,@user_name,@user_ip,@content,@is_lock,@add_time,@is_reply,@reply_content,@reply_time)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4),
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@user_ip", SqlDbType.NVarChar,255),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@is_reply", SqlDbType.TinyInt,1),
					new SqlParameter("@reply_content", SqlDbType.NText),
					new SqlParameter("@reply_time", SqlDbType.DateTime)};
            parameters[0].Value = model.channel_id;
            parameters[1].Value = model.article_id;
            parameters[2].Value = model.parent_id;
            parameters[3].Value = model.user_id;
            parameters[4].Value = model.user_name;
            parameters[5].Value = model.user_ip;
            parameters[6].Value = model.content;
            parameters[7].Value = model.is_lock;
            parameters[8].Value = model.add_time;
            parameters[9].Value = model.is_reply;
            parameters[10].Value = model.reply_content;
            parameters[11].Value = model.reply_time;

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
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_comment set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_comment model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_comment set ");
            strSql.Append("channel_id=@channel_id,");
            strSql.Append("article_id=@article_id,");
            strSql.Append("parent_id=@parent_id,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("user_ip=@user_ip,");
            strSql.Append("content=@content,");
            strSql.Append("is_lock=@is_lock,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("is_reply=@is_reply,");
            strSql.Append("reply_content=@reply_content,");
            strSql.Append("reply_time=@reply_time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@article_id", SqlDbType.Int,4),
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@user_ip", SqlDbType.NVarChar,255),
					new SqlParameter("@content", SqlDbType.NText),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@is_reply", SqlDbType.TinyInt,1),
					new SqlParameter("@reply_content", SqlDbType.NText),
					new SqlParameter("@reply_time", SqlDbType.DateTime)};
            parameters[0].Value = model.id;
            parameters[1].Value = model.channel_id;
            parameters[2].Value = model.article_id;
            parameters[3].Value = model.parent_id;
            parameters[4].Value = model.user_id;
            parameters[5].Value = model.user_name;
            parameters[6].Value = model.user_ip;
            parameters[7].Value = model.content;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.add_time;
            parameters[10].Value = model.is_reply;
            parameters[11].Value = model.reply_content;
            parameters[12].Value = model.reply_time;

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
            strSql.Append("delete from " + databaseprefix + "article_comment ");
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
        public Model.article_comment GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time");
            strSql.Append(" from " + databaseprefix + "article_comment ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article_comment model = new Model.article_comment();
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
        public Model.article_comment DataRowToModel(DataRow row)
        {
            Model.article_comment model = new Model.article_comment();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["channel_id"] != null && row["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(row["channel_id"].ToString());
                }
                if (row["article_id"] != null && row["article_id"].ToString() != "")
                {
                    model.article_id = int.Parse(row["article_id"].ToString());
                }
                if (row["parent_id"] != null && row["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(row["parent_id"].ToString());
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(row["user_id"].ToString());
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["user_ip"] != null)
                {
                    model.user_ip = row["user_ip"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
                if (row["is_reply"] != null && row["is_reply"].ToString() != "")
                {
                    model.is_reply = int.Parse(row["is_reply"].ToString());
                }
                if (row["reply_content"] != null)
                {
                    model.reply_content = row["reply_content"].ToString();
                }
                if (row["reply_time"] != null && row["reply_time"].ToString() != "")
                {
                    model.reply_time = DateTime.Parse(row["reply_time"].ToString());
                }
            }
            return model;
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
            strSql.Append(" id,channel_id,article_id,parent_id,user_id,user_name,user_ip,content,is_lock,add_time,is_reply,reply_content,reply_time ");
            strSql.Append(" FROM " + databaseprefix + "article_comment ");
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
            strSql.Append("select * FROM " + databaseprefix + "article_comment");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion
    }
}