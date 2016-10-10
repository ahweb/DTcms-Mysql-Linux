<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="plugin_list.aspx.cs" Inherits="DTcms.Web.admin.settings.plugin_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>插件设置</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
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
  <span>系统管理</span>
  <i class="arrow"></i>
  <span>插件设置</span>
</div>
<!--/导航栏-->

<!--工具栏-->
<div id="floatHead" class="toolbar-wrap">
  <div class="toolbar">
    <div class="box-wrap">
      <a class="menu-btn"></a>
      <div class="l-list">
        <ul class="icon-list">
          <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
          <li><asp:LinkButton ID="lbtnInstall" runat="server" CssClass="save" OnClientClick="return ExePostBack('lbtnInstall','已安装的插件不执行重复安装，是否继续？');" onclick="lbtnInstall_Click"><i></i><span>安装</span></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnUnInstall" runat="server" CssClass="del" OnClientClick="return ExePostBack('lbtnUnInstall','卸载插件不会删除插件目录，是否继续？');" onclick="lbtnUnInstall_Click"><i></i><span>卸载</span></asp:LinkButton></li>
          <li><asp:LinkButton ID="lbtnRemark" runat="server" CssClass="folder" OnClientClick="return CheckPostBack('lbtnRemark');" onclick="lbtnRemark_Click"><i></i><span>生成模板</span></asp:LinkButton></li>
        </ul>
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
      <th align="left" width="20%">插件名称</th>
      <th align="left" width="12%">目录</th>
      <th align="left" width="12%">作者</th>
      <th align="left" width="10%">版本号</th>
      <th align="left">备注</th>
      <th width="10%">状态</th>
    </tr>
  </HeaderTemplate>
  <ItemTemplate>
    <tr>
      <td align="center">
        <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" />
        <asp:HiddenField ID="hidDirName" Value='<%#Eval("directory")%>' runat="server" />
      </td>
      <td><%#Eval("name")%></td>
      <td><%#Eval("directory")%></td>
      <td><%#Eval("author")%></td>
      <td><%#Eval("version")%></td>
      <td><%#Eval("description")%></td>
      <td align="center">
        <%#Convert.ToInt32(Eval("isload")) == 1 ? "已安装" : "未安装"%>
      </td>
    </tr>
  </ItemTemplate>
  <FooterTemplate>
    <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"7\">暂无记录</td></tr>" : ""%>
  </table>
  </FooterTemplate>
  </asp:Repeater>
</div>
<!--/列表-->

</form>
</body>
</html>
