<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.article_list" ValidateRequest="false" %>
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
	const string channel = "goods";
	const int pagesize = 20;

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>");
	templateBuilder.Append(Utils.ObjectToStr(model.title));
	templateBuilder.Append(" - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(model.seo_keywords));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(model.seo_description));
	templateBuilder.Append("\" />\r\n<link href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("css/pagination.css\" rel=\"stylesheet\" />\r\n<link href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/jquery/jquery-1.11.2.min.js\"></");
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


	templateBuilder.Append("\r\n<!--/Header-->\r\n\r\n<div class=\"section clearfix\">\r\n\r\n  <div class=\"wrapper auto clearfix\">\r\n    ");
	string category_nav = get_category_menu("goods_list", category_id);

	templateBuilder.Append("\r\n    <div class=\"curr-nav\">当前位置：<a href=\"");
	templateBuilder.Append(linkurl("index"));

	templateBuilder.Append("\">首页</a> &gt; <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">购物商城</a>");
	templateBuilder.Append(Utils.ObjectToStr(category_nav));
	templateBuilder.Append("</div>\r\n    \r\n    <div class=\"screen-box\">\r\n      <dl>\r\n        <dt>分类：</dt>\r\n        <dd>\r\n          ");
	if (category_id==0)
	{

	templateBuilder.Append("\r\n            <a class=\"selected\" href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">全部</a>\r\n          ");
	}
	else
	{

	templateBuilder.Append("\r\n            <a href=\"");
	templateBuilder.Append(linkurl("goods"));

	templateBuilder.Append("\">全部</a>\r\n          ");
	}	//end for if

	DataTable categoryList = get_category_child_list(channel,0);

	foreach(DataRow cdr in categoryList.Rows)
	{

	if (category_id==Utils.StrToInt(Utils.ObjectToStr(cdr["id"]), 0))
	{

	templateBuilder.Append("\r\n              <a class=\"selected\" href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(cdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr["title"]) + "</a>\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n              <a href=\"");
	templateBuilder.Append(linkurl("goods_list",Utils.ObjectToStr(cdr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(cdr["title"]) + "</a>\r\n            ");
	}	//end for if

	}	//end for if

	templateBuilder.Append("\r\n        </dd>\r\n      </dl>\r\n      \r\n    </div>\r\n      \r\n    <div class=\"img-list2\">\r\n      <ul>\r\n        ");
	DataTable goodsList = get_article_list(channel, category_id, pagesize, page, "status=0", out totalcount, out pagelist, "goods_list", category_id, "__id__");

	templateBuilder.Append("<!--取得一个分页DataTable-->\r\n        ");
	foreach(DataRow dr in goodsList.Rows)
	{

	templateBuilder.Append("\r\n        <li>\r\n          <div class=\"wrap-box\">\r\n            <div class=\"img-box\">\r\n              <a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">\r\n                ");
	if (Utils.ObjectToStr(dr["is_red"])=="1")
	{

	templateBuilder.Append("\r\n                <div class=\"abs-txt\">推荐</div>\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n                <img src=\"" + Utils.ObjectToStr(dr["img_url"]) + "\" />\r\n              </a>\r\n            </div>\r\n            <div class=\"info\">\r\n              <h3><a title=\"" + Utils.ObjectToStr(dr["title"]) + "\" href=\"");
	templateBuilder.Append(linkurl("goods_show",Utils.ObjectToStr(dr["id"])));

	templateBuilder.Append("\">" + Utils.ObjectToStr(dr["title"]) + "</a></h3>\r\n              <div class=\"col\">\r\n                <b>￥" + Utils.ObjectToStr(dr["market_price"]) + "</b>元\r\n              </div>\r\n              <div class=\"col\">\r\n                <i>库存：" + Utils.ObjectToStr(dr["stock_quantity"]) + "件</i>\r\n                市场价：<s>" + Utils.ObjectToStr(dr["sell_price"]) + "</s>\r\n              </div>\r\n            </div>\r\n          </div>\r\n        </li>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n      </ul>\r\n    </div>\r\n    \r\n    <!--页码列表-->\r\n    <div class=\"page-box\">\r\n      <div class=\"digg\">");
	templateBuilder.Append(Utils.ObjectToStr(pagelist));
	templateBuilder.Append("</div>\r\n    </div>\r\n    <!--/页码列表-->\r\n    \r\n  </div>\r\n</div>\r\n\r\n<!--Footer-->\r\n");

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
