<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="role_edit.aspx.cs" Inherits="DTcms.Web.admin.manager.role_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑角色</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>

<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //是否启用权限
        if ($("#ddlRoleType").find("option:selected").attr("value") == 1) {
            $(".border-table").find("input[type='checkbox']").prop("disabled", true);
        }
        $("#ddlRoleType").change(function () {
            if ($(this).find("option:selected").attr("value") == 1) {
                $(".border-table").find("input[type='checkbox']").prop("checked", false);
                $(".border-table").find("input[type='checkbox']").prop("disabled", true);
            } else {
                $(".border-table").find("input[type='checkbox']").prop("disabled", false);
            }
        });
        //权限全选
        $("input[name='checkAll']").click(function () {
            if ($(this).prop("checked") == true) {
                $(this).parent().siblings("td").find("input[type='checkbox']").prop("checked", true);
            } else {
                $(this).parent().siblings("td").find("input[type='checkbox']").prop("checked", false);
            }
        });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="role_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="manager_list.aspx"><span>管理员</span></a>
  <i class="arrow"></i>
  <a href="role_list.aspx"><span>管理角色</span></a>
  <i class="arrow"></i>
  <span>编辑角色</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">编辑角色信息</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>角色类型</dt>
    <dd>
      <div class="rule-single-select">
        <asp:DropDownList id="ddlRoleType" runat="server" datatype="*" errormsg="请选择角色类型！" sucmsg=" "></asp:DropDownList>
      </div>
    </dd>
  </dl>
  <dl>
    <dt>角色名称</dt>
    <dd><asp:TextBox ID="txtRoleName" runat="server" CssClass="input normal" datatype="*1-100" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*角色中文名称，100字符内</span></dd>
  </dl>   
  <dl>
    <dt>管理权限</dt>
    <dd>
      <table border="0" cellspacing="0" cellpadding="0" class="border-table" width="98%">
        <thead>
          <tr>
            <th width="30%">导航名称</th>
            <th>权限分配</th>
            <th width="10%">全选</th>
          </tr>
        </thead>
        <tbody>
          <asp:Repeater ID="rptList" runat="server" onitemdatabound="rptList_ItemDataBound">
          <ItemTemplate>
          <tr>
            <td style="white-space:nowrap;word-break:break-all;overflow:hidden;">
              <asp:HiddenField ID="hidName" Value='<%#Eval("name") %>' runat="server" />
              <asp:HiddenField ID="hidActionType" Value='<%#Eval("action_type") %>' runat="server" />
              <asp:HiddenField ID="hidLayer" Value='<%#Eval("class_layer") %>' runat="server" />
              <asp:Literal ID="LitFirst" runat="server"></asp:Literal>
              <%#Eval("title")%>
            </td>
            <td>
              <asp:CheckBoxList ID="cblActionType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow" CssClass="cbllist"></asp:CheckBoxList>
            </td>
            <td align="center"><input name="checkAll" type="checkbox" /></td>
          </tr>
          </ItemTemplate>
          </asp:Repeater>
        </tbody>
      </table>
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
