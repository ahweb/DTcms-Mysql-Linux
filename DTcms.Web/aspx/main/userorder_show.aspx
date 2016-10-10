<%@ Page Language="C#" AutoEventWireup="true" Inherits="DTcms.Web.UI.Page.userorder_show" ValidateRequest="false" %>
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

	templateBuilder.Append("<!DOCTYPE html PUBLIC \"-//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">\r\n<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />\r\n<title>查看订单 - ");
	templateBuilder.Append(Utils.ObjectToStr(site.name));
	templateBuilder.Append("</title>\r\n<meta name=\"keywords\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_keyword));
	templateBuilder.Append("\" />\r\n<meta name=\"description\" content=\"");
	templateBuilder.Append(Utils.ObjectToStr(site.seo_description));
	templateBuilder.Append("\" />\r\n<link href=\"");
	templateBuilder.Append("/templates/main");
	templateBuilder.Append("/css/style.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<link href=\"");
	templateBuilder.Append(Utils.ObjectToStr(config.webpath));
	templateBuilder.Append("scripts/artdialog/ui-dialog.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n<script type=\"text/javascript\" charset=\"utf-8\" src=\"");
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


	templateBuilder.Append("\r\n    <!--/左侧导航-->\r\n    \r\n    <!--右侧内容-->\r\n    <div class=\"home-box\">\r\n      <!--查看订单-->\r\n      <div class=\"u-tab-head\">\r\n        <p>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("userorder","list"));

	templateBuilder.Append("\">交易订单</a>\r\n          <a href=\"");
	templateBuilder.Append(linkurl("userorder","close"));

	templateBuilder.Append("\">已关闭订单</a>\r\n        </p>\r\n      </div>\r\n      <div class=\"u-tab-content\">\r\n        <div class=\"title-div\">\r\n          <strong>查看订单</strong>\r\n        </div>\r\n        \r\n        ");
	if (model.status<4)
	{

	if (model.payment_status>0)
	{

	templateBuilder.Append("\r\n        <div class=\"step-box\">\r\n        ");
	}
	else
	{

	templateBuilder.Append("\r\n        <div class=\"step-box mini\">\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n          <ul>\r\n            <!--下单-->\r\n            <li class=\"first done\">\r\n              <div class=\"progress\">\r\n                <span class=\"text\">下单</span>\r\n              </div>\r\n              <div class=\"info\">\r\n                ");
	templateBuilder.Append(Utils.ObjectToStr(model.add_time));
	templateBuilder.Append("\r\n              </div>\r\n            </li>\r\n            <!--/下单-->\r\n            \r\n            ");
	if (model.payment_status>0)
	{

	templateBuilder.Append("\r\n            <!--付款-->\r\n            ");
	if (model.payment_status==2)
	{

	templateBuilder.Append("\r\n              <li class=\"done\">\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n              <li>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n              <div class=\"progress\">\r\n                <span class=\"text\">付款</span>\r\n              </div>\r\n              <div class=\"info\">\r\n                ");
	if (model.payment_status==2)
	{

	templateBuilder.Append("\r\n                ");
	templateBuilder.Append(Utils.ObjectToStr(model.payment_time));
	templateBuilder.Append("\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n              </div>\r\n            </li>\r\n            <!--/付款-->\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n            \r\n            <!--确认-->\r\n            ");
	if (model.status>=2)
	{

	templateBuilder.Append("\r\n            <li class=\"done\">\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <li>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n              <div class=\"progress\">\r\n                <span class=\"text\">确认</span>\r\n              </div>\r\n              <div class=\"info\">\r\n                ");
	if (model.status>=2)
	{

	templateBuilder.Append("\r\n                ");
	templateBuilder.Append(Utils.ObjectToStr(model.confirm_time));
	templateBuilder.Append("\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n              </div>\r\n            </li>\r\n            <!--/确认-->\r\n            \r\n            <!--发货-->\r\n            ");
	if (model.express_status==2)
	{

	templateBuilder.Append("\r\n            <li class=\"done\">\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <li>\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n              <div class=\"progress\">\r\n                <span class=\"text\">发货</span>\r\n              </div>\r\n              <div class=\"info\">\r\n                ");
	if (model.express_status==2)
	{

	templateBuilder.Append("\r\n                ");
	templateBuilder.Append(Utils.ObjectToStr(model.express_time));
	templateBuilder.Append("\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n              </div>\r\n            </li>\r\n            <!--发货-->\r\n            \r\n            <!--完成-->\r\n            ");
	if (model.status>2)
	{

	templateBuilder.Append("\r\n            <li class=\"last done\">\r\n            ");
	}
	else
	{

	templateBuilder.Append("\r\n            <li class=\"last\">\r\n            ");
	}	//end for if

	templateBuilder.Append("\r\n              <div class=\"progress\">\r\n                <span class=\"text\">完成</span>\r\n              </div>\r\n              <div class=\"info\">\r\n                ");
	if (model.status>2)
	{

	templateBuilder.Append("\r\n                ");
	templateBuilder.Append(Utils.ObjectToStr(model.complete_time));
	templateBuilder.Append("\r\n                ");
	}	//end for if

	templateBuilder.Append("\r\n              </div>\r\n            </li>\r\n            <!--完成-->\r\n          </ul>\r\n        </div>\r\n        <div class=\"line20\"></div>\r\n        ");
	}	//end for if

	templateBuilder.Append("\r\n\r\n        <div class=\"form-box accept-box\">\r\n          <dl class=\"head\">\r\n            <dd>\r\n              订单号：");
	templateBuilder.Append(Utils.ObjectToStr(model.order_no));
	templateBuilder.Append("\r\n              ");
	if (get_order_payment_status(model.id))
	{

	templateBuilder.Append("\r\n              <a class=\"btn-pay\" href=\"");
	templateBuilder.Append(linkurl("payment","confirm",model.order_no));

	templateBuilder.Append("\">去付款</a>\r\n              ");
	}	//end for if

	templateBuilder.Append("\r\n            </dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>订单状态：</dt>\r\n            <dd>\r\n              ");
	templateBuilder.Append(get_order_status(model.id).ToString());

	templateBuilder.Append("\r\n            </dd>\r\n          </dl>\r\n          ");
	if (model.payment_status>0)
	{

	templateBuilder.Append("\r\n          <dl>\r\n            <dt>支付方式：</dt>\r\n            <dd>");
	templateBuilder.Append(get_payment_title(model.payment_id).ToString());

	templateBuilder.Append("</dd>\r\n          </dl>\r\n          ");
	}	//end for if

	if (model.express_status==2)
	{

	templateBuilder.Append("\r\n          <dl>\r\n            <dt>发货单号：</dt>\r\n            <dd>");
	templateBuilder.Append(get_express_title(model.express_id).ToString());

	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.express_no));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </div>\r\n              \r\n        <div class=\"line10\"></div>\r\n        <table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"5\" class=\"ftable\">\r\n          <tr>\r\n            <th align=\"left\" colspan=\"2\">商品信息</th>\r\n            <th width=\"10%\">单价</td>\r\n            <th width=\"10%\">积分</th>\r\n            <th width=\"10%\">数量</th>\r\n            <th width=\"10%\">金额</th>\r\n            <th width=\"10%\">积分</th>\r\n          </tr>\r\n          ");
	if (model.order_goods!=null)
	{

	foreach(DTcms.Model.order_goods modelt in model.order_goods)
	{

	templateBuilder.Append("\r\n          <tr>\r\n            <td width=\"60\">\r\n              <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">\r\n                <img src=\"");
	templateBuilder.Append(get_article_img_url(modelt.article_id).ToString());

	templateBuilder.Append("\" class=\"img\" />\r\n              </a>\r\n            </td>\r\n            <td align=\"left\">\r\n              <a target=\"_blank\" href=\"");
	templateBuilder.Append(linkurl("goods_show",modelt.article_id));

	templateBuilder.Append("\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.goods_title));
	templateBuilder.Append("</a>\r\n              <p>");
	templateBuilder.Append(Utils.ObjectToStr(modelt.spec_text));
	templateBuilder.Append("</p>\r\n            </td>\r\n            <td align=\"center\">\r\n              <s>￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.goods_price));
	templateBuilder.Append("</s>\r\n              <p>￥");
	templateBuilder.Append(Utils.ObjectToStr(modelt.real_price));
	templateBuilder.Append("</p>\r\n            </td>\r\n            <td align=\"center\">\r\n              ");
	if (modelt.point>0)
	{

	templateBuilder.Append("\r\n              +\r\n              ");
	}	//end for if

	templateBuilder.Append("\r\n              ");
	templateBuilder.Append(Utils.ObjectToStr(modelt.point));
	templateBuilder.Append("\r\n            </td>\r\n            <td align=\"center\">");
	templateBuilder.Append(Utils.ObjectToStr(modelt.quantity));
	templateBuilder.Append("</td>\r\n            <td align=\"center\">￥");
	templateBuilder.Append((modelt.real_price*modelt.quantity).ToString());

	templateBuilder.Append("</td>\r\n            <td align=\"center\">");
	templateBuilder.Append((modelt.point*modelt.quantity).ToString());

	templateBuilder.Append("</td>\r\n          </tr>\r\n          ");
	}	//end for if

	}
	else
	{

	templateBuilder.Append("\r\n          <tr><td colspan=\"7\" align=\"center\">暂无记录</td></tr>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n          <tr>\r\n            <td colspan=\"7\" align=\"right\">\r\n              <p>商品金额：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.real_amount));
	templateBuilder.Append("</b>&nbsp;&nbsp;+&nbsp;&nbsp;运费：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.express_fee));
	templateBuilder.Append("</b>&nbsp;&nbsp;+ &nbsp;&nbsp;支付手续费：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.payment_fee));
	templateBuilder.Append("</b>&nbsp;&nbsp;税费：<b class=\"red\">");
	templateBuilder.Append(Utils.ObjectToStr(model.invoice_taxes));
	templateBuilder.Append("</b></p>\r\n              <p style=\"font-size:22px;\">应付总金额：<b class=\"red\">￥");
	templateBuilder.Append(Utils.ObjectToStr(model.order_amount));
	templateBuilder.Append("</b></p>\r\n            </td>\r\n          </tr>\r\n        </table>\r\n        \r\n        <div class=\"line10\"></div>\r\n        <div class=\"form-box accept-box\">\r\n          <dl class=\"head\">\r\n            <dd>收货信息</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>顾客姓名：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.accept_name));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>送货地址：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.area));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.address));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.post_code));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>联系电话：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.mobile));
	templateBuilder.Append(" ");
	templateBuilder.Append(Utils.ObjectToStr(model.telphone));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>电子邮箱：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.email));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>备注留言：</dt>\r\n            <dd>");
	templateBuilder.Append(Utils.ObjectToStr(model.message));
	templateBuilder.Append("</dd>\r\n          </dl>\r\n          <dl>\r\n            <dt>开具发票：</dt>\r\n            <dd>\r\n              ");
	if (model.is_invoice==1)
	{

	templateBuilder.Append("\r\n               是\r\n              ");
	}
	else
	{

	templateBuilder.Append("\r\n               否\r\n              ");
	}	//end for if

	templateBuilder.Append("\r\n            </dd>\r\n          </dl>\r\n          ");
	if (model.is_invoice==1)
	{

	templateBuilder.Append("\r\n          <dl>\r\n            <dt>发票抬头：</dt>\r\n            <dd>\r\n              ");
	templateBuilder.Append(Utils.ObjectToStr(model.invoice_title));
	templateBuilder.Append("\r\n            </dd>\r\n          </dl>\r\n          ");
	}	//end for if

	templateBuilder.Append("\r\n        </div>\r\n        \r\n      </div>\r\n      <!--/查看订单-->\r\n    </div>\r\n    <!--/右侧内容-->\r\n  </div>\r\n</div>\r\n\r\n<!--Footer-->\r\n");

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
