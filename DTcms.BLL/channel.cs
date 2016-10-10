using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using DTcms.Common;

namespace DTcms.BLL
{
    /// <summary>
    /// 系统频道表
    /// </summary>
    public partial class channel
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.Mysql.channel dal;

        public channel()
        {
            dal = new DAL.Mysql.channel(siteConfig.sysdatabaseprefix);
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
        /// 查询频道名称是否存在
        /// </summary>
        public bool Exists(string name)
        {
            //与站点目录下的一级文件夹是否同名
            if (DirPathExists(siteConfig.webpath, name))
            {
                return true;
            }
            //与站点aspx目录下的一级文件夹是否同名
            if (DirPathExists(siteConfig.webpath + "/" + DTKeys.DIRECTORY_REWRITE_ASPX + "/", name))
            {
                return true;
            }
            //与存在的频道名称是否同名
            if (dal.Exists(name))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回数据总数
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.channel model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Model.channel model)
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
        public Model.channel GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Model.channel GetModel(string channel_name)
        {
            return dal.GetModel(channel_name);
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

        #region 扩展方法===============================
        /// <summary>
        /// 修改一列数据
        /// </summary>
        public void UpdateField(int id, string strValue)
        {
            dal.UpdateField(id, strValue);
        }

        /// <summary>
        /// 返回频道名称
        /// </summary>
        public string GetChannelName(int id)
        {
            return dal.GetChannelName(id);
        }

        /// <summary>
        /// 返回频道ID
        /// </summary>
        public int GetChannelId(string name)
        {
            return dal.GetChannelId(name);
        }

        /// <summary>
        /// 保存排序
        /// </summary>
        public bool UpdateSort(int id, int sort_id)
        {
            //取得频道的名称
            string channel_name = dal.GetChannelName(id);
            if (channel_name == string.Empty)
            {
                return false;
            }
            if (new BLL.navigation().UpdateField("channel_" + channel_name, "sort_id=" + sort_id))
            {
                dal.UpdateField(id, "sort_id=" + sort_id);
                return true;
            }
            return false;
        }
        #endregion

        #region 私有方法===============================
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