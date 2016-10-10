using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth.renren
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得返回参数
            string state = DTRequest.GetQueryString("state");
            string code = DTRequest.GetQueryString("code");

            string access_token = string.Empty;
            string expires_in = string.Empty;
            string openid = string.Empty;

            if (Session["oauth_state"] == null || Session["oauth_state"].ToString() == "" || state != Session["oauth_state"].ToString())
            {
                Response.Write("出错啦，state未初始化！");
                return;
            }
            if (string.IsNullOrEmpty(code))
            {
                Response.Write("授权被取消，相关信息：" + DTRequest.GetQueryString("error"));
                return;
            }
            
            //获取Access Token
            Dictionary<string,object> dic = renren_helper.get_access_token(code);
            if (dic == null)
            {
                Response.Write("错误代码：，无法获取Access Token，请检查App Key是否正确！");
            }

            access_token = dic["access_token"].ToString();
            expires_in = dic["expires_in"].ToString();
            Dictionary<string, object> dic1 = dic["user"] as Dictionary<string, object>;
            openid = dic1["id"].ToString();
            //储存获取数据用到的信息
            Session["oauth_name"] = "renren";
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().linkurl("oauth_login"));
            return;

        }
    }
}