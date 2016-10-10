using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// 用户生成码
    /// </summary>
    public partial class user_code
    {
        private string databaseprefix; //数据库表名前缀
        public user_code(string _databaseprefix)
        { 
            databaseprefix = _databaseprefix; 
        }

        #region 基本方法================================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "user_code order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "user_code");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string type, string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_code");
            strSql.Append(" where status=0 and datediff(now(),eff_time)<=0 and type=@type and user_name=@user_name");
            MySqlParameter[] parameters = {
					new MySqlParameter("@type", MySqlDbType.VarChar,20),
                    new MySqlParameter("@user_name", MySqlDbType.VarChar,100)};
            parameters[0].Value = type;
            parameters[1].Value = user_name;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_code model)
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
                        strSql.Append("insert into " + databaseprefix + "user_code(");
                        strSql.Append("user_id,user_name,type,str_code,count,status,user_ip,eff_time,add_time)");
                        strSql.Append(" values (");
                        strSql.Append("@user_id,@user_name,@type,@str_code,@count,@status,@user_ip,@eff_time,@add_time)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@type", MySqlDbType.VarChar,20),
					            new MySqlParameter("@str_code", MySqlDbType.VarChar,255),
                                new MySqlParameter("@count", MySqlDbType.Int32,4),
					            new MySqlParameter("@status", MySqlDbType.Int32,4),
                                new MySqlParameter("@user_ip", MySqlDbType.VarChar,20),
					            new MySqlParameter("@eff_time", MySqlDbType.Date),
					            new MySqlParameter("@add_time", MySqlDbType.Date)};
                        parameters[0].Value = model.user_id;
                        parameters[1].Value = model.user_name;
                        parameters[2].Value = model.type;
                        parameters[3].Value = model.str_code;
                        parameters[4].Value = model.count;
                        parameters[5].Value = model.status;
                        parameters[6].Value = model.user_ip;
                        parameters[7].Value = model.eff_time;
                        parameters[8].Value = model.add_time;
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
        public bool Update(Model.user_code model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_code set ");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("type=@type,");
            strSql.Append("str_code=@str_code,");
            strSql.Append("count=@count,");
            strSql.Append("status=@status,");
            strSql.Append("user_ip=@user_ip,");
            strSql.Append("eff_time=@eff_time,");
            strSql.Append("add_time=@add_time");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_id", MySqlDbType.Int32,4),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
					new MySqlParameter("@type", MySqlDbType.VarChar,20),
					new MySqlParameter("@str_code", MySqlDbType.VarChar,255),
                    new MySqlParameter("@count", MySqlDbType.Int32,4),
					new MySqlParameter("@status", MySqlDbType.Int32,4),
                    new MySqlParameter("@user_ip", MySqlDbType.VarChar,20),
					new MySqlParameter("@eff_time", MySqlDbType.Date),
					new MySqlParameter("@add_time", MySqlDbType.Date),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.type;
            parameters[3].Value = model.str_code;
            parameters[4].Value = model.count;
            parameters[5].Value = model.status;
            parameters[6].Value = model.user_ip;
            parameters[7].Value = model.eff_time;
            parameters[8].Value = model.add_time;
            parameters[9].Value = model.id;

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
            strSql.Append("delete from " + databaseprefix + "user_code ");
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
        /// 根据条件批量删除
        /// </summary>
        public bool Delete(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_code ");
            strSql.Append(" where " + strWhere);
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
        /// 得到一个对象实体
        /// </summary>
        public Model.user_code GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,type,str_code,count,status,user_ip,eff_time,add_time");
            strSql.Append(" from " + databaseprefix + "user_code ");
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
        /// 根据生成码得到一个对象实体
        /// </summary>
        public Model.user_code GetModel(string str_code)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,type,str_code,count,status,user_ip,eff_time,add_time");
            strSql.Append(" from " + databaseprefix + "user_code ");
            strSql.Append(" where status=0 and datediff(now(),eff_time)<=0 and str_code=@str_code limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@str_code", MySqlDbType.VarChar,255)};
            parameters[0].Value = str_code;

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
        /// 根据用户名得到一个对象实体
        /// </summary>
        /// <param name="datepart">日期格式,d(天)hh(小时)n(分钟)s秒</param>
        public Model.user_code GetModel(string user_name, string code_type, string datepart)
        {
            switch (datepart)
            {
                case "d": datepart = "DAY"; break;
                case "hh": datepart = "HOUR"; break;
                case "n": datepart = "MINUTE"; break;
                case "s": datepart = "SECOND"; break;
                default: datepart = "DAY"; break;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,user_id,user_name,type,str_code,count,status,user_ip,eff_time,add_time");
            strSql.Append(" from " + databaseprefix + "user_code ");
            strSql.Append(" where status=0 and TIMESTAMPDIFF('" + datepart + "',eff_time,now())<=0 and user_name=@user_name and type=@type");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@user_name", MySqlDbType.VarChar,100),
                    new MySqlParameter("@type", MySqlDbType.VarChar,20)};
            parameters[0].Value = user_name;
            parameters[1].Value = code_type;

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
            
            strSql.Append(" id,user_id,user_name,type,str_code,count,status,user_ip,eff_time,add_time ");
            strSql.Append(" FROM " + databaseprefix + "user_code ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_code");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "user_code ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperMySql.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        public Model.user_code DataRowToModel(DataRow row)
        {
            Model.user_code model = new Model.user_code();
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
                if (row["type"] != null)
                {
                    model.type = row["type"].ToString();
                }
                if (row["str_code"] != null)
                {
                    model.str_code = row["str_code"].ToString();
                }
                if (row["count"] != null && row["count"].ToString() != "")
                {
                    model.count = int.Parse(row["count"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["user_ip"] != null)
                {
                    model.user_ip = row["user_ip"].ToString();
                }
                if (row["eff_time"] != null && row["eff_time"].ToString() != "")
                {
                    model.eff_time = DateTime.Parse(row["eff_time"].ToString());
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