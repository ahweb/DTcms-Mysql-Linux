using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// 数据访问类:管理角色
    /// </summary>
    public partial class manager_role
    {
        private string databaseprefix; //数据库表名前缀
        public manager_role(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "manager_role order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "manager_role");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回角色名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select role_name from " + databaseprefix + "manager_role");
            strSql.Append(" where id=" + id);
            strSql.Append(" limit 1");
            string title = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.manager_role model)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "manager_role(");
                        strSql.Append("role_name,role_type,is_sys)");
                        strSql.Append(" values (");
                        strSql.Append("@role_name,@role_type,@is_sys)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@role_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@role_type", MySqlDbType.Int32,4),
                                new MySqlParameter("@is_sys", MySqlDbType.Int32,4)};
                        parameters[0].Value = model.role_name;
                        parameters[1].Value = model.role_type;
                        parameters[2].Value = model.is_sys;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        model.id = GetMaxId(conn, trans);

                        StringBuilder strSql2;
                        if (model.manager_role_values != null)
                        {
                            foreach (Model.manager_role_value modelt in model.manager_role_values)
                            {
                                strSql2 = new StringBuilder();
                                strSql2.Append("insert into " + databaseprefix + "manager_role_value(");
                                strSql2.Append("role_id,nav_name,action_type)");
                                strSql2.Append(" values (");
                                strSql2.Append("@role_id,@nav_name,@action_type)");
                                MySqlParameter[] parameters2 = {
						                new MySqlParameter("@role_id", MySqlDbType.Int32,4),
					                    new MySqlParameter("@nav_name", MySqlDbType.VarChar,100),
					                    new MySqlParameter("@action_type", MySqlDbType.VarChar,50)};
                                parameters2[0].Value = model.id;
                                parameters2[1].Value = modelt.nav_name;
                                parameters2[2].Value = modelt.action_type;
                                DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                            }
                        }
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
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.manager_role model)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "manager_role set ");
                        strSql.Append("role_name=@role_name,");
                        strSql.Append("role_type=@role_type,");
                        strSql.Append("is_sys=@is_sys");
                        strSql.Append(" where id=@id");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@role_name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@role_type", MySqlDbType.Int32,4),
                                new MySqlParameter("@is_sys", MySqlDbType.Int32,4),
                                new MySqlParameter("@id", MySqlDbType.Int32,4)};
                        parameters[0].Value = model.role_name;
                        parameters[1].Value = model.role_type;
                        parameters[2].Value = model.is_sys;
                        parameters[3].Value = model.id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //先删除该角色所有权限
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("delete from " + databaseprefix + "manager_role_value where role_id=@role_id ");
                        MySqlParameter[] parameters2 = {
					            new MySqlParameter("@role_id", MySqlDbType.Int32,4)};
                        parameters2[0].Value = model.id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);

                        //添加权限
                        if (model.manager_role_values != null)
                        {
                            StringBuilder strSql3;
                            foreach (Model.manager_role_value modelt in model.manager_role_values)
                            {
                                strSql3 = new StringBuilder();
                                strSql3.Append("insert into " + databaseprefix + "manager_role_value(");
                                strSql3.Append("role_id,nav_name,action_type)");
                                strSql3.Append(" values (");
                                strSql3.Append("@role_id,@nav_name,@action_type)");
                                MySqlParameter[] parameters3 = {
						                new MySqlParameter("@role_id", MySqlDbType.Int32,4),
					                    new MySqlParameter("@nav_name", MySqlDbType.VarChar,100),
					                    new MySqlParameter("@action_type", MySqlDbType.VarChar,50)};
                                parameters3[0].Value = model.id;
                                parameters3[1].Value = modelt.nav_name;
                                parameters3[2].Value = modelt.action_type;
                                DbHelperMySql.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);
                            }
                        }

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
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            Hashtable sqllist = new Hashtable();
            //删除管理角色权限
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "manager_role_value ");
            strSql.Append(" where role_id=@role_id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@role_id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;
            sqllist.Add(strSql.ToString(), parameters);

            //删除管理角色
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "manager_role ");
            strSql2.Append(" where id=@id");
            MySqlParameter[] parameters2 = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters2[0].Value = id;
            sqllist.Add(strSql2.ToString(), parameters2);

            bool result = DbHelperMySql.ExecuteSqlTran(sqllist);
            if (result)
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
        public Model.manager_role GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,role_name,role_type,is_sys from " + databaseprefix + "manager_role ");
            strSql.Append(" where id=@id");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.manager_role model = new Model.manager_role();
            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                #region 父表信息
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.role_name = ds.Tables[0].Rows[0]["role_name"].ToString();
                if (ds.Tables[0].Rows[0]["role_type"].ToString() != "")
                {
                    model.role_type = int.Parse(ds.Tables[0].Rows[0]["role_type"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
                }
                #endregion

                #region 子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,role_id,nav_name,action_type from " + databaseprefix + "manager_role_value ");
                strSql2.Append(" where role_id=@role_id");
                MySqlParameter[] parameters2 = {
					new MySqlParameter("@role_id", MySqlDbType.Int32,4)};
                parameters2[0].Value = id;
                DataSet ds2 = DbHelperMySql.Query(strSql2.ToString(), parameters2);
                if (ds2.Tables[0].Rows.Count > 0)
                {
                    List<Model.manager_role_value> models = new List<Model.manager_role_value>();
                    Model.manager_role_value modelt;
                    foreach (DataRow dr in ds2.Tables[0].Rows)
                    {
                        modelt = new Model.manager_role_value();
                        if (dr["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(dr["id"].ToString());
                        }
                        if (dr["role_id"].ToString() != "")
                        {
                            modelt.role_id = int.Parse(dr["role_id"].ToString());
                        }
                        modelt.nav_name = dr["nav_name"].ToString();
                        modelt.action_type = dr["action_type"].ToString();
                        models.Add(modelt);
                    }
                    model.manager_role_values = models;
                }
                #endregion

                return model;
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,role_name,role_type,is_sys ");
            strSql.Append(" FROM " + databaseprefix + "manager_role ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperMySql.Query(strSql.ToString());
        }

        #endregion  Method
    }
}