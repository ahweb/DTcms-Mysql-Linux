using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
	/// <summary>
	/// 用户登录日志
	/// </summary>
	public partial class user_login_log
	{
        private string databaseprefix; //数据库表名前缀
        public user_login_log(string _databaseprefix)
		{
            databaseprefix = _databaseprefix; 
        }

		#region 基本方法=================================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_login_log order by id desc limit 1";
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
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_login_log");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
			parameters[0].Value = id;

			return DbHelperMySql.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.user_login_log model)
		{
            int newId;
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
			            StringBuilder strSql=new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "user_login_log(");
			            strSql.Append("user_id,user_name,remark,login_time,login_ip)");
			            strSql.Append(" values (");
			            strSql.Append("@user_id,@user_name,@remark,@login_time,@login_ip)");
			            strSql.Append(";select @@IDENTITY");
			            MySqlParameter[] parameters = {
					            new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@remark", MySqlDbType.VarChar,255),
					            new MySqlParameter("@login_time", MySqlDbType.Date),
					            new MySqlParameter("@login_ip", MySqlDbType.VarChar,50)};
			            parameters[0].Value = model.user_id;
			            parameters[1].Value = model.user_name;
			            parameters[2].Value = model.remark;
			            parameters[3].Value = model.login_time;
			            parameters[4].Value = model.login_ip;
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_login_log ");
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
        public Model.user_login_log GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,remark,login_time,login_ip from " + databaseprefix + "user_login_log ");
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
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" id,user_id,user_name,remark,login_time,login_ip ");
            strSql.Append(" FROM " + databaseprefix + "user_login_log ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
            strSql.Append("select * FROM " + databaseprefix + "user_login_log");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion

        #region 扩展方法=================================
        /// <summary>
        /// 同一天内是否有登录过
        /// </summary>
        public bool ExistsDay(string username)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_login_log");
            strSql.Append(" where user_name=@user_name and DATEDIFF(now(),login_time)=0");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = username;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据用户名取得最后登录的MODEL
        /// </summary>
        public Model.user_login_log GetLastModel(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,remark,login_time,login_ip from " + databaseprefix + "user_login_log");
            strSql.Append(" where user_name=@user_name order by id desc limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = user_name;

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
        /// 将对象转换为实体
        /// </summary>
        public Model.user_login_log DataRowToModel(DataRow row)
        {
            Model.user_login_log model = new Model.user_login_log();
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
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["login_time"] != null && row["login_time"].ToString() != "")
                {
                    model.login_time = DateTime.Parse(row["login_time"].ToString());
                }
                if (row["login_ip"] != null)
                {
                    model.login_ip = row["login_ip"].ToString();
                }
            }
            return model;
        }
        #endregion
    }
}

