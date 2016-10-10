using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;

namespace DTcms.DAL.SqlServer
{
	/// <summary>
	/// 数据访问类:后台导航菜单
	/// </summary>
	public partial class navigation
	{
        private string databaseprefix; //数据库表名前缀
        public navigation(string _databaseprefix)
        {
            databaseprefix = _databaseprefix;
        }

        #region 基本方法===============================
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "navigation");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = id;

			return DbHelperSQL.Exists(strSql.ToString(),parameters);
		}

        /// <summary>
        /// 查询是否存在该记录
        /// </summary>
        public bool Exists(string name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "navigation");
            strSql.Append(" where name=@name ");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.VarChar,50)};
            parameters[0].Value = name;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.navigation model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "navigation(");
			strSql.Append("parent_id,channel_id,nav_type,name,title,sub_title,icon_url,link_url,sort_id,is_lock,remark,action_type,is_sys)");
			strSql.Append(" values (");
			strSql.Append("@parent_id,@channel_id,@nav_type,@name,@title,@sub_title,@icon_url,@link_url,@sort_id,@is_lock,@remark,@action_type,@is_sys)");
			strSql.Append(";select @@IDENTITY");
			SqlParameter[] parameters = {
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@nav_type", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@sub_title", SqlDbType.NVarChar,100),
					new SqlParameter("@icon_url", SqlDbType.NVarChar,255),
					new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@action_type", SqlDbType.NVarChar,500),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1)};
			parameters[0].Value = model.parent_id;
			parameters[1].Value = model.channel_id;
			parameters[2].Value = model.nav_type;
			parameters[3].Value = model.name;
			parameters[4].Value = model.title;
			parameters[5].Value = model.sub_title;
			parameters[6].Value = model.icon_url;
			parameters[7].Value = model.link_url;
			parameters[8].Value = model.sort_id;
			parameters[9].Value = model.is_lock;
			parameters[10].Value = model.remark;
			parameters[11].Value = model.action_type;
			parameters[12].Value = model.is_sys;

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
		public bool Update(Model.navigation model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set ");
			strSql.Append("parent_id=@parent_id,");
			strSql.Append("channel_id=@channel_id,");
			strSql.Append("nav_type=@nav_type,");
			strSql.Append("name=@name,");
			strSql.Append("title=@title,");
			strSql.Append("sub_title=@sub_title,");
			strSql.Append("icon_url=@icon_url,");
			strSql.Append("link_url=@link_url,");
			strSql.Append("sort_id=@sort_id,");
			strSql.Append("is_lock=@is_lock,");
			strSql.Append("remark=@remark,");
			strSql.Append("action_type=@action_type,");
			strSql.Append("is_sys=@is_sys");
			strSql.Append(" where id=@id");
			SqlParameter[] parameters = {
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@nav_type", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@sub_title", SqlDbType.NVarChar,100),
					new SqlParameter("@icon_url", SqlDbType.NVarChar,255),
					new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@remark", SqlDbType.NVarChar,500),
					new SqlParameter("@action_type", SqlDbType.NVarChar,500),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
					new SqlParameter("@id", SqlDbType.Int,4)};
			parameters[0].Value = model.parent_id;
			parameters[1].Value = model.channel_id;
			parameters[2].Value = model.nav_type;
			parameters[3].Value = model.name;
			parameters[4].Value = model.title;
			parameters[5].Value = model.sub_title;
			parameters[6].Value = model.icon_url;
			parameters[7].Value = model.link_url;
			parameters[8].Value = model.sort_id;
			parameters[9].Value = model.is_lock;
			parameters[10].Value = model.remark;
			parameters[11].Value = model.action_type;
			parameters[12].Value = model.is_sys;
			parameters[13].Value = model.id;

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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "navigation");
            strSql.Append(" where id in(" + GetIds(id) + ")");

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public Model.navigation GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,parent_id,channel_id,nav_type,name,title,sub_title,icon_url,link_url,sort_id,is_lock,remark,action_type,is_sys");
            strSql.Append(" from " + databaseprefix + "navigation ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.navigation model = new Model.navigation();
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
        /// 得到一个对象实体
        /// </summary>
        public Model.navigation GetModel(string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,parent_id,channel_id,nav_type,name,title,sub_title,icon_url,link_url,sort_id,is_lock,remark,action_type,is_sys");
            strSql.Append(" from " + databaseprefix + "navigation ");
            strSql.Append(" where name=@nav_name");
            SqlParameter[] parameters = {
					new SqlParameter("@nav_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = nav_name;

            Model.navigation model = new Model.navigation();
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
        /// 获取类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="nav_type">导航类别</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id, string nav_type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,parent_id,channel_id,nav_type,name,title,sub_title,icon_url,link_url,sort_id,is_lock,remark,action_type,is_sys");
            strSql.Append(" FROM " + databaseprefix + "navigation");
            strSql.Append(" where nav_type='" + nav_type + "'");
            strSql.Append(" order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            //重组列表
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //创建一个新的DataTable增加一个深度字段
            DataTable newData = new DataTable();
            newData.Columns.Add("id", typeof(int));
            newData.Columns.Add("parent_id", typeof(int));
            newData.Columns.Add("channel_id", typeof(int));
            newData.Columns.Add("class_layer", typeof(int));
            newData.Columns.Add("nav_type", typeof(string));
            newData.Columns.Add("name", typeof(string));
            newData.Columns.Add("title", typeof(string));
            newData.Columns.Add("sub_title", typeof(string));
            newData.Columns.Add("icon_url", typeof(string));
            newData.Columns.Add("link_url", typeof(string));
            newData.Columns.Add("sort_id", typeof(int));
            newData.Columns.Add("is_lock", typeof(int));
            newData.Columns.Add("remark", typeof(string));
            newData.Columns.Add("action_type", typeof(string));
            newData.Columns.Add("is_sys", typeof(int));
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parent_id, 0);
            return newData;
        }
		#endregion

        #region 扩展方法===============================
        /// <summary>
        /// 根据导航的名称查询其ID
        /// </summary>
        public int GetNavId(string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql.Append(" where name=@nav_name");
            SqlParameter[] parameters = {
					new SqlParameter("@nav_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = nav_name;
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString(), parameters));
            return Utils.StrToInt(str, 0);
        }

        /// <summary>
        /// 获取父类下的所有子类ID(含自己本身)
        /// </summary>
        public string GetIds(int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,parent_id");
            strSql.Append(" FROM " + databaseprefix + "navigation");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            string ids = parent_id.ToString() + ",";
            GetChildIds(ds.Tables[0], parent_id, ref ids);
            return ids.TrimEnd(',');
        }

        /// <summary>
        /// 获取父类下的所有子类ID(含自己本身)
        /// </summary>
        public string GetIds(string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql.Append(" where name=@nav_name");
            SqlParameter[] parameters = {
					new SqlParameter("@nav_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = nav_name;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetIds(Convert.ToInt32(obj));
            }
            return string.Empty;
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set " + strValue);
            strSql.Append(" where id=" + id);
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        public bool UpdateField(string name, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set " + strValue);
            strSql.Append(" where name='" + name + "'");
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
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
        /// 修改一条记录，带事务
        /// </summary>
        public bool Update(SqlConnection conn, SqlTransaction trans, string old_name, string new_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set name=@new_name");
            strSql.Append(" where name=@old_name");
            SqlParameter[] parameters = {
					            new SqlParameter("@new_name", SqlDbType.NVarChar,50),
					            new SqlParameter("@old_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = new_name;
            parameters[1].Value = old_name;
            int rows = DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
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
        /// 修改一条记录，带事务
        /// </summary>
        public bool Update(SqlConnection conn, SqlTransaction trans, string old_name, string new_name, string title, int sort_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set");
            strSql.Append(" name=@new_name,");
            strSql.Append(" title=@title,");
            strSql.Append(" sort_id=@sort_id");
            strSql.Append(" where name=@old_name");
            SqlParameter[] parameters = {
					new SqlParameter("@new_name", SqlDbType.NVarChar,50),
                    new SqlParameter("@title", SqlDbType.NVarChar,100),
                    new SqlParameter("@sort_id", SqlDbType.Int,4),
                    new SqlParameter("@old_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = new_name;
            parameters[1].Value = title;
            parameters[2].Value = sort_id;
            parameters[3].Value = old_name;
            int rows = DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
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
        /// 修改一条记录，带事务
        /// </summary>
        public bool Update(SqlConnection conn, SqlTransaction trans, string old_name, int parent_id, string nav_name, string title, int sort_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "navigation set");
            strSql.Append(" parent_id=@parent_id,");
            strSql.Append(" name=@name,");
            strSql.Append(" title=@title,");
            strSql.Append(" sort_id=@sort_id");
            strSql.Append(" where name=@old_name");
            SqlParameter[] parameters = {
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
                    new SqlParameter("@title", SqlDbType.NVarChar,100),
                    new SqlParameter("@sort_id", SqlDbType.Int,4),
                    new SqlParameter("@old_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = parent_id;
            parameters[1].Value = nav_name;
            parameters[2].Value = title;
            parameters[3].Value = sort_id;
            parameters[4].Value = old_name;
            int rows = DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
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
        /// 快捷添加系统默认导航
        /// </summary>
        public int Add(string parent_name, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            //先根据名称查询该父ID
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql1.Append(" where name=@parent_name");
            SqlParameter[] parameters1 = {
					new SqlParameter("@parent_name", SqlDbType.NVarChar,50)};
            parameters1[0].Value = parent_name;
            object obj1 = DbHelperSQL.GetSingle(strSql1.ToString(), parameters1);
            if (obj1 == null)
            {
                return 0;
            }
            int parent_id = Convert.ToInt32(obj1);

            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "navigation(");
            strSql.Append("parent_id,channel_id,nav_type,name,title,link_url,sort_id,action_type,is_lock,is_sys)");
            strSql.Append(" values (");
            strSql.Append("@parent_id,@channel_id,@nav_type,@name,@title,@link_url,@sort_id,@action_type,@is_lock,@is_sys)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@nav_type", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@action_type", SqlDbType.NVarChar,500),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1)};
            parameters[0].Value = parent_id;
            parameters[1].Value = channel_id;
            parameters[2].Value = DTEnums.NavigationEnum.System.ToString();
            parameters[3].Value = nav_name;
            parameters[4].Value = title;
            parameters[5].Value = link_url;
            parameters[6].Value = sort_id;
            parameters[7].Value = action_type;
            parameters[8].Value = 0;
            parameters[9].Value = 1;
            object obj2 = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return Convert.ToInt32(obj2);
        }

        /// <summary>
        /// 快捷添加系统默认导航，带事务
        /// </summary>
        public int Add(SqlConnection conn, SqlTransaction trans, string parent_name, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            //先根据名称查询该父ID
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id from " + databaseprefix + "navigation");
            strSql.Append(" where name=@parent_name");
            SqlParameter[] parameters = {
					new SqlParameter("@parent_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = parent_name;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            int parent_id = Convert.ToInt32(obj);

            return Add(conn, trans, parent_id, nav_name, title, link_url, sort_id, channel_id, action_type);
        }

        /// <summary>
        /// 快捷添加系统默认导航，带事务
        /// </summary>
        public int Add(SqlConnection conn, SqlTransaction trans, int parent_id, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "navigation(");
            strSql.Append("parent_id,channel_id,nav_type,name,title,link_url,sort_id,action_type,is_lock,is_sys)");
            strSql.Append(" values (");
            strSql.Append("@parent_id,@channel_id,@nav_type,@name,@title,@link_url,@sort_id,@action_type,@is_lock,@is_sys)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
					new SqlParameter("@parent_id", SqlDbType.Int,4),
					new SqlParameter("@channel_id", SqlDbType.Int,4),
					new SqlParameter("@nav_type", SqlDbType.NVarChar,50),
					new SqlParameter("@name", SqlDbType.NVarChar,50),
					new SqlParameter("@title", SqlDbType.NVarChar,100),
					new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					new SqlParameter("@sort_id", SqlDbType.Int,4),
					new SqlParameter("@action_type", SqlDbType.NVarChar,500),
					new SqlParameter("@is_lock", SqlDbType.TinyInt,1),
					new SqlParameter("@is_sys", SqlDbType.TinyInt,1)};
            parameters[0].Value = parent_id;
            parameters[1].Value = channel_id;
            parameters[2].Value = DTEnums.NavigationEnum.System.ToString();
            parameters[3].Value = nav_name;
            parameters[4].Value = title;
            parameters[5].Value = link_url;
            parameters[6].Value = sort_id;
            parameters[7].Value = action_type;
            parameters[8].Value = 0;
            parameters[9].Value = 1;
            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            return Convert.ToInt32(obj);
        }

        /// <summary>
        /// 得到一个对象实体，带事务
        /// </summary>
        public Model.navigation GetModel(SqlConnection conn, SqlTransaction trans, string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,parent_id,channel_id,nav_type,name,title,sub_title,icon_url,link_url,sort_id,is_lock,remark,action_type,is_sys");
            strSql.Append(" from " + databaseprefix + "navigation ");
            strSql.Append(" where name=@nav_name");
            SqlParameter[] parameters = {
					new SqlParameter("@nav_name", SqlDbType.NVarChar,50)};
            parameters[0].Value = nav_name;

            Model.navigation model = new Model.navigation();
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString(), parameters);
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
        /// 删除一条数据，带事务
        /// </summary>
        public bool Delete(SqlConnection conn, SqlTransaction trans, string nav_name)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "navigation");
            strSql.Append(" where name=@name");
            SqlParameter[] parameters = {
					new SqlParameter("@name", SqlDbType.NVarChar,50)};
            parameters[0].Value = nav_name;

            int rows = DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

        #region 私有方法===============================
        /// <summary>
        /// 组合成对象实体
        /// </summary>
        private Model.navigation DataRowToModel(DataRow row)
        {
            Model.navigation model = new Model.navigation();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["parent_id"] != null && row["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(row["parent_id"].ToString());
                }
                if (row["channel_id"] != null && row["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(row["channel_id"].ToString());
                }
                if (row["nav_type"] != null)
                {
                    model.nav_type = row["nav_type"].ToString();
                }
                if (row["name"] != null)
                {
                    model.name = row["name"].ToString();
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["sub_title"] != null)
                {
                    model.sub_title = row["sub_title"].ToString();
                }
                if (row["icon_url"] != null)
                {
                    model.icon_url = row["icon_url"].ToString();
                }
                if (row["link_url"] != null)
                {
                    model.link_url = row["link_url"].ToString();
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["is_lock"] != null && row["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(row["is_lock"].ToString());
                }
                if (row["remark"] != null)
                {
                    model.remark = row["remark"].ToString();
                }
                if (row["action_type"] != null)
                {
                    model.action_type = row["action_type"].ToString();
                }
                if (row["is_sys"] != null && row["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(row["is_sys"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int class_layer)
        {
            class_layer++;
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                row["channel_id"] = int.Parse(dr[i]["channel_id"].ToString());
                row["class_layer"] = class_layer;
                row["nav_type"] = dr[i]["nav_type"].ToString();
                row["name"] = dr[i]["name"].ToString();
                row["title"] = dr[i]["title"].ToString();
                row["sub_title"] = dr[i]["sub_title"].ToString();
                row["icon_url"] = dr[i]["icon_url"].ToString();
                row["link_url"] = dr[i]["link_url"].ToString();
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                row["is_lock"] = int.Parse(dr[i]["is_lock"].ToString());
                row["remark"] = dr[i]["remark"].ToString();
                row["action_type"] = dr[i]["action_type"].ToString();
                row["is_sys"] = int.Parse(dr[i]["is_sys"].ToString());
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()), class_layer);
            }
        }

        /// <summary>
        /// 获取父类下的所有子类ID
        /// </summary>
        private void GetChildIds(DataTable dt, int parent_id, ref string ids)
        {
            DataRow[] dr = dt.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                ids += dr[i]["id"].ToString() + ",";
                //调用自身迭代
                this.GetChildIds(dt, int.Parse(dr[i]["id"].ToString()), ref ids);
            }
        }
        #endregion
    }
}

