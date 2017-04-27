using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.IO;
using log4net;

namespace DTcms.Web
{
    public class Global : System.Web.HttpApplication
    {
        private readonly ILog Logger = LogManager.GetLogger(typeof(Global));

        protected void Application_Start(object sender, EventArgs e)
        {
            Common.Utils.LogWrite("info","程序开始启动！");
            FileInfo configFile = new FileInfo(HttpContext.Current.Server.MapPath("log4net.config"));
            Common.Utils.LogWrite("info",configFile.ToString());

            log4net.Config.XmlConfigurator.Configure(configFile);


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = HttpContext.Current.Server.GetLastError();

            Logger.Error(ex.ToString());
        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}