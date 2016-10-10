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
    /// 数据访问类:订单
    /// </summary>
    public partial class orders
    {
        private string databaseprefix; //数据库表名前缀
        public orders(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "orders");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "orders");
            strSql.Append(" where order_no=@order_no ");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 增加一条数据,及其子表数据
        /// </summary>
        public int Add(Model.orders model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "orders(");
            strSql.Append("order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time)");
            strSql.Append(" values (");
            strSql.Append("@order_no,@trade_no,@user_id,@user_name,@payment_id,@payment_fee,@payment_status,@payment_time,@express_id,@express_no,@express_fee,@express_status,@express_time,@accept_name,@post_code,@telphone,@mobile,@email,@area,@address,@message,@remark,@is_invoice,@invoice_title,@invoice_taxes,@payable_amount,@real_amount,@order_amount,@point,@status,@add_time,@confirm_time,@complete_time)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@trade_no", SqlDbType.NVarChar,100),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@payment_id", SqlDbType.Int,4),
					new SqlParameter("@payment_fee", SqlDbType.Decimal,5),
					new SqlParameter("@payment_status", SqlDbType.TinyInt,1),
					new SqlParameter("@payment_time", SqlDbType.DateTime),
					new SqlParameter("@express_id", SqlDbType.Int,4),
					new SqlParameter("@express_no", SqlDbType.NVarChar,100),
					new SqlParameter("@express_fee", SqlDbType.Decimal,5),
					new SqlParameter("@express_status", SqlDbType.TinyInt,1),
					new SqlParameter("@express_time", SqlDbType.DateTime),
					new SqlParameter("@accept_name", SqlDbType.NVarChar,50),
					new SqlParameter("@post_code", SqlDbType.NVarChar,20),
					new SqlParameter("@telphone", SqlDbType.NVarChar,30),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
                    new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@area", SqlDbType.NVarChar,100),
					new SqlParameter("@address", SqlDbType.NVarChar,500),
					new SqlParameter("@message", SqlDbType.NVarChar,500),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@is_invoice", SqlDbType.TinyInt,1),
					new SqlParameter("@invoice_title", SqlDbType.VarChar,100),
					new SqlParameter("@invoice_taxes", SqlDbType.Decimal,5),
					new SqlParameter("@payable_amount", SqlDbType.Decimal,5),
					new SqlParameter("@real_amount", SqlDbType.Decimal,5),
					new SqlParameter("@order_amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@confirm_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
                    new SqlParameter("@ReturnValue",SqlDbType.Int)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.trade_no;
            parameters[2].Value = model.user_id;
            parameters[3].Value = model.user_name;
            parameters[4].Value = model.payment_id;
            parameters[5].Value = model.payment_fee;
            parameters[6].Value = model.payment_status;
            parameters[7].Value = model.payment_time;
            parameters[8].Value = model.express_id;
            parameters[9].Value = model.express_no;
            parameters[10].Value = model.express_fee;
            parameters[11].Value = model.express_status;
            parameters[12].Value = model.express_time;
            parameters[13].Value = model.accept_name;
            parameters[14].Value = model.post_code;
            parameters[15].Value = model.telphone;
            parameters[16].Value = model.mobile;
            parameters[17].Value = model.email;
            parameters[18].Value = model.area;
            parameters[19].Value = model.address;
            parameters[20].Value = model.message;
            parameters[21].Value = model.remark;
            parameters[22].Value = model.is_invoice;
            parameters[23].Value = model.invoice_title;
            parameters[24].Value = model.invoice_taxes;
            parameters[25].Value = model.payable_amount;
            parameters[26].Value = model.real_amount;
            parameters[27].Value = model.order_amount;
            parameters[28].Value = model.point;
            parameters[29].Value = model.status;
            parameters[30].Value = model.add_time;
            parameters[31].Value = model.confirm_time;
            parameters[32].Value = model.complete_time;
            parameters[33].Direction = ParameterDirection.Output;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);
            sqllist.Add(cmd);

            //订单商品列表
            if (model.order_goods != null)
            {
                StringBuilder strSql2;
                StringBuilder strSql3;
                StringBuilder strSql4;
                foreach (Model.order_goods modelt in model.order_goods)
                {
                    //添加订单商品
                    strSql2 = new StringBuilder();
                    strSql2.Append("insert into " + databaseprefix + "order_goods(");
                    strSql2.Append("article_id,order_id,goods_no,goods_title,img_url,spec_text,goods_price,real_price,quantity,point)");
                    strSql2.Append(" values (");
                    strSql2.Append("@article_id,@order_id,@goods_no,@goods_title,@img_url,@spec_text,@goods_price,@real_price,@quantity,@point)");
                    SqlParameter[] parameters2 = {
					        new SqlParameter("@article_id", SqlDbType.Int,4),
					        new SqlParameter("@order_id", SqlDbType.Int,4),
                            new SqlParameter("@goods_no", SqlDbType.NVarChar,50),
					        new SqlParameter("@goods_title", SqlDbType.NVarChar,100),
					        new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					        new SqlParameter("@spec_text", SqlDbType.Text),
					        new SqlParameter("@goods_price", SqlDbType.Decimal,5),
					        new SqlParameter("@real_price", SqlDbType.Decimal,5),
					        new SqlParameter("@quantity", SqlDbType.Int,4),
					        new SqlParameter("@point", SqlDbType.Int,4)};
                    parameters2[0].Value = modelt.article_id;
                    parameters2[1].Direction = ParameterDirection.InputOutput;
                    parameters2[2].Value = modelt.goods_no;
                    parameters2[3].Value = modelt.goods_title;
                    parameters2[4].Value = modelt.img_url;
                    parameters2[5].Value = modelt.spec_text;
                    parameters2[6].Value = modelt.goods_price;
                    parameters2[7].Value = modelt.real_price;
                    parameters2[8].Value = modelt.quantity;
                    parameters2[9].Value = modelt.point;
                    cmd = new CommandInfo(strSql2.ToString(), parameters2);
                    sqllist.Add(cmd);

                    //扣减商品库存
                    strSql3 = new StringBuilder();
                    strSql3.Append("update " + databaseprefix + "article_attribute_value set ");
                    strSql3.Append("stock_quantity=stock_quantity-@stock_quantity where article_id=@article_id");
                    SqlParameter[] parameters3 = {
                            new SqlParameter("@stock_quantity", SqlDbType.Int,4),
                            new SqlParameter("@article_id", SqlDbType.Int,4)};
                    parameters3[0].Value = modelt.quantity;
                    parameters3[1].Value = modelt.article_id;
                    cmd = new CommandInfo(strSql3.ToString(), parameters3);
                    sqllist.Add(cmd);
                }
            }

            DbHelperSQL.ExecuteSqlTranWithIndentity(sqllist);
            return (int)parameters[33].Value;
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.orders model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "orders set ");
            strSql.Append("order_no=@order_no,");
            strSql.Append("trade_no=@trade_no,");
            strSql.Append("user_id=@user_id,");
            strSql.Append("user_name=@user_name,");
            strSql.Append("payment_id=@payment_id,");
            strSql.Append("payment_fee=@payment_fee,");
            strSql.Append("payment_status=@payment_status,");
            strSql.Append("payment_time=@payment_time,");
            strSql.Append("express_id=@express_id,");
            strSql.Append("express_no=@express_no,");
            strSql.Append("express_fee=@express_fee,");
            strSql.Append("express_status=@express_status,");
            strSql.Append("express_time=@express_time,");
            strSql.Append("accept_name=@accept_name,");
            strSql.Append("post_code=@post_code,");
            strSql.Append("telphone=@telphone,");
            strSql.Append("mobile=@mobile,");
            strSql.Append("email=@email,");
            strSql.Append("area=@area,");
            strSql.Append("address=@address,");
            strSql.Append("message=@message,");
            strSql.Append("remark=@remark,");
            strSql.Append("is_invoice=@is_invoice,");
            strSql.Append("invoice_title=@invoice_title,");
            strSql.Append("invoice_taxes=@invoice_taxes,");
            strSql.Append("payable_amount=@payable_amount,");
            strSql.Append("real_amount=@real_amount,");
            strSql.Append("order_amount=@order_amount,");
            strSql.Append("point=@point,");
            strSql.Append("status=@status,");
            strSql.Append("add_time=@add_time,");
            strSql.Append("confirm_time=@confirm_time,");
            strSql.Append("complete_time=@complete_time");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100),
					new SqlParameter("@trade_no", SqlDbType.NVarChar,100),
					new SqlParameter("@user_id", SqlDbType.Int,4),
					new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					new SqlParameter("@payment_id", SqlDbType.Int,4),
					new SqlParameter("@payment_fee", SqlDbType.Decimal,5),
					new SqlParameter("@payment_status", SqlDbType.TinyInt,1),
					new SqlParameter("@payment_time", SqlDbType.DateTime),
					new SqlParameter("@express_id", SqlDbType.Int,4),
					new SqlParameter("@express_no", SqlDbType.NVarChar,100),
					new SqlParameter("@express_fee", SqlDbType.Decimal,5),
					new SqlParameter("@express_status", SqlDbType.TinyInt,1),
					new SqlParameter("@express_time", SqlDbType.DateTime),
					new SqlParameter("@accept_name", SqlDbType.NVarChar,50),
					new SqlParameter("@post_code", SqlDbType.NVarChar,20),
					new SqlParameter("@telphone", SqlDbType.NVarChar,30),
					new SqlParameter("@mobile", SqlDbType.NVarChar,20),
                    new SqlParameter("@email", SqlDbType.NVarChar,30),
					new SqlParameter("@area", SqlDbType.NVarChar,100),
					new SqlParameter("@address", SqlDbType.NVarChar,500),
					new SqlParameter("@message", SqlDbType.NVarChar,500),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@is_invoice", SqlDbType.TinyInt,1),
					new SqlParameter("@invoice_title", SqlDbType.VarChar,100),
					new SqlParameter("@invoice_taxes", SqlDbType.Decimal,5),
					new SqlParameter("@payable_amount", SqlDbType.Decimal,5),
					new SqlParameter("@real_amount", SqlDbType.Decimal,5),
					new SqlParameter("@order_amount", SqlDbType.Decimal,5),
					new SqlParameter("@point", SqlDbType.Int,4),
					new SqlParameter("@status", SqlDbType.TinyInt,1),
					new SqlParameter("@add_time", SqlDbType.DateTime),
					new SqlParameter("@confirm_time", SqlDbType.DateTime),
					new SqlParameter("@complete_time", SqlDbType.DateTime),
                    new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = model.order_no;
            parameters[1].Value = model.trade_no;
            parameters[2].Value = model.user_id;
            parameters[3].Value = model.user_name;
            parameters[4].Value = model.payment_id;
            parameters[5].Value = model.payment_fee;
            parameters[6].Value = model.payment_status;
            parameters[7].Value = model.payment_time;
            parameters[8].Value = model.express_id;
            parameters[9].Value = model.express_no;
            parameters[10].Value = model.express_fee;
            parameters[11].Value = model.express_status;
            parameters[12].Value = model.express_time;
            parameters[13].Value = model.accept_name;
            parameters[14].Value = model.post_code;
            parameters[15].Value = model.telphone;
            parameters[16].Value = model.mobile;
            parameters[17].Value = model.email;
            parameters[18].Value = model.area;
            parameters[19].Value = model.address;
            parameters[20].Value = model.message;
            parameters[21].Value = model.remark;
            parameters[22].Value = model.is_invoice;
            parameters[23].Value = model.invoice_title;
            parameters[24].Value = model.invoice_taxes;
            parameters[25].Value = model.payable_amount;
            parameters[26].Value = model.real_amount;
            parameters[27].Value = model.order_amount;
            parameters[28].Value = model.point;
            parameters[29].Value = model.status;
            parameters[30].Value = model.add_time;
            parameters[31].Value = model.confirm_time;
            parameters[32].Value = model.complete_time;
            parameters[33].Value = model.id;

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
        /// 删除一条数据，及子表所有相关数据
        /// </summary>
        public bool Delete(int id)
        {
            List<CommandInfo> sqllist = new List<CommandInfo>();
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "order_goods ");
            strSql2.Append(" where order_id=@order_id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@order_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "orders ");
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
        public Model.orders GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time");
            strSql.Append(" from " + databaseprefix + "orders");
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
        /// 根据订单号返回一个实体
        /// </summary>
        public Model.orders GetModel(string order_no)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time");
            strSql.Append(" from " + databaseprefix + "orders");
            strSql.Append(" where order_no=@order_no");
            SqlParameter[] parameters = {
					new SqlParameter("@order_no", SqlDbType.NVarChar,100)};
            parameters[0].Value = order_no;

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
            strSql.Append(" id,order_no,trade_no,user_id,user_name,payment_id,payment_fee,payment_status,payment_time,express_id,express_no,express_fee,express_status,express_time,accept_name,post_code,telphone,mobile,email,area,address,message,remark,is_invoice,invoice_title,invoice_taxes,payable_amount,real_amount,order_amount,point,status,add_time,confirm_time,complete_time ");
            strSql.Append(" FROM " + databaseprefix + "orders ");
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
            strSql.Append("select * FROM " + databaseprefix + "orders");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H from " + databaseprefix + "orders ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "orders set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "orders set " + strValue);
            strSql.Append(" where order_no='" + order_no + "'");
            int rowsAffected = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public Model.orders DataRowToModel(DataRow row)
        {
            Model.orders model = new Model.orders();
            if (row != null)
            {
                #region 主表信息
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["order_no"] != null)
                {
                    model.order_no = row["order_no"].ToString();
                }
                if (row["trade_no"] != null)
                {
                    model.trade_no = row["trade_no"].ToString();
                }
                if (row["user_id"] != null && row["user_id"].ToString() != "")
                {
                    model.user_id = int.Parse(row["user_id"].ToString());
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["payment_id"] != null && row["payment_id"].ToString() != "")
                {
                    model.payment_id = int.Parse(row["payment_id"].ToString());
                }
                if (row["payment_fee"] != null && row["payment_fee"].ToString() != "")
                {
                    model.payment_fee = decimal.Parse(row["payment_fee"].ToString());
                }
                if (row["payment_status"] != null && row["payment_status"].ToString() != "")
                {
                    model.payment_status = int.Parse(row["payment_status"].ToString());
                }
                if (row["payment_time"] != null && row["payment_time"].ToString() != "")
                {
                    model.payment_time = DateTime.Parse(row["payment_time"].ToString());
                }
                if (row["express_id"] != null && row["express_id"].ToString() != "")
                {
                    model.express_id = int.Parse(row["express_id"].ToString());
                }
                if (row["express_no"] != null)
                {
                    model.express_no = row["express_no"].ToString();
                }
                if (row["express_fee"] != null && row["express_fee"].ToString() != "")
                {
                    model.express_fee = decimal.Parse(row["express_fee"].ToString());
                }
                if (row["express_status"] != null && row["express_status"].ToString() != "")
                {
                    model.express_status = int.Parse(row["express_status"].ToString());
                }
                if (row["express_time"] != null && row["express_time"].ToString() != "")
                {
                    model.express_time = DateTime.Parse(row["express_time"].ToString());
                }
                if (row["accept_name"] != null)
                {
                    model.accept_name = row["accept_name"].ToString();
                }
                if (row["post_code"] != null)
                {
                    model.post_code = row["post_code"].ToString();
                }
                if (row["telphone"] != null)
                {
                    model.telphone = row["telphone"].ToString();
                }
                if (row["mobile"] != null)
                {
                    model.mobile = row["mobile"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["area"] != null)
                {
                    model.area = row["area"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["message"] != null)
                {
                    model.message = row["message"].ToString();
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["is_invoice"] != null && row["is_invoice"].ToString() != "")
                {
                    model.is_invoice = int.Parse(row["is_invoice"].ToString());
                }
                if (row["invoice_title"] != null)
                {
                    model.invoice_title = row["invoice_title"].ToString();
                }
                if (row["invoice_taxes"] != null && row["invoice_taxes"].ToString() != "")
                {
                    model.invoice_taxes = decimal.Parse(row["invoice_taxes"].ToString());
                }
                if (row["payable_amount"] != null && row["payable_amount"].ToString() != "")
                {
                    model.payable_amount = decimal.Parse(row["payable_amount"].ToString());
                }
                if (row["real_amount"] != null && row["real_amount"].ToString() != "")
                {
                    model.real_amount = decimal.Parse(row["real_amount"].ToString());
                }
                if (row["order_amount"] != null && row["order_amount"].ToString() != "")
                {
                    model.order_amount = decimal.Parse(row["order_amount"].ToString());
                }
                if (row["point"] != null && row["point"].ToString() != "")
                {
                    model.point = int.Parse(row["point"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
                if (row["confirm_time"] != null && row["confirm_time"].ToString() != "")
                {
                    model.confirm_time = DateTime.Parse(row["confirm_time"].ToString());
                }
                if (row["complete_time"] != null && row["complete_time"].ToString() != "")
                {
                    model.complete_time = DateTime.Parse(row["complete_time"].ToString());
                }
                #endregion

                #region 子表信息
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,article_id,order_id,goods_no,goods_title,img_url,spec_text,goods_price,real_price,quantity,point");
                strSql2.Append(" from " + databaseprefix + "order_goods ");
                strSql2.Append(" where order_id=@id ");
                SqlParameter[] parameters2 = {
					    new SqlParameter("@id", SqlDbType.Int,4)};
                parameters2[0].Value = model.id;
                DataSet ds2 = DbHelperSQL.Query(strSql2.ToString(), parameters2);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    List<Model.order_goods> ls = new List<Model.order_goods>();
                    for (int n = 0; n < ds2.Tables[0].Rows.Count; n++)
                    {
                        Model.order_goods modelt = new Model.order_goods();
                        if (ds2.Tables[0].Rows[n]["id"] != null && ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["article_id"] != null && ds2.Tables[0].Rows[n]["article_id"].ToString() != "")
                        {
                            modelt.article_id = int.Parse(ds2.Tables[0].Rows[n]["article_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["order_id"] != null && ds2.Tables[0].Rows[n]["order_id"].ToString() != "")
                        {
                            modelt.order_id = int.Parse(ds2.Tables[0].Rows[n]["order_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["goods_no"] != null)
                        {
                            modelt.goods_no = ds2.Tables[0].Rows[n]["goods_no"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["goods_title"] != null)
                        {
                            modelt.goods_title = ds2.Tables[0].Rows[n]["goods_title"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["img_url"] != null)
                        {
                            modelt.img_url = ds2.Tables[0].Rows[n]["img_url"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["spec_text"] != null)
                        {
                            modelt.spec_text = ds2.Tables[0].Rows[n]["spec_text"].ToString();
                        }
                        if (ds2.Tables[0].Rows[n]["goods_price"] != null && ds2.Tables[0].Rows[n]["goods_price"].ToString() != "")
                        {
                            modelt.goods_price = decimal.Parse(ds2.Tables[0].Rows[n]["goods_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["real_price"] != null && ds2.Tables[0].Rows[n]["real_price"].ToString() != "")
                        {
                            modelt.real_price = decimal.Parse(ds2.Tables[0].Rows[n]["real_price"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["quantity"] != null && ds2.Tables[0].Rows[n]["quantity"].ToString() != "")
                        {
                            modelt.quantity = int.Parse(ds2.Tables[0].Rows[n]["quantity"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["point"] != null && ds2.Tables[0].Rows[n]["point"].ToString() != "")
                        {
                            modelt.point = int.Parse(ds2.Tables[0].Rows[n]["point"].ToString());
                        }
                        ls.Add(modelt);
                    }
                    model.order_goods = ls;
                }
                #endregion
            }
            return model;
        }
        #endregion
    }
}