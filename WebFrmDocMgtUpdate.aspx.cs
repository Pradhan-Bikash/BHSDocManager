using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BPWEBAccessControl
{
	public partial class Document_Update : System.Web.UI.Page
	{
		#region Form Event
		protected void Page_Load(object sender, EventArgs e)
		{
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            Master.masterlblFormName.Text = "Document Update";
            this.lblfrmName.Text = "Document Update";
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
            
            HideLog();
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
                    Cancel();

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

        #region Form Common Functions

        private void ClearTheForm()
        {
            try
            {
                SetDefault();
            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
                //
            }
        }
        public void ShowLog(string strMessage)
        {
            TxtMsgBox.Visible = true;
            TxtMsgBox.Text = strMessage;
        }//enf of function
        public void HideLog()
        {
            TxtMsgBox.Visible = false;
            TxtMsgBox.Text = "";

        }//end of function
        private void DisplayUserName()
        {
            ((Label)Master.FindControl("lblUserId")).Text = (string)Session["USER"];
        }//eof
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

        #region DIALOG FUNCTIONS
        /// <summary>
        /// display Msg In View 2
        /// </summary>
        /// <param name="textMsg">Msg Text To be Displayed</param>
        /// <param name="type">Type of the Msg - Error,Ok,Confirmation,Info</param>
        /// <param name="FLAG">FLAG = name of function from which dialog generate</param>

        private void displayMsgs(string textMsg, string type, string FLAG)
        {
            try
            {
                this.dlgMsg.Text = textMsg;
                if (type.Trim().ToUpper() == "ERROR")
                {
                    this.dlgImage.ImageUrl = "Picture/error.png";
                    this.dlgOk.Visible = true;
                    this.dlgCancel.Visible = false;
                    this.dlgMsg.ForeColor = System.Drawing.Color.Red;
                }
                else if (type.Trim().ToUpper() == "OK")
                {
                    this.dlgImage.ImageUrl = "Picture/ok.png";
                    this.dlgOk.Visible = true;
                    this.dlgCancel.Visible = false;
                    this.dlgMsg.ForeColor = System.Drawing.Color.Green;
                }
                else if (type.Trim().ToUpper() == "CONFIRMATION")
                {
                    this.dlgImage.ImageUrl = "Picture/confirm.png";
                    this.dlgOk.Visible = true;
                    this.dlgCancel.Visible = true;
                    this.dlgMsg.ForeColor = System.Drawing.Color.Blue;
                }
                else
                {
                    this.dlgImage.ImageUrl = "Picture/info.png";
                    this.dlgOk.Visible = true;
                    this.dlgCancel.Visible = false;
                    this.dlgMsg.ForeColor = System.Drawing.Color.Black;
                }
                this.lblViewName.Text = this.mvwDataVw.GetActiveView().ID.ToString();
                this.lblViewState.Text = FLAG.Trim();
                this.mvwDataVw.SetActiveView(this.vw01);

            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }
        #endregion

        #region Search
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string stringKey = "";
            string FLAG = "";
            try
            {

                if (this.tbValue.Text.Trim() != "")
                {
                    if (this.ddlSearchBy.SelectedValue.Trim() != "")
                    {
                        FLAG = this.lblViewState.Text.Trim();
                        stringKey = this.ddlSearchBy.SelectedValue.Trim() + " like '%" + this.tbValue.Text.Trim() + "%'";                                                   //*********//
                    }
                    LoadData(false, stringKey, FLAG);
                }
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        protected void btnCancelSearch_Click(object sender, EventArgs e)
        {
            CancelSearch();
        }//eof
        public void Grid_Command(object sender, DataGridCommandEventArgs e)
        {
            string vwIndex = this.lblViewName.Text;
            string DOCID = "";
            string CollectionID = "";
            if (((LinkButton)e.CommandSource).CommandName != "Page")
            {
               
                TableCell DOCIDCell = e.Item.Cells[1];                                       
                TableCell CollectionIDCell = e.Item.Cells[2];
                DOCID = DOCIDCell.Text;
                CollectionID = CollectionIDCell.Text;
            }
            if (((LinkButton)e.CommandSource).CommandName == "EditFile")
            {
                //Edit
                if (this.lblViewState.Text.Trim() == "")
                {
					Cancel();
					this.txtEntryId.Text = DOCID;
					LoadDetails();
				}
                if (this.lblViewState.Text.Trim() == "DOCMANAGER")
                {
                    Cancel();
                    this.txtEntryId.Text = DOCID;
                    LoadDetails();
                }
                this.mvwDataVw.ActiveViewIndex = returnView(vwIndex.Trim());
            }
        }//eof 
        #endregion

        #region DIALOG BOX
        protected void dlgOk_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblDlgState.Text = "TRUE";
                dialogFunction();
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        protected void dlgCancel_Click(object sender, EventArgs e)
        {
            try
            {
                this.lblDlgState.Text = "False";
                dialogFunction();
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        #endregion

        #region DATA ENTRY EVENTS

        protected void Button_AddNew_Click(object sender, EventArgs e)
        {
            AddNew();
        }//eof
        protected void Button_save_Click(object sender, EventArgs e)
        {
            SaveData();
        }//eof
        protected void Button_Delete_Click(object sender, EventArgs e)
        {
            displayMsgs("Are you sure to delete this data???", "Confirmation", "Delete");
        }//eof
        protected void Button_Cancel_Click(object sender, EventArgs e)
        {
            Cancel();
        }//eof
        protected void Button_Edit_Click(object sender, EventArgs e)
        {
            EditExist();
        }//eof
        protected void Button_LogOff_Click(object sender, EventArgs e)
        {
            LogOff();
        }//eof

        #endregion

        #region Customized Functions

        #region SEARCH RELATED FUNCTIONS
        private void LoadData(bool IsLoad, string strKey, string FLAG)
        {

            string SB = "";
            PWOMS.clsDocApplication objApp = null;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataView dvLocal = null;
            this.panSearch.Visible = false;
            this.dgSearch.DataSource = null;
            this.dgSearch.Visible = false;
            this.lblInfoDox.Text = "";
            int rowNo = 0;
            string strSiteId = Session["USER_SITE"].ToString().Trim();

            string fromDate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string toDate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            try
            {
                objApp = new PWOMS.clsDocApplication();
                if (FLAG == "") //this is default search function
                {
                    //objApp.SearchTeleData(fromDate, toDate, strKey,strSiteId, out dsLocal);
                }
                if (FLAG == "DOCMANAGER")
                {
                    objApp.SearchDocument(fromDate, toDate, strKey, strSiteId, out dsLocal);
                }
                this.lblViewState.Text = FLAG;
                //Make the 1/1/1901 blank

                dtLocal = dsLocal.Tables[0];

                LoadViewScreenInformation(ref dtLocal, "", IsLoad, FLAG);                       // Show all the column name of tbl employee in SearchBy Drop down menu

                dvLocal = new DataView();
                dvLocal.Table = dtLocal;
                this.dgSearch.DataSource = dvLocal;

                if (dvLocal.Count > 0)
                {
                    this.dgSearch.Visible = true;
                    this.dgSearch.DataBind();
                    this.panSearch.Visible = true;
                    rowNo = dvLocal.Count;
                    SB = SB + rowNo + " Record(s) found";
                    this.lblInfoDox.Text = SB;
                }
                else
                {
                    SB = "No record found....................";
                    this.lblInfoDox.Text = SB;
                }

            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {

            }
        }//eof
        private void LoadViewScreenInformation(ref DataTable dtFind, string strExcludeColums, bool IsLoad, string FLAG)
        {
            try
            {
                if (IsLoad == true)
                {
                    this.ddlSearchBy.DataSource = GetTableDefination(ref dtFind);
                    this.ddlSearchBy.DataTextField = "EntryID";
                    this.ddlSearchBy.DataValueField = "EntryID";
                    this.ddlSearchBy.DataBind();
                    this.ddlSearchBy.SelectedIndex = 1;

                    //if (FLAG=="PAY")
                    //{
                    //    this.ddlSearchBy.SelectedValue = "Supplier";
                    //}

                }
            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
                //
            }
        } //eof
        private DataTable GetTableDefination(ref DataTable dtFind)                                  // Get all The Column Name of tblDocMgt
        {

            DataTable dt = new DataTable("tblSearchKeyList");
            dt.Columns.Add("EntryID", typeof(String));

            for (Int32 i = 0; i < dtFind.Columns.Count /*= 4*/; i++)
            {
                if (dtFind.Columns[i].DataType == typeof(System.String) ||
                    dtFind.Columns[i].DataType == typeof(System.Char))
                {
                    //if (dtFind.Columns[i].ColumnName.ToString().Length >= 4)
                    //{
                    //    if (dtFind.Columns[i].ColumnName.ToString().Substring(dtFind.Columns[i].ColumnName.ToString().Length - 4, 4).ToUpper() == "DATE")
                    //    {//do nothing
                    //    }
                    //    else
                    //    {
                    //        dt.Rows.Add(new Object[] { dtFind.Columns[i].ColumnName.ToString() });
                    //    }
                    //}
                    //else
                    //{
                    dt.Rows.Add(new Object[] { dtFind.Columns[i].ColumnName.ToString() });
                    //}
                }
            }
            dt.AcceptChanges();
            return dt;
        } //eof
        private void CancelSearch()
        {
            string vwIndex = this.lblViewName.Text;
            try
            {
                this.lblViewState.Text = "";
                this.lblViewName.Text = "";
                this.lblViewName.Text = "";
                this.lblSearchTitle.Text = "Search";
                this.dgSearch.DataSource = null;
                this.dgSearch.DataBind();
                this.dgSearch.Visible = false;
                this.panSearch.Visible = false;
                if (vwIndex.Trim().ToUpper() == "VW02")
                {
                    this.mvwDataVw.SetActiveView(this.vw02);
                }
                else
                {
                    this.mvwDataVw.SetActiveView(this.vw00);
                }
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        #endregion

        #region Dialouge Related Function

        private void dialogFunction()
        {
            string FLAG = "";
            string vwName = "";
            //string isReturn = "YES";
            try
            {
                FLAG = this.lblViewState.Text.ToUpper().Trim();
                vwName = this.lblViewName.Text.ToUpper().Trim();
                //mention the confirmation functions actions
                if (FLAG.Trim().ToUpper() == "DELETE")
                {
                    if (this.lblDlgState.Text.Trim().ToUpper() == "TRUE")
                    {
                        DeleteData();
                        //isReturn = "NO";
                    }
                }
                this.lblDlgState.Text = "";
                this.lblViewState.Text = "";
                this.lblViewName.Text = "";
                this.mvwDataVw.ActiveViewIndex = returnView(vwName);
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof

        private int returnView(string vwId)
        {
            string vwIds = "";
            int vwNo = 0;
            try
            {
                foreach (View vw in mvwDataVw.Views)
                {
                    vwIds = vw.ID.ToString().ToUpper().Trim();
                    if (string.Compare(vwId.ToUpper().Trim(), vwIds.Trim(), false) == 0)
                    {
                        break;
                    }
                    vwNo++;
                }
            }
            catch (Exception ez)
            {
                ShowLog("Error: \n" + ez.Message.ToString());
            }
            finally
            {
            }

            return vwNo;
        }//eof
        #endregion

        #region DATA ENTRY RELATED FUNCTIONS

        private void AddNew()
        {
            bplib.clsGenDocID objGenID = null;
            string strResCode = null;
            try
            {
                Cancel();
                Session["VERIFICATION_STATE"] = 2;                                             

                this.txtEntryId.Text = "";
                this.txtEntryId.Enabled = false;

                // for add new a new auto Company Id will be added
                if ((int)Session["VERIFICATION_STATE"] == 2)
                {
                    objGenID = new bplib.clsGenDocID();
                    objGenID.GenDOCIdTest(System.DateTime.Now.ToShortDateString().ToString(), "DOCMANAGER", out strResCode);
                    strResCode = "DOC" + strResCode;
                    this.txtEntryId.Text = strResCode;

                }

                this.btnSave.Text = "Create";
            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
                //
            }
        } //eof

        private void SaveData()
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;
            string modulename = "DOCMANAGER.EDIT";

            PWOMS.clsDocApplication objApp = null;

            bool DATA_OK = false;
            try
            {
               
                objApp = new PWOMS.clsDocApplication();
                if (DATA_OK == false)
                {
                    if (this.txtEntryDT.Text == "" || bplib.clsWebLib.IsDateOK(this.txtEntryDT.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the Entry date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-20023')");
                        throw (ex);
                    }
                    if (this.txtDocName.Text.Trim() == "" || this.txtDocName.Text.Trim().Length > 500)
                    {
                        System.Exception ex = new Exception("Define the Document Name...(Max length allowed 500)");
                        throw (ex);
                    }
                    if (this.ddlDocGroup.Text.Trim() == "")
                    {
                        System.Exception ex = new Exception("Define the Documents Group...");
                        throw (ex);
                    }
                    if (this.txtVersion.Text.Trim() == "" || this.txtVersion.Text.Trim().Length > 50)
                    {
                        System.Exception ex = new Exception("Define the Version NO...(Max length allowed 50)");

                        throw (ex);
                    }
                    if (this.txtBuild.Text.Trim() == "" || this.txtBuild.Text.Trim().Length > 50)
                    {
                        System.Exception ex = new Exception("Define the Document Build No...(Max length allowed 50)");
                        throw (ex);
                    }

                    //if (this.txtHeader.Text.Trim() == "")
                    //{
                    //    System.Exception ex = new Exception("Define the Document Header...");
                    //    throw (ex);
                    //}
                    //if (this.txtSec1.Text.Trim() == "" )
                    //{
                    //    System.Exception ex = new Exception("Define the Document Section2...");
                    //    throw (ex);
                    //}
                    //if (this.txtCon1.Text.Trim() == "" )
                    //{
                    //    System.Exception ex = new Exception("Define the Document Content2");
                    //    throw (ex);
                    //}
                    //if (this.txtSec2.Text.Trim() == "")
                    //{
                    //    System.Exception ex = new Exception("Define the Document Section1...");
                    //    throw (ex);
                    //}
                    //if (this.txtCon2.Text.Trim() == "" )
                    //{
                    //    System.Exception ex = new Exception("Define the Document Content1...");
                    //    throw (ex);
                    //}
                    //if (this.txtFooter.Text.Trim() == "")
                    //{
                    //    System.Exception ex = new Exception("Define the Document Footer...(Max length allowed 50)");
                    //    throw (ex);
                    //}

                    DATA_OK = true;
                }

                if (DATA_OK == true)
                {
                    objApp.GetDataOfDOC(this.txtEntryId.Text.Trim(), out dsLocal);
                    dtLocal = dsLocal.Tables[0];
                    dvLocal = new DataView();
                    dvLocal.Table = dtLocal;

                    //Isolate the specific row to edit or is not available then add
                    dvLocal.RowFilter = "EntryID='" + this.txtEntryId.Text.Trim() + "'";

                    if (dvLocal.Count == 0)
                    { // Add new block
                        drLocal = dtLocal.NewRow();
                        UpdateTheDataRow("ADDNEW", ref drLocal);
                        dtLocal.Rows.Add(drLocal);
                    }
                    else
                    {//edit block
                        drLocal = dvLocal[0].Row;
                        drLocal.BeginEdit();
                        UpdateTheDataRow("EDIT", ref drLocal);
                        drLocal.EndEdit();
                    }
                    // Reset the filter
                    dvLocal.RowFilter = null;
                    objApp.SaveData(ref dsLocal);

                    Cancel();

                    displayMsgs("Data saved Successfully......!!!!", "Ok", "Save");

                }
            }
            catch (System.Exception ex)
            {
                //throw (ex);
                displayMsgs("Error Details : <br/>" + ex.Message.ToString(), "Error", "Save");
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

        private void UpdateTheDataRow(string OPN_FLAG, ref System.Data.DataRow drLocal)
        {
            try
            {
                string filePath1="";
                string filePath2="";
                string filePath3="";
                string section1HTML = Server.HtmlEncode(this.txtSec1.Text.Trim());
                string section2HTML = Server.HtmlEncode(this.txtSec2.Text.Trim());
                string Contetn1HTML = Server.HtmlEncode(this.txtCon1.Text.Trim());
                string Content2HTML = Server.HtmlEncode(this.txtCon2.Text.Trim());
                //var secHtml =this.txtSec1.ToString();
                //var encodeHtmlSec1 = Server.HtmlEncode(secHtml);
                if (txtFileP1.HasFile)
                {
                    //File path 1
                    string fileName1 = System.IO.Path.GetFileName(this.txtFileP1.PostedFile.FileName);
                    string extension1 = System.IO.Path.GetExtension(fileName1); 
                    string uniqueFileName1 = this.txtEntryId.Text.ToString() + "A" + extension1; 
                    filePath1 = Server.MapPath("~/Documents/") + uniqueFileName1;
                    txtFileP1.SaveAs(filePath1);
                }
                if (txtFileP2.HasFile)
                {
					//File Path 2

					string fileName2 = System.IO.Path.GetFileName(this.txtFileP2.PostedFile.FileName);
                    string extension2 = System.IO.Path.GetExtension(fileName2); 
                    string uniqueFileName2 = this.txtEntryId.Text.ToString() + "B" + extension2; 
                    filePath2 = Server.MapPath("~/Documents/") + uniqueFileName2;
                    txtFileP2.SaveAs(filePath2);
				}
                if (txtFileP3.HasFile)
                {
                    //File Path 3
                    string fileName3 = System.IO.Path.GetFileName(this.txtFileP3.PostedFile.FileName);
                    string extension3 = System.IO.Path.GetExtension(fileName3); 
                    string uniqueFileName3 = this.txtEntryId.Text.ToString() + "C" + extension3; 
                    filePath3 = Server.MapPath("~/Documents/") + uniqueFileName3;
                    txtFileP3.SaveAs(filePath3);
                }

					if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["EntryID"] = bplib.clsWebLib.RetValidLen(this.txtEntryId.Text.ToString().Trim().ToUpper(), 20);  
                }
                drLocal["EntryDateTime"] = "" + bplib.clsWebLib.DateData_AppToDB(this.txtEntryDT.Text.ToString(), bplib.clsWebLib.DB_DATE_FORMAT);
                drLocal["Documents_Group"] = bplib.clsWebLib.RetValidLen(this.ddlDocGroup.Text.Trim(),50);
                drLocal["DocumentName"] = bplib.clsWebLib.RetValidLen(this.txtDocName.Text.Trim(),500);
                drLocal["DocumentDescription"] = bplib.clsWebLib.RetValidLen(this.txtDocDESC.Text.Trim(),500);
                drLocal["VersionNo"] = bplib.clsWebLib.RetValidLen(this.txtVersion.Text.Trim(),50);
                drLocal["BuildNo"] = bplib.clsWebLib.RetValidLen(this.txtBuild.Text.Trim(),50);
                drLocal["Header"] = bplib.clsWebLib.RetValidLen(this.txtHeader.Text.Trim());
                drLocal["Section1"] = section1HTML;//bplib.clsWebLib.RetValidLen(this.txtSec1.Text.Trim());
                drLocal["Content1"] = Contetn1HTML;
                drLocal["Section2"] = section2HTML;
                drLocal["Content2"] = Content2HTML;
                drLocal["Footer"] = bplib.clsWebLib.RetValidLen(this.txtFooter.Text.Trim());
                drLocal["FilePath1"] = filePath1;
				drLocal["FilePath2"] = filePath2;
				drLocal["FilePath3"] = filePath3;
                drLocal["CreateDate"] = DateTime.Now.ToString();
                drLocal["UpdateDate"] = DateTime.Now.ToString();
                drLocal["UpdateBy"] = Environment.MachineName; 
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //
            }
        } //eof

        private void EditExist()
        {
            try
            {
                this.lblViewName.Text = this.mvwDataVw.GetActiveView().ID.ToString();
                LoadData(true, "", "DOCMANAGER");
                this.tbValue.Text = "";
                this.lblSearchTitle.Text = "Search : DOCMANAGER";
                this.btnCancelSearch.Visible = true; //set as false in Cancel() function; if the search screen is the first screen
                this.mvwDataVw.SetActiveView(this.vw00);
            }
            catch (Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
            }
        }//eof

        private void LoadDetails()
        {
            System.Data.DataSet dsLocal = null;
            PWOMS.clsDocApplication objApp = null;
            string strSiteId = Session["USER_SITE"].ToString().Trim();

            try
            {
                if (this.txtEntryId.Text.Trim() == "" || this.txtEntryId.Text.Trim().Length > 20)
                {
                    return;
                }
                objApp = new PWOMS.clsDocApplication();
                objApp.GetDataOfDOC(this.txtEntryId.Text.Trim(), /*strSiteId.ToString(),*/ out dsLocal);
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    string decodedSection1HTML = Server.HtmlDecode("" + dsLocal.Tables[0].Rows[0]["Section1"].ToString());
                    string decodedSection2HTML = Server.HtmlDecode("" + dsLocal.Tables[0].Rows[0]["Section2"].ToString());
                    string decodedContent1HTML = Server.HtmlDecode("" + dsLocal.Tables[0].Rows[0]["Content1"].ToString());
                    string decodedContent2HTML = Server.HtmlDecode("" + dsLocal.Tables[0].Rows[0]["Content2"].ToString());
                    this.txtEntryId.Text = "" + dsLocal.Tables[0].Rows[0]["EntryID"].ToString();
                    this.txtEntryDT.Text = "" + bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[0]["EntryDateTime"].ToString().Trim(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                    this.ddlDocGroup.SelectedValue = "" + dsLocal.Tables[0].Rows[0]["Documents_Group"].ToString();
                    this.txtDocName.Text = "" + dsLocal.Tables[0].Rows[0]["DocumentName"].ToString();
                    this.txtDocDESC.Text = "" + dsLocal.Tables[0].Rows[0]["DocumentDescription"].ToString();
                    this.txtVersion.Text = "" + dsLocal.Tables[0].Rows[0]["VersionNo"].ToString();
                    this.txtBuild.Text = "" + dsLocal.Tables[0].Rows[0]["BuildNo"].ToString();
                    this.txtHeader.Text = "" + dsLocal.Tables[0].Rows[0]["Header"].ToString();
                    this.txtSec1.Text = decodedSection1HTML;
                    this.txtSec2.Text = decodedSection2HTML;
                    this.txtCon1.Text = decodedContent1HTML;
                    this.txtCon2.Text = decodedContent2HTML;
                    this.txtFooter.Text = "" + dsLocal.Tables[0].Rows[0]["Footer"].ToString();
                    if (!string.IsNullOrEmpty(dsLocal.Tables[0].Rows[0]["FilePath1"].ToString()))
                    {
                        lblFileP1.Text = dsLocal.Tables[0].Rows[0]["FilePath1"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dsLocal.Tables[0].Rows[0]["FilePath2"].ToString()))
                    {
                        lblFileP2.Text = dsLocal.Tables[0].Rows[0]["FilePath2"].ToString();
                    }
                    if (!string.IsNullOrEmpty(dsLocal.Tables[0].Rows[0]["FilePath3"].ToString()))
                    {
                        lblFileP3.Text = dsLocal.Tables[0].Rows[0]["FilePath3"].ToString();
                    }
                    Session["VERIFICATION_STATE"] = 1;
                    this.btnSave.Text = "Save";
                    Button_AddNew.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
                dsLocal = null;
            }
        }//eof

        private void DeleteData()
        {
            PWOMS.clsDocApplication objApp = null;
            bool DATA_OK = false;
            string strSiteId = Session["USER_SITE"].ToString().Trim();
            string modulename = "DOCMANAGER.DELETE";
            try
            {
                bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], modulename.ToUpper());
                objApp = new PWOMS.clsDocApplication();
                if (DATA_OK == false)
                {
                    if (this.txtEntryId.Text.Trim() == "" || this.txtEntryId.Text.Trim().Length > 20)
                    {
                        System.Exception ex = new Exception("Define the Entry Id...");
                        throw (ex);
                    }

                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    objApp.DeleteDataOfDOC(this.txtEntryId.Text.Trim(), /*strSiteId.ToString(),*/ "tblDOCMgt");

                    Cancel();
                    displayMsgs("Data delete successful !!!", "Ok", "DEL");
                }
            }
            catch (System.Exception ex)
            {
                displayMsgs("Error Details : <br/>" + ex.Message.ToString(), "Ok", "DEL");
            }
            finally
            {
                objApp = null;
            }
        }//eof

        private void LoadDynamicData()
        {
			System.Data.DataSet dsLocal;
			System.Data.DataTable dtLocal;
			bplib.clsFixedVariable objFix = null;
			PWOMS.clsDocApplication objApp = null;

			string strSiteId = Session["USER_SITE"].ToString().Trim();

			//string[] strGroupType = { "MANAGEMENT", "MERCHANDISING" };
			//string[] strHOType = { "Yes", "No" };

			try
			{
				objFix = new bplib.clsFixedVariable();
				objApp = new PWOMS.clsDocApplication();

				//Document Group Fixed Entity

				objFix.GetEntityFixedValiablesDesc(out dsLocal, "Documents_Group");
				this.ddlDocGroup.DataSource = dsLocal;
				this.ddlDocGroup.DataTextField = "CODE";
				this.ddlDocGroup.DataValueField = "CODE";
				this.ddlDocGroup.DataBind();
				this.ddlDocGroup.Items.Insert(0, new ListItem("", ""));
			}
			catch (System.Exception ex)
			{
				ShowLog("Error: \n" + ex.Message.ToString());
			}
			finally
			{
				dsLocal = null;
				objApp = null;
				objFix = null;
			}
		}//eof
        private void Cancel()
        {
            HideLog();
            ClearTheForm();
            Session["VERIFICATION_STATE"] = 0;
            //this.btnSave.Text = "Save";

            //this.panSearch.Visible = false;
            //this.dgSearch.DataSource = null;
            //this.dgSearch.Visible = false;
        }//eof
        private void SetDefault()
        {
            try
            {
                this.txtEntryId.Text = "";
                this.txtEntryDT.Text = "";
                this.ddlDocGroup.SelectedIndex = -1;
                this.txtDocName.Text = "";
                this.txtDocDESC.Text = "";
                this.txtVersion.Text = "";
                this.txtBuild.Text = "";
                this.txtHeader.Text = "";
                this.txtSec1.Text = "";
                this.txtSec2.Text = "";
                this.txtCon1.Text = "";
                this.txtCon2.Text = "";
                this.txtFooter.Text = "";

            }
            catch (System.Exception ex)
            {
                ShowLog("Error: \n" + ex.Message.ToString());
            }
            finally
            {
                //
            }
        }//eof

        private void LogOff()
        {
            //Session["LOGIN_STATUS"] = 0;
            //Session["USER"] = "";
            Session["VERIFICATION_STATE"] = 0;
            Response.Redirect("AppControlPanel.aspx");
        }//eof

        #endregion

        #endregion
    }
}