using System;
using System.Xml;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.API.Payment.tenpaypc
{
	/// <summary>
	/// TenpayUtil 的摘要说明。
	/// </summary>
	public class TenpayUtil
	{
        public string tenpay = "1";
        public string partner = ""; //财付通商户号
        public string key  = ""; //财付通密钥;
        public string type = "1"; //接口类型1即时到帐2担保交易
        public string return_url = ""; //显示支付通知页面;
        public string notify_url = ""; //支付完成后的回调处理页面;

        public TenpayUtil()
        {
            //读取XML配置信息
            string fullPath = Utils.GetMapPath("~/xmlconfig/tenpaypc.config");
            XmlDocument doc = new XmlDocument();
            doc.Load(fullPath);
            XmlNode _partner = doc.SelectSingleNode(@"Root/partner");
            XmlNode _key = doc.SelectSingleNode(@"Root/key");
            XmlNode _type = doc.SelectSingleNode(@"Root/type");
            XmlNode _return_url = doc.SelectSingleNode(@"Root/return_url");
            XmlNode _notify_url = doc.SelectSingleNode(@"Root/notify_url");
            //读取站点配置信息
            Model.siteconfig model = new BLL.siteconfig().loadConfig();

            partner = _partner.InnerText;
            key = _key.InnerText;
            type = _type.InnerText;
            return_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + _return_url.InnerText;
            notify_url = "http://" + HttpContext.Current.Request.Url.Authority.ToLower() + _notify_url.InnerText;
        }

		/** 对字符串进行URL编码 */
		public string UrlEncode(string instr, string charset)
		{
			//return instr;
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlEncode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;
			}
		}

		/** 对字符串进行URL解码 */
		public string UrlDecode(string instr, string charset)
		{
			if(instr == null || instr.Trim() == "")
				return "";
			else
			{
				string res;
				
				try
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding(charset));

				}
				catch (Exception ex)
				{
					res = HttpUtility.UrlDecode(instr,Encoding.GetEncoding("GB2312"));
				}
				
		
				return res;

			}
		}

		/** 取时间戳生成随即数,替换交易单号中的后10位流水号 */
		public UInt32 UnixStamp()
		{
			TimeSpan ts = DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
			return Convert.ToUInt32(ts.TotalSeconds);
		}
		/** 取随机数 */
		public string BuildRandomStr(int length) 
		{
			Random rand = new Random();

			int num = rand.Next();

			string str = num.ToString();

			if(str.Length > length)
			{
				str = str.Substring(0,length);
			}
			else if(str.Length < length)
			{
				int n = length - str.Length;
				while(n > 0)
				{
					str.Insert(0, "0");
					n--;
				}
			}
			
			return str;
		}
	}
}