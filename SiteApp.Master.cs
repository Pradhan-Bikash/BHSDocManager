using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
    public partial class SiteApp : System.Web.UI.MasterPage
    {
        int fSize = 15;
        protected void Page_Load(object sender, EventArgs e)
        {
            Msg_Box();
            if (fSize > 0)
            {
                this.lblFormName.Font.Size = fSize;
            }
            else
            {
                this.lblFormName.Font.Size = 15;
            }

            this.lblSiteID.Text = " | " + (string)Session["USER_SITE"] + " | ";

            if (Page.IsPostBack == true)
            {
                // 
            }
            else
            {

                this.ACCCTL.Visible = false;
                if (Request.QueryString["cat"] == null || Request.QueryString["cat"] == "")
                {
                    //                    
                }
                else
                {
                    if (Request.QueryString["cat"] == "1")
                    {
                        this.ACCCTL.Visible = true;
                    }
                }
                
            }

        }//eof

        
        #region Form Properties
        public Label masterlblFormName
        {
            get
            {
                return lblFormName;
            }
            set
            {
                lblFormName = value;
            }
        }//eof
        public int TitleFontSize
        {
            set
            {
                Int32.TryParse(bplib.clsWebLib.GetNumData(value.ToString()), out fSize);
            }
        }//eof        
        
        #endregion

        #region customized Event
        protected void LinkButton_Click(object sender, EventArgs e)
        {
            string strID = "";
            string strModuleId = "";
            //strID = ((System.Web.UI.WebControls.LinkButton)sender).ID.ToString().ToUpper();
            strID = ((System.Web.UI.WebControls.LinkButton)sender).CommandName.ToString().ToUpper();
            strModuleId = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument.ToString().ToUpper();
            MenuSwitch(strID, strModuleId);
        }
        // Dont use this pattern for application management case .... this is only for user access management interface part. 
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            string strID = "";
            string strModuleId = "";
            //strID = ((System.Web.UI.WebControls.LinkButton)sender).ID.ToString().ToUpper();
            strID = ((System.Web.UI.WebControls.LinkButton)sender).CommandName.ToString().ToUpper();
            strModuleId = ((System.Web.UI.WebControls.LinkButton)sender).CommandArgument.ToString().ToUpper();
            MenuSwitch2(strID);
        }
        #endregion

        #region Custom Events
        protected void linMsgViewClose_Click(object sender, EventArgs e)
        {
            Msg_Box();
        }

        protected void linAbout_Click(object sender, EventArgs e)
        {
            Session_Reset();
            Response.Redirect("about.aspx");
        }
        protected void linContact_Click(object sender, EventArgs e)
        {
            Session_Reset();
            Response.Redirect("contact.aspx");
        }
        protected void linDashBoard_Click(Object sender, EventArgs e)
        {
            //Response.Redirect("AppMasterPanel.aspx?cat=Dashboard");
        }//eof 
        protected void linMyTran_Click(Object sender, EventArgs e)
        {
            //Response.Redirect("AppMasterPanel.aspx?cat=MyTran");
        }//eof   
        protected void linReport_Click(Object sender, EventArgs e)
        {
            //Response.Redirect("AppMasterPanel.aspx?cat=Report");
        }//eof   
        protected void linSettings_Click(Object sender, EventArgs e)
        {
            //Response.Redirect("AppMasterPanel.aspx?cat=Settings");
        }//eof   
        protected void Button_LogOff_Click(Object sender, EventArgs e)
        {
            LogOff();
        }//eof    
        #endregion

        #region Support functions
        public void Msg_Box()
        {
            Msg_Box(0, "");
        }
        public void Msg_Box(int ShowSwitch, string Message)
        {
            if (ShowSwitch == 1)
            {
                MsgViewBK.Visible = true;
                MsgView.Visible = true;
                lblmyMsgBoxTxt.Text = Message.ToString().Trim();

            }
            else
            {
                MsgViewBK.Visible = false;
                MsgView.Visible = false;
                lblmyMsgBoxTxt.Text = "";
            }
        }

        #endregion

        #region Custom Functions
        private void MenuSwitch(string strID, string strModuleId)
        {
            try
            {
                bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], strModuleId.ToUpper());
                switch (strID)
                {
                    case "EMPLOYEE INFO":
                        Response.Redirect("webFrmEmployeeInfo.aspx");
                        break;
                    //case "EMPLOYEE":
                    //    Response.Redirect("WebFrmEmployee.aspx");
                    //    break;
                    //case "CUSTOMER":
                    //    Response.Redirect("WebFrmCustomer.aspx");
                    //    break;
                    //case "CUSREPORT":
                    //    Response.Redirect("WebCusReport.aspx");
                    //    break;
                    case "DOCUPDATE":
                        Response.Redirect("DocMgtUpdate.aspx");
                        break;
                    case "DOCVIEW":
                        Response.Redirect("DocMgtView.aspx");
                        break;
                    case "BANK INFO":
                        Response.Redirect("webFrmBankInfo.aspx");
                        break;
                    case "LEASE AGREEMENT":
                        Response.Redirect("webFrmLeaseAgreement.aspx");
                        break;
                    case "FUNDING INFO":
                        Response.Redirect("webFrmFundingInfo.aspx");
                        break;
                    case "BUSINESS REGISTRATION":
                        Response.Redirect("webFrmBizRegInfo.aspx");
                        break;


                    case "ENTITYFIXEDVARIABLES":
                        Response.Redirect("AppFixedEntityVarManage.aspx");
                        break;

                    case "REPORTS":
                        Response.Redirect("webfrmPWOMSReport.aspx");
                        break;
                    case "EMPREPORT":
                        Response.Redirect("webfrmEmpReport.aspx");
                        break;

                    default:
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Msg_Box(1, "Error : <br/>" + ex.Message.ToString());
                //displayMessage("Error : <br/>" + ex.Message.ToString());
                //ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                //do nothing;
            }
        } //eof
        private void LogOff()
        {
            Session_Reset();
            Response.Redirect("default.aspx");
        }//eof
        private void Session_Reset()
        {
            Session["LOGIN_STATUS"] = 0;
            Session["USER"] = "";
            Session["DEAFULT_LOGIN"] = 0;
            Session["VERIFICATION_STATE"] = 0;
            Session["DEPTID"] = "";
            Session["WHO_ASKING"] = "";
        }

        // below one -  decicated for Access control managment issue - cannot use for acces check 
        private void MenuSwitch2(string strID)
        {
            try
            {
                //bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], strID.ToUpper());
                switch (strID)
                {

                    case "SECURITYCONTROL":
                        Page.Response.Redirect("WebFrmSecurityControl.aspx");
                        break;
                    case "PWD_RENEW":
                        Page.Response.Redirect("WebFrmPasswordReinstate.aspx");
                        break;
                    default:
                        Page.Response.Redirect("Default.aspx");//
                        break;
                }
            }
            catch (System.Exception ex)
            {
                Msg_Box(1, "Error : <br/>" + ex.Message.ToString());
            }
            finally
            {
                //do nothing;
            }
        } //eof
        

        #endregion

    }
}