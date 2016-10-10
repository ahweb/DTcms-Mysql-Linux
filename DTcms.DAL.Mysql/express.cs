using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
    /// <summary>
    /// 数据访问类:物流快递
    /// </summary>
    public partial class express
    {
        private string databaseprefix; //数据库表名前缀
        public express(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法============================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "express order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "express");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.express model)
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
                        strSql.Append("insert into " + databaseprefix + "express(");
                        strSql.Append("title,express_code,express_fee,website,remark,sort_id,is_lock)");
                        strSql.Append(" values (");
                        strSql.Append("@title,@express_code,@express_fee,@website,@remark,@sort_id,@is_lock)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@express_code", MySqlDbType.VarChar,100),
					            new MySqlParameter("@express_fee", MySqlDbType.Decimal,5),
					            new MySqlParameter("@website", MySqlDbType.VarChar,255),
					            new MySqlParameter("@remark", MySqlDbType.VarChar),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_lock", MySqlDbType.Int32,4)};
                        parameters[0].Value = model.title;
                        parameters[1].Value = model.express_code;
                        parameters[2].Value = model.express_fee;
                        parameters[3].Value = model.website;
                        parameters[4].Value = model.remark;
                        parameters[5].Value = model.sort_id;
                        parameters[6].Value = model.is_lock;
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
        public bool Update(Model.express model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "express set ");
            strSql.Append("title=@title,");
            strSql.Append("express_code=@express_code,");
            strSql.Append("express_fee=@express_fee,");
            strSql.Append("website=@website,");
            strSql.Append("remark=@remark,");
            strSql.Append("sort_id=@sort_id,");
            strSql.Append("is_lock=@is_lock");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@express_code", MySqlDbType.VarChar,100),
					new MySqlParameter("@express_fee", MySqlDbType.Decimal,5),
					new MySqlParameter("@website", MySqlDbType.VarChar,255),
					new MySqlParameter("@remark", MySqlDbType.VarChar),
					new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					new MySqlParameter("@is_lock", MySqlDbType.Int32,4),
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.express_code;
            parameters[2].Value = model.express_fee;
            parameters[3].Value = model.website;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.sort_id;
            parameters[6].Value = model.is_lock;
            parameters[7].Value = model.id;

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
            strSql.Append("delete from " + databaseprefix + "express ");
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
        public Model.express GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,express_code,express_fee,website,remark,sort_id,is_lock");
            strSql.Append(" from " + databaseprefix + "express");
            strSql.Append(" where id=@id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.express model = new Model.express();
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,express_code,express_fee,website,remark,sort_id,is_lock ");
            strSql.Append(" FROM " + databaseprefix + "express ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by sort_id asc,id desc");
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            
            strSql.Append(" id,title,express_code,express_fee,website,remark,sort_id,is_lock ");
            strSql.Append(" FROM " + databaseprefix + "express ");
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
            strSql.Append("select * FROM " + databaseprefix + "express");
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
        /// 返回标题名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title from " + databaseprefix + "express");
            strSql.Append(" where id=" + id);
            strSql.Append(" limit 1");
            string title = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "-";
            }
            return title;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "express set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.express DataRowToModel(DataRow row)
        {
            Model.express model = new Model.express();
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
                if (row["express_code"] != null)
                {
                    model.express_code = row["express_code"].ToString();
                }
                if (row["express_fee"] != null && row["express_fee"].ToString() != "")
                {
                    model.express_fee = decimal.Parse(row["express_fee"].ToString());
                }
                if (row["website"] != null)
                {
                    model.website = row["website"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}