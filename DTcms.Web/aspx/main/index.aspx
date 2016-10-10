<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.index" ValidateRequest="false" %>
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

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_title));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/jquery.flexslider-min.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/js/common.js\"></");
	templateBuilder.Append("script>\r\n<script type=\"text/javascript\">\r\n$(function(){\r\n	$(\".focusbox\").flexslider({\r\n		directionNav: false,\r\n		pauseOnAction: false\r\n	});\r\n});\r\n</");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"section clearfix\">\r\n  <div class=\"ntitle\">\r\n    <h2>\r\n      <a href=\"");
	templateBuilder.Append(linkurl("news"));

	templateBuilder.Append("\">新闻资讯<em></em></a>\r\n    </h2>\r\n    <p>\r\n      <!--类别-->\r\n      ");
	DataTable newsCList = get_category_child_list("news",0);

	int ncdr__loop__id=0;
	foreach(DataRow ncdr in newsCList.Rows)
	{
		ncdr__loop__id++;


	if (ncdr__loop__id==1)
	{

	templateBuilder.Append("\r\n      <a class=\"no-bg\" href=\"");
	templateBuilder.Append(linkurl("news_list",Utils.ObjectToStr(ncdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(ncdr["title"]) + "</a>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <a href=\"");
	templateBuilder.Append(linkurl("news_list",Utils.ObjectToStr(ncdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(ncdr["title"]) + "</a>\r\n      ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n      <!--/类别-->\r\n    </p>\r\n  </div>\r\n  <div class=\"wrapper clearfix\">\r\n    <div class=\"main-left\">\r\n      <div class=\"focusbox\">\r\n        <ul class=\"slides\">\r\n          ");
	DataTable focusNews = get_article_list("news", 0, 8, "status=0 and is_slide=1 and img_url<>''");

	foreach(DataRow dr in focusNews.Rows)
	{

	templateBuilder.Append("\r\n          <li>\r\n            <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n              <span class=\"note-bg\"></span>\r\n              <span class=\"note-txt\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n              <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n            </a>\r\n          </li>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </ul>\r\n      </div>\r\n    </div>\r\n    \r\n    <div class=\"main-left\" style=\"margin-right:0;\">\r\n      <ul class=\"txt-list\">\r\n        ");
	DataTable newsList = get_article_list("news", 0, 9, "status=0");

	int newdr__loop__id=0;
	foreach(DataRow newdr in newsList.Rows)
	{
		newdr__loop__id++;


	if (newdr__loop__id==1)
	{

	templateBuilder.Append("\r\n        <li class=\"tit\">\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <li>\r\n         <span>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(newdr["add_time"])).ToString("MM-dd"));

	templateBuilder.Append("</span>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n          <a title=\"" + Utils.ObjectToStr(newdr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(newdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(newdr["title"]) + "</a>\r\n        </li>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n    <div class=\"sidebar-right\" style=\"margin-left:15px;\">\r\n      <ul class=\"img-list ilist\">\r\n        ");
	DataTable topNewsList = get_article_list("news", 0, 2, "status=0 and is_top=1 and img_url<>''");

	foreach(DataRow dr in topNewsList.Rows)
	{

	templateBuilder.Append("\r\n        <li>\r\n          <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("news_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n            ");
	if (Utils.ObjectToStr(dr["is_top"])=="1")
	{

	templateBuilder.Append("\r\n            <span class=\"abs-txt\">头条</span>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <span class=\"abs-bg\"></span>\r\n            <span class=\"txt1\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n            <span class=\"txt2\">\r\n              <i>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(dr["add_time"])).ToString("MM-dd"));

	templateBuilder.Append("</i>\r\n              <p>" + Utils.ObjectToStr(dr["zhaiyao"]) + "</p>\r\n            </span>\r\n            <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n          </a>\r\n        </li>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"section clearfix\">\r\n  <div class=\"ntitle\">\r\n    <h2>\r\n      <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城<em></em></a>\r\n    </h2>\r\n    <p>\r\n      <!--类别-->\r\n      ");
	DataTable goodsCList = get_category_child_list("goods",0);

	int gcdr__loop__id=0;
	foreach(DataRow gcdr in goodsCList.Rows)
	{
		gcdr__loop__id++;


	if (gcdr__loop__id==1)
	{

	templateBuilder.Append("\r\n      <a class=\"no-bg\" href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(gcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(gcdr["title"]) + "</a>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <a href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(gcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(gcdr["title"]) + "</a>\r\n      ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n      <!--/类别-->\r\n    </p>\r\n  </div>\r\n  <div class=\"wrapper clearfix\">\r\n    <div class=\"main-left ilist\">\r\n      ");
	DataTable focusGoods = get_article_list("goods", 0, 1, "status=0 and is_slide=1");

	foreach(DataRow dr in focusGoods.Rows)
	{

	templateBuilder.Append("\r\n      <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n        <span class=\"abs-bg\"></span>\r\n        <span class=\"txt1\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n        <span class=\"txt2\">\r\n          <p>" + Utils.ObjectToStr(dr["sub_title"]) + "</p>\r\n        </span>\r\n        <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n      </a>\r\n      ");
	}	//end for if

	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"main-right\">\r\n      <ul class=\"img-list ilist\">\r\n        ");
	DataTable redGoods = get_article_list("goods", 0, 6, "status=0 and is_red=1");

	foreach(DataRow dr in redGoods.Rows)
	{

	templateBuilder.Append("\r\n        <li>\r\n          <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n            ");
	if (Utils.ObjectToStr(dr["is_top"])=="1")
	{

	templateBuilder.Append("\r\n            <span class=\"abs-txt\">特价</span>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            <span class=\"abs-bg\"></span>\r\n            <span class=\"price\">\r\n              <i>¥" + Utils.ObjectToStr(dr["market_price"]) + "</i>\r\n              <b>¥</b>" + Utils.ObjectToStr(dr["sell_price"]) + "\r\n            </span>\r\n            <span class=\"protxt\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n            <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n          </a>\r\n        </li>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"section clearfix\">\r\n  <div class=\"ntitle\">\r\n    <h2>\r\n      <a href=\"");
	templateBuilder.Append(linkurl("video"));

	templateBuilder.Append("\">视频专区<em></em></a>\r\n    </h2>\r\n    <p>\r\n      <!--类别-->\r\n      ");
	DataTable videoCList = get_category_child_list("video",0);

	int vcdr__loop__id=0;
	foreach(DataRow vcdr in videoCList.Rows)
	{
		vcdr__loop__id++;


	if (vcdr__loop__id==1)
	{

	templateBuilder.Append("\r\n      <a class=\"no-bg\" href=\"");
	templateBuilder.Append(linkurl("video_list",Utils.ObjectToStr(vcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(vcdr["title"]) + "</a>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <a href=\"");
	templateBuilder.Append(linkurl("video_list",Utils.ObjectToStr(vcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(vcdr["title"]) + "</a>\r\n      ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n      <!--/类别-->\r\n    </p>\r\n  </div>\r\n  <div class=\"wrapper clearfix\">\r\n    <div class=\"main-left ilist\">\r\n      ");
	DataTable focusVideo = get_article_list("video", 0, 1, "status=0 and is_slide=1");

	foreach(DataRow dr in focusVideo.Rows)
	{

	templateBuilder.Append("\r\n      <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("video_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n        <em></em>\r\n        <span class=\"abs-bg\"></span>\r\n        <span class=\"txt1\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n        <span class=\"txt2\">\r\n          <p>" + Utils.ObjectToStr(dr["sub_title"]) + "</p>\r\n        </span>\r\n        <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n      </a>\r\n      ");
	}	//end for if

	templateBuilder.Append("\r\n    </div>\r\n    <div class=\"main-right\">\r\n      <ul class=\"img-list ilist\">\r\n        ");
	DataTable redVideo = get_article_list("video", 0, 6, "status=0 and is_red=1");

	foreach(DataRow dr in redVideo.Rows)
	{

	templateBuilder.Append("\r\n        <li>\r\n          <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("video_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n            <em></em>\r\n            <span class=\"abs-bg\"></span>\r\n            <span class=\"txt1\">" + Utils.ObjectToStr(dr["title"]) + "</span>\r\n            <span class=\"txt2\">\r\n              <p>" + Utils.ObjectToStr(dr["sub_title"]) + "</p>\r\n            </span>\r\n            <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n          </a>\r\n        </li>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"section clearfix\">\r\n  <div class=\"ntitle\">\r\n    <h2>\r\n      <a href=\"");
	templateBuilder.Append(linkurl("photo"));

	templateBuilder.Append("\">图片分享<em></em></a>\r\n    </h2>\r\n    <p>\r\n      <!--类别-->\r\n      ");
	DataTable photoCList = get_category_child_list("photo",0);

	int pcdr__loop__id=0;
	foreach(DataRow pcdr in photoCList.Rows)
	{
		pcdr__loop__id++;


	if (pcdr__loop__id==1)
	{

	templateBuilder.Append("\r\n      <a class=\"no-bg\" href=\"");
	templateBuilder.Append(linkurl("photo_list",Utils.ObjectToStr(pcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(pcdr["title"]) + "</a>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <a href=\"");
	templateBuilder.Append(linkurl("photo_list",Utils.ObjectToStr(pcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(pcdr["title"]) + "</a>\r\n      ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n      <!--/类别-->\r\n    </p>\r\n  </div>\r\n  <div class=\"wrapper clearfix\">\r\n    <div class=\"photo-list ilist\">\r\n      <ul>\r\n        ");
	DataTable redPhoto = get_article_list("photo", 0, 6, "status=0 and is_red=1");

	int photodr__loop__id=0;
	foreach(DataRow photodr in redPhoto.Rows)
	{
		photodr__loop__id++;


	templateBuilder.Append("\r\n        <li class=\"col-");
	templateBuilder.Append(Utils.ObjectToStr(photodr__loop__id));
	templateBuilder.Append("\">\r\n          <a title=\"" + Utils.ObjectToStr(photodr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("photo_show",Utils.ObjectToStr(photodr["id"])));

	templateBuilder.Append("\">\r\n            <span class=\"abs-bg\"></span>\r\n            <span class=\"txt1\">" + Utils.ObjectToStr(photodr["title"]) + "</span>\r\n            <span class=\"txt2\">\r\n              <p>" + Utils.ObjectToStr(photodr["add_time"]) + "</p>\r\n            </span>\r\n            <img src=\"" + Utils.ObjectToStr(photodr["img_url"]) + "\" />\r\n          </a>\r\n        </li>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"section clearfix\">\r\n  <div class=\"ntitle\">\r\n    <h2>\r\n      <a href=\"");
	templateBuilder.Append(linkurl("down"));

	templateBuilder.Append("\">资源下载<em></em></a>\r\n    </h2>\r\n    <p>\r\n      <!--类别-->\r\n      ");
	DataTable downCList = get_category_child_list("down",0);

	int dcdr__loop__id=0;
	foreach(DataRow dcdr in downCList.Rows)
	{
		dcdr__loop__id++;


	if (dcdr__loop__id==1)
	{

	templateBuilder.Append("\r\n      <a class=\"no-bg\" href=\"");
	templateBuilder.Append(linkurl("down_list",Utils.ObjectToStr(dcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dcdr["title"]) + "</a>\r\n      ");
	}
	else
	{

	templateBuilder.Append("\r\n      <a href=\"");
	templateBuilder.Append(linkurl("down_list",Utils.ObjectToStr(dcdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dcdr["title"]) + "</a>\r\n      ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n      <!--/类别-->\r\n    </p>\r\n  </div>\r\n  <div class=\"wrapper clearfix\">\r\n    <ul class=\"img-list high ilist\">\r\n      ");
	DataTable redDown = get_article_list("down", 0, 5, "status=0 and is_red=1");

	foreach(DataRow dr in redDown.Rows)
	{

	templateBuilder.Append("\r\n      <li>\r\n        <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("down_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n          <div class=\"img-box\">\r\n            <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n          </div>\r\n          <div class=\"info\">\r\n            <h3>" + Utils.ObjectToStr(dr["title"]) + "</h3>\r\n            <span>\r\n              <i>下载：<script type=\"text/javascript\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("tools/submit_ajax.ashx?action=view_attach_count&id=" + Utils.ObjectToStr(dr["id"]) + "&view=count\"></");
	templateBuilder.Append("script>次</i>\r\n              <p>");	templateBuilder.Append(Utils.ObjectToDateTime(Utils.ObjectToStr(dr["add_time"])).ToString("yyyy-MM-dd"));

	templateBuilder.Append("</p>\r\n            </span>\r\n          </div>\r\n        </a>\r\n      </li>\r\n      ");
	}	//end for if

	templateBuilder.Append("\r\n    </ul>\r\n  </div>\r\n</div>\r\n\r\n<div class=\"section clearfix\">\r\n  <div class=\"ntitle\">\r\n    <h2>\r\n      <a href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">友情链接<em></em></a>\r\n    </h2>\r\n    <p>\r\n      <a class=\"no-bg\" href=\"");
	templateBuilder.Append(linkurl("link"));

	templateBuilder.Append("\">申请链接</a>\r\n    </p>\r\n  </div>\r\n  <div class=\"wrapper link-box\">\r\n    <div class=\"txt\">\r\n      ");
	DataTable linkList1 = get_plugin_method("DTcms.Web.Plugin.Link", "link", "get_link_list", 0, "is_lock=0 and is_image=0 and is_red=1");

	foreach(DataRow dr in linkList1.Rows)
	{

	templateBuilder.Append("\r\n        <a target=\"_blank\" href=\"" + Utils.ObjectToStr(dr["site_url"]) + "\">" + Utils.ObjectToStr(dr["title"]) + "</a> | \r\n      ");
	}	//end for if

	templateBuilder.Append("\r\n    </div>\r\n    <ul class=\"img\">\r\n      ");
	DataTable linkList2 = get_plugin_method("DTcms.Web.Plugin.Link", "link", "get_link_list", 10, "is_lock=0 and is_image=1 and is_red=1");

	foreach(DataRow dr in linkList2.Rows)
	{

	templateBuilder.Append("\r\n        <li><a target=\"_blank\" href=\"" + Utils.ObjectToStr(dr["site_url"]) + "\" title=\"" + Utils.ObjectToStr(dr["title"]) + "\"><img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" /></a></li>\r\n      ");
	}	//end for if

	templateBuilder.Append("\r\n    </ul>\r\n  </div>\r\n</div>\r\n\r\n<!--Footer-->\r\n");

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
