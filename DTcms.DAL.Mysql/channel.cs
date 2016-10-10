using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
	/// <summary>
	/// 数据访问类:频道
	/// </summary>
	public partial class channel
	{
        private string databaseprefix; //数据库表名前缀
        public channel(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "channel order by id desc limit 1";
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
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "channel");
			strSql.Append(" where id=@id ");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
			parameters[0].Value = id;

			return DbHelperMySql.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 查询是否存在该记录
        /// </summary>
        public bool Exists(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "channel");
            strSql.Append(" where name=@name ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@name", MySqlDbType.VarChar,50)};
            parameters[0].Value = name;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "channel");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperMySql.GetSingle(strSql.ToString()));
        }

		/// <summary>
        /// 增加一条数据及其子表
		/// </summary>
		public int Add(Model.channel model)
		{
            //取得站点对应的导航ID
            int parent_id = new DAL.Mysql.channel_site(databaseprefix).GetSiteNavId(model.site_id);
            if (parent_id == 0)
            {
                return 0;
            }
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "channel(");
			            strSql.Append("site_id,name,title,is_albums,is_attach,is_spec,sort_id)");
			            strSql.Append(" values (");
			            strSql.Append("@site_id,@name,@title,@is_albums,@is_attach,@is_spec,@sort_id)");
			            MySqlParameter[] parameters = {
					            new MySqlParameter("@site_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@name", MySqlDbType.VarChar,50),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@is_albums", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_attach", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_spec", MySqlDbType.Int32,4),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4)};
			            parameters[0].Value = model.site_id;
			            parameters[1].Value = model.name;
			            parameters[2].Value = model.title;
			            parameters[3].Value = model.is_albums;
			            parameters[4].Value = model.is_attach;
			            parameters[5].Value = model.is_spec;
			            parameters[6].Value = model.sort_id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        model.id = GetMaxId(conn, trans);

                        //扩展字段
                        if (model.channel_fields != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.channel_field modelt in model.channel_fields)
                            {
                                strSql2 = new StringBuilder();
                                strSql2.Append("insert into " + databaseprefix + "channel_field(");
                                strSql2.Append("channel_id,field_id)");
                                strSql2.Append(" values (");
                                strSql2.Append("@channel_id,@field_id)");
                                MySqlParameter[] parameters2 = {
					                    new MySqlParameter("@channel_id", MySqlDbType.Int32,4),
					                    new MySqlParameter("@field_id", MySqlDbType.Int32,4)};
                                parameters2[0].Value = model.id;
                                parameters2[1].Value = modelt.field_id;
                                DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                            }
                        }

                        //添加视图
                        StringBuilder strSql3 = new StringBuilder();
                        strSql3.Append("CREATE VIEW view_channel_" + model.name + " as");
                        strSql3.Append(" SELECT " + databaseprefix + "article.*");
                        if (model.channel_fields != null)
                        {
                            foreach (Model.channel_field modelt in model.channel_fields)
                            {
                                Model.article_attribute_field fieldModel = new DAL.Mysql.article_attribute_field(databaseprefix).GetModel(modelt.field_id);
                                if (fieldModel != null)
                                {
                                    strSql3.Append("," + databaseprefix + "article_attribute_value." + fieldModel.name);
                                }
                            }
                        }
                        strSql3.Append(" FROM " + databaseprefix + "article_attribute_value INNER JOIN");
                        strSql3.Append(" " + databaseprefix + "article ON " + databaseprefix + "article_attribute_value.article_id = " + databaseprefix + "article.id");
                        strSql3.Append(" WHERE " + databaseprefix + "article.channel_id=" + model.id);
                        DbHelperMySql.ExecuteSql(conn, trans, strSql3.ToString());

                        //添加导航菜单
                        int newNavId = new DAL.Mysql.navigation(databaseprefix).Add(conn, trans, parent_id, "channel_" + model.name, model.title, "", model.sort_id, model.id, "Show");
                        new DAL.Mysql.navigation(databaseprefix).Add(conn, trans, newNavId, "channel_" + model.name + "_list", "内容管理", "article/article_list.aspx", 99, model.id, "Show,View,Add,Edit,Delete,Audit");
                        new DAL.Mysql.navigation(databaseprefix).Add(conn, trans, newNavId, "channel_" + model.name + "_category", "栏目类别", "article/category_list.aspx", 100, model.id, "Show,View,Add,Edit,Delete");
                        new DAL.Mysql.navigation(databaseprefix).Add(conn, trans, newNavId, "channel_" + model.name + "_comment", "评论管理", "article/comment_list.aspx", 101, model.id, "Show,View,Delete,Reply");

                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
            return model.id;
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.channel model)
		{
            Model.channel oldModel = GetModel(model.id); //旧的数据
            //取得站点对应的导航ID
            int parent_id = new DAL.Mysql.channel_site(databaseprefix).GetSiteNavId(model.site_id);
            if (parent_id == 0)
            {
                return false;
            }
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
			            StringBuilder strSql=new StringBuilder();
			            strSql.Append("update " + databaseprefix + "channel set ");
			            strSql.Append("site_id=@site_id,");
			            strSql.Append("name=@name,");
			            strSql.Append("title=@title,");
			            strSql.Append("is_albums=@is_albums,");
			            strSql.Append("is_attach=@is_attach,");
			            strSql.Append("is_spec=@is_spec,");
			            strSql.Append("sort_id=@sort_id");
			            strSql.Append(" where id=@id ");
			            MySqlParameter[] parameters = {
					            new MySqlParameter("@site_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@name", MySqlDbType.VarChar,50),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@is_albums", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_attach", MySqlDbType.Int32,4),
					            new MySqlParameter("@is_spec", MySqlDbType.Int32,4),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
			            parameters[0].Value = model.site_id;
			            parameters[1].Value = model.name;
			            parameters[2].Value = model.title;
			            parameters[3].Value = model.is_albums;
			            parameters[4].Value = model.is_attach;
			            parameters[5].Value = model.is_spec;
			            parameters[6].Value = model.sort_id;
			            parameters[7].Value = model.id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //删除已移除的扩展字段
                        FieldDelete(conn, trans, model.channel_fields, model.id);
                        //添加扩展字段
                        if (model.channel_fields != null)
                        {
                            StringBuilder strSql2;
                            foreach (Model.channel_field modelt in model.channel_fields)
                            {
                                strSql2 = new StringBuilder();
                                Model.channel_field fieldModel = null;
                                if (oldModel.channel_fields != null)
                                {
                                    fieldModel = oldModel.channel_fields.Find(p => p.field_id == modelt.field_id); //查找是否已经存在
                                }
                                if (fieldModel == null) //如果不存在则添加
                                {
                                    strSql2.Append("insert into " + databaseprefix + "channel_field(");
                                    strSql2.Append("channel_id,field_id)");
                                    strSql2.Append(" values (");
                                    strSql2.Append("@channel_id,@field_id)");
                                    MySqlParameter[] parameters2 = {
					                        new MySqlParameter("@channel_id", MySqlDbType.Int32,4),
					                        new MySqlParameter("@field_id", MySqlDbType.Int32,4)};
                                    parameters2[0].Value = modelt.channel_id;
                                    parameters2[1].Value = modelt.field_id;
                                    DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                                }
                            }
                        }
                        //删除旧视图重建新视图
                        RehabChannelViews(conn, trans, model, oldModel.name);

                        //修改对应的导航
                        new DAL.Mysql.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name, parent_id, "channel_" + model.name, model.title, model.sort_id);
                        new DAL.Mysql.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name + "_list", "channel_" + model.name + "_list"); //内容管理
                        new DAL.Mysql.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name + "_category", "channel_" + model.name + "_category"); //栏目类别
                        new DAL.Mysql.navigation(databaseprefix).Update(conn, trans, "channel_" + oldModel.name + "_comment", "channel_" + model.name + "_comment"); //评论管理

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
            //取得频道的名称
            string channel_name = GetChannelName(id);
            if (string.IsNullOrEmpty(channel_name))
            {
                return false;
            }
            //取得要删除的所有导航ID
            string navIds = new navigation(databaseprefix).GetIds("channel_" + channel_name);

            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //删除导航主表
                        if (!string.IsNullOrEmpty(navIds))
                        {
                            DbHelperMySql.ExecuteSql(conn, trans, "delete from " + databaseprefix + "navigation where id in(" + navIds + ")");
                        }

                        //删除视图
                        StringBuilder strSql1 = new StringBuilder();
                        //strSql1.Append("if exists (select 1 from sysobjects where id = object_id('view_channel_" + channel_name + "') and type = 'V')");
                        strSql1.Append("drop view view_channel_" + channel_name);
                        DbHelperMySql.ExecuteSql(conn, trans, strSql1.ToString());

                        //删除频道扩展字段表
                        StringBuilder strSql2 = new StringBuilder();
                        strSql2.Append("delete from " + databaseprefix + "channel_field ");
                        strSql2.Append(" where channel_id=@channel_id ");
                        MySqlParameter[] parameters2 = {
					            new MySqlParameter("@channel_id", MySqlDbType.Int32,4)};
                        parameters2[0].Value = id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);

                        //删除频道表
                        StringBuilder strSql3 = new StringBuilder();
                        strSql3.Append("delete from " + databaseprefix + "channel ");
                        strSql3.Append(" where id=@id ");
                        MySqlParameter[] parameters3 = {
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
                        parameters3[0].Value = id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);

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
        public Model.channel GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,site_id,name,title,is_albums,is_attach,is_spec,sort_id from " + databaseprefix + "channel ");
            strSql.Append(" where id=@id ");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.channel model = new Model.channel();
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
        public Model.channel GetModel(string channel_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,site_id,name,title,is_albums,is_attach,is_spec,sort_id from " + databaseprefix + "channel ");
            strSql.Append(" where name=@channel_name ");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@channel_name", MySqlDbType.VarChar,50)};
            parameters[0].Value = channel_name;

            Model.channel model = new Model.channel();
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
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
            StringBuilder strSql = new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" id,site_id,name,title,is_albums,is_attach,is_spec,sort_id ");
            strSql.Append(" FROM " + databaseprefix + "channel ");
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
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
            strSql.Append("select * FROM " + databaseprefix + "channel");
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
        /// 将对象转换实体
        /// </summary>
        public Model.channel DataRowToModel(DataRow row)
        {
            Model.channel model = new Model.channel();
            if (row != null)
            {
                #region 主表信息======================
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["site_id"] != null && row["site_id"].ToString() != "")
                {
                    model.site_id = int.Parse(row["site_id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["is_albums"] != null && row["is_albums"].ToString() != "")
                {
                    model.is_albums = int.Parse(row["is_albums"].ToString());
                }
                if (row["is_attach"] != null && row["is_attach"].ToString() != "")
                {
                    model.is_attach = int.Parse(row["is_attach"].ToString());
                }
                if (row["is_spec"] != null && row["is_spec"].ToString() != "")
                {
                    model.is_spec = int.Parse(row["is_spec"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                #endregion

                #region 子表信息======================
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,channel_id,field_id from " + databaseprefix + "channel_field ");
                strSql2.Append(" where channel_id=@channel_id ");
                MySqlParameter[] parameters2 = {
					    new MySqlParameter("@channel_id", MySqlDbType.Int32,4)};
                parameters2[0].Value = model.id;
                DataSet ds2 = DbHelperMySql.Query(strSql2.ToString(), parameters2);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    int i = ds2.Tables[0].Rows.Count;
                    List<Model.channel_field> models = new List<Model.channel_field>();
                    Model.channel_field modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new Model.channel_field();
                        if (ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["channel_id"].ToString() != "")
                        {
                            modelt.channel_id = int.Parse(ds2.Tables[0].Rows[n]["channel_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["field_id"].ToString() != "")
                        {
                            modelt.field_id = int.Parse(ds2.Tables[0].Rows[n]["field_id"].ToString());
                        }
                        models.Add(modelt);
                    }
                    model.channel_fields = models;
                }
                #endregion
            }
            return model;
        }

        /// <summary>
        /// 将对象转换实体
        /// </summary>
        public Model.channel DataRowToModel(MySqlConnection conn, MySqlTransaction trans, DataRow row)
        {
            Model.channel model = new Model.channel();
            if (row != null)
            {
                #region 主表信息======================
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["site_id"] != null && row["site_id"].ToString() != "")
                {
                    model.site_id = int.Parse(row["site_id"].ToString());
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["is_albums"] != null && row["is_albums"].ToString() != "")
                {
                    model.is_albums = int.Parse(row["is_albums"].ToString());
                }
                if (row["is_attach"] != null && row["is_attach"].ToString() != "")
                {
                    model.is_attach = int.Parse(row["is_attach"].ToString());
                }
                if (row["is_spec"] != null && row["is_spec"].ToString() != "")
                {
                    model.is_spec = int.Parse(row["is_spec"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                #endregion

                #region 子表信息======================
                StringBuilder strSql2 = new StringBuilder();
                strSql2.Append("select id,channel_id,field_id from " + databaseprefix + "channel_field ");
                strSql2.Append(" where channel_id=@channel_id limit 1");
                MySqlParameter[] parameters2 = {
					    new MySqlParameter("@channel_id", MySqlDbType.Int32,4)};
                parameters2[0].Value = model.id;
                DataSet ds2 = DbHelperMySql.Query(conn, trans, strSql2.ToString(), parameters2);

                if (ds2.Tables[0].Rows.Count > 0)
                {
                    int i = ds2.Tables[0].Rows.Count;
                    List<Model.channel_field> models = new List<Model.channel_field>();
                    Model.channel_field modelt;
                    for (int n = 0; n < i; n++)
                    {
                        modelt = new Model.channel_field();
                        if (ds2.Tables[0].Rows[n]["id"].ToString() != "")
                        {
                            modelt.id = int.Parse(ds2.Tables[0].Rows[n]["id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["channel_id"].ToString() != "")
                        {
                            modelt.channel_id = int.Parse(ds2.Tables[0].Rows[n]["channel_id"].ToString());
                        }
                        if (ds2.Tables[0].Rows[n]["field_id"].ToString() != "")
                        {
                            modelt.field_id = int.Parse(ds2.Tables[0].Rows[n]["field_id"].ToString());
                        }
                        models.Add(modelt);
                    }
                    model.channel_fields = models;
                }
                #endregion
            }
            return model;
        }

        /// <summary>
        /// 得到一个对象实体，带事务
        /// </summary>
        public Model.channel GetModel(MySqlConnection conn, MySqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,site_id,name,title,is_albums,is_attach,is_spec,sort_id from " + databaseprefix + "channel ");
            strSql.Append(" where id=@id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.channel model = new Model.channel();
            DataSet ds = DbHelperMySql.Query(conn, trans, strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(conn, trans, ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "channel set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 返回频道名称
        /// </summary>
        public string GetChannelName(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select name from " + databaseprefix + "channel");
            strSql.Append(" where id=" + id);
            strSql.Append(" limit 1");
            object obj = DbHelperMySql.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return Convert.ToString(obj);
            }
            return string.Empty;
        }

        /// <summary>
        /// 返回频道ID
        /// </summary>
        public int GetChannelId(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "channel");
            strSql.Append(" where name=@name ");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@name", MySqlDbType.VarChar,50)};
            parameters[0].Value = name;
            object obj = DbHelperMySql.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        /// <summary>
        /// 删除及重建该频道视图
        /// </summary>
        public void RehabChannelViews(MySqlConnection conn, MySqlTransaction trans, Model.channel model, string old_name)
        {
            //删除旧的视图
            StringBuilder strSql1 = new StringBuilder();
            //strSql1.Append("if exists (select 1 from sysobjects where id = object_id('view_channel_" + old_name + "') and type = 'V')");
            try
            {
                strSql1.Append("drop view view_channel_" + old_name);
                DbHelperMySql.ExecuteSql(conn, trans, strSql1.ToString());
            }
            catch { }
            
            //添加视图
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("CREATE VIEW view_channel_" + model.name + " as");
            strSql2.Append(" SELECT " + databaseprefix + "article.*");
            if (model.channel_fields != null)
            {
                foreach (Model.channel_field modelt in model.channel_fields)
                {
                    Model.article_attribute_field fieldModel = new DAL.Mysql.article_attribute_field(databaseprefix).GetModel(modelt.field_id);
                    if (fieldModel != null)
                    {
                        strSql2.Append("," + databaseprefix + "article_attribute_value." + fieldModel.name);
                    }
                }
            }
            strSql2.Append(" FROM " + databaseprefix + "article_attribute_value INNER JOIN");
            strSql2.Append(" " + databaseprefix + "article ON " + databaseprefix + "article_attribute_value.article_id = " + databaseprefix + "article.id");
            strSql2.Append(" WHERE " + databaseprefix + "article.channel_id=" + model.id);
            DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString());
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 删除已移除的频道扩展字段
        /// </summary>
        private void FieldDelete(MySqlConnection conn, MySqlTransaction trans, List<Model.channel_field> models, int channel_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,field_id from " + databaseprefix + "channel_field");
            strSql.Append(" where channel_id=" + channel_id);
            DataSet ds = DbHelperMySql.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                Model.channel_field model = models.Find(p => p.field_id == int.Parse(dr["field_id"].ToString())); //查找对应的字段ID
                if (model == null)
                {
                    DbHelperMySql.ExecuteSql(conn, trans, "delete from " + databaseprefix + "channel_field where channel_id=" + channel_id + " and field_id=" + dr["field_id"].ToString()); //删除该字段
                }
            }
        }
        #endregion
    }
}

