using System;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 下载附件
    /// </summary>
    public partial class article_attach
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
      
        private readonly DAL.Mysql.article_attach dal;
   
        public article_attach()
        {
            dal = new DAL.Mysql.article_attach(siteConfig.sysdatabaseprefix);
        }

        #region 基本方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// 检查用户是否下载过该附件
        /// </summary>
        public bool ExistsLog(int attach_id, int user_id)
        {
            return dal.ExistsLog(attach_id, user_id);
        }

        /// <summary>
        /// 获取下载次数
        /// </summary>
        public int GetDownNum(int id)
        {
            return dal.GetDownNum(id);
        }

        /// <summary>
        /// 获取总下载次数
        /// </summary>
        public int GetCountNum(int article_id)
        {
            return dal.GetCountNum(article_id);
        }

        /// <summary>
        /// 插入一条下载附件记录
        /// </summary>
        public int AddLog(int user_id, string user_name, int attach_id, string file_name)
        {
            Model.user_attach_log model = new Model.user_attach_log();
            model.user_id = user_id;
            model.user_name = user_name;
            model.attach_id = attach_id;
            model.file_name = file_name;
            model.add_time = DateTime.Now;
            return dal.AddLog(model);
        }

        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.article_attach GetModel(int id)
        {
            return dal.GetModel(id);
        }

        //删除更新的旧文件
        public void DeleteFile(int id, string filePath)
        {
            Model.article_attach model = GetModel(id);
            if (model != null && model.file_path != filePath)
            {
                Utils.DeleteFile(model.file_path);
            }
        }

        #endregion  Method

    }
}