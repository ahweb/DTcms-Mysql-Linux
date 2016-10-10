using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth.renren
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //获得配置信息
            oauth_config config = oauth_helper.get_config("renren");
            if (config == null)
            {
                Response.Write("出错了，您尚未配置QQ互联的API信息！");
                return;
            }
            string state = Guid.NewGuid().ToString().Replace("-", "");
            Session["oauth_state"] = state;
            string send_url = "https://graph.renren.com/oauth/authorize?response_type=code&client_id=" + config.oauth_app_id + "&state=" + state + "&redirect_uri=" + Utils.UrlEncode(config.return_uri) + "&scope=read_user_share read_user_feed";
            //开始发送
            Response.Redirect(send_url);
        }
    }
}