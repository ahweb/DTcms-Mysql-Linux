using System;
using System.Data;
using System.Text;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;
using System.Collections;

namespace DTcms.Web.Plugin.submits.DAL
{
    /// <summary>
    /// 扩展属性数据访问类:submits_field
    /// </summary>
    public partial class submits_field
    {
        private string databaseprefix; //数据库表名前缀
        public submits_field(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法========================================
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "submits_field");
            strSql.Append(" where id=@id ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 查询是否存在列
        /// </summary>
        public bool Exists(string column_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "submits_field");
            strSql.Append(" where name=@name ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@name", MySqlDbType.VarChar,100)};
            parameters[0].Value = column_name;

            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("SELECT COUNT(*) FROM information_schema.COLUMNS WHERE table_name = '" + databaseprefix + "submits'");
            strSql2.Append(" AND COLUMN_NAME = @name");
            MySqlConnection connection = new MySqlConnection(DbHelperMySql.connectionString);   
            strSql2.Append(" AND TABLE_SCHEMA = '"+ connection.Database + "'");


            //strSql2.Append("select count(0) from syscolumns");
            //strSql2.Append(" where id=object_id('" + databaseprefix + "submits') and name=@name ");
            MySqlParameter[] parameters2 = {
					new MySqlParameter("@name", MySqlDbType.VarChar,100)};
            parameters2[0].Value = column_name;

