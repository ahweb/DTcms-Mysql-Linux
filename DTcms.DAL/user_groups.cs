using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.SqlServer
{
    /// <summary>
    ///  数据访问类:用户组别
    /// </summary>
    public partial class user_groups
    {
        private string databaseprefix; //数据库表名前缀
        public user_groups(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法===============================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_groups");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_groups(");
            strSql.Append("title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock)");
            strSql.Append(" values (");
            strSql.Append("@title,@grade,@upgrade_exp,@amount,@point,@discount,@is_default,@is_upgrade,@is_lock)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@grade", SqlDbType.Int,4),
					new SqlParameter("@upgrade_exp", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@discount", SqlDbType.Int,4),
					new SqlParameter("@is_default", SqlDbType.TinyInt,1),
					new SqlParameter("@is_upgrade", SqlDbType.TinyInt,1),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.grade;
            parameters[2].Value = model.upgrade_exp;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.point;
            parameters[5].Value = model.discount;
            parameters[6].Value = model.is_default;
            parameters[7].Value = model.is_upgrade;
            parameters[8].Value = model.is_lock;

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
        public bool Update(Model.user_groups model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_groups set ");
            strSql.Append("title=@title,");
            strSql.Append("grade=@grade,");
            strSql.Append("upgrade_exp=@upgrade_exp,");
            strSql.Append("amount=@amount,");
            strSql.Append("point=@point,");
            strSql.Append("discount=@discount,");
            strSql.Append("is_default=@is_default,");
            strSql.Append("is_upgrade=@is_upgrade,");
            strSql.Append("is_lock=@is_lock");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@grade", SqlDbType.Int,4),
					new SqlParameter("@upgrade_exp", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@discount", SqlDbType.Int,4),
					new SqlParameter("@is_default", SqlDbType.TinyInt,1),
					new SqlParameter("@is_upgrade", SqlDbType.TinyInt,1),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.title;
            parameters[1].Value = model.grade;
            parameters[2].Value = model.upgrade_exp;
            parameters[3].Value = model.amount;
            parameters[4].Value = model.point;
            parameters[5].Value = model.discount;
            parameters[6].Value = model.is_default;
            parameters[7].Value = model.is_upgrade;
            parameters[8].Value = model.is_lock;
            parameters[9].Value = model.id;

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
            //删除会员组价格
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "user_group_price ");
            strSql1.Append(" where group_id=@group_id ");
            SqlParameter[] parameters1 = {
                    new SqlParameter("@group_id", SqlDbType.Int,4)};
            parameters1[0].Value = id;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_groups ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
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
        public Model.user_groups GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
            strSql.Append(" from " + databaseprefix + "user_groups");
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
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock ");
            strSql.Append(" FROM " + databaseprefix + "user_groups ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }
        #endregion

        #region 扩展方法===============================
        /// <summary>
        /// 返回标题名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "user_groups");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 获取会员组折扣
        /// </summary>
        public int GetDiscount(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 discount from " + databaseprefix + "user_groups");
            strSql.Append(" where id=" + id);
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 取得默认组别实体
        /// </summary>
        public Model.user_groups GetDefault()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
            strSql.Append(" from " + databaseprefix + "user_groups");
            strSql.Append(" where is_lock=0 order by is_default desc,id asc");

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
        /// 根据经验值返回升级的组别实体
        /// </summary>
        public Model.user_groups GetUpgrade(int group_id, int exp)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,title,grade,upgrade_exp,amount,point,discount,is_default,is_upgrade,is_lock");
            strSql.Append(" from " + databaseprefix + "user_groups");
            strSql.Append(" where is_lock=0 and is_upgrade=1 and grade>(select grade from " + databaseprefix + "user_groups where id=" + group_id + ") and upgrade_exp<=" + exp);
            strSql.Append(" order by grade asc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
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
        public Model.user_groups DataRowToModel(DataRow row)
        {
            Model.user_groups model = new Model.user_groups();
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
                if (row["grade"] != null && row["grade"].ToString() != "")
                {
                    model.grade = int.Parse(row["grade"].ToString());
                }
                if (row["upgrade_exp"] != null && row["upgrade_exp"].ToString() != "")
                {
                    model.upgrade_exp = int.Parse(row["upgrade_exp"].ToString());
                }
                if (row["amount"] != null && row["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(row["amount"].ToString());
                }
                if (row["point"] != null && row["point"].ToString() != "")
                {
                    model.point = int.Parse(row["point"].ToString());
                }
                if (row["discount"] != null && row["discount"].ToString() != "")
                {
                    model.discount = int.Parse(row["discount"].ToString());
                }
                if (row["is_default"] != null && row["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(row["is_default"].ToString());
                }
                if (row["is_upgrade"] != null && row["is_upgrade"].ToString() != "")
                {
                    model.is_upgrade = int.Parse(row["is_upgrade"].ToString());
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