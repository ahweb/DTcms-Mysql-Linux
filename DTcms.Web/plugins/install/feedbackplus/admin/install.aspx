<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="install.aspx.cs" Inherits="DTcms.Web.Plugin.FeedbackPlus.admin.install" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>报名配置</title>
<link href="../../../<%=siteConfig.webmanagepath %>/skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../../../<%=siteConfig.webmanagepath %>/js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
    });
</script>
</head>
<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="index.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../../../<%=siteConfig.webmanagepath %>/center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>插件管理</span>
  <i class="arrow"></i>
  <span>在线留言</span>
  <i class="arrow"></i>
  <span>参数配置</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">参数配置</a></li>
      </ul>
    </div>
  </div>
</div>
<div class="tab-content">
    <dl>
        <dt>留言内容通知</dt>
        <dd>
          <div class="rule-multi-radio">
            <asp:RadioButtonList ID="rblBookMsg" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Value="0" Selected="True">关闭通知</asp:ListItem>
            <asp:ListItem Value="1">短信通知</asp:ListItem>
            <asp:ListItem Value="2">邮件通知</asp:ListItem>
            </asp:RadioButtonList>
          </div>
        </dd>
    </dl>
    <dl>
        <dt>模板别名</dt>
        <dd>
          <asp:TextBox ID="txtBookTemplet" runat="server" CssClass="input normal" datatype="*" sucmsg=" " />
          <span class="Validform_checktip">*通知模板调用别名</span>
        </dd>
    </dl>
    <dl>
        <dt>接收邮箱或手机</dt>
        <dd>
          <asp:TextBox ID="txtReceive" runat="server" CssClass="input normal" datatype="*" sucmsg=" " />
          <span class="Validform_checktip">*多个以分号隔开</span>
        </dd>
   </dl>
</div>
<!--/内容-->

<!--工具栏-->
<div class="page-footer">
  <div class="btn-wrap">
    <asp:Button ID="btnSubmit" runat="server" Text="提交保存" CssClass="btn" onclick="btnSubmit_Click" />
    <input name="btnReturn" type="button" value="返回上一页" class="btn yellow" onclick="javascript:history.back(-1);" />
  </div>
</div>
<!--/工具栏-->
</form>
</body>
</html>