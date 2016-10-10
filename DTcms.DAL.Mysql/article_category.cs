using System;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using DTcms.DBUtility;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.DAL.Mysql
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
        /// 得到最大ID dt_article
        /// </summary>
        private int GetMaxId(MySqlConnection conn, MySqlTransaction trans)
        {
            string strSql = "select id from " + databaseprefix + "article_category order by id desc limit 1";
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
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article_category");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;
            return DbHelperMySql.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select title from " + databaseprefix + "article_category");
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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_category model)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "article_category(");
                        strSql.Append("channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description)");
                        strSql.Append(" values (");
                        strSql.Append("@channel_id,@title,@call_index,@parent_id,@class_list,@class_layer,@sort_id,@link_url,@img_url,@content,@seo_title,@seo_keywords,@seo_description)");
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@channel_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
                                new MySqlParameter("@call_index", MySqlDbType.VarChar,50),
					            new MySqlParameter("@parent_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@class_list", MySqlDbType.VarChar,500),
					            new MySqlParameter("@class_layer", MySqlDbType.Int32,4),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@link_url", MySqlDbType.VarChar,255),
					            new MySqlParameter("@img_url", MySqlDbType.VarChar,255),
					            new MySqlParameter("@content", MySqlDbType.VarChar),
					            new MySqlParameter("@seo_title", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_keywords", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_description", MySqlDbType.VarChar,255)};
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

                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        //取得新插入的ID
                        model.id = GetMaxId(conn, trans);

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
                        DbHelperMySql.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set class_list='" + model.class_list + "', class_layer=" + model.class_layer + " where id=" + model.id); //带事务
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
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article_category model)
        {
            using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
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
                            DbHelperMySql.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set parent_id=" + oldModel.parent_id + ",class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + model.parent_id); //带事务
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
                        MySqlParameter[] parameters = {
					            new MySqlParameter("@channel_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@title", MySqlDbType.VarChar,100),
                                new MySqlParameter("@call_index", MySqlDbType.VarChar,50),
					            new MySqlParameter("@parent_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@class_list", MySqlDbType.VarChar,500),
					            new MySqlParameter("@class_layer", MySqlDbType.Int32,4),
					            new MySqlParameter("@sort_id", MySqlDbType.Int32,4),
					            new MySqlParameter("@link_url", MySqlDbType.VarChar,255),
					            new MySqlParameter("@img_url", MySqlDbType.VarChar,255),
					            new MySqlParameter("@content", MySqlDbType.VarChar),
					            new MySqlParameter("@seo_title", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_keywords", MySqlDbType.VarChar,255),
					            new MySqlParameter("@seo_description", MySqlDbType.VarChar,255),
					            new MySqlParameter("@id", MySqlDbType.Int32,4)};
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
                        DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);

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
            DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_category GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
            strSql.Append(" from " + databaseprefix + "article_category ");
            strSql.Append(" where id=@id");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.article_category model = new Model.article_category();
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
        /// 得到一个对象实体(重载，带事务)
        /// </summary>
        public Model.article_category GetModel(MySqlConnection conn, MySqlTransaction trans, int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
            strSql.Append(" from " + databaseprefix + "article_category ");
            strSql.Append(" where id=@id");
            strSql.Append(" limit 1");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.article_category model = new Model.article_category();
            DataSet ds = DbHelperMySql.Query(conn, trans, strSql.ToString(), parameters);
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
        public DataTable GetChildList(int parent_id, int channel_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,title,call_index,parent_id,class_list,class_layer,sort_id,link_url,img_url,content,seo_title,seo_keywords,seo_description");
            strSql.Append(" from " + databaseprefix + "article_category");
            strSql.Append(" where channel_id=" + channel_id + " and parent_id=" + parent_id + " order by sort_id asc,id desc");
            DataSet ds = DbHelperMySql.Query(strSql.ToString());
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
            DataSet ds = DbHelperMySql.Query(strSql.ToString());
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
            strSql.Append("select parent_id from " + databaseprefix + "article_category");
            strSql.Append(" where id=" + id);
            strSql.Append(" limit 1");
            return Convert.ToInt32(DbHelperMySql.GetSingle(strSql.ToString()));
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
        private void UpdateChilds(MySqlConnection conn, MySqlTransaction trans, int parent_id)
        {
            //查找父节点信息
            Model.article_category model = GetModel(conn, trans, parent_id);
            if (model != null)
            {
                //查找子节点
                string strSql = "select id from " + databaseprefix + "article_category where parent_id=" + parent_id;
                DataSet ds = DbHelperMySql.Query(conn, trans, strSql); //带事务
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    //修改子节点的ID列表及深度
                    int id = int.Parse(dr["id"].ToString());
                    string class_list = model.class_list + id + ",";
                    int class_layer = model.class_layer + 1;
                    DbHelperMySql.ExecuteSql(conn, trans, "update " + databaseprefix + "article_category set class_list='" + class_list + "', class_layer=" + class_layer + " where id=" + id); //带事务

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
            return DbHelperMySql.Exists(strSql.ToString());
        }

        #endregion
    }
}