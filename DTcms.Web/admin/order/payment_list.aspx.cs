using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.order
{
    public partial class payment_list : Web.UI.ManagePage
    {
        protected string keywords = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("order_payment", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind("id>0" + CombSqlTxt(keywords), "sort_id asc,id desc");
            }
        }

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            txtKeywords.Text = this.keywords;
            BLL.payment bll = new BLL.payment();
            this.rptList.DataSource = bll.GetList(0, _strWhere, _orderby);
            this.rptList.DataBind();
        }
        #endregion

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and title like  '%" + _keywords + "%'");
            }

            return strTemp.ToString();
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("payment_list.aspx", "keywords={0}", txtKeywords.Text));
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("order_payment", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.payment bll = new BLL.payment();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                int sortId;
                if (!int.TryParse(((TextBox)rptList.Items[i].FindControl("txtSortId")).Text.Trim(), out sortId))
                {
                    sortId = 99;
                }
                bll.UpdateField(id, "sort_id=" + sortId.ToString());
            }
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存支付方式排序"); //记录日志
            JscriptMsg("保存排序成功！", Utils.CombUrlTxt("payment_list.aspx", "keywords={0}", this.keywords));
        }

    }
}