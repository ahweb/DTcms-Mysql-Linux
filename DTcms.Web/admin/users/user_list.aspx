<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user_list.aspx.cs" Inherits="DTcms.Web.admin.users.user_list" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>会员管理</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<link href="../../css/pagination.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    //发送短信
    function PostSMS(mobile) {
        var mobiles = "";
        if (arguments.length == 1) { //如果有传入值
            mobiles = mobile;
        } else {
            lenNum = $(".checkall input:checked").length;
            $(".checkall input:checked").each(function (i) {
                if ($(this).parent().siblings('input[name="hidMobile"]').val() != "") {
                    mobiles += $(this).parent().siblings('input[name="hidMobile"]').val();
                    if (i < lenNum - 1) {
                        mobiles += ',';
                    }
                }
            });
        }
        if (mobiles == "") {
            top.dialog({
                title: '提示',
                content: '对不起，手机号码不能为空！',
                okValue: '确定',
                ok: function () { }
            }).showModal();
            return false;
        }
        var smsdialog = parent.dialog({
            title: '发送手机短信',
            content: '<textarea id="txtSmsContent" name="txtSmsContent" rows="2" cols="20" class="input"></textarea>',
            okValue: '确定',
            ok: function () {
                var remark = $("#txtSmsContent", parent.document).val();
                if (remark == "") {
                    top.dialog({
                        title: '提示',
                        content: '对不起，请输入手机短信内容！',
                        okValue: '确定',
                        ok: function () { }
                    }).showModal(smsdialog);
                    return false;
                }
                var postData = { "mobiles": mobiles, "content": remark };
                //发送AJAX请求
                $.ajax({
                    type: "post",
                    url: "../../tools/admin_ajax.ashx?action=sms_message_post",
                    data: postData,
                    dataType: "json",
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        top.dialog({
                            title: '提示',
                            content: '尝试发送失败，错误信息：' + errorThrown,
                            okValue: '确定',
                            ok: function () { }
                        }).showModal(smsdialog);
                    },
                    success: function (data, textStatus) {
                        if (data.status == 1) {
                            smsdialog.close().remove();
                            var d = top.dialog({ content: data.msg }).show();
                            setTimeout(function () {
                                d.close().remove();
                                location.reload();
                            }, 2000);
                        } else {
                            top.dialog({
                                title: '提示',
                                content: '错误提示：' + data.msg,
                                okValue: '确定',
                                ok: function () { }
                            }).showModal(smsdialog);
                        }
                    }
                });
                return false;
            },
            cancelValue: '取消',
            cancel: function () { }
        }).showModal();
    }
</script>
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
  <span>会员列表</span>
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
          <li><a class="msg" href="javascript:;" onclick="PostSMS();"><i></i><span>短信</span></a></li>
          <li><a class="all" href="javascript:;" onclick="checkAll(this);"><i></i><span>全选</span></a></li>
          <li><asp:LinkButton ID="btnDelete" runat="server" CssClass="del" OnClientClick="return ExePostBack('btnDelete');" onclick="btnDelete_Click"><i></i><span>删除</span></asp:LinkButton></li>
        </ul>
        <div class="menu-list">
          <div class="rule-single-select">
            <asp:DropDownList ID="ddlGroupId" runat="server" AutoPostBack="True" onselectedindexchanged="ddlGroupId_SelectedIndexChanged"></asp:DropDownList>
          </div>
        </div>
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
    <th align="left" colspan="2">用户名</th>
    <th align="left" width="12%">会员组</th>
    <th align="left" width="12%">邮箱</th>
    <th width="8%">余额</th>
    <th width="8%">积分</th>
    <th width="8%">状态</th>
    <th width="8%">操作</th>
  </tr>
</HeaderTemplate>
<ItemTemplate>
  <tr>
    <td align="center">
      <asp:CheckBox ID="chkId" CssClass="checkall" runat="server" style="vertical-align:middle;" />
      <asp:HiddenField ID="hidId" Value='<%#Eval("id")%>' runat="server" />
      <input name="hidMobile" type="hidden" value="<%#Eval("mobile")%>" />
    </td>
    <td width="64">
      <a href="user_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">
        <%#Eval("avatar").ToString() != "" ? "<img width=\"64\" height=\"64\" src=\"" + Eval("avatar") + "\" />" : "<b class=\"user-avatar\"></b>"%>
      </a>
    </td>
    <td>
      <div class="user-box">
        <h4><b><%#Eval("user_name")%></b> (昵称：<%#Eval("nick_name")%>)</h4>
        <i>注册时间：<%#string.Format("{0:g}",Eval("reg_time"))%></i>
        <span>
          <a class="amount" href="amount_log.aspx?keywords=<%#Eval("user_name")%>" title="消费记录">余额</a>
          <a class="card" href="recharge_list.aspx?keywords=<%#Eval("user_name")%>" title="充值记录">充值</a>
          <a class="point" href="point_log.aspx?keywords=<%#Eval("user_name")%>" title="积分记录">积分</a>
          <a class="msg" href="message_list.aspx?keywords=<%#Eval("user_name")%>" title="消息记录">短消息</a>
          <%#Eval("mobile").ToString() != "" ? "<a class=\"sms\" href=\"javascript:;\" onclick=\"PostSMS('" + Eval("mobile") + "');\" title=\"发送手机短信通知\">短信通知</a>" : ""%>
        </span>
      </div>
    </td>
    <td><%#new DTcms.BLL.user_groups().GetTitle(Convert.ToInt32(Eval("group_id")))%></td>
    <td><%#Eval("email")%></td>
    <td align="center"><%#Eval("amount")%></td>
    <td align="center"><%#Eval("point")%></td>
    <td align="center"><%#GetUserStatus(Convert.ToInt32(Eval("status")))%></td>
    <td align="center"><a href="user_edit.aspx?action=<%#DTEnums.ActionEnum.Edit %>&id=<%#Eval("id")%>">修改</a></td>
  </tr>
</ItemTemplate>
<FooterTemplate>
  <%#rptList.Items.Count == 0 ? "<tr><td align=\"center\" colspan=\"9\">暂无记录</td></tr>" : ""%>
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