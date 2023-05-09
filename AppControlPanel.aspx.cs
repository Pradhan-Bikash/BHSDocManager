using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
    public partial class AppControlPanel : System.Web.UI.Page
    {
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            ((Label)Master.FindControl("lblFormName")).Text = "Control Panel";
            string strCategory = "";

            try
            {
                if ((int)Session["DEAFULT_LOGIN"] == 0)
                {
                    Page.Response.Redirect("default.aspx");
                    return;
                }
                else
                {
                    if ((int)Session["LOGIN_STATUS"] == 0)
                    {
                        Page.Response.Redirect("default.aspx");
                        return;
                    }
                    DisplayUserName();
                }
                if (Page.IsPostBack == true)
                {
                    // 
                }
                else
                {

                    Session["VERIFICATION_STATE"] = 0;
                    if (Request.QueryString["cat"] == null || Request.QueryString["cat"] == "")
                    {
                        navigation();
                    }
                    else
                    {
                        strCategory = Request.QueryString["cat"].ToString().Trim();
                        navigation(strCategory);
                    }
                }
            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.ToString());
            }
            finally
            {
                //
            }
        }
        #endregion

        #region Form Common Functions
        private void ClearTheForm()
        {
            try
            {
                //SetDefault();
            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        }//end of function
        public void ShowLog(string strMessage)
        {
            this.panError.Visible = true;
            TxtMsgBox.Text = strMessage;
        }//eof
        public void HideLog()
        {
            TxtMsgBox.Visible = false;
            TxtMsgBox.Text = "";

        }//end of function
        private void DisplayUserName()
        {            
            ((Label)Master.FindControl("lblUserId")).Text = (string)Session["USER"];
        }//eof
        #endregion

        #region customized Event
        protected void LinkButton_Click(object sender, EventArgs e)
        {
            string strID = null;
            //strID = ((System.Web.UI.WebControls.LinkButton)sender).ID.ToString().ToUpper();
            strID = ((System.Web.UI.WebControls.ImageButton)sender).CommandName.ToString().ToUpper();
            MenuSwitch(strID);
        }
        #endregion

        #region Cutomized Function
        private void MenuSwitch(string strID)
        {
            try
            {
                //bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], strID.ToUpper());
                switch (strID)
                {

                    case "SECURITYCONTROL":
                        Page.Response.Redirect("WebFrmSecurityControl.aspx");
                        break;
                    default:
                        Page.Response.Redirect("Default.aspx");//
                        break;
                }
            }
            catch (System.Exception ex)
            {
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                //do nothing;
            }
        } //eof
        private void navigation(string strFLAG = "Entry")                                                                                                                                                                                                                                                         
        {
            try
            {
                if (string.Compare(strFLAG, "Report", true) == 0)
                {
                    this.mvwControls.SetActiveView(this.vwReport);
                    this.lblVwTitle.Text = "Reports";
                }
                else if (string.Compare(strFLAG, "Admin", true) == 0)
                {
                    this.mvwControls.SetActiveView(this.vwAdmin);
                    this.lblVwTitle.Text = "Admin";
                }
                else
                {
                    this.mvwControls.SetActiveView(this.vwEntry);
                    this.lblVwTitle.Text = "Home";
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }//eof
        #endregion

    }
}