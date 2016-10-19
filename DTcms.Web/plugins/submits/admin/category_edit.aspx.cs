using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;
using System.Data;

namespace DTcms.Web.Plugin.submits.admin
{
    public partial class category_edit : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("传输参数不正确！", "back", "Error");
                    return;
                }
                if (!new BLL.submits_category().Exists(this.id))
                {
                    JscriptMsg("记录不存在或已被删除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                FieldBind();
                ChkAdminLevel("site_submits_category", DTEnums.ActionEnum.View.ToString()); //检查权限
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    txtTitle.Attributes.Add("onBlur", "change2cn(this.value, this.form.txtBuildPath)");
                    txtBuildPath.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=submits_category_validate");
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.submits_category bll = new BLL.submits_category();
            Model.submits_category model = bll.GetModel(_id);

            txtTitle.Text = model.title;
           // txtBuildPath.Text = model.build_path;
          //  txtBuildPath.Attributes.Add("ajaxurl", "../../tools/admin_ajax.ashx?action=submits_category_validate&old_build_path=" + Utils.UrlEncode(model.build_path));
            txtBuildPath.Focus(); //设置焦点，防止JS无法提交
           // txtDomain.Text = model.domain;
            txtSortId.Text = model.sort_id.ToString();
            if (model.is_default == 1)
            {
                cbIsDefault.Checked = true;
            }
            else
            {
                cbIsDefault.Checked = false;
            }
            if (model.field_ids!= "")
            {
                string[] ids_s = model.field_ids.Split(',');
                for (int i = 0; i < cblAttributeField.Items.Count; i++)
                {
                    //Model.channel_field modelt = model.channel_fields.Find(p => p.field_id == int.Parse(cblAttributeField.Items[i].Value)); //查找对应的字段ID
                    foreach(string str in ids_s){
                        if (int.Parse(cblAttributeField.Items[i].Value).ToString() == str)
                        {
                            cblAttributeField.Items[i].Selected = true;
                        }
                    }
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.submits_category model = new Model.submits_category();
            BLL.submits_category bll = new BLL.submits_category();

            model.title = txtTitle.Text.Trim();
         //   model.build_path = txtBuildPath.Text.Trim();
         //   model.domain = txtDomain.Text.Trim();
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            string ids = "";
            for (int i = 0; i < cblAttributeField.Items.Count; i++)
            {
                if (cblAttributeField.Items[i].Selected)
                {
                    //ls.Add(new Model.channel_field { field_id = Utils.StrToInt(cblAttributeField.Items[i].Value, 0) });
                    ids = ids + Utils.StrToInt(cblAttributeField.Items[i].Value, 0).ToString() + ",";
                }
            }
            if (ids.Length > 0)
            {
                model.field_ids = ids.Substring(0, ids.Length - 1);
            }
            if (cbIsDefault.Checked == true)
            {
                model.is_default = 1;
            }
            else
            {
                model.is_default = 0;
            }

            if (bll.Add(model) > 0)
            {
                //更新一下域名缓存
                CacheHelper.Remove(DTKeys.CACHE_SITE_HTTP_DOMAIN);
                AddAdminLog(DTEnums.ActionEnum.Add.ToString(), "添加表单分类:" + model.title); //记录日志
                return true;
            }
            
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.submits_category bll = new BLL.submits_category();
            Model.submits_category model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
           // model.build_path = txtBuildPath.Text.Trim();
           // model.domain = txtDomain.Text.Trim();
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            string ids = "";
            for (int i = 0; i < cblAttributeField.Items.Count; i++)
            {
                if (cblAttributeField.Items[i].Selected)
                {
                    //ls.Add(new Model.channel_field { field_id = Utils.StrToInt(cblAttributeField.Items[i].Value, 0) });
                    ids = ids + Utils.StrToInt(cblAttributeField.Items[i].Value, 0).ToString() + ",";
                }
            }
            if (ids.Length > 0)
            {
                model.field_ids = ids.Substring(0, ids.Length - 1);
            }
            else {
                model.field_ids = "";
            }
            if (cbIsDefault.Checked == true)
            {
                model.is_default = 1;
            }
            else
            {
                model.is_default = 0;
            }

            if (bll.Update(model))
            {
                //更新一下域名缓存
                CacheHelper.Remove(DTKeys.CACHE_SITE_HTTP_DOMAIN);
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改表单分类:" + model.title); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        #region 绑定扩展字段=============================
        private void FieldBind()
        {
            BLL.submits_field bll = new BLL.submits_field();
            DataTable dt = bll.GetList(0, "", "sort_id asc,id desc").Tables[0];

            this.cblAttributeField.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                this.cblAttributeField.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("site_submits_category", DTEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("修改表单分类成功！", "category_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("site_submits_category", DTEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    JscriptMsg("保存过程中发生错误！", "");
                    return;
                }
                JscriptMsg("添加表单分类成功！", "category_list.aspx");
            }
        }
    }
}