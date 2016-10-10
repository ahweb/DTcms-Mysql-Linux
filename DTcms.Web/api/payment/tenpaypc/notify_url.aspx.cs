using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.Payment.tenpaypc;
using DTcms.Common;

namespace DTcms.Web.api.payment.tenpaypc
{
    public partial class notify_url : System.Web.UI.Page
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
                ///通知id
                string notify_id = resHandler.getParameter("notify_id");
                //通过通知ID查询，确保通知来至财付通
                //创建查询请求
                RequestHandler queryReq = new RequestHandler(Context);
                queryReq.init();
                queryReq.setKey(xmlConfig.key);
                queryReq.setGateUrl("https://gw.tenpay.com/gateway/simpleverifynotifyid.xml");
                queryReq.setParameter("partner", xmlConfig.partner);
                queryReq.setParameter("notify_id", notify_id);

                //通信对象
                TenpayHttpClient httpClient = new TenpayHttpClient();
                httpClient.setTimeOut(5);
                //设置请求内容
                httpClient.setReqContent(queryReq.getRequestURL());
                //后台调用
                if (httpClient.call())
                {
                    //设置结果参数
                    ClientResponseHandler queryRes = new ClientResponseHandler();
                    queryRes.setContent(httpClient.getResContent());
                    queryRes.setKey(xmlConfig.key);
                    //判断签名及结果
                    //只有签名正确,retcode为0，trade_state为0才是支付成功
                    if (queryRes.isTenpaySign())
                    {
                        //取结果参数做业务处理
                        string order_no = resHandler.getParameter("out_trade_no").ToUpper();
                        //财付通订单号
                        string trade_no = resHandler.getParameter("transaction_id");
                        //金额,以分为单位
                        string total_fee = resHandler.getParameter("total_fee");
                        //如果有使用折扣券，discount有值，total_fee+discount=原请求的total_fee
                        string discount = resHandler.getParameter("discount");
                        //支付结果
                        string trade_state = resHandler.getParameter("trade_state");
                        //交易模式，1即时到帐 2中介担保
                        string trade_mode = resHandler.getParameter("trade_mode");

                        //判断签名及结果
                        if ("0".Equals(queryRes.getParameter("retcode")))
                        {
                            if ("1".Equals(trade_mode))
                            {
                                #region 即时到账处理方法====================================
                                if ("0".Equals(trade_state))
                                {
                                    if (order_no.StartsWith("R")) //充值订单
                                    {
                                        BLL.user_recharge bll = new BLL.user_recharge();
                                        Model.user_recharge model = bll.GetModel(order_no);
                                        if (model == null)
                                        {
                                            Response.Write("该订单号不存在");
                                            return;
                                        }
                                        if (model.status == 1) //已成功
                                        {
                                            Response.Write("success");
                                            return;
                                        }
                                        if (model.amount != (decimal.Parse(total_fee) / 100))
                                        {
                                            Response.Write("订单金额和支付金额不相符");
                                            return;
                                        }
                                        bool result = bll.Confirm(order_no);
                                        if (!result)
                                        {
                                            Response.Write("修改订单状态失败");
                                            return;
                                        }
                                    }
                                    else if (order_no.StartsWith("B")) //商品订单
                                    {
                                        BLL.orders bll = new BLL.orders();
                                        Model.orders model = bll.GetModel(order_no);
                                        if (model == null)
                                        {
                                            Response.Write("该订单号不存在");
                                            return;
                                        }
                                        if (model.payment_status == 2) //已付款
                                        {
                                            Response.Write("success");
                                            return;
                                        }
                                        if (model.order_amount != (decimal.Parse(total_fee) / 100))
                                        {
                                            Response.Write("订单金额和支付金额不相符");
                                            return;
                                        }
                                        bool result = bll.UpdateField(order_no, "trade_no='" + trade_no + "',status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
                                        if (!result)
                                        {
                                            Response.Write("修改订单状态失败");
                                            return;
                                        }
                                        //扣除积分
                                        if (model.point < 0)
                                        {
                                            new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
                                        }
                                    }
                                    //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                                    Response.Write("success");
                                }
                                else
                                {
                                    Response.Write("即时到账支付失败");
                                }
                                #endregion
                            }
                            else if ("2".Equals(trade_mode)) //担保交易
                            {
                                #region 担保交易处理方法====================================
                                if ("0".Equals(trade_state)) //付款成功
                                {
                                    if (order_no.StartsWith("R")) //充值订单
                                    {
                                        BLL.user_recharge bll = new BLL.user_recharge();
                                        Model.user_recharge model = bll.GetModel(order_no);
                                        if (model == null)
                                        {
                                            Response.Write("该订单号不存在");
                                            return;
                                        }
                                        if (model.status == 1) //已成功
                                        {
                                            Response.Write("success");
                                            return;
                                        }
                                        if (model.amount != (decimal.Parse(total_fee) / 100))
                                        {
                                            Response.Write("订单金额和支付金额不相符");
                                            return;
                                        }
                                        bool result = bll.Confirm(order_no);
                                        if (!result)
                                        {
                                            Response.Write("修改订单状态失败");
                                            return;
                                        }
                                    }
                                    else if (order_no.StartsWith("B")) //商品订单
                                    {
                                        BLL.orders bll = new BLL.orders();
                                        Model.orders model = bll.GetModel(order_no);
                                        if (model == null)
                                        {
                                            Response.Write("该订单号不存在");
                                            return;
                                        }
                                        if (model.payment_status == 2) //已付款
                                        {
                                            Response.Write("success");
                                            return;
                                        }
                                        if (model.order_amount != (decimal.Parse(total_fee) / 100))
                                        {
                                            Response.Write("订单金额和支付金额不相符");
                                            return;
                                        }
                                        bool result = bll.UpdateField(order_no, "trade_no='" + trade_no + "',status=2,payment_status=2,payment_time='" + DateTime.Now + "'");
                                        if (!result)
                                        {
                                            Response.Write("修改订单状态失败");
                                            return;
                                        }
                                        //扣除积分
                                        if (model.point < 0)
                                        {
                                            new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "换购扣除积分，订单号：" + model.order_no, false);
                                        }
                                    }
                                }
                                else if ("5".Equals(trade_state)) //买家收货确认，交易成功
                                {
                                    if (order_no.StartsWith("B")) //商品订单
                                    {
                                        BLL.orders bll = new BLL.orders();
                                        Model.orders model = bll.GetModel(order_no);
                                        if (model == null)
                                        {
                                            Response.Write("该订单号不存在");
                                            return;
                                        }
                                        if (model.status > 2) //订单状态已经完成结束
                                        {
                                            Response.Write("success");
                                            return;
                                        }
                                        if (model.order_amount != decimal.Parse(total_fee))
                                        {
                                            Response.Write("订单金额和支付金额不相符");
                                            return;
                                        }
                                        bool result = bll.UpdateField(order_no, "status=3,complete_time='" + DateTime.Now + "'");
                                        if (!result)
                                        {
                                            Response.Write("修改订单状态失败");
                                            return;
                                        }
                                        //给会员增加积分检查升级
                                        if (model.user_id > 0 && model.point > 0)
                                        {
                                            new BLL.user_point_log().Add(model.user_id, model.user_name, model.point, "购物获得积分，订单号：" + model.order_no, true);
                                        }
                                    }
                                }
                                //给财付通系统发送成功信息，财付通系统收到此结果后不再进行后续通知
                                Response.Write("success");
                                #endregion
                            }
                        }
                        else
                        {
                            Response.Write("查询验证签名失败或id验证失败");
                        }
                    }
                    else
                    {
                        Response.Write("通知ID查询签名验证失败");
                    }
                }
                else
                {
                    Response.Write("后台调用通信失败");
                }
            }
            else
            {
                Response.Write("签名验证失败");
            }
            Response.End();
        }
    }
}