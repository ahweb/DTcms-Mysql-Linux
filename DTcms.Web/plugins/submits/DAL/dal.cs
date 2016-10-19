/*
 submits Plugin by www.chenpan.com.cn  by 20141230
*/
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
	/// 数据访问类:表单提交
	/// </summary>
	public partial class submits
	{
        private string databaseprefix; //数据库表名前缀
        public submits(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select count(1) from " + databaseprefix + "submits");
			strSql.Append(" where id=@id");
			MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
			parameters[0].Value = id;

			return DbHelperMySql.Exists(strSql.ToString(),parameters);
		}

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.submits model)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("insert into " + databaseprefix + "submits(");
            strSql.Append("title,content,user_name,user_tel,user_qq,user_email,add_time,is_lock)");
			strSql.Append(" values (");
            strSql.Append("@title,@content,@user_name,@user_tel,@user_qq,@user_email,@add_time,@is_lock)");
			strSql.Append(";select @@IDENTITY");
            strSql.Append(";set @ReturnValue=@@IDENTITY");
			MySqlParameter[] parameters = {
					new MySqlParameter("@title", MySqlDbType.VarChar,100),
					new MySqlParameter("@content", MySqlDbType.LongText),
					new MySqlParameter("@user_name", MySqlDbType.VarChar,50),
					new MySqlParameter("@user_tel", MySqlDbType.VarChar,30),
					new MySqlParameter("@user_qq", MySqlDbType.VarChar,30),
					new MySqlParameter("@user_email", MySqlDbType.VarChar,100),
					new MySqlParameter("@add_time", MySqlDbType.Date),
                    new MySqlParameter("@is_lock", MySqlDbType.Int32,4),
                    new MySqlParameter("@ReturnValue",MySqlDbType.Int32)};
			parameters[0].Value = model.title;
			parameters[1].Value = model.content;
			parameters[2].Value = model.user_name;
			parameters[3].Value = model.user_tel;
			parameters[4].Value = model.user_qq;
			parameters[5].Value = model.user_email;
			parameters[6].Value = model.add_time;
            parameters[7].Value = model.is_lock;
            parameters[8].Direction = ParameterDirection.Output;

            Hashtable sqllist = new Hashtable();
            sqllist.Add(strSql.ToString(), parameters);

            //添加扩展字段
            if (model.fields != null)
            {
                StringBuilder strSql2 = new StringBuilder();
                StringBuilder strFieldName = new StringBuilder(); //字段列表
                StringBuilder strFieldVar = new StringBuilder(); //字段声明
                MySqlParameter[] parameters2 = new MySqlParameter[model.fields.Count + 1];
                int i = 1;
                strFieldName.Append("submits_id");
                strFieldVar.Append("@submits_id");
                parameters2[0] = new MySqlParameter("@submits_id", MySqlDbType.Int32, 4);
                parameters2[0].Direction = ParameterDirection.InputOutput;
                foreach (KeyValuePair<string, string> kvp in model.fields)
                {
                    strFieldName.Append("," + kvp.Key);
                    strFieldVar.Append(",@" + kvp.Key);
                    if (kvp.Value.Length <= 4000)
                    {
                        parameters2[i] = new MySqlParameter("@" + kvp.Key, MySqlDbType.VarChar, kvp.Value.Length);
                    }
                    else
                    {
                        parameters2[i] = new MySqlParameter("@" + kvp.Key, MySqlDbType.LongText);
                    }

                    parameters2[i].Value = kvp.Value;
                    i++;
                }
                strSql2.Append("insert into " + databaseprefix + "submits_value(");
                strSql2.Append(strFieldName.ToString() + ")");
                strSql2.Append(" values (");
                strSql2.Append(strFieldVar.ToString() + ")");
                sqllist.Add(strSql2.ToString(), parameters2);
              
            }
            DbHelperMySql.ExecuteSqlTran(sqllist);
            string str = parameters[8].Value.ToString();
            return (int)parameters[8].Value;


		}

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update " + databaseprefix + "submits set " + strValue);
            strSql.Append(" where id=" + id);
            DbHelperMySql.ExecuteSql(strSql.ToString());
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.submits model)
        {
          using (MySqlConnection conn = new MySqlConnection(DbHelperMySql.connectionString))
            {
                conn.Open();
                using (MySqlTransaction trans = conn.BeginTransaction())
                {
                  try
                  {
                    StringBuilder strSql = new StringBuilder();
                    strSql.Append("update " + databaseprefix + "submits set ");
                    strSql.Append("title=@title,");
                    strSql.Append("content=@content,");
                    strSql.Append("user_name=@user_name,");
                    strSql.Append("user_tel=@user_tel,");
                    strSql.Append("user_qq=@user_qq,");
                    strSql.Append("user_email=@user_email,");
                    strSql.Append("add_time=@add_time,");
                    strSql.Append("reply_content=@reply_content,");
                    strSql.Append("reply_time=@reply_time,");
                    strSql.Append("is_lock=@is_lock");
                    strSql.Append(" where id=@id");
                    MySqlParameter[] parameters = {
					        new MySqlParameter("@title", MySqlDbType.VarChar,100),
					        new MySqlParameter("@content", MySqlDbType.LongText),
					        new MySqlParameter("@user_name", MySqlDbType.VarChar,50),
					        new MySqlParameter("@user_tel", MySqlDbType.VarChar,30),
					        new MySqlParameter("@user_qq", MySqlDbType.VarChar,30),
					        new MySqlParameter("@user_email", MySqlDbType.VarChar,100),
					        new MySqlParameter("@add_time", MySqlDbType.Date),
					        new MySqlParameter("@reply_content", MySqlDbType.LongText),
					        new MySqlParameter("@reply_time", MySqlDbType.Date),
					        new MySqlParameter("@is_lock", MySqlDbType.Int32,4),
					        new MySqlParameter("@id", MySqlDbType.Int32,4)};
                    parameters[0].Value = model.title;
                    parameters[1].Value = model.content;
                    parameters[2].Value = model.user_name;
                    parameters[3].Value = model.user_tel;
                    parameters[4].Value = model.user_qq;
                    parameters[5].Value = model.user_email;
                    parameters[6].Value = model.add_time;
                    parameters[7].Value = model.reply_content;
                    parameters[8].Value = model.reply_time;
                    parameters[9].Value = model.is_lock;
                    parameters[10].Value = model.id;
                    DbHelperMySql.ExecuteSql(conn, trans, strSql.ToString(), parameters);        
                    //修改扩展字段
                    if (model.fields.Count > 0)
                    {
                        StringBuilder strSql2 = new StringBuilder();
                        StringBuilder strFieldName = new StringBuilder(); //字段列表
                        MySqlParameter[] parameters2 = new MySqlParameter[model.fields.Count + 1];
                        int i = 0;
                        foreach (KeyValuePair<string, string> kvp in model.fields)
                        {
                            strFieldName.Append(kvp.Key + "=@" + kvp.Key + ",");
                            if (kvp.Value.Length <= 4000)
                            {
                                parameters2[i] = new MySqlParameter("@" + kvp.Key, MySqlDbType.VarChar, kvp.Value.Length);
                            }
                            else
                            {
                                parameters2[i] = new MySqlParameter("@" + kvp.Key, MySqlDbType.LongText);
                            }
                            parameters2[i].Value = kvp.Value;
                            i++;
                        }
                        strSql2.Append("update " + databaseprefix + "submits_value set ");
                        strSql2.Append(Utils.DelLastComma(strFieldName.ToString()));
                        strSql2.Append(" where submits_id=@submits_id");
                        parameters2[i] = new MySqlParameter("@submits_id", MySqlDbType.Int32, 4);
                        parameters2[i].Value = model.id;
                        DbHelperMySql.ExecuteSql(conn, trans, strSql2.ToString(), parameters2);
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

            //删除扩展字段表
            StringBuilder strSql1 = new StringBuilder();
            strSql1.Append("delete from " + databaseprefix + "submits_value ");
            strSql1.Append(" where submits_id=@submits_id ");
            MySqlParameter[] parameters1 = {
					new MySqlParameter("@submits_id", MySqlDbType.Int32,4)};
            parameters1[0].Value = id;
            Hashtable sqllist = new Hashtable();
            sqllist.Add(strSql1.ToString(), parameters1);
            

            //删除主表
            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from " + databaseprefix + "submits ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;
            sqllist.Add(strSql.ToString(), parameters);
            

            var rowsAffected = DbHelperMySql.ExecuteSqlTran(sqllist);
            if (rowsAffected)
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
        public Model.submits  GetModel(int id)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 id,title,content,user_name,user_tel,user_qq,user_email,add_time,reply_content,reply_time,is_lock from " + databaseprefix + "submits ");
            strSql.Append(" where id=@id");
            MySqlParameter[] parameters = {
					new MySqlParameter("@id", MySqlDbType.Int32,4)};
            parameters[0].Value = id;

            Model.submits model = new Model.submits();
            DataSet ds = DbHelperMySql.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["id"] != null && ds.Tables[0].Rows[0]["id"].ToString() != "")
                {
                    model.id = int.Parse(ds.Tables[0].Rows[0]["id"].ToString());
                }
                if (ds.Tables[0].Rows[0]["title"] != null && ds.Tables[0].Rows[0]["title"].ToString() != "")
                {
                    model.title = ds.Tables[0].Rows[0]["title"].ToString();
                }
                if (ds.Tables[0].Rows[0]["content"] != null && ds.Tables[0].Rows[0]["content"].ToString() != "")
                {
                    model.content = ds.Tables[0].Rows[0]["content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_name"] != null && ds.Tables[0].Rows[0]["user_name"].ToString() != "")
                {
                    model.user_name = ds.Tables[0].Rows[0]["user_name"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_tel"] != null && ds.Tables[0].Rows[0]["user_tel"].ToString() != "")
                {
                    model.user_tel = ds.Tables[0].Rows[0]["user_tel"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_qq"] != null && ds.Tables[0].Rows[0]["user_qq"].ToString() != "")
                {
                    model.user_qq = ds.Tables[0].Rows[0]["user_qq"].ToString();
                }
                if (ds.Tables[0].Rows[0]["user_email"] != null && ds.Tables[0].Rows[0]["user_email"].ToString() != "")
                {
                    model.user_email = ds.Tables[0].Rows[0]["user_email"].ToString();
                }
                if (ds.Tables[0].Rows[0]["add_time"] != null && ds.Tables[0].Rows[0]["add_time"].ToString() != "")
                {
                    model.add_time = DateTime.Parse(ds.Tables[0].Rows[0]["add_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["reply_content"] != null && ds.Tables[0].Rows[0]["reply_content"].ToString() != "")
                {
                    model.reply_content = ds.Tables[0].Rows[0]["reply_content"].ToString();
                }
                if (ds.Tables[0].Rows[0]["reply_time"] != null && ds.Tables[0].Rows[0]["reply_time"].ToString() != "")
                {
                    model.reply_time = DateTime.Parse(ds.Tables[0].Rows[0]["reply_time"].ToString());
                }
                if (ds.Tables[0].Rows[0]["is_lock"] != null && ds.Tables[0].Rows[0]["is_lock"].ToString() != "")
                {
                    model.is_lock = int.Parse(ds.Tables[0].Rows[0]["is_lock"].ToString());
                }
                #region 扩展字段信息==================
                //查询该频道的扩展字段名称
                DataTable dt = new submits_field(databaseprefix).GetList("").Tables[0];
                if (dt.Rows.Count > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (DataRow dr in dt.Rows)
                    {
                        sb.Append(dr["name"].ToString() + ",");
                    }
                    StringBuilder strSql2 = new StringBuilder();
                    strSql2.Append("select top 1 " + Utils.DelLastComma(sb.ToString()) + " from " + databaseprefix + "submits_value ");
                    strSql2.Append(" where submits_id=@submits_id ");
                    MySqlParameter[] parameters2 = {
					    new MySqlParameter("@submits_id", MySqlDbType.Int32,4)};
                    parameters2[0].Value = id;

                    DataSet ds2 = DbHelperMySql.Query(strSql2.ToString(), parameters2);
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        Dictionary<string, string> dic = new Dictionary<string, string>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            if (ds2.Tables[0].Rows[0][dr["name"].ToString()] != null)
                            {
                                dic.Add(dr["name"].ToString(), ds2.Tables[0].Rows[0][dr["name"].ToString()].ToString());
                            }
                            else
                            {
                                dic.Add(dr["name"].ToString(), "");
                            }
                        }
                        model.fields = dic;
                    }
                }

                #endregion

                return model;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select ");
            if (Top > 0)
            {
                strSql.Append(" top " + Top.ToString());
            }
            strSql.Append(" a.id, a.title,a.content,a.reply_content, a.user_name,a.user_tel,a.user_qq,a.user_email,a.reply_time,a.is_lock,b.* FROM " + databaseprefix + "submits as a left join " + databaseprefix + "submits_value as b on a.id=b.submits_id ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by b.add_time desc");
            return DbHelperMySql.Query(strSql.ToString());
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select a.id,a.title,a.content,a.user_name,a.user_tel,a.user_qq,a.user_email,a.reply_content,a.reply_time,a.is_lock,b.* FROM " + databaseprefix + "submits as a left join " + databaseprefix + "submits_value as b on a.id=b.submits_id ");
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

