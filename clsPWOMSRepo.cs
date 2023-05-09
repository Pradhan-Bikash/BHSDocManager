using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Data;
using Syncfusion.XlsIO;


namespace PWOMS
{
    public class clsPWOMSRepo
    {
        private string JPLLogoPath;
        public clsPWOMSRepo()
        {
             JPLLogoPath = System.Web.HttpContext.Current.Server.MapPath("Picture/" + "JPLIND.png");
        }//eof

        #region Cutomized Function About ProcLock
        public string CheckAndLock_PROC(string procid, string userid)
        {
            System.Data.DataSet dsLocal;
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string SB = "";
            try
            {
                strSql = "Select * from BPProcLoc where procid='" + procid + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsLocal, false, false, "", "1");
                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    if ("" + dsLocal.Tables[0].Rows[0]["updateby"].ToString() != "")
                    {
                        SB = SB + "<font style='FONT-WEIGHT: bold; FONT-SIZE: 10pt; COLOR: Blue; FONT-FAMILY: Courier New, Tahoma,Verdana, Arial'>";
                        SB = SB + "Hello !!! " + userid;
                        SB = SB + "<br>";
                        SB = SB + "The related system process (proc id: " + procid + " / [" + "" + dsLocal.Tables[0].Rows[0]["descr"].ToString() + "]) for this view is currently used by other user (" + "" + dsLocal.Tables[0].Rows[0]["updateby"].ToString() + ") from " + "" + dsLocal.Tables[0].Rows[0]["sessionupdate"].ToString() + ". Please try after some time. Thank you.";
                        SB = SB + "<br>";
                        SB = SB + "</font>";
                    }
                    else
                    {
                        objCon.OpenConnection("1");
                        strSql = "Update BPProcLoc set updateby='" + userid + "', Sessionupdate='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' where procid='" + procid + "'";
                        objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                        objCon.CloseConnection();
                    }
                }
                else
                {
                    System.Exception ex = new Exception("This proc ID does not existing in procloc table. Please update.");
                    throw (ex);
                }
                return SB;
            }
            catch (System.Exception ex)
            {
                //this will show all details ...
                //strResult=ex.ToString();
                //This will show only message part...

                SB = "<table id='TableDocBase2' style='Z-INDEX: 101' width='700' height='80' border='0' cellpadding='0' cellspacing='0' align='center' bgColor='#cee1ff'>";
                SB = SB + "<tr>";
                SB = SB + "<td width='700' height='20' colspan='3' bgColor='#cee1ff' align='left'>";
                SB = SB + "<font style='FONT-WEIGHT: normal; FONT-SIZE: 9pt; COLOR: Red; FONT-FAMILY: Verdana, Arial'>";
                SB = SB + ex.Message.ToString();
                SB = SB + "</font>";
                SB = SB + "</td></tr></table>";
                return SB;
            }
            finally
            {
                objCon = null;
                dsLocal = null;
            }
        } // end function

        public void RemoveLock_PROC(string procid)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                strSql = "Update BPProcLoc set updateby='" + System.DBNull.Value + "', Sessionupdate='" + System.DBNull.Value + "' where procid='" + procid + "'";
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CloseConnection();
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        } // end function
        #endregion

        #region [PWOMS201] -Employee Info.XLSX
        public void EmployeeInfoDataXls(string fromdate, string todate, string strSiteId, string strHeader, ref Syncfusion.XlsIO.IWorksheet sheet)
        {
            System.Data.DataSet dsLocal = null;
            string hex = "#FFDDEBF7";
            System.Drawing.Color LightBlue_Color = System.Drawing.ColorTranslator.FromHtml(hex);
            int ROW = 1;
            int i = 0;

            try
            {
                #region Report Titel
                sheet.Range["A1:R2"].CellStyle.Font.FontName = "Calibri";
                sheet.Range["A1:R2"].CellStyle.Font.Size = 12f;
                sheet.Range["A1:R2"].CellStyle.Font.Bold = true;
                sheet.Range["A1:R2"].RowHeight = 20;
                sheet.Range["A1:R2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                sheet.Range["A1:R2"].VerticalAlignment = ExcelVAlign.VAlignTop;
                sheet.Range["A1:R1"].CellStyle.Color = System.Drawing.Color.LightYellow;
                sheet.Range["A1:R1"].RowHeight = 15;
                sheet.Range["A1:R1"].Merge();
                sheet.Range["A1"].Text = strHeader;

                #endregion

                sheet.Range["A4"].FreezePanes();

                ROW = 3;

                GetDataOfEmployeeInfo(fromdate, todate, strSiteId, out dsLocal);

                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    #region Details Heading

                    sheet.Range["A" + ROW + ":R" + ROW].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    sheet.Range["A" + ROW + ":R" + ROW].VerticalAlignment = ExcelVAlign.VAlignCenter;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.FontName = "Calibri";
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.Size = 9f;
                    sheet.Range["A" + ROW + ":R" + ROW].WrapText = true;
                    sheet.Range["A" + ROW + ":R" + ROW].RowHeight = 36;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Interior.Color = LightBlue_Color;
                    sheet.Range["A" + ROW + ":R" + ROW].VerticalAlignment = ExcelVAlign.VAlignTop;


                    sheet.Range["A" + ROW].Text = "Location";
                    sheet.Range["A" + ROW].ColumnWidth = 10;
                    sheet.Range["B" + ROW].Text = "Employee Name";
                    sheet.Range["B" + ROW].ColumnWidth = 20;
                    sheet.Range["C" + ROW].Text = "Employee # (system generated)";
                    sheet.Range["C" + ROW].ColumnWidth = 11;
                    sheet.Range["D" + ROW].Text = "Gender";
                    sheet.Range["D" + ROW].ColumnWidth = 7;
                    sheet.Range["E" + ROW].Text = "Date of Birth";
                    sheet.Range["E" + ROW].ColumnWidth = 11;
                    sheet.Range["F" + ROW].Text = "Academic Qualification";
                    sheet.Range["F" + ROW].ColumnWidth = 12;
                    sheet.Range["G" + ROW].Text = "Date of Hire";
                    sheet.Range["G" + ROW].ColumnWidth = 11;
                    sheet.Range["H" + ROW].Text = "Department";
                    sheet.Range["H" + ROW].ColumnWidth = 12;
                    sheet.Range["I" + ROW].Text = "Current Title";
                    sheet.Range["I" + ROW].ColumnWidth = 12;
                    sheet.Range["J" + ROW].Text = "Current Salary";
                    sheet.Range["J" + ROW].ColumnWidth = 8;
                    sheet.Range["K" + ROW].Text = "Last Salary";
                    sheet.Range["K" + ROW].ColumnWidth = 8;
                    sheet.Range["L" + ROW].Text = "Last Raise Date";
                    sheet.Range["L" + ROW].ColumnWidth = 11;
                    sheet.Range["M" + ROW].Text = "Raise Amount";
                    sheet.Range["M" + ROW].ColumnWidth = 8;
                    sheet.Range["N" + ROW].Text = "Raise %";
                    sheet.Range["N" + ROW].ColumnWidth = 8;
                    sheet.Range["O" + ROW].Text = "Reason for Raise";
                    sheet.Range["O" + ROW].ColumnWidth = 12;
                    sheet.Range["P" + ROW].Text = "Rating of Performance Appraisal";
                    sheet.Range["P" + ROW].ColumnWidth = 10;
                    sheet.Range["Q" + ROW].Text = "Email Address";
                    sheet.Range["Q" + ROW].ColumnWidth = 20;
                    sheet.Range["R" + ROW].Text = "Mobile phone #";
                    sheet.Range["R" + ROW].ColumnWidth = 12;

                    #endregion

                    ROW++;

                    i = 0;

                    while (i < dsLocal.Tables[0].Rows.Count)
                    {

                        sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.FontName = "Calibri";
                        sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.Size = 9f;
                        sheet.Range["A" + ROW + ":R" + ROW].WrapText = true;
                        sheet.Range["A" + ROW + ":R" + ROW].RowHeight = 13;
                        sheet.Range["A" + ROW + ":R" + ROW].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        sheet.Range["J" + ROW + ":K" + ROW].HorizontalAlignment = ExcelHAlign.HAlignRight;
                        sheet.Range["M" + ROW + ":N" + ROW].HorizontalAlignment = ExcelHAlign.HAlignRight;
                        sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Hair;
                        sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                        sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;


                        sheet["A" + ROW].Text = dsLocal.Tables[0].Rows[i]["Location"].ToString();
                        sheet["B" + ROW].Text = dsLocal.Tables[0].Rows[i]["NameTitle"].ToString() + " " + dsLocal.Tables[0].Rows[i]["FirstName"].ToString() + " " + dsLocal.Tables[0].Rows[i]["LastName"].ToString();
                        sheet["C" + ROW].Text = dsLocal.Tables[0].Rows[i]["EmployeeCode"].ToString();
                        sheet["D" + ROW].Text = dsLocal.Tables[0].Rows[i]["Gender"].ToString();
                        sheet["E" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["DateOfBirth"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["F" + ROW].Text = dsLocal.Tables[0].Rows[i]["AcademicQualification"].ToString();
                        sheet["G" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["DateOfJoin"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["H" + ROW].Text = dsLocal.Tables[0].Rows[i]["Department"].ToString();
                        sheet["I" + ROW].Text = dsLocal.Tables[0].Rows[i]["Designation"].ToString();
                        sheet["J" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["CurrentSalary"].ToString()));
                        sheet["K" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["LastSalary"].ToString()));
                        sheet["L" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["LastRaiseDate"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["M" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["RaiseAmount"].ToString()));
                        sheet["N" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["RaisePercent"].ToString()));
                        sheet["O" + ROW].Text = dsLocal.Tables[0].Rows[i]["ReasonForRaise"].ToString();
                        sheet["P" + ROW].Text = dsLocal.Tables[0].Rows[i]["AppraisalRating"].ToString();
                        sheet["Q" + ROW].Text = dsLocal.Tables[0].Rows[i]["EmailAddress"].ToString();
                        sheet["R" + ROW].Text = dsLocal.Tables[0].Rows[i]["PhoneNumber"].ToString();
                       
                        i++;
                        ROW++;

                    }

                }
                else
                {
                    sheet.Range["A" + ROW + ":R" + ROW].Text = "No Data Available................";
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.FontName = "Tahoma";
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.Size = 8f;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.White;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Interior.Color = System.Drawing.Color.Red;
                    sheet.Range["A" + ROW + ":R" + ROW].Merge();
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":R" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    //ROW++;
                }


                ////Protecting Worksheet using Password
                //sheet.UsedRange.CellStyle.Locked = true;
                //sheet.Protect(bplib.clsWebLib.REPORT_PASSWORD.ToString());

                //optional - set a border during print
                //sheet.UsedRange.BorderAround(ExcelLineStyle.Medium, ExcelKnownColors.Black);

                //Setting Page format - Actual page set to be used to all reports must 
                sheet.PageSetup.TopMargin = .17;
                sheet.PageSetup.BottomMargin = .24;
                sheet.PageSetup.FooterMargin = .17;
                sheet.PageSetup.LeftFooter = "&\"Times New Roman\"&06" + "Printing Date : " + System.DateTime.Now.ToString("dd-MMM-yyyy");
                sheet.PageSetup.RightFooter = "&\"Times New Roman\"&06" + "Page &P of &N";
                sheet.PageSetup.LeftMargin = .17;
                sheet.PageSetup.RightMargin = .17;
                sheet.PageSetup.Orientation = ExcelPageOrientation.Landscape;
                sheet.PageSetup.FitToPagesTall = 0;
                sheet.PageSetup.FitToPagesWide = 1;
                sheet.PageSetup.PrintTitleRows = "$1:$1";
                //---------------------------
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsLocal = null;
            }
        }//EOF
        private DataTable GetDataOfEmployeeInfo(string fromdate, string todate, string strUserRef, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = @"Select * from tblEmployeeMaster
                Where UpdateOn Between '"+ fromdate + "' and '"+ todate + @"'
                Order By Location,EmployeeCode";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");

                return dsRef.Tables[0];

            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF

        #endregion       

        #region [PWOMS202] -Bank Info.XLSX
        public void BankInfoDataXls(string fromdate, string todate, string strSiteId, string strHeader, ref Syncfusion.XlsIO.IWorksheet sheet)
        {
            System.Data.DataSet dsLocal = null;
            string hex = "#FFDDEBF7";
            System.Drawing.Color LightBlue_Color = System.Drawing.ColorTranslator.FromHtml(hex);
            int ROW = 1;
            int i = 0;

            try
            {
                #region Report Titel
                sheet.Range["A1:O2"].CellStyle.Font.FontName = "Calibri";
                sheet.Range["A1:O2"].CellStyle.Font.Size = 12f;
                sheet.Range["A1:O2"].CellStyle.Font.Bold = true;
                sheet.Range["A1:O2"].RowHeight = 20;
                sheet.Range["A1:O2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                sheet.Range["A1:O2"].VerticalAlignment = ExcelVAlign.VAlignTop;
                sheet.Range["A1:O1"].CellStyle.Color = System.Drawing.Color.LightYellow;
                sheet.Range["A1:O1"].RowHeight = 15;
                sheet.Range["A1:O1"].Merge();
                sheet.Range["A1"].Text = strHeader;

                #endregion

                sheet.Range["A4"].FreezePanes();

                ROW = 3;

                GetDataOfBankInfo(fromdate, todate, strSiteId, out dsLocal);

                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    #region Details Heading

                    sheet.Range["A" + ROW + ":O" + ROW].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    sheet.Range["A" + ROW + ":O" + ROW].VerticalAlignment = ExcelVAlign.VAlignCenter;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.FontName = "Calibri";
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.Size = 9f;
                    sheet.Range["A" + ROW + ":O" + ROW].WrapText = true;
                    sheet.Range["A" + ROW + ":O" + ROW].RowHeight = 36;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Interior.Color = LightBlue_Color;
                    sheet.Range["A" + ROW + ":O" + ROW].VerticalAlignment = ExcelVAlign.VAlignTop;

                    sheet.Range["A" + ROW].Text = "Location";
                    sheet.Range["A" + ROW].ColumnWidth = 10;
                    sheet.Range["B" + ROW].Text = "Bank Name";
                    sheet.Range["B" + ROW].ColumnWidth = 20;
                    sheet.Range["C" + ROW].Text = "Bank Account Name";
                    sheet.Range["C" + ROW].ColumnWidth = 15;
                    sheet.Range["D" + ROW].Text = "Bank Address";
                    sheet.Range["D" + ROW].ColumnWidth = 20;
                    sheet.Range["E" + ROW].Text = "Bank Code";
                    sheet.Range["E" + ROW].ColumnWidth = 10;
                    sheet.Range["F" + ROW].Text = "Branch Code";
                    sheet.Range["F" + ROW].ColumnWidth = 10;
                    sheet.Range["G" + ROW].Text = "Account Code";
                    sheet.Range["G" + ROW].ColumnWidth = 10;
                    sheet.Range["H" + ROW].Text = "SWIFT Code";
                    sheet.Range["H" + ROW].ColumnWidth = 10;
                    sheet.Range["I" + ROW].Text = "SORT Code";
                    sheet.Range["I" + ROW].ColumnWidth = 10;
                    sheet.Range["J" + ROW].Text = "IBANs Code";
                    sheet.Range["J" + ROW].ColumnWidth = 10;
                    sheet.Range["K" + ROW].Text = "BSB Code";
                    sheet.Range["K" + ROW].ColumnWidth = 10;
                    sheet.Range["L" + ROW].Text = "Name of the Correspondent Bank";
                    sheet.Range["L" + ROW].ColumnWidth = 15;
                    sheet.Range["M" + ROW].Text = "Address of the Correspendent Bank";
                    sheet.Range["M" + ROW].ColumnWidth = 20;
                    sheet.Range["N" + ROW].Text = "Authorised Signatory";
                    sheet.Range["N" + ROW].ColumnWidth = 15;
                    sheet.Range["O" + ROW].Text = "Remarks";
                    sheet.Range["O" + ROW].ColumnWidth = 20;                   

                    #endregion

                    ROW++;

                    i = 0;

                    while (i < dsLocal.Tables[0].Rows.Count)
                    {

                        sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.FontName = "Calibri";
                        sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.Size = 9f;
                        sheet.Range["A" + ROW + ":O" + ROW].WrapText = true;
                        sheet.Range["A" + ROW + ":O" + ROW].RowHeight = 13;
                        sheet.Range["A" + ROW + ":O" + ROW].HorizontalAlignment = ExcelHAlign.HAlignLeft;                       
                        sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Hair;
                        sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                        sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

                        sheet["A" + ROW].Text = dsLocal.Tables[0].Rows[i]["Location"].ToString();
                        sheet["B" + ROW].Text = dsLocal.Tables[0].Rows[i]["BankName"].ToString();
                        sheet["C" + ROW].Text = dsLocal.Tables[0].Rows[i]["BankAccountName"].ToString();
                        sheet["D" + ROW].Text = dsLocal.Tables[0].Rows[i]["BankAddress"].ToString();
                        sheet["E" + ROW].Text = dsLocal.Tables[0].Rows[i]["BankCode"].ToString();
                        sheet["F" + ROW].Text = dsLocal.Tables[0].Rows[i]["BranchCode"].ToString();
                        sheet["G" + ROW].Text = dsLocal.Tables[0].Rows[i]["AccountCode"].ToString();
                        sheet["H" + ROW].Text = dsLocal.Tables[0].Rows[i]["SWIFTCode"].ToString();
                        sheet["I" + ROW].Text = dsLocal.Tables[0].Rows[i]["SORTCode"].ToString();
                        sheet["J" + ROW].Text = dsLocal.Tables[0].Rows[i]["IBANsCode"].ToString();
                        sheet["K" + ROW].Text = dsLocal.Tables[0].Rows[i]["BSBCode"].ToString();
                        sheet["L" + ROW].Text = dsLocal.Tables[0].Rows[i]["CorrespondentBankName"].ToString();
                        sheet["M" + ROW].Text = dsLocal.Tables[0].Rows[i]["CorrespendentBankAddress"].ToString();
                        sheet["N" + ROW].Text = dsLocal.Tables[0].Rows[i]["NameTitle"].ToString() + " " + dsLocal.Tables[0].Rows[i]["FirstName"].ToString() + " " + dsLocal.Tables[0].Rows[i]["LastName"].ToString();
                        sheet["O" + ROW].Text = dsLocal.Tables[0].Rows[i]["Remarks"].ToString();                     

                        i++;
                        ROW++;

                    }

                }
                else
                {
                    sheet.Range["A" + ROW + ":O" + ROW].Text = "No Data Available................";
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.FontName = "Tahoma";
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.Size = 8f;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.White;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Interior.Color = System.Drawing.Color.Red;
                    sheet.Range["A" + ROW + ":O" + ROW].Merge();
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":O" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    //ROW++;
                }


                ////Protecting Worksheet using Password
                //sheet.UsedRange.CellStyle.Locked = true;
                //sheet.Protect(bplib.clsWebLib.REPORT_PASSWORD.ToString());

                //optional - set a border during print
                //sheet.UsedRange.BorderAround(ExcelLineStyle.Medium, ExcelKnownColors.Black);

                //Setting Page format - Actual page set to be used to all reports must 
                sheet.PageSetup.TopMargin = .17;
                sheet.PageSetup.BottomMargin = .24;
                sheet.PageSetup.FooterMargin = .17;
                sheet.PageSetup.LeftFooter = "&\"Times New Roman\"&06" + "Printing Date : " + System.DateTime.Now.ToString("dd-MMM-yyyy");
                sheet.PageSetup.RightFooter = "&\"Times New Roman\"&06" + "Page &P of &N";
                sheet.PageSetup.LeftMargin = .17;
                sheet.PageSetup.RightMargin = .17;
                sheet.PageSetup.Orientation = ExcelPageOrientation.Landscape;
                sheet.PageSetup.FitToPagesTall = 0;
                sheet.PageSetup.FitToPagesWide = 1;
                sheet.PageSetup.PrintTitleRows = "$1:$1";
                //---------------------------
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsLocal = null;
            }
        }//EOF
        private DataTable GetDataOfBankInfo(string fromdate, string todate, string strUserRef, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = @"Select A.SystemID, A.Location, A.BankName, A.BankAccountName, A.BankAddress, 
                A.BankCode, A.BranchCode, A.AccountCode, A.SWIFTCode, A.SORTCode, A.IBANsCode, A.BSBCode,
                A.CorrespondentBankName, A.CorrespendentBankAddress, A.AuthorisedSignatoryID, A.Remarks,
                B.NameTitle, B.FirstName, B.MiddleName, B.LastName
                From tblBankData A
                Left Outer Join tblEmployeeMaster B On A.AuthorisedSignatoryID=B.EmployeeCode
                Where A.UpdateOn Between '" + fromdate + "' and '" + todate + @"'
                Order By A.Location,A.SystemID";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");

                return dsRef.Tables[0];

            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF

        #endregion

        #region [PWOMS203] -Lease Agreement Info.XLSX
        public void LeaseAgreementInfoDataXls(string fromdate, string todate, string strSiteId, string strHeader, ref Syncfusion.XlsIO.IWorksheet sheet)
        {
            System.Data.DataSet dsLocal = null;
            string hex = "#FFDDEBF7";
            System.Drawing.Color LightBlue_Color = System.Drawing.ColorTranslator.FromHtml(hex);
            int ROW = 1;
            int i = 0;

            try
            {
                #region Report Titel
                sheet.Range["A1:P2"].CellStyle.Font.FontName = "Calibri";
                sheet.Range["A1:P2"].CellStyle.Font.Size = 12f;
                sheet.Range["A1:P2"].CellStyle.Font.Bold = true;
                sheet.Range["A1:P2"].RowHeight = 20;
                sheet.Range["A1:P2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                sheet.Range["A1:P2"].VerticalAlignment = ExcelVAlign.VAlignTop;
                sheet.Range["A1:P1"].CellStyle.Color = System.Drawing.Color.LightYellow;
                sheet.Range["A1:P1"].RowHeight = 15;
                sheet.Range["A1:P1"].Merge();
                sheet.Range["A1"].Text = strHeader;

                #endregion

                sheet.Range["A4"].FreezePanes();

                ROW = 3;

                GetDataOfLeaseAgreementInfo(fromdate, todate, strSiteId, out dsLocal);

                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    #region Details Heading

                    sheet.Range["A" + ROW + ":P" + ROW].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    sheet.Range["A" + ROW + ":P" + ROW].VerticalAlignment = ExcelVAlign.VAlignCenter;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.FontName = "Calibri";
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.Size = 9f;
                    sheet.Range["A" + ROW + ":P" + ROW].WrapText = true;
                    sheet.Range["A" + ROW + ":P" + ROW].RowHeight = 36;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Interior.Color = LightBlue_Color;
                    sheet.Range["A" + ROW + ":P" + ROW].VerticalAlignment = ExcelVAlign.VAlignTop;

                    sheet.Range["A" + ROW].Text = "Location";
                    sheet.Range["A" + ROW].ColumnWidth = 10;
                    sheet.Range["B" + ROW].Text = "Office Address";
                    sheet.Range["B" + ROW].ColumnWidth = 20;
                    sheet.Range["C" + ROW].Text = "Lettable area (sq. m)";
                    sheet.Range["C" + ROW].ColumnWidth = 15;
                    sheet.Range["D" + ROW].Text = "Monthly Rental Fee";
                    sheet.Range["D" + ROW].ColumnWidth = 20;
                    sheet.Range["E" + ROW].Text = "Monthly Service Fee";
                    sheet.Range["E" + ROW].ColumnWidth = 10;
                    sheet.Range["F" + ROW].Text = "Start Date of the Lease";
                    sheet.Range["F" + ROW].ColumnWidth = 10;
                    sheet.Range["G" + ROW].Text = "End Date of the Lease";
                    sheet.Range["G" + ROW].ColumnWidth = 10;
                    sheet.Range["H" + ROW].Text = "Min. Duration of the Lease (Month)";
                    sheet.Range["H" + ROW].ColumnWidth = 10;
                    sheet.Range["I" + ROW].Text = "Name of the Real Estate Agent";
                    sheet.Range["I" + ROW].ColumnWidth = 10;
                    sheet.Range["J" + ROW].Text = "Email of the Real Estate Agent";
                    sheet.Range["J" + ROW].ColumnWidth = 10;
                    sheet.Range["K" + ROW].Text = "Phone # of the Real Estate Agent";
                    sheet.Range["K" + ROW].ColumnWidth = 10;
                    sheet.Range["L" + ROW].Text = "Name of the Landlord";
                    sheet.Range["L" + ROW].ColumnWidth = 15;
                    sheet.Range["M" + ROW].Text = "Email of the Landlord";
                    sheet.Range["M" + ROW].ColumnWidth = 20;
                    sheet.Range["N" + ROW].Text = "Phone # of the Landlord";
                    sheet.Range["N" + ROW].ColumnWidth = 15;
                    sheet.Range["O" + ROW].Text = "Other terms & conditions";
                    sheet.Range["O" + ROW].ColumnWidth = 20;
                    sheet.Range["P" + ROW].Text = "Employee in charge";
                    sheet.Range["P" + ROW].ColumnWidth = 20;

                    #endregion

                    ROW++;

                    i = 0;

                    while (i < dsLocal.Tables[0].Rows.Count)
                    {

                        sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.FontName = "Calibri";
                        sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.Size = 9f;
                        sheet.Range["A" + ROW + ":P" + ROW].WrapText = true;
                        sheet.Range["A" + ROW + ":P" + ROW].RowHeight = 13;
                        sheet.Range["A" + ROW + ":P" + ROW].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        sheet.Range["C" + ROW + ":E" + ROW].HorizontalAlignment = ExcelHAlign.HAlignRight;
                        sheet.Range["H" + ROW + ":H" + ROW].HorizontalAlignment = ExcelHAlign.HAlignRight;
                        sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Hair;
                        sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                        sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

                        sheet["A" + ROW].Text = dsLocal.Tables[0].Rows[i]["Location"].ToString();
                        sheet["B" + ROW].Text = dsLocal.Tables[0].Rows[i]["OfficeAddress"].ToString();
                        sheet["C" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["LettableAreaSQM"].ToString()));
                        sheet["D" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["MonthlyRentalFee"].ToString()));
                        sheet["E" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["MonthlyServiceFee"].ToString()));
                        sheet["F" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["LeaseStartDate"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["G" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["LeaseEndDate"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["H" + ROW].Number = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[i]["LeaseMinDurationMonth"].ToString()));
                        sheet["I" + ROW].Text = dsLocal.Tables[0].Rows[i]["AgentName"].ToString();
                        sheet["J" + ROW].Text = dsLocal.Tables[0].Rows[i]["AgentEmail"].ToString();
                        sheet["K" + ROW].Text = dsLocal.Tables[0].Rows[i]["AgentPhone"].ToString();
                        sheet["L" + ROW].Text = dsLocal.Tables[0].Rows[i]["LandlordName"].ToString();
                        sheet["M" + ROW].Text = dsLocal.Tables[0].Rows[i]["LandlordEmail"].ToString();
                        sheet["N" + ROW].Text = dsLocal.Tables[0].Rows[i]["LandlordPhone"].ToString();
                        sheet["O" + ROW].Text = dsLocal.Tables[0].Rows[i]["OtherTermsConditions"].ToString();
                        sheet["P" + ROW].Text = dsLocal.Tables[0].Rows[i]["NameTitle"].ToString() + " " + dsLocal.Tables[0].Rows[i]["FirstName"].ToString() + " " + dsLocal.Tables[0].Rows[i]["LastName"].ToString();                        

                        i++;
                        ROW++;

                    }

                }
                else
                {
                    sheet.Range["A" + ROW + ":P" + ROW].Text = "No Data Available................";
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.FontName = "Tahoma";
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.Size = 8f;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.White;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Interior.Color = System.Drawing.Color.Red;
                    sheet.Range["A" + ROW + ":P" + ROW].Merge();
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":P" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    //ROW++;
                }


                ////Protecting Worksheet using Password
                //sheet.UsedRange.CellStyle.Locked = true;
                //sheet.Protect(bplib.clsWebLib.REPORT_PASSWORD.ToString());

                //optional - set a border during print
                //sheet.UsedRange.BorderAround(ExcelLineStyle.Medium, ExcelKnownColors.Black);

                //Setting Page format - Actual page set to be used to all reports must 
                sheet.PageSetup.TopMargin = .17;
                sheet.PageSetup.BottomMargin = .24;
                sheet.PageSetup.FooterMargin = .17;
                sheet.PageSetup.LeftFooter = "&\"Times New Roman\"&06" + "Printing Date : " + System.DateTime.Now.ToString("dd-MMM-yyyy");
                sheet.PageSetup.RightFooter = "&\"Times New Roman\"&06" + "Page &P of &N";
                sheet.PageSetup.LeftMargin = .17;
                sheet.PageSetup.RightMargin = .17;
                sheet.PageSetup.Orientation = ExcelPageOrientation.Landscape;
                sheet.PageSetup.FitToPagesTall = 0;
                sheet.PageSetup.FitToPagesWide = 1;
                sheet.PageSetup.PrintTitleRows = "$1:$1";
                //---------------------------
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsLocal = null;
            }
        }//EOF
        private DataTable GetDataOfLeaseAgreementInfo(string fromdate, string todate, string strUserRef, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = @"Select A.SystemID, A.Location, A.OfficeAddress, A.LettableAreaSQM, A.MonthlyRentalFee, 
                A.MonthlyServiceFee, A.LeaseStartDate, A.LeaseEndDate, A.LeaseMinDurationMonth, A.AgentName,A.AgentEmail,
                A.AgentPhone, A.LandlordName, A.LandlordEmail, A.LandlordPhone, A.OtherTermsConditions, A.EmpInchargeID,
                B.NameTitle, B.FirstName, B.MiddleName, B.LastName
                from tblLeaseAgreement A
                Left Outer Join tblEmployeeMaster B On A.EmpInchargeID=B.EmployeeCode
                Where A.UpdateOn Between '" + fromdate + "' and '" + todate + @"'
                Order By A.Location,A.SystemID";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");

                return dsRef.Tables[0];

            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF

        #endregion 

        #region [PWOMS204] -Funding Info.XLSX
        public void FundingInfoDataXls(string fromdate, string todate, string strSiteId, string strHeader, ref Syncfusion.XlsIO.IWorksheet sheet)
        {
            System.Data.DataSet dsLocal = null;
            string hex = "#FFDDEBF7";
            System.Drawing.Color LightBlue_Color = System.Drawing.ColorTranslator.FromHtml(hex);
            int ROW = 1;
            int i = 0;

            try
            {
                #region Report Titel
                sheet.Range["A1:I2"].CellStyle.Font.FontName = "Calibri";
                sheet.Range["A1:I2"].CellStyle.Font.Size = 12f;
                sheet.Range["A1:I2"].CellStyle.Font.Bold = true;
                sheet.Range["A1:I2"].RowHeight = 20;
                sheet.Range["A1:I2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                sheet.Range["A1:I2"].VerticalAlignment = ExcelVAlign.VAlignTop;
                sheet.Range["A1:I1"].CellStyle.Color = System.Drawing.Color.LightYellow;
                sheet.Range["A1:I1"].RowHeight = 15;
                sheet.Range["A1:I1"].Merge();
                sheet.Range["A1"].Text = strHeader;

                #endregion

                sheet.Range["A4"].FreezePanes();

                ROW = 3;

                GetDataOfFundingInfo(fromdate, todate, strSiteId, out dsLocal);

                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    #region Details Heading

                    sheet.Range["A" + ROW + ":I" + ROW].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    sheet.Range["A" + ROW + ":I" + ROW].VerticalAlignment = ExcelVAlign.VAlignCenter;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.FontName = "Calibri";
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.Size = 9f;
                    sheet.Range["A" + ROW + ":I" + ROW].WrapText = true;
                    sheet.Range["A" + ROW + ":I" + ROW].RowHeight = 36;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Interior.Color = LightBlue_Color;
                    sheet.Range["A" + ROW + ":I" + ROW].VerticalAlignment = ExcelVAlign.VAlignTop;

                    sheet.Range["A" + ROW].Text = "Location";
                    sheet.Range["A" + ROW].ColumnWidth = 10;
                    sheet.Range["B" + ROW].Text = "Name of the Scheme";
                    sheet.Range["B" + ROW].ColumnWidth = 20;
                    sheet.Range["C" + ROW].Text = "Name of the Govt Dept";
                    sheet.Range["C" + ROW].ColumnWidth = 15;
                    sheet.Range["D" + ROW].Text = "Email of the government official";
                    sheet.Range["D" + ROW].ColumnWidth = 15;
                    sheet.Range["E" + ROW].Text = "Phone # of the government official";
                    sheet.Range["E" + ROW].ColumnWidth = 12;
                    sheet.Range["F" + ROW].Text = "Date of application";
                    sheet.Range["F" + ROW].ColumnWidth = 11;
                    sheet.Range["G" + ROW].Text = "Current Status";
                    sheet.Range["G" + ROW].ColumnWidth = 10;
                    sheet.Range["H" + ROW].Text = "Date of status update";
                    sheet.Range["H" + ROW].ColumnWidth = 11;
                    sheet.Range["I" + ROW].Text = "Employee in charge";
                    sheet.Range["I" + ROW].ColumnWidth = 12;                    

                    #endregion

                    ROW++;

                    i = 0;

                    while (i < dsLocal.Tables[0].Rows.Count)
                    {

                        sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.FontName = "Calibri";
                        sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.Size = 9f;
                        sheet.Range["A" + ROW + ":I" + ROW].WrapText = true;
                        sheet.Range["A" + ROW + ":I" + ROW].RowHeight = 13;
                        sheet.Range["A" + ROW + ":I" + ROW].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Hair;
                        sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                        sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

                        sheet["A" + ROW].Text = dsLocal.Tables[0].Rows[i]["Location"].ToString();
                        sheet["B" + ROW].Text = dsLocal.Tables[0].Rows[i]["SchemeName"].ToString();
                        sheet["C" + ROW].Text = dsLocal.Tables[0].Rows[i]["GovtDeptName"].ToString();
                        sheet["D" + ROW].Text = dsLocal.Tables[0].Rows[i]["GovtOfficialEmail"].ToString();
                        sheet["E" + ROW].Text = dsLocal.Tables[0].Rows[i]["GovtOfficialPhone"].ToString();
                        sheet["F" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["ApplicationDate"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["G" + ROW].Text = dsLocal.Tables[0].Rows[i]["CurrentStatus"].ToString();
                        sheet["H" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["StatusUpdateDate"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["I" + ROW].Text = dsLocal.Tables[0].Rows[i]["NameTitle"].ToString() + " " + dsLocal.Tables[0].Rows[i]["FirstName"].ToString() + " " + dsLocal.Tables[0].Rows[i]["LastName"].ToString();

                        i++;
                        ROW++;

                    }

                }
                else
                {
                    sheet.Range["A" + ROW + ":I" + ROW].Text = "No Data Available................";
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.FontName = "Tahoma";
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.Size = 8f;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.White;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Interior.Color = System.Drawing.Color.Red;
                    sheet.Range["A" + ROW + ":I" + ROW].Merge();
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":I" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    //ROW++;
                }


                ////Protecting Worksheet using Password
                //sheet.UsedRange.CellStyle.Locked = true;
                //sheet.Protect(bplib.clsWebLib.REPORT_PASSWORD.ToString());

                //optional - set a border during print
                //sheet.UsedRange.BorderAround(ExcelLineStyle.Medium, ExcelKnownColors.Black);

                //Setting Page format - Actual page set to be used to all reports must 
                sheet.PageSetup.TopMargin = .17;
                sheet.PageSetup.BottomMargin = .24;
                sheet.PageSetup.FooterMargin = .17;
                sheet.PageSetup.LeftFooter = "&\"Times New Roman\"&06" + "Printing Date : " + System.DateTime.Now.ToString("dd-MMM-yyyy");
                sheet.PageSetup.RightFooter = "&\"Times New Roman\"&06" + "Page &P of &N";
                sheet.PageSetup.LeftMargin = .17;
                sheet.PageSetup.RightMargin = .17;
                sheet.PageSetup.Orientation = ExcelPageOrientation.Landscape;
                sheet.PageSetup.FitToPagesTall = 0;
                sheet.PageSetup.FitToPagesWide = 1;
                sheet.PageSetup.PrintTitleRows = "$1:$1";
                //---------------------------
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsLocal = null;
            }
        }//EOF
        private DataTable GetDataOfFundingInfo(string fromdate, string todate, string strUserRef, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = @"Select A.SystemID, A.Location, A.SchemeName, A.GovtDeptName, A.GovtOfficialEmail, 
                A.GovtOfficialPhone, A.ApplicationDate, A.CurrentStatus, A.StatusUpdateDate, A.EmpInchargeID,
                B.NameTitle, B.FirstName, B.MiddleName, B.LastName
                From tblFundingInfo A
                Left Outer Join tblEmployeeMaster B On A.EmpInchargeID=B.EmployeeCode
                Where A.UpdateOn Between '" + fromdate + "' and '" + todate + @"'
                Order By A.Location,A.SystemID";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");

                return dsRef.Tables[0];

            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF

        #endregion

        #region [PWOMS205] -BusinessReg Info.XLSX
        public void BusinessRegInfoDataXls(string fromdate, string todate, string strSiteId, string strHeader, ref Syncfusion.XlsIO.IWorksheet sheet)
        {
            System.Data.DataSet dsLocal = null;
            string hex = "#FFDDEBF7";
            System.Drawing.Color LightBlue_Color = System.Drawing.ColorTranslator.FromHtml(hex);
            int ROW = 1;
            int i = 0;

            try
            {
                #region Report Titel
                sheet.Range["A1:E2"].CellStyle.Font.FontName = "Calibri";
                sheet.Range["A1:E2"].CellStyle.Font.Size = 12f;
                sheet.Range["A1:E2"].CellStyle.Font.Bold = true;
                sheet.Range["A1:E2"].RowHeight = 20;
                sheet.Range["A1:E2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                sheet.Range["A1:E2"].VerticalAlignment = ExcelVAlign.VAlignTop;
                sheet.Range["A1:E1"].CellStyle.Color = System.Drawing.Color.LightYellow;
                sheet.Range["A1:E1"].RowHeight = 15;
                sheet.Range["A1:E1"].Merge();
                sheet.Range["A1"].Text = strHeader;

                #endregion

                sheet.Range["A4"].FreezePanes();

                ROW = 3;

                GetDataOfBusinessRegInfo(fromdate, todate, strSiteId, out dsLocal);

                if (dsLocal.Tables[0].Rows.Count > 0)
                {
                    #region Details Heading

                    sheet.Range["A" + ROW + ":E" + ROW].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                    sheet.Range["A" + ROW + ":E" + ROW].VerticalAlignment = ExcelVAlign.VAlignCenter;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.FontName = "Calibri";
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.Size = 9f;
                    sheet.Range["A" + ROW + ":E" + ROW].WrapText = true;
                    sheet.Range["A" + ROW + ":E" + ROW].RowHeight = 36;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Interior.Color = LightBlue_Color;
                    sheet.Range["A" + ROW + ":E" + ROW].VerticalAlignment = ExcelVAlign.VAlignTop;

                    sheet.Range["A" + ROW].Text = "Location";
                    sheet.Range["A" + ROW].ColumnWidth = 10;
                    sheet.Range["B" + ROW].Text = "BR #";
                    sheet.Range["B" + ROW].ColumnWidth = 15;
                    sheet.Range["C" + ROW].Text = "Expiry Date";
                    sheet.Range["C" + ROW].ColumnWidth = 11;
                    sheet.Range["D" + ROW].Text = "Registered Address";
                    sheet.Range["D" + ROW].ColumnWidth = 25;
                    sheet.Range["E" + ROW].Text = "Employee in charge";
                    sheet.Range["E" + ROW].ColumnWidth = 12;                   

                    #endregion

                    ROW++;

                    i = 0;

                    while (i < dsLocal.Tables[0].Rows.Count)
                    {

                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.FontName = "Calibri";
                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.Size = 9f;
                        sheet.Range["A" + ROW + ":E" + ROW].WrapText = true;
                        sheet.Range["A" + ROW + ":E" + ROW].RowHeight = 13;
                        sheet.Range["A" + ROW + ":E" + ROW].HorizontalAlignment = ExcelHAlign.HAlignLeft;
                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Hair;
                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;

                        sheet["A" + ROW].Text = dsLocal.Tables[0].Rows[i]["Location"].ToString();
                        sheet["B" + ROW].Text = dsLocal.Tables[0].Rows[i]["BRNumber"].ToString();
                        sheet["C" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["ExpiryDate"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["D" + ROW].Text = dsLocal.Tables[0].Rows[i]["RegisteredAddress"].ToString();
                        sheet["E" + ROW].Text = dsLocal.Tables[0].Rows[i]["NameTitle"].ToString() + " " + dsLocal.Tables[0].Rows[i]["FirstName"].ToString() + " " + dsLocal.Tables[0].Rows[i]["LastName"].ToString();

                        i++;
                        ROW++;

                    }

                }
                else
                {
                    sheet.Range["A" + ROW + ":E" + ROW].Text = "No Data Available................";
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.FontName = "Tahoma";
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.Size = 8f;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.Bold = true;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Font.Color = Syncfusion.XlsIO.ExcelKnownColors.White;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Interior.Color = System.Drawing.Color.Red;
                    sheet.Range["A" + ROW + ":E" + ROW].Merge();
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Thin;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                    sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;
                    //ROW++;
                }


                ////Protecting Worksheet using Password
                //sheet.UsedRange.CellStyle.Locked = true;
                //sheet.Protect(bplib.clsWebLib.REPORT_PASSWORD.ToString());

                //optional - set a border during print
                //sheet.UsedRange.BorderAround(ExcelLineStyle.Medium, ExcelKnownColors.Black);

                //Setting Page format - Actual page set to be used to all reports must 
                sheet.PageSetup.TopMargin = .17;
                sheet.PageSetup.BottomMargin = .24;
                sheet.PageSetup.FooterMargin = .17;
                sheet.PageSetup.LeftFooter = "&\"Times New Roman\"&06" + "Printing Date : " + System.DateTime.Now.ToString("dd-MMM-yyyy");
                sheet.PageSetup.RightFooter = "&\"Times New Roman\"&06" + "Page &P of &N";
                sheet.PageSetup.LeftMargin = .17;
                sheet.PageSetup.RightMargin = .17;
                sheet.PageSetup.Orientation = ExcelPageOrientation.Landscape;
                sheet.PageSetup.FitToPagesTall = 0;
                sheet.PageSetup.FitToPagesWide = 1;
                sheet.PageSetup.PrintTitleRows = "$1:$1";
                //---------------------------
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsLocal = null;
            }
        }//EOF
        private DataTable GetDataOfBusinessRegInfo(string fromdate, string todate, string strUserRef, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = @"Select A.SystemID, A.Location, A.BRNumber, A.ExpiryDate, A.RegisteredAddress, A.EmpInchargeID, 
                B.NameTitle, B.FirstName, B.MiddleName, B.LastName
                From tblBizRegInfo A
                Left Outer Join tblEmployeeMaster B On A.EmpInchargeID=B.EmployeeCode
                Where A.UpdateOn Between '" + fromdate + "' and '" + todate + @"'
                Order By A.Location,A.SystemID";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");

                return dsRef.Tables[0];

            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF

        #endregion

        #region Common Function

        public void GetDataOfCompanyInfo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCompanyInfo where SiteId='" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//eof
        public void GetDataofTermsAndCondition(string strDocType, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = "Select * From tblSMSTermsAndCondition Where DocType='" + strDocType + "' And SiteID='" + strSiteId + "' Order by RowID";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF

        public void SaveData(ref System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.SaveDataSetThroughAdapter(ref dsRef, false, "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }

        } // EOF 
 
        #endregion
    }
}