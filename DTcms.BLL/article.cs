using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 文章内容
    /// </summary>
    public partial class article
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.Mysql.article dal;

        public article()
        {
            dal = new DAL.Mysql.article(siteConfig.sysdatabaseprefix);
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
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string call_index)
        {
            return dal.Exists(call_index);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.article model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int id)
        {
            string content = dal.GetContent(id); //获取信息内容
            bool result = dal.Delete(id);
            if (result && !string.IsNullOrEmpty(content))
            {
                Utils.DeleteContentPic(content, siteConfig.webpath + siteConfig.filepath); //删除内容图片
            }
            return result;
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article GetModel(string call_index)
        {
            return dal.GetModel(call_index);
        }

        /// <summary>
        /// 获得前几行数据
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }
        
        /// <summary>
        /// 获得查询分页数据
        /// </summary>
        public DataSet GetList(int channel_id, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(channel_id, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }

        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string title)
        {
            return dal.ExistsTitle(title);
        }
        /// <summary>
        /// 是否存在标题
        /// </summary>
        public bool ExistsTitle(string title, int category_id)
        {
            return dal.ExistsTitle(title, category_id);
        }

        /// <summary>
        /// 返回信息标题
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// 获取阅读次数
        /// </summary>
        public int GetClick(int id)
        {
            return dal.GetClick(id);
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 返回商品库存数量
        /// </summary>
        public int GetStockQuantity(int id)
        {
            return dal.GetStockQuantity(id);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }
        #endregion

        #region 前台模板调用方法========================
        /// <summary>
        /// 根据视图显示前几条数据
        /// </summary>
        public DataSet GetList(string channel_name, int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(channel_name, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 根据视图显示前几条数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(channel_name, category_id, Top, strWhere, filedOrder);
        }

        /// <summary>
        /// 根据视图获得查询分页数据
        /// </summary>
        public DataSet GetList(string channel_name, int category_id, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(channel_name, category_id, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
          /// <summary>
        /// 根据视图获取总记录数
        /// </summary>
        public int GetCount(string channel_name, int category_id, string strWhere)
        {
            return dal.GetCount(channel_name, category_id,strWhere);
        }

        /// <summary>
        /// 获得关健字查询分页数据(搜索用到)
        /// </summary>
        public DataSet GetSearch(string channel_name, int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetSearch(channel_name, pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion

    }
}