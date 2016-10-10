using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 订单表
    /// </summary>
    public partial class orders
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private DAL.Mysql.orders dal;
        public orders()
        {
            dal = new DAL.Mysql.orders(siteConfig.sysdatabaseprefix);
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
        public bool Exists(string order_no)
        {
            return dal.Exists(order_no);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.orders model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.orders model)
        {
            //计算订单总金额:商品总金额+配送费用+支付手续费
            model.order_amount = model.real_amount + model.express_fee + model.payment_fee + model.invoice_taxes;
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
        public Model.orders GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 根据订单号返回一个实体
        /// </summary>
        public Model.orders GetModel(string order_no)
        {
            return dal.GetModel(order_no);
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
        /// 返回数据数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public bool UpdateField(string order_no, string strValue)
        {
            return dal.UpdateField(order_no, strValue);
        }
        #endregion
    }
}