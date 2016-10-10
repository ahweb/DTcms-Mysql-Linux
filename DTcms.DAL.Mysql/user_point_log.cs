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
    /// 积分记录日志
    /// </summary>
    public partial class user_point_log
    {
        private string databaseprefix; //数据库表名前缀
        public user_point_log(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法==============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_point_log order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "user_point_log");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_point_log model, bool is_upgrade)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "user_point_log(");
                        strSql.Append("user_id,user_name,value,remark,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@user_id,@user_name,@value,@remark,@add_time)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@value", MySqlDbType.Int32,4),
					            new MySqlParameter("@remark", MySqlDbType.VarChar,500),
					            new MySqlParameter("@add_time", MySqlDbType.Date)};
                        parameters[0].Value = model.user_id;
                        parameters[1].Value = model.user_name;
                        parameters[2].Value = model.value;
                        parameters[3].Value = model.remark;
                        parameters[4].Value = model.add_time;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        model.id = GetMaxId(conn, trans);

                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("update " + databaseprefix + "users set point=point+" + model.value);
                        if (model.value > 0 && is_upgrade)
                        {
                            strSql2.Append(",exp=exp+" + model.value);
                        }
                        strSql2.Append(" where id=@id");
                        MySqlParameter[] parameters2 = {
                                new MySqlParameter("@id", MySqlDbType.Int32,4)};
                        parameters2[0].Value = model.user_id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return -1;
                    }
                }
            }
            return model.id;
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_point_log ");
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
        /// 根据用户名删除一条数据
        /// </summary>
        public bool Delete(int id, string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_point_log ");
            strSql.Append(" where id=@id and user_name=@user_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4),
                    new MySqlParameter("@user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = id;
            parameters[1].Value = user_name;

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
        public Model.user_point_log GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,value,remark,add_time");
            strSql.Append(" from " + databaseprefix + "user_point_log ");
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            
            strSql.Append(" id,user_id,user_name,value,remark,add_time ");
            strSql.Append(" FROM " + databaseprefix + "user_point_log ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_point_log");
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
        public Model.user_point_log DataRowToModel(DataRow row)
        {
            Model.user_point_log model = new Model.user_point_log();
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
                if (row["value"] != null && row["value"].ToString() != "")
                {
                    model.value = int.Parse(row["value"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
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