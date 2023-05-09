using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace BPWEBAccessControl
{
    public class Global : HttpApplication
    {
        private System.ComponentModel.IContainer components = null;

        public static string SystemFilePath = "COMM_APP_DOX";
        public static readonly string PageTitle = "BHS Infotech Ltd. (Web client)";
        
        //public static string CipherKey = @"KEY2020";

        public Global()
        {
            InitializeComponent();
        }
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            LoginApplication("bptestdbadmin", "bptestdbadmin@20", "1", "no need because i am using the web.config");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["LOGIN_STATUS"] = 0;
            Session["USER"] = "";
            Session["DEAFULT_LOGIN"] = 0;
            Session["VERIFICATION_STATE"] = 0;
            Session["DEPTID"] = "";
            Session["WHO_ASKING"] = "";

            Session["LANG_APP_SWITCH"] = "";
            Session["USER_SITE"] = "";
        }

        #region Customized functions
        public static string TitleText()
        {
            string ss = "BHS (Web client)";
            return ss;
        }
        public static string FooterText()
        {
            string ss = "BPWEBAccAndAppMixV3 (version 4.7.2)(Build no: 01202022.01)";
            return ss;
        }
        public static string DateAndTimeDisplay()
        {
            string ss = System.DateTime.Now.ToString("dddd, MMMM dd, yyyy");
            return ss;
        }

        public static void LoginApplication(string strUser, string strPassword, string strDefaultConID, string strPath)
        {
            /// -----------------------------------------------------------------
            /// For SysInfoNet.mdb stored profile based connection initialization
            /// -----------------------------------------------------------------
            //ConnectionManager.DAL.ConBuilder objConBuilder=new ConnectionManager.DAL.ConBuilder(strPath);
            //objConBuilder.DeSeralizeConnectionFromMDB();

            ///-----------------------------------------------------------------
            /// For App.config / web.config based connection initialization 
            /// ----------------------------------------------------------------
            ConnectionManager.DAL.ConBuilder objConBuilder = new ConnectionManager.DAL.ConBuilder();
            objConBuilder.DeSeralizeConnectionFromAppConfigFile();

            /// ----- Set command lines no need to chnage---------- 
            ConnectionManager.DAL.ConBuilder.SetDefaultConnectionCode(strDefaultConID);

            /// The follwoing two command Lines don't use in the web application 
            /// in Application Start Event. In Application start I am just  				/// inistiting the variables. 
            /// I am calling this following two command lines directly from clsWebUISequrityControl.cs in heckDefaultConnection

            //ConnectionManager.DAL.ConManager objCon = new ConnectionManager.DAL.ConManager(strDefaultConID);
            //objCon.LoginApplication(strUser, strPassword ,  strDefaultConID , strPath); 
        }
        #endregion

        #region Web Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
        }
        #endregion
    }
}