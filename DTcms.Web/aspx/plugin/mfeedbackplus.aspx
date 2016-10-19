<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.Plugin.FeedbackPlus.feedbackplus" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by DTcms Template Engine at 2016/10/19 17:16:11.
		本页面代码由DTcms模板引擎生成于 2016/10/19 17:16:11. 
	*/

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html>\r\n<!--HTML5 doctype-->\r\n<html>\r\n<head>\r\n<meta http-equiv=\"Content-type\" content=\"text/html; charset=utf-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0, maximum-scale=1.0, minimum-scale=1.0, user-scalable=0\">\r\n<meta name=\"apple-mobile-web-app-capable\" content=\"yes\" />\r\n<title>留言反馈 - ");
	templateBuilder.Append(Utils.ObjectToStr(config.webname));
	templateBuilder.Append("</title>\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/mobile");
	templateBuilder.Append("/jqmobi/css/icons.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/mobile");
	templateBuilder.Append("/jqmobi/css/af.ui.base.css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append("/templates/mobile");
	templateBuilder.Append("/css/style.css\" />\r\n<!--jqMobi主JS-->\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/mobile");
	templateBuilder.Append("/jqmobi/jq.appframework.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/mobile");
	templateBuilder.Append("/jqmobi/ui/appframework.ui.js\"></");
	templateBuilder.Append("script>\r\n<!--jqMobi插件-->\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/mobile");
	templateBuilder.Append("/jqmobi/plugins/jq.slidemenu.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/mobile");
	templateBuilder.Append("/js/base.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n	$(document).ready(function(e) {\r\n        $(\".page-list a\").attr(\"data-ignore\",true);\r\n		//初始化发表评论表单\r\n		AjaxInitForm('#feedbackplus_form', '#btnSubmit', 1, '#turl');\r\n    });\r\n</");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body>\r\n<div id=\"afui\">\r\n  <div id=\"content\">\r\n  \r\n    <!--留言反馈-->\r\n	<div id=\"mainPanel\" class=\"panel\" data-footer=\"main_footer\">\r\n      <header>\r\n        <a onclick=\"history.back(-1);\" class=\"backButton\">返回</a>\r\n        <h1>在线留言</h1>\r\n        <a onclick=\"$.ui.toggleSideMenu()\" class=\"menuButton\"></a>\r\n      </header>\r\n      \r\n      <div>\r\n        <ol class=\"comment-list\">\r\n          ");
	DataTable list = new DTcms.Web.Plugin.FeedbackPlus.feedbackplus().get_feedbackplus_list(10, page, "", out totalcount);

	string pagelist = get_page_link(10, page, totalcount, "mfeedbackplus", "__id__");

	foreach(DataRow dr in list.Rows)
	{

	templateBuilder.Append("\r\n          <li>\r\n            <div class=\"inner\">\r\n              <p>" + Utils.ObjectToStr(dr["content"]) + "</p>\r\n              <div class=\"meta\">\r\n                <span class=\"blue\">" + Utils.ObjectToStr(dr["user_name"]) + "</span>\r\n                <span class=\"time\">" + Utils.ObjectToStr(dr["add_time"]) + "</span>\r\n              </div>\r\n            </div>\r\n            ");
	if (Utils.ObjectToStr(dr["reply_content"])!="")
	{

	templateBuilder.Append("\r\n            <div class=\"answer\">\r\n              <div class=\"meta\">\r\n                <span class=\"time\">" + Utils.ObjectToStr(dr["reply_time"]) + "</span>\r\n                <span class=\"blue\">管理员回复：</span>\r\n              </div>\r\n              <p>" + Utils.ObjectToStr(dr["reply_content"]) + "</p>\r\n            </div>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n          </li>\r\n          ");
	}	//end for if

	if (list.Rows.Count<1)
	{

	templateBuilder.Append("\r\n          <div style=\"line-height:50px;text-align:center;\">暂无留言信息！</div>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </ol>\r\n      </div>\r\n      \r\n      <!--分页页码-->\r\n      <div class=\"page-list\" style=\"margin:5px 0;\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n      <!--/分页页码-->\r\n      \r\n      <!--页面底部导航-->\r\n      ");



	templateBuilder.Append("\r\n      <!--页面底部导航-->\r\n	</div>\r\n    <!--留言反馈-->\r\n    \r\n    <!--发表留言-->\r\n    <div id=\"addPanel\" class=\"panel\" data-footer=\"main_footer\">\r\n      <header>\r\n        <a onclick=\"history.back(-1);\" class=\"backButton\">返回</a>\r\n        <h1>发表留言</h1>\r\n        <a onclick=\"$.ui.toggleSideMenu()\" class=\"menuButton\"></a>\r\n      </header>\r\n      \r\n      <form id=\"feedbackplus_form\" name=\"feedbackplus_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/feedbackplus/ajax.ashx?action=add&site=");
	templateBuilder.Append(Utils.ObjectToStr(site.build_path));
	templateBuilder.Append("\">\r\n        <div class=\"form-box\">\r\n          <div><input id=\"txtUserName\" name=\"txtUserName\" type=\"text\" placeholder=\"输入用户昵称\"  datatype=\"*\" nullmsg=\"请输入用户昵称\" sucmsg=\" \" /></div>\r\n          <div><input id=\"txtUserTel\" name=\"txtUserTel\" type=\"text\" placeholder=\"输入联系电话\" datatype=\"*0-20\" sucmsg=\" \" /></div>\r\n          <div><input id=\"txtUserQQ\" name=\"txtUserQQ\" type=\"text\" placeholder=\"输入联系QQ\" datatype=\"*0-20\" sucmsg=\" \" /></div>\r\n          <div><input id=\"txtUserEmail\" name=\"txtUserEmail\" type=\"text\" placeholder=\"邮箱地址\" /></div>\r\n          <div><input id=\"txtTitle\" name=\"txtTitle\" type=\"text\" placeholder=\"输入留言标题\" datatype=\"*2-100\" nullmsg=\"请填写留言标题\" sucmsg=\" \" /></div>\r\n          <div><textarea id=\"txtContent\" name=\"txtContent\" rows=\"3\" placeholder=\"输入您要反馈的留言内容\" datatype=\"*\" nullmsg=\"请填写留言内容\" sucmsg=\" \"></textarea></div>\r\n          <div><input id=\"txtCode\" name=\"txtCode\" type=\"text\" placeholder=\"验证码\" style=\"width:100px;\" datatype=\"*\" nullmsg=\"请输入验证码\" sucmsg=\" \" />\r\n          <a id=\"verifyCode\" href=\"javascript:;\" onclick=\"ToggleCode(this, '/tools/verify_code.ashx');return false;\" style=\"display:inline-block;\"><img src=\"http://demo.dtcms.net/tools/verify_code.ashx\" width=\"80\" height=\"30\" style=\"vertical-align:middle;\" /> 看不清楚？</a>\r\n          </div>\r\n          <div><input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"发表留言\" class=\"btn orange full\" /></div>\r\n        </div>\r\n      </form>\r\n      <div style=\"padding-bottom:15px;\"></div>\r\n      <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("mfeedbackplus"));

	templateBuilder.Append("\" />\r\n      \r\n    </div>\r\n    <!--/发表留言-->\r\n    \r\n    <!--底部导航-->\r\n    <footer id=\"main_footer\">\r\n      <a href=\"#mainPanel\" class=\"icon message pressed\">留言反馈</a>\r\n      <a href=\"#addPanel\" class=\"icon pencil\">发表留言</a>\r\n    </footer>\r\n    <!--/底部导航-->\r\n	\r\n    <!--左侧导航-->\r\n    ");



	templateBuilder.Append("\r\n    <!--/左侧导航-->\r\n      \r\n  </div>\r\n</div>\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
