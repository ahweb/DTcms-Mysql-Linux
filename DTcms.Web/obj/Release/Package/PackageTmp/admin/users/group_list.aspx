<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="group_list.aspx.cs" Inherits="DTcms.Web.admin.users.group_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>会员组管理</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
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
  <span>会员组别</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><a class="add" href="group_edit.aspx?action=<%=DTEnums.ActionEnum.Add %>"><i></i><span>新增</span></a></li>
          <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
          <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
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
        <th align="left">组别名称</th>
        <th width="8%">等级值</th>
        <th width="12%">升级积分</th>
        <th width="12%">初始金额</th>
        <th width="12%">初始积分</th>
        <th width="9%">购物折扣</th>
        <th width="8%">注册组</th>
        <th width="6%">状态</th>
        <th width="10%">操作</th>
      </tr>
      </HeaderTemplate>
      <ItemTemplate>
      <tr>
        <td align="center">
          <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" />
          <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
        </td>
        <td>
          <a href="group_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>"><%#Eval("title")%></a>
        </td>
        <td align="center"><%#Eval("grade")%></td>
        <td align="center"><%#Eval("upgrade_exp")%></td>
        <td align="center"><%#Eval("amount")%></td>
        <td align="center"><%#Eval("point")%></td>
        <td align="center"><%#Eval("discount")%>%</td>
        <td align="center"><%#Convert.ToInt32(Eval("is_default")) == 1 ? "√" : "×"%></td>
        <td align="center"><%#Convert.ToInt32(Eval("is_lock")) == 1 ? "禁用" : "启用"%></td>
        <td align="center">
          <a href="group_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a>
        </td>
      </tr>
      </ItemTemplate>
      <FooterTemplate>
      <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"10\">暂无记录</td></tr>" : ""%>
      </table>
    </FooterTemplate>
  </asp:Repeater>
</div>
<!--/列表-->

</form>
</body>
</html>
