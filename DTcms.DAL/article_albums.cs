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
	/// 图片相册
	/// </summary>
	public partial class article_albums
	{
        private string databaseprefix; //数据库表名前缀
        public article_albums(string _databaseprefix)
		{
            databaseprefix = _databaseprefix;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_albums> GetList(int article_id)
        {
            List<Model.article_albums> modelList = new List<Model.article_albums>();

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,article_id,thumb_path,original_path,remark,add_time ");
            strSql.Append(" FROM " + databaseprefix + "article_albums ");
            strSql.Append(" where article_id=" + article_id);
            DataTable dt = DbHelperSQL.Query(strSql.ToString()).Tables[0];

            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.article_albums model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.article_albums();
                    if (dt.Rows[n]["id"] != null && dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    if (dt.Rows[n]["article_id"] != null && dt.Rows[n]["article_id"].ToString() != "")
                    {
                        model.article_id = int.Parse(dt.Rows[n]["article_id"].ToString());
                    }
                    if (dt.Rows[n]["thumb_path"] != null && dt.Rows[n]["thumb_path"].ToString() != "")
                    {
                        model.thumb_path = dt.Rows[n]["thumb_path"].ToString();
                    }
                    if (dt.Rows[n]["original_path"] != null && dt.Rows[n]["original_path"].ToString() != "")
                    {
                        model.original_path = dt.Rows[n]["original_path"].ToString();
                    }
                    if (dt.Rows[n]["remark"] != null && dt.Rows[n]["remark"].ToString() != "")
                    {
                        model.remark = dt.Rows[n]["remark"].ToString();
                    }
                    if (dt.Rows[0]["add_time"].ToString() != "")
                    {
                        model.add_time = DateTime.Parse(dt.Rows[0]["add_time"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 查找不存在的图片并删除已删除的图片及数据
        /// </summary>
        public void DeleteList(SqlConnection conn, SqlTransaction trans, List<Model.article_albums> models, int article_id)
        {
            StringBuilder idList = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    if (modelt.id > 0)
                    {
                        idList.Append(modelt.id + ",");
                    }
                }
            }
            string id_list = Utils.DelLastChar(idList.ToString(), ",");
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select id,thumb_path,original_path from " + databaseprefix + "article_albums where article_id=" + article_id);
            if (!string.IsNullOrEmpty(id_list))
            {
                strSql.Append(" and id not in(" + id_list + ")");
            }
            DataSet ds = DbHelperSQL.Query(conn, trans, strSql.ToString());
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                int rows = DbHelperSQL.ExecuteSql(conn, trans, "delete from " + databaseprefix + "article_albums where id=" + dr["id"].ToString()); //删除数据库
                if (rows > 0)
                {
                    Utils.DeleteFile(dr["thumb_path"].ToString()); //删除缩略图
                    Utils.DeleteFile(dr["original_path"].ToString()); //删除原图
                }
            }
        }

        /// <summary>
        /// 删除相册图片
        /// </summary>
        public void DeleteFile(List<Model.article_albums> models)
        {
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    Utils.DeleteFile(modelt.thumb_path);
                    Utils.DeleteFile(modelt.original_path);
                }
            }
        }

	}
}

