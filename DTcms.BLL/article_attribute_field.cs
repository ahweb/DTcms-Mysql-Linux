using System;
using System.Data;
using System.Collections.Generic;

namespace DTcms.BLL
{
    /// <summary>
    /// 扩展属性表
    /// </summary>
    public partial class article_attribute_field
    {
        private readonly Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.Mysql.article_attribute_field dal;

        public article_attribute_field()
        {
            dal = new DAL.Mysql.article_attribute_field(siteConfig.sysdatabaseprefix);
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
        /// 查询是否存在列
        /// </summary>
        public bool Exists(string column_name)
        {
            return dal.Exists(column_name);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Model.article_attribute_field model)
        {
            switch (model.control_type)
            {
                case "single-text": //单行文本
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }
                    break;
                case "multi-text": //多行文本
                    goto case "single-text";
                case "editor": //编辑器
                    model.data_type = "ntext";
                    break;
                case "images": //图片
                    model.data_type = "nvarchar(255)";
                    break;
                case "video": //视频
                    model.data_type = "nvarchar(255)";
                    break;
                case "number": //数字
                    if (model.data_place > 0)
                    {
                        model.data_type = "decimal(9," + model.data_place + ")";
                    }
                    else
                    {
                        model.data_type = "int";
                    }
                    break;
                case "checkbox": //复选框
                    model.data_type = "tinyint";
                    break;
                case "multi-radio": //多项单选
                    if (model.data_type == "int")
                    {
                        model.data_length = 4;
                        model.data_type = "int";
                    }
                    else
                    {
                        if (model.data_length > 0 && model.data_length <= 4000)
                        {
                            model.data_type = "nvarchar(" + model.data_length + ")";
                        }
                        else if (model.data_length > 4000)
                        {
                            model.data_type = "ntext";
                        }
                        else
                        {
                            model.data_length = 50;
                            model.data_type = "nvarchar(50)";
                        }
                    }

                    break;
                case "multi-checkbox": //多项多选
                    goto case "single-text";
            }
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
        public bool Update(Model.article_attribute_field model)
        {
            switch (model.control_type)
            {
                case "single-text": //单行文本
                    if (model.data_length > 0 && model.data_length <= 4000)
                    {
                        model.data_type = "nvarchar(" + model.data_length + ")";
                    }
                    else if (model.data_length > 4000)
                    {
                        model.data_type = "ntext";
                    }
                    else
                    {
                        model.data_length = 50;
                        model.data_type = "nvarchar(50)";
                    }
                    break;
                case "multi-text": //多行文本
                    goto case "single-text";
                case "editor": //编辑器
                    model.data_type = "ntext";
                    break;
                case "images": //图片
                    model.data_type = "nvarchar(255)";
                    break;
                case "video": //视频
                    model.data_type = "nvarchar(255)";
                    break;
                case "number": //数字
                    if (model.data_place > 0)
                    {
                        model.data_type = "decimal(9," + model.data_place + ")";
                    }
                    else
                    {
                        model.data_type = "int";
                    }
                    break;
                case "checkbox": //复选框
                    model.data_type = "tinyint";
                    break;
                case "multi-radio": //多项单选
                    if (model.data_type == "int")
                    {
                        model.data_length = 4;
                        model.data_type = "int";
                    }
                    else
                    {
                        if (model.data_length > 0 && model.data_length <= 4000)
                        {
                            model.data_type = "nvarchar(" + model.data_length + ")";
                        }
                        else if (model.data_length > 4000)
                        {
                            model.data_type = "ntext";
                        }
                        else
                        {
                            model.data_length = 50;
                            model.data_type = "nvarchar(50)";
                        }
                    }

                    break;
                case "multi-checkbox": //多项多选
                    goto case "single-text";
            }
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
        public Model.article_attribute_field GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_attribute_field> GetModelList(int channel_id, string strWhere)
        {
            DataSet ds = dal.GetList(channel_id, strWhere);
            return DataTableToList(ds.Tables[0]);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.article_attribute_field> DataTableToList(DataTable dt)
        {
            List<Model.article_attribute_field> modelList = new List<Model.article_attribute_field>();
            int rowsCount = dt.Rows.Count;
            if (rowsCount > 0)
            {
                Model.article_attribute_field model;
                for (int n = 0; n < rowsCount; n++)
                {
                    model = new Model.article_attribute_field();
                    if (dt.Rows[n]["id"].ToString() != "")
                    {
                        model.id = int.Parse(dt.Rows[n]["id"].ToString());
                    }
                    model.name = dt.Rows[n]["name"].ToString();
                    model.title = dt.Rows[n]["title"].ToString();
                    model.control_type = dt.Rows[n]["control_type"].ToString();
                    model.data_type = dt.Rows[n]["data_type"].ToString();
                    if (dt.Rows[n]["data_length"].ToString() != "")
                    {
                        model.data_length = int.Parse(dt.Rows[n]["data_length"].ToString());
                    }
                    if (dt.Rows[n]["data_place"].ToString() != "")
                    {
                        model.data_place = int.Parse(dt.Rows[n]["data_place"].ToString());
                    }
                    model.item_option = dt.Rows[n]["item_option"].ToString();
                    model.default_value = dt.Rows[n]["default_value"].ToString();
                    if (dt.Rows[n]["is_required"].ToString() != "")
                    {
                        model.is_required = int.Parse(dt.Rows[n]["is_required"].ToString());
                    }
                    if (dt.Rows[n]["is_password"].ToString() != "")
                    {
                        model.is_password = int.Parse(dt.Rows[n]["is_password"].ToString());
                    }
                    if (dt.Rows[n]["is_html"].ToString() != "")
                    {
                        model.is_html = int.Parse(dt.Rows[n]["is_html"].ToString());
                    }
                    if (dt.Rows[n]["editor_type"].ToString() != "")
                    {
                        model.editor_type = int.Parse(dt.Rows[n]["editor_type"].ToString());
                    }
                    model.valid_tip_msg = dt.Rows[n]["valid_tip_msg"].ToString();
                    model.valid_error_msg = dt.Rows[n]["valid_error_msg"].ToString();
                    model.valid_pattern = dt.Rows[n]["valid_pattern"].ToString();
                    if (dt.Rows[n]["sort_id"].ToString() != "")
                    {
                        model.sort_id = int.Parse(dt.Rows[n]["sort_id"].ToString());
                    }
                    if (dt.Rows[n]["is_sys"].ToString() != "")
                    {
                        model.is_sys = int.Parse(dt.Rows[n]["is_sys"].ToString());
                    }
                    modelList.Add(model);
                }
            }
            return modelList;
        }

        /// <summary>
        /// 获得频道对应的数据
        /// </summary>
        public DataSet GetList(int channel_id, string strWhere)
        {
            return dal.GetList(channel_id, strWhere);
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