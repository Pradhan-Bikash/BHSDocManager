using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Syncfusion.XlsIO;

namespace PWOMS
{
    public partial class webfrmPWOMSReport : System.Web.UI.Page
    {
        #region Form's Events
        private void Page_Load(object sender, System.EventArgs e)
        {
            Master.Page.Title = BPWEBAccessControl.Global.PageTitle;
            Master.masterlblFormName.Text = "SMS Reports / Views";
            this.lblfrmName.Text = "SMS Reports / Views";

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
                    this.txtFromDate.Text = System.DateTime.Now.AddDays(-15).ToString("dd-MMM-yyyy");
                    this.txtToDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");

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
                dtLocal = clsSysLanguage.sysLanguage();
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
                if (this.lblViewState.Text.Trim() == "")
                {
                    //Cancel();
                    //this.TextEntryID.Text = DOCID;
                    //LoadDetails();
                }
                if (this.lblViewState.Text.Trim() == "TELEDATA")
                {
                    Cancel();
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

        protected void imgbtnXL201_Click(object sender, ImageClickEventArgs e)
        {
            ProcessToViewEmployeeInfo();
        }
        protected void imgbtnXL202_Click(object sender, ImageClickEventArgs e)
        {
            ProcessToViewBankInfo();
        }//eof
        protected void imgbtnXL203_Click(object sender, ImageClickEventArgs e)
        {
            ProcessToViewLeaseAgreementInfo();
        }//eof
        protected void imgbtnXL204_Click(object sender, ImageClickEventArgs e)
        {
            ProcessToViewFundingInfo();
        }//eof
        protected void imgbtnXL205_Click(object sender, ImageClickEventArgs e)
        {
            ProcessToViewBusinessRegInfo();
        }//eof        

        protected void btnLogOff_Click(object sender, EventArgs e)
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
            //string strSiteId = Session["USER_SITE"].ToString().Trim();

            string fromDate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            string toDate = System.DateTime.Now.ToString("dd-MMM-yyyy");
            try
            {
                objApp = new PWOMS.clsApplication();
                if (FLAG == "") //this is default search function
                {
                    //objApp.SearchTeleData(fromDate, toDate, strKey, strSiteId, out dsLocal);
                }
                if (FLAG == "TELEDATA")
                {
                    //objApp.SearchTeleData(fromDate, toDate, strKey, strSiteId, out dsLocal);
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
                        //DeleteData();
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
        private void LoadDynamicData()
        {
            System.Data.DataSet dsLocal;
            System.Data.DataTable dtLocal;
            PWOMS.clsHK objApp = null;
            PWOMS.clsApplication objApp2 = null;
            string strSiteId = Session["USER_SITE"].ToString().Trim();

            string[] strOrderType = { "ENQUERY", "ORDER_CONFIRMED" };

            try
            {
                objApp = new PWOMS.clsHK();
                objApp2 = new PWOMS.clsApplication();

                ////Customer List data
                //objApp.GetDataOfCustomerForCombo(strSiteId.ToString(), out dsLocal);
                //this.ddlCustomer.DataSource = dsLocal;
                //this.ddlCustomer.DataValueField = "SysRefID";
                //this.ddlCustomer.DataTextField = "CompanyName";
                //this.ddlCustomer.DataBind();
                //this.ddlCustomer.Items.Insert(0, new ListItem("ALL", "ALL"));
                //this.ddlCustomer.SelectedValue = "ALL";

                ////Order Type data - Dyna table
                //dtLocal = bplib.clsWebLib.CreateNewTableForArray("OrderType", strOrderType);
                //this.ddlOrderType.DataSource = dtLocal;
                //this.ddlOrderType.DataTextField = "OrderType";
                //this.ddlOrderType.DataBind();
                //this.ddlOrderType.Items.Insert(0, new ListItem("ALL", "ALL"));
                //this.ddlOrderType.SelectedValue = "ALL";

                ////Brand/Buyer List data
                //objApp2.SearchBrandSMSOrderDetails(strSiteId, out dsLocal);
                //this.ddlBrand_Buyer.DataSource = dsLocal;
                //this.ddlBrand_Buyer.DataValueField = "Brand";
                //this.ddlBrand_Buyer.DataTextField = "Brand";
                //this.ddlBrand_Buyer.DataBind();
                ////this.ddlBrand_Buyer.Items.Insert(0, new ListItem("", ""));
                //this.ddlBrand_Buyer.Items.Insert(0, new ListItem("ALL", "ALL"));
                //this.ddlBrand_Buyer.SelectedValue = "ALL";

            }
            catch (System.Exception ex)
            {
                ShowLog(ex.ToString());
            }
            finally
            {
                dsLocal = null;
                objApp = null;
            }
        }//eof
        private void Cancel()
        {
            HideLog();
            ClearTheForm();
            Session["VERIFICATION_STATE"] = 0;

            this.panSearch.Visible = false;
            this.dgSearch.DataSource = null;
            this.dgSearch.Visible = false;
        }//eof
        private void SetDefault()
        {
            try
            {


                this.lblViewState.Text = "";
                this.lblDlgState.Text = "";
                this.lblSearchTitle.Text = "Search";
                this.lblViewName.Text = "";

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
            //Response.Redirect(TradingOrder.Global.HomeControlPage);
            Response.Redirect("AppControlPanel.aspx?cat=Report");
        }//eof
        #endregion

        #region Excel Reports

        #region Employee Info (PWOMS201)
        private void ProcessToViewEmployeeInfo()
        {
            PWOMS.clsPWOMSRepo objApp = null;
            string strViewHeader = "";
            string strFileName = "";
            string strMessage = "";
            bool DATA_OK = false;
            HideLog();
            try
            {
                objApp = new PWOMS.clsPWOMSRepo();
                if (DATA_OK == false)
                {
                    if (this.txtFromDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtFromDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the From Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    if (this.txtToDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtToDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the To Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    //procloc

                    string fromDate = bplib.clsWebLib.DateData_DBToApp(this.txtFromDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    string toDate = bplib.clsWebLib.DateData_DBToApp(this.txtToDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    this.DOCX.InnerHtml = clsWebProcDataBuilder.CheckAndLock_PROC("PWOMS201", ((string)Session["user"]));
                    if (this.DOCX.InnerHtml.Trim() == "")
                    {
                        if (strMessage != "")
                        {
                            this.DOCX.InnerHtml = strMessage;
                            return;
                        }
                        strViewHeader = "[PWOMS201] Employee Info (Update Date Between: " + fromDate +" and " + toDate +")";
                        strFileName = Server.MapPath("EmployeeInfo.xlsx");
                        CreateEmployeeInfoDataXL(fromDate,toDate, strFileName, strViewHeader);
                        ShowLog("Report gen process executed sucessfully.....");
                        clsWebProcDataBuilder.RemoveLock_PROC("PWOMS201");
                        //proloc end
                    }
                }
            }
            catch (Exception ex)
            {
				clsWebProcDataBuilder.RemoveLock_PROC("PWOMS201");
				ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                //dsLocal = null;
                objApp = null;
            }
        } //eof
        public void CreateEmployeeInfoDataXL(string fromdate, string todate, string fileName, string strHeader)
        {
            ExcelEngine excelEngine = null;
            IApplication application = null;
            IWorkbook workbook = null;
            string strSiteId = Session["USER_SITE"].ToString();
           PWOMS.clsPWOMSRepo objView;
            System.IO.FileInfo myFile = null;

            try
            {
                /// Writer 	
                myFile = new System.IO.FileInfo(fileName);
                if (myFile.Exists == true)
                {
                    myFile.Delete();
                }

                // Create Header 

                //Step 1 : Instantiate the spreadsheet creation engine.
                excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                application = excelEngine.Excel;
                //The new workbook Creating with 1 worksheets
                workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];
                sheet.Name = "EmployeeInfo";
                //Grid Line False
                sheet.IsGridLinesVisible = false;
                workbook.Version = ExcelVersion.Excel2016;

                objView = new PWOMS.clsPWOMSRepo();
                objView.EmployeeInfoDataXls(fromdate,todate, strSiteId, strHeader, ref sheet);

                //Saving the workbook to disk.
                workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.PromptDialog);
            }
            catch (System.IO.IOException iex)
            {
                throw (iex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //No exception will be thrown if there are unsaved workbooks.
                workbook.Close();
                excelEngine.ThrowNotSavedOnDestroy = false;
                application = null;
                excelEngine.Dispose();
                myFile = null;
                objView = null;
            }
        }//eof
        #endregion

        #region Bank Info (PWOMS202)
        private void ProcessToViewBankInfo()
        {
            PWOMS.clsPWOMSRepo objApp = null;
            string strViewHeader = "";
            string strFileName = "";
            string strMessage = "";
            bool DATA_OK = false;
            HideLog();
            try
            {
                objApp = new PWOMS.clsPWOMSRepo();
                if (DATA_OK == false)
                {
                    if (this.txtFromDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtFromDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the From Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    if (this.txtToDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtToDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the To Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    //procloc

                    string fromDate = bplib.clsWebLib.DateData_DBToApp(this.txtFromDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    string toDate = bplib.clsWebLib.DateData_DBToApp(this.txtToDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    this.DOCX.InnerHtml = clsWebProcDataBuilder.CheckAndLock_PROC("PWOMS202", ((string)Session["user"]));
                    if (this.DOCX.InnerHtml.Trim() == "")
                    {
                        if (strMessage != "")
                        {
                            this.DOCX.InnerHtml = strMessage;
                            return;
                        }
                        strViewHeader = "[PWOMS202] Bank Info (Update Date Between: " + fromDate + " and " + toDate + ")";
                        strFileName = Server.MapPath("BankInfo.xlsx");
                        CreateBankInfoDataXL(fromDate, toDate, strFileName, strViewHeader);
                        ShowLog("Report gen process executed sucessfully.....");
                        clsWebProcDataBuilder.RemoveLock_PROC("PWOMS202");
                        //proloc end
                    }
                }
            }
            catch (Exception ex)
            {
                clsWebProcDataBuilder.RemoveLock_PROC("PWOMS202");
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                //dsLocal = null;
                objApp = null;
            }
        } //eof
        public void CreateBankInfoDataXL(string fromdate, string todate, string fileName, string strHeader)
        {
            ExcelEngine excelEngine = null;
            IApplication application = null;
            IWorkbook workbook = null;
            string strSiteId = Session["USER_SITE"].ToString();
            PWOMS.clsPWOMSRepo objView;
            System.IO.FileInfo myFile = null;

            try
            {
                /// Writer 	
                myFile = new System.IO.FileInfo(fileName);
                if (myFile.Exists == true)
                {
                    myFile.Delete();
                }

                // Create Header 

                //Step 1 : Instantiate the spreadsheet creation engine.
                excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                application = excelEngine.Excel;
                //The new workbook Creating with 1 worksheets
                workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];
                sheet.Name = "BankInfo";
                //Grid Line False
                sheet.IsGridLinesVisible = false;
                workbook.Version = ExcelVersion.Excel2016;

                objView = new PWOMS.clsPWOMSRepo();
                objView.BankInfoDataXls(fromdate, todate, strSiteId, strHeader, ref sheet);

                //Saving the workbook to disk.
                workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.PromptDialog);
            }
            catch (System.IO.IOException iex)
            {
                throw (iex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //No exception will be thrown if there are unsaved workbooks.
                workbook.Close();
                excelEngine.ThrowNotSavedOnDestroy = false;
                application = null;
                excelEngine.Dispose();
                myFile = null;
                objView = null;
            }
        }//eof
        #endregion

        #region Lease Agreement Info (PWOMS203)
        private void ProcessToViewLeaseAgreementInfo()
        {
            PWOMS.clsPWOMSRepo objApp = null;
            string strViewHeader = "";
            string strFileName = "";
            string strMessage = "";
            bool DATA_OK = false;
            HideLog();
            try
            {
                objApp = new PWOMS.clsPWOMSRepo();
                if (DATA_OK == false)
                {
                    if (this.txtFromDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtFromDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the From Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    if (this.txtToDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtToDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the To Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    //procloc

                    string fromDate = bplib.clsWebLib.DateData_DBToApp(this.txtFromDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    string toDate = bplib.clsWebLib.DateData_DBToApp(this.txtToDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    this.DOCX.InnerHtml = clsWebProcDataBuilder.CheckAndLock_PROC("PWOMS203", ((string)Session["user"]));
                    if (this.DOCX.InnerHtml.Trim() == "")
                    {
                        if (strMessage != "")
                        {
                            this.DOCX.InnerHtml = strMessage;
                            return;
                        }
                        strViewHeader = "[PWOMS203] Lease Agreement Info (Update Date Between: " + fromDate + " and " + toDate + ")";
                        strFileName = Server.MapPath("LeaseAgreementInfo.xlsx");
                        CreateLeaseAgreementInfoDataXL(fromDate, toDate, strFileName, strViewHeader);
                        ShowLog("Report gen process executed sucessfully.....");
                        clsWebProcDataBuilder.RemoveLock_PROC("PWOMS203");
                        //proloc end
                    }
                }
            }
            catch (Exception ex)
            {
                clsWebProcDataBuilder.RemoveLock_PROC("PWOMS203");
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                //dsLocal = null;
                objApp = null;
            }
        } //eof
        public void CreateLeaseAgreementInfoDataXL(string fromdate, string todate, string fileName, string strHeader)
        {
            ExcelEngine excelEngine = null;
            IApplication application = null;
            IWorkbook workbook = null;
            string strSiteId = Session["USER_SITE"].ToString();
            PWOMS.clsPWOMSRepo objView;
            System.IO.FileInfo myFile = null;

            try
            {
                /// Writer 	
                myFile = new System.IO.FileInfo(fileName);
                if (myFile.Exists == true)
                {
                    myFile.Delete();
                }

                // Create Header 

                //Step 1 : Instantiate the spreadsheet creation engine.
                excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                application = excelEngine.Excel;
                //The new workbook Creating with 1 worksheets
                workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];
                sheet.Name = "LeaseAgreementInfo";
                //Grid Line False
                sheet.IsGridLinesVisible = false;
                workbook.Version = ExcelVersion.Excel2016;

                objView = new PWOMS.clsPWOMSRepo();
                objView.LeaseAgreementInfoDataXls(fromdate, todate, strSiteId, strHeader, ref sheet);

                //Saving the workbook to disk.
                workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.PromptDialog);
            }
            catch (System.IO.IOException iex)
            {
                throw (iex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //No exception will be thrown if there are unsaved workbooks.
                workbook.Close();
                excelEngine.ThrowNotSavedOnDestroy = false;
                application = null;
                excelEngine.Dispose();
                myFile = null;
                objView = null;
            }
        }//eof
        #endregion

        #region Funding Info (PWOMS204)
        private void ProcessToViewFundingInfo()
        {
            PWOMS.clsPWOMSRepo objApp = null;
            string strViewHeader = "";
            string strFileName = "";
            string strMessage = "";
            bool DATA_OK = false;
            HideLog();
            try
            {
                objApp = new PWOMS.clsPWOMSRepo();
                if (DATA_OK == false)
                {
                    if (this.txtFromDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtFromDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the From Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    if (this.txtToDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtToDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the To Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    //procloc

                    string fromDate = bplib.clsWebLib.DateData_DBToApp(this.txtFromDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    string toDate = bplib.clsWebLib.DateData_DBToApp(this.txtToDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    this.DOCX.InnerHtml = clsWebProcDataBuilder.CheckAndLock_PROC("PWOMS204", ((string)Session["user"]));
                    if (this.DOCX.InnerHtml.Trim() == "")
                    {
                        if (strMessage != "")
                        {
                            this.DOCX.InnerHtml = strMessage;
                            return;
                        }
                        strViewHeader = "[PWOMS204] Funding Info (Update Date Between: " + fromDate + " and " + toDate + ")";
                        strFileName = Server.MapPath("FundingInfo.xlsx");
                        CreateFundingInfoDataXL(fromDate, toDate, strFileName, strViewHeader);
                        ShowLog("Report gen process executed sucessfully.....");
                        clsWebProcDataBuilder.RemoveLock_PROC("PWOMS204");
                        //proloc end
                    }
                }
            }
            catch (Exception ex)
            {
                clsWebProcDataBuilder.RemoveLock_PROC("PWOMS204");
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                //dsLocal = null;
                objApp = null;
            }
        } //eof
        public void CreateFundingInfoDataXL(string fromdate, string todate, string fileName, string strHeader)
        {
            ExcelEngine excelEngine = null;
            IApplication application = null;
            IWorkbook workbook = null;
            string strSiteId = Session["USER_SITE"].ToString();
            PWOMS.clsPWOMSRepo objView;
            System.IO.FileInfo myFile = null;

            try
            {
                /// Writer 	
                myFile = new System.IO.FileInfo(fileName);
                if (myFile.Exists == true)
                {
                    myFile.Delete();
                }

                // Create Header 

                //Step 1 : Instantiate the spreadsheet creation engine.
                excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                application = excelEngine.Excel;
                //The new workbook Creating with 1 worksheets
                workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];
                sheet.Name = "Funding";
                //Grid Line False
                sheet.IsGridLinesVisible = false;
                workbook.Version = ExcelVersion.Excel2016;

                objView = new PWOMS.clsPWOMSRepo();
                objView.FundingInfoDataXls(fromdate, todate, strSiteId, strHeader, ref sheet);

                //Saving the workbook to disk.
                workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.PromptDialog);
            }
            catch (System.IO.IOException iex)
            {
                throw (iex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //No exception will be thrown if there are unsaved workbooks.
                workbook.Close();
                excelEngine.ThrowNotSavedOnDestroy = false;
                application = null;
                excelEngine.Dispose();
                myFile = null;
                objView = null;
            }
        }//eof
        #endregion

        #region BusinessReg Info (PWOMS205)
        private void ProcessToViewBusinessRegInfo()
        {
            PWOMS.clsPWOMSRepo objApp = null;
            string strViewHeader = "";
            string strFileName = "";
            string strMessage = "";
            bool DATA_OK = false;
            HideLog();
            try
            {
                objApp = new PWOMS.clsPWOMSRepo();
                if (DATA_OK == false)
                {
                    if (this.txtFromDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtFromDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the From Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    if (this.txtToDate.Text == "" || bplib.clsWebLib.IsDateOK(this.txtToDate.Text.Trim()) == false)
                    {
                        System.Exception ex = new Exception("Define the To Date........... (allowed format is  dd-MMM-yyyy ex: '01-jan-2008')");
                        throw (ex);
                    }

                    DATA_OK = true;
                }
                if (DATA_OK == true)
                {
                    //procloc

                    string fromDate = bplib.clsWebLib.DateData_DBToApp(this.txtFromDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    string toDate = bplib.clsWebLib.DateData_DBToApp(this.txtToDate.Text.ToString().Trim().ToString(), bplib.clsWebLib.STD_DATE_FORMAT).ToString("dd-MMM-yyyy");

                    this.DOCX.InnerHtml = clsWebProcDataBuilder.CheckAndLock_PROC("PWOMS205", ((string)Session["user"]));
                    if (this.DOCX.InnerHtml.Trim() == "")
                    {
                        if (strMessage != "")
                        {
                            this.DOCX.InnerHtml = strMessage;
                            return;
                        }
                        strViewHeader = "[PWOMS205] Business Registration Info (Update Date Between: " + fromDate + " and " + toDate + ")";
                        strFileName = Server.MapPath("BusinessRegInfo.xlsx");
                        CreateBusinessRegInfoDataXL(fromDate, toDate, strFileName, strViewHeader);
                        ShowLog("Report gen process executed sucessfully.....");
                        clsWebProcDataBuilder.RemoveLock_PROC("PWOMS205");
                        //proloc end
                    }
                }
            }
            catch (Exception ex)
            {
                clsWebProcDataBuilder.RemoveLock_PROC("PWOMS205");
                ShowLog("Error:" + ex.Message.ToString());
            }
            finally
            {
                //dsLocal = null;
                objApp = null;
            }
        } //eof
        public void CreateBusinessRegInfoDataXL(string fromdate, string todate, string fileName, string strHeader)
        {
            ExcelEngine excelEngine = null;
            IApplication application = null;
            IWorkbook workbook = null;
            string strSiteId = Session["USER_SITE"].ToString();
            PWOMS.clsPWOMSRepo objView;
            System.IO.FileInfo myFile = null;

            try
            {
                /// Writer 	
                myFile = new System.IO.FileInfo(fileName);
                if (myFile.Exists == true)
                {
                    myFile.Delete();
                }

                // Create Header 

                //Step 1 : Instantiate the spreadsheet creation engine.
                excelEngine = new ExcelEngine();
                //Step 2 : Instantiate the excel application object.
                application = excelEngine.Excel;
                //The new workbook Creating with 1 worksheets
                workbook = application.Workbooks.Create(1);
                //The first worksheet object in the worksheets collection is accessed.
                IWorksheet sheet = workbook.Worksheets[0];
                sheet.Name = "BusinessRegInfo";
                //Grid Line False
                sheet.IsGridLinesVisible = false;
                workbook.Version = ExcelVersion.Excel2016;

                objView = new PWOMS.clsPWOMSRepo();
                objView.BusinessRegInfoDataXls(fromdate, todate, strSiteId, strHeader, ref sheet);

                //Saving the workbook to disk.
                workbook.SaveAs(fileName, ExcelSaveType.SaveAsXLS, Response, ExcelDownloadType.PromptDialog);
            }
            catch (System.IO.IOException iex)
            {
                throw (iex);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                //No exception will be thrown if there are unsaved workbooks.
                workbook.Close();
                excelEngine.ThrowNotSavedOnDestroy = false;
                application = null;
                excelEngine.Dispose();
                myFile = null;
                objView = null;
            }
        }//eof
        #endregion

        #endregion

        #endregion

    }
}