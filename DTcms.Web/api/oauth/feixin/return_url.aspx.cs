using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth.feixin
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
            Dictionary<string, object> dic1 = feixin_helper.get_access_token(code);
            if (dic1 == null || !dic1.ContainsKey("access_token"))
            {
                Response.Write("出错了，无法获取Access Token，请检查App Key是否正确！");
                return;
            }
            access_token = dic1["access_token"].ToString();
            expires_in = dic1["expires_in"].ToString();
            //第二步：通过Access Token来获取用户的ID
            Dictionary<string, object> dic2 = feixin_helper.get_info(access_token);
            if (dic2 == null || !dic2.ContainsKey("userId"))
            {
                Response.Write("出错啦，无法获取用户授权uid！");
                return;
            }
            openid = dic2["userId"].ToString();
            //储存获取数据用到的信息
            Session["oauth_name"] = "feixin";
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //第三步：跳转到指定页面
            Response.Redirect(new Web.UI.BasePage().linkurl("oauth_login"));
            return;

        }
    }
}