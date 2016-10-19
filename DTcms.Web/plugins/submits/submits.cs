using System;
using System.Text;
using System.Data;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DTcms.Common;


namespace DTcms.Web.Plugin.submits
{
    public partial class submits : Web.UI.BasePage
    {
        protected int page; //当前页码
        protected int totalcount; //OUT数据总数
        public string html_fields = string.Empty;
        protected int category_id;
        /// <summary>
        /// 重写虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            page = DTRequest.GetQueryInt("page", 1);
            category_id = DTRequest.GetQueryInt("category_id", 0);
            try
            {
                CreateOtherField(category_id);
            }
            catch { }
        }
       

        /// <summary>
        /// 表单列表
        /// </summary>
        /// <param name="top">显示条数</param>
        /// <param name="strwhere">查询条件</param>
        /// <returns>DataTable</returns>
        public DataTable get_submits_list(int top, string strwhere)
        {
            DataTable dt = new DataTable();
            string _where = "";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " " + strwhere;
            }
            dt = new BLL.submits().GetList(top, _where).Tables[0];
            return dt;
        }

        /// <summary>
        /// 表单分页列表
        /// </summary>
        /// <param name="page_size">页面大小</param>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <returns>DateTable</returns>
        public DataTable get_submits_list(int page_size, int page_index, string strwhere, out int totalcount)
        {
            DataTable dt = new DataTable();
            string _where = " ";
            if (!string.IsNullOrEmpty(strwhere))
            {
                _where += " " + strwhere;
            }
            dt = new BLL.submits().GetList(page_size, page_index, _where, "add_time desc", out totalcount).Tables[0];
            return dt;
        }

      

        /// <summary>
        /// 文章分页列表
        /// </summary>
        /// <param name="page_index">当前页码</param>
        /// <param name="strwhere">查询条件</param>
        /// <param name="totalcount">总记录数</param>
        /// <param name="_key">URL配置名称</param>
        /// <param name="_params">传输参数</param>
        /// <returns>DataTable</returns>
        public DataTable get_submits_list_all( int category_id, int page_index, int page_size, string strwhere, out int totalcount, out string pagelist, string _key, params object[] _params)
        {
            DataTable dt = new DataTable();
            if (!string.IsNullOrEmpty(""))
            {
              //  dt = new BLL.article().GetList( category_id, page_index, strwhere, "sort_id asc,add_time desc", out totalcount, out pagesize).Tables[0];
                
                dt= get_submits_list(page_size, page_index, strwhere, out totalcount);
                pagelist = Utils.OutPageList(page_size, page_index, totalcount, linkurl(_key, _params), 8);
            }
            else
            {
                totalcount = 0;
                pagelist = "";
            }
            return dt;
        }







        #region 创建其它扩展字段=========================
        public void CreateOtherField(int category_id)
        {
            StringBuilder sbhtml = new StringBuilder();
            string strwhere = "";
            if (category_id != 0)
            {
                BLL.submits_category bll_c = new BLL.submits_category();
                string ids = bll_c.GetIds(category_id);
                if (ids != "") {             
                    strwhere = " and id in(" + ids + ")";
                    List<Model.submits_field> ls = new BLL.submits_field().GetModelList("is_sys=1" + strwhere);
                    foreach (Model.submits_field modelt in ls)
                    {

                        sbhtml.Append("<dl>");
                        sbhtml.Append("<dt>" + modelt.title + "：</dt>");
                        sbhtml.Append("<dd>");
                        switch (modelt.control_type)
                        {
                            case "single-text":
                                #region  单行文本
                                if (modelt.control_type == "single-text" || modelt.control_type == "number" || modelt.control_type == "images") //单行
                                {
                                    sbhtml.Append("<input name=\"field_control_" + modelt.name + "\" id=\"field_control_" + modelt.name + "\" ");
                                    if (modelt.control_type == "number") //数字
                                    {
                                        sbhtml.Append("class=\"input small\"");
                                    }
                                    else if (modelt.control_type == "images")
                                    {
                                        sbhtml.Append(" class=\"input txt upload-path\"");
                                    }
                                    else
                                    {
                                        sbhtml.Append("class=\"input txt\"");
                                    }
                                    //是否密码框
                                    if (modelt.is_password == 1)
                                    {
                                        sbhtml.Append("type=\"password\"");
                                    }
                                    else
                                    {
                                        sbhtml.Append("type=\"text\" ");
                                    }

                                    #region 验证信息
                                    //验证提示信息
                                    if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                                    {
                                        sbhtml.Append("tipmsg=\"" + modelt.valid_tip_msg + "\" ");
                                    }
                                    //验证失败提示信息
                                    if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                                    {
                                        sbhtml.Append("errormsg=\"" + modelt.valid_error_msg + "\" ");
                                    }
                                    //验证表达式
                                    if (!string.IsNullOrEmpty(modelt.valid_pattern))
                                    {
                                        sbhtml.Append("datatype=\"" + modelt.valid_pattern + "\"");
                                        sbhtml.Append("sucmsg=\"\"");
                                    }
                                    else
                                    {
                                        sbhtml.Append("datatype=\"*\"");
                                        sbhtml.Append("sucmsg=\"\"");
                                    }
                                    #endregion

                                    //设置默认值
                                    sbhtml.Append(" value=\"" + modelt.default_value + "\"/>");
                                }
                                if (modelt.control_type == "images")//如果是图片增加图片上传按钮
                                {
                                    sbhtml.Append("<div class=\"upload-box upload-img\" style=\"margin-left:4px;\"></div>");
                                }

                                //创建一个提示
                                sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                                #endregion
                                break;
                            case "multi-text": //多行文本
                                #region 多行文本
                                sbhtml.Append("<textarea name=\"field_control_" + modelt.name + "\" id=\"field_control_" + modelt.name + "\" style=\"width:350px;height:80px;\"");

                                sbhtml.Append(" class=\"input txt\" ");
                                #region 验证信息
                                //验证提示信息
                                if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                                {
                                    sbhtml.Append("tipmsg=\"" + modelt.valid_tip_msg + "\" ");
                                }
                                //验证失败提示信息
                                if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                                {
                                    sbhtml.Append("errormsg=\"" + modelt.valid_error_msg + "\" ");
                                }
                                //验证表达式
                                if (!string.IsNullOrEmpty(modelt.valid_pattern))
                                {
                                    sbhtml.Append("datatype=\"" + modelt.valid_pattern + "\"");
                                    sbhtml.Append("sucmsg=\"\"");
                                }
                                else
                                {
                                    sbhtml.Append("datatype=\"*\"");
                                    sbhtml.Append("sucmsg=\"\"");
                                }

                                #endregion
                                sbhtml.Append(">");
                                sbhtml.Append(modelt.default_value + "</textarea>");
                                sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                                #endregion
                                break;
                            case "editor": //编辑器
                                #region 编辑器
                                sbhtml.Append("<textarea name=\"field_control_" + modelt.name + "\" id=\"field_control_" + modelt.name + "\" style=\"visibility:hidden;\"");
                                #region 验证信息
                                //验证提示信息
                                if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                                {
                                    sbhtml.Append("tipmsg=\"" + modelt.valid_tip_msg + "\" ");
                                }
                                //验证失败提示信息
                                if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                                {
                                    sbhtml.Append("errormsg=\"" + modelt.valid_error_msg + "\" ");
                                }
                                //验证表达式
                                if (!string.IsNullOrEmpty(modelt.valid_pattern))
                                {
                                    sbhtml.Append("datatype=\"" + modelt.valid_pattern + "\"");
                                    sbhtml.Append("sucmsg=\"\"");
                                }
                                else
                                {
                                    sbhtml.Append("datatype=\"*\"");
                                    sbhtml.Append("sucmsg=\"\"");
                                }

                                #endregion

                                if (modelt.editor_type == 1)
                                {
                                    sbhtml.Append(" class=\"editor-mini\">");
                                }
                                else
                                {
                                    sbhtml.Append(" class=\"editor\">");
                                }
                                sbhtml.Append(modelt.default_value + "</textarea>");
                                sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                                #endregion
                                break;
                            case "images": //图片上传
                                goto case "single-text";
                            case "number": //数字
                                goto case "single-text";
                            case "checkbox": //复选框
                                sbhtml.Append("<div class=\"rule-single-checkbox\"><input id=\"field_control_" + modelt.name + "\" type=\"checkbox\" name=\"field_control_" + modelt.name + "\"/></div>");
                                sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                                break;
                            case "multi-radio": //多项单选
                                #region 多项单选
                                sbhtml.Append("<div class=\"rule-multi-radio\"><span id=\"field_control_\"" + modelt.name + ">");
                                //赋值选项
                                string[] valArr = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                                for (int i = 0; i < valArr.Length; i++)
                                {
                                    string[] valItemArr = valArr[i].Split('|');
                                    if (valItemArr.Length == 2)
                                    {
                                        string ck = "";
                                        if (modelt.default_value == valItemArr[1])
                                        {
                                            ck = "checked=\"checked\"";
                                        }
                                        sbhtml.Append("<input id=\"field_control_" + modelt.name + "0\" type=\"radio\" name=\"field_control_" + modelt.name + "0\" value=\"" + valItemArr[1] + "\" " + ck);
                                        sbhtml.Append("  /><label for=\"field_control_" + modelt.name + "0\">" + valItemArr[0] + "</label>");
                                    }
                                }
                                sbhtml.Append("</span></div>");
                                sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                                #endregion
                                break;
                            case "multi-checkbox": //多项多选
                                #region 多项多选
                                sbhtml.Append("<div class=\"rule-multi-checkbox\"><span id=\"field_control_" + modelt.name + "\">");
                                //赋值选项
                                string[] valArr2 = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                                for (int i = 0; i < valArr2.Length; i++)
                                {
                                    string[] valItemArr2 = valArr2[i].Split('|');
                                    if (valItemArr2.Length == 2)
                                    {
                                        sbhtml.Append("<input id=\"field_control_" + modelt.name + i.ToString() + "\" type=\"checkbox\" name=\"field_control_" + modelt.name + i.ToString() + "\"  value=\"" + valItemArr2[1] + "\" ");
                                        sbhtml.Append(" /><label for=\"field_control_" + modelt.name + i.ToString() + "\">" + valItemArr2[0] + "</label>");
                                    }
                                }
                                sbhtml.Append("</span></div>");
                                sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                                #endregion
                                break;
                        }
                        sbhtml.Append("</dd></dl><dl>");
                        sbhtml.Append("<!--submits Plugin by http://www.chenpan.com.cn -->");
                    }
                }
            }


           html_fields= sbhtml.ToString();
        }
        #endregion


        #region 前台显示其它扩展字段=========================
        public  string ShowOtherField(int category_id,int submits_id)
        {
            StringBuilder sbhtml = new StringBuilder();
            string strwhere = "";
            if (category_id != 0)
            {
                BLL.submits_category bll_c = new BLL.submits_category();
                string ids =bll_c.GetIds(category_id);
                if(ids==""){
                    return "";
                }
                strwhere = " and id in(" + ids + ")";
              
            }

            List<Model.submits_field> ls = new BLL.submits_field().GetModelList("is_sys=1" + strwhere);
            foreach (Model.submits_field modelt in ls)
            {

                //sbhtml.Append("<dl>");
                //sbhtml.Append("<dt>" + modelt.title + "：</dt>");
               // sbhtml.Append("<dd>");
                sbhtml.Append("<span > " + modelt.title + " : ");
                try
                {
                    sbhtml.Append(DTcms.DBUtility.DbHelperMySql.GetSingle("select " + modelt.name + " from dt_submits_value where submits_id=" + submits_id).ToString());
                }
                catch { }
                sbhtml.Append(" </span></br>");
                // sbhtml.Append("</dd></dl><dl>");
                // sbhtml.Append("<!--submits Plugin by http://www.chenpan.com.cn -->");
            }
            return sbhtml.ToString();
        }
        #endregion





    }
}
