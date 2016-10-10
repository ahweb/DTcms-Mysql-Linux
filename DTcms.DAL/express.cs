using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.SqlServer
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "express");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.express model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "express(");
            strSql.Append("title,express_code,express_fee,website,remark,sort_id,is_lock)");
            strSql.Append(" values (");
            strSql.Append("@title,@express_code,@express_fee,@website,@remark,@sort_id,@is_lock)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@express_code", SqlDbType.NVarChar,100),
					new SqlParameter("@express_fee", SqlDbType.Decimal,5),
					new SqlParameter("@website", SqlDbType.NVarChar,255),
					new SqlParameter("@remark", SqlDbType.NText),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.express_code;
            parameters[2].Value = model.express_fee;
            parameters[3].Value = model.website;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.sort_id;
            parameters[6].Value = model.is_lock;

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
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@express_code", SqlDbType.NVarChar,100),
					new SqlParameter("@express_fee", SqlDbType.Decimal,5),
					new SqlParameter("@website", SqlDbType.NVarChar,255),
					new SqlParameter("@remark", SqlDbType.NText),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.express_code;
            parameters[2].Value = model.express_fee;
            parameters[3].Value = model.website;
            parameters[4].Value = model.remark;
            parameters[5].Value = model.sort_id;
            parameters[6].Value = model.is_lock;
            parameters[7].Value = model.id;

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
            strSql.Append("delete from " + databaseprefix + "express ");
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
        public Model.express GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,express_code,express_fee,website,remark,sort_id,is_lock");
            strSql.Append(" from " + databaseprefix + "express");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.express model = new Model.express();
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
            return DbHelperSQL.Query(strSql.ToString());
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
            strSql.Append(" id,title,express_code,express_fee,website,remark,sort_id,is_lock ");
            strSql.Append(" FROM " + databaseprefix + "express ");
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
            strSql.Append("select * FROM " + databaseprefix + "express");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
        #endregion

        #region 扩展方法============================
        /// <summary>
        /// 返回标题名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "express");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
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
            DbHelperSQL.ExecuteSql(strSql.ToString());
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