            if (DbHelperMySql.Exists(strSql.ToString(), parameters) || DbHelperMySql.Exists(strSql2.ToString(), parameters2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.submits_field model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "submits_field(");
            strSql.Append("name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys)");
            strSql.Append(" values (");
            strSql.Append("@name,@title,@control_type,@data_type,@data_length,@data_place,@item_option,@default_value,@is_required,@is_password,@is_html,@editor_type,@valid_tip_msg,@valid_error_msg,@valid_pattern,@sort_id,@is_sys)");
            strSql.Append(";set @ReturnValue= @@IDENTITY");
            MySqlParameter[] parameters = {
					new MySqlParameter("@name", MySqlDbType.VarChar,100),
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@control_type", MySqlDbType.VarChar,50),
					new MySqlParameter("@data_type", MySqlDbType.VarChar,50),
					new MySqlParameter("@data_length", MySqlDbType.Int32,4),
					new MySqlParameter("@data_place", MySqlDbType.Int32,4),
					new MySqlParameter("@item_option", MySqlDbType.LongText),
					new MySqlParameter("@default_value", MySqlDbType.LongText),
					new MySqlParameter("@is_required", MySqlDbType.Int32,4),
					new MySqlParameter("@is_password", MySqlDbType.Int32,4),
					new MySqlParameter("@is_html", MySqlDbType.Int32,4),
					new MySqlParameter("@editor_type", MySqlDbType.Int32,4),
					new MySqlParameter("@valid_tip_msg", MySqlDbType.VarChar,255),
					new MySqlParameter("@valid_error_msg", MySqlDbType.VarChar,255),
					new MySqlParameter("@valid_pattern", MySqlDbType.VarChar,500),
					new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					new MySqlParameter("@is_sys", MySqlDbType.Int32,4),
                    new MySqlParameter("@ReturnValue",MySqlDbType.Int32)};
            parameters[0].Value = model.name;
            parameters[1].Value = model.title;
            parameters[2].Value = model.control_type;
            parameters[3].Value = model.data_type;
            parameters[4].Value = model.data_length;
            parameters[5].Value = model.data_place;
            parameters[6].Value = model.item_option;
            parameters[7].Value = model.default_value;
            parameters[8].Value = model.is_required;
            parameters[9].Value = model.is_password;
            parameters[10].Value = model.is_html;
            parameters[11].Value = model.editor_type;
            parameters[12].Value = model.valid_tip_msg;
            parameters[13].Value = model.valid_error_msg;
            parameters[14].Value = model.valid_pattern;
            parameters[15].Value = model.sort_id;
            parameters[16].Value = model.is_sys;
            parameters[17].Direction = ParameterDirection.Output;

            Hashtable sqllist = new Hashtable();
            sqllist.Add(strSql.ToString(), parameters);
            

            //增加扩展字段表中一列
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("alter table " + databaseprefix + "submits_value add " + model.name + " " + model.data_type);
            MySqlParameter[] parameters2 = { };
            sqllist.Add(strSql2.ToString(), parameters2);
            

            DbHelperMySql.ExecuteSqlTran(sqllist);
            return (int)parameters[17].Value;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "submits_field set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.submits_field model)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "submits_field set ");
                        strSql.Append("name=@name,");
                        strSql.Append("title=@title,");
                        strSql.Append("control_type=@control_type,");
                        strSql.Append("data_type=@data_type,");
                        strSql.Append("data_length=@data_length,");
                        strSql.Append("data_place=@data_place,");
                        strSql.Append("item_option=@item_option,");
                        strSql.Append("default_value=@default_value,");
                        strSql.Append("is_required=@is_required,");
                        strSql.Append("is_password=@is_password,");
                        strSql.Append("is_html=@is_html,");
                        strSql.Append("editor_type=@editor_type,");
                        strSql.Append("valid_tip_msg=@valid_tip_msg,");
                        strSql.Append("valid_error_msg=@valid_error_msg,");
                        strSql.Append("valid_pattern=@valid_pattern,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("is_sys=@is_sys");
                        strSql.Append(" where id=@id");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@id", MySqlDbType.Int32,4),
					            new MySqlParameter("@name", MySqlDbType.VarChar,100),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@control_type", MySqlDbType.VarChar,50),
					            new MySqlParameter("@data_type", MySqlDbType.VarChar,50),
					            new MySqlParameter("@data_length", MySqlDbType.Int32,4),
					            new MySqlParameter("@data_place", MySqlDbType.Int32,4),
					            new MySqlParameter("@item_option", MySqlDbType.LongText),
					            new MySqlParameter("@default_value", MySqlDbType.LongText),
					            new MySqlParameter("@is_required", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_password", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_html", MySqlDbType.Int32,4),
					            new MySqlParameter("@editor_type", MySqlDbType.Int32,4),
					            new MySqlParameter("@valid_tip_msg", MySqlDbType.VarChar,255),
					            new MySqlParameter("@valid_error_msg", MySqlDbType.VarChar,255),
					            new MySqlParameter("@valid_pattern", MySqlDbType.VarChar,500),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_sys", MySqlDbType.Int32,4)};
                        parameters[0].Value = model.id;
                        parameters[1].Value = model.name;
                        parameters[2].Value = model.title;
                        parameters[3].Value = model.control_type;
                        parameters[4].Value = model.data_type;
                        parameters[5].Value = model.data_length;
                        parameters[6].Value = model.data_place;
                        parameters[7].Value = model.item_option;
                        parameters[8].Value = model.default_value;
                        parameters[9].Value = model.is_required;
                        parameters[10].Value = model.is_password;
                        parameters[11].Value = model.is_html;
                        parameters[12].Value = model.editor_type;
                        parameters[13].Value = model.valid_tip_msg;
                        parameters[14].Value = model.valid_error_msg;
                        parameters[15].Value = model.valid_pattern;
                        parameters[16].Value = model.sort_id;
                        parameters[17].Value = model.is_sys;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //修改扩展字段表中一列
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("alter table " + databaseprefix + "submits_value alter column " + model.name + " " + model.data_type);
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString());
                        //没有错误确认事务
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
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            //取得Model信息
            Model.submits_field model = GetModel(id);
            //开始删除
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //查找关联的频道ID，得到后以备使用
                        //StringBuilder strSql1 = new StringBuilder();
                        //strSql1.Append("select channel_id,field_id from " + databaseprefix + "channel_field ");
                        //strSql1.Append(" where field_id=@field_id");
                        //MySqlParameter[] parameters1 = {
                        //        new MySqlParameter("@field_id", MySqlDbType.Int32,4)};
                        //parameters1[0].Value = id;
                        //DataTable dt = DbHelperMySql.Query(conn, trans, strSql1.ToString(), parameters1).Tables[0];

                        //删除频道关联的字段表
                        //StringBuilder strSql2 = new StringBuilder();
                        //strSql2.Append("delete from " + databaseprefix + "channel_field");
                        //strSql2.Append(" where field_id=@field_id");
                        //MySqlParameter[] parameters2 = {
                        //        new MySqlParameter("@field_id", MySqlDbType.Int32,4)};
                        //parameters2[0].Value = id;
                        //DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);

                        //重建对应频道的视图
                        //if (dt.Rows.Count > 0)
                        //{
                        //    foreach (DataRow dr in dt.Rows)
                        //    {
                        //        Model.channel modelt = new DAL.channel(databaseprefix).GetModel(conn, trans, int.Parse(dr["channel_id"].ToString()));
                        //        if (modelt != null)
                        //        {
                        //            new DAL.channel(databaseprefix).RehabChannelViews(conn, trans, modelt, modelt.name);
                        //        }
                        //    }
                        //}

                        //删除主表
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("delete from " + databaseprefix + "submits_field ");
                        strSql.Append(" where id=@id");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
                        parameters[0].Value = id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        
                        //删除扩展字段表中一列
                        DbHelperMySql.ExecuteSql(conn, trans, "alter table " + databaseprefix + "submits_value drop column " + model.name);

                        //没有错误确认事务
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
        /// 得到一个对象实体
        /// </summary>
        public Model.submits_field GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  id,name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys");
            strSql.Append(" from " + databaseprefix + "submits_field ");
            strSql.Append(" where id=@id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.submits_field model = new Model.submits_field();
            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                model.name = ds.Tables[0].Rows[0]["name"].ToString();
                model.title = ds.Tables[0].Rows[0]["title"].ToString();
                model.control_type = ds.Tables[0].Rows[0]["control_type"].ToString();
                model.data_type = ds.Tables[0].Rows[0]["data_type"].ToString();
                if (ds.Tables[0].Rows[0]["data_length"].ToString() != "")
                {
                    model.data_length = int.Parse(ds.Tables[0].Rows[0]["data_length"].ToString());
                }
                if (ds.Tables[0].Rows[0]["data_place"].ToString() != "")
                {
                    model.data_place = int.Parse(ds.Tables[0].Rows[0]["data_place"].ToString());
                }
                model.item_option = ds.Tables[0].Rows[0]["item_option"].ToString();
                model.default_value = ds.Tables[0].Rows[0]["default_value"].ToString();
                if (ds.Tables[0].Rows[0]["is_required"].ToString() != "")
                {
                    model.is_required = int.Parse(ds.Tables[0].Rows[0]["is_required"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_password"].ToString() != "")
                {
                    model.is_password = int.Parse(ds.Tables[0].Rows[0]["is_password"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_html"].ToString() != "")
                {
                    model.is_html = int.Parse(ds.Tables[0].Rows[0]["is_html"].ToString());
                }
                if (ds.Tables[0].Rows[0]["editor_type"].ToString() != "")
                {
                    model.editor_type = int.Parse(ds.Tables[0].Rows[0]["editor_type"].ToString());
                }
                model.valid_tip_msg = ds.Tables[0].Rows[0]["valid_tip_msg"].ToString();
                model.valid_error_msg = ds.Tables[0].Rows[0]["valid_error_msg"].ToString();
                model.valid_pattern = ds.Tables[0].Rows[0]["valid_pattern"].ToString();
                if (ds.Tables[0].Rows[0]["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(ds.Tables[0].Rows[0]["sort_id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(ds.Tables[0].Rows[0]["is_sys"].ToString());
                }
                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得频道对应的数据
        /// </summary>
        public DataSet GetList( string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select dt_submits_field.* ");
            strSql.Append(" FROM " + databaseprefix + "submits_field ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where  " + strWhere);
            }
            strSql.Append(" order by sort_id asc," + databaseprefix + "submits_field.id desc");
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" id,name,title,control_type,data_type,data_length,data_place,item_option,default_value,is_required,is_password,is_html,editor_type,valid_tip_msg,valid_error_msg,valid_pattern,sort_id,is_sys ");
            strSql.Append(" FROM " + databaseprefix + "submits_field ");
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
            strSql.Append("select * FROM " + databaseprefix + "submits_field");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperMySql.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperMySql.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion  Method
    }
}