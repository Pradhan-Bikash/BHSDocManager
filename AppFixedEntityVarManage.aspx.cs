using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PWOMS
{
    public partial class AppFixedEntityVarManage : System.Web.UI.Page
    {
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {           
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            Master.masterlblFormName.Text = "Fixed Entity Create / Update";
            this.lblfrmName.Text= "Fixed Entity Create / Update";

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
            //----------------
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
                SetDefault();
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
        private string MessageText(string Code)
        {
            return clsSysLanguage.AppMessageText(Code, Session["LANG_APP_SWITCH"].ToString());
        }//eof

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
                ShowLog(ex.ToString());
            }
            finally
            {
            }
        }
        #endregion

        #endregion

        #region Customized Event

        #region SEARCH
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
                        stringKey = this.ddlSearchBy.SelectedValue.Trim() + " like '%" + this.tbValue.Text.Trim() + "%'";
                    }
                    LoadData(false, stringKey, FLAG);
                }
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
            }
        }//eof
        protected void btnCancelSearch_Click(object sender, EventArgs e)
        {
            CancelSearch();
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
                ShowLog(ex.ToString());
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
                ShowLog(ex.ToString());
            }
            finally
            {
            }
        }//eof
        #endregion

        #region DATA ENTRY EVENTS
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNew();
        }//eof
        protected void Button_Save_Click(object sender, EventArgs e)
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
        protected void btnLogOff_Click(object sender, EventArgs e)
        {
            LogOff();
        }//eof
        public void Button_Select_Click(object sender, System.EventArgs e)
        {
            LoadOnDataGridView();

        }//End function	
        protected void GridViewFixedVariable_Command(object sender, GridViewCommandEventArgs e)
        {
            string EntityType = "";
            string Code = "";
            string CollectionID = "";
            int RowID = 0;

            try
            {
                CollectionID = e.CommandName.ToString();

                if (CollectionID.Trim().ToUpper() == "SELECT")
                {
                    RowID = Int32.Parse(e.CommandArgument.ToString());

                    EntityType = this.GridViewFixedVariable.DataKeys[RowID].Value.ToString();

                    Code = this.GridViewFixedVariable.Rows[RowID].Cells[1].Text.ToString();
                    Session["VERIFICATION_STATE"] = 1;

                    this.txtEntryType.Text = EntityType;
                    this.txtCode.Text = Code;
                    Edit();
                    //LoadCompDetails(DOCID);

                }


            }
            catch (Exception ex)
            {
                displayMsgs("Error !!" + ex.Message.ToString(), "Error", "Save");
            }
            finally
            {
                RowID = 0;
                EntityType = "";
                Code = "";
            }

        }//eof
        #endregion
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
                    objApp.SearchTeleData(fromDate, toDate, strKey, strSiteId, out dsLocal);
                }
                if (FLAG == "TELEDATA")
                {
                    objApp.SearchTeleData(fromDate, toDate, strKey, strSiteId, out dsLocal);
                }
                this.lblViewState.Text = FLAG;
                //Make the 1/1/1901 blank
                if (rowNo > 0)
                {
                    //for (int i = 0; i < dsLocal.Tables[0].Columns.Count; i++)
                    //{
                    //    type = dsLocal.Tables[0].Columns[i].DataType.Name.ToString();
                    //    str = dsLocal.Tables[0].Columns[i].ColumnName.ToString().Trim();
                    //    if (dsLocal.Tables[0].Columns[i].DataType.Name == "DateTime")
                    //    {

                    //        for (int k = 0; k < dsLocal.Tables[0].Rows.Count; k++)
                    //        {
                    //            strs = "";
                    //            strs = String.Format("{0:d}", dsLocal.Tables[0].Rows[k][str]);
                    //            if ((strs == "1/1/1901") || (strs == "01/01/1901"))
                    //            {
                    //                dsLocal.Tables[0].Rows[k][str] = System.DBNull.Value;
                    //            }
                    //        }
                    //    }
                    //}
                }//eof
                dtLocal = dsLocal.Tables[0];

                LoadViewScreenInformation(ref dtLocal, "", IsLoad, FLAG);

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
                ShowLog("Error: \n" + ex.ToString());
            }
            finally
            {

            }
        }//end function
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
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        } // End Function 
        private DataTable GetTableDefination(ref DataTable dtFind)
        {

            DataTable dt = new DataTable("tblSearchKeyList");
            dt.Columns.Add("EntryID", typeof(String));

            for (Int32 i = 0; i < dtFind.Columns.Count; i++)
            {
                if (dtFind.Columns[i].DataType == typeof(System.String) ||
                    dtFind.Columns[i].DataType == typeof(System.Char))
                {
                    if (dtFind.Columns[i].ColumnName.ToString().Length >= 4)
                    {
                        if (dtFind.Columns[i].ColumnName.ToString().Substring(dtFind.Columns[i].ColumnName.ToString().Length - 4, 4).ToUpper() == "DATE")
                        {//do nothing
                        }
                        else
                        {
                            dt.Rows.Add(new Object[] { dtFind.Columns[i].ColumnName.ToString() });
                        }
                    }
                    else
                    {
                        dt.Rows.Add(new Object[] { dtFind.Columns[i].ColumnName.ToString() });
                    }
                }
            }
            dt.AcceptChanges();
            return dt;
        } // End Function 
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
                ShowLog(ex.ToString());
            }
            finally
            {
            }
        }//eof
        #endregion

        #region Dialog related Functions
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
                this.dlgMsg.Text = "Error : Delete operation is failed..... Please see the log";
                this.lblViewState.Text = "Error";
                this.dlgImage.ImageUrl = "Picture/error.png";
                this.dlgOk.Visible = true;
                this.dlgCancel.Visible = false;
                this.dlgMsg.ForeColor = System.Drawing.Color.Red;
                ShowLog(ex.Message.ToString());
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
                ShowLog(ez.ToString());
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
            try
            {
                if (this.ddlEntityType.SelectedValue != null)
                {
                    this.txtEntryType.Text = this.ddlEntityType.SelectedValue.ToString().Trim();
                    this.txtCode.Text = "";
                    this.txtDescription.Text = "";
                    this.txtValue.Text = "0";
                }
                else
                {
                    Cancel();
                }

                Session["VERIFICATION_STATE"] = 2;
                this.Button_Save.Text = "Create";
            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
            }
        } // end function
        private void SaveData()
        {
            bplib.clsFixedVariable objFixed;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;
            string modulename = "EntityFixedVariables.EDIT";
            objFixed = new bplib.clsFixedVariable();
            bool DATA_OK = false;
            try
            {
                bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], modulename.ToUpper());
                if ((int)Session["VERIFICATION_STATE"] != 0)
                {
                    if (DATA_OK == false)
                    {

                        if (this.txtEntryType.Text.Trim() == "" || this.txtEntryType.Text.Trim().Length > 50)
                        {
                            //System.Exception ex = new Exception("Define the Entry Type");
                            System.Exception ex = new Exception(MessageText("AB10001"));
                            throw (ex);
                        }
                        if (txtCode.Text.Trim() == "" || txtCode.Text.Trim().Length > 150)
                        {
                            //System.Exception ex = new Exception("Define the Code .... (max length 150)");
                            System.Exception ex = new Exception(MessageText("AB10002"));
                            throw (ex);
                        }
                        if (txtDescription.Text.Trim() == "" || txtDescription.Text.Trim().Length > 150)
                        {
                            //System.Exception ex = new Exception("Define the Decription .... (max length 150)");
                            System.Exception ex = new Exception(MessageText("AB10003"));
                            throw (ex);
                        }

                        if (bplib.clsWebLib.IsNumeric(this.txtValue.Text.Trim().ToString()) == false)
                        {
                            //System.Exception ex = new System.Exception("The Value Code Cannot be text");
                            System.Exception ex = new Exception(MessageText("AB10004"));
                            throw (ex);
                        }
                        DATA_OK = true;
                    }
                    if (DATA_OK == true)
                    {
                        // for add new a new auto res Id will be added

                        objFixed.GetFixedValiablesInformation(out dsLocal, this.txtEntryType.Text.Trim(), txtCode.Text.Trim(), "EntityFixedVariables");
                        dtLocal = dsLocal.Tables[0];
                        dvLocal = new DataView();
                        dvLocal.Table = dtLocal;
                        dvLocal.RowFilter = "Code='" + txtCode.Text.Trim() + "' and EntityType='" + this.txtEntryType.Text.Trim() + "'";

                        if (dvLocal.Count == 0) //addnew
                        {
                            drLocal = dtLocal.NewRow();
                            drLocal["EntityType"] = this.txtEntryType.Text.Trim();
                            drLocal["code"] = txtCode.Text.Trim();
                            drLocal["Description"] = txtDescription.Text.Trim();
                            drLocal["Value"] = txtValue.Text.Trim();
                            dtLocal.Rows.Add(drLocal);
                        }
                        else
                        {//edit block
                            drLocal = dvLocal[0].Row;
                            drLocal.BeginEdit();
                            drLocal["Description"] = txtDescription.Text.Trim();
                            drLocal["Value"] = txtValue.Text.Trim();
                            drLocal.EndEdit();
                        }
                        dvLocal.RowFilter = null;
                        objFixed.SaveFixedVaraible(ref dsLocal);

                        ShowLog("Data saved sucessfully...");
                        displayMsgs("Data saved Successfully......!!!!", "Ok", "Save");

                        LoadOnDataGridView();

                        Cancel();

                    }
                }
                else
                {
                    displayMsgs("Save is NOT possible now. Please press [Add new] / [Edit] as required and create / retrieve the data in the form, then press [Save / Create] button.", "Error", "Save");
                }
            }
            catch (System.Exception ex)
            {
                ShowLog("Error !! occurred in data saving process");
                displayMsgs("Error Details : <br/>" + ex.Message.ToString(), "Error", "Save");
            }
            finally
            {
                objFixed = null;
                drLocal = null;
                dvLocal = null;
                dtLocal = null;
                dsLocal = null;
            }
        }//eof
        private void Edit()
        {
            System.Data.DataSet dsLocal = null;
            bplib.clsFixedVariable objFixed = null;

            try
            {
                objFixed = new bplib.clsFixedVariable();
                Session["VERIFICATION_STATE"] = 1;

                objFixed.GetFixedValiablesInformation(out dsLocal, this.txtEntryType.Text.Trim(), txtCode.Text.Trim(), "EntityFixedVariables");
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    this.txtEntryType.Text = "" + "" + dsLocal.Tables[0].Rows[0]["EntityType"].ToString();
                    this.txtCode.Text = "" + "" + dsLocal.Tables[0].Rows[0]["Code"].ToString().Trim();
                    this.txtDescription.Text = "" + "" + dsLocal.Tables[0].Rows[0]["Description"].ToString().Trim();
                    this.txtValue.Text = "" + "" + dsLocal.Tables[0].Rows[0]["Value"].ToString();

                }
                else
                {
                    ShowLog("Select Data to the Grid !!!");
                }

                this.Button_Save.Text = "Update";
            }
            catch (System.Exception ex)
            {
                ShowLog("Error !! " + ex.ToString());
            }
            finally
            {
                objFixed = null;
                dsLocal = null;

            }
        } //eof
        private void DeleteData()
        {
            bplib.clsFixedVariable objFixed = null;
            bool DATA_OK = false;
            string modulename = "EntityFixedVariables.DELETE";
            try
            {
                bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], modulename.ToUpper());
                if ((int)Session["VERIFICATION_STATE"] != 0)
                {
                    if (DATA_OK == false)
                    {

                        if (this.txtEntryType.Text.Trim() == "")
                        {
                            //System.Exception ex = new Exception("Select Entry Type .... ");
                            System.Exception ex = new Exception(MessageText("AB10001"));
                            throw (ex);
                        }
                        if (txtCode.Text.Trim() == "")
                        {
                            //System.Exception ex = new Exception("Select the Code .... ");
                            System.Exception ex = new Exception(MessageText("AB10005"));
                            throw (ex);
                        }
                        DATA_OK = true;
                    }
                    if (DATA_OK == true)
                    {
                        objFixed = new bplib.clsFixedVariable();
                        objFixed.DeleteFixedVaraibleInfo(this.txtEntryType.Text.Trim().ToString(), txtCode.Text.Trim().ToString(), "EntityFixedVariables");

                        Cancel();
                        displayMsgs("Data delete successful !!!", "Ok", "DEL");


                    }
                    else
                    {
                        displayMsgs("Delete is NOT possible now. Please press <Edit> and retrieve the data in the form, then press <Delete> button.", "Ok", "DEL");
                    }
                }
            }
            catch (System.Exception ex)
            {                
                displayMsgs("Error Details : <br/>" + ex.Message.ToString(), "Error", "DEL");
            }
            finally
            {
                objFixed = null;
            }

        }//eof      
        private void LoadOnDataGridView()
        {
            System.Data.DataSet dsLocal = null;

            bplib.clsFixedVariable objFixed = null;
            this.GridViewFixedVariable.DataSource = null;

            try
            {
                if (this.ddlEntityType.SelectedValue == null)
                {
                    return;
                }

                objFixed = new bplib.clsFixedVariable();
                this.ddlEntityType.Enabled = false;

                objFixed.GetEntityFixedValiables(out dsLocal, this.ddlEntityType.SelectedValue.Trim());
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    this.GridViewFixedVariable.DataSource = dsLocal;
                    this.GridViewFixedVariable.DataBind();
                    this.GridViewFixedVariable.Visible = true;
                }
                else
                {
                    this.GridViewFixedVariable.Visible = false;
                }

                Button_Save.Enabled = true;
                this.Button_Save.Text = "Save";
                Button_Delete.Enabled = true;
            }
            catch (System.Exception ex)
            {
                ShowLog("Error !! " + ex.ToString());
            }
            finally
            {
                objFixed = null;
                dsLocal = null;
            }
        }//eof
        private void LoadDynamicData()
        {
            System.Data.DataSet dsLocal;
            System.Data.DataTable dtLocal;
            bplib.clsFixedVariable objApp = null;

            string[] strGroupType = { "MANAGEMENT", "MERCHANDISING", "IT" };
            string[] strHOType = { "Yes", "No" };

            try
            {

                objApp = new bplib.clsFixedVariable();
                objApp.GetWebComboFixedVariable(out dsLocal, "EntityFixedVariables", "EntityType");

                this.ddlEntityType.DataSource = dsLocal;
                this.ddlEntityType.DataTextField = "EntityType";
                this.ddlEntityType.DataValueField = "EntityType";
                this.ddlEntityType.DataBind();
                this.ddlEntityType.SelectedIndex = -1;

            }
            catch (System.Exception ex)
            {
                ShowLog("Error !! " + ex.ToString());
            }
            finally
            {
                dsLocal = null;
                objApp = null;
            }
        }//eof
        private void Cancel()
        {

            try
            {
                this.ddlEntityType.SelectedIndex = -1;
                GridViewFixedVariable.DataSource = null;
                GridViewFixedVariable.Visible = false;

                this.Button_Select.Enabled = true;
                this.Button_Cancel.Enabled = true;
                this.ddlEntityType.Enabled = true;
                this.txtEntryType.Text = "";
                this.txtDescription.Text = "";
                this.txtCode.Text = "";
                this.txtValue.Text = "";
                this.Button_Save.Text = "Save";

            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        }//eof
        private void SetDefault()
        {
            try
            {

                this.ddlEntityType.Enabled = true;
                LoadDynamicData();
                LoadOnDataGridView();
                this.txtEntryType.Text = "";
                this.txtDescription.Text = "";
                this.txtCode.Text = "";
                this.txtValue.Text = "";
                this.Button_Save.Text = "Save";
                Button_Select.Enabled = true;
            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
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
            Response.Redirect("AppControlPanel.aspx?cat=Admin");
        }//eof
        #endregion

        #endregion

    }
}