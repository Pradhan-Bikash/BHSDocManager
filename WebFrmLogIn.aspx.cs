using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PWOMS
{
    public partial class WebFrmLogIn : System.Web.UI.Page
    {
        
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            Page.SetFocus(this.TextUserID);     //to maintain the focus of each time this page load
            int LOGIN_STATUS = 0;
            bplib.clsWebUISecurityControl objSeqCtl;

            this.Form.DefaultButton = btnLogIn.UniqueID;
            ShowMsgBoard(0);

            
            try
            {
                objSeqCtl = new bplib.clsWebUISecurityControl();

                if (Page.IsPostBack == true)
                {
                    // do nothing
                }
                else
                {
                    // because log in optional 
                    objSeqCtl.CheckDefaultConnection("pwoffice", "@21pwofficedb", "1", "no need because i am using the web.config");
                    Session["DEAFULT_LOGIN"] = 1;
                    loadSite();
                    Session["LOGIN_STATUS"] = 0;
                    Session["USER"] = "";
                    TextUserID.Text = "";
                    TextPWD.Text = "";
                    Session["USER_SITE"] = "";
                    TextUserID.Focus();

                    Master.UpdateBody("bgLogIn");
                    loadLanguageOnLabel();
                }
            }
            catch (System.Exception ex)
            {
                TextUserID.Text = "";
                TextPWD.Text = "";
                this.ddlSite.SelectedValue = "SELECT SITE";

                ShowMsgBoard(1, "Log In Failed.....<br/>Probably you are not authorized to access this application. Please contact system administrator. <br/>" + ex.Message.ToString()) ;
            }
            finally
            {
                objSeqCtl = null;
                Session["LOGIN_STATUS"] = LOGIN_STATUS;
            }
        }// end of function 
        #endregion

        #region Customized Event
        protected void dlgOk_Click(object sender, EventArgs e)
        {
            ShowMsgBoard(0);
        }//eof
        protected void btnLogIn_Click(object sender, EventArgs e)
        {
            ControlLogIn();
        }
        protected void btnLogInClose_Click(object sender, EventArgs e)
        {
            Page.Response.Redirect("Default.aspx", false);
        }

        #endregion

        #region Customized Function
        private void ShowMsgBoard(int intswitch)
        {
            ShowMsgBoard(intswitch, "");
        }
        private void ShowMsgBoard(int intswitch, string strMsgToDisplay)
        {
            if (intswitch == 0)
            {
                DivMsgBoard.Visible = false;
            }
            else
            {
                DivMsgBoard.Visible = true;
                dlgMsg.Text = strMsgToDisplay;
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

                if (ADMIN_LOGIN == false)
                {
                    // Checking the user access
                    if (objApp.CheckNormalUser(TextUserID.Text.ToUpper().ToString(), TextPWD.Text.ToUpper().ToString(), ref LOGIN_STATUS, ref Message, ref strgroup) == false)
                    {
                        TextUserID.Text = "";
                        TextPWD.Text = "";
                        ShowMsgBoard(1, "Log In Failed.....<br/>Probably you are not authorized to access this application. Please contact system administrator.");
                    }
                    else
                    {
                        // Checking the Site access 
                        if (objApp.CheckUserAccToSite(TextUserID.Text.ToUpper().ToString(), ddlSite.SelectedValue.ToString().Trim().ToUpper(), ref LOGIN_STATUS, ref Message) == false)
                        {
                            TextUserID.Text = "";
                            TextPWD.Text = "";
                            ShowMsgBoard(1, "Log In Failed.....<br/>Probably you are not authorized to access this application. Please contact system administrator.");
                        }
                        else
                        {                            
                            Session["USER"] = TextUserID.Text.ToUpper();
                            Session["USER_GROUP"] = strgroup.ToUpper();
                            Session["USER_SITE"] = this.ddlSite.SelectedValue.ToString().Trim();
                            if (LOGIN_STATUS > 1)
                            {
                                Page.Response.Redirect("AppControlPanel.aspx", false);
                            }
                        }
                    }


                }//end admin login
            }
            catch (System.Exception ex)
            {
                TextUserID.Text = "";
                TextPWD.Text = "";
                ShowMsgBoard(1, "Log In Failed.....<br/>Probably you are not authorized to access this application. Please contact system administrator. <br/>" + ex.Message.ToString());


            }
            finally
            {
                objSeqCtl = null;
                objApp = null;
                Session["LOGIN_STATUS"] = LOGIN_STATUS;
                TextUserID.Text = "";
                TextPWD.Text = "";
            }
        }// end function.
        private void loadSite()
        {
            System.Data.DataSet dsLocal = null;
            bplib.clsWebUISecurityControl objApp = null;

            try
            {

                objApp = new bplib.clsWebUISecurityControl();
                objApp.getSites(out dsLocal);
                this.ddlSite.DataSource = dsLocal;
                this.ddlSite.DataTextField = "SITEID";
                this.ddlSite.DataValueField = "SITEID";
                this.ddlSite.DataBind();
                //this.ddlSite.Items.Add("SELECT SITE");
                this.ddlSite.SelectedValue = "HO";
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
        private void loadLanguageOnLabel()
        {

            System.Data.DataTable dtLocal = null;
            System.Data.DataTable dtTemp = null;

            //// Setting Language of UI 
            //Session["LANG_APP_SWITCH"] = "TC"; // To test off leter

            String APP_LANG_SELECTED = Session["LANG_APP_SWITCH"].ToString();

            if (APP_LANG_SELECTED == "EN")
            {
                APP_LANG_SELECTED = "";
            }

            try
            {
                if (APP_LANG_SELECTED != "")
                {
                    //dtLocal = clsSysLanguage.sysLanguage(strFlag: "Default");
                    dtLocal = clsSysLanguage.sysLanguage(strFlag: APP_LANG_SELECTED);
                    if (dtLocal != null)
                    {
                        dtLocal.DefaultView.RowFilter = "AppName='HRMSApp' and ScreenName='WebFrmLogIn'";
                        dtLocal.DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                        dtTemp = dtLocal.DefaultView.ToTable();
                        if (dtTemp.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtTemp.Rows)
                            {
                                if (this.dataUpdatePanel.FindControl(dr["CtrlID"].ToString().Trim()) != null)
                                {
                                    if (dr["CtrlType"].ToString().Trim().ToUpper() == "LABEL")
                                    {
                                        System.Web.UI.WebControls.Label lblCtrl =
                                    (Label)(this.dataUpdatePanel.FindControl(dr["CtrlID"].ToString().Trim()));
                                        lblCtrl.Text = dr["CtrlText"].ToString().Trim();
                                    }
                                    else if (dr["CtrlType"].ToString().Trim().ToUpper() == "LINKBUTTON")
                                    {
                                        System.Web.UI.WebControls.LinkButton lblCtrl =
                                    (LinkButton)(this.dataUpdatePanel.FindControl(dr["CtrlID"].ToString().Trim()));
                                        lblCtrl.Text = dr["CtrlText"].ToString().Trim();
                                    }
                                    else if (dr["CtrlType"].ToString().Trim().ToUpper() == "BUTTON")
                                    {
                                        System.Web.UI.WebControls.Button lblCtrl =
                                    (Button)(this.dataUpdatePanel.FindControl(dr["CtrlID"].ToString().Trim()));
                                        lblCtrl.Text = dr["CtrlText"].ToString().Trim();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //Session["ErrorDetails"] = "Language Loading Failed......<br><br>" + ex.Message.ToString();
                //Session["ErrorFrom"] = "WebFrmLogIn.aspx";
                //Response.Redirect("WebFrmError.aspx");
                ShowMsgBoard(1, "Language Loading Failed......<br><br>" + ex.Message.ToString());
            }
            finally
            {
                dtLocal = null;
                APP_LANG_SELECTED = "";
            }
        }//eof
        private string MessageText(string Code)
        {
            return clsSysLanguage.AppMessageText(Code, Session["LANG_APP_SWITCH"].ToString());
        }//eof
        #endregion

    }
}