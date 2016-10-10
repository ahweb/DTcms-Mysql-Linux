using System;
using System.Xml;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.API.OAuth
{
    public class oauth_helper
    {
        public oauth_helper()
        { }

        /// <summary>
        /// 获取OAuth配置信息
        /// </summary>
        /// <param name="oauth_name"></param>
        public static oauth_config get_config(string oauth_name)
        {
            //读取接口配置信息
            Model.user_oauth_app model = new BLL.user_oauth_app().GetModel(oauth_name);
            if (model != null)
            {
                //读取站点配置信息
                Model.siteconfig siteConfig = new BLL.siteconfig().loadConfig();
                //赋值
                oauth_config config = new oauth_config();
                config.oauth_name = model.api_path.Trim();
                config.oauth_app_id = model.app_id.Trim();
                config.oauth_app_key = model.app_key.Trim();
                config.return_uri = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + siteConfig.webpath + "api/oauth/" + model.api_path + "/return_url.aspx";
                return config;
            }
            return null;
        }
    }
}
