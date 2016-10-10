using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 邮件模板
    /// </summary>
    public partial class mail_template
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.Mysql.mail_template dal;
        public mail_template()
        {
            dal = new DAL.Mysql.mail_template(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法============================
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
        public int Add(Model.mail_template model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.mail_template model)
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
        public Model.mail_template GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.mail_template GetModel(string call_index)
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
        public DataSet GetList(int pageSize, int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }
        #endregion
    }
}