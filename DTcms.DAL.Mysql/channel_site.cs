using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.Mysql
{
	/// <summary>
	/// 数据访问类:频道站点
	/// </summary>
	public partial class channel_site
	{
        private string databaseprefix; //数据库表名前缀
        public channel_site(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

        #region 基本方法================================
        /// <summary>
        /// 得到最大ID
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "channel_site order by id desc limit 1";
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
            strSql.Append("select count(1) from " + databaseprefix + "channel_site");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
			parameters[0].Value = id;

			return DbHelperMySql.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 查询生成目录名是否存在
        /// </summary>
        public bool Exists(string build_path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "channel_site");
            strSql.Append(" where build_path=@build_path ");
            MySqlParameter[] parameters = {
					new MySqlParameter("@build_path", MySqlDbType.VarChar,100)};
            parameters[0].Value = build_path;

            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.channel_site model)
		{
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //添加主表信息
			            StringBuilder strSql=new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "channel_site(");
			            strSql.Append("title,build_path,templet_path,domain,name,logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id)");
			            strSql.Append(" values (");
			            strSql.Append("@title,@build_path,@templet_path,@domain,@name,@logo,@company,@address,@tel,@fax,@email,@crod,@copyright,@seo_title,@seo_keyword,@seo_description,@is_default,@sort_id)");
			            MySqlParameter[] parameters = {
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@build_path", MySqlDbType.VarChar,100),
					            new MySqlParameter("@templet_path", MySqlDbType.VarChar,100),
					            new MySqlParameter("@domain", MySqlDbType.VarChar,255),
					            new MySqlParameter("@name", MySqlDbType.VarChar,255),
					            new MySqlParameter("@logo", MySqlDbType.VarChar,255),
					            new MySqlParameter("@company", MySqlDbType.VarChar,255),
					            new MySqlParameter("@address", MySqlDbType.VarChar,255),
					            new MySqlParameter("@tel", MySqlDbType.VarChar,30),
					            new MySqlParameter("@fax", MySqlDbType.VarChar,30),
					            new MySqlParameter("@email", MySqlDbType.VarChar,50),
					            new MySqlParameter("@crod", MySqlDbType.VarChar,100),
					            new MySqlParameter("@copyright", MySqlDbType.VarChar),
					            new MySqlParameter("@seo_title", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_keyword", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_description", MySqlDbType.VarChar,500),
					            new MySqlParameter("@is_default", MySqlDbType.Int32,4),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4)};
			            parameters[0].Value = model.title;
			            parameters[1].Value = model.build_path;
			            parameters[2].Value = model.templet_path;
			            parameters[3].Value = model.domain;
			            parameters[4].Value = model.name;
			            parameters[5].Value = model.logo;
			            parameters[6].Value = model.company;
			            parameters[7].Value = model.address;
			            parameters[8].Value = model.tel;
			            parameters[9].Value = model.fax;
			            parameters[10].Value = model.email;
			            parameters[11].Value = model.crod;
			            parameters[12].Value = model.copyright;
			            parameters[13].Value = model.seo_title;
			            parameters[14].Value = model.seo_keyword;
			            parameters[15].Value = model.seo_description;
			            parameters[16].Value = model.is_default;
			            parameters[17].Value = model.sort_id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        model.id = GetMaxId(conn, trans);
                        //添加导航菜单
                        new DAL.Mysql.navigation(databaseprefix).Add(conn, trans, "sys_contents", "channel_" + model.build_path, model.title, "", model.sort_id, 0, "Show");
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
        public bool Update(Model.channel_site model, string old_build_path)
		{
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //修改主表信息
			            StringBuilder strSql=new StringBuilder();
                        strSql.Append("update " + databaseprefix + "channel_site set ");
			            strSql.Append("title=@title,");
			            strSql.Append("build_path=@build_path,");
			            strSql.Append("templet_path=@templet_path,");
			            strSql.Append("domain=@domain,");
			            strSql.Append("name=@name,");
			            strSql.Append("logo=@logo,");
			            strSql.Append("company=@company,");
			            strSql.Append("address=@address,");
			            strSql.Append("tel=@tel,");
			            strSql.Append("fax=@fax,");
			            strSql.Append("email=@email,");
			            strSql.Append("crod=@crod,");
			            strSql.Append("copyright=@copyright,");
			            strSql.Append("seo_title=@seo_title,");
			            strSql.Append("seo_keyword=@seo_keyword,");
			            strSql.Append("seo_description=@seo_description,");
			            strSql.Append("is_default=@is_default,");
			            strSql.Append("sort_id=@sort_id");
			            strSql.Append(" where id=@id");
			            MySqlParameter[] parameters = {
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
					            new MySqlParameter("@build_path", MySqlDbType.VarChar,100),
					            new MySqlParameter("@templet_path", MySqlDbType.VarChar,100),
					            new MySqlParameter("@domain", MySqlDbType.VarChar,255),
					            new MySqlParameter("@name", MySqlDbType.VarChar,255),
					            new MySqlParameter("@logo", MySqlDbType.VarChar,255),
					            new MySqlParameter("@company", MySqlDbType.VarChar,255),
					            new MySqlParameter("@address", MySqlDbType.VarChar,255),
					            new MySqlParameter("@tel", MySqlDbType.VarChar,30),
					            new MySqlParameter("@fax", MySqlDbType.VarChar,30),
					            new MySqlParameter("@email", MySqlDbType.VarChar,50),
					            new MySqlParameter("@crod", MySqlDbType.VarChar,100),
					            new MySqlParameter("@copyright", MySqlDbType.Text),
					            new MySqlParameter("@seo_title", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_keyword", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_description", MySqlDbType.VarChar,500),
					            new MySqlParameter("@is_default", MySqlDbType.Int32,4),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
			            parameters[0].Value = model.title;
			            parameters[1].Value = model.build_path;
			            parameters[2].Value = model.templet_path;
			            parameters[3].Value = model.domain;
			            parameters[4].Value = model.name;
			            parameters[5].Value = model.logo;
			            parameters[6].Value = model.company;
			            parameters[7].Value = model.address;
			            parameters[8].Value = model.tel;
			            parameters[9].Value = model.fax;
			            parameters[10].Value = model.email;
			            parameters[11].Value = model.crod;
			            parameters[12].Value = model.copyright;
			            parameters[13].Value = model.seo_title;
			            parameters[14].Value = model.seo_keyword;
			            parameters[15].Value = model.seo_description;
			            parameters[16].Value = model.is_default;
			            parameters[17].Value = model.sort_id;
			            parameters[18].Value = model.id;
			            DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(),parameters);

                        //检查旧导航是否存在
                        if (new DAL.Mysql.navigation(databaseprefix).GetModel(conn, trans, "channel_" + old_build_path) != null)
                        {
                            //修改导航菜单
                            new DAL.Mysql.navigation(databaseprefix).Update(conn, trans, "channel_" + old_build_path, "channel_" + model.build_path, model.title, model.sort_id);
                        }
                        else //需要添加新导航菜单
                        {
                            new DAL.Mysql.navigation(databaseprefix).Add(conn, trans, "sys_contents", "channel_" + model.build_path, model.title, "", model.sort_id, 0, "Show");
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
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
            string build_path = GetBuildPath(id);
            if (string.IsNullOrEmpty(build_path))
            {
                return false;
            }
            //取得要删除的所有导航ID
            string navIds = new navigation(databaseprefix).GetIds("channel_" + build_path);

            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //删除导航
                        if (!string.IsNullOrEmpty(navIds))
                        {
                            DbHelperMySql.ExecuteSql(conn, trans, "delete from " + databaseprefix + "navigation where id in(" + navIds + ")");
                        }
                        //删除站点
			            StringBuilder strSql=new StringBuilder();
                        strSql.Append("delete from " + databaseprefix + "channel_site ");
			            strSql.Append(" where id=@id");
			            MySqlParameter[] parameters = {
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
			            parameters[0].Value = id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);

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
        public Model.channel_site GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,build_path,templet_path,domain,name,logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id");
            strSql.Append(" from " + databaseprefix + "channel_site");
            strSql.Append(" where id=@id limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.channel_site model = new Model.channel_site();
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
        public Model.channel_site GetModel(string build_path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,title,build_path,templet_path,domain,name,logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id");
            strSql.Append(" from " + databaseprefix + "channel_site");
            strSql.Append(" where build_path=@build_path limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@build_path", MySqlDbType.VarChar,50)};
            parameters[0].Value = build_path;

            Model.channel_site model = new Model.channel_site();
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
			StringBuilder strSql=new StringBuilder();
			strSql.Append("select ");
			
			strSql.Append(" id,title,build_path,templet_path,domain,name,logo,company,address,tel,fax,email,crod,copyright,seo_title,seo_keyword,seo_description,is_default,sort_id");
            strSql.Append(" from " + databaseprefix + "channel_site");
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
            strSql.Append("select * FROM " + databaseprefix + "channel_site");
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
        /// 返回站点名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title from " + databaseprefix + "channel_site");
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
        /// 返回站点名称
        /// </summary>
        public string GetTitle(string build_path)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title from " + databaseprefix + "channel_site");
            strSql.Append(" where build_path=@build_path");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@build_path", MySqlDbType.VarChar,100)};
            parameters[0].Value = build_path;
            string title = Convert.ToString(DbHelperMySql.GetSingle(strSql.ToString(), parameters));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 返回站点的生成目录名
        /// </summary>
        public string GetBuildPath(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select build_path from " + databaseprefix + "channel_site");
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
        /// 返回站点对应的导航ID
        /// </summary>
        public int GetSiteNavId(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select N.id from " + databaseprefix + "navigation as N," + databaseprefix + "channel_site as S");
            strSql.Append(" where 'channel_'+S.build_path=N.name and S.id=" + id);
            object obj = DbHelperMySql.GetSingle(strSql.ToString());
            if (obj != null)
            {
                return Convert.ToInt32(obj);
            }
            return 0;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "channel_site set " + strValue);
            strSql.Append(" where id=" + id);
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
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string build_path, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "channel_site set " + strValue);
            strSql.Append(" where build_path=@build_path");
            MySqlParameter[] parameters = {
                    new MySqlParameter("@build_path", MySqlDbType.VarChar,100)};
            parameters[0].Value = build_path;
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
        /// 将对象转换为实体
        /// </summary>
        public Model.channel_site DataRowToModel(DataRow row)
        {
            Model.channel_site model = new Model.channel_site();
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
                if (row["build_path"] != null)
                {
                    model.build_path = row["build_path"].ToString();
                }
                if (row["templet_path"] != null)
                {
                    model.templet_path = row["templet_path"].ToString();
                }
                if (row["domain"] != null)
                {
                    model.domain = row["domain"].ToString();
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["logo"] != null)
                {
                    model.logo = row["logo"].ToString();
                }
                if (row["company"] != null)
                {
                    model.company = row["company"].ToString();
                }
                if (row["address"] != null)
                {
                    model.address = row["address"].ToString();
                }
                if (row["tel"] != null)
                {
                    model.tel = row["tel"].ToString();
                }
                if (row["fax"] != null)
                {
                    model.fax = row["fax"].ToString();
                }
                if (row["email"] != null)
                {
                    model.email = row["email"].ToString();
                }
                if (row["crod"] != null)
                {
                    model.crod = row["crod"].ToString();
                }
                if (row["copyright"] != null)
                {
                    model.copyright = row["copyright"].ToString();
                }
                if (row["seo_title"] != null)
                {
                    model.seo_title = row["seo_title"].ToString();
                }
                if (row["seo_keyword"] != null)
                {
                    model.seo_keyword = row["seo_keyword"].ToString();
                }
                if (row["seo_description"] != null)
                {
                    model.seo_description = row["seo_description"].ToString();
                }
                if (row["is_default"] != null && row["is_default"].ToString() != "")
                {
                    model.is_default = int.Parse(row["is_default"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
            }
            return model;
        }
        #endregion

    }
}

