using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.SqlServer
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
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_login_log");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.user_login_log model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_login_log(");
			strSql.Append("user_id,user_name,remark,login_time,login_ip)");
			strSql.Append(" values (");
			strSql.Append("@user_id,@user_name,@remark,@login_time,@login_ip)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@remark", SqlDbType.NVarChar,255),
					new SqlParameter("@login_time", SqlDbType.DateTime),
					new SqlParameter("@login_ip", SqlDbType.NVarChar,50)};
			parameters[0].Value = model.user_id;
			parameters[1].Value = model.user_name;
			parameters[2].Value = model.remark;
			parameters[3].Value = model.login_time;
			parameters[4].Value = model.login_ip;

			object obj = DbHelperSQL.GetSingle(strSql.ToString(),parameters);
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_login_log ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			int rows=DbHelperSQL.ExecuteSql(strSql.ToString(),parameters);
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
            strSql.Append("select top 1 id,user_id,user_name,remark,login_time,login_ip from " + databaseprefix + "user_login_log ");
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
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			if(Top>0)
			{
				strSql.Append(" top "+Top.ToString());
			}
			strSql.Append(" id,user_id,user_name,remark,login_time,login_ip ");
            strSql.Append(" FROM " + databaseprefix + "user_login_log ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
            strSql.Append("select * FROM " + databaseprefix + "user_login_log");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
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
            strSql.Append(" where user_name=@user_name and DATEDIFF(day,login_time,getdate())=0");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = username;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 根据用户名取得最后登录的MODEL
        /// </summary>
        public Model.user_login_log GetLastModel(string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,user_id,user_name,remark,login_time,login_ip from " + databaseprefix + "user_login_log");
            strSql.Append(" where user_name=@user_name order by id desc");
            SqlParameter[] parameters = {
					new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = user_name;

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

