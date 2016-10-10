using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
	/// <summary>
	/// 数据访问类:user_attach_log
	/// </summary>
	public partial class user_attach_log
	{
        private string databaseprefix; //数据库表名前缀
        public user_attach_log(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

        #region 基本方法==============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_attach_log order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "user_attach_log");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.user_attach_log model)
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
                        strSql.Append("insert into " + databaseprefix + "user_attach_log(");
			            strSql.Append("user_id,user_name,attach_id,file_name,add_time)");
			            strSql.Append(" values (");
			            strSql.Append("@user_id,@user_name,@attach_id,@file_name,@add_time)");
			            MySqlParameter[] parameters = {
					            new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@attach_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@file_name", MySqlDbType.VarChar,255),
					            new MySqlParameter("@add_time", MySqlDbType.Date)};
			            parameters[0].Value = model.user_id;
			            parameters[1].Value = model.user_name;
			            parameters[2].Value = model.attach_id;
			            parameters[3].Value = model.file_name;
			            parameters[4].Value = model.add_time;
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
        public bool Update(Model.user_attach_log model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_attach_log set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("attach_id=@attach_id,");
            strSql.Append("file_name=@file_name,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					new MySqlParameter("@attach_id", MySqlDbType.Int32,4),
					new MySqlParameter("@file_name", MySqlDbType.VarChar,255),
					new MySqlParameter("@add_time", MySqlDbType.Date),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.attach_id;
            parameters[3].Value = model.file_name;
            parameters[4].Value = model.add_time;
            parameters[5].Value = model.id;

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
            strSql.Append("delete from " + databaseprefix + "user_attach_log ");
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
		public Model.user_attach_log GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select id,user_id,user_name,attach_id,file_name,add_time from " + databaseprefix + "user_attach_log ");
            strSql.Append(" where id=@id limit 1");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
			parameters[0].Value = id;

			DataSet ds=DbHelperMySql.Query(strSql.ToString(),parameters);
			if(ds.Tables[0].Rows.Count>0)
			{
				return DataRowToModel(ds.Tables[0].Rows[0]);
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// 获得数据列表
		/// </summary>
		public DataSet GetList(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,user_id,user_name,attach_id,file_name,add_time ");
            strSql.Append(" FROM " + databaseprefix + "user_attach_log ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperMySql.Query(strSql.ToString());
		}

		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" id,user_id,user_name,attach_id,file_name,add_time ");
            strSql.Append(" FROM " + databaseprefix + "user_attach_log ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_attach_log");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion

        #region 扩展方法==============================
        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        public Model.user_attach_log DataRowToModel(DataRow row)
        {
            Model.user_attach_log model = new Model.user_attach_log();
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
                if (row["attach_id"] != null && row["attach_id"].ToString() != "")
                {
                    model.attach_id = int.Parse(row["attach_id"].ToString());
                }
                if (row["file_name"] != null)
                {
                    model.file_name = row["file_name"].ToString();
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

