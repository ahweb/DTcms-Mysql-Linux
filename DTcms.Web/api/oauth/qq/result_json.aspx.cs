using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;

namespace DTcms.Web.api.oauth.qq
{
    public partial class result_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string oauth_access_token = string.Empty;
            string oauth_openid = string.Empty;
            string oauth_name = string.Empty;

            if (Session["oauth_name"] == null || Session["oauth_access_token"] == null || Session["oauth_openid"] == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，Access Token已过期或不存在！\"}");
                return;
            }
            oauth_name = Session["oauth_name"].ToString();
            oauth_access_token = Session["oauth_access_token"].ToString();
            oauth_openid = Session["oauth_openid"].ToString();
            Dictionary<string, object> dic = qq_helper.get_user_info(oauth_access_token, oauth_openid);
            if (dic == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
                return;
            }
            try
            {
                if (dic["ret"].ToString() != "0")
                {
                    Response.Write("{\"ret\":\"" + dic["ret"].ToString() + "\", \"msg\":\"出错信息:" + dic["msg"].ToString() + "！\"}");
                    return;
                }
                StringBuilder str = new StringBuilder();
                str.Append("{");
                str.Append("\"ret\": \"" + dic["ret"].ToString() + "\", ");
                str.Append("\"msg\": \"" + dic["msg"].ToString() + "\", ");
                str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
                str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
                str.Append("\"oauth_openid\": \"" + oauth_openid + "\", ");
                str.Append("\"nick\": \"" + dic["nickname"].ToString() + "\", ");
                str.Append("\"avatar\": \"" + dic["figureurl_qq_2"].ToString() + "\", ");
                str.Append("\"sex\": \"" + dic["gender"].ToString() + "\", ");
                str.Append("\"birthday\": \"\"");
                str.Append("}");
                Response.Write(str.ToString());
            }
            catch
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出错啦，无法获取授权用户信息！\"}");
            }
            return;
        }
    }
}