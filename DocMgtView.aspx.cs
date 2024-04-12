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
                dsLocal.Relations.Add("ChildRows", dsLocal.Tables[0].Columns["EntryID"], dsLocal.Tables[0].Columns["PrenID"]);
                    foreach (DataRow lbl1Datarow in dsLocal.Tables[0].Rows)
                    {
                        //TreeNode parentTreeNode = new TreeNode();
                        //parentTreeNode.Text = lbl1Datarow["Header"].ToString(); // Assuming 'HeaderText' is the column name for node text
                        //parentTreeNode.NavigateUrl = lbl1Datarow["FilePath1"].ToString(); // Assuming 'ID' is the primary key column name
                        //                                                                 // You can add additional properties to the node if needed
                        //DataRow[] childRows = lbl1Datarow.GetChildRows("ChildRows");
                        //foreach(DataRow lbl2DataRow in ch)
                        //// Add the node to the TreeView
                        //TreeView1.Nodes.Add(node);
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


    }

}