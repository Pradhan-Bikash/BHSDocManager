using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace PWOMS
{
    public partial class webFrmBizRegInfo : System.Web.UI.Page
    {
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            Master.masterlblFormName.Text = "Business Registration Info";
            this.lblfrmName.Text = "Business Registration Info";

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
                    //lblInfo.Text = "The entry form is in default state. If you want to add new data, press [add new] button. If you want to edit any existing data please press [Edit] button.";
                    LoadDynamicData();
                    loadLanguageOnLabel();
                    Cancel();
                    //if (Request.QueryString["id01"] == null || Request.QueryString["id01"] == "")
                    //{
                    //    SetDefault();
                    //}
                    //else
                    //{
                    //    SetDefault();
                    //    this.TextSystemID.Text = Request.QueryString["id01"].ToString().Trim();
                    //    LoadDetails();
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
                // e.Item is the row of the table where the command fired
                // For bound columns the value is stored in the Text property of TableCell
                // Start from '0'
                TableCell DOCIDCell = e.Item.Cells[1];
                TableCell CollectionIDCell = e.Item.Cells[2];
                DOCID = DOCIDCell.Text;
                CollectionID = CollectionIDCell.Text;
            }
            if (((LinkButton)e.CommandSource).CommandName == "EditFile")
            {
                //Edit
                if (this.lblViewState.Text.Trim() == "Employee Info")
                {
                    this.lblEmpInchargeID.Text = DOCID;
                    this.txtEmpIncharge.Text = CollectionID;
                }
                if (this.lblViewState.Text.Trim() == "Business Registration Info")
                {
                    Cancel();
                    this.txtSystemID.Text = DOCID;
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

        protected void Button_Edit_Click(object sender, EventArgs e)
        {
            EditExist();
        }//eof

        protected void Button_FindEmpIncharge_Click(object sender, EventArgs e)
        {
            FindEmployee();
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

        protected void Button_LogOff_Click(object sender, EventArgs e)
        {
            LogOff();
        }//eof
        #endregion

        #endregion

        #region Customized Functions

        #region SEARCH RELATED FUNCTIONS
        private void LoadData(bool IsLoad, string strKey, string FLAG)
        {

            string SB = "";
            PWOMS.clsApplication objApp = null;
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
                objApp = new PWOMS.clsApplication();
                if (FLAG == "") //this is default search function
                {
                    //objApp.SearchTeleData(fromDate, toDate, strKey,strSiteId, out dsLocal);
                }
                if (FLAG == "Business Registration Info")
                {
                    objApp.SearchBizRegInfoData(fromDate, toDate, strKey, strSiteId, out dsLocal);
                }
                if (FLAG == "Employee Info")
                {
                    objApp.SearchEmployeeData(fromDate, toDate, strKey, strSiteId, out dsLocal);
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
            bplib.clsGenID objGenID = null;
            string strResCode = null;
            try
            {
                Cancel();
                Session["VERIFICATION_STATE"] = 2;

                this.txtSystemID.Text = "";
                this.txtSystemID.Enabled = false;

                // for add new a new auto Company Id will be added
                if ((int)Session["VERIFICATION_STATE"] == 2)
                {
                    objGenID = new bplib.clsGenID();
                    objGenID.GenID(System.DateTime.Now.ToShortDateString().ToString(), "BizRegInfo", out strResCode);
                    strResCode = "B" + strResCode;
                    this.txtSystemID.Text = strResCode;
                    
                }
                
                this.Button_save.Text = "Create";
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
            string modulename = "Business Registration.EDIT";
            string strSiteId = Session["USER_SITE"].ToString().Trim();
            PWOMS.clsApplication objApp = null;

            bool DATA_OK = false;
            try
            {
                bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], modulename.ToUpper());
                objApp = new PWOMS.clsApplication();
                if (DATA_OK == false)
                {
                    if (this.txtSystemID.Text.Trim() == "" || this.txtSystemID.Text.Trim().Length > 20)
                    {
                        System.Exception ex = new Exception("Define the System ID...(Max length allowed 20)");
                        throw (ex);
                    }
                    if (this.ddlLocation.SelectedValue.Trim() == "" || ddlLocation.SelectedValue.Trim().Length > 150)
                    {
                        System.Exception ex = new Exception("Define the Location...(Max length allowed 150)");
                        throw (ex);
                    }
                    if (this.txtBRNumber.Text.Trim() == "" || this.txtBRNumber.Text.Trim().Length > 100)
                    {
                        System.Exception ex = new Exception("Define the Registration Number...(Max length allowed 100)");
                        throw (ex);
                    }
                    if (this.txtExpiryDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtExpiryDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the Expiry Date.(allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }
                    if (this.txtRegisteredAddress.Text.Trim() == "" || this.txtRegisteredAddress.Text.Trim().Length > 250)
                    {
                        System.Exception ex = new Exception("Define the Registered Address...(Max length allowed 250)");
                        throw (ex);
                    }

                    DATA_OK = true;
                }

                if (DATA_OK == true)
                    {
                        objApp.GetDataOfBizRegInfo(this.txtSystemID.Text.Trim(), strSiteId.ToString(), out dsLocal);
                        dtLocal = dsLocal.Tables[0];
                        dvLocal = new DataView();
                        dvLocal.Table = dtLocal;

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
                        dvLocal.RowFilter = null;
                        objApp.SaveData(ref dsLocal);

                        Cancel();
                        displayMsgs("Data saved Successfully......!!!!", "Ok", "Save");                        

                    }
            }
            catch (System.Exception ex)
            {
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

                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["SystemID"] = bplib.clsWebLib.RetValidLen(this.txtSystemID.Text.ToString().Trim().ToUpper(), 20);
                    drLocal["DateAdded"] = "" + bplib.clsWebLib.DateData_AppToDB(System.DateTime.Now.ToShortDateString(), bplib.clsWebLib.DB_DATE_FORMAT);
                    drLocal["AddedBy"] = bplib.clsWebLib.RetValidLen(((string)Session["USER"]), 20);
                }
                drLocal["Location"] = bplib.clsWebLib.RetValidLen(this.ddlLocation.SelectedValue.ToString().Trim(), 150);
                drLocal["BRNumber"] = bplib.clsWebLib.RetValidLen(this.txtBRNumber.Text.Trim(), 100);             
                drLocal["ExpiryDate"] = "" + bplib.clsWebLib.DateData_AppToDB(this.txtExpiryDate.Text.ToString(), bplib.clsWebLib.DB_DATE_FORMAT);
                
                drLocal["RegisteredAddress"] = bplib.clsWebLib.RetValidLen(this.txtRegisteredAddress.Text.Trim(), 250);
                drLocal["EmpInchargeID"] = bplib.clsWebLib.RetValidLen(this.lblEmpInchargeID.Text.Trim(), 20);

                drLocal["UpdateOn"] = "" + bplib.clsWebLib.DateData_AppToDB(System.DateTime.Now.ToShortDateString(), bplib.clsWebLib.DB_DATE_FORMAT);
                drLocal["UpdateBy"] = bplib.clsWebLib.RetValidLen(((string)Session["USER"]), 20);
                drLocal["SITEID"] = bplib.clsWebLib.RetValidLen(((string)Session["USER_SITE"]), 20);            
                

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
                LoadData(true, "", "Business Registration Info");
                this.tbValue.Text = "";
                this.lblSearchTitle.Text = "Search : Business Registration Info";
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
        private void FindEmployee()
        {
            try
            {
                this.lblViewName.Text = this.mvwDataVw.GetActiveView().ID.ToString();
                LoadData(true, "", "Employee Info");
                this.tbValue.Text = "";
                this.lblSearchTitle.Text = "Search : Employee Info";
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
            PWOMS.clsApplication objApp = null;
            string strSiteId = Session["USER_SITE"].ToString().Trim();

            try
            {
                if (this.txtSystemID.Text.Trim() == "" || this.txtSystemID.Text.Trim().Length > 20)
                {
                    return;
                }
                objApp = new PWOMS.clsApplication();
                objApp.GetDataBizRegInfoForLoad(this.txtSystemID.Text.Trim(), strSiteId.ToString(), out dsLocal);
                if (dsLocal.Tables[0].Rows.Count > 0)
                {                 
                    this.txtSystemID.Text = "" + dsLocal.Tables[0].Rows[0]["SystemID"].ToString();
                    this.ddlLocation.SelectedValue = "" + dsLocal.Tables[0].Rows[0]["Location"].ToString();
                    this.txtBRNumber.Text = "" + dsLocal.Tables[0].Rows[0]["BRNumber"].ToString();                    
                    this.txtExpiryDate.Text = "" + bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[0]["ExpiryDate"].ToString().Trim(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                    this.txtRegisteredAddress.Text = "" + dsLocal.Tables[0].Rows[0]["RegisteredAddress"].ToString();
                    this.lblEmpInchargeID.Text = "" + dsLocal.Tables[0].Rows[0]["EmpInchargeID"].ToString();
                    this.txtEmpIncharge.Text = "" + dsLocal.Tables[0].Rows[0]["EmpName"].ToString();

                    Session["VERIFICATION_STATE"] = 1;
                    this.Button_save.Text = "Save";
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
            PWOMS.clsApplication objApp = null;
            bool DATA_OK = false;
            string strSiteId = Session["USER_SITE"].ToString().Trim();
            string modulename = "Business Registration.DELETE";
            try
            {
                bplib.clsAppSeq.CheckUserAccess((string)Session["USER"], modulename.ToUpper());
                objApp = new PWOMS.clsApplication();
                if (DATA_OK == false)
                {
                    if (this.txtSystemID.Text.Trim() == "" || this.txtSystemID.Text.Trim().Length > 20)
                    {
                        System.Exception ex = new Exception("Define the System ID...");
                        throw (ex);
                    }

                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    objApp.DeleteDataOfBizRegInfo(this.txtSystemID.Text.Trim(), strSiteId.ToString(), "tblBizRegInfo");

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
            PWOMS.clsApplication objApp = null;

            string strSiteId = Session["USER_SITE"].ToString().Trim();

            string[] strGroupType = { "MANAGEMENT", "MERCHANDISING" };
            string[] strHOType = { "Yes", "No" };

            try
            {

                objFix = new bplib.clsFixedVariable();
                objApp = new PWOMS.clsApplication();               

                //LOCATION data - FixedVariables
                objFix.GetEntityFixedValiablesDesc(out dsLocal, "LOCATION");
                this.ddlLocation.DataSource = dsLocal;
                this.ddlLocation.DataTextField = "CODE";
                this.ddlLocation.DataValueField = "CODE";
                this.ddlLocation.DataBind();
                this.ddlLocation.Items.Insert(0, new ListItem("", ""));
                

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
            
            this.Button_AddNew.Visible = true;
            this.Button_Edit.Visible = true;
            this.Button_save.Text = "Save";
            this.Button_Delete.Enabled = true;

            this.panSearch.Visible = false;
            this.dgSearch.DataSource = null;
            this.dgSearch.Visible = false;
        }//eof
        private void SetDefault()
        {
            try
            {                
                this.txtSystemID.Text = "";
                this.ddlLocation.SelectedIndex = -1;
                this.txtBRNumber.Text = "";
                this.txtExpiryDate.Text = "";
                this.txtRegisteredAddress.Text = "";
                this.lblEmpInchargeID.Text = "";
                this.txtEmpIncharge.Text = "";

                this.lblViewState.Text = "";
                this.lblDlgState.Text = "";
                this.lblSearchTitle.Text = "Search";
                this.lblViewName.Text = "";

               
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