using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DTcms.Common;

namespace DTcms.Web.Plugin.submits
{
    /// <summary>
    /// 管理后台AJAX处理页
    /// </summary>
    public class ajax : IHttpHandler, IRequiresSessionState
    {
       DTcms.Model.siteconfig siteConfig = new DTcms.BLL.siteconfig().loadConfig();
       DTcms.Model.userconfig userConfig = new DTcms.BLL.userconfig().loadConfig();

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = DTRequest.GetQueryString("action");

            switch (action)
            {
                case "submits_field_validate": //验证扩展字段是否重复
                    submits_field_validate(context);
                    break;

                case "add": //提交表单
                    submits_add(context);
                    break;
                case "add_mail": //提交表单并且发送邮件
                    submits_add_mail(context);
                    break;
                case "mobile_add": //提交表单
                    msubmits_add(context);
                    break;
                case "mobile_add2": //提交表单
                    msubmits_add2(context);
                    break;
                case "reply": //回复表单
                    submits_reply(context);
                    break; 
                default:
                    context.Response.Write("{\"status\":0, \"msg\":\"请检查请求参数！\"}");
                    break;

            }

        }

        #region 提交表单================================
        private void submits_add(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.submits bll = new BLL.submits();
            Model.submits model = new Model.submits();

            string _code = DTRequest.GetFormString("txtCode");
            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            string _user_name = DTRequest.GetFormString("txtUserName");
            string _user_tel = DTRequest.GetFormString("txtUserTel");
            string _user_qq = DTRequest.GetFormString("txtUserQQ");
            string _user_email = DTRequest.GetFormString("txtUserEmail");
            string _category_id = DTRequest.GetFormIntValue("category_id", 0).ToString();

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
            //if (string.IsNullOrEmpty(_content))
            //{
            //    context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入留言的内容！\"}");
            //    return;
            //}

            model.title = Utils.DropHTML(_title);
            model.content = Utils.ToHtml(_content);
            model.user_name = Utils.DropHTML(_user_name);
            model.user_tel = Utils.DropHTML(_user_tel);
            // model.user_qq = Utils.DropHTML(_user_qq);     
            model.user_qq = _category_id;//保存表单类别
            model.user_email = Utils.DropHTML(_user_email);
            model.add_time = DateTime.Now;
            model.is_lock = 1; //不需要审核，请改为0
            model.fields = SetFieldValues(); //扩展字段赋值

            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，提交成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        #region 提交表单并且发送邮件================================
        private void submits_add_mail(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.submits bll = new BLL.submits();
            Model.submits model = new Model.submits();

            string _code = DTRequest.GetFormString("txtCode");
            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            string _user_name = DTRequest.GetFormString("txtUserName");
            string _user_tel = DTRequest.GetFormString("txtUserTel");
            string _user_qq = DTRequest.GetFormString("txtUserQQ");
            string _user_email = DTRequest.GetFormString("txtUserEmail");
            string _category_id = DTRequest.GetFormIntValue("category_id", 0).ToString();

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
            //if (string.IsNullOrEmpty(_content))
            //{
            //    context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入留言的内容！\"}");
            //    return;
            //}

            model.title = Utils.DropHTML(_title);
            model.content = Utils.ToHtml(_content);
            model.user_name = Utils.DropHTML(_user_name);
            model.user_tel = Utils.DropHTML(_user_tel);
            // model.user_qq = Utils.DropHTML(_user_qq);     
            model.user_qq = _category_id;//保存表单类别
            model.user_email = Utils.DropHTML(_user_email);
            model.add_time = DateTime.Now;
            model.is_lock = 1; //不需要审核，请改为0
            model.fields = SetFieldValues(); //扩展字段赋值
            int a=bll.Add(model);
            if ( a> 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，提交成功！\"}");
                //发送邮件
                try
                {
                    string title =siteConfig.webname+" 【"+model.title+"-"+DateTime.Now+"】";
                     StringBuilder sb = new StringBuilder();
                     sb.Append("<strong>"+model.user_name+"</strong>，您在"+siteConfig.webname+"提交的表单详情,<a href=\""+siteConfig.weburl+"/feedback_"+a+".html\">点击查看</a>");
                     sb.Append("<p><br />无法打开请复制 " + siteConfig.weburl + "/feedback_" + a + ".htm 粘贴在浏览器访问<br /></p>");
                     sb.Append("<div style=\"text-align:right;\">来自："+siteConfig.webname+" "+DateTime.Now+" </div>");
                    if (model.user_email != "")
                    {
                        //发送给用户
                        DTMail.sendMail(siteConfig.emailsmtp, siteConfig.emailssl,
                            siteConfig.emailusername,
                            DESEncrypt.Decrypt(siteConfig.emailpassword),
                            siteConfig.emailnickname,
                            siteConfig.emailfrom,
                            model.user_email,
                            title,sb.ToString());
                        StringBuilder sb2 = new StringBuilder();
                        sb2.Append("<strong>" + model.user_name + "</strong>，在" + siteConfig.webname + "提交的表单详情,<a href=\"" + siteConfig.weburl + "/feedback_" + a + ".html\">点击查看</a>");
                        sb2.Append("<p><br />无法打开请复制 " + siteConfig.weburl + "/feedback_" + a + ".htm 粘贴在浏览器访问<br /></p>");
                        sb2.Append("<div style=\"text-align:right;\">来自：" + siteConfig.webname + " " + DateTime.Now + " </div>");

                        //发送给系统管理员
                        DTMail.sendMail(siteConfig.emailsmtp, siteConfig.emailssl,
                           siteConfig.emailusername,
                           DESEncrypt.Decrypt(siteConfig.emailpassword),
                           siteConfig.emailnickname,
                           siteConfig.emailfrom,
                           siteConfig.webmail,
                           title, sb2.ToString());
                    }
                }
                catch
                {
                    
                }
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        #region 手机版提交表单无验证码================================
        private void msubmits_add(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.submits bll = new BLL.submits();
            Model.submits model = new Model.submits();

            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            string _user_name = DTRequest.GetFormString("txtUserName");
            string _user_tel = DTRequest.GetFormString("txtUserTel");
            string _user_qq = DTRequest.GetFormString("txtUserQQ");
            string _user_email = DTRequest.GetFormString("txtUserEmail");
            string _category_id = DTRequest.GetFormIntValue("category_id", 0).ToString();
            string _order_no = DTRequest.GetFormString("order_no").ToString();

            //if (string.IsNullOrEmpty(_content))
            //{
            //    context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入的内容！\"}");
            //    return;
            //}

            model.title = Utils.DropHTML(_title);
            model.content = Utils.ToHtml(_content);
            model.user_name = Utils.DropHTML(_user_name);
            model.user_tel = Utils.DropHTML(_user_tel);
            // model.user_qq = Utils.DropHTML(_user_qq);     
            model.user_qq = _category_id;//保存表单类别
            model.user_email = Utils.DropHTML(_user_email);
            model.add_time = DateTime.Now;
            model.user_tel = Utils.DropHTML(_order_no);//保存订单号
            model.is_lock = 1; //不需要审核，请改为0
            model.fields = SetFieldValues(); //扩展字段赋值

            DTcms.Model.users u_model = new DTcms.Web.UI.BasePage().GetUserInfo();
            if (u_model == null) {

                   context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请登录！\"}");
                   return;

            }
            if (_title.Trim() == "")
            {

                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入内容！\"}");
                return;

            }

           
            model.user_name = u_model.user_name;


            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，提交成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }

