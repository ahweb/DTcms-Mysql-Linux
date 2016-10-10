using System;
using System.Data;
using System.Collections.Generic;

namespace DTcms.BLL
{
    /// <summary>
    /// OAuth应用
    /// </summary>
    public partial class user_oauth_app
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.Mysql.user_oauth_app dal;
        public user_oauth_app()
        {
            dal = new DAL.Mysql.user_oauth_app(siteConfig.sysdatabaseprefix);
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
        /// 增加一条数据
        /// </summary>
        public int Add(Model.user_oauth_app model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.user_oauth_app model)
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
        public Model.user_oauth_app GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据接口目录返回一个实体
        /// </summary>
        public Model.user_oauth_app GetModel(string api_path)
        {
            return dal.GetModel(api_path);
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
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion

        #region 扩展方法================================
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }
        #endregion
    }
}