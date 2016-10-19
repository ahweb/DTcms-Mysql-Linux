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
	/// 数据访问类:文章内容
	/// </summary>
	public partial class article
	{
        private string databaseprefix; //数据库表名前缀
        public article(string _databaseprefix)
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
            strSql.Append("select count(1) from " + databaseprefix + "article");
            strSql.Append(" where id=@id ");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string call_index)
        {
            if (string.IsNullOrEmpty(call_index))
            {
                return false;
            }
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article");
            strSql.Append(" where call_index=@call_index ");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
        public int Add(Model.article model)
        {
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 添加主表数据====================
                        StringBuilder strSql = new StringBuilder();
                        strSql.Append("insert into " + databaseprefix + "article(");
                        strSql.Append("channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,is_msg,is_top,is_red,is_hot,is_slide,is_sys,user_name,add_time,update_time)");
                        strSql.Append(" values (");
                        strSql.Append("@channel_id,@category_id,@call_index,@title,@link_url,@img_url,@seo_title,@seo_keywords,@seo_description,@zhaiyao,@content,@sort_id,@click,@status,@is_msg,@is_top,@is_red,@is_hot,@is_slide,@is_sys,@user_name,@add_time,@update_time)");
                        strSql.Append(";select @@IDENTITY");
                        SqlParameter[] parameters = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4),
					            new SqlParameter("@category_id", SqlDbType.Int,4),
					            new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
					            new SqlParameter("@zhaiyao", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@click", SqlDbType.Int,4),
					            new SqlParameter("@status", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_top", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_red", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_hot", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_slide", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
					            new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
					            new SqlParameter("@update_time", SqlDbType.DateTime)};
                        parameters[0].Value = model.channel_id;
                        parameters[1].Value = model.category_id;
                        parameters[2].Value = model.call_index;
                        parameters[3].Value = model.title;
                        parameters[4].Value = model.link_url;
                        parameters[5].Value = model.img_url;
                        parameters[6].Value = model.seo_title;
                        parameters[7].Value = model.seo_keywords;
                        parameters[8].Value = model.seo_description;
                        parameters[9].Value = model.zhaiyao;
                        parameters[10].Value = model.content;
                        parameters[11].Value = model.sort_id;
                        parameters[12].Value = model.click;
                        parameters[13].Value = model.status;
                        parameters[14].Value = model.is_msg;
                        parameters[15].Value = model.is_top;
                        parameters[16].Value = model.is_red;
                        parameters[17].Value = model.is_hot;
                        parameters[18].Value = model.is_slide;
                        parameters[19].Value = model.is_sys;
                        parameters[20].Value = model.user_name;
                        parameters[21].Value = model.add_time;
                        parameters[22].Value = model.update_time;
                        //添加主表数据
                        object obj = DbHelperSQL.GetSingle(conn, trans, strSql.ToString(), parameters); //带事务
                        model.id = Convert.ToInt32(obj);
                        #endregion

                        #region 添加扩展字段====================
                        StringBuilder strSql2 = new StringBuilder();
                        StringBuilder strFieldName = new StringBuilder(); //字段列表
                        StringBuilder strFieldVar = new StringBuilder(); //字段声明
                        SqlParameter[] parameters2 = new SqlParameter[model.fields.Count + 1];
                        int i = 1;
                        strFieldName.Append("article_id");
                        strFieldVar.Append("@article_id");
                        parameters2[0] = new SqlParameter("@article_id", SqlDbType.Int, 4);
                        parameters2[0].Value = model.id;
                        foreach (KeyValuePair<string, string> kvp in model.fields)
                        {
                            strFieldName.Append("," + kvp.Key);
                            strFieldVar.Append(",@" + kvp.Key);
                            if (kvp.Value.Length <= 4000)
                            {
                                parameters2[i] = new SqlParameter("@" + kvp.Key, SqlDbType.NVarChar, kvp.Value.Length);
                            }
                            else
                            {
                                parameters2[i] = new SqlParameter("@" + kvp.Key, SqlDbType.NText);
                            }

                            parameters2[i].Value = kvp.Value;
                            i++;
                        }
                        strSql2.Append("insert into " + databaseprefix + "article_attribute_value(");
                        strSql2.Append(strFieldName.ToString() + ")");
                        strSql2.Append(" values (");
                        strSql2.Append(strFieldVar.ToString() + ")");
                        DbHelperSQL.GetSingle(conn, trans, strSql2.ToString(), parameters2); //带事务
                        #endregion

                        #region 添加图片相册====================
                        if (model.albums != null)
                        {
                            StringBuilder strSql3;
                            foreach (Model.article_albums modelt in model.albums)
                            {
                                strSql3 = new StringBuilder();
                                strSql3.Append("insert into " + databaseprefix + "article_albums(");
                                strSql3.Append("article_id,thumb_path,original_path,remark)");
                                strSql3.Append(" values (");
                                strSql3.Append("@article_id,@thumb_path,@original_path,@remark)");
                                SqlParameter[] parameters3 = {
					                new SqlParameter("@article_id", SqlDbType.Int,4),
					                new SqlParameter("@thumb_path", SqlDbType.NVarChar,255),
					                new SqlParameter("@original_path", SqlDbType.NVarChar,255),
					                new SqlParameter("@remark", SqlDbType.NVarChar,500)};
                                parameters3[0].Value = model.id;
                                parameters3[1].Value = modelt.thumb_path;
                                parameters3[2].Value = modelt.original_path;
                                parameters3[3].Value = modelt.remark;
                                DbHelperSQL.GetSingle(conn, trans, strSql3.ToString(), parameters3); //带事务
                            }
                        }
                        #endregion

                        #region 添加内容附件====================
                        if (model.attach != null)
                        {
                            StringBuilder strSql4;
                            foreach (Model.article_attach modelt in model.attach)
                            {
                                strSql4 = new StringBuilder();
                                strSql4.Append("insert into " + databaseprefix + "article_attach(");
                                strSql4.Append("article_id,file_name,file_path,file_size,file_ext,down_num,point)");
                                strSql4.Append(" values (");
                                strSql4.Append("@article_id,@file_name,@file_path,@file_size,@file_ext,@down_num,@point)");
                                SqlParameter[] parameters4 = {
					                    new SqlParameter("@article_id", SqlDbType.Int,4),
					                    new SqlParameter("@file_name", SqlDbType.NVarChar,100),
					                    new SqlParameter("@file_path", SqlDbType.NVarChar,255),
					                    new SqlParameter("@file_size", SqlDbType.Int,4),
					                    new SqlParameter("@file_ext", SqlDbType.NVarChar,20),
					                    new SqlParameter("@down_num", SqlDbType.Int,4),
					                    new SqlParameter("@point", SqlDbType.Int,4)};
                                parameters4[0].Value = model.id;
                                parameters4[1].Value = modelt.file_name;
                                parameters4[2].Value = modelt.file_path;
                                parameters4[3].Value = modelt.file_size;
                                parameters4[4].Value = modelt.file_ext;
                                parameters4[5].Value = modelt.down_num;
                                parameters4[6].Value = modelt.point;
                                DbHelperSQL.GetSingle(conn, trans, strSql4.ToString(), parameters4); //带事务
                            }
                        }
                        #endregion

                        #region 用户组价格====================
                        if (model.group_price != null)
                        {
                            StringBuilder strSql5;
                            foreach (Model.user_group_price modelt in model.group_price)
                            {
                                strSql5 = new StringBuilder();
                                strSql5.Append("insert into " + databaseprefix + "user_group_price(");
                                strSql5.Append("article_id,group_id,price)");
                                strSql5.Append(" values (");
                                strSql5.Append("@article_id,@group_id,@price)");
                                SqlParameter[] parameters5 = {
						                new SqlParameter("@article_id", SqlDbType.Int,4),
					                    new SqlParameter("@group_id", SqlDbType.Int,4),
					                    new SqlParameter("@price", SqlDbType.Decimal,5)};
                                parameters5[0].Value = model.id;
                                parameters5[1].Value = modelt.group_id;
                                parameters5[2].Value = modelt.price;
                                DbHelperSQL.GetSingle(conn, trans, strSql5.ToString(), parameters5); //带事务
                            }
                        }
                        #endregion

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
		public bool Update(Model.article model)
		{
            using (SqlConnection conn = new SqlConnection(DbHelperSQL.connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    try
                    {
                        #region 修改主表数据==========================
                        StringBuilder strSql=new StringBuilder();
			            strSql.Append("update " + databaseprefix + "article set ");
			            strSql.Append("channel_id=@channel_id,");
			            strSql.Append("category_id=@category_id,");
			            strSql.Append("call_index=@call_index,");
			            strSql.Append("title=@title,");
			            strSql.Append("link_url=@link_url,");
			            strSql.Append("img_url=@img_url,");
			            strSql.Append("seo_title=@seo_title,");
			            strSql.Append("seo_keywords=@seo_keywords,");
			            strSql.Append("seo_description=@seo_description,");
			            strSql.Append("zhaiyao=@zhaiyao,");
			            strSql.Append("content=@content,");
			            strSql.Append("sort_id=@sort_id,");
			            strSql.Append("click=@click,");
			            strSql.Append("status=@status,");
			            strSql.Append("is_msg=@is_msg,");
			            strSql.Append("is_top=@is_top,");
			            strSql.Append("is_red=@is_red,");
			            strSql.Append("is_hot=@is_hot,");
			            strSql.Append("is_slide=@is_slide,");
			            strSql.Append("is_sys=@is_sys,");
			            strSql.Append("user_name=@user_name,");
			            strSql.Append("add_time=@add_time,");
			            strSql.Append("update_time=@update_time");
			            strSql.Append(" where id=@id");
			            SqlParameter[] parameters = {
					            new SqlParameter("@channel_id", SqlDbType.Int,4),
					            new SqlParameter("@category_id", SqlDbType.Int,4),
					            new SqlParameter("@call_index", SqlDbType.NVarChar,50),
					            new SqlParameter("@title", SqlDbType.NVarChar,100),
					            new SqlParameter("@link_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@img_url", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_title", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_keywords", SqlDbType.NVarChar,255),
					            new SqlParameter("@seo_description", SqlDbType.NVarChar,255),
					            new SqlParameter("@zhaiyao", SqlDbType.NVarChar,255),
					            new SqlParameter("@content", SqlDbType.NText),
					            new SqlParameter("@sort_id", SqlDbType.Int,4),
					            new SqlParameter("@click", SqlDbType.Int,4),
					            new SqlParameter("@status", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_msg", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_top", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_red", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_hot", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_slide", SqlDbType.TinyInt,1),
					            new SqlParameter("@is_sys", SqlDbType.TinyInt,1),
					            new SqlParameter("@user_name", SqlDbType.NVarChar,100),
					            new SqlParameter("@add_time", SqlDbType.DateTime),
					            new SqlParameter("@update_time", SqlDbType.DateTime),
					            new SqlParameter("@id", SqlDbType.Int,4)};
			            parameters[0].Value = model.channel_id;
			            parameters[1].Value = model.category_id;
			            parameters[2].Value = model.call_index;
			            parameters[3].Value = model.title;
			            parameters[4].Value = model.link_url;
			            parameters[5].Value = model.img_url;
			            parameters[6].Value = model.seo_title;
			            parameters[7].Value = model.seo_keywords;
			            parameters[8].Value = model.seo_description;
			            parameters[9].Value = model.zhaiyao;
			            parameters[10].Value = model.content;
			            parameters[11].Value = model.sort_id;
			            parameters[12].Value = model.click;
			            parameters[13].Value = model.status;
			            parameters[14].Value = model.is_msg;
			            parameters[15].Value = model.is_top;
			            parameters[16].Value = model.is_red;
			            parameters[17].Value = model.is_hot;
			            parameters[18].Value = model.is_slide;
			            parameters[19].Value = model.is_sys;
			            parameters[20].Value = model.user_name;
			            parameters[21].Value = model.add_time;
			            parameters[22].Value = model.update_time;
			            parameters[23].Value = model.id;
                        DbHelperSQL.ExecuteSql(conn, trans, strSql.ToString(), parameters);
                        #endregion

                        #region 修改扩展字段==========================
                        if (model.fields.Count > 0)
                        {
                            StringBuilder strSql2 = new StringBuilder();
                            StringBuilder strFieldName = new StringBuilder(); //字段列表
                            SqlParameter[] parameters2 = new SqlParameter[model.fields.Count + 1];
                            int i = 0;
                            foreach (KeyValuePair<string, string> kvp in model.fields)
                            {
                                strFieldName.Append(kvp.Key + "=@" + kvp.Key + ",");
                                if (kvp.Value.Length <= 4000)
                                {
                                    parameters2[i] = new SqlParameter("@" + kvp.Key, SqlDbType.NVarChar, kvp.Value.Length);
                                }
                                else
                                {
                                    parameters2[i] = new SqlParameter("@" + kvp.Key, SqlDbType.NText);
                                }
                                parameters2[i].Value = kvp.Value;
                                i++;
                            }
                            strSql2.Append("update " + databaseprefix + "article_attribute_value set ");
                            strSql2.Append(Utils.DelLastComma(strFieldName.ToString()));
                            strSql2.Append(" where article_id=@article_id");
                            parameters2[i] = new SqlParameter("@article_id", SqlDbType.Int, 4);
                            parameters2[i].Value = model.id;
                            DbHelperSQL.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
                        }
                        #endregion

                        #region 修改图片相册==========================
                        //删除已删除的图片
                        new article_albums(databaseprefix).DeleteList(conn, trans, model.albums, model.id);
                        //添加/修改相册
                        if (model.albums != null)
                        {
                            StringBuilder strSql3;
                            foreach (Model.article_albums modelt in model.albums)
                            {
                                strSql3 = new StringBuilder();
                                if (modelt.id > 0)
                                {
                                    strSql3.Append("update " + databaseprefix + "article_albums set ");
                                    strSql3.Append("article_id=@article_id,");
                                    strSql3.Append("thumb_path=@thumb_path,");
                                    strSql3.Append("original_path=@original_path,");
                                    strSql3.Append("remark=@remark");
                                    strSql3.Append(" where id=@id");
                                    SqlParameter[] parameters3 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@thumb_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@original_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@remark", SqlDbType.NVarChar,500),
                                            new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters3[0].Value = modelt.article_id;
                                    parameters3[1].Value = modelt.thumb_path;
                                    parameters3[2].Value = modelt.original_path;
                                    parameters3[3].Value = modelt.remark;
                                    parameters3[4].Value = modelt.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);
                                }
                                else
                                {
                                    strSql3.Append("insert into " + databaseprefix + "article_albums(");
                                    strSql3.Append("article_id,thumb_path,original_path,remark)");
                                    strSql3.Append(" values (");
                                    strSql3.Append("@article_id,@thumb_path,@original_path,@remark)");
                                    SqlParameter[] parameters3 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@thumb_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@original_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@remark", SqlDbType.NVarChar,500)};
                                    parameters3[0].Value = modelt.article_id;
                                    parameters3[1].Value = modelt.thumb_path;
                                    parameters3[2].Value = modelt.original_path;
                                    parameters3[3].Value = modelt.remark;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql3.ToString(), parameters3);
                                }
                            }
                        }
                        #endregion

                        #region 修改内容附件==========================
                        //删除已删除的附件
                        new article_attach(databaseprefix).DeleteList(conn, trans, model.attach, model.id);
                        // 添加/修改附件
                        if (model.attach != null)
                        {
                            StringBuilder strSql4;
                            foreach (Model.article_attach modelt in model.attach)
                            {
                                strSql4 = new StringBuilder();
                                if (modelt.id > 0)
                                {
                                    strSql4.Append("update " + databaseprefix + "article_attach set ");
                                    strSql4.Append("article_id=@article_id,");
                                    strSql4.Append("file_name=@file_name,");
                                    strSql4.Append("file_path=@file_path,");
                                    strSql4.Append("file_size=@file_size,");
                                    strSql4.Append("file_ext=@file_ext,");
                                    strSql4.Append("point=@point");
                                    strSql4.Append(" where id=@id");
                                    SqlParameter[] parameters4 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@file_name", SqlDbType.NVarChar,100),
					                        new SqlParameter("@file_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@file_size", SqlDbType.Int,4),
					                        new SqlParameter("@file_ext", SqlDbType.NVarChar,20),
					                        new SqlParameter("@point", SqlDbType.Int,4),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters4[0].Value = modelt.article_id;
                                    parameters4[1].Value = modelt.file_name;
                                    parameters4[2].Value = modelt.file_path;
                                    parameters4[3].Value = modelt.file_size;
                                    parameters4[4].Value = modelt.file_ext;
                                    parameters4[5].Value = modelt.point;
                                    parameters4[6].Value = modelt.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql4.ToString(), parameters4);
                                }
                                else
                                {
                                    strSql4.Append("insert into " + databaseprefix + "article_attach(");
                                    strSql4.Append("article_id,file_name,file_path,file_size,file_ext,down_num,point)");
                                    strSql4.Append(" values (");
                                    strSql4.Append("@article_id,@file_name,@file_path,@file_size,@file_ext,@down_num,@point)");
                                    SqlParameter[] parameters4 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@file_name", SqlDbType.NVarChar,100),
					                        new SqlParameter("@file_path", SqlDbType.NVarChar,255),
					                        new SqlParameter("@file_size", SqlDbType.Int,4),
					                        new SqlParameter("@file_ext", SqlDbType.NVarChar,20),
					                        new SqlParameter("@down_num", SqlDbType.Int,4),
					                        new SqlParameter("@point", SqlDbType.Int,4)};
                                    parameters4[0].Value = modelt.article_id;
                                    parameters4[1].Value = modelt.file_name;
                                    parameters4[2].Value = modelt.file_path;
                                    parameters4[3].Value = modelt.file_size;
                                    parameters4[4].Value = modelt.file_ext;
                                    parameters4[5].Value = modelt.down_num;
                                    parameters4[6].Value = modelt.point;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql4.ToString(), parameters4);
                                }
                            }
                        }
                        #endregion

                        #region 修改会员组价格========================
                        if (model.group_price != null)
                        {
                            StringBuilder strSql5;
                            foreach (Model.user_group_price modelt in model.group_price)
                            {
                                strSql5 = new StringBuilder();
                                if (modelt.id > 0)
                                {
                                    strSql5.Append("update " + databaseprefix + "user_group_price set ");
                                    strSql5.Append("article_id=@article_id,");
                                    strSql5.Append("group_id=@group_id,");
                                    strSql5.Append("price=@price");
                                    strSql5.Append(" where id=@id");
                                    SqlParameter[] parameters5 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@group_id", SqlDbType.Int,4),
					                        new SqlParameter("@price", SqlDbType.Decimal,5),
					                        new SqlParameter("@id", SqlDbType.Int,4)};
                                    parameters5[0].Value = modelt.article_id;
                                    parameters5[1].Value = modelt.group_id;
                                    parameters5[2].Value = modelt.price;
                                    parameters5[3].Value = modelt.id;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql5.ToString(), parameters5);
                                }
                                else
                                {
                                    strSql5.Append("insert into " + databaseprefix + "user_group_price(");
                                    strSql5.Append("article_id,group_id,price)");
                                    strSql5.Append(" values (");
                                    strSql5.Append("@article_id,@group_id,@price)");
                                    SqlParameter[] parameters5 = {
					                        new SqlParameter("@article_id", SqlDbType.Int,4),
					                        new SqlParameter("@group_id", SqlDbType.Int,4),
					                        new SqlParameter("@price", SqlDbType.Decimal,5)};
                                    parameters5[0].Value = modelt.article_id;
                                    parameters5[1].Value = modelt.group_id;
                                    parameters5[2].Value = modelt.price;
                                    DbHelperSQL.ExecuteSql(conn, trans, strSql5.ToString(), parameters5);
                                }
                            }
                        }
                        #endregion

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
            //取得相册MODEL
            List<Model.article_albums> albumsList = new DAL.SqlServer.article_albums(databaseprefix).GetList(id);
            //取得附件MODEL
            List<Model.article_attach> attachList = new DAL.SqlServer.article_attach(databaseprefix).GetList(id);

            //删除扩展字段表
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "article_attribute_value ");
            strSql1.Append(" where article_id=@article_id ");
            SqlParameter[] parameters1 = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters1[0].Value = id;
            List<CommandInfo> sqllist = new List<CommandInfo>();
            CommandInfo cmd = new CommandInfo(strSql1.ToString(), parameters1);
            sqllist.Add(cmd);

            //删除图片相册
            StringBuilder strSql2 = new StringBuilder();
            strSql2.Append("delete from " + databaseprefix + "article_albums ");
            strSql2.Append(" where article_id=@article_id ");
            SqlParameter[] parameters2 = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters2[0].Value = id;
            cmd = new CommandInfo(strSql2.ToString(), parameters2);
            sqllist.Add(cmd);

            //删除附件
            StringBuilder strSql3 = new StringBuilder();
            strSql3.Append("delete from " + databaseprefix + "article_attach ");
            strSql3.Append(" where article_id=@article_id ");
            SqlParameter[] parameters3 = {
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters3[0].Value = id;
            cmd = new CommandInfo(strSql3.ToString(), parameters3);
            sqllist.Add(cmd);

            //删除用户组价格
            StringBuilder strSql4 = new StringBuilder();
            strSql4.Append("delete from " + databaseprefix + "user_group_price ");
            strSql4.Append(" where article_id=@article_id ");
            SqlParameter[] parameters4 = {
                    new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters4[0].Value = id;
            cmd = new CommandInfo(strSql4.ToString(), parameters4);
            sqllist.Add(cmd);

            //删除评论
            StringBuilder strSql8 = new StringBuilder();
            strSql8.Append("delete from " + databaseprefix + "article_comment ");
            strSql8.Append(" where article_id=@article_id ");
            SqlParameter[] parameters8 = {
					new SqlParameter("@article_id", SqlDbType.Int,4)};
            parameters8[0].Value = id;
            cmd = new CommandInfo(strSql8.ToString(), parameters8);
            sqllist.Add(cmd);

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "article ");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;
            cmd = new  CommandInfo(strSql.ToString(), parameters);
           
            sqllist.Add(cmd);

            int rowsAffected = DbHelperSQL.ExecuteSqlTran(sqllist);
            if (rowsAffected > 0)
            {
                new DAL.SqlServer.article_albums(databaseprefix).DeleteFile(albumsList); //删除图片
                new DAL.SqlServer.article_attach(databaseprefix).DeleteFile(attachList); //删除附件
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
        public Model.article GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 id,channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,is_msg,is_top,is_red,is_hot,is_slide,is_sys,user_name,add_time,update_time");
            strSql.Append(" from " + databaseprefix + "article");
            strSql.Append(" where id=@id");
            SqlParameter[] parameters = {
					new SqlParameter("@id", SqlDbType.Int,4)};
            parameters[0].Value = id;

            Model.article model = new Model.article();
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
        public Model.article GetModel(string call_index)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id from " + databaseprefix + "article");
            strSql.Append(" where call_index=@call_index");
            SqlParameter[] parameters = {
					new SqlParameter("@call_index", SqlDbType.NVarChar,50)};
            parameters[0].Value = call_index;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj != null)
            {
                return GetModel(Convert.ToInt32(obj));
            }
            return null;
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
            strSql.Append(" id,channel_id,category_id,call_index,title,link_url,img_url,seo_title,seo_keywords,seo_description,zhaiyao,content,sort_id,click,status,is_msg,is_top,is_red,is_hot,is_slide,is_sys,user_name,add_time,update_time ");
            strSql.Append(" FROM " + databaseprefix + "article ");
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
        public DataSet GetList(int channel_id, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM " + databaseprefix + "article");
            if (channel_id > 0)
            {
                strSql.Append(" where channel_id=" + channel_id);
            }
            if (category_id > 0)
            {
                if (channel_id > 0)
                {
                    strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
                }
                else
                {
                    strSql.Append(" where category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
                }
            }
            if (strWhere.Trim() != "")
            {
                if (channel_id > 0 || category_id > 0)
                {
                    strSql.Append(" and " + strWhere);
                }
                else
                {
                    strSql.Append(" where " + strWhere);
                }
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }
		#endregion

        #region 扩展方法================================
        /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string title)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article");
            strSql.Append(" where title=@title ");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.VarChar,200)};
            parameters[0].Value = title;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string title, int category_id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "article");
            strSql.Append(" where title=@title and category_id=@category_id");
            SqlParameter[] parameters = {
					new SqlParameter("@title", SqlDbType.VarChar,200),
                    new SqlParameter("@category_id", SqlDbType.Int,4)  }
                                        ;
            parameters[0].Value = title;
            parameters[1].Value = category_id;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 返回信息标题
        /// </summary>
        public string GetTitle(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 title from " + databaseprefix + "article");
            strSql.Append(" where id=" + id);
            string title = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(title))
            {
                return string.Empty;
            }
            return title;
        }

        /// <summary>
        /// 返回信息内容
        /// </summary>
        public string GetContent(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 content from " + databaseprefix + "article");
            strSql.Append(" where id=" + id);
            string content = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(content))
            {
                return "";
            }
            return content;
        }

        /// <summary>
        /// 获取阅读次数
        /// </summary>
        public int GetClick(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 click from " + databaseprefix + "article");
            strSql.Append(" where id=" + id);
            string str = Convert.ToString(DbHelperSQL.GetSingle(strSql.ToString()));
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }
            return Convert.ToInt32(str);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(*) as H ");
            strSql.Append(" from " + databaseprefix + "article");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 返回商品库存数量
        /// </summary>
        public int GetStockQuantity(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 stock_quantity ");
            strSql.Append(" from " + databaseprefix + "article_attribute_value");
            strSql.Append(" where article_id=" + id);
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "article set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperSQL.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 获得会员组价格
        /// </summary>
        private List<Model.user_group_price> GetGroupPrice(int article_id)
        {
            List<Model.user_group_price> ls = new List<Model.user_group_price>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,group_id,price from " + databaseprefix + "user_group_price ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.user_group_price model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.user_group_price();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["group_id"] != null && dt.Rows[n]["group_id"].ToString() != "")
                    {
                        model.group_id = int.Parse(dt.Rows[n]["group_id"].ToString());
                    }
                    if (dt.Rows[n]["price"] != null && dt.Rows[n]["price"].ToString() != "")
                    {
                        model.price = decimal.Parse(dt.Rows[n]["price"].ToString());
                    }
                    ls.Add(model);
                }
            }
            return ls;
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 将对象转换为实体
        /// </summary>
        private Model.article DataRowToModel(DataRow row)
        {
            Model.article model = new Model.article();
            if (row != null)
            {
                #region 主表信息======================
                if (row["id"] != null && row["id"].ToString() != "")
                {
                    model.id = int.Parse(row["id"].ToString());
                }
                if (row["channel_id"] != null && row["channel_id"].ToString() != "")
                {
                    model.channel_id = int.Parse(row["channel_id"].ToString());
                }
                if (row["category_id"] != null && row["category_id"].ToString() != "")
                {
                    model.category_id = int.Parse(row["category_id"].ToString());
                }
                if (row["call_index"] != null)
                {
                    model.call_index = row["call_index"].ToString();
                }
                if (row["title"] != null)
                {
                    model.title = row["title"].ToString();
                }
                if (row["link_url"] != null)
                {
                    model.link_url = row["link_url"].ToString();
                }
                if (row["img_url"] != null)
                {
                    model.img_url = row["img_url"].ToString();
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
                if (row["zhaiyao"] != null)
                {
                    model.zhaiyao = row["zhaiyao"].ToString();
                }
                if (row["content"] != null)
                {
                    model.content = row["content"].ToString();
                }
                if (row["sort_id"] != null && row["sort_id"].ToString() != "")
                {
                    model.sort_id = int.Parse(row["sort_id"].ToString());
                }
                if (row["click"] != null && row["click"].ToString() != "")
                {
                    model.click = int.Parse(row["click"].ToString());
                }
                if (row["status"] != null && row["status"].ToString() != "")
                {
                    model.status = int.Parse(row["status"].ToString());
                }
                if (row["is_msg"] != null && row["is_msg"].ToString() != "")
                {
                    model.is_msg = int.Parse(row["is_msg"].ToString());
                }
                if (row["is_top"] != null && row["is_top"].ToString() != "")
                {
                    model.is_top = int.Parse(row["is_top"].ToString());
                }
                if (row["is_red"] != null && row["is_red"].ToString() != "")
                {
                    model.is_red = int.Parse(row["is_red"].ToString());
                }
                if (row["is_hot"] != null && row["is_hot"].ToString() != "")
                {
                    model.is_hot = int.Parse(row["is_hot"].ToString());
                }
                if (row["is_slide"] != null && row["is_slide"].ToString() != "")
                {
                    model.is_slide = int.Parse(row["is_slide"].ToString());
                }
                if (row["is_sys"] != null && row["is_sys"].ToString() != "")
                {
                    model.is_sys = int.Parse(row["is_sys"].ToString());
                }
                if (row["user_name"] != null)
                {
                    model.user_name = row["user_name"].ToString();
                }
                if (row["add_time"] != null && row["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(row["add_time"].ToString());
                }
                if (row["update_time"] != null && row["update_time"].ToString() != "")
                {
                    model.update_time = DateTime.Parse(row["update_time"].ToString());
                }
                #endregion

                //扩展字段信息
                model.fields = new article_attribute_field(databaseprefix).GetFields(model.channel_id, model.id, string.Empty);
                //相册信息
                model.albums = new article_albums(databaseprefix).GetList(model.id);
                //附件信息
                model.attach = new article_attach(databaseprefix).GetList(model.id);
                //用户组价格
                model.group_price = GetGroupPrice(model.id);
            }
            return model;
        }
        #endregion

        #region 前台模板调用方法========================
        /// <summary>
        /// 根据视图显示前几条数据
        /// </summary>
        public DataSet GetList(string channel_name, int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * FROM view_channel_" + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据视图获取总记录数
        /// </summary>
        public int GetCount(string channel_name, int category_id, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            strSql.Append(" count(1) FROM view_channel_" + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            return Convert.ToInt32(DbHelperSQL.GetSingle(strSql.ToString()));
        }

        /// <summary>
        /// 根据视图显示前几条数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int Top, string strWhere, string filedOrder)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" * FROM view_channel_" + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" order by " + filedOrder);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 根据视图获得查询分页数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * FROM view_channel_" + channel_name);
            strSql.Append(" where datediff(d,add_time,getdate())>=0");
            if (category_id > 0)
            {
                strSql.Append(" and category_id in(select id from " + databaseprefix + "article_category where class_list like '%," + category_id + ",%')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        /// <summary>
        /// 获得关健字查询分页数据(搜索用到)
        /// </summary>
        public DataSet GetSearch(string channel_name, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,channel_id,call_index,title,zhaiyao,add_time,img_url from " + databaseprefix + "article");
            strSql.Append(" where id>0");
            if (!string.IsNullOrEmpty(channel_name))
            {
                strSql.Append(" and channel_id=(select id from " + databaseprefix + "channel where [name]='" + channel_name + "')");
            }
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            recordCount = Convert.ToInt32(DbHelperSQL.GetSingle(PagingHelper.CreateCountingSql(strSql.ToString())));
            return DbHelperSQL.Query(PagingHelper.CreatePagingSql(recordCount, pageSize, pageIndex, strSql.ToString(), filedOrder));
        }

        #endregion
    }
}

