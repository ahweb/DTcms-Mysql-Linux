using System;
using System.Collections.Generic;
using System.Text;
using DTcms.Common;

namespace DTcms.API.OAuth
{
    public class taobao_helper
    {
        public taobao_helper()
        { }

        /// <summary>
        /// 取得Access Token
        /// </summary>
        /// <param name="code">临时Authorization Code</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_access_token(string code)
        {
            //获得配置信息
            oauth_config config = oauth_helper.get_config("taobao");
            string send_url = "https://oauth.taobao.com/token";
            string param= "grant_type=authorization_code&code=" + code + "&client_id=" + config.oauth_app_id + "&client_secret=" + config.oauth_app_key + "&redirect_uri=" + Utils.UrlEncode(config.return_uri);
            //发送并接受返回值
            string result = Utils.HttpPost(send_url, param);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                return dic;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 获取登录用户自己的详细信息
        /// </summary>
        /// <param name="access_token">临时的Access Token</param>
        /// <returns>Dictionary</returns>
        public static Dictionary<string, object> get_info(string access_token, string fields)
        {
            string send_url = "https://eco.taobao.com/router/rest?access_token=" + access_token + "&method=taobao.user.buyer.get&format=json&v=2.0&fields=" + fields;
            //发送并接受返回值
            string result = Utils.HttpGet(send_url);
            if (result.Contains("error"))
            {
                return null;
            }
            try
            {
                Dictionary<string, object> dic = JsonHelper.DataRowFromJSON(result);
                if (dic.Count > 0)
                {
                    return dic;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

    }
}
