using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
	/// <summary>
	/// 数据访问类:会员价格
	/// </summary>
    public partial class user_group_price
    {
        private string databaseprefix; //数据库表名前缀
        public user_group_price(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法===============================
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.user_group_price GetModel(int goods_id, int group_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,goods_id,group_id,price from " + databaseprefix + "user_group_price ");
            strSql.Append(" where goods_id=@goods_id and group_id=@group_id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@goods_id", MySqlDbType.Int32,4),
                    new MySqlParameter("@group_id", MySqlDbType.Int32,4)};
            parameters[0].Value = goods_id;
            parameters[1].Value = group_id;

            Model.user_group_price model = new Model.user_group_price();
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
        /// 得到一个对象实体
        /// </summary>
        public Model.user_group_price DataRowToModel(DataRow row)
        {
            Model.user_group_price model = new Model.user_group_price();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["article_id"] != null && row["article_id"].ToString() != "")
                {
                    model.article_id = int.Parse(row["article_id"].ToString());
                }
                if (row["group_id"] != null && row["group_id"].ToString() != "")
                {
                    model.group_id = int.Parse(row["group_id"].ToString());
                }
                if (row["price"] != null && row["price"].ToString() != "")
                {
                    model.price = decimal.Parse(row["price"].ToString());
                }
            }
            return model;
        }

        #endregion
    }
}

