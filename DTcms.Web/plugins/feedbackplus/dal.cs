using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;


namespace DTcms.Web.Plugin.FeedbackPlus.DAL
{
	/// <summary>
	/// 数据访问类:在线留言
	/// </summary>
	public partial class feedbackplus
	{
        private string databaseprefix; //数据库表名前缀
        public feedbackplus(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "feedbackplus");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
			parameters[0].Value = id;

			return DbHelperMySql.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.feedbackplus model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "feedbackplus(");
            strSql.Append("site_path,title,content,user_name,user_tel,user_qq,user_email,add_time,is_lock)");
			strSql.Append(" values (");
            strSql.Append("@site_path,@title,@content,@user_name,@user_tel,@user_qq,@user_email,@add_time,@is_lock)");
			strSql.Append(";select @@IDENTITY");
			MySqlParameter[] parameters = {
                    new MySqlParameter("@site_path", MySqlDbType.VarChar,100),
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.LongText),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,50),
					new MySqlParameter("@user_tel", MySqlDbType.VarChar,30),
					new MySqlParameter("@user_qq", MySqlDbType.VarChar,30),
					new MySqlParameter("@user_email", MySqlDbType.VarChar,100),
					new MySqlParameter("@add_time", MySqlDbType.Date),
                    new MySqlParameter("@is_lock", MySqlDbType.Int32,4)};
            parameters[0].Value = model.site_path;
            parameters[1].Value = model.title;
			parameters[2].Value = model.content;
			parameters[3].Value = model.user_name;
			parameters[4].Value = model.user_tel;
			parameters[5].Value = model.user_qq;
			parameters[6].Value = model.user_email;
			parameters[7].Value = model.add_time;
            parameters[8].Value = model.is_lock;

			object obj = DbHelperMySql.GetSingle(strSql.ToString(),parameters);
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
            strSql.Append("update " + databaseprefix + "feedbackplus set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.feedbackplus model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "feedbackplus set ");
            strSql.Append("site_path=@site_path,");
            strSql.Append("title=@title,");
            strSql.Append("content=@content,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("user_tel=@user_tel,");
            strSql.Append("user_qq=@user_qq,");
            strSql.Append("user_email=@user_email,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("reply_content=@reply_content,");
            strSql.Append("reply_time=@reply_time,");
            strSql.Append("is_lock=@is_lock");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@site_path", MySqlDbType.VarChar,100),
                    new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.LongText),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,50),
					new MySqlParameter("@user_tel", MySqlDbType.VarChar,30),
					new MySqlParameter("@user_qq", MySqlDbType.VarChar,30),
					new MySqlParameter("@user_email", MySqlDbType.VarChar,100),
					new MySqlParameter("@add_time", MySqlDbType.Date),
					new MySqlParameter("@reply_content", MySqlDbType.LongText),
					new MySqlParameter("@reply_time", MySqlDbType.Date),
					new MySqlParameter("@is_lock", MySqlDbType.Int32,4),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.site_path;
            parameters[1].Value = model.title;
            parameters[2].Value = model.content;
            parameters[3].Value = model.user_name;
            parameters[4].Value = model.user_tel;
            parameters[5].Value = model.user_qq;
            parameters[6].Value = model.user_email;
            parameters[7].Value = model.add_time;
            parameters[8].Value = model.reply_content;
            parameters[9].Value = model.reply_time;
            parameters[10].Value = model.is_lock;
            parameters[11].Value = model.id;

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
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "feedbackplus ");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
			parameters[0].Value = id;

			int rows=DbHelperMySql.ExecuteSql(strSql.ToString(),parameters);
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
        public Model.feedbackplus GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,site_path,title,content,user_name,user_tel,user_qq,user_email,add_time,reply_content,reply_time,is_lock");
            strSql.Append(" from " + databaseprefix + "feedbackplus ");
            strSql.Append(" where id=@id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.feedbackplus model = new Model.feedbackplus();
            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["site_path"] != null && ds.Tables[0].Rows[0]["site_path"].ToString() != "")
                {
                    model.site_path = ds.Tables[0].Rows[0]["site_path"].ToString();
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["content"] != null && ds.Tables[0].Rows[0]["content"].ToString() != "")
                {
                    model.content = ds.Tables[0].Rows[0]["content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_tel"] != null && ds.Tables[0].Rows[0]["user_tel"].ToString() != "")
                {
                    model.user_tel = ds.Tables[0].Rows[0]["user_tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_qq"] != null && ds.Tables[0].Rows[0]["user_qq"].ToString() != "")
                {
                    model.user_qq = ds.Tables[0].Rows[0]["user_qq"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_email"] != null && ds.Tables[0].Rows[0]["user_email"].ToString() != "")
                {
                    model.user_email = ds.Tables[0].Rows[0]["user_email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reply_content"] != null && ds.Tables[0].Rows[0]["reply_content"].ToString() != "")
                {
                    model.reply_content = ds.Tables[0].Rows[0]["reply_content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["reply_time"] != null && ds.Tables[0].Rows[0]["reply_time"].ToString() != "")
                {
                    model.reply_time = DateTime.Parse(ds.Tables[0].Rows[0]["reply_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
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
        public DataSet GetList(int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" id,site_path,title,content,user_name,user_tel,user_qq,user_email,add_time,reply_content,reply_time,is_lock ");
            strSql.Append(" FROM " + databaseprefix + "feedbackplus ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by add_time desc");
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
            strSql.Append("select * FROM " + databaseprefix + "feedbackplus");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

		#endregion  Method
	}

    /// <summary>
    /// 数据访问类:配置文件
    /// </summary>
    public partial class install
    {
        private static object lockHelper = new object();

        public install()
        {
        }

        #region 扩展设置参数
        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.install loadConfig(string configFilePath)
        {
            return (Model.install)SerializationHelper.Load(typeof(Model.install), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.install saveConifg(Model.install model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }
        #endregion
    }
}

