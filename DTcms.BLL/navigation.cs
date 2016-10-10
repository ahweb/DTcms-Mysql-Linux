using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
	/// <summary>
	/// 系统导航菜单
	/// </summary>
	public partial class navigation
	{
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
		private readonly DAL.Mysql.navigation dal;
		public navigation()
		{
            dal = new DAL.Mysql.navigation(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法===============================
        /// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(int id)
		{
			return dal.Exists(id);
		}

        /// <summary>
        /// 查询名称是否存在
        /// </summary>
        public bool Exists(string name)
        {
            return dal.Exists(name);
        }

		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int Add(Model.navigation model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(Model.navigation model)
		{
			return dal.Update(model);
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
        public Model.navigation GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.navigation GetModel(string name)
        {
            return dal.GetModel(name);
        }

        /// <summary>
        /// 取得所有类别列表
        /// </summary>
        /// <param name="parent_id">父ID</param>
        /// <param name="nav_type">导航类别</param>
        /// <returns>DataTable</returns>
        public DataTable GetList(int parent_id, string nav_type)
        {
            return dal.GetList(parent_id, nav_type);
        }
		#endregion

        #region 扩展方法===============================
        /// <summary>
        /// 根据导航的名称查询其ID
        /// </summary>
        /// <param name="nav_name">菜单名称</param>
        /// <returns>int</returns>
        public int GetNavId(string nav_name)
        {
            return dal.GetNavId(nav_name);
        }

        /// <summary>
        /// 快捷添加系统默认导航
        /// </summary>
        /// <param name="parent_name">父导航名称</param>
        /// <param name="nav_name">导航名称</param>
        /// <param name="title">导航标题</param>
        /// <param name="link_url">链接地址</param>
        /// <param name="sort_id">排序数字</param>
        /// <param name="channel_id">所属频道ID</param>
        /// <param name="action_type">操作权限以英文逗号分隔开</param>
        /// <returns>int</returns>
        public int Add(string parent_name, string nav_name, string title, string link_url, int sort_id, int channel_id, string action_type)
        {
            return dal.Add(parent_name, nav_name, title, link_url, sort_id, channel_id, action_type);
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
        public bool UpdateField(string name, string strValue)
        {
            return dal.UpdateField(name, strValue);
        }
        #endregion
    }
}

