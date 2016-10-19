using System.Collections.Generic;
using System.Data;
using System.IO;
using DTcms.Common;

namespace DTcms.Web.Plugin.submits.BLL
{
	/// <summary>
	/// 频道分类表
	/// </summary>
	public partial class submits_category
	{
        private readonly DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.submits_category dal;

		public submits_category()
		{
            dal = new DAL.submits_category(siteConfig.sysdatabaseprefix);
        }
		#region 基本方法========================================

		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}



        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }
        /// <summary>
        /// 返回类别名称
        /// </summary>
        public string GetIds(int id)
        {
            return dal.GetIds(id);
        }
        /// <summary>
        /// 返回频道分类的生成目录名
        /// </summary>
        public string GetBuildPath(int id)
        {
            return dal.GetBuildPath(id);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.submits_category model)
		{
            int newCategoryId = dal.Add(model);          
            return newCategoryId;
		}

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(int id, string strValue)
        {
            return dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string build_path, string strValue)
        {
            return dal.UpdateField(build_path, strValue);
        }

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.submits_category model)
		{
            Model.submits_category oldModel = dal.GetModel(model.id);
            if (dal.Update(model))
            {                            
                return true;
            }
            return false;
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int id)
		{
			return dal.Delete(id);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public Model.submits_category GetModel(int id)
		{
			return dal.GetModel(id);
		}



		/// <summary>
		/// 获得前几行数据
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

		#endregion

        #region 扩展方法========================================
        /// <summary>
        /// 返回默认的生成目录
        /// </summary>
        public string GetDefaultPath()
        {
            DataTable dt = GetList(1, "is_default=1", "sort_id asc,id desc").Tables[0];
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["build_path"].ToString();
            }
            return string.Empty;
        }



        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.submits_category> DataTableToList(DataTable dt)
        {
            List<Model.submits_category> modelList = new List<Model.submits_category>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.submits_category model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.submits_category();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.title = dt.Rows[n]["title"].ToString();
                    model.field_ids = dt.Rows[n]["field_ids"].ToString();
                    if (dt.Rows[n]["is_default"].ToString() != "")
                    {
                        model.is_default = int.Parse(dt.Rows[n]["is_default"].ToString());
                    }
                    if (dt.Rows[n]["sort_id"].ToString() != "")
                    {
                        model.sort_id = int.Parse(dt.Rows[n]["sort_id"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }



        #endregion
    }
}

