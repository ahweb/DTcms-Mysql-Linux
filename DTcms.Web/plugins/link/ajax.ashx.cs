using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using DTcms.Web.UI;
using DTcms.Common;

namespace DTcms.Web.Plugin.Link
{
    /// <summary>
    /// AJAX处理页
    /// </summary>
    public class ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = context.Request.Params["action"];

            switch (action)
            {
                case "add": //添加链接
                    link_add(context);
                    break;
            }

        }

        #region 添加友情链接================================
        private void link_add(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.link bll = new BLL.link();
            Model.link model = new Model.link();

            string _site_path = DTRequest.GetQueryString("site");
            string _code = DTRequest.GetFormString("txtCode");
            string _title =DTRequest.GetFormString("txtTitle");
            string _user_name = DTRequest.GetFormString("txtUserName");
            string _user_tel =DTRequest.GetFormString("txtUserTel");
            string _email = DTRequest.GetFormString("txtEmail");
            string _site_url = DTRequest.GetFormString("txtSiteUrl");
            string _img_url = DTRequest.GetFormString("txtImgUrl");

            //检查站点目录
            if (string.IsNullOrEmpty(_site_path))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，网站传输参数有误！\"}");
                return;
            }
            //校检验证码
            if (string.IsNullOrEmpty(_code))
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，请输入验证码！\"}");
                return;
            }
            if (context.Session[DTKeys.SESSION_CODE] == null)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，系验证码已过期！\"}");
                return;
            }
            if (_code.ToLower() != (context.Session[DTKeys.SESSION_CODE].ToString()).ToLower())
            {
                context.Response.Write("{\"status\":0, \"msg\":\"验证码与系统的不一致！\"}");
                return;
            }
            if (string.IsNullOrEmpty(_title))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入网站标题！\"}");
                return;
            }
            if (string.IsNullOrEmpty(_site_url))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入网站网址！\"}");
                return;
            }
            model.site_path = Utils.DropHTML(_site_path);
            model.title = Utils.DropHTML(_title);
            model.is_lock = 1;
            model.is_red = 0;
            model.user_name = Utils.DropHTML(_user_name);
            model.user_tel = Utils.DropHTML(_user_tel);
            model.email = Utils.DropHTML(_email);
            model.site_url = Utils.DropHTML(_site_url);
            model.img_url = Utils.DropHTML(_img_url);
            model.is_image = 1;
            if (string.IsNullOrEmpty(model.img_url))
            {
                model.is_image = 0;
            }
            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，提交成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}