using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
    public partial class WebFrmPasswordReinstate : System.Web.UI.Page
    {
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {
            Master.Page.Title = Global.PageTitle + " - Reset Password";
            this.lblAppLink.Text = System.Configuration.ConfigurationManager.AppSettings["APP_TO_MANAGE"].ToString();
            Page.SetFocus(this.TextUserID);     //to maintain the focus of each time this page load
            this.Form.DefaultButton = this.btnResetPasswd.UniqueID;

            //int LOGIN_STATUS = 0;
            bplib.clsWebUISecurityControl objSeqCtl;

            try
            {
                objSeqCtl = new bplib.clsWebUISecurityControl();


                if (Page.IsPostBack == true)
                {


                }
                else
                {
                    cancel();
                    // because log in optional 
                    objSeqCtl.CheckDefaultConnection("pwoffice", "@21pwofficedb", "1", "no need because i am using the web.config");
                    Session["DEAFULT_LOGIN"] = 1;
                    Session["VERIFICATION_STATE"] = 0;
                    Session["LOGIN_STATUS"] = 0;
                    Session["USER"] = "";
                    Session["USER_SITE"] = "";
                    TextUserID.Focus();

                    Master.UpdateBody("bg-offwhite");

                }
            }
            catch (System.Exception ex)
            {
                TxtMsgBox.Text = "";
                this.panError.Visible = true;
                TxtMsgBox.Visible = true;
                TxtMsgBox.Text = "Error: <br/>" + ex.Message.ToString();
                this.TxtMsgBox.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                //
            }
        }//eof
        #endregion

        #region Customized Event
        protected void btnResetPasswd_Click(object sender, EventArgs e)
        {
            Session["VERIFICATION_STATE"] = 2;
            resetUserData();
        }//eof
        #endregion

        #region Customized Functions
        private void resetUserData()
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            BPWEBAccessControl.clsSecurityControl objApp = null;

            bool DATA_OK = false;
            string strNewPassword = "";
            string strPasswordStatus = "";
            int iLogInStatus = 0;
            bplib.clsGenID objGenID = null;
            string strResCode = null;
            try
            {
                if ((int)Session["VERIFICATION_STATE"] != 0)
                {
                    objApp = new BPWEBAccessControl.clsSecurityControl();
                    if (DATA_OK == false)
                    {
                        
                        if (this.TextUserID.Text.Trim() == "" || this.TextUserID.Text.Trim().Length > 20)
                        {
                            System.Exception ex = new Exception("Define the User ID...(Max character allowed : 20)");
                            throw (ex);
                        }
                        if (this.TextPWD.Text.Trim() == "" || this.TextPWD.Text.Trim().Length > 10)
                        {
                            System.Exception ex = new Exception("Define the password... (Max character allowed : 10)");
                            throw (ex);
                        }
                        iLogInStatus = objApp.checkLoginStatus(this.TextUserID.Text.Trim(), this.TextPWD.Text.Trim());
                        if (iLogInStatus == 0)
                        {
                            throw new Exception("User ID or Password not matched......Please Check");
                        }
                        if (this.panInfo.Visible == false && iLogInStatus == 2)
                        {
                            this.panInfo.Visible = true;
                            throw new Exception("Email Address Not Found........Please Update It");
                        }
                        if (string.IsNullOrWhiteSpace(this.tbNewPassword.Text))
                        {
                            throw new Exception("New Password must not be Blank......");
                        }
                        if (string.Compare(this.tbNewPassword.Text,this.tbVerifyPassword.Text)!=0)
                        {
                            throw new Exception("The password confirmation does not match......");
                        }
                        
                        strNewPassword = String.Concat(this.tbNewPassword.Text.Trim().Where(c => char.IsWhiteSpace(c) == false));
                        
                        if (!objApp.checkPassword(strNewPassword.Trim(), out strPasswordStatus))
                        {
                            throw new Exception(strPasswordStatus);
                        }                        
                        if (this.panInfo.Visible)
                        {
                            if (!objApp.CheckEmailPatern(this.tbEMail.Text.Trim()))
                            {
                                throw new Exception("Define e-Mail ID......(Error(10087) in mail Pattern)");
                            }
                        }
                        if(objApp.checkMatchedWithPasswordHistory(this.TextUserID.Text.Trim(), strNewPassword.Trim()))
                        {
                            throw new Exception("Password may not be similiar to last 5 passwords......Please Check");
                        }

                        DATA_OK = true;
                    }
                    if (DATA_OK == true)
                    {
                        objApp.getBlankDSOfPasswordHistory(out dsLocal);
                        dtLocal = dsLocal.Tables[0];
                        dvLocal = new System.Data.DataView();
                        dvLocal.Table = dtLocal;
                        objGenID = new bplib.clsGenID();
                        objGenID.GenID(System.DateTime.Now.ToShortDateString().ToString(), "PWD", out strResCode);
                        strResCode = "P" + strResCode;
                        dvLocal.RowFilter = "EntryId='" + strResCode.Trim() + "'";

                        if (dvLocal.Count == 0)
                        { // Add new block
                            drLocal = dtLocal.NewRow();
                            UpdateTheDataRow("ADDNEW", strResCode, ref drLocal);
                            dtLocal.Rows.Add(drLocal);
                        }
                        else
                        {//edit block
                            throw new Exception("Unable to EDIT......Inform to IT Admin");
                        }
                        dvLocal.RowFilter = null;
                        objApp.SaveData(ref dsLocal);
                        if (!string.IsNullOrWhiteSpace(this.tbEMail.Text))
                        {
                            objApp.updateUserInformation(this.TextUserID.Text.Trim(), strNewPassword, this.tbEMail.Text.Trim());                            
                        }
                        else
                        {
                            objApp.updateUserInformation(this.TextUserID.Text.Trim(), strNewPassword);
                        }

                        cancel();
                        this.panError.Visible = true;
                        TxtMsgBox.Visible = true;
                        TxtMsgBox.Text = "User Data updated successfully....";
                        this.TxtMsgBox.ForeColor = System.Drawing.Color.Green;
                    }
                }
                else
                {
                    this.panError.Visible = true;
                    TxtMsgBox.Visible = true;
                    TxtMsgBox.Text = "Error: Session Problem.........Save is NOT possible now";
                    this.TxtMsgBox.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (System.Exception ex)
            {
                this.panError.Visible = true;
                TxtMsgBox.Visible = true;
                TxtMsgBox.Text = "Error: <br/>" + ex.Message.ToString();
                this.TxtMsgBox.ForeColor = System.Drawing.Color.Red;
            }
            finally
            {
                objApp = null;
                drLocal = null;
                dvLocal = null;
                dtLocal = null;
                dsLocal = null;

            }

        }//eof
        private void UpdateTheDataRow(string OPN_FLAG,string strResCode, ref System.Data.DataRow drLocal)
        {
            string strPWD = "";
            try
            {

                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["EntryId"] = bplib.clsWebLib.RetValidLen(strResCode.Trim(), 20);
                    drLocal["UserID"] = bplib.clsWebLib.RetValidLen(this.TextUserID.Text.Trim(), 20);
                    strPWD = String.Concat(this.tbNewPassword.Text.Trim().Where(c => char.IsWhiteSpace(c) == false));
                    drLocal["Password"] = bplib.clsWebLib.RetValidLen(BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(strPWD.Trim(), "bapps"), 100);
                    drLocal["LastUpdate"] = System.DateTime.Today;

                    drLocal["UpdateOn"] = System.DateTime.Now.ToShortDateString();                    
                }

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        }//eof

        private void cancel()
        {            
            Session["VERIFICATION_STATE"] = 0;

            this.panInfo.Visible = false;
            this.tbEMail.Text = "";

            this.TextUserID.Text = "";
            this.TextPWD.Text = "";
            this.tbNewPassword.Text = "";
            this.tbVerifyPassword.Text = "";

            this.panError.Visible = false;
            TxtMsgBox.Visible = false;
            this.TxtMsgBox.Text = "";
            this.TxtMsgBox.ForeColor = System.Drawing.Color.Red;
        }//eof
        #endregion

    }
}