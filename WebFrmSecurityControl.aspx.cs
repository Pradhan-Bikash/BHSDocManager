using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace BPWEBAccessControl
{
    public partial class WebFrmSecurityControl : System.Web.UI.Page
    {
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {
            Master.Page.Title = Global.PageTitle;
            ((Label)Master.FindControl("lblFormName")).Text = "User Access Control";

            if (Session["DEAFULT_LOGIN"] == null || (int)Session["DEAFULT_LOGIN"] == 0)
            {
                Page.Response.Redirect("default.aspx");
                return;
            }
            else
            {
                if (Session["LOGIN_STATUS"] == null || (int)Session["LOGIN_STATUS"] == 0)
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
                    Cancel();
                    tabMenuNavigation();

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
        }//eof
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
                ShowLog(ex.Message.ToString());
            }
            finally
            {
                //
            }
        }//eof
        public void ShowLog(string strMessage)
        {
            this.panError.Visible = true;
            TxtMsgBox.Text = strMessage;
        }//eof
        public void HideLog()
        {
            this.panError.Visible = false;
            TxtMsgBox.Text = "";

        }//eof
        private void DisplayUserName()
        {
            ((Label)Master.FindControl("lblUserId")).Text = (string)Session["USER"];
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
                ShowLog(ex.Message.ToString());
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
                    LoadData(false, FLAG, "", "", stringKey);
                }
            }
            catch (Exception ex)
            {
                ShowLog(ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        protected void btnCancelSearch_Click(object sender, EventArgs e)
        {
            cancelSearch();
        }//eof
        protected void gvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string vwIndex = "";
            string DOCID = "";
            string strFileEntryId = "";
            string strPiRefNo = "";
            try
            {
                vwIndex = this.lblViewName.Text;
                if (this.lblViewState.Text.Trim() == "UDATA")
                {
                    /*
                     * index will give you the selected row ID, provided by the DataKeyNames attribute in your .aspx page. This however does require the "Enable Selection" to be checked. (Go to your .aspx page, designer, click your gridview, you should see the "Enable selection" attribute).
                     * */
                    //int index = Convert.ToInt16(this.gvSearch.SelectedDataKey.Value);
                    DOCID = this.gvSearch.SelectedRow.Cells[1].Text;
                    CancelUser();
                    this.tbUserId.Text = DOCID;
                    LoadUserDetails();
                }
                this.mvwDataVw.ActiveViewIndex = returnView(vwIndex.Trim());
                setDefaultSearch();
            }
            catch (Exception ex)
            {

                ShowLog("Error : \n" + ex.Message.ToString());
            }
        }//eof
        protected void btnSearchDate_Click(object sender, EventArgs e)
        {
            string FLAG = this.lblViewState.Text.Trim();
            try
            {
                if (string.IsNullOrWhiteSpace(this.tbFromDate.Text) == true || clsWebProcDataBuilder.convertToDateTime(this.tbFromDate.Text.Trim(), clsWebProcDataBuilder.CheckDateFormat.CHECK).Year == 1901)
                {
                    throw new Exception("Defaine From Date..............(Date Format Example : 01-Jan-2008)");
                }
                if (string.IsNullOrWhiteSpace(this.tbToDate.Text) == true || clsWebProcDataBuilder.convertToDateTime(this.tbToDate.Text.Trim(), clsWebProcDataBuilder.CheckDateFormat.CHECK).Year == 1901)
                {
                    throw new Exception("Defaine To Date..............(Date Format Example : 01-Jan-2008)");
                }
                if (System.DateTime.Compare(clsWebProcDataBuilder.convertToDateTime(this.tbFromDate.Text.Trim(), clsWebProcDataBuilder.CheckDateFormat.CHECK), clsWebProcDataBuilder.convertToDateTime(this.tbToDate.Text.Trim(), clsWebProcDataBuilder.CheckDateFormat.CHECK)) > 0)
                {
                    throw new Exception("Defaine From / To Date..............(From Date cannot be later than To Date)");
                }
                LoadData(false, FLAG, this.tbFromDate.Text, this.tbToDate.Text, "");
            }
            catch (Exception ex)
            {
                ShowLog(ex.Message.ToString());
            }
            finally
            {
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
                ShowLog(ex.Message.ToString());
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
                ShowLog(ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        #endregion

        #region DATA ENTRY EVENTS

        //-------------App Access Control--------
        protected void btnMenuListUpdate_Click(object sender, EventArgs e)
        {
            loadMenuLists();
            
            this.tbSearchValue.Text = "";
        }//eof
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            getUserDetails();
        }//eof
        public void RowDataBound_Status(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            string Testing = "";
            int iRate = 0;
            //System.DateTime dt;
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Testing = (DataBinder.Eval(e.Row.DataItem, "RATE")).ToString();
                    iRate = Convert.ToInt32(bplib.clsWebLib.GetNumData(Testing.Trim()));
                    if (iRate == 0)
                    {
                        //e.Row.BackColor = System.Drawing.Color.Yellow;
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFFF");
                        e.Row.Font.Bold = false;
                    }
                    if (iRate == 25)
                    {
                        //e.Row.BackColor = System.Drawing.Color.Yellow;
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#00FFFF");
                        e.Row.Font.Bold = true;
                    }
                    if (iRate == 50)
                    {
                        //e.Row.BackColor = System.Drawing.Color.Yellow;
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFF00");
                        e.Row.Font.Bold = true;
                    }
                    if (iRate == 75)
                    {
                        //e.Row.BackColor = System.Drawing.Color.Yellow;
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#7CFC00");
                        e.Row.Font.Bold = true;
                    }
                    //dt = Convert.ToDateTime((DataBinder.Eval(e.Row.DataItem, "sentDate")).ToString().Trim());
                    //if (dt.Year == 1901 || dt.Year == 1900 || dt.Year == 1)
                    //{
                    //    e.Row.Cells[5].Text = "";
                    //}
                }
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }

        }//eof
        protected void btnSearchIn_Click(object sender, EventArgs e)
        {
            Cancel();
            MyDataGrid.DataSource = UserAccManagerInfo("SEARCH");
            MyDataGrid.DataBind();
            this.panGrid.Visible = true;
            //this.btnRefresh.Enabled = true; --- call within the UserAccManagerInfo("SEARCH") function
        }//eof
        protected void cbModule_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.MyDataGrid.Rows.Count > 0)
                {
                    foreach (GridViewRow gr in this.MyDataGrid.Rows)
                    {
                        CheckBox cb1 = (CheckBox)(gr.FindControl("cbAccStat"));
                        CheckBox cb2 = (CheckBox)(gr.FindControl("cbEditStat"));
                        CheckBox cb3 = (CheckBox)(gr.FindControl("cbDelStat"));
                        cb1.Checked = this.cbModule.Checked;
                        cb2.Checked = this.cbModule.Checked;
                        cb3.Checked = this.cbModule.Checked;
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
            }
        }//eof
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Session["VERIFICATION_STATE"] = 2;
            SaveData();
        }//eof
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            displayMsgs("Are you sure to delete this data???", "Confirmation", "Delete");
        }//eof
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Cancel();
            this.tbSearchValue.Text = "";
        }//eof



        //---------User Create / Edit / Delete--
        protected void btnAddNewUser_Click(object sender, EventArgs e)
        {
            addNewUsers();
        }//eof
        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            EditExistUsers();
        }//eof
        protected void btnSaveUser_Click(object sender, EventArgs e)
        {
            SaveUserData();
        }//eof
        protected void btnDelUser_Click(object sender, EventArgs e)
        {
            displayMsgs("Are you sure to delete this data??? \nBy Deleting this Data all the access allocate to this User will also be deleted!", "Confirmation", "Delete");
        }//eof
        protected void btnCancelUser_Click(object sender, EventArgs e)
        {
            CancelUser();
        }//eof


        //------Copy Access----------
        protected void btnCopyAcc_Click(object sender, EventArgs e)
        {
            CopyAccess();
        }//eof
        protected void linbtnRefreshCopyAccess_Click(object sender, EventArgs e)
        {
            loadAllUsers();
        }//eof
        protected void linbtnCancelCopyAcc_Click(object sender, EventArgs e)
        {
            loadAllUsers();
            this.tbLog.Text = "";
        }//eof


        //---------System Admin Info----------
        protected void linbtnSysSave_Click(object sender, EventArgs e)
        {
            SaveData_Sysuser();
        }//eof
        protected void linbtnSysDel_Click(object sender, EventArgs e)
        {
            displayMsgs("Are you sure to delete this data???", "Confirmation", "Delete");
        }//eof
        protected void linbtnSysCancel_Click(object sender, EventArgs e)
        {
            CancelSysUser();
        }//eof
        protected void tbSysUserId_TextChanged(object sender, EventArgs e)
        {
            this.linbtnSysDel.Enabled = false;
            this.linbtnSysSave.Enabled = true;
            this.linbtnSysSave.Text = "Create";
            Session["VERIFICATION_STATE"] = 2;
        }//eof
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            loadSelectedUser();
        }//eof


        //----------Site Access----------
        protected void btnSiteCheck_Click(object sender, EventArgs e)
        {
            getSiteUserDetails();
            loadSiteLists();
        }//eof
        protected void btnSaveSite_Click(object sender, EventArgs e)
        {
            if (this.ddlSiteUsers.SelectedValue.ToString().Trim() != "SELECT USER")
            {
                SaveSiteData();
            }
        }//eof
        protected void cbSelectSites_CheckedChanged(object sender, EventArgs e)
        {
            if (this.gvSite.Rows.Count > 0)
            {
                foreach (GridViewRow gr in this.gvSite.Rows)
                {
                    CheckBox cb1 = (CheckBox)(gr.FindControl("cbAccStat"));
                    cb1.Checked = this.cbSelectSites.Checked;
                }
            }
        }//eof
        protected void ddlSiteUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["VERIFICATION_STATE"] = 2;
            loadSiteLists();
        }//eof
        protected void btnDelSite_Click(object sender, EventArgs e)
        {
            if (this.ddlSiteUsers.SelectedValue.ToString().Trim() != "SELECT USER")
            {
                displayMsgs("Are you sure to delete this data??? \nBy Deleting this Data all the Site Allocation to this User will also be deleted!", "Confirmation", "Delete");
            }
        }//eof
        protected void btnSiteCancel_Click(object sender, EventArgs e)
        {
            CancelSite();
        }//eof


        protected void tab_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton link = sender as LinkButton;
                string strNAVFLAG = link.CommandName.ToString();
                tabMenuNavigation(strNAVFLAG);
            }
            catch (Exception ex)
            {
                ShowLog("Error in TAB operation........\n" + ex.ToString());
            }

        }//eof
        protected void vwActivate_EnableLogOff(object sender, EventArgs e)
        {
            this.btnLogOff.Visible = true;

            this.tabUserCreate.Visible = true;
            this.tabUserAcc.Visible = true;
            this.tabAccCopy.Visible = true;
            this.tabSysInfo.Visible = true;
            this.tabSite.Visible = true;

        }//eof
        protected void vwActivate_DisableLogOff(object sender, EventArgs e)
        {
            this.btnLogOff.Visible = false;

            this.tabUserCreate.Visible = false;
            this.tabUserAcc.Visible = false;
            this.tabAccCopy.Visible = false;
            this.tabSysInfo.Visible = false;
            this.tabSite.Visible = false;

        }//eof
        protected void btnLogOff_Click(object sender, EventArgs e)
        {
            LogOff();
        }//eof

        #endregion
        #endregion

        #region Customized Functions

        #region SEARCH RELATED FUNCTIONS
        private void LoadData(bool isLoad, string FLAG, string strFromDate, string strToDate, string strKey)
        {

            string SB = "";
            BPWEBAccessControl.clsSecurityControl objApp = null;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            this.panSearch.Visible = false;
            this.gvSearch.Visible = false;
            int rowNo = 0;
            string strSiteId = Session["USER_SITE"].ToString().Trim();
            string strUserId = "";
            this.lblSearchResult.Text = "";
            try
            {
                objApp = new BPWEBAccessControl.clsSecurityControl();
                strUserId = bplib.clsWebLib.RetValidLen(((string)Session["USER"]), 20).ToString();
                //SEARCH DATA KEY NAME = "UserID"
                if (string.IsNullOrWhiteSpace(FLAG) || FLAG.Trim().ToUpper() == "UDATA")
                {
                    objApp.SearchUserData(strFromDate, strToDate, strKey, out dsLocal);
                    dtLocal = new System.Data.DataTable();
                    if (isLoad)
                    {
                        dtLocal = dsLocal.Tables[0].DefaultView.ToTable(true, new string[] { "UserID", "UserGroup", "UserLocation" });
                        LoadViewScreenInformation(ref dtLocal, "", isLoad, FLAG);
                    }
                    dtLocal = dsLocal.Tables[0].DefaultView.ToTable(true, new string[] { "UserID", "Password", "UserGroup", "UserLocation", "UpdateOn" });
                }

                this.lblViewState.Text = FLAG;



                this.gvSearch.DataSource = dtLocal;
                this.gvSearch.DataBind();
                rowNo = dtLocal.Rows.Count;
                if (rowNo == 0)
                {
                    this.panSearch.Visible = false;
                    this.gvSearch.Visible = false;
                    if (!isLoad)
                    {
                        this.lblSearchResult.Text = "No Record's found......";
                    }
                }
                else
                {
                    this.panSearch.Visible = true;
                    this.gvSearch.Visible = true;
                    if (!isLoad)
                    {
                        this.lblSearchResult.Text = rowNo + " Record(s) found...";
                    }
                }


            }
            catch (System.Exception ex)
            {
                throw ex;
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
                ShowLog(ex.Message.ToString());
            }
            finally
            {
                //
            }
        }//eof
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
        }//eof
        private void cancelSearch()
        {
            string vwIndex = this.lblViewName.Text;
            try
            {
                setDefaultSearch();
                if (vwIndex.Trim().ToUpper() == "VW03")
                {
                    this.mvwDataVw.SetActiveView(this.vw03);
                }
                //else
                //{
                //    this.mvwDataVw.SetActiveView(this.vw00);
                //}
            }
            catch (Exception ex)
            {
                ShowLog(ex.Message.ToString());
            }
            finally
            {
            }
        }//eof
        private void setDefaultSearch()
        {
            this.lblViewState.Text = "";
            this.lblViewName.Text = "";
            this.lblSearchResult.Text = "";
            this.lblSearchTitle.Text = "Search";
            this.gvSearch.Visible = false;
            this.panSearch.Visible = false;
            this.tbValue.Text = "";
            this.tbFromDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
            this.tbToDate.Text = System.DateTime.Today.ToString("dd-MMM-yyyy");
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
                        Session["VERIFICATION_STATE"] = 1;
                        if (vwName.Trim() == "VW02")
                        {
                            DeleteData();
                        }
                        if (vwName.Trim() == "VW03")
                        {
                            DeleteUserData();
                        }
                        if (vwName.Trim() == "VW05")
                        {
                            DeleteData_Sysuser();
                        }
                        if (vwName.Trim() == "VW06")
                        {
                            DeleteSiteData();
                        }
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
                ShowLog(ex.ToString());
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
                ShowLog(ez.Message.ToString());
            }
            finally
            {
            }

            return vwNo;
        }//eof
        #endregion

        #region DATA ENTRY RELATED FUNCTIONS

        #region App Access Control [VW02]
        private void loadMenuLists()
        {

            try
            {
                Cancel();
                MyDataGrid.DataSource = UserAccManagerInfo("");
                MyDataGrid.DataBind();
                this.panGrid.Visible = true;
                this.btnRefresh.Enabled = true;
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        }//eof
        private void getUserDetails()
        {
            string userCode = "";
            BPWEBAccessControl.clsSecurityControl objApp = null;
            System.Data.DataSet dsLocal = null;
            try
            {
                userCode = this.ddlUserID.SelectedValue.ToString().Trim();
                objApp = new BPWEBAccessControl.clsSecurityControl();
                if (userCode.Trim() == "" || userCode.ToUpper().Trim() == "SELECT USER")
                {
                    objApp.GetUserAccManagerInfo(out dsLocal);
                    this.ddlUserID.DataSource = dsLocal.Tables[0].DefaultView;
                    this.ddlUserID.DataTextField = "USERID";
                    this.ddlUserID.DataValueField = "USERID";
                    this.ddlUserID.DataBind();
                    this.ddlUserID.Items.Add("SELECT USER");
                    this.ddlUserID.SelectedValue = "SELECT USER";
                    loadMenuLists();
                    this.btnSave.Enabled = false;
                    this.btnDelete.Enabled = false;
                }
                else
                {

                    MyDataGrid.DataSource = UserAccManagerInfo(userCode.Trim());
                    objApp.GetUserAccManagerInfo(out dsLocal);
                    MyDataGrid.DataBind();
                    this.panGrid.Visible = true;
                    this.btnSave.Enabled = true;
                    this.btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                dsLocal = null;
            }
        }//eof
        private DataTable UserAccManagerInfo(string UserCode)
        {
            System.Data.DataSet dsRef = null;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtUserAccess = null;
            BPWEBAccessControl.clsSecurityControl objApp = null;

            string moduleName = "";
            double moduleId = 0;
            int iPrimKey = 0;
            int iRate = 0;
            string searchKey = "";

            DataColumn col1;
            DataColumn col2;
            DataColumn col3;
            DataColumn col4;
            DataColumn col5;
            DataColumn col6;
            DataColumn col7;

            try
            {


                dtUserAccess = new System.Data.DataTable();
                col1 = new DataColumn("PRIKEY");
                col2 = new DataColumn("ModuleID");
                col3 = new DataColumn("MODULENAME");
                col4 = new DataColumn("ACCESS");
                col5 = new DataColumn("EDIT");
                col6 = new DataColumn("DELETE");
                col7 = new DataColumn("RATE");



                col1.DataType = System.Type.GetType("System.String");
                col2.DataType = System.Type.GetType("System.Double");
                col3.DataType = System.Type.GetType("System.String");
                col4.DataType = System.Type.GetType("System.Boolean");
                col5.DataType = System.Type.GetType("System.Boolean");
                col6.DataType = System.Type.GetType("System.Boolean");
                col7.DataType = System.Type.GetType("System.Int32");


                dtUserAccess.Columns.Add(col1);
                dtUserAccess.Columns.Add(col2);
                dtUserAccess.Columns.Add(col3);
                dtUserAccess.Columns.Add(col4);
                dtUserAccess.Columns.Add(col5);
                dtUserAccess.Columns.Add(col6);
                dtUserAccess.Columns.Add(col7);

                objApp = new BPWEBAccessControl.clsSecurityControl();

                if (UserCode.Trim() != "")
                {
                    if (UserCode.Trim().ToUpper() == "SEARCH")
                    {
                        searchKey = this.ddlSearchKey.SelectedValue.ToString().Trim();
                        searchKey += " like '%" + this.tbSearchValue.Text.Trim() + "%'";
                        iPrimKey++;
                        objApp.searchModules(searchKey, out dsRef);
                        this.lblAppAccessInSearch.Text = "Search Result : " + dsRef.Tables[0].Rows.Count.ToString() + " Items Found";
                        if (dsRef.Tables[0].Rows.Count > 0)
                        {
                            for (int ROWS = 0; ROWS < dsRef.Tables[0].Rows.Count; ROWS++)
                            {
                                DataRow dr = dtUserAccess.NewRow();
                                dr["PRIKEY"] = "PK-" + iPrimKey;
                                dr["ModuleID"] = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsRef.Tables[0].Rows[ROWS]["ModuleID"].ToString().Trim()));
                                dr["MODULENAME"] = dsRef.Tables[0].Rows[ROWS]["MODULENAME"].ToString().Trim();
                                dr["ACCESS"] = Convert.ToBoolean("FALSE");
                                dr["EDIT"] = Convert.ToBoolean("FALSE");
                                dr["DELETE"] = Convert.ToBoolean("FALSE");
                                dr["RATE"] = Convert.ToInt32("0");
                                dtUserAccess.Rows.Add(dr);
                                iPrimKey++;
                            }
                            this.btnRefresh.Enabled = true;
                        }
                        else
                        {
                            this.btnRefresh.Enabled = false;
                        }
                    }
                    else
                    {
                        objApp.GetUserAccManagerInfo(UserCode.Trim(), out dsLocal);
                        iPrimKey++;

                        foreach (GridViewRow gr in this.MyDataGrid.Rows)
                        {
                            moduleName = gr.Cells[2].Text.Trim();
                            moduleId = Convert.ToDouble(bplib.clsWebLib.GetNumData(gr.Cells[1].Text.Trim()));
                            DataRow dr = dtUserAccess.NewRow();
                            dr["PRIKEY"] = "PK-" + iPrimKey;
                            dr["ModuleID"] = moduleId;
                            dr["MODULENAME"] = moduleName.Trim();

                            dsLocal.Tables[0].DefaultView.RowFilter = "USERID='" + UserCode.Trim() + "' and MODULENAME='" + moduleName.Trim() + "'";
                            if (dsLocal.Tables[0].DefaultView.Count > 0)
                            {
                                dr["ACCESS"] = Convert.ToBoolean("TRUE");
                                iRate += 25;
                            }
                            else
                            {
                                dr["ACCESS"] = Convert.ToBoolean("FALSE");
                                iRate += 0;
                            }
                            dsLocal.Tables[0].DefaultView.RowFilter = "USERID='" + UserCode.Trim() + "' and MODULENAME='" + moduleName.Trim() + ".EDIT'";
                            if (dsLocal.Tables[0].DefaultView.Count > 0)
                            {
                                dr["EDIT"] = Convert.ToBoolean("TRUE");
                                iRate += 25;
                            }
                            else
                            {
                                dr["EDIT"] = Convert.ToBoolean("FALSE");
                                iRate += 0;
                            }
                            dsLocal.Tables[0].DefaultView.RowFilter = "USERID='" + UserCode.Trim() + "' and MODULENAME='" + moduleName.Trim() + ".DELETE'";
                            if (dsLocal.Tables[0].DefaultView.Count > 0)
                            {
                                dr["DELETE"] = Convert.ToBoolean("TRUE");
                                iRate += 25;
                            }
                            else
                            {
                                dr["DELETE"] = Convert.ToBoolean("FALSE");
                                iRate += 0;
                            }
                            dr["RATE"] = iRate;
                            dtUserAccess.Rows.Add(dr);
                            iRate = 0;
                            iPrimKey++;
                        }
                    }
                }
                else
                {
                    iPrimKey++;
                    objApp.searchModules(out dsRef);

                    if (dsRef.Tables[0].Rows.Count > 0)
                    {
                        for (int ROWS = 0; ROWS < dsRef.Tables[0].Rows.Count; ROWS++)
                        {
                            DataRow dr = dtUserAccess.NewRow();
                            dr["PRIKEY"] = "PK-" + iPrimKey;
                            dr["ModuleID"] = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsRef.Tables[0].Rows[ROWS]["ModuleID"].ToString().Trim()));
                            dr["MODULENAME"] = dsRef.Tables[0].Rows[ROWS]["MODULENAME"].ToString().Trim();
                            dr["ACCESS"] = Convert.ToBoolean("FALSE");
                            dr["EDIT"] = Convert.ToBoolean("FALSE");
                            dr["DELETE"] = Convert.ToBoolean("FALSE");
                            dr["RATE"] = Convert.ToInt32("0");

                            dtUserAccess.Rows.Add(dr);
                            iPrimKey++;
                        }
                    }
                }

                return dtUserAccess;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objApp = null;
                dsRef = null;
                dsLocal = null;
            }
        }// End function
        private void SaveData()
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            //System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            string userId = "";

            BPWEBAccessControl.clsSecurityControl objApp = null;

            bool DATA_OK = false;
            try
            {
                if ((int)Session["VERIFICATION_STATE"] != 0)
                {
                    objApp = new BPWEBAccessControl.clsSecurityControl();
                    userId = this.ddlUserID.SelectedValue.ToString().Trim();
                    if (DATA_OK == false)
                    {
                        if (userId.Trim() == "" || userId.Trim().Length > 20)
                        {
                            System.Exception ex = new Exception("Define the User ID...........(Max length allowed 20)");
                            throw (ex);
                        }
                        if (userId.ToUpper().Trim() == "SELECT USER")
                        {
                            System.Exception ex = new Exception("This is not a valid User ID..........Please Select a Valid User Id");
                            throw (ex);
                        }
                        if (this.MyDataGrid.Rows.Count <= 0)
                        {
                            System.Exception ex = new Exception("There is no Module ..........");
                            throw (ex);
                        }

                        DATA_OK = true;
                    }
                    if (DATA_OK == true)
                    {
                        //objApp.DeleteUserAccData(userId);
                        objApp.GetUserAccManagerInfo(userId, out dsLocal);
                        dtLocal = dsLocal.Tables[0];
                        dvLocal = new DataView();
                        dvLocal.Table = dtLocal;
                        dvLocal.RowFilter = "USERID='" + userId.Trim() + "' and MODULENAME='XXXXXXX'";

                        if (dvLocal.Count == 0)
                        { // Add new block
                            UpdateTheDataRow("ADDNEW", ref dtLocal);
                        }
                        //else
                        //{//edit block
                        //    drLocal = dvLocal[0].Row;
                        //    drLocal.BeginEdit();
                        //    UpdateTheDataRow("EDIT", ref drLocal);
                        //    drLocal.EndEdit();
                        //}
                        dvLocal.RowFilter = null;
                        objApp.SaveData(ref dsLocal);

                        ShowLog("Data saved sucessfully...");
                        Cancel();
                        displayMsgs("Data saved Successfully......!!!!", "Ok", "Save");
                        //System.Web.UI.Page this_page_ref = this;
                        //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Data update successful !!!", "bappskey1");
                        //this_page_ref = null;                        

                    }
                }
                else
                {
                    //System.Web.UI.Page this_page_ref = this;
                    //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Save is NOT possible now. Please press <Add new> / <Edit> as required and create / retrieve the data in the form, then press <Save / Create> button.", "bappskey1");
                    //this_page_ref = null;
                    displayMsgs("Save is NOT possible now. Please press [Refresh] and Select an User, then press [Save] button.", "Error", "Save");
                }
            }
            catch (System.Exception ex)
            {
                //System.Web.UI.Page this_page_ref = this;
                //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Error !! occurred in data saving process. Please see the log below for details.", "bappskey1");
                //this_page_ref = null;
                displayMsgs("Error !! occurred in data saving process. Please see the log below for details.", "Error", "Save");
                ShowLog("Error:  " + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
                //drLocal = null;
                dvLocal = null;
                dtLocal = null;
                dsLocal = null;

            }

        }//end of function
        private void UpdateTheDataRow(string OPN_FLAG, ref System.Data.DataTable dtLocal)
        {
            string moduleAcc1 = "";
            string moduleAcc2 = "";
            string moduleAcc3 = "";
            string module = "";
            string userId = "";
            BPWEBAccessControl.clsSecurityControl objApp = null;
            try
            {
                objApp = new BPWEBAccessControl.clsSecurityControl();
                if (OPN_FLAG == "ADDNEW")
                {
                    foreach (GridViewRow gr in this.MyDataGrid.Rows)
                    {
                        module = gr.Cells[2].Text.Trim().ToString();
                        userId = bplib.clsWebLib.RetValidLen(this.ddlUserID.SelectedValue.ToString().Trim(), 20).ToString().Trim();

                        objApp.DeleteUserModule(userId, module);
                        CheckBox cb1 = (CheckBox)(gr.FindControl("cbAccStat"));
                        if (cb1.Checked)
                        {
                            moduleAcc1 = module.Trim();
                        }
                        else
                        {
                            moduleAcc1 = "";
                        }

                        CheckBox cb2 = (CheckBox)(gr.FindControl("cbEditStat"));
                        if (cb2.Checked)
                        {
                            moduleAcc2 = module.Trim() + ".EDIT";
                        }
                        else
                        {
                            moduleAcc2 = "";
                        }

                        CheckBox cb3 = (CheckBox)(gr.FindControl("cbDelStat"));
                        if (cb3.Checked)
                        {
                            moduleAcc3 = module.Trim() + ".DELETE";
                        }
                        else
                        {
                            moduleAcc3 = "";
                        }

                        if (moduleAcc1.Trim() != "")
                        {
                            System.Data.DataRow drLocal = dtLocal.NewRow();
                            drLocal["USERID"] = userId;
                            drLocal["ModuleName"] = moduleAcc1.Trim();
                            dtLocal.Rows.Add(drLocal);
                        }
                        if (moduleAcc2.Trim() != "")
                        {
                            System.Data.DataRow drLocal = dtLocal.NewRow();
                            drLocal["USERID"] = userId;
                            drLocal["ModuleName"] = moduleAcc2.Trim();
                            dtLocal.Rows.Add(drLocal);
                        }
                        if (moduleAcc3.Trim() != "")
                        {
                            System.Data.DataRow drLocal = dtLocal.NewRow();
                            drLocal["USERID"] = userId;
                            drLocal["ModuleName"] = moduleAcc3.Trim();
                            dtLocal.Rows.Add(drLocal);
                        }

                    }
                }

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objApp = null;
            }
        } // end function
        private void DeleteData()
        {
            BPWEBAccessControl.clsSecurityControl objApp = null;
            bool DATA_OK = false;
            try
            {
                if (DATA_OK == false)
                {
                    if (this.ddlUserID.SelectedValue.ToString().Trim() == "" || this.ddlUserID.SelectedValue.ToString().Trim().Length > 20)
                    {
                        System.Exception ex = new Exception("Define the UserID..........");
                        throw (ex);
                    }
                    if (this.ddlUserID.SelectedValue.ToString().Trim() == "SELECT USER")
                    {
                        System.Exception ex = new Exception("Define the UserID..........");
                        throw (ex);
                    }
                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    if ((int)Session["VERIFICATION_STATE"] == 1)
                    {
                        objApp = new BPWEBAccessControl.clsSecurityControl();
                        objApp.DeleteUserAccData(this.ddlUserID.SelectedValue.ToString().Trim());


                        ShowLog("Data deleted sucessfully...");
                        Cancel();
                        displayMsgs("Data delete successful !!!", "Ok", "DEL");
                        //System.Web.UI.Page this_page_ref = this;
                        //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Data delete successful !!!", "bappskey1");
                        //this_page_ref = null;
                    }
                    else
                    {
                        //System.Web.UI.Page this_page_ref = this;
                        //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Delete is NOT possible now. Please press <Edit> and retrieve the data in the form, then press <Delete> button.", "bappskey1");
                        //this_page_ref = null;
                        displayMsgs("Delete is NOT possible now. Please Select an User, then press <Delete> button.", "Ok", "DEL");
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Web.UI.Page this_page_ref = this;
                bplib.clsWebLib.BappsAlert(ref this_page_ref, "Error !! occurred in data deleting process. " + ex.Message.ToString() + " Please see the log below for details.", "bappskey1");
                this_page_ref = null;
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
            }
        }//end function
        private void Cancel()
        {
            HideLog();
            ClearTheForm();
            Session["VERIFICATION_STATE"] = 0;
            this.btnSave.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnRefresh.Enabled = false;
            this.cbModule.Checked = false;
            this.panGrid.Visible = false;
        }//end of function
        private void SetDefault()
        {
            try
            {

                this.ddlUserID.SelectedValue = "SELECT USER";
                this.lblAppAccessInSearch.Text = "Search";
                //if ((string)Session["USER_GROUP"] == "SUPR")
                //{
                this.btnDelete.Visible = true;
                //}
                //else
                //{
                //    this.btnDelete.Visible = false;
                //}
            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        }// end function
        #endregion

        #region App User Info (Creating Users) [VW03]
        private void addNewUsers()
        {
            try
            {
                Session["VERIFICATION_STATE"] = 2;
                this.tbUserId.Text = "";
                this.tbPassword.Text = "";
                //this.ddlGroup.
                //this.ddlLocation
                this.btnEditUser.Visible = false;
                this.btnDelUser.Visible = false;
                this.btnSaveUser.Text = "Create";
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        }//eof
        private void SaveUserData()
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            BPWEBAccessControl.clsSecurityControl objApp = null;

            bool DATA_OK = false;
            string strPasswordStatus = "";
            string strPWD = "";
            try
            {
                if ((int)Session["VERIFICATION_STATE"] != 0)
                {
                    objApp = new BPWEBAccessControl.clsSecurityControl();
                    if (DATA_OK == false)
                    {
                        if (this.tbUserId.Text.Trim() == "" || this.tbUserId.Text.Trim().Length > 20)
                        {
                            System.Exception ex = new Exception("Define the User ID...(Max character allowed : 20)");
                            throw (ex);
                        }
                        //if (this.tbPassword.Text.Trim() == "" || this.tbUserId.Text.Trim().Length < 8)
                        //{
                        //    System.Exception ex = new Exception("Define the password... (Min character allowed : 8)");
                        //    throw (ex);
                        //}
                        if (this.tbPassword.Text.Trim() == "")
                        {
                            System.Exception ex = new Exception("Define the password... ");
                            throw (ex);
                        }
                        else
                        {
                            strPWD = String.Concat(this.tbPassword.Text.Trim().Where(c => char.IsWhiteSpace(c) == false));
                            if (strPWD.Length > 10)
                            {
                                System.Exception ex = new Exception("Define the password... (Max character allowed : 10)");
                                throw (ex);
                            }
                        }
                        if (!objApp.checkPassword(strPWD.Trim(),out strPasswordStatus))
                        {
                            throw new Exception(strPasswordStatus);
                        }
                        if (string.IsNullOrWhiteSpace(this.tbUserName.Text.Trim())==true || this.tbUserId.Text.Trim().Length > 500)
                        {
                            System.Exception ex = new Exception("Define the User Name...(Max character allowed : 500)");
                            throw (ex);
                        }
                        if (string.IsNullOrWhiteSpace(this.tbUserEmailID.Text.Trim()) == true || this.tbUserEmailID.Text.Trim().Length > 150)
                        {
                            System.Exception ex = new Exception("Define the e-Mail ID...(Max character allowed : 150)");
                            throw (ex);
                        }
                        if (!objApp.CheckEmailPatern(this.tbUserEmailID.Text.Trim()))
                        {
                            throw new Exception("Define e-Mail ID......(Error(10087) in mail Pattern)");
                        }

                        DATA_OK = true;
                    }
                    if (DATA_OK == true)
                    {
                        objApp.GetUserDetails(this.tbUserId.Text.Trim(), out dsLocal);
                        dtLocal = dsLocal.Tables[0];
                        dvLocal = new DataView();
                        dvLocal.Table = dtLocal;
                        dvLocal.RowFilter = "USERID='" + this.tbUserId.Text.Trim() + "'";

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

                        ShowLog("Data saved sucessfully...");
                        CancelUser();
                        displayMsgs("Data saved Successfully......!!!!", "Ok", "Save");
                    }
                }
                else
                {
                    displayMsgs("Save is NOT possible now. Please press [Add new] / [Edit] as required and create / retrieve the data in the form, then press [Save / Create] button.", "Error", "Save");
                }
            }
            catch (System.Exception ex)
            {
                displayMsgs("Error !! occurred in data saving process. Please see the log below for details.", "Error", "Save");
                ShowLog("Error:  " + ex.Message.ToString());
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
            string strPWD = "";
            try
            {

                if (OPN_FLAG == "ADDNEW")
                {
                    drLocal["UserID"] = bplib.clsWebLib.RetValidLen(this.tbUserId.Text.Trim(), 20);
                    drLocal["UserCreatedOn"] = bplib.clsWebLib.DateData_AppToDB(System.DateTime.Today.ToString(), bplib.clsWebLib.DB_DATE_FORMAT);
                }
                strPWD = String.Concat(this.tbPassword.Text.Trim().Where(c => char.IsWhiteSpace(c) == false));
                drLocal["Password"] = bplib.clsWebLib.RetValidLen(BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(strPWD.Trim(), "bapps"), 100);
                drLocal["UserGroup"] = bplib.clsWebLib.RetValidLen(this.ddlUserGroup.SelectedValue.ToString().Trim(), 250);
                drLocal["UserLocation"] = bplib.clsWebLib.RetValidLen(this.ddlUserLocation.SelectedValue.ToString().Trim(), 250);
                drLocal["IsAdmin"] = bplib.clsWebLib.RetValidLen(this.ddlIsAdmin.SelectedValue.ToString().Trim(), 100);
                drLocal["PWD_RESET_REQ"] = bplib.clsWebLib.RetValidLen(this.ddlPWD_RESET_REQ.SelectedValue.ToString().Trim(), 100);

                drLocal["UserTitle"] = bplib.clsWebLib.RetValidLen(this.ddlUserTitle.SelectedValue.ToString().Trim(), 5);
                drLocal["UserName"] = bplib.clsWebLib.RetValidLen(this.tbUserName.Text.Trim(), 500);
                drLocal["UserDepartment"] = bplib.clsWebLib.RetValidLen(this.ddlUserDepartment.SelectedValue.ToString().Trim(), 250);
                drLocal["UserDesignation"] = bplib.clsWebLib.RetValidLen(this.ddlUserDesignation.SelectedValue.ToString().Trim(), 250);
                drLocal["UserEmailID"] = bplib.clsWebLib.RetValidLen(this.tbUserEmailID.Text.Trim(), 150);
                drLocal["UserCountry"] = bplib.clsWebLib.RetValidLen(this.ddlUserCountry.SelectedValue.ToString().Trim(), 250);
                drLocal["LastUpdate"] = System.DateTime.Today;
                drLocal["UpdateBy"] = bplib.clsWebLib.RetValidLen(((string)Session["USER"]), 20);
                drLocal["UpdateOn"] = System.DateTime.Now.ToShortDateString();
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
        private void EditExistUsers()
        {
            try
            {
                string strFromDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                string strToDate = System.DateTime.Today.ToString("dd-MMM-yyyy");
                this.lblViewName.Text = this.mvwDataVw.GetActiveView().ID.ToString();
                LoadData(true, "UDATA", strFromDate, strToDate, "");
                this.tbValue.Text = "";
                this.lblSearchTitle.Text = "Search : User ID";
                this.btnCancelSearch.Visible = true; //set as false in Cancel() function; if the search screen is the first screen
                this.mvwDataVw.SetActiveView(this.vw00);
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
            }
        }//eof
        private void LoadUserDetails()
        {

            System.Data.DataSet dsLocal = null;
            BPWEBAccessControl.clsSecurityControl objApp = null;
            string strPWD = "";
            try
            {

                if (this.tbUserId.Text.Trim() == "" || this.tbUserId.Text.Trim().Length > 20)
                {
                    return;
                }
                objApp = new BPWEBAccessControl.clsSecurityControl();
                objApp.GetUserDetails(this.tbUserId.Text.ToString().Trim(), out dsLocal);
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    this.tbUserId.Text = dsLocal.Tables[0].Rows[0]["UserID"].ToString();
                    strPWD = dsLocal.Tables[0].Rows[0]["Password"].ToString();
                    if (this.tbUserId.Text.Trim() != "")
                    {
                        strPWD = BPEnCodeDecodeLib.clsEnCodeDeCode.DecodeHI(strPWD, "bapps");
                    }
                    this.tbPassword.Text = strPWD;
                    this.ddlUserGroup.SelectedValue = dsLocal.Tables[0].Rows[0]["UserGroup"].ToString();
                    this.ddlUserLocation.SelectedValue = dsLocal.Tables[0].Rows[0]["UserLocation"].ToString();
                    this.ddlIsAdmin.SelectedValue = dsLocal.Tables[0].Rows[0]["IsAdmin"].ToString();
                    this.ddlPWD_RESET_REQ.SelectedValue = dsLocal.Tables[0].Rows[0]["PWD_RESET_REQ"].ToString();
                    
                    
                    this.ddlUserTitle.SelectedValue = dsLocal.Tables[0].Rows[0]["UserTitle"].ToString();
                    this.tbUserName.Text = dsLocal.Tables[0].Rows[0]["UserName"].ToString();
                    this.ddlUserDepartment.SelectedValue = dsLocal.Tables[0].Rows[0]["UserDepartment"].ToString();
                    this.ddlUserDesignation.SelectedValue = dsLocal.Tables[0].Rows[0]["UserDesignation"].ToString();
                    this.tbUserEmailID.Text = dsLocal.Tables[0].Rows[0]["UserEmailID"].ToString();
                    this.ddlUserCountry.SelectedValue = dsLocal.Tables[0].Rows[0]["UserCountry"].ToString();

                    Session["VERIFICATION_STATE"] = 1;
                    this.btnSaveUser.Text = "Save";
                    this.btnAddNewUser.Visible = false;
                }
            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                objApp = null;
                dsLocal = null;
            }
        }//eof
        private void DeleteUserData()
        {
            BPWEBAccessControl.clsSecurityControl objApp = null;
            bool DATA_OK = false;
            try
            {
                if (DATA_OK == false)
                {
                    if (this.tbUserId.Text.Trim() == "" || this.tbUserId.Text.Trim().Length > 20)
                    {
                        System.Exception ex = new Exception("Define the EntryID...");
                        throw (ex);
                    }
                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    if ((int)Session["VERIFICATION_STATE"] == 1)
                    {
                        objApp = new BPWEBAccessControl.clsSecurityControl();
                        objApp.DeleteSiteAccData(this.tbUserId.Text.Trim());
                        objApp.DeleteUserAccData(this.tbUserId.Text.Trim());
                        objApp.DeleteUserData(this.tbUserId.Text.Trim());


                        ShowLog("Data deleted sucessfully...");
                        CancelUser();
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
                displayMsgs("Error : " + ex.ToString(), "Error", "DEL");
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
            }
        }//eof
        private void SetDefaultUser()
        {
            try
            {

                this.tbUserId.Text = "";
                this.tbPassword.Text = "";
                this.ddlUserLocation.SelectedValue = "HEADOFFICE";
                this.ddlUserGroup.SelectedValue = "USER";
                this.ddlIsAdmin.SelectedValue = "NO";
                this.ddlPWD_RESET_REQ.SelectedValue = "YES";


                this.ddlUserTitle.SelectedValue = "MR";
                this.tbUserName.Text = "";
                this.ddlUserDepartment.SelectedIndex = -1;
                this.ddlUserDesignation.SelectedIndex = -1;
                this.tbUserEmailID.Text = "";
                this.ddlUserCountry.SelectedIndex = -1;

                this.lblViewState.Text = "";
                this.lblDlgState.Text = "";
                this.lblViewName.Text = "";

                if ((string)Session["USER_GROUP"] == "SUPR")
                {
                    this.btnDelUser.Visible = true;
                }
                else
                {
                    this.btnDelUser.Visible = false;
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
        }//eof
        private void CancelUser()
        {
            HideLog();
            SetDefaultUser();
            setDefaultSearch();
            Session["VERIFICATION_STATE"] = 0;
            this.btnAddNewUser.Visible = true;
            this.btnEditUser.Visible = true;
            this.btnSaveUser.Text = "Save";
            this.btnDelUser.Enabled = true;
        }//eof
        #endregion

        #region Access Copy Utility [VW04]
        private void loadAllUsers()
        {
            System.Data.DataSet dsLocal = null;
            BPWEBAccessControl.clsSecurityControl objApp = null;
            try
            {
                //Source Load Users
                objApp = new BPWEBAccessControl.clsSecurityControl();
                objApp.GetUserAccManagerInfo(out dsLocal);
                this.ddlSourceId.DataSource = dsLocal.Tables[0].DefaultView;
                this.ddlSourceId.DataTextField = "USERID";
                this.ddlSourceId.DataValueField = "USERID";
                this.ddlSourceId.DataBind();
                this.ddlSourceId.Items.Add("SELECT USER");
                this.ddlSourceId.SelectedValue = "SELECT USER";

                //Target Load Users
                objApp = new BPWEBAccessControl.clsSecurityControl();
                objApp.GetUserAccManagerInfo(out dsLocal);
                this.ddlDestUserId.DataSource = dsLocal.Tables[0].DefaultView;
                this.ddlDestUserId.DataTextField = "USERID";
                this.ddlDestUserId.DataValueField = "USERID";
                this.ddlDestUserId.DataBind();
                this.ddlDestUserId.Items.Add("SELECT USER");
                this.ddlDestUserId.SelectedValue = "SELECT USER";
            }
            catch (Exception ex)
            {
                ShowLog("Error : " + ex.ToString());
            }
            finally
            {
                dsLocal = null;
                objApp = null;
            }
        }//eof
        private void CopyAccess()
        {
            BPWEBAccessControl.clsSecurityControl objSys = null;
            bool dataOk = false;
            string strMsgs = "";
            try
            {
                if (dataOk == false)
                {
                    if (this.ddlDestUserId.SelectedValue.ToString().Trim() == "" || this.ddlDestUserId.SelectedValue.ToString().Trim() == "SELECT USER")
                    {
                        System.Exception ex = new Exception("Please select Target User to Copy the Access ............... ");
                        throw (ex);
                    }
                    if (this.ddlSourceId.SelectedValue.ToString().Trim() == "" || this.ddlSourceId.SelectedValue.ToString().Trim() == "SELECT USER")
                    {
                        System.Exception ex = new Exception("Please select Source User to Copy the Access ............... ");
                        throw (ex);
                    }
                    dataOk = true;
                }

                if (dataOk == true)
                {
                    objSys = new BPWEBAccessControl.clsSecurityControl();
                    strMsgs = objSys.CopySystemAccess(this.ddlSourceId.SelectedValue.ToString().Trim(), this.ddlDestUserId.SelectedValue.ToString().Trim());
                    this.tbLog.Text = strMsgs;
                }
                else
                {
                    ShowLog("Problem in copy technique");
                }

            }
            catch (System.Exception ex)
            {
                displayMsgs("Error !! occurred in data copy process. Please see the log below for details.", "Error", "Save");
                ShowLog("Error:  " + ex.Message.ToString());
            }
            finally
            {
                //
            }
        }// end user
        #endregion

        #region System Admin Info [VW05]
        private void SaveData_Sysuser()
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;
            bool DATA_OK = false;
            BPWEBAccessControl.clsSecurityControl objApp = null;

            try
            {
                if ((int)Session["VERIFICATION_STATE"] != 0)
                {
                    objApp = new BPWEBAccessControl.clsSecurityControl();
                    if (DATA_OK == false)
                    {
                        if (this.tbSysUserId.Text.Trim() == "" || this.tbSysUserId.Text.Trim().Length > 20)
                        {
                            System.Exception ex = new Exception("Define the USER ID...(Max length allowed 20)");
                            throw (ex);
                        }
                        if (this.tbSysPasswd.Text.Trim() == "" || this.tbSysPasswd.Text.Trim().Length > 20)
                        {
                            System.Exception ex = new Exception("Define the password... (Max length allowed 20)");
                            throw (ex);
                        }
                        DATA_OK = true;
                    }
                    if (DATA_OK == true)
                    {
                        objApp.GetUserList(this.tbSysUserId.Text.Trim(), out dsLocal);
                        dtLocal = dsLocal.Tables[0];
                        dvLocal = new DataView();
                        dvLocal.Table = dtLocal;
                        dvLocal.RowFilter = "UserID='" + this.tbSysUserId.Text.Trim() + "'";

                        if (dvLocal.Count == 0)
                        { // Add new block
                            drLocal = dtLocal.NewRow();
                            drLocal["UserID"] = bplib.clsWebLib.RetValidLen(this.tbSysUserId.Text.Trim().ToUpper(), 20);
                            drLocal["PWD"] = BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(this.tbSysPasswd.Text.Trim(), "bapps");
                            dtLocal.Rows.Add(drLocal);
                        }
                        else
                        {//edit block
                            drLocal = dvLocal[0].Row;
                            drLocal.BeginEdit();
                            drLocal["PWD"] = BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(this.tbSysPasswd.Text.Trim(), "bapps");
                            drLocal.EndEdit();
                        }
                        objApp.SaveData(ref dsLocal);
                        dvLocal.RowFilter = null;
                        ShowLog("Data saved sucessfully...");
                        CancelSysUser();
                        SysUserList();
                        displayMsgs("Data saved Successfully......!!!!", "Ok", "Save");
                    }
                }
                else
                {
                    displayMsgs("Save is NOT possible now. Please press [Add new] / [Edit] as required and create / retrieve the data in the form, then press [Save / Create] button.", "Error", "Save");
                }
            }
            catch (System.Exception ex)
            {
                displayMsgs("Error !! occurred in data saving process. Please see the log below for details.", "Error", "Save");
                ShowLog("Error:  " + ex.Message.ToString());
            }
            finally
            {
                drLocal = null;
                dvLocal = null;
                dtLocal = null;
                dsLocal = null;
                objApp = null;
            }
        }//end of function
        private void loadSelectedUser()
        {
            System.Data.DataSet dsLocal = null;
            BPWEBAccessControl.clsSecurityControl objApp = null;
            HideLog();
            try
            {
                this.tbSysPasswd.Text = "";
                this.tbSysUserId.Text = "";
                if (this.lbUserLists.Items.Count > 0)
                {
                    if (this.lbUserLists.SelectedIndex >= 0)
                    {
                        objApp = new BPWEBAccessControl.clsSecurityControl();
                        objApp.GetUserList(this.lbUserLists.SelectedItem.Text.Trim(), out dsLocal);
                        this.tbSysUserId.Text = this.lbUserLists.SelectedItem.Text.Trim();
                        this.tbSysPasswd.Text = BPEnCodeDecodeLib.clsEnCodeDeCode.DecodeHI(dsLocal.Tables[0].Rows[0]["PWD"].ToString().Trim(), "bapps");
                        this.linbtnSysSave.Text = "Save";
                        this.linbtnSysSave.Enabled = true;
                        this.linbtnSysDel.Enabled = true;
                        Session["VERIFICATION_STATE"] = 1;
                        this.tbSysUserId.ReadOnly = true;
                    }
                    else
                    {
                        ShowLog("Please Select a User..........");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
            }
        }
        private void DeleteData_Sysuser()
        {
            BPWEBAccessControl.clsSecurityControl objApp = null;
            bool DATA_OK = false;
            try
            {
                if (DATA_OK == false)
                {
                    if (this.tbSysUserId.Text.Trim() == "" || this.tbSysUserId.Text.Trim().Length > 20)
                    {
                        System.Exception ex = new Exception("Define the USER ID...(Max length allowed 20)");
                        throw (ex);
                    }
                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    if ((int)Session["VERIFICATION_STATE"] == 1)
                    {
                        objApp = new BPWEBAccessControl.clsSecurityControl();
                        objApp.DeleteSysUserData(this.tbSysUserId.Text.Trim());

                        ShowLog("Data deleted sucessfully...");
                        CancelSysUser();
                        SysUserList();
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
                displayMsgs("Error : " + ex.ToString(), "Error", "DEL");
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
            }
        }//end function
        private void CancelSysUser()
        {
            try
            {
                HideLog();
                this.linbtnSysSave.Enabled = false;
                this.tbSysUserId.ReadOnly = false;
                this.linbtnSysDel.Enabled = false;
                SetSysDefault();
                Session["VERIFICATION_STATE"] = 0;
                this.linbtnSysSave.Text = "Save";

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                // clean variable
            }
        }//end of function
        private void SetSysDefault()
        {
            try
            {
                this.tbSysUserId.Text = "";
                this.tbSysPasswd.Text = "";
            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        }// end function
        private void SysUserList()
        {
            System.Data.DataSet dsLocal = null;
            BPWEBAccessControl.clsSecurityControl objApp = null;
            try
            {
                objApp = new BPWEBAccessControl.clsSecurityControl();
                objApp.GetUserList("", out dsLocal);
                this.lbUserLists.Items.Clear();
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    this.lbUserLists.DataSource = dsLocal.Tables[0].DefaultView;
                    this.lbUserLists.DataTextField = "USERID";
                    this.lbUserLists.DataValueField = "USERID";
                    this.lbUserLists.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsLocal = null;
                objApp = null;
            }
        }//eof
        #endregion

        #region Site Access [VW06]
        private void loadSiteLists()
        {

            try
            {
                //CancelSite();
                this.gvSite.DataSource = UserSiteManagerInfo();
                this.gvSite.DataBind();
                this.panSite.Visible = true;
            }
            catch (Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                //
            }
        }//eof
        private void getSiteUserDetails()
        {
            string userCode = "";
            BPWEBAccessControl.clsSecurityControl objApp = null;
            System.Data.DataSet dsLocal = null;
            try
            {
                objApp = new BPWEBAccessControl.clsSecurityControl();
                objApp.GetUserAccManagerInfo(out dsLocal);
                this.ddlSiteUsers.Items.Clear();
                this.ddlSiteUsers.DataSource = dsLocal.Tables[0].DefaultView;
                this.ddlSiteUsers.DataTextField = "USERID";
                this.ddlSiteUsers.DataValueField = "USERID";
                this.ddlSiteUsers.DataBind();
                this.ddlSiteUsers.Items.Add("SELECT USER");
                this.ddlSiteUsers.SelectedValue = "SELECT USER";
                this.btnSaveSite.Enabled = true;
                this.btnDelSite.Enabled = true;

            }
            catch (Exception ex)
            {
            }
            finally
            {
                dsLocal = null;
            }
        }//eof
        private DataTable UserSiteManagerInfo()
        {
            System.Data.DataSet dsRef = null;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtSiteAccess = null;
            BPWEBAccessControl.clsSecurityControl objApp = null;

            string strSiteId = "";
            string strStrGroup = "";
            int iPrimKey = 0;
            int iRate = 0;
            string UserCode = "";

            DataColumn col1;
            DataColumn col2;
            DataColumn col3;
            DataColumn col4;
            DataColumn col5;

            try
            {


                dtSiteAccess = new System.Data.DataTable();
                col1 = new DataColumn("PRIKEY");
                col2 = new DataColumn("SITEID");
                col3 = new DataColumn("SITE_GROUP");
                col4 = new DataColumn("ACCESS");
                col5 = new DataColumn("RATE");



                col1.DataType = System.Type.GetType("System.String");
                col2.DataType = System.Type.GetType("System.String");
                col3.DataType = System.Type.GetType("System.String");
                col4.DataType = System.Type.GetType("System.Boolean");
                col5.DataType = System.Type.GetType("System.Int32");


                dtSiteAccess.Columns.Add(col1);
                dtSiteAccess.Columns.Add(col2);
                dtSiteAccess.Columns.Add(col3);
                dtSiteAccess.Columns.Add(col4);
                dtSiteAccess.Columns.Add(col5);

                objApp = new BPWEBAccessControl.clsSecurityControl();
                UserCode = this.ddlSiteUsers.SelectedValue.ToString().Trim();
                if (UserCode.Trim() != "SELECT USER" && string.IsNullOrEmpty(UserCode.Trim()) == false)
                {
                    objApp.GetUserSitesInfo(UserCode.Trim(), out dsLocal);
                    iPrimKey++;

                    foreach (GridViewRow gr in this.gvSite.Rows)
                    {
                        strSiteId = gr.Cells[2].Text.Trim();
                        strStrGroup = gr.Cells[1].Text.Trim();
                        DataRow dr = dtSiteAccess.NewRow();
                        dr["PRIKEY"] = "PK-" + iPrimKey;
                        dr["SITEID"] = strSiteId;
                        dr["SITE_GROUP"] = strStrGroup.Trim();

                        dsLocal.Tables[0].DefaultView.RowFilter = "USERID='" + UserCode.Trim() + "' and SITEID='" + strSiteId.Trim() + "'";
                        if (dsLocal.Tables[0].DefaultView.Count > 0)
                        {
                            dr["ACCESS"] = Convert.ToBoolean("TRUE");
                            iRate += 75;
                        }
                        else
                        {
                            dr["ACCESS"] = Convert.ToBoolean("FALSE");
                            iRate += 0;
                        }
                        dr["RATE"] = iRate;
                        dtSiteAccess.Rows.Add(dr);
                        iRate = 0;
                        iPrimKey++;
                    }
                }
                else
                {
                    iPrimKey++;
                    objApp.searchSites(out dsRef);

                    if (dsRef.Tables[0].Rows.Count > 0)
                    {
                        for (int ROWS = 0; ROWS < dsRef.Tables[0].Rows.Count; ROWS++)
                        {
                            DataRow dr = dtSiteAccess.NewRow();
                            dr["PRIKEY"] = "PK-" + iPrimKey;
                            dr["SITEID"] = dsRef.Tables[0].Rows[ROWS]["SITEID"].ToString().Trim();
                            dr["SITE_GROUP"] = dsRef.Tables[0].Rows[ROWS]["SITE_GROUP"].ToString().Trim();
                            dr["ACCESS"] = Convert.ToBoolean("FALSE");
                            dr["RATE"] = Convert.ToInt32("0");

                            dtSiteAccess.Rows.Add(dr);
                            iPrimKey++;
                        }
                    }
                }

                return dtSiteAccess;

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objApp = null;
                dsRef = null;
                dsLocal = null;
            }
        }//eof
        private void SaveSiteData()
        {
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            //System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            string userId = "";

            BPWEBAccessControl.clsSecurityControl objApp = null;

            bool DATA_OK = false;
            try
            {
                if ((int)Session["VERIFICATION_STATE"] != 0)
                {
                    objApp = new BPWEBAccessControl.clsSecurityControl();
                    userId = this.ddlSiteUsers.SelectedValue.ToString().Trim();
                    if (DATA_OK == false)
                    {
                        if (userId.Trim() == "" || userId.Trim().Length > 20)
                        {
                            System.Exception ex = new Exception("Define the User ID...........(Max length allowed 20)");
                            throw (ex);
                        }
                        if (userId.ToUpper().Trim() == "SELECT USER")
                        {
                            System.Exception ex = new Exception("This is not a valid User ID..........Please Select a Valid User Id");
                            throw (ex);
                        }
                        if (this.gvSite.Rows.Count <= 0)
                        {
                            System.Exception ex = new Exception("There is no Site ..........");
                            throw (ex);
                        }

                        DATA_OK = true;
                    }
                    if (DATA_OK == true)
                    {
                        objApp.DeleteSiteModule(userId);
                        objApp.GetSitesInfo(userId, out dsLocal);
                        dtLocal = dsLocal.Tables[0];
                        dvLocal = new DataView();
                        dvLocal.Table = dtLocal;
                        dvLocal.RowFilter = "USERID='" + userId.Trim() + "' and SITEID='XXXXXXX'";

                        if (dvLocal.Count == 0)
                        {
                            // Add new block
                            UpdateTheSiteDataRow("ADDNEW", ref dtLocal);
                        }
                        //else
                        //{//edit block
                        //    drLocal = dvLocal[0].Row;
                        //    drLocal.BeginEdit();
                        //    UpdateTheDataRow("EDIT", ref drLocal);
                        //    drLocal.EndEdit();
                        //}
                        dvLocal.RowFilter = null;
                        objApp.SaveData(ref dsLocal);

                        ShowLog("Data saved sucessfully...");
                        CancelSite();
                        displayMsgs("Data saved Successfully......!!!!", "Ok", "Save");
                        //System.Web.UI.Page this_page_ref = this;
                        //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Data update successful !!!", "bappskey1");
                        //this_page_ref = null;                        

                    }
                }
                else
                {
                    //System.Web.UI.Page this_page_ref = this;
                    //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Save is NOT possible now. Please press <Add new> / <Edit> as required and create / retrieve the data in the form, then press <Save / Create> button.", "bappskey1");
                    //this_page_ref = null;
                    displayMsgs("Save is NOT possible now. Please press [Refresh] and Select an User, then press [Save] button.", "Error", "Save");
                }
            }
            catch (System.Exception ex)
            {
                //System.Web.UI.Page this_page_ref = this;
                //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Error !! occurred in data saving process. Please see the log below for details.", "bappskey1");
                //this_page_ref = null;
                displayMsgs("Error !! occurred in data saving process. Please see the log below for details.", "Error", "Save");
                ShowLog("Error:  " + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
                //drLocal = null;
                dvLocal = null;
                dtLocal = null;
                dsLocal = null;

            }

        }//eof
        private void UpdateTheSiteDataRow(string OPN_FLAG, ref System.Data.DataTable dtLocal)
        {
            string siteAccess = "";
            string siteId = "";
            string userId = "";
            BPWEBAccessControl.clsSecurityControl objApp = null;
            try
            {
                objApp = new BPWEBAccessControl.clsSecurityControl();
                userId = bplib.clsWebLib.RetValidLen(this.ddlSiteUsers.SelectedValue.ToString().Trim(), 20).ToString().Trim();
                if (OPN_FLAG == "ADDNEW")
                {
                    foreach (GridViewRow gr in this.gvSite.Rows)
                    {
                        siteId = gr.Cells[2].Text.Trim().ToString();
                        //objApp.DeleteSiteModule(userId, siteId);

                        CheckBox cb1 = (CheckBox)(gr.FindControl("cbAccStat"));
                        if (cb1.Checked)
                        {
                            siteAccess = siteId.Trim();
                        }
                        else
                        {
                            siteAccess = "";
                        }

                        if (string.IsNullOrEmpty(siteAccess.Trim()) == false)
                        {
                            System.Data.DataRow drLocal = dtLocal.NewRow();
                            drLocal["USERID"] = userId;
                            drLocal["SITEID"] = siteAccess.Trim();
                            dtLocal.Rows.Add(drLocal);
                        }

                    }
                }

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objApp = null;
            }
        }//eof
        private void DeleteSiteData()
        {
            BPWEBAccessControl.clsSecurityControl objApp = null;
            bool DATA_OK = false;
            try
            {
                if (DATA_OK == false)
                {
                    if (this.ddlSiteUsers.SelectedValue.ToString().Trim() == "" || this.ddlUserID.SelectedValue.ToString().Trim().Length > 20)
                    {
                        System.Exception ex = new Exception("Define the UserID..........");
                        throw (ex);
                    }
                    if (this.ddlSiteUsers.SelectedValue.ToString().Trim() == "SELECT USER")
                    {
                        System.Exception ex = new Exception("Define the UserID..........");
                        throw (ex);
                    }
                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    if ((int)Session["VERIFICATION_STATE"] == 1)
                    {
                        objApp = new BPWEBAccessControl.clsSecurityControl();
                        objApp.DeleteSiteModule(this.ddlSiteUsers.SelectedValue.ToString().Trim());


                        ShowLog("Data deleted sucessfully...");
                        CancelSite();
                        displayMsgs("Data delete successful !!!", "Ok", "DEL");
                        //System.Web.UI.Page this_page_ref = this;
                        //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Data delete successful !!!", "bappskey1");
                        //this_page_ref = null;
                    }
                    else
                    {
                        //System.Web.UI.Page this_page_ref = this;
                        //bplib.clsWebLib.BappsAlert(ref this_page_ref, "Delete is NOT possible now. Please press <Edit> and retrieve the data in the form, then press <Delete> button.", "bappskey1");
                        //this_page_ref = null;
                        displayMsgs("Delete is NOT possible now. Please Select an User, then press <Delete> button.", "Ok", "DEL");
                    }
                }
            }
            catch (System.Exception ex)
            {
                System.Web.UI.Page this_page_ref = this;
                bplib.clsWebLib.BappsAlert(ref this_page_ref, "Error !! occurred in data deleting process. " + ex.Message.ToString() + " Please see the log below for details.", "bappskey1");
                this_page_ref = null;
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                objApp = null;
            }
        }//eof
        private void CancelSite()
        {
            HideLog();
            SetSiteDefault();
            Session["VERIFICATION_STATE"] = 0;
            this.btnSaveSite.Enabled = false;
            this.btnDelSite.Enabled = false;
            this.cbSelectSites.Checked = false;
            this.panSite.Visible = false;

        }//eof
        private void SetSiteDefault()
        {
            try
            {

                this.ddlSiteUsers.SelectedValue = "SELECT USER";
                this.btnDelSite.Visible = true;
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
        #endregion


        private void tabMenuNavigation(string strNAVFLAG = "USERCREATE")
        {
            try
            {
                switch (strNAVFLAG.Trim().ToUpper())
                {

                    case "ACCESSCOPY":
                        this.mvwDataVw.SetActiveView(this.vw04);

                        this.tabAccCopy.BackColor = System.Drawing.Color.White;
                        this.tabAccCopy.ForeColor = System.Drawing.Color.Blue;
                        this.tabAccCopy.BorderStyle = System.Web.UI.WebControls.BorderStyle.Inset;
                        this.tabAccCopy.Font.Bold = true;

                        this.ddlSourceId.SelectedValue = "SELECT USER";
                        this.ddlDestUserId.SelectedValue = "SELECT USER";

                        this.tabUserCreate.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserCreate.ForeColor = System.Drawing.Color.Black;
                        this.tabUserCreate.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserCreate.Font.Bold = false;
                        this.tabUserAcc.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserAcc.ForeColor = System.Drawing.Color.Black;
                        this.tabUserAcc.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserAcc.Font.Bold = false;
                        this.tabSysInfo.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSysInfo.ForeColor = System.Drawing.Color.Black;
                        this.tabSysInfo.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSysInfo.Font.Bold = false;
                        this.tabSite.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSite.ForeColor = System.Drawing.Color.Black;
                        this.tabSite.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSite.Font.Bold = false;
                        break;
                    case "SYSINFO":
                        CancelSysUser();
                        this.mvwDataVw.SetActiveView(this.vw05);

                        this.tabSysInfo.BackColor = System.Drawing.Color.White;
                        this.tabSysInfo.ForeColor = System.Drawing.Color.Blue;
                        this.tabSysInfo.BorderStyle = System.Web.UI.WebControls.BorderStyle.Inset;
                        this.tabSysInfo.Font.Bold = true;

                        this.tabUserCreate.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserCreate.ForeColor = System.Drawing.Color.Black;
                        this.tabUserCreate.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserCreate.Font.Bold = false;
                        this.tabUserAcc.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserAcc.ForeColor = System.Drawing.Color.Black;
                        this.tabUserAcc.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserAcc.Font.Bold = false;
                        this.tabAccCopy.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabAccCopy.ForeColor = System.Drawing.Color.Black;
                        this.tabAccCopy.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabAccCopy.Font.Bold = false;
                        this.tabSite.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSite.ForeColor = System.Drawing.Color.Black;
                        this.tabSite.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSite.Font.Bold = false;
                        break;
                    case "APPACCESS":
                        Cancel();
                        this.mvwDataVw.SetActiveView(this.vw02);

                        this.tabUserAcc.BackColor = System.Drawing.Color.White;
                        this.tabUserAcc.ForeColor = System.Drawing.Color.Blue;
                        this.tabUserAcc.BorderStyle = System.Web.UI.WebControls.BorderStyle.Inset;
                        this.tabUserAcc.Font.Bold = true;

                        this.tabUserCreate.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserCreate.ForeColor = System.Drawing.Color.Black;
                        this.tabUserCreate.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserCreate.Font.Bold = false;
                        this.tabAccCopy.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabAccCopy.ForeColor = System.Drawing.Color.Black;
                        this.tabAccCopy.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabAccCopy.Font.Bold = false;
                        this.tabSysInfo.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSysInfo.ForeColor = System.Drawing.Color.Black;
                        this.tabSysInfo.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSysInfo.Font.Bold = false;
                        this.tabSite.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSite.ForeColor = System.Drawing.Color.Black;
                        this.tabSite.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSite.Font.Bold = false;
                        break;
                    case "SITEACCESS":
                        CancelSite();
                        this.mvwDataVw.SetActiveView(this.vw06);

                        this.tabSite.BackColor = System.Drawing.Color.White;
                        this.tabSite.ForeColor = System.Drawing.Color.Blue;
                        this.tabSite.BorderStyle = System.Web.UI.WebControls.BorderStyle.Inset;
                        this.tabSite.Font.Bold = true;


                        this.tabSysInfo.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSysInfo.ForeColor = System.Drawing.Color.Black;
                        this.tabSysInfo.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSysInfo.Font.Bold = false;
                        this.tabUserCreate.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserCreate.ForeColor = System.Drawing.Color.Black;
                        this.tabUserCreate.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserCreate.Font.Bold = false;
                        this.tabUserAcc.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserAcc.ForeColor = System.Drawing.Color.Black;
                        this.tabUserAcc.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserAcc.Font.Bold = false;
                        this.tabAccCopy.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabAccCopy.ForeColor = System.Drawing.Color.Black;
                        this.tabAccCopy.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabAccCopy.Font.Bold = false;
                        break;
                    default:
                        CancelUser();

                        this.mvwDataVw.SetActiveView(this.vw03);

                        this.tabUserCreate.BackColor = System.Drawing.Color.White;
                        this.tabUserCreate.ForeColor = System.Drawing.Color.Blue;
                        this.tabUserCreate.BorderStyle = System.Web.UI.WebControls.BorderStyle.Inset;
                        this.tabUserCreate.Font.Bold = true;

                        this.tabUserAcc.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabUserAcc.ForeColor = System.Drawing.Color.Black;
                        this.tabUserAcc.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabUserAcc.Font.Bold = false;
                        this.tabAccCopy.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabAccCopy.ForeColor = System.Drawing.Color.Black;
                        this.tabAccCopy.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabAccCopy.Font.Bold = false;
                        this.tabSysInfo.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSysInfo.ForeColor = System.Drawing.Color.Black;
                        this.tabSysInfo.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSysInfo.Font.Bold = false;
                        this.tabSite.BackColor = System.Drawing.Color.FromArgb(255, 199, 33);
                        this.tabSite.ForeColor = System.Drawing.Color.Black;
                        this.tabSite.BorderStyle = System.Web.UI.WebControls.BorderStyle.Groove;
                        this.tabSite.Font.Bold = false;
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }//eof
        private void LoadDynamicData()
        {
            System.Data.DataSet dsLocal;
            BPWEBAccessControl.clsSecurityControl objApp = null;
            System.Data.DataTable dtLocal = null;

            string[] strIsAdmin = { "NO", "YES" };
            string[] strPWD_RESET_REQ = { "YES", "NO" };
            string[] strTitle = { "MR", "MRS","MS" };


            string[] strModule = { "ModuleID", "ModuleName" };
            try
            {
                objApp = new BPWEBAccessControl.clsSecurityControl();


                #region App User Info (Creating Users) [VW03]
                //--------UserGroup
                objApp.getSystemModules("USERGROUP", out dsLocal);
                this.ddlUserGroup.DataSource = dsLocal;
                this.ddlUserGroup.DataValueField = "Descriptions";
                this.ddlUserGroup.DataTextField = "Descriptions";
                this.ddlUserGroup.DataBind();
                this.ddlUserGroup.SelectedIndex = -1;

                //--------UserLocation
                objApp.getSystemModules("USERLOCATION", out dsLocal);
                this.ddlUserLocation.DataSource = dsLocal;
                this.ddlUserLocation.DataValueField = "Descriptions";
                this.ddlUserLocation.DataTextField = "Descriptions";
                this.ddlUserLocation.DataBind();
                this.ddlUserLocation.SelectedIndex = -1;

                //--------UserLocation
                objApp.getSystemModules("USERLOCATION", out dsLocal);
                this.ddlUserLocation.DataSource = dsLocal;
                this.ddlUserLocation.DataValueField = "Descriptions";
                this.ddlUserLocation.DataTextField = "Descriptions";
                this.ddlUserLocation.DataBind();
                this.ddlUserLocation.SelectedIndex = -1;

                //--------UserDepartment
                objApp.getSystemModules("USERDEPT", out dsLocal);
                this.ddlUserDepartment.DataSource = dsLocal;
                this.ddlUserDepartment.DataValueField = "Descriptions";
                this.ddlUserDepartment.DataTextField = "Descriptions";
                this.ddlUserDepartment.DataBind();
                this.ddlUserDepartment.SelectedIndex = -1;

                //--------UserDesignation
                objApp.getSystemModules("USERDESIG", out dsLocal);
                this.ddlUserDesignation.DataSource = dsLocal;
                this.ddlUserDesignation.DataValueField = "Descriptions";
                this.ddlUserDesignation.DataTextField = "Descriptions";
                this.ddlUserDesignation.DataBind();
                this.ddlUserDesignation.SelectedIndex = -1;

                //--------UserCountry
                objApp.getSystemModules("USERCOUNTRY", out dsLocal);
                this.ddlUserCountry.DataSource = dsLocal;
                this.ddlUserCountry.DataValueField = "Descriptions";
                this.ddlUserCountry.DataTextField = "Descriptions";
                this.ddlUserCountry.DataBind();
                this.ddlUserCountry.SelectedIndex = -1;

                //UserTitle
                dtLocal = new System.Data.DataTable();
                dtLocal = bplib.clsWebLib.CreateNewTableForArray("VALUES", strTitle);
                this.ddlUserTitle.DataSource = dtLocal.DefaultView;
                this.ddlUserTitle.DataTextField = "VALUES";
                this.ddlUserTitle.DataValueField = "VALUES";
                this.ddlUserTitle.DataBind();
                this.ddlUserTitle.SelectedValue = "MR";

                //IS ADMIN?
                dtLocal = new System.Data.DataTable();
                dtLocal = bplib.clsWebLib.CreateNewTableForArray("VALUES", strIsAdmin);
                this.ddlIsAdmin.DataSource = dtLocal.DefaultView;
                this.ddlIsAdmin.DataTextField = "VALUES";
                this.ddlIsAdmin.DataValueField = "VALUES";
                this.ddlIsAdmin.DataBind();
                this.ddlIsAdmin.SelectedValue = "NO";

                //PWD_RESET_REQ
                dtLocal = new System.Data.DataTable();
                dtLocal = bplib.clsWebLib.CreateNewTableForArray("VALUES", strPWD_RESET_REQ);
                this.ddlPWD_RESET_REQ.DataSource = dtLocal;
                this.ddlPWD_RESET_REQ.DataTextField = "VALUES";
                this.ddlPWD_RESET_REQ.DataValueField = "VALUES";
                this.ddlPWD_RESET_REQ.DataBind();
                this.ddlPWD_RESET_REQ.SelectedValue = "USER";
                #endregion


                objApp.GetUserAccManagerInfo(out dsLocal);

                #region App Access Control [VW02]
                this.ddlUserID.Items.Clear();
                this.ddlUserID.DataSource = dsLocal.Tables[0].DefaultView;
                this.ddlUserID.DataTextField = "USERID";
                this.ddlUserID.DataValueField = "USERID";
                this.ddlUserID.DataBind();
                this.ddlUserID.Items.Add("SELECT USER");
                this.ddlUserID.SelectedValue = "SELECT USER";

                //-------MODULE------------------------------------------------
                this.ddlSearchKey.Items.Clear();
                dtLocal = new System.Data.DataTable();
                dtLocal = bplib.clsWebLib.CreateNewTableForArray("MODULE", strModule);
                this.ddlSearchKey.DataSource = dtLocal;
                this.ddlSearchKey.DataTextField = "MODULE";
                this.ddlSearchKey.DataTextField = "MODULE";
                this.ddlSearchKey.DataBind();
                #endregion

                #region Access Copy Utility [VW04]
                //Source Load Users
                this.ddlSourceId.Items.Clear();
                this.ddlSourceId.DataSource = dsLocal.Tables[0].DefaultView;
                this.ddlSourceId.DataTextField = "USERID";
                this.ddlSourceId.DataValueField = "USERID";
                this.ddlSourceId.DataBind();
                this.ddlSourceId.Items.Add("SELECT USER");
                this.ddlSourceId.SelectedValue = "SELECT USER";

                //Target Load Users
                this.ddlDestUserId.Items.Clear();
                this.ddlDestUserId.DataSource = dsLocal.Tables[0].DefaultView;
                this.ddlDestUserId.DataTextField = "USERID";
                this.ddlDestUserId.DataValueField = "USERID";
                this.ddlDestUserId.DataBind();
                this.ddlDestUserId.Items.Add("SELECT USER");
                this.ddlDestUserId.SelectedValue = "SELECT USER";
                #endregion

                #region System Admin Info [VW05]
                //--------------------------------------
                objApp.GetUserList("", out dsLocal);
                this.lbUserLists.Items.Clear();
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    this.lbUserLists.DataSource = dsLocal.Tables[0].DefaultView;
                    this.lbUserLists.DataTextField = "USERID";
                    this.lbUserLists.DataValueField = "USERID";
                    this.lbUserLists.DataBind();
                }
                #endregion

                #region Site Access [VW06]
                //-------------
                this.ddlSiteUsers.Items.Clear();
                this.ddlSiteUsers.DataSource = dsLocal.Tables[0].DefaultView;
                this.ddlSiteUsers.DataTextField = "USERID";
                this.ddlSiteUsers.DataValueField = "USERID";
                this.ddlSiteUsers.DataBind();
                this.ddlSiteUsers.Items.Add("SELECT USER");
                this.ddlSiteUsers.SelectedValue = "SELECT USER";
                #endregion

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
        private void LogOff()
        {
            //Session["LOGIN_STATUS"] = 0;
            //Session["USER"] = "";
            Session["VERIFICATION_STATE"] = 0;
            Response.Redirect("AppControlPanel.aspx?cat=Entry");
        }//eof
        #endregion

        #endregion

    }

}
    