        private void msubmits_add2(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.submits bll = new BLL.submits();
            Model.submits model = new Model.submits();

            string _title = DTRequest.GetFormString("txtTitle");
            string _content = DTRequest.GetFormString("txtContent");
            string _user_name = DTRequest.GetFormString("txtUserName");
            string _user_tel = DTRequest.GetFormString("txtUserTel");
            string _user_qq = DTRequest.GetFormString("txtUserQQ");
            string _user_email = DTRequest.GetFormString("txtUserEmail");
            string _category_id = DTRequest.GetFormIntValue("category_id", 0).ToString();
            string _order_no = DTRequest.GetFormString("order_no").ToString();

            //if (string.IsNullOrEmpty(_content))
            //{
            //    context.Response.Write("{\"status\": 0, \"msg\": \"对不起，请输入的内容！\"}");
            //    return;
            //}

            model.title = Utils.DropHTML(_title);
            model.content = Utils.ToHtml(_content);
            model.user_name = Utils.DropHTML(_user_name);
            model.user_tel = Utils.DropHTML(_user_tel);
            // model.user_qq = Utils.DropHTML(_user_qq);     
            model.user_qq = _category_id;//保存表单类别
            model.user_email = Utils.DropHTML(_user_email);
            model.add_time = DateTime.Now;
            model.user_tel = Utils.DropHTML(_order_no);//保存订单号
            model.is_lock = 1; //不需要审核，请改为0
            model.fields = SetFieldValues(); //扩展字段赋值

            if (bll.Add(model) > 0)
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，提交成功！\"}");
                return;
            }
            context.Response.Write("{\"status\": 0, \"msg\": \"对不起，保存过程中发生错误！\"}");
            return;
        }
        #endregion

        #region 回复表单================================
        private void submits_reply(HttpContext context)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.submits bll = new BLL.submits();
            Model.submits model = new Model.submits();

            int _id = DTRequest.GetFormInt("id",0);
            
            string _content = DTRequest.GetFormString("txtContent");
            if (string.IsNullOrEmpty(_content)) {
                context.Response.Write("{\"status\":0, \"msg\":\"请输入回复内容！\"}");
            }

            //
            DTcms.Model.users u_model = new DTcms.Web.UI.BasePage().GetUserInfo();
            if(!bll.Exists(_id)){

                context.Response.Write("{\"status\":0, \"msg\":\"记录不存在！\"}");
                return;
            }
            model = bll.GetModel(_id);
            if (u_model==null||u_model.group_id!=2)
            {
                context.Response.Write("{\"status\":0, \"msg\":\"对不起，您没有权限！\"}");
                return;
            }
            model.user_email = u_model.user_name;//回复会员
           
            model.reply_content = Utils.ToHtml(_content);
            model.reply_time = DateTime.Now;


            if (bll.Update(model))
            {
                context.Response.Write("{\"status\": 1, \"msg\": \"恭喜您，回复成功！\"}");
                return;
            }
            else
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"对不起，回复过程中发生错误！\"}");
                return;
            }
        }
        #endregion

        #region 验证扩展字段是否重复============================
        private void submits_field_validate(HttpContext context)
        {
            string column_name = DTRequest.GetString("param");
            if (string.IsNullOrEmpty(column_name))
            {
                context.Response.Write("{ \"info\":\"名称不可为空\", \"status\":\"n\" }");
                return;
            }
            BLL.submits_field bll = new BLL.submits_field();
            if (bll.Exists(column_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion
        
        #region 创建其它扩展字段=========================
        /*
        private void CreateOtherField(HttpContext context)
        {
            StringBuilder sbhtml = new StringBuilder();
            List<Model.submits_field> ls = new BLL.submits_field().GetModelList("is_sys=1");
            foreach (Model.submits_field modelt in ls)
            {
                
                sbhtml.Append("<dl>");
                sbhtml.Append("<dt>"+modelt.title+"</dt>");
                sbhtml.Append("<dd>");
                switch (modelt.control_type)
                {
                    case "single-text":
                        #region  单行文本
                        if (modelt.control_type == "single-text" || modelt.control_type == "number" || modelt.control_type == "images") //单行
                        {
                           sbhtml.Append("<input name=\"field_control_"+ modelt.name+"\" id=\"field_control_"+modelt.name+"\" ");
                           if (modelt.control_type == "number") //数字
                           {
                              sbhtml.Append("class=\"input small\"");
                           }
                           else if (modelt.control_type == "imgage")
                           {
                              sbhtml.Append(" class=\"input normal upload-path\"" );                          
                           }
                           else
                           {
                               sbhtml.Append("class=\"input normal\"");
                           }
                            //是否密码框
                            if (modelt.is_password == 1)
                            {
                                sbhtml.Append("type=\"password\"");
                            }
                            else{
                                sbhtml.Append("type=\"text\" ");
                            }

                            #region 验证信息
                            //验证提示信息
                            if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                            {
                                sbhtml.Append("tipmsg=\""+ modelt.valid_tip_msg+"\" ");
                            }
                            //验证失败提示信息
                            if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                            {
                                sbhtml.Append("errormsg=\""+ modelt.valid_error_msg+"\" ");
                            }
                            //验证表达式
                            if (!string.IsNullOrEmpty(modelt.valid_pattern))
                            {
                                sbhtml.Append("datatype=\""+ modelt.valid_pattern+"\"");
                                sbhtml.Append("sucmsg=\"\"");
                            }
                            #endregion

                        //设置默认值
                        sbhtml.Append(" value=\"" + modelt.default_value + "\"/>");
                        }
                        if (modelt.control_type == "images")//如果是图片增加图片上传按钮
                        {
                            sbhtml.Append("<div class=\"upload-box upload-img\" style=\"margin-left:4px;\"></div>");
                        }

                        //创建一个提示
                        sbhtml.Append("<span class=\"Validform_checktip\">"+ modelt.valid_tip_msg+"</span>");
                        #endregion
                      break;
                    case "multi-text": //多行文本
                        #region 多行文本
                        sbhtml.Append("<textarea name=\"field_control_"+modelt.name+"\" id=\"field_control_"+modelt.name+"\" rows=\"2\" cols=\"20\" ");
                        #region 验证信息
                        //验证提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            sbhtml.Append("tipmsg=\"" + modelt.valid_tip_msg + "\" ");
                        }
                        //验证失败提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                        {
                            sbhtml.Append("errormsg=\"" + modelt.valid_error_msg + "\" ");
                        }
                        //验证表达式
                        if (!string.IsNullOrEmpty(modelt.valid_pattern))
                        {
                            sbhtml.Append("datatype=\"" + modelt.valid_pattern + "\"");
                            sbhtml.Append("sucmsg=\"\"");
                        }
                        #endregion                  
                        sbhtml.Append(" class=\"input\">");                     
                        sbhtml.Append(modelt.default_value + "</textarea>");
                        sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                        #endregion
                      break;
                    case "editor": //编辑器
                        #region 编辑器
                        sbhtml.Append("<textarea name=\"field_control_"+modelt.name+"\" id=\"field_control_"+modelt.name+"\" style=\"visibility:hidden;\"");
                        #region 验证信息
                        //验证提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_tip_msg))
                        {
                            sbhtml.Append("tipmsg=\"" + modelt.valid_tip_msg + "\" ");
                        }
                        //验证失败提示信息
                        if (!string.IsNullOrEmpty(modelt.valid_error_msg))
                        {
                            sbhtml.Append("errormsg=\"" + modelt.valid_error_msg + "\" ");
                        }
                        //验证表达式
                        if (!string.IsNullOrEmpty(modelt.valid_pattern))
                        {
                            sbhtml.Append("datatype=\"" + modelt.valid_pattern + "\"");
                            sbhtml.Append("sucmsg=\"\"");
                        }
                        #endregion

                        if (modelt.editor_type == 1)
                        {
                            sbhtml.Append(" class=\"editor-mini\">");
                        }
                        else
                        {
                            sbhtml.Append(" class=\"editor\">");
                        }
                        sbhtml.Append(modelt.default_value + "</textarea>");
                        sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                        #endregion
                      break;
                    case "images": //图片上传
                      goto case "single-text";
                    case "number": //数字
                      goto case "single-text";
                    case "checkbox": //复选框
                      sbhtml.Append("<div class=\"rule-single-checkbox\"><input id=\"field_control_"+modelt.name+"\" type=\"checkbox\" name=\"field_control_"+modelt.name+"\" /></div>");
                      sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                        break;
                    case "multi-radio": //多项单选
                        #region 多项单选
                        sbhtml.Append("<div class=\"rule-multi-radio\"><span id=\"field_control_mashan\">");
                       //赋值选项
                       string[] valArr = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                       for (int i = 0; i < valArr.Length; i++)
                       {
                           string[] valItemArr = valArr[i].Split('|');
                           if (valItemArr.Length == 2)
                           {
                               string ck = "";
                               if (modelt.default_value == valItemArr[1]) {
                                   ck = "checked=\"checked\"";
                               }
                               sbhtml.Append("<input id=\"field_control_" + modelt.name + "_" + i + "\" type=\"radio\" name=\"field_control_" + modelt.name + "\" value=\"" + valItemArr[1] + "\" " + ck + " /><label for=\"field_control_" + modelt.name + "_" + i + "\">" + valItemArr[1] + "</label>");
                           }
                       }
                       sbhtml.Append("</span></div>");
                       sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                        #endregion
                        break;
                    case "multi-checkbox": //多项多选
                        #region 多项多选
                        sbhtml.Append("<div class=\"rule-multi-checkbox\"><span id=\"field_control_" + modelt.name + "\">");
                        //赋值选项
                        string[] valArr2 = modelt.item_option.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
                        for (int i = 0; i < valArr2.Length; i++)
                        {
                            string[] valItemArr2 = valArr2[i].Split('|');
                            if (valItemArr2.Length == 2)
                            {
                                sbhtml.Append("<input id=\"field_control_"+modelt.name+"_"+i+"\" type=\"checkbox\" name=\"field_control_"+modelt.name+"$"+i+"\" /><label for=\"field_control_"+modelt.name+"_"+i+"\">"+valItemArr2[0]+"</label>");

                            }
                        }
                        sbhtml.Append("</span></div>");
                        sbhtml.Append("<span class=\"Validform_checktip\">" + modelt.valid_tip_msg + "</span>");
                        #endregion
                        break;
                }
                sbhtml.Append("</dd></dl><dl>");
            }
            context.Response.Write(sbhtml.ToString());
        }
         * */
        #endregion

        #region 扩展字段赋值=============================
        private Dictionary<string, string> SetFieldValues()
        {
            DataTable dt = new BLL.submits_field().GetList("is_sys=1").Tables[0];
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (DataRow dr in dt.Rows)
            {
                //查找相应的控件
                switch (dr["control_type"].ToString())
                {
                    case "single-text": //单行文本
                        dic.Add(dr["name"].ToString(), DTRequest.GetFormString("field_control_" + dr["name"].ToString()).Trim());
                        break;
                    case "multi-text": //多行文本
                        goto case "single-text";
                    case "editor": //编辑器
                        dic.Add(dr["name"].ToString(), DTRequest.GetFormString("field_control_" + dr["name"].ToString()).Trim());
                        break;
                    case "images": //图片上传
                        goto case "single-text";
                    case "number": //数字
                        goto case "single-text";
                    case "checkbox": //复选框
                        string f_value=DTRequest.GetFormString("field_control_" + dr["name"].ToString()).Trim();
                        if (f_value == "on")
                        {
                            dic.Add(dr["name"].ToString(), "1");
                        }
                        else {
                            dic.Add(dr["name"].ToString(), "0");
                        }
                           
                        break;
                    case "multi-radio": //多项单选                    
                        dic.Add(dr["name"].ToString(), DTRequest.GetFormString("field_control_" + dr["name"].ToString()+"0").Trim());
                        break;
                    case "multi-checkbox": //多项多选
                        //CheckBoxList cblControl = FindControl("field_control_" + dr["name"].ToString()) as CheckBoxList;
                        string value = DBUtility.DbHelperMySql.GetSingle("select item_option from dt_submits_field where name = '"+dr["name"].ToString()+"'").ToString();
                        string[] valArr2 = value.Split('|');
                        value = "";
                        for (int i = 0; i < valArr2.Length; i++)
                        {
                            string value_ = DTRequest.GetFormString("field_control_" + dr["name"].ToString() + i.ToString()).Trim();
                            if (value_ != "")
                            {
                                value = value_ + "," + value;
                            }
                        }
                        value = Utils.DelLastComma(value);
                        dic.Add(dr["name"].ToString(), value);
                        break;
                }
            }
            return dic;
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