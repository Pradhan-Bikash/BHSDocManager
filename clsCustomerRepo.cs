using Syncfusion.XlsIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace PWOMS
{
	public class clsCustomerRepo
	{
        #region [PWOMS207] -Customer Info.XLSX
        public void CustomerInfoDataXls(string fromdate, string todate, string strSiteId, string strHeader, ref Syncfusion.XlsIO.IWorksheet sheet)
        {
            System.Data.DataSet dsLocal = null;
            string hex = "#FFDDEBF7";
            System.Drawing.Color LightBlue_Color = System.Drawing.ColorTranslator.FromHtml(hex);
            int ROW = 1;
            int i = 0;

            try
            {
                #region Report Titel
                sheet.Range["A1:K2"].CellStyle.Font.FontName = "Calibri";
                sheet.Range["A1:K2"].CellStyle.Font.Size = 12f;
                sheet.Range["A1:K2"].CellStyle.Font.Bold = true;
                sheet.Range["A1:K2"].RowHeight = 20;
                sheet.Range["A1:K2"].HorizontalAlignment = ExcelHAlign.HAlignCenter;
                sheet.Range["A1:K2"].VerticalAlignment = ExcelVAlign.VAlignTop;
                sheet.Range["A1:K2"].CellStyle.Color = System.Drawing.Color.LightYellow;
                sheet.Range["A1:K2"].RowHeight = 15;
                sheet.Range["A1:K2"].Merge();
                sheet.Range["A1"].Text = strHeader;

                #endregion

                sheet.Range["A4"].FreezePanes();

                ROW = 3;

                GetDataOfCustomerInfo(fromdate, todate, strSiteId, out dsLocal);

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


                    sheet.Range["A" + ROW].Text = "CustomerID";
                    sheet.Range["A" + ROW].ColumnWidth = 10;
                    sheet.Range["B" + ROW].Text = "Customer Name";
                    sheet.Range["B" + ROW].ColumnWidth = 20;
                    sheet.Range["C" + ROW].Text = "Customer Type";
                    sheet.Range["C" + ROW].ColumnWidth = 11;
                    sheet.Range["D" + ROW].Text = "Register Date";
                    sheet.Range["D" + ROW].ColumnWidth = 12;
                    sheet.Range["E" + ROW].Text = "Phone Number #";
                    sheet.Range["E" + ROW].ColumnWidth = 11;
                    

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
                        //sheet.Range["J" + ROW + ":G" + ROW].HorizontalAlignment = ExcelHAlign.HAlignRight;
                        //sheet.Range["M" + ROW + ":G" + ROW].HorizontalAlignment = ExcelHAlign.HAlignRight;
                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders.LineStyle = ExcelLineStyle.Hair;
                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalDown].ShowDiagonalLine = false;
                        sheet.Range["A" + ROW + ":E" + ROW].CellStyle.Borders[ExcelBordersIndex.DiagonalUp].ShowDiagonalLine = false;


                        sheet["A" + ROW].Text = dsLocal.Tables[0].Rows[i]["CustomerId"].ToString();
                        sheet["B" + ROW].Text = dsLocal.Tables[0].Rows[i]["CustomerName"].ToString();
                        sheet["C" + ROW].Text = dsLocal.Tables[0].Rows[i]["CustomerType"].ToString();
                        sheet["D" + ROW].Text = bplib.clsWebLib.makeBaseBlank(bplib.clsWebLib.DateData_DBToApp(dsLocal.Tables[0].Rows[i]["RegisterDate"].ToString(), bplib.clsWebLib.DB_DATE_FORMAT).ToString("dd-MMM-yyyy"));
                        sheet["E" + ROW].Text = dsLocal.Tables[0].Rows[i]["Phone"].ToString();

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
        private DataTable GetDataOfCustomerInfo(string fromdate, string todate, string strUserRef, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = @"Select * from tbl_customer
                Where RegisterDate Between '" + fromdate + "' and '" + todate + @"'
                Order By CustomerID";

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
    }
}