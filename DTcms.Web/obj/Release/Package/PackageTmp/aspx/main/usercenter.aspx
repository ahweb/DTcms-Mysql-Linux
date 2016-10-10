<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.usercenter" ValidateRequest="false" %>
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

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>会员中心 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<link rel=\"stylesheet\" type=\"text/css\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"section clearfix\">\r\n  <div class=\"line30\"></div>\r\n\r\n  <div class=\"info-wrap\">\r\n    <!--左侧导航-->\r\n    ");

	templateBuilder.Append("    <div class=\"info-box\">\r\n      <div class=\"avatar-box\">\r\n        <a href=\"");
	templateBuilder.Append(linkurl("usercenter","avatar"));

	templateBuilder.Append("\" class=\"img-box\">\r\n          ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n            <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" />\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n            <img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/user-avatar.png\" />\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </a>\r\n        <h3>\r\n        ");
	if (userModel.nick_name!="")
	{

	templateBuilder.Append("\r\n          ");
	templateBuilder.Append(Utils.ObjectToStr(userModel.nick_name));
	templateBuilder.Append("\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n          ");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n        </h3>\r\n        <p>余额：");
	templateBuilder.Append(Utils.ObjectToStr(userModel.amount));
	templateBuilder.Append(" 元</p>\r\n        <p>积分：");
	templateBuilder.Append(Utils.ObjectToStr(userModel.point));
	templateBuilder.Append(" 分</p>					\r\n      </div>\r\n      <ul class=\"side-nav\">\r\n        <li>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\">订单管理</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("userorder","close"));

	templateBuilder.Append("\">已关闭订单</a>\r\n        </li>\r\n        <li>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("useramount","recharge"));

	templateBuilder.Append("\">账户余额</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("userpoint","convert"));

	templateBuilder.Append("\">我的积分</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("usermessage","system"));

	templateBuilder.Append("\">站内消息</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("usercenter","invite"));

	templateBuilder.Append("\">邀请好友</a>\r\n        </li>\r\n        <li>\r\n          \r\n          <a href=\"");
	templateBuilder.Append(linkurl("usercenter","proinfo"));

	templateBuilder.Append("\">个人资料</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("usercenter","avatar"));

	templateBuilder.Append("\">头像设置</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("usercenter","password"));

	templateBuilder.Append("\">修改密码</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("usercenter","exit"));

	templateBuilder.Append("\">退出登录</a>\r\n        </li>\r\n      </ul>\r\n    </div>");


	templateBuilder.Append("\r\n    <!--/左侧导航-->\r\n    \r\n    <!--右侧内容-->\r\n    <div class=\"home-box\">\r\n      ");
	if (action=="index")
	{

	templateBuilder.Append("\r\n      <!--会员中心-->\r\n      <div class=\"u-tab-content\">\r\n        <div class=\"title-div\">\r\n          <strong>会员中心</strong>\r\n        </div>\r\n        \r\n        <div class=\"head-box clearfix\">\r\n          <div class=\"img-box\">\r\n            ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n              <img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" />\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n              <img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/user-avatar.png\" alt=\"求真像\" />\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n          </div>\r\n          <div class=\"inner\">\r\n            <h3>尊敬的会员 ");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("，欢迎您！</h3>\r\n            <p>您的会员等级：");
	templateBuilder.Append(Utils.ObjectToStr(groupModel.title));
	templateBuilder.Append("</p>\r\n            <p>个人成长值：");
	templateBuilder.Append(Utils.ObjectToStr(userModel.exp));
	templateBuilder.Append("点</p>\r\n            <p>\r\n              <a href=\"");
	templateBuilder.Append(linkurl("usercenter","avatar"));

	templateBuilder.Append("\">设置头像</a>\r\n              &nbsp;&nbsp;&nbsp;&nbsp;<a href=\"");
	templateBuilder.Append(linkurl("usercenter","password"));

	templateBuilder.Append("\">修改密码</a>\r\n            </p>\r\n          </div>\r\n        </div>\r\n        <div class=\"clear\"></div>\r\n        \r\n        <div class=\"head-inner clearfix\">\r\n          <h3>账户统计</h3>\r\n          <dl>\r\n            <dt>账户余额：</dt>\r\n            <dd>\r\n              <b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(userModel.amount));
	templateBuilder.Append("</b> 元\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>账户积分：</dt>\r\n            <dd>\r\n              <b class=\"red\">");
	templateBuilder.Append(Utils.ObjectToStr(userModel.point));
	templateBuilder.Append("</b> 分\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>未完成订单：</dt>\r\n            <dd>\r\n              <b class=\"red\">\r\n                ");
	templateBuilder.Append(get_user_order_count("status<3 and user_id="+userModel.id).ToString());

	templateBuilder.Append("\r\n              </b> 个\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>未读消息：</dt>\r\n            <dd>\r\n              <b class=\"red\">\r\n                ");
	templateBuilder.Append(get_user_message_count("type<3 and is_read=0 and accept_user_name='"+userModel.user_name+"'").ToString());

	templateBuilder.Append("\r\n              </b>条\r\n            </dd>\r\n          </dl>\r\n        </div>\r\n        \r\n        <div class=\"head-inner clearfix\">\r\n          <h3>登录统计</h3>\r\n          <dl>\r\n            <dt>本次登录IP：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(curr_login_ip));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>上次登录IP：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(pre_login_ip));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>注册时间：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.reg_time));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>上次登录时间：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(pre_login_time));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n        </div>\r\n        \r\n      </div>\r\n      <!--/会员中心-->\r\n      \r\n      ");
	}
	else if (action=="password")
	{

	templateBuilder.Append("\r\n      <!--修改密码-->\r\n      <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n      <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\">\r\n	    $(function(){\r\n			//初始化表单\r\n			AjaxInitForm('#pwdForm', '#btnSubmit', 1);\r\n		});\r\n	  </");
	templateBuilder.Append("script>\r\n      <div class=\"u-tab-head\">\r\n        <p>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("usercenter","proinfo"));

	templateBuilder.Append("\">个人资料</a>\r\n          <a class=\"selected\" href=\"");
	templateBuilder.Append(linkurl("usercenter","password"));

	templateBuilder.Append("\">修改密码</a>\r\n        </p>\r\n      </div>\r\n      <div class=\"u-tab-content\">\r\n        <div class=\"title-div\">\r\n          <strong>修改密码</strong>\r\n        </div>\r\n        <form name=\"pwdForm\" id=\"pwdForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_password_edit\">\r\n        <div class=\"form-box\">\r\n          <dl>\r\n            <dt>用户名：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>旧密码：</dt>\r\n            <dd>\r\n              <input name=\"txtOldPassword\" id=\"txtOldPassword\" type=\"password\" class=\"input txt\" datatype=\"*6-20\" nullmsg=\"请输入旧密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>新密码：</dt>\r\n            <dd>\r\n              <input name=\"txtPassword\" id=\"txtPassword\" type=\"password\" class=\"input txt\" datatype=\"*6-20\" nullmsg=\"请输入新密码\" errormsg=\"密码范围在6-20位之间\" sucmsg=\" \" />\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>确认新密码：</dt>\r\n            <dd>\r\n              <input name=\"txtPassword1\" id=\"txtPassword1\" type=\"password\" class=\"input txt\" datatype=\"*\" recheck=\"txtPassword\" nullmsg=\"请再输入一次新密码\" errormsg=\"两次输入的密码不一致\" sucmsg=\" \" />\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt></dt>\r\n            <dd>\r\n              <input name=\"btnSubmit\" id=\"btnSubmit\" type=\"submit\" class=\"btn btn-success\" value=\"确认修改\" />\r\n            </dd>\r\n          </dl>\r\n        </div>\r\n        </form>\r\n      </div>\r\n      <!--/修改密码-->\r\n      \r\n      ");
	}
	else if (action=="proinfo")
	{

	templateBuilder.Append("\r\n      <!--修改资料-->\r\n      <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n      <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/PCASClass.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/datepicker/WdatePicker.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\">\r\n	    $(function(){\r\n			//初始化表单\r\n			AjaxInitForm('#infoForm', '#btnSubmit', 1);\r\n			//初始化地区\r\n			var mypcas = new PCAS(\"txtProvince,所属省份\", \"txtCity,所属城市\", \"txtArea,所属地区\");\r\n			var areaArr = (\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.area));
	templateBuilder.Append("\").split(\",\");\r\n			if (areaArr.length == 3) {\r\n              mypcas.SetValue(areaArr[0], areaArr[1], areaArr[2]);\r\n            }\r\n		});\r\n	  </");
	templateBuilder.Append("script>\r\n      <div class=\"u-tab-head\">\r\n        <p>\r\n          <a class=\"selected\" href=\"");
	templateBuilder.Append(linkurl("usercenter","proinfo"));

	templateBuilder.Append("\">个人资料</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("usercenter","password"));

	templateBuilder.Append("\">修改密码</a>\r\n        </p>\r\n      </div>\r\n      <div class=\"u-tab-content\">\r\n        <div class=\"title-div\">\r\n          <strong>个人资料</strong>\r\n        </div>\r\n        <form name=\"infoForm\" id=\"infoForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_info_edit\">\r\n        <div class=\"form-box\">\r\n          <dl>\r\n            <dt>用户名：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(userModel.user_name));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>昵称：</dt>\r\n            <dd><input name=\"txtNickName\" id=\"txtNickName\" type=\"text\" class=\"input txt\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.nick_name));
	templateBuilder.Append("\" datatype=\"*\" sucmsg=\" \" /></dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>性别：</dt>\r\n            <dd>\r\n              ");
	if (userModel.sex=="男")
	{

	templateBuilder.Append("\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"男\" checked=\"checked\" />男</label> \r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"女\" />女</label>\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"保密\" datatype=\"*\" sucmsg=\" \" />保密</label>\r\n                ");
	}
	else if (userModel.sex=="女")
	{

	templateBuilder.Append("\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"男\" />男</label>\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"女\" checked=\"checked\" />女</label>\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"保密\" datatype=\"*\" sucmsg=\" \" />保密</label>\r\n                ");
	}
	else
	{

	templateBuilder.Append("\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"男\" />男</label>\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"女\" />女</label>\r\n                <label class=\"radio\"><input name=\"rblSex\" type=\"radio\" value=\"保密\" checked=\"checked\" datatype=\"*\" sucmsg=\" \" />保密</label>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>生日：</dt>\r\n            <dd>\r\n              ");
	if (userModel.birthday==null)
	{

	templateBuilder.Append("\r\n              <input name=\"txtBirthday\" id=\"txtBirthday\" type=\"text\" class=\"input txt\" maxlength=\"30\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd'});\" />\r\n              ");
	}
	else
	{

	templateBuilder.Append("\r\n              <input name=\"txtBirthday\" id=\"txtBirthday\" type=\"text\" class=\"input txt\" maxlength=\"30\" onfocus=\"WdatePicker({dateFmt:'yyyy-MM-dd'});\" value=\"");	templateBuilder.Append(Utils.ObjectToDateTime(userModel.birthday).ToString("yyyy-M-d"));

	templateBuilder.Append("\" />\r\n              ");
	}	//end for if

	templateBuilder.Append("\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>邮箱：</dt>\r\n            <dd><input name=\"txtEmail\" id=\"txtEmail\" type=\"text\" class=\"input txt\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.email));
	templateBuilder.Append("\" datatype=\"e\" sucmsg=\" \" /></dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>手机：</dt>\r\n            <dd><input name=\"txtMobile\" id=\"txtMobile\" type=\"text\" class=\"input txt\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.mobile));
	templateBuilder.Append("\" datatype=\"m\" sucmsg=\" \" /></dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>电话：</dt>\r\n            <dd><input name=\"txtTelphone\" id=\"txtTelphone\" type=\"text\" class=\"input txt\" maxlength=\"30\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.telphone));
	templateBuilder.Append("\" /></dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>所属地区：</dt>\r\n            <dd>\r\n              <select id=\"txtProvince\" name=\"txtProvince\" class=\"select\"></select>\r\n              <select id=\"txtCity\" name=\"txtCity\" class=\"select\"></select>\r\n              <select id=\"txtArea\" name=\"txtArea\" class=\"select\" datatype=\"*\" sucmsg=\" \"></select>\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>详细地址：</dt>\r\n            <dd><input name=\"txtAddress\" id=\"txtAddress\" type=\"text\" class=\"input wide\" maxlength=\"250\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.address));
	templateBuilder.Append("\" /></dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>在线QQ：</dt>\r\n            <dd><input name=\"txtQQ\" id=\"txtQQ\" type=\"text\" class=\"input txt\" maxlength=\"20\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.qq));
	templateBuilder.Append("\" /></dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>MSN账户：</dt>\r\n            <dd><input name=\"txtMsn\" id=\"txtMsn\" type=\"text\" class=\"input txt\" maxlength=\"20\" value=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.msn));
	templateBuilder.Append("\" /></dd>\r\n          </dl>\r\n          <dl>\r\n            <dt></dt>\r\n            <dd><input name=\"btnSubmit\" id=\"btnSubmit\" type=\"submit\" class=\"btn btn-success\" value=\"确认修改\" /></dd>\r\n          </dl>\r\n        </div>\r\n        </form>\r\n      </div>\r\n      <!--/修改资料-->\r\n      \r\n      ");
	}
	else if (action=="avatar")
	{

	templateBuilder.Append("\r\n      <!--设置头像-->\r\n      <link rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/jquery.jcrop.css\" type=\"text/css\" />\r\n      <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/webuploader/webuploader.min.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.jcrop.min.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/avatar.js\"></");
	templateBuilder.Append("script>\r\n      <script type=\"text/javascript\">\r\n	  $(function(){\r\n	      initWebUploader('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("');\r\n	  });\r\n	  </");
	templateBuilder.Append("script>\r\n      <div class=\"u-tab-content\">\r\n        <div class=\"title-div\">\r\n          <strong>头像设置</strong>\r\n        </div>\r\n        <p><strong>设置我的新头像</strong></p>\r\n        <p>如果您还没有设置自己的头像，系统会显示为默认头像，您需要自己上传一张新照片来作为自己的个人头像。</p>\r\n        <p>上传成功后，请裁剪合适的头像确认保存后才能生效。</p>\r\n        <div class=\"line20\"></div>\r\n        \r\n        <div id=\"upload-box\" class=\"upload-box\"></div>\r\n        \r\n        <div class=\"cropper-wraper\">\r\n          <div class=\"cropper-box\">\r\n            <img id=\"target\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/avatar-bg.png\" width=\"350\" height=\"350\" />\r\n          </div>\r\n          <div class=\"cropper-view\">\r\n            <div class=\"img-box\">\r\n              ");
	if (userModel.avatar!="")
	{

	templateBuilder.Append("\r\n                <img id=\"preview\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(userModel.avatar));
	templateBuilder.Append("\" />\r\n              ");
	}
	else
	{

	templateBuilder.Append("\r\n                <img id=\"preview\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/user-avatar.png\" />\r\n              ");
	}	//end for if

	templateBuilder.Append("\r\n              <span>头像预览</span>\r\n            </div>\r\n            <div class=\"btn-box\">\r\n              <p><strong>头像预览区</strong></p>\r\n              <p><input name=\"btnSubmit\" type=\"button\" class=\"btn\" value=\"确定保存\" onclick=\"CropSubmit(this);\" /></p>\r\n              <p class=\"tip\">提示：生成头像大小180*180相素上传图片后，左侧选取图片合适大小，点击下面的保存按钮。</p>\r\n            </div>\r\n          </div>\r\n          \r\n        </div>\r\n      </div>\r\n      <form id=\"uploadForm\" name=\"uploadForm\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_avatar_crop\">\r\n        <input id=\"hideFileName\" name=\"hideFileName\" type=\"hidden\" />\r\n        <input id=\"hideX1\" name=\"hideX1\" type=\"hidden\" value=\"0\" />\r\n        <input id=\"hideY1\" name=\"hideY1\" type=\"hidden\" value=\"0\" />\r\n        <input id=\"hideWidth\" name=\"hideWidth\" type=\"hidden\" value=\"0\" />\r\n        <input id=\"hideHeight\" name=\"hideHeight\" type=\"hidden\" value=\"0\" />\r\n      </form>\r\n      <!--/设置头像-->\r\n      \r\n      ");
	}
	else if (action=="invite")
	{

	templateBuilder.Append("\r\n      <!--邀请码-->\r\n      <div class=\"u-tab-head\">\r\n        <p>\r\n          <a class=\"selected\" href=\"");
	templateBuilder.Append(linkurl("usercenter","invite"));

	templateBuilder.Append("\">邀请注册</a>\r\n        </p>\r\n        <span>\r\n          <a class=\"add\" href=\"javascript:;\" onclick=\"clickSubmit('");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=user_invite_code');\">申请邀请码</a>\r\n        </span>\r\n      </div>\r\n      <div class=\"u-tab-content\">\r\n        <div class=\"title-div\">\r\n          <strong>我的邀请码</strong>\r\n        </div>\r\n        <p>说明：您购买的邀请码会在失效之后由系统定时清理，不会长期驻留在列表中</p>\r\n        <div class=\"line10\"></div>\r\n        <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"ftable\">\r\n          <tr>\r\n            <th align=\"left\">邀请码</th>\r\n            <th width=\"150\">申请时间</th>\r\n            <th width=\"150\">过期时间</th>\r\n            <th width=\"80\">已使用次数</th>\r\n            <th width=\"80\">状态</th>\r\n          </tr>\r\n          ");
	DataTable inviteList = get_user_invite_list(0, "user_name='"+userModel.user_name+"'");

	templateBuilder.Append("<!--取得一个DataTable-->\r\n          ");
	foreach(DataRow dr in inviteList.Rows)
	{

	templateBuilder.Append("\r\n          <tr>\r\n            <td>\r\n              " + Utils.ObjectToStr(dr["str_code"]) + " &nbsp; \r\n              <a href=\"javascript:;\" onclick=\"copyText('邀请码：" + Utils.ObjectToStr(dr["str_code"]) + "');\">[复制]</a>\r\n            </td>\r\n            <td align=\"center\">" + Utils.ObjectToStr(dr["add_time"]) + "</td>\r\n            <td align=\"center\">\r\n              " + Utils.ObjectToStr(dr["eff_time"]) + "\r\n            </td>\r\n            <td align=\"center\">" + Utils.ObjectToStr(dr["count"]) + "</td>\r\n            <td align=\"center\">\r\n              ");
	if (get_invite_status(Utils.ObjectToStr(dr["str_code"])))
	{

	templateBuilder.Append("\r\n              有效\r\n              ");
	}
	else
	{

	templateBuilder.Append("\r\n              已失效\r\n              ");
	}	//end for if

	templateBuilder.Append("\r\n            </td>\r\n          </tr>\r\n          ");
	}	//end for if

	if (inviteList.Rows.Count==0)
	{

	templateBuilder.Append("\r\n          <tr><td colspan=\"8\" align=\"center\">暂无邀请码...</td></tr>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </table>\r\n      </div>\r\n      <!--/邀请码-->\r\n      ");
	}	//end for if

	templateBuilder.Append("\r\n      \r\n    </div>\r\n    <!--/右侧内容-->\r\n  </div>\r\n</div>\r\n\r\n<!--Footer-->\r\n");

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
