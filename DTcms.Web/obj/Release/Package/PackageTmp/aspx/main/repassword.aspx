<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.repassword" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by DTcms Template Engine at 2015/5/15 1:37:44.
		本页面代码由DTcms模板引擎生成于 2015/5/15 1:37:44. 
	*/

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>会员登录 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<link href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<link href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n</head>\r\n\r\n<body>\r\n<!--Header-->\r\n");

	templateBuilder.Append("<div class=\"header\">\r\n  <div class=\"header-wrap\">\r\n    <div class=\"section\">\r\n      <div class=\"left-box\">\r\n        <a class=\"logo\" href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</a>\r\n        <p class=\"nav\">\r\n          <a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">资讯</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">商城</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">图片</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">下载</a>\r\n        </p>\r\n      </div>\r\n      <div class=\"search\">\r\n        <input id=\"keywords\" name=\"keywords\" class=\"input\" type=\"text\" onkeydown=\"if(event.keyCode==13){SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');return false};\" placeholder=\"输入回车搜索\" x-webkit-speech=\"\" />\r\n        <input class=\"submit\" type=\"submit\" onclick=\"SiteSearch('");
	templateBuilder.Append(linkurl("search"));

	templateBuilder.Append("', '#keywords');\" value=\"搜索\" />\r\n      </div>\r\n      <div class=\"right-box\">\r\n      <script type=\"text/javascript\">\r\n			$.ajax({\r\n				type: \"POST\",\r\n				url: \"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_check_login\",\r\n				dataType: \"json\",\r\n				timeout: 20000,\r\n				success: function (data, textStatus) {\r\n					if (data.status == 1) {\r\n						$(\"#menu\").prepend('<li class=\"line\"><a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出</a></li>');\r\n						$(\"#menu\").prepend('<li class=\"login\"><em></em><a href=\"");
	templateBuilder.Append(linkurl("usercenter","index"));

	templateBuilder.Append("\">会员中心</a></li>');\r\n					} else {\r\n						$(\"#menu\").prepend('<li class=\"line\"><a href=\"");
	templateBuilder.Append(linkurl("register"));

	templateBuilder.Append("\">注册</a></li>');\r\n						$(\"#menu\").prepend('<li class=\"login\"><em></em><a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">登录</a></li>');\r\n					}\r\n				}\r\n			});\r\n		</");
	templateBuilder.Append("script>\r\n        <ul id=\"menu\">\r\n          <li>\r\n            <a href=\"");
	templateBuilder.Append(linkurl("cart"));

	templateBuilder.Append("\">购物车<span id=\"shoppingCartCount\"><script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_cart_count\"></");
	templateBuilder.Append("script></span>件</a>\r\n          </li>\r\n          <li><a href=\"");
	templateBuilder.Append(linkurl("content","contact"));

	templateBuilder.Append("\">联系我们</a></li>\r\n        </ul>\r\n      </div>\r\n    </div>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"main-box\">\r\n  <div class=\"section clearfix\">\r\n  ");
	if (action=="")
	{

	templateBuilder.Append("\r\n    <!--取回密码-->\r\n    <script type=\"text/javascript\">\r\n      $(function(){\r\n        //初始化表单\r\n        AjaxInitForm('#pwdform', '#btnSubmit', 1);\r\n      });\r\n    </");
	templateBuilder.Append("script>\r\n    <div class=\"main-tit\">\r\n      <h2>取回密码</h2>\r\n    </div>\r\n    <div class=\"inner-box\">\r\n      <form id=\"pwdform\" name=\"pwdform\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_getpassword&site=");
	templateBuilder.Append(Utils.ObjectToStr(site.build_path));
	templateBuilder.Append("\">\r\n      <div class=\"dl-list\">\r\n        <dl>\r\n          <dt>取回方式：</dt>\r\n          <dd>\r\n            <label><input name=\"txtType\" type=\"radio\" value=\"mobile\" checked=\"checked\" /> 手机短信</label>\r\n            <label><input name=\"txtType\" type=\"radio\" value=\"email\" datatype=\"*\" sucmsg=\" \" /> 电子邮箱</label>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>用 户 名：</dt>\r\n          <dd>\r\n            <input id=\"txtUserName\" name=\"txtUserName\" type=\"text\" class=\"input txt\" datatype=\"*1-50\" sucmsg=\" \" />\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>验 证 码：</dt>\r\n          <dd>\r\n            <input id=\"txtCode\" name=\"txtCode\" type=\"text\" class=\"input code\" placeholder=\"输入验证码\" datatype=\"s4-20\" nullmsg=\"请输入右边显示的验证码\" sucmsg=\" \" />\r\n            <a class=\"send\" title=\"点击切换验证码\" href=\"javascript:;\" onclick=\"ToggleCode(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx');return false;\">\r\n              <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx\" width=\"80\" height=\"22\" />\r\n            </a>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt></dt>\r\n          <dd>\r\n            <input name=\"btnSubmit\" id=\"btnSubmit\" type=\"submit\" class=\"btn btn-success\" value=\"确认提交\" />\r\n          </dd>\r\n        </dl>\r\n      </div>\r\n      </form>\r\n    </div>\r\n    <!--取回密码-->\r\n    \r\n  ");
	}
	else if (action=="mobile")
	{

	templateBuilder.Append("\r\n    <!--手机取回密码-->\r\n	<script type=\"text/javascript\">\r\n      $(function(){\r\n        //初始化表单\r\n        AjaxInitForm('#pwdform', '#btnSubmit', 1, '#turl');\r\n      });\r\n    </");
	templateBuilder.Append("script>\r\n    <div class=\"main-tit\">\r\n      <h2>重设密码</h2>\r\n    </div>\r\n    <div class=\"inner-box\">\r\n      <form id=\"pwdform\" name=\"pwdform\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_repassword&site=");
	templateBuilder.Append(Utils.ObjectToStr(site.build_path));
	templateBuilder.Append("\">\r\n      <div class=\"dl-list\">\r\n        <dl>\r\n          <dt>短信验证码：</dt>\r\n          <dd>\r\n            <input name=\"hideCode\" id=\"txtTelphone\" type=\"text\" class=\"input txt\" datatype=\"*\" />\r\n            <span class=\"Validform_checktip\">输入您手机收到的验证码</span>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>新 密 码：</dt>\r\n          <dd>\r\n            <input name=\"txtPassword\" id=\"txtPassword\" type=\"password\" class=\"input txt\" datatype=\"*6-20\" nullmsg=\"请输入新密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n            <span class=\"Validform_checktip\">重新设置新的密码</span>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>确认新密码：</dt>\r\n          <dd>\r\n            <input name=\"txtPassword1\" id=\"txtPassword1\" type=\"password\" class=\"input txt\" datatype=\"*\" recheck=\"txtPassword\" nullmsg=\"请再输入一次新密码\" errormsg=\"两次输入的密码不一致\" sucmsg=\" \" />\r\n            <span class=\"Validform_checktip\">再次输入新的密码</span>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt></dt>\r\n          <dd><input name=\"btnSubmit\" id=\"btnSubmit\" type=\"submit\" class=\"btn btn-success\" value=\"确认提交\" /></dd>\r\n        </dl>\r\n      </div>\r\n      </form>\r\n    </div>\r\n    <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\" />\r\n    <!--/手机取回密码-->\r\n\r\n  ");
	}
	else if (action=="email")
	{

	templateBuilder.Append("\r\n    <!--邮箱取回密码-->\r\n    <script type=\"text/javascript\">\r\n      $(function(){\r\n        //初始化表单\r\n        AjaxInitForm('#pwdform', '#btnSubmit', 1, '#turl');\r\n      });\r\n    </");
	templateBuilder.Append("script>\r\n    <div class=\"main-tit\">\r\n      <h2>重设密码</h2>\r\n    </div>\r\n    <div class=\"inner-box\">\r\n      <form id=\"pwdform\" name=\"pwdform\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_repassword&site=");
	templateBuilder.Append(Utils.ObjectToStr(site.build_path));
	templateBuilder.Append("\">\r\n      <div class=\"dl-list\">\r\n        <dl>\r\n          <dt>用户名：</dt>\r\n          <dd>\r\n            ");
	templateBuilder.Append(Utils.ObjectToStr(username));
	templateBuilder.Append("\r\n            <input name=\"hideCode\" type=\"hidden\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(code));
	templateBuilder.Append("\" />\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>新密码：</dt>\r\n          <dd>\r\n            <input name=\"txtPassword\" id=\"txtPassword\" type=\"password\" class=\"input txt\" datatype=\"*6-20\" nullmsg=\"请输入新密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n            <span class=\"Validform_checktip\">重新设置新的密码</span>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>确认新密码：</dt>\r\n          <dd>\r\n            <input name=\"txtPassword1\" id=\"txtPassword1\" type=\"password\" class=\"input txt\" datatype=\"*\" recheck=\"txtPassword\" nullmsg=\"请再输入一次新密码\" errormsg=\"两次输入的密码不一致\" sucmsg=\" \" />\r\n            <span class=\"Validform_checktip\">再次输入新的密码</span>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt></dt>\r\n          <dd><input name=\"btnSubmit\" id=\"btnSubmit\" type=\"submit\" class=\"btn btn-success\" value=\"确认提交\" /></dd>\r\n        </dl>\r\n      </div>\r\n      </form>\r\n    </div>\r\n    <input id=\"turl\" type=\"hidden\" value=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\" />\r\n    <!--/邮箱取回密码-->\r\n\r\n  ");
	}
	else if (action=="error")
	{

	templateBuilder.Append("\r\n  <!--错误界面-->\r\n  <div class=\"main-tit\">\r\n      <h2>温馨提示</h2>\r\n    </div>\r\n    <div class=\"inner-box\">\r\n      <div class=\"msg-tips\">\r\n        <div class=\"ico error\"></div>\r\n        <div class=\"msg\">\r\n          <strong>出错啦，该用户不存在或验证已过期！</strong>\r\n          <p>无法验证你的账户，不知神马原因，可能是你的用户名不存在或者验证码已经过期啦！</p>\r\n          <p>不过别担心，如果您还记得你的会员名称的话，点击这里<a href=\"");
	templateBuilder.Append(linkurl("login"));

	templateBuilder.Append("\">进入会员中心</a>吧。</p>\r\n        </div>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <!--/错误界面-->\r\n  ");
	}	//end for if

	templateBuilder.Append("\r\n\r\n  </div>\r\n</div>\r\n\r\n<!--Footer-->\r\n");

	templateBuilder.Append("<div class=\"footer clearfix\">\r\n  <div class=\"foot-nav\">\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首 页</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("content","about"));

	templateBuilder.Append("\">关于我们</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频专区</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">资源下载</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">图片分享</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">留言反馈</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">友情链接</a>|\r\n    <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("content","contact"));

	templateBuilder.Append("\">联系我们</a>\r\n  </div>\r\n  <div class=\"copyright\">\r\n    <p>版权所有 ");
	templateBuilder.Append(site.company.ToString());

	templateBuilder.Append(" 粤ICP备11064298号 DTcms版本号：");
	templateBuilder.Append(Utils.GetVersion().ToString());

	templateBuilder.Append(" 旗舰版</p>\r\n    <p>Copyright &copy; 2009-2015 dtcms.net Corporation,All Rights Reserved.</p>\r\n    <p><script src=\"http://s24.cnzz.com/stat.php?id=1996164&web_id=1996164&show=pic\" language=\"javascript\"></");
	templateBuilder.Append("script></p>\r\n  </div>\r\n</div>");


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>");
	Response.Write(templateBuilder.ToString());
}
</script>
