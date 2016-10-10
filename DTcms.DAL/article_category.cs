using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using DTcms.DBUtility;
using DTcms.Common;
using System.Collections.Generic;

namespace DTcms.DAL.SqlServer
{
    /// <summary>
    /// 数据访问类:内容类别
    /// </summary>
    public partial class article_category
    {
        private string databaseprefix; //数据库表名前缀
        public article_category(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "article_category");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "article_category");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return "";
            }
            return title;
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_category model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "article_category(");
                        strSql.Append("channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description)");
                        strSql.Append(" values (");
                        strSql.Append("@channel_id,@title,@call_index,@parent_id,@class_list,@class_layer,@sort_id,@link_url,@img_url,@content,@seo_title,@seo_keywords,@seo_description)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
                                new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					            new SqlParameter("@parent_id", SqlDbType.Int,4),
					            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
					            new SqlParameter("@class_layer", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_description", SqlDbType.NVarChar,255)};
                        parameters[0].Value = model.channel_id;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.call_index;
                        parameters[3].Value = model.parent_id;
                        parameters[4].Value = model.class_list;
                        parameters[5].Value = model.class_layer;
                        parameters[6].Value = model.sort_id;
                        parameters[7].Value = model.link_url;
                        parameters[8].Value = model.img_url;
                        parameters[9].Value = model.content;
                        parameters[10].Value = model.seo_title;
                        parameters[11].Value = model.seo_keywords;
                        parameters[12].Value = model.seo_description;

                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务
                        model.id = Convert.ToInt32(obj);

                        if (model.parent_id > 0)
                        {
                            Model.article_category model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }
                        //修改节点列表和深度
                        DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set class_list='" + model.class_list + "', class_layer=" + model.class_layer + " where id=" + model.id); //带事务
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
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article_category set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_category model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        //先判断选中的父节点是否被包含
                        if (IsContainNode(model.id, model.parent_id))
                        {
                            //查找旧数据
                            Model.article_category oldModel = GetModel(model.id);
                            //查找旧父节点数据
                            string class_list = "," + model.parent_id + ",";
                            int class_layer = 1;
                            if (oldModel.parent_id > 0)
                            {
                                Model.article_category oldParentModel = GetModel(conn, trans, oldModel.parent_id); //带事务
                                class_list = oldParentModel.class_list + model.parent_id + ",";
                                class_layer = oldParentModel.class_layer + 1;
                            }
                            //先提升选中的父节点
                            DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set parent_id=" + oldModel.parent_id + ",class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + model.parent_id); //带事务
                            UpdateChilds(conn, trans, model.parent_id); //带事务
                        }
                        //更新子节点
                        if (model.parent_id > 0)
                        {
                            Model.article_category model2 = GetModel(conn, trans, model.parent_id); //带事务
                            model.class_list = model2.class_list + model.id + ",";
                            model.class_layer = model2.class_layer + 1;
                        }
                        else
                        {
                            model.class_list = "," + model.id + ",";
                            model.class_layer = 1;
                        }


                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("update " + databaseprefix + "article_category set ");
                        strSql.Append("channel_id=@channel_id,");
                        strSql.Append("title=@title,");
                        strSql.Append("call_index=@call_index,");
                        strSql.Append("parent_id=@parent_id,");
                        strSql.Append("class_list=@class_list,");
                        strSql.Append("class_layer=@class_layer,");
                        strSql.Append("sort_id=@sort_id,");
                        strSql.Append("link_url=@link_url,");
                        strSql.Append("img_url=@img_url,");
                        strSql.Append("content=@content,");
                        strSql.Append("seo_title=@seo_title,");
                        strSql.Append("seo_keywords=@seo_keywords,");
                        strSql.Append("seo_description=@seo_description");
                        strSql.Append(" where id=@id");
                        SqlParameter[] parameters = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
                                new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					            new SqlParameter("@parent_id", SqlDbType.Int,4),
					            new SqlParameter("@class_list", SqlDbType.NVarChar,500),
					            new SqlParameter("@class_layer", SqlDbType.Int,4),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
					            new SqlParameter("@id", SqlDbType.Int,4)};
                        parameters[0].Value = model.channel_id;
                        parameters[1].Value = model.title;
                        parameters[2].Value = model.call_index;
                        parameters[3].Value = model.parent_id;
                        parameters[4].Value = model.class_list;
                        parameters[5].Value = model.class_layer;
                        parameters[6].Value = model.sort_id;
                        parameters[7].Value = model.link_url;
                        parameters[8].Value = model.img_url;
                        parameters[9].Value = model.content;
                        parameters[10].Value = model.seo_title;
                        parameters[11].Value = model.seo_keywords;
                        parameters[12].Value = model.seo_description;
                        parameters[13].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);

                        //更新子节点
                        UpdateChilds(conn, trans, model.id);
                        trans.Commit();
                    }
                    catch (Exception ex)
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
        public void Delete(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article_category ");
            strSql.Append(" where class_list like '%," + id + ",%' ");
            DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_category GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
            strSql.Append(" from " + databaseprefix + "article_category ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article_category model = new Model.article_category();
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
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        public Model.article_category GetModel(SqlConnection conn, SqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
            strSql.Append(" from " + databaseprefix + "article_category ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article_category model = new Model.article_category();
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
        /// 取得指定类别下的列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetChildList(int parent_id, int channel_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
            strSql.Append(" from " + databaseprefix + "article_category");
            strSql.Append(" where channel_id=" + channel_id + " and parent_id=" + parent_id + " order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds.Tables[0];
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="channel_id">频道ID</param>
        /// <returns></returns>
        public DataTable GetList(int parent_id, int channel_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
            strSql.Append(" from " + databaseprefix + "article_category");
            strSql.Append(" where channel_id=" + channel_id + " order by sort_id asc,id desc");
            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            DataTable oldData = ds.Tables[0] as DataTable;
            if (oldData == null)
            {
                return null;
            }
            //复制结构
            DataTable newData = oldData.Clone();
            //调用迭代组合成DAGATABLE
            GetChilds(oldData, newData, parent_id, channel_id);
            return newData;
        }
        #endregion

        #region 扩展方法================================
        public int GetParentId(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 parent_id from " + databaseprefix + "article_category");
            strSql.Append(" where id=" + id);
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        public Model.article_category DataRowToModel(DataRow row)
        {
            Model.article_category model = new Model.article_category();
            if (row != null)
            {
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["channel_id"] != null && row["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(row["channel_id"].ToString());
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["call_index"] != null)
                {
                    model.call_index = row["call_index"].ToString();
                }
                if (row["parent_id"] != null && row["parent_id"].ToString() != "")
                {
                    model.parent_id = int.Parse(row["parent_id"].ToString());
                }
                if (row["class_list"] != null)
                {
                    model.class_list = row["class_list"].ToString();
                }
                if (row["class_layer"] != null && row["class_layer"].ToString() != "")
                {
                    model.class_layer = int.Parse(row["class_layer"].ToString());
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["link_url"] != null)
                {
                    model.link_url = row["link_url"].ToString();
                }
                if (row["img_url"] != null)
                {
                    model.img_url = row["img_url"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["seo_title"] != null)
                {
                    model.seo_title = row["seo_title"].ToString();
                }
                if (row["seo_keywords"] != null)
                {
                    model.seo_keywords = row["seo_keywords"].ToString();
                }
                if (row["seo_description"] != null)
                {
                    model.seo_description = row["seo_description"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 从内存中取得所有下级类别列表（自身迭代）
        /// </summary>
        private void GetChilds(DataTable oldData, DataTable newData, int parent_id, int channel_id)
        {
            DataRow[] dr = oldData.Select("parent_id=" + parent_id);
            for (int i = 0; i < dr.Length; i++)
            {
                //添加一行数据
                DataRow row = newData.NewRow();
                row["id"] = int.Parse(dr[i]["id"].ToString());
                row["channel_id"] = int.Parse(dr[i]["channel_id"].ToString());
                row["title"] = dr[i]["title"].ToString();
                row["call_index"] = dr[i]["call_index"].ToString();
                row["parent_id"] = int.Parse(dr[i]["parent_id"].ToString());
                row["class_list"] = dr[i]["class_list"].ToString();
                row["class_layer"] = int.Parse(dr[i]["class_layer"].ToString());
                row["sort_id"] = int.Parse(dr[i]["sort_id"].ToString());
                row["link_url"] = dr[i]["link_url"].ToString();
                row["img_url"] = dr[i]["img_url"].ToString();
                row["content"] = dr[i]["content"].ToString();
                row["seo_title"] = dr[i]["seo_title"].ToString();
                row["seo_keywords"] = dr[i]["seo_keywords"].ToString();
                row["seo_description"] = dr[i]["seo_description"].ToString();
                newData.Rows.Add(row);
                //调用自身迭代
                this.GetChilds(oldData, newData, int.Parse(dr[i]["id"].ToString()), channel_id);
            }
        }

        /// <summary>
        /// 修改子节点的ID列表及深度（自身迭代）
        /// </summary>
        /// <param name="parent_id"></param>
        private void UpdateChilds(SqlConnection conn, SqlTransaction trans, int parent_id)
        {
            //查找父节点信息
            Model.article_category model = GetModel(conn, trans, parent_id);
            if (model != null)
            {
                //查找子节点
                string strSql = "select id from " + databaseprefix + "article_category where parent_id=" + parent_id;
                DataSet ds = DbHelperSQL.Query(conn, trans, strSql); //带事务
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //修改子节点的ID列表及深度
                    int id = int.Parse(dr["id"].ToString());
                    string class_list = model.class_list + id + ",";
                    int class_layer = model.class_layer + 1;
                    DbHelperSQL.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + id); //带事务

                    //调用自身迭代
                    this.UpdateChilds(conn, trans, id); //带事务
                }
            }
        }

        /// <summary>
        /// 验证节点是否被包含
        /// </summary>
        /// <param name="id">待查询的节点</param>
        /// <param name="parent_id">父节点</param>
        /// <returns></returns>
        private bool IsContainNode(int id, int parent_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_category ");
            strSql.Append(" where class_list like '%," + id + ",%' and id=" + parent_id);
            return DbHelperSQL.Exists(strSql.ToString());
        }

        #endregion
    }
}