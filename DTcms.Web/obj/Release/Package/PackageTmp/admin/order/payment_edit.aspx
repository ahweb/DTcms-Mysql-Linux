<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="payment_edit.aspx.cs" Inherits="DTcms.Web.admin.order.payment_edit" ValidateRequest="false" %>
<%@ Import namespace="DTcms.Common" %>

<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<meta name="viewport" content="width=device-width,minimum-scale=1.0,maximum-scale=1.0,initial-scale=1.0,user-scalable=no" />
<meta name="apple-mobile-web-app-capable" content="yes" />
<title>编辑支付方式</title>
<link href="../../scripts/artdialog/ui-dialog.css" rel="stylesheet" type="text/css" />
<link href="../skin/default/style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/jquery-1.11.2.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/jquery/Validform_v5.3.2_min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/artdialog/dialog-plus-min.js"></script>
<script type="text/javascript" charset="utf-8" src="../../scripts/webuploader/webuploader.min.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/uploader.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/laymain.js"></script>
<script type="text/javascript" charset="utf-8" src="../js/common.js"></script>
<script type="text/javascript">
    $(function () {
        //初始化表单验证
        $("#form1").initValidform();
        //初始化上传控件
        $(".upload-img").InitUploader({ sendurl: "../../tools/upload_ajax.ashx", swf: "../../scripts/webuploader/uploader.swf" });
    });
</script>
</head>

<body class="mainbody">
<form id="form1" runat="server">
<!--导航栏-->
<div class="location">
  <a href="payment_list.aspx" class="back"><i></i><span>返回列表页</span></a>
  <a href="../center.aspx" class="home"><i></i><span>首页</span></a>
  <i class="arrow"></i>
  <a href="payment_list.aspx"><span>支付方式</span></a>
  <i class="arrow"></i>
  <span>编辑支付方式</span>
</div>
<div class="line10"></div>
<!--/导航栏-->

<!--内容-->
<div id="floatHead" class="content-tab-wrap">
  <div class="content-tab">
    <div class="content-tab-ul-wrap">
      <ul>
        <li><a class="selected" href="javascript:;">配置支付方式</a></li>
      </ul>
    </div>
  </div>
</div>

<div class="tab-content">
  <dl>
    <dt>支付名称</dt>
    <dd><asp:TextBox ID="txtTitle" runat="server" CssClass="input normal" datatype="*2-100" sucmsg=" "></asp:TextBox> <span class="Validform_checktip">*</span></dd>
  </dl>
  <dl>
    <dt>支付类型</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">线上付款</asp:ListItem>
        <asp:ListItem Value="2">线下付款</asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <span class="Validform_checktip">*线上付款自动验证付款状态</span>
    </dd>
  </dl>
  <dl>
    <dt>是否启用</dt>
    <dd>
      <div class="rule-single-checkbox">
          <asp:CheckBox ID="cbIsLock" runat="server" />
      </div>
      <span class="Validform_checktip">*不启用则不显示该支付方式</span>
    </dd>
  </dl>
  <dl>
    <dt>排序数字</dt>
    <dd>
      <asp:TextBox ID="txtSortId" runat="server" CssClass="input small" datatype="n" sucmsg=" ">99</asp:TextBox>
      <span class="Validform_checktip">*数字，越小越向前</span>
    </dd>
  </dl>
  <dl>
    <dt>手续费类型</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblPoundageType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">百分比</asp:ListItem>
        <asp:ListItem Value="2">固定金额</asp:ListItem>
        </asp:RadioButtonList>
      </div>
      <span class="Validform_checktip">*百分比的计算公式：商品总金额+(商品总金额*百分比)+配送费用=订单总金额</span>
    </dd>
  </dl>
  <dl>
    <dt>支付手续费</dt>
    <dd>
      <asp:TextBox ID="txtPoundageAmount" runat="server" CssClass="input small" datatype="/^(([1-9]{1}\d*)|([0]{1}))(\.(\d){1,2})?$/" sucmsg=" ">0</asp:TextBox>
      <span class="Validform_checktip">*注意：百分比取值范围：0-100，固定金额单位为“元”</span>
    </dd>
  </dl>
  <%if (model.api_path.ToLower()=="alipaypc")
    { %>
  <dl>
    <dt>支付宝账号</dt>
    <dd>
      <asp:TextBox ID="txtAlipaySellerEmail" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*签约支付宝账号或卖家支付宝帐户</span>
    </dd>
  </dl>
  <dl>
    <dt>合作者身份(partner ID)</dt>
    <dd>
      <asp:TextBox ID="txtAlipayPartner" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*合作身份者ID，以2088开头由16位纯数字组成的字符串</span>
    </dd>
  </dl>
  <dl>
    <dt>交易安全校验码(key)</dt>
    <dd>
      <asp:TextBox ID="txtAlipayKey" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*交易安全检验码，由数字和字母组成的32位字符串</span>
    </dd>
  </dl>
  <dl>
    <dt>接口类型</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblAlipayType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">即时到帐</asp:ListItem>
        <asp:ListItem Value="2">担保交易</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <%}
    else if (model.api_path.ToLower()=="tenpaypc")
    { %>
  <dl>
    <dt>财付通商户号</dt>
    <dd>
      <asp:TextBox ID="txtTenpayBargainorId" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*财付通商家服务商户号</span>
    </dd>
  </dl>
  <dl>
    <dt>财付通密钥</dt>
    <dd>
      <asp:TextBox ID="txtTenpayKey" runat="server" CssClass="input normal" datatype="*" sucmsg=" "></asp:TextBox>
      <span class="Validform_checktip">*财付通商家服务密钥</span>
    </dd>
  </dl>
  <dl>
    <dt>接口类型</dt>
    <dd>
      <div class="rule-multi-radio">
        <asp:RadioButtonList ID="rblTenpayType" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="1" Selected="True">即时到帐</asp:ListItem>
        <asp:ListItem Value="2">担保交易</asp:ListItem>
        </asp:RadioButtonList>
      </div>
    </dd>
  </dl>
  <%} %>
  <dl>
    <dt>显示图标</dt>
    <dd>
      <asp:TextBox ID="txtImgUrl" runat="server" CssClass="input normal upload-path" />
      <div class="upload-box upload-img"></div>
    </dd>
  </dl>
  <dl>
    <dt>描述说明</dt>
    <dd>
      <asp:TextBox ID="txtRemark" runat="server" CssClass="input normal" TextMode="MultiLine" />
      <span class="Validform_checktip">支付方式的描述说明，支持HTML代码</span>
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
