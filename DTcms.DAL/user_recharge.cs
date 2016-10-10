using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.SqlServer
{
	/// <summary>
	/// 数据访问类:用户充值记录
	/// </summary>
	public partial class user_recharge
	{
        private string databaseprefix; //数据库表名前缀
        public user_recharge(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

		#region 基本方法==============================
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "user_recharge");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.user_recharge model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_recharge(");
			strSql.Append("user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time)");
			strSql.Append(" values (");
			strSql.Append("@user_id,@user_name,@recharge_no,@payment_id,@amount,@status,@add_time,@complete_time)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@recharge_no", SqlDbType.NVarChar,100),
					new SqlParameter("@payment_id", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime)};
			parameters[0].Value = model.user_id;
			parameters[1].Value = model.user_name;
			parameters[2].Value = model.recharge_no;
			parameters[3].Value = model.payment_id;
			parameters[4].Value = model.amount;
			parameters[5].Value = model.status;
			parameters[6].Value = model.add_time;
			parameters[7].Value = model.complete_time;

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
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.user_recharge model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_recharge set ");
			strSql.Append("user_id=@user_id,");
			strSql.Append("user_name=@user_name,");
			strSql.Append("recharge_no=@recharge_no,");
			strSql.Append("payment_id=@payment_id,");
			strSql.Append("amount=@amount,");
			strSql.Append("status=@status,");
			strSql.Append("add_time=@add_time,");
			strSql.Append("complete_time=@complete_time");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@recharge_no", SqlDbType.NVarChar,100),
					new SqlParameter("@payment_id", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.user_id;
			parameters[1].Value = model.user_name;
			parameters[2].Value = model.recharge_no;
			parameters[3].Value = model.payment_id;
			parameters[4].Value = model.amount;
			parameters[5].Value = model.status;
			parameters[6].Value = model.add_time;
			parameters[7].Value = model.complete_time;
			parameters[8].Value = model.id;

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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_recharge ");
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id, string user_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "user_recharge ");
            strSql.Append(" where id=@id and user_name=@user_name");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4),
                    new SqlParameter("@user_name", SqlDbType.NVarChar,100)};
            parameters[0].Value = id;
            parameters[1].Value = user_name;

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
		public Model.user_recharge GetModel(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select top 1 id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time");
            strSql.Append(" from " + databaseprefix + "user_recharge ");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			DataSet ds=DbHelperSQL.Query(strSql.ToString(),parameters);
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
        /// 得到一个对象实体
        /// </summary>
        public Model.user_recharge GetModel(string recharge_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time");
            strSql.Append(" from " + databaseprefix + "user_recharge ");
            strSql.Append(" where recharge_no=@recharge_no");
            SqlParameter[] parameters = {
					new SqlParameter("@recharge_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = recharge_no;

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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time ");
            strSql.Append(" FROM " + databaseprefix + "user_recharge ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			return DbHelperSQL.Query(strSql.ToString());
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
			strSql.Append(" id,user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time ");
            strSql.Append(" FROM " + databaseprefix + "user_recharge ");
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
            strSql.Append("select * FROM " + databaseprefix + "user_recharge");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion

        #region 扩展方法==============================
        /// <summary>
        /// 直接充值订单
        /// </summary>
        public bool Recharge(Model.user_recharge model)
        {
            //增加一条账户余额记录
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into " + databaseprefix + "user_amount_log(");
            strSql3.Append("user_id,user_name,value,remark,add_time)");
            strSql3.Append(" values (");
            strSql3.Append("@user_id,@user_name,@value,@remark,@add_time)");
            SqlParameter[] parameters3 = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@value", SqlDbType.Decimal,5),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@add_time", SqlDbType.DateTime)};
            parameters3[0].Value = model.user_id;
            parameters3[1].Value = model.user_name;
            parameters3[2].Value = model.amount;
            parameters3[3].Value = "在线充值，单号：" + model.recharge_no;
            parameters3[4].Value = DateTime.Now;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);
            //修改用户表金额
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update " + databaseprefix + "users set amount=amount+" + model.amount);
            strSql2.Append(" where id=@id");
            SqlParameter[] parameters2 = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = model.user_id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);
            //添加充值表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "user_recharge(");
            strSql.Append("user_id,user_name,recharge_no,payment_id,amount,status,add_time,complete_time)");
            strSql.Append(" values (");
            strSql.Append("@user_id,@user_name,@recharge_no,@payment_id,@amount,@status,@add_time,@complete_time)");
            SqlParameter[] parameters = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@recharge_no", SqlDbType.NVarChar,100),
					new SqlParameter("@payment_id", SqlDbType.Int,4),
					new SqlParameter("@amount", SqlDbType.Decimal,5),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime)};
            parameters[0].Value = model.user_id;
            parameters[1].Value = model.user_name;
            parameters[2].Value = model.recharge_no;
            parameters[3].Value = model.payment_id;
            parameters[4].Value = model.amount;
            parameters[5].Value = model.status;
            parameters[6].Value = model.add_time;
            parameters[7].Value = model.complete_time;
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
        /// 确认充值订单
        /// </summary>
        public bool Confirm(string recharge_no)
        {
            Model.user_recharge model = GetModel(recharge_no); //根据充值单号得到实体
            if (model == null)
            {
                return false;
            }
            //增加一条账户余额记录
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("insert into " + databaseprefix + "user_amount_log(");
            strSql3.Append("user_id,user_name,value,remark,add_time)");
            strSql3.Append(" values (");
            strSql3.Append("@user_id,@user_name,@value,@remark,@add_time)");
            SqlParameter[] parameters3 = {
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@value", SqlDbType.Decimal,5),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@add_time", SqlDbType.DateTime)};
            parameters3[0].Value = model.user_id;
            parameters3[1].Value = model.user_name;
            parameters3[2].Value = model.amount;
            parameters3[3].Value = "在线充值，单号：" + recharge_no;
            parameters3[4].Value = DateTime.Now;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);
            //修改用户表金额
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("update " + databaseprefix + "users set amount=amount+" + model.amount);
            strSql2.Append(" where id=@id");
            SqlParameter[] parameters2 = {
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters2[0].Value = model.user_id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);
            //更新充值表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "user_recharge set ");
            strSql.Append("status=@status,");
            strSql.Append("complete_time=@complete_time");
            strSql.Append(" where recharge_no=@recharge_no");
            SqlParameter[] parameters = {
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
					new SqlParameter("@recharge_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = 1;
            parameters[1].Value = DateTime.Now;
            parameters[2].Value = recharge_no;
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
        /// 将对象转换为实体
        /// </summary>
        public Model.user_recharge DataRowToModel(DataRow row)
        {
            Model.user_recharge model = new Model.user_recharge();
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
                if (row["recharge_no"] != null)
                {
                    model.recharge_no = row["recharge_no"].ToString();
                }
                if (row["payment_id"] != null && row["payment_id"].ToString() != "")
                {
                    model.payment_id = int.Parse(row["payment_id"].ToString());
                }
                if (row["amount"] != null && row["amount"].ToString() != "")
                {
                    model.amount = decimal.Parse(row["amount"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
                if (row["complete_time"] != null && row["complete_time"].ToString() != "")
                {
                    model.complete_time = DateTime.Parse(row["complete_time"].ToString());
                }
            }
            return model;
        }
        #endregion
    }
}

