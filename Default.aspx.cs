using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
    public partial class _Default : Page
    {
        
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {
            this.lblAppLink.Text = System.Configuration.ConfigurationManager.AppSettings["APP_TO_MANAGE"].ToString();
            Master.Page.Title = Global.PageTitle;
            Page.SetFocus(this.TextUserID);     //to maintain the focus of each time this page load
            this.Form.DefaultButton = this.btnLogIn.UniqueID;

            int LOGIN_STATUS = 0;
            bplib.clsWebUISecurityControl objSeqCtl;

            try
            {
                objSeqCtl = new bplib.clsWebUISecurityControl();


                if (Page.IsPostBack == true)
                {


                }
                else
                {
                    this.panError.Visible = false;
                    TxtMsgBox.Visible = false;
                    this.TxtMsgBox.Text = "";
                    ViewLogInPad(false);
                    // because log in optional 
                    objSeqCtl.CheckDefaultConnection("pwoffice", "@21pwofficedb", "1", "no need because i am using the web.config");
                    Session["DEAFULT_LOGIN"] = 1;                    
                    Session["LOGIN_STATUS"] = 0;
                    Session["USER"] = "";
                    Session["USER_SITE"] = "";
                    //TextUserID.Text = "";//for this project
                    //TextPWD.Text = "";//for this project
                    TextUserID.Focus();
                    loadSite();

                    //Master.UpdateBody("bg");
                    Master.UpdateBody("bgDefault");
                    
                    //loadLanguageOnLabel();
                }
            }
            catch (System.Exception ex)
            {
                //TextUserID.Text = "";//for this project
                //TextPWD.Text = "";//for this project
                TxtMsgBox.Text = "";
                this.ddlSite.SelectedValue = "SELECT SITE";
                TxtMsgBox.Visible = true;
                TxtMsgBox.Text = "Log In Failed... :\n" + ex.ToString();
                this.panError.Visible = true;
            }
            finally
            {
                objSeqCtl = null;
                Session["LOGIN_STATUS"] = LOGIN_STATUS;
            }
        }//eof
        #endregion

        #region Customized Event
        public void btnLogIn_Click(object sender, EventArgs e)
        {
            ControlLogIn();
        }//eof
        public void linkBtnLogin_Click(object sender, EventArgs e)
        {            
            Response.Redirect("WebFrmLogIn.aspx");
        }//eof

        protected void linkPwdRenew_Click(object sender, EventArgs e)
        {
            Response.Redirect("WebFrmPasswordReinstate.aspx");
        }
        protected void lnkBtnAdminAccess_Click(object sender, EventArgs e)
        {
            ViewLogInPad(true);
        }
        protected void lnkClose_Click(object sender, EventArgs e)
        {
            ViewLogInPad(false);
        }
        #endregion

        #region Customized Function
        private void ViewLogInPad(bool mVisible)
        {
            if (mVisible == true)
            {
                this.LogInContainer.Visible = true;
                this.LogIn.Visible = true;
            }
            else
            {
                this.LogInContainer.Visible = false;
                this.LogIn.Visible = false;
            }
        }
        private void ControlLogIn()
        {
            string Message = string.Empty;
            string strgroup = "";
            bplib.clsWebUISecurityControl objSeqCtl;
            bplib.clsAppSeq objApp;

            bool ADMIN_LOGIN = false;
            int LOGIN_STATUS = 0;

            try
            {
                objSeqCtl = new bplib.clsWebUISecurityControl();
                objApp = new bplib.clsAppSeq();

                // Second level check will be DATA ADMIN (if any there)
                if (System.String.Compare(TextUserID.Text.ToUpper().ToString(), "DATAADMIN", true) == 0)
                {
                    ADMIN_LOGIN = true;
                    if (objSeqCtl.ValidateUser(TextUserID.Text, TextPWD.Text, ref LOGIN_STATUS, ref Message) == true)
                    {
                        TxtMsgBox.Visible = false;
                        Session["USER"] = TextUserID.Text.ToUpper();
                        Session["USER_GROUP"] = "SUPR";
                        if (LOGIN_STATUS > 1)
                        {
                            Page.Response.Redirect("AppControlPanel.aspx?cat=1");

                        }
                    }
                    else
                    {
                        TxtMsgBox.Text = "Probably you are not authorized to access from web……...";
                        TxtMsgBox.Visible = true;
                        this.panError.Visible = true;
                    }
                } // DATA Admin
                else
                {
                    TxtMsgBox.Text = "NOT AUTHORIZED ID... ";
                    TxtMsgBox.Visible = true;
                    this.panError.Visible = true;
                }
            }
            catch (System.Exception ex)
            {
                TextUserID.Text = "";
                TextPWD.Text = "";
                TxtMsgBox.Text = "";
                TxtMsgBox.Visible = true;
                TxtMsgBox.Text = "Log In Failed...... :\n" + ex.Message.ToString();
                this.panError.Visible = true;
            }
            finally
            {
                objSeqCtl = null;
                objApp = null;
                Session["LOGIN_STATUS"] = LOGIN_STATUS;
                TextUserID.Text = "";
                TextPWD.Text = "";
            }
        }//eof
        private void loadSite()
        {
            System.Data.DataSet dsLocal = null;
            bplib.clsWebUISecurityControl objApp = null;

            try
            {

                objApp = new bplib.clsWebUISecurityControl();
                objApp.getSites(out dsLocal);
                this.ddlSite.DataSource = dsLocal;
                this.ddlSite.DataTextField = "SITE_GROUP";
                this.ddlSite.DataValueField = "SITEID";
                this.ddlSite.DataBind();

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objApp = null;
                dsLocal = null;
            }
        }//eof
        #endregion


       
    }
}