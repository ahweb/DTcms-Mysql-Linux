using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.tenpaypc;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpaypc
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            TenpayUtil xmlConfig = new TenpayUtil();
            //创建ResponseHandler实例
            ResponseHandler resHandler = new ResponseHandler(Context);
            resHandler.setKey(xmlConfig.key);

            //判断签名
            if (resHandler.isTenpaySign())
            {
                //通知id
                string notify_id = resHandler.getParameter("notify_id");
                //商户订单号
                string out_trade_no = resHandler.getParameter("out_trade_no").ToUpper();
                //财付通订单号
                string transaction_id = resHandler.getParameter("transaction_id");
                //金额,以分为单位
                string total_fee = resHandler.getParameter("total_fee");
                //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                string discount = resHandler.getParameter("discount");
                //支付结果
                string trade_state = resHandler.getParameter("trade_state");
                //交易模式，1即时到账，2中介担保
                string trade_mode = resHandler.getParameter("trade_mode");

                if ("0".Equals(trade_state))
                {
                    //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                    Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=succeed&order_no=" + out_trade_no));
                    return;
                }
                else
                {
                    //失败状态
                    Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=error"));
                    return;
                }
            }
            else
            {
                //认证签名失败
                Response.Redirect(new Web.UI.BasePage().linkurl("payment", "?action=error"));
            }

        }
    }
}