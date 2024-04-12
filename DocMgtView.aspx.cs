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
                objCon = new ConnectionManager.DAL.ConManager("1"); // Assuming "1" is a connection string identifier
                DataSet dsLocal;
                objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, false, "1"); // Open dataset

                if (dsLocal != null && dsLocal.Tables.Count > 0)
                {
                    DataTable table = dsLocal.Tables[0];
                    DataView view = new DataView(table);
                    // HashSet<string> uniqueGroups = new HashSet<string>(); // HashSet to store unique group names
                    Dictionary<string, TreeNode> groupNodes = new Dictionary<string, TreeNode>();
                    foreach (DataRowView row in view)
                    {
                        string groupName = row["Documents_Group"].ToString();
                        string docName = row["DocumentName"].ToString();

                        if (!groupNodes.ContainsKey(groupName))
                        {
                            TreeNode groupNode = new TreeNode(groupName);
                            TreeView1.Nodes.Add(groupNode);
                            groupNodes[groupName] = groupNode;
                        }

                        // Add the header as a child node under the corresponding group node
                        TreeNode headerNode = new TreeNode(docName);
                        groupNodes[groupName].ChildNodes.Add(headerNode);
                    }
                    TreeView1.ExpandAll();
                }
            }
            catch (Exception ex)
            {
                // Log and/or rethrow or handle the exception as needed
                throw ex;
            }
            finally
            {
                if (objCon != null)
                {
                    // Make sure to close the connection
                    objCon = null;
                }
            }
        }

        private void PopulateSubNodes(TreeNode parentNode, DataTable table)
        {
            DataView view = new DataView(table);
            view.RowFilter = $"PrenID = {parentNode.Value}"; // Filter to find child nodes

            foreach (DataRowView row in view)
            {
                TreeNode childNode = new TreeNode(row["Header"].ToString(), row["EntryID"].ToString());
                childNode.NavigateUrl = row["FilePath1"].ToString();
                parentNode.ChildNodes.Add(childNode); // Add child node to the parent node
                PopulateSubNodes(childNode, table); // Recursive call to add further child nodes
            }
        }

    }
}
