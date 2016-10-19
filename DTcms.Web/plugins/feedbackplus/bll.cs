using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.Web.Plugin.FeedbackPlus.BLL
{
	/// <summary>
	/// 在线留言
	/// </summary>
    public partial class feedbackplus
    {
        private readonly DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.feedbackplus dal;
        public feedbackplus()
        {
            dal = new DAL.feedbackplus(siteConfig.sysdatabaseprefix);
        }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.feedbackplus model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.feedbackplus model)
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
        public Model.feedbackplus GetModel(int id)
        {
            return dal.GetModel(id);
        }
        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere)
        {
            return dal.GetList(Top, strWhere);
        }

        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion  Method
    }

    /// <summary>
    /// 配置文件
    /// </summary>
    public partial class install
    {
        private readonly DAL.install dal;

        public install()
        {
            dal = new DAL.install();
        }

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.install loadConfig(string config_path)
        {
            string cacheName = "gs_cache_feedbackplus_config";
            Model.install model = CacheHelper.Get<Model.install>(cacheName);
            if (model == null)
            {
                CacheHelper.Insert(cacheName, dal.loadConfig(Utils.GetMapPath(config_path)), Utils.GetMapPath(config_path));
                model = CacheHelper.Get<Model.install>(cacheName);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.install saveConifg(Model.install model, string config_path)
        {
            return dal.saveConifg(model, Utils.GetMapPath(config_path));
        }
    }
}

