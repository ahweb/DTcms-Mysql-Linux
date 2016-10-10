using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth.sina
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
            string client_id = string.Empty;
            string openid = string.Empty;

            if (Session["oauth_state"] == null || Session["oauth_state"].ToString() == "" || state != Session["oauth_state"].ToString())
            {
                Response.Write("出错啦，state未初始化！");
                return;
            }
            
            //第一步：获取Access Token
            Dictionary<string, object> dic = sina_helper.get_access_token(code);
            if (dic == null || !dic.ContainsKey("access_token"))
            {
                Response.Write("出错了，无法获取Access Token，请检查App Key是否正确！");
                return;
            }

            access_token = dic["access_token"].ToString();
            expires_in = dic["expires_in"].ToString();
            openid = dic["uid"].ToString();
            //储存获取数据用到的信息
            Session["oauth_name"] = "sina";
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //第二步：跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().linkurl("oauth_login"));
            return;

        }
    }
}