using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.Common;

namespace DTcms.Web.admin.settings
{
    public partial class nav_list : Web.UI.ManagePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }

        //数据绑定
        private void RptBind()
        {
            BLL.navigation bll = new BLL.navigation();
            DataTable dt = bll.GetList(0, DTEnums.NavigationEnum.System.ToString());
            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }

        //美化列表
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                Literal LitFirst = (Literal)e.Item.FindControl("LitFirst");
                HiddenField hidLayer = (HiddenField)e.Item.FindControl("hidLayer");
                string LitStyle = "<span style=\"display:inline-block;width:{0}px;\"></span>{1}{2}";
                string LitImg1 = "<span class=\"folder-open\"></span>";
                string LitImg2 = "<span class=\"folder-line\"></span>";

                int classLayer = Convert.ToInt32(hidLayer.Value);
                if (classLayer == 1)
                {
                    LitFirst.Text = LitImg1;
                }
                else
                {
                    LitFirst.Text = string.Format(LitStyle, (classLayer - 2) * 24, LitImg2, LitImg1);
                }
            }
        }

        //保存排序
        protected void btnSave_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Edit.ToString()); //检查权限
            BLL.navigation bll = new BLL.navigation();
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
            AddAdminLog(DTEnums.ActionEnum.Edit.ToString(), "保存导航排序"); //记录日志
            JscriptMsg("保存排序成功！", "nav_list.aspx");
        }

        //删除导航
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("sys_navigation", DTEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.navigation bll = new BLL.navigation();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                int id = Convert.ToInt32(((HiddenField)rptList.Items[i].FindControl("hidId")).Value);
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Delete(id);
                }
            }
            AddAdminLog(DTEnums.ActionEnum.Delete.ToString(), "删除导航菜单"); //记录日志
            JscriptMsg("删除数据成功！", "nav_list.aspx", "parent.loadMenuTree");
        }

    }
}