<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.Plugin.Feedback.feedback" ValidateRequest="false" %>
<%@ Import namespace="System.Collections.Generic" %>
<%@ Import namespace="System.Text" %>
<%@ Import namespace="System.Data" %>
<%@ Import namespace="DTcms.Common" %>

<script runat="server">
override protected void OnInit(EventArgs e)
{

	/* 
		This page was created by DTcms Template Engine at 2015/5/3 14:45:05.
		本页面代码由DTcms模板引擎生成于 2015/5/3 14:45:05. 
	*/

	base.OnInit(e);
	StringBuilder templateBuilder = new StringBuilder(220000);

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>留言反馈 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" name=\"keywords\" />\r\n<meta content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" name=\"description\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/validate.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" />\r\n<link type=\"text/css\" rel=\"stylesheet\" href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" />\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery.form.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/Validform_v5.3.2_min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/dialog-plus-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n$(function(){\r\n	//初始化发表评论表单\r\n	AjaxInitForm('#feedback_form', '#btnSubmit', 1);\r\n});\r\n</");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"section clearfix\">\r\n  <!--右边-->\r\n  <div class=\"list-right\">\r\n    <div class=\"sidebar-box\">\r\n      <div class=\"line30\"></div>\r\n      <h3>栏目导航</h3>\r\n      <ul class=\"navbar\">\r\n        ");
	DataTable contentlist = get_article_list("content", 0, 0, "status=0");

	foreach(DataRow dr in contentlist.Rows)
	{

	templateBuilder.Append("\r\n        <li>\r\n          <h4><a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("content",Utils.ObjectToStr(dr["call_index"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a></h4>\r\n        </li>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n        <li>\r\n          <h4><a href=\"");
	templateBuilder.Append(linkurl("feedback"));

	templateBuilder.Append("\">留言反馈</a></h4>\r\n        </li>\r\n        <li>\r\n          <h4><a href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">友情链接</a></h4>\r\n        </li>\r\n      </ul>\r\n      <div class=\"line20\"></div>\r\n      <h3>客户服务</h3>\r\n      <div class=\"sidebar-txt\">\r\n        <p><strong>");
	templateBuilder.Append(Utils.ObjectToStr(site.company));
	templateBuilder.Append("</strong></p>\r\n        <p>地址：");
	templateBuilder.Append(Utils.ObjectToStr(site.address));
	templateBuilder.Append("</p>\r\n        <p>电话：");
	templateBuilder.Append(Utils.ObjectToStr(site.tel));
	templateBuilder.Append("</p>\r\n        <p>E-mail：");
	templateBuilder.Append(Utils.ObjectToStr(site.email));
	templateBuilder.Append("</p>\r\n        <p>新浪微博：http://weibo.com/dtcms</p>\r\n      </div>\r\n    </div>\r\n  </div>\r\n  <!--/右边-->\r\n  \r\n  <!--左边-->\r\n  <div class=\"list-auto\">\r\n    <div class=\"ntitle\">\r\n      <h2>\r\n        <a>留言反馈</a>\r\n      </h2>\r\n    </div>\r\n    <!--留言列表-->\r\n    <div class=\"comment-box\">\r\n      <ol class=\"comment-list\">\r\n        ");
	DataTable feedbackList = new DTcms.Web.Plugin.Feedback.feedback().get_feedback_list(10, page, "", out totalcount);

	string pagelist = get_page_link(10, page, totalcount, "feedback", "__id__");

	foreach(DataRow dr in feedbackList.Rows)
	{

	templateBuilder.Append("\r\n          <li>\r\n            <div class=\"avatar\">\r\n              <img src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/images/user-avatar.png\">\r\n            </div>\r\n            <div class=\"inner\">\r\n              <p>" + Utils.ObjectToStr(dr["content"]) + "</p>\r\n              <div class=\"meta\">\r\n                <span class=\"blue\">" + Utils.ObjectToStr(dr["user_name"]) + "</span>\r\n                <span class=\"time\">" + Utils.ObjectToStr(dr["add_time"]) + "</span>\r\n              </div>\r\n            </div>\r\n            ");
	if (Utils.ObjectToStr(dr["reply_content"])!="")
	{

	templateBuilder.Append("\r\n            <div class=\"answer\">\r\n              <div class=\"meta\">\r\n                <span class=\"right time\">" + Utils.ObjectToStr(dr["reply_time"]) + "</span>\r\n                <span class=\"blue\">管理员回复：</span>\r\n              </div>\r\n              <p>" + Utils.ObjectToStr(dr["reply_content"]) + "</p>\r\n            </div>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n          </li>\r\n        ");
	}	//end for if

	if (totalcount==0)
	{

	templateBuilder.Append("\r\n          <p style=\"line-height:35px;text-align:center;\">暂无留言，快来发表留言吧！</p>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n      </ol>\r\n    </div>\r\n    <!--放置页码-->\r\n    <div class=\"page-box\" style=\"margin-left:-8px;\">\r\n      <div id=\"pagination\" class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n    </div>\r\n    <div class=\"line10\"></div>\r\n    <!--/放置页码-->\r\n    <!--/留言列表-->\r\n    \r\n    <!--发表留言-->\r\n    <h2 class=\"base-tit\">\r\n      <span>发表留言<a name=\"Add\" id=\"Add\"></a></span>\r\n    </h2>\r\n    <div class=\"line10\"></div>\r\n    <form id=\"feedback_form\" name=\"feedback_form\" url=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("plugins/feedback/ajax.ashx?action=add&site=");
	templateBuilder.Append(Utils.ObjectToStr(site.build_path));
	templateBuilder.Append("\">\r\n      <div class=\"form-box\" style=\"border:0;\">\r\n        <dl>\r\n          <dt>用户昵称：</dt>\r\n          <dd>\r\n            <input id=\"txtUserName\" name=\"txtUserName\" type=\"text\" class=\"input txt\" datatype=\"*\" sucmsg=\" \" />\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>联系电话：</dt>\r\n          <dd>\r\n            <input id=\"txtUserTel\" name=\"txtUserTel\" type=\"text\" class=\"input txt\" datatype=\"*0-20\" sucmsg=\" \" />\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>在线QQ：</dt>\r\n          <dd>\r\n            <input id=\"txtUserQQ\" name=\"txtUserQQ\" type=\"text\" class=\"input txt\" datatype=\"*0-20\" sucmsg=\" \" />\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>电子邮箱：</dt>\r\n          <dd>\r\n            <input id=\"txtUserEmail\" name=\"txtUserEmail\" type=\"text\" class=\"input txt\" />\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>留言标题：</dt>\r\n          <dd>\r\n            <input id=\"txtTitle\" name=\"txtTitle\" type=\"text\" class=\"input txt\" datatype=\"*2-100\" sucmsg=\" \" />\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>留言内容：</dt>\r\n          <dd>\r\n            <textarea id=\"txtContent\" name=\"txtContent\" class=\"input txt\" datatype=\"*\" sucmsg=\" \" style=\"width:350px;height:80px;\"></textarea>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt>验证码：</dt>\r\n          <dd>\r\n            <input id=\"txtCode\" name=\"txtCode\" type=\"text\" class=\"input small\" datatype=\"*\" sucmsg=\" \" />\r\n            <a href=\"javascript:;\" onclick=\"ToggleCode(this, '");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx');return false;\"><img src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/verify_code.ashx\" width=\"80\" height=\"22\" align=\"absmiddle\" /> 看不清楚？</a>\r\n            <span class=\"Validform_checktip\"></span>\r\n          </dd>\r\n        </dl>\r\n        <dl>\r\n          <dt></dt>\r\n          <dd>\r\n           <input id=\"btnSubmit\" name=\"btnSubmit\" type=\"submit\" value=\"发表留言\" class=\"btn\" />\r\n          </dd>\r\n        </dl>\r\n      </div>\r\n    </form>\r\n    <!--发表留言-->\r\n  </div>\r\n  <!--/左边-->\r\n</div>\r\n\r\n<!--Footer-->\r\n");

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


	templateBuilder.Append("\r\n<!--/Footer-->\r\n</body>\r\n</html>\r\n");
	Response.Write(templateBuilder.ToString());
}
</script>
