using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
	public partial class DocMgtView : System.Web.UI.Page
	{
		#region Form Event
		protected void Page_Load(object sender, EventArgs e)
		{
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            Master.masterlblFormName.Text = "Document View";
            this.lblfrmName.Text = "Document View";
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
				//DisplayUserName();
			}
			//----------------
			//HideLog();
			try
            {
                if (Page.IsPostBack == true)
                {
                    // 
                }
                else
                {

                    Session["VERIFICATION_STATE"] = 0;
					LoadDynamicData();
					loadLanguageOnLabel();
					//Cancel();

				}
            }
            catch (System.Exception ex)
            {
                // ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }
        #endregion

        #region commom Function
        public void ShowLog(string strMessage)
        {
            //TxtMsgBox.Visible = true;
            //TxtMsgBox.Text = strMessage;
        }//enf of function
        private void loadLanguageOnLabel()
        {

            System.Data.DataTable dtLocal = null;
            System.Data.DataTable dtTemp = null;
            try
            {
                dtLocal = PWOMS.clsSysLanguage.sysLanguage();
                if (dtLocal != null)
                {
                    dtLocal.DefaultView.RowFilter = "ScreenName='Application'";
                    dtLocal.DefaultView.RowStateFilter = DataViewRowState.CurrentRows;
                    dtTemp = dtLocal.DefaultView.ToTable();
                    if (dtTemp.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtTemp.Rows)
                        {
                            System.Web.UI.WebControls.Label lblCtrl =
                            (Label)(this.dataUpdatePanel.FindControl(dr["CtrlID"].ToString().Trim()));

                            lblCtrl.Text = dr["LNG"].ToString().Trim();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLog("Error in Loading Language : " + ex.Message.ToString().Trim());
            }
            finally
            {
                dtLocal = null;
            }
        }//eof       

        #endregion
        private void LoadDynamicData()
        {
            string strSQl = "SELECT * FROM tblDOCMgt"; // Selecting both header text and content
            ConnectionManager.DAL.ConManager objCon = null;

            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                DataSet dsLocal;
                objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, false, "1");

                if (dsLocal != null && dsLocal.Tables.Count > 0)
                {
                    DataTable dtLocal = dsLocal.Tables[0];

                    foreach (DataRow row in dtLocal.Rows)
                    {
                        string headerText = row["Header1"].ToString();
                        string section1 = row["Section1"].ToString();
                        string section2 = row["Section2"].ToString();
                        RenderSidebarItem(headerText,section1,section2);
                    }
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objCon != null)
                {
                    objCon=null;
                }
            }
        }

        private void RenderSidebarItem(string headerText, string section1,string section2)
        {
            string sidebarItemHtml = $@"
            <li style='background-color: #feeec3 !important; color: #8D6F08 !important;font-weight: bold;' class='experience-section''>{headerText}</li>
             <li style = 'background-color: var(--leftbar-explore-section-color) !important;' ><a class='share-experience-modal' href='#' style='cursor:pointer;display: block;border-bottom: 1px solid var(--gfg-body-color-alternate);'>{section1}</a></li>
            <li style = 'background-color: var(--leftbar-explore-section-color) !important;' ><a class='share-experience-modal' href='#' style='cursor:pointer;display: block;border-bottom: 1px solid var(--gfg-body-color-alternate);'>{section2}</a></li>
        ";

            sidebarContent.Controls.Add(new LiteralControl(sidebarItemHtml));
        }//eof

    }

}