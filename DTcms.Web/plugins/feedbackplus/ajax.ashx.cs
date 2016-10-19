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

namespace DTcms.Web.Plugin.FeedbackPlus
{
    /// <summary>
    /// 管理后台AJAX处理页
    /// </summary>
    public class ajax : IHttpHandler, IRequiresSessionState
    {
        DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig();
        Model.install config = new BLL.install().loadConfig("config/install.config");

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "add": //发布留言
                    feedbackplus_add(context);
                    break;
            }

        }

        #region 发布留言================================
        private void feedbackplus_add(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.feedbackplus bll = new BLL.feedbackplus();
            Model.feedbackplus model = new Model.feedbackplus();

            string _site_path = DTRequest.GetQueryString("site");
            string _code = DTRequest.GetFormString("txtCode");
            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            string _user_name = DTRequest.GetFormString("txtUserName");
            string _user_tel = DTRequest.GetFormString("txtUserTel");
            string _user_qq = DTRequest.GetFormString("txtUserQQ");
            string _user_email = DTRequest.GetFormString("txtUserEmail");

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
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，验证码已过期！\"}");
                return;
            }
            if (_code.ToLower() != (context.Session[DTKeys.SESSION_CODE].ToString()).ToLower())
            {
                context.Response.Write("{\"status\":0, \"msg\":\"验证码与系统的不一致！\"}");
                return;
            }
            if (string.IsNullOrEmpty(_content))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入留言的内容！\"}");
                return;
            }
            model.site_path = Utils.DropHTML(_site_path);
            model.title = Utils.DropHTML(_title);
            model.content = Utils.ToHtml(_content);
            model.user_name = Utils.DropHTML(_user_name);
            model.user_tel = Utils.DropHTML(_user_tel);
            model.user_qq = Utils.DropHTML(_user_qq);
            model.user_email = Utils.DropHTML(_user_email);
            model.add_time = DateTime.Now;
            model.is_lock = 1; //不需要审核，请改为0
            if (bll.Add(model) > 0)
            {
                //是否开启通知功能
                if (config.bookmsg > 0 && config.receive != "")
                {
                    switch (config.bookmsg)
                    {
                        case 1:
                            DTcms.Model.sms_template smsModel = new DTcms.BLL.sms_template().GetModel(config.booktemplet); //取得短信内容
                            if (smsModel != null)
                            {
                                //替换模板内容
                                string smstxt = smsModel.content;
                                smstxt = smstxt.Replace("{webname}", siteConfig.webname);
                                smstxt = smstxt.Replace("{webtel}", siteConfig.webtel);
                                smstxt = smstxt.Replace("{weburl}", siteConfig.weburl);
                                smstxt = smstxt.Replace("{username}", model.user_name);
                                smstxt = smstxt.Replace("{usertel}", model.user_tel);
                                smstxt = smstxt.Replace("{userqq}", model.user_qq);
                                smstxt = smstxt.Replace("{useremail}", model.user_email);
                                smstxt = smstxt.Replace("{usertitle}", model.title);
                                smstxt = smstxt.Replace("{usercontent}", model.content);
                                //发送短信
                                string tipMsg = string.Empty;
                                bool result = new DTcms.BLL.sms_message().Send(config.receive, smstxt, 1, out tipMsg);
                                //if (!result)
                                //{
                                //    LogHelper.WriteLog("手机信息发送失败!");
                                //}
                            }
                            break;
                        case 2:
                            //获得邮件内容
                            DTcms.Model.mail_template mailModel = new DTcms.BLL.mail_template().GetModel(config.booktemplet);
                            if (mailModel != null)
                            {
                                //替换模板内容
                                string titletxt = mailModel.maill_title;
                                string bodytxt = mailModel.content;

                                titletxt = titletxt.Replace("{webname}", siteConfig.webname);
                                titletxt = titletxt.Replace("{username}", model.user_name);

                                bodytxt = bodytxt.Replace("{webname}", siteConfig.webname);
                                bodytxt = bodytxt.Replace("{webtel}", siteConfig.webtel);
                                bodytxt = bodytxt.Replace("{weburl}", siteConfig.weburl);

                                bodytxt = bodytxt.Replace("{username}", model.user_name);
                                bodytxt = bodytxt.Replace("{usertel}", model.user_tel);
                                bodytxt = bodytxt.Replace("{userqq}", model.user_qq);
                                bodytxt = bodytxt.Replace("{useremail}", model.user_email);
                                bodytxt = bodytxt.Replace("{usertitle}", model.title);
                                bodytxt = bodytxt.Replace("{usercontent}", model.content);
                                //循环发送
                                string[] emailArr = config.receive.Split(',');
                                foreach (string email in emailArr)
                                {
                                    if (DTcms.Common.Utils.IsValidEmail(email))
                                    {
                                        //发送邮件
                                        try
                                        {
                                            DTMail.sendMail(siteConfig.emailsmtp, siteConfig.emailssl,
                                                siteConfig.emailusername,
                                                DESEncrypt.Decrypt(siteConfig.emailpassword, siteConfig.sysencryptstring),
                                                siteConfig.emailnickname,
                                                siteConfig.emailfrom,
                                                email,
                                                titletxt, bodytxt);
                                        }
                                        catch
                                        {
                                            //LogHelper.WriteLog("邮件发送失败!");
                                        }
                                    }
                                }
                            }
                            break;
                    }
                }
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，留言提交成功！\"}");
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