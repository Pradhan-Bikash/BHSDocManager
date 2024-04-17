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
        #region Load TreeView Data
        private void LoadDynamicData()
        {
            string strSQl = "SELECT * FROM tblDOCMgt";
            ConnectionManager.DAL.ConManager objCon = null;

            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                DataSet dsLocal;
                objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, false, "1");

                if (dsLocal != null && dsLocal.Tables.Count > 0)
                {
                    DataTable table = dsLocal.Tables[0];
                    DataView view = new DataView(table);
                    Dictionary<string, TreeNode> groupNodes = new Dictionary<string, TreeNode>();
                    foreach (DataRowView row in view)
                    {
                        string entryId = row["EntryID"].ToString(); 
                        string groupName = row["Documents_Group"].ToString();
                        string docName = row["DocumentName"].ToString();

                        if (!groupNodes.ContainsKey(groupName))
                        {
                            TreeNode groupNode = new TreeNode(groupName);
                            TreeView1.Nodes.Add(groupNode);
                            groupNodes[groupName] = groupNode;
                        }

                        // Add the header as a child node under the corresponding group node
                        TreeNode headerNode = new TreeNode(docName); // Setting Value to NodeID
                        headerNode.Value = entryId;
                        groupNodes[groupName].ChildNodes.Add(headerNode);
                    }
                    TreeView1.ExpandAll();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (objCon != null)
                {
                    objCon = null;
                }
            }
        }
        #endregion
        #region Load Document Details
        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {
            TreeNode selectedNode = TreeView1.SelectedNode;
            if (selectedNode != null)
            {
                string nodeId = selectedNode.Value; // This will now contain the NodeID
                DataTable dtDocument = GetDocumentDetails(nodeId);
                if (dtDocument != null && dtDocument.Rows.Count > 0)
                {
                    DataRow docRow = dtDocument.Rows[0];

                    lblDocDESC.Text = docRow["DocumentDescription"].ToString();
                    lblVNo.Text = docRow["VersionNo"].ToString();
                    lblBNo.Text = docRow["BuildNo"].ToString();
                    lblHeader.Text = docRow["Header"].ToString();
                    dvSec1.InnerHtml = Server.HtmlDecode("" + docRow["Section1"].ToString());
                    dvSec2.InnerHtml = Server.HtmlDecode("" + docRow["Section2"].ToString());
                    dvCon1.InnerHtml = Server.HtmlDecode("" + docRow["Content1"].ToString());
                    dvCon2.InnerHtml = Server.HtmlDecode("" + docRow["Content2"].ToString());

                    lblFooter.Text = docRow["Footer"].ToString();
                }
            }
        }

        private DataTable GetDocumentDetails(string nodeId)
        {
            DataTable dtDocument = new DataTable();

            string strSQl = "SELECT * FROM tblDOCMgt";
            ConnectionManager.DAL.ConManager objCon = null;

            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1"); // Assuming "1" is a connection string identifier
                DataSet dsLocal;
                objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, false, "1"); // Open dataset

                if (dsLocal != null && dsLocal.Tables.Count > 0)
                {
                    dtDocument = dsLocal.Tables[0];
                    
                    if (!string.IsNullOrEmpty(nodeId))
                    {
                        dtDocument.DefaultView.RowFilter = "EntryID = '" + nodeId.Replace("'", "''") + "'";
                        dtDocument = dtDocument.DefaultView.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
                objCon = null;
            }

            return dtDocument;
        }//eof
		#endregion
		#region File Download Related
		private void GetFileName(string btnName)
		{
            string filePath = "";
            TreeNode selectedNode = TreeView1.SelectedNode;
            if (selectedNode != null)
            {

                string docName = selectedNode.Text;
                DataTable dtDocument = GetDocumentDetails(docName);
                if (dtDocument != null && dtDocument.Rows.Count > 0)
                {
                    DataRow docRow = dtDocument.Rows[0];
                    if (btnName == "btnDownload1")
                    {
                        btnDownload1.Text = docRow["FilePath1"].ToString();
                        filePath = btnDownload1.Text;
                        btnDownload1.Text = "Download File 1";
                    }
                    else if (btnName == "btnDownload2")
                    {
                        btnDownload2.Text = docRow["FilePath2"].ToString();
                        filePath = btnDownload2.Text;
                        btnDownload2.Text = "Download File 2";
                    }
                   else if (btnName == "btnDownload3")
                    {
                        btnDownload3.Text = docRow["FilePath3"].ToString();
                        filePath = btnDownload3.Text;
                        btnDownload3.Text = "Download File 3";
                    }
                }
            }
            if (!string.IsNullOrEmpty(filePath))
            {
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + System.IO.Path.GetFileName(filePath));
                Response.TransmitFile(filePath); // Use physical path directly
                Response.End();
            }
            else
            {
                string script = "alert('File not found!');";
                ScriptManager.RegisterStartupScript(this, GetType(), "FileNotFoundScript", script, true);
            }
        }//eof
        protected void btnDownload1_Click(object sender, EventArgs e)
        {
            GetFileName("btnDownload1");
        }//eof

        protected void btnDownload2_Click(object sender, EventArgs e)
        {
            GetFileName("btnDownload2");
        }//eof

        protected void btnDownload3_Click(object sender, EventArgs e)
        {
            GetFileName("btnDownload3");
        }//eof
		#endregion

	}

}
