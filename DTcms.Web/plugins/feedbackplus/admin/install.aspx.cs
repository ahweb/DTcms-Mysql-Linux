using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.Plugin.FeedbackPlus.admin
{
    public partial class install : Web.UI.ManagePage
    {
        protected int property = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.property = DTRequest.GetQueryInt("property");

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("plugin_feedbackplus", DTEnums.ActionEnum.View.ToString()); //检查权限
                //赋值
                ShowInfo();
            }
        }

        #region 赋值操作=================================
        private void ShowInfo()
        {
            BLL.install bll = new BLL.install();
            Model.install model = bll.loadConfig("../config/install.config");

            rblBookMsg.SelectedValue = model.bookmsg.ToString();
            txtBookTemplet.Text = model.booktemplet;
            txtReceive.Text = model.receive;
        }
        #endregion

        #region 保存操作
        /// <summary>
        /// 保存配置信息
        /// </summary>
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("plugin_feedbackplus", DTEnums.ActionEnum.Instal.ToString()); //检查权限
            BLL.install bll = new BLL.install();
            Model.install model = bll.loadConfig("../config/install.config");
            try
            {
                model.bookmsg = Utils.StrToInt(rblBookMsg.SelectedValue, 0);
                model.booktemplet = txtBookTemplet.Text.Trim() ;
                model.receive = txtReceive.Text.Trim();

                bll.saveConifg(model, "../config/install.config");
                AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "修改留言插件配置信息"); //记录日志
                JscriptMsg("修改留言插件配置信息成功！", "index.aspx");
            }
            catch
            {
                JscriptMsg("文件写入失败，请检查文件夹权限！", "");
            }
        }
        #endregion
    }
}