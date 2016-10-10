<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_audit.aspx.cs" Inherits="DTcms.Web.admin.users.user_audit" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>会员审核</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="javascript:history.back(-1);" class="back"><i></i><span>返回上一页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <span>会员管理</span>
  <i class="arrow"></i>
  <span>审核用户</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><a class="add" href="user_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>
          <li><asp:LinkButton ID="btnAudit" runat="server" CssClass="lock" OnClientClick="return ExePostBack('btnAudit','审核通过后会员正常使用，是否继续？');" onclick="btnAudit_Click"><i></i><span>审核通过</span></asp:LinkButton></li>
        </ul>
      </div>
      <div class="r-list">
        <asp:TextBox ID="txtKeywords" runat="server" CssClass="keyword" />
        <asp:LinkButton ID="lbtnSearch" runat="server" CssClass="btn-search" onclick="btnSearch_Click">查询</asp:LinkButton>
      </div>
    </div>
  </div>
</div>
<!--/工具栏-->

<!--列表-->
<div class="table-container">
<asp:Repeater ID="rptList" runat="server">
<HeaderTemplate>
<table width="100%" border="0" cellspacing="0" cellpadding="0" class="ltable">
  <tr>
    <th width="8%">选择</th>
    <th align="left">用户名</th>
    <th align="left" width="12%">会员组</th>
    <th align="left" width="12%">手机</th>
    <th align="left" width="12%">邮箱</th>
    <th width="8%">余额</th>
    <th width="8%">积分</th>
    <th width="8%">状态</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
    </td>
    <td>
      <div class="user-box" style="padding-left:0;">
        <h4><b><a href="user_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("user_name")%></a></b> (昵称：<%#Eval("nick_name")%>)</h4>
        <i>注册时间：<%#string.Format("{0:g}",Eval("reg_time"))%></i>
      </div>
    </td>
    <td><%#new DTcms.BLL.user_groups().GetTitle(Convert.ToInt32(Eval("group_id")))%></td>
    <td><%#Eval("mobile").ToString() != "" ? "-" : Eval("mobile")%></td>
    <td><%#Eval("email")%></td>
    <td align="center"><%#Eval("amount")%></td>
    <td align="center"><%#Eval("point")%></td>
    <td align="center"><%#GetUserStatus(Convert.ToInt32(Eval("status")))%></td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"8\">暂无会员需要审核</td></tr>" : ""%>
</table>
</FooterTemplate>
</asp:Repeater>
</div>
<!--/列表-->

<!--内容底部-->
<div class="line20"></div>
<div class="pagelist">
  <div class="l-btns">
    <span>显示</span><asp:TextBox ID="txtPageNum" runat="server" CssClass="pagenum" onkeydown="return checkNumber(event);"
                OnTextChanged="txtPageNum_TextChanged" AutoPostBack="True"></asp:TextBox><span>条/页</span>
  </div>
  <div id="PageContent" runat="server" class="default"></div>
</div>
<!--/内容底部-->
</form>
</body>
</html>
