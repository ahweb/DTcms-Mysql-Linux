using System;
using System.IO;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
	/// <summary>
	/// 站点管理
	/// </summary>
	public partial class channel_site
	{
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
		private readonly DAL.Mysql.channel_site dal;
        public channel_site()
        {
            dal = new DAL.Mysql.channel_site(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法================================
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        /// <summary>
        /// 查询生成目录名是否存在
        /// </summary>
        public bool Exists(string build_path)
        {
            //与站点目录下的一级文件夹是否重名
            if (DirPathExists(siteConfig.webpath, build_path))
            {
                return true;
            }
            //与站点aspx目录下的一级文件夹是否重名
            if (DirPathExists(siteConfig.webpath + "/" + DTKeys.DIRECTORY_REWRITE_ASPX + "/", build_path))
            {
                return true;
            }
            //与频道名称是否重名
            if (new DAL.Mysql.channel(siteConfig.sysdatabaseprefix).Exists(build_path))
            {
                return true;
            }
            //与其它站点目录是否重名
            if (dal.Exists(build_path))
            {
                return true;
            }
            return false;
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.channel_site model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.channel_site model)
		{
            string old_build_path = dal.GetBuildPath(model.id);
            if (string.IsNullOrEmpty(old_build_path))
            {
                return false;
            }
            if (dal.Update(model, old_build_path))
            {
                if (old_build_path.ToLower() != model.build_path.ToLower())
                {
                    //更改频道分类对应的目录名称
                    Utils.MoveDirectory(siteConfig.webpath + DTKeys.DIRECTORY_REWRITE_ASPX + "/" + old_build_path,
                        siteConfig.webpath + DTKeys.DIRECTORY_REWRITE_ASPX + "/" + model.build_path);
                    Utils.MoveDirectory(siteConfig.webpath + DTKeys.DIRECTORY_REWRITE_HTML + "/" + old_build_path,
                        siteConfig.webpath + DTKeys.DIRECTORY_REWRITE_HTML + "/" + model.build_path);
                }
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
		public Model.channel_site GetModel(int id)
		{
			return dal.GetModel(id);
		}

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.channel_site GetModel(string build_path)
        {
            return dal.GetModel(build_path);
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

        #region 扩展方法================================
        /// <summary>
        /// 返回站点名称
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// 返回站点名称
        /// </summary>
        public string GetTitle(string build_path)
        {
            return dal.GetTitle(build_path);
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
        /// 保存排序
        /// </summary>
        public bool UpdateSort(int id, int sort_id)
        {
            //取得站点的目录名
            string build_path = dal.GetBuildPath(id);
            if (build_path == string.Empty)
            {
                return false;
            }
            new BLL.navigation().UpdateField("channel_" + build_path, "sort_id=" + sort_id);
            dal.UpdateField(id, "sort_id=" + sort_id);
            return true;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.channel_site> GetModelList()
        {
            DataSet ds = dal.GetList(0, string.Empty, "sort_id asc,id desc");
            return DataTableToList(ds.Tables[0]);
        }
        #endregion

        #region 私有方法================================
        /// <summary>
        /// 获得数据列表
        /// </summary>
        private List<Model.channel_site> DataTableToList(DataTable dt)
        {
            List<Model.channel_site> modelList = new List<Model.channel_site>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                for (int n = 0; n < rowsCount; n++)
                {
                    modelList.Add(dal.DataRowToModel(dt.Rows[n]));
                }
            }
            return modelList;
        }

        /// <summary>
        /// 检查生成目录名与指定路径下的一级目录是否同名
        /// </summary>
        /// <param name="dirPath">指定的路径</param>
        /// <param name="build_path">生成目录名</param>
        /// <returns>bool</returns>
        private bool DirPathExists(string dirPath, string build_path)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(dirPath));
            foreach (DirectoryInfo dir in dirInfo.GetDirectories())
            {
                if (build_path.ToLower() == dir.Name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }
        #endregion
    }
}

