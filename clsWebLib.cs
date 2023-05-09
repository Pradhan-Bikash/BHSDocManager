using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;


namespace bplib
{
    public static class clsWebLib
    {


        #region customized function

        public static readonly string DB_DATE_FORMAT = "yyyy-MM-dd";
        public static readonly string STD_DATE_FORMAT = "yyyy-MM-dd";
        public static readonly string GROUPID = "G2014-3";
        public static string BappsMultiLineStringBuilder(string str)
        {
            int i = 0;
            char[] param = { '\n' };
            char[] lineEnd = { '\r' };
            string ss = "";
            if (str.Trim() == "")
            {
                return ("");
            }
            string[] lines = str.Split(param);
            foreach (string s in lines)
            {
                lines[i++] = s.TrimEnd(lineEnd);
            }
            foreach (string line in lines)
            {
                ss = ss + line + "<br>";
            }

            return (ss);

        }//end of function
        public static object RetValidLen(string str, int How_Long_Should_It_Be)
        {

            string removechar = "";
            if (str.Trim() == "")
            {
                return (object)Convert.DBNull;
            }
            removechar = str.Trim();
            removechar = removechar.Replace("'", " ");
            if ((removechar.Trim()).Length > How_Long_Should_It_Be)
            {
                return (object)(removechar.Substring(1, How_Long_Should_It_Be));
            }
            else
            {
                return (object)removechar.Trim();
            }
        }//end of function
        public static object RetValidLen(string str)
        {

            string removechar = "";
            if (str.Trim() == "")
            {
                return (object)Convert.DBNull;
            }
            removechar = str.Trim();
            removechar = removechar.Replace("'", " ");
            ////if ((removechar.Trim()).Length > How_Long_Should_It_Be)
            ////{
            ////    return (object)(removechar.Substring(1, How_Long_Should_It_Be));
            ////}
            ////else
            ////{
            ////    return (object)removechar.Trim();
            ////}
            return (object)removechar.Trim();

        }//end of function
        #endregion

        //Info message box 
        #region Alert + confirm jscript message builder utill
        //Use of the function:
        //------------------------------------------------
        // for Alert

        //			System.Web.UI.Page this_page_ref=this;
        //			bplib.clsWebLib.BappsAlert(ref this_page_ref, "Error !! occurred in data saving process. Please see the log below for details.","bappskey1");
        //			this_page_ref=null;

        // For confirmation in isPostback False part u have rgoster the button with scrip.
        // To activate the delete conformation we need 
        // add the delete button a  new additional attribute.
        // so on cancel it will not allow to post.
        // this following command ha to be added in if not postback 
        // part. ---------- Bappa
        //bplib.clsWebLib.bappsConfirm(ref this.Button_delete,"Are you sure to delete this data");

        public static void BappsOpenDocument(ref System.Web.UI.Page aspxPage, string strFilePath, string strKey)
        {
            string strScript = "<script language=JavaScript>window.open('" + strFilePath + "','',width='500',height='700')</script>";
            if (aspxPage.ClientScript.IsStartupScriptRegistered(strKey) == false)
            {
                aspxPage.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), strKey, strScript);
            }
        }//end of function
        public static void BappsOpenDocumentWide(ref System.Web.UI.Page aspxPage, string strFilePath, string strKey)
        {
            string strScript = "<script language=JavaScript>window.open('" + strFilePath + "','bapps','width='+screen.width+',height='+screen.height*0.90+',location=no,directories=no,menubar=no,toolbar=no,scrollbars=yes,status=yes,resizable=yes,left=0,top=0')</script>";
            if (aspxPage.ClientScript.IsStartupScriptRegistered(strKey) == false)
            {
                aspxPage.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), strKey, strScript);
            }
        }//end of function
        public static void BappsAlert(ref System.Web.UI.Page aspxPage, string strMessage, string strKey)
        {
            string strScript = "<script language=JavaScript>alert('" + strMessage + "')</script>";
            if (aspxPage.ClientScript.IsStartupScriptRegistered(strKey) == false)
            {
                aspxPage.ClientScript.RegisterStartupScript(typeof(System.Web.UI.Page), strKey, strScript);
            }
        }//end of function

        public static void bappsConfirm(ref System.Web.UI.WebControls.Button btn, string strMessage)
        {
            btn.Attributes.Add("onclick", "return confirm('" + strMessage + "');");
        }//end of function

        #endregion

        #region App Date / time type data managment
        //sample use
        //txtModificationDate.Text = bplib.clsutilib.DateData_DBToApp(dsLocal.Tables[0].Rows[0]["ModificationDate"].ToString()).ToString("d");
        //drLocal["ModificationDate"] =bplib.clsutilib.DateData_AppToDB(System.DateTime.Now,clsStartUp.DB_DATE_FORMAT);
        private static bool DateOkCheck(string strdate)
        {
            try
            {
                System.DateTime myDt = System.Convert.ToDateTime(strdate);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
                //
            }
        }// end function

        public static object chk_NullDateData(object dateValue)
        {
            if (DateOkCheck("" + dateValue.ToString()) == false)
            {
                dateValue = "";
            }

            if (("" + dateValue.ToString()) == "")
            {
                System.DateTime dt = new System.DateTime(1901, 1, 1);
                dateValue = (object)dt;
            }
            return (object)dateValue;
        }
        public static System.DateTime AppDateConvert(object dateValue, string input_date_format, string output_date_format)
        {
            string strDate = null;
            dateValue = chk_NullDateData(dateValue);
            strDate = dateValue.ToString();
            if (strDate != "")
            {
                if (input_date_format.Trim() != "")
                {
                    if (output_date_format.Trim() != "")
                    {
                        System.Globalization.DateTimeFormatInfo InputFormat = new System.Globalization.DateTimeFormatInfo();
                        InputFormat.ShortDatePattern = input_date_format;
                        System.DateTime myDt = System.Convert.ToDateTime(strDate, InputFormat);
                        strDate = myDt.ToString(output_date_format);
                    }
                }
            }
            return System.Convert.ToDateTime(strDate);
        }// End of function

        internal static bool GetBoolData(string v)
        {
            throw new NotImplementedException();
        }

        public static System.DateTime DateData_AppToDB(object dateValue, string DB_Level_date_format)
        {
            string strDate = null;
            strDate = dateValue.ToString();
            if (DB_Level_date_format != "")
            {
                // Collecting the user terminal set format 
                System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
                strDate = AppDateConvert(strDate, USER_TERMINAL_DATE_FORMAT.ShortDatePattern.ToString(), DB_Level_date_format).ToString();
            }
            return System.Convert.ToDateTime(strDate);
        }// End of function

        public static System.DateTime DateData_DBToApp(object dateValue)
        {
            string strDate = null;
            strDate = dateValue.ToString();

            System.Globalization.DateTimeFormatInfo myDBDateFormat = new System.Globalization.CultureInfo("en-US", false).DateTimeFormat;
            strDate = DateData_DBToApp(dateValue, myDBDateFormat.ShortDatePattern.ToString()).ToString();
            return System.Convert.ToDateTime(strDate);
        }// End function
        public static System.DateTime DateData_DBToApp(object dateValue, string DB_Level_date_format)
        {
            string strDate = null;
            strDate = dateValue.ToString();
            if (DB_Level_date_format != "")
            {
                // Collecting the user terminal set format 
                System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
                strDate = AppDateConvert(strDate, DB_Level_date_format, USER_TERMINAL_DATE_FORMAT.ShortDatePattern.ToString()).ToString();
            }
            return System.Convert.ToDateTime(strDate);
        }// End of function

        public static String makeBaseBlank(object dateValue)
        {
            System.DateTime dt;
            dt = System.Convert.ToDateTime(dateValue.ToString());
            if (dt.Year == 1901)
            {
                return "";
            }
            else
            {
                return dateValue.ToString();
            }
        }// End of function

        public static string AppSysTimeFormat(object TimeValue)
        {
            string strTime = null;
            strTime = TimeValue.ToString();
            if (strTime != "")
            {
                System.Globalization.DateTimeFormatInfo AppTimeFormat = new System.Globalization.DateTimeFormatInfo();
                AppTimeFormat.ShortTimePattern = "HH:mm:ss";
                System.DateTime dt = System.Convert.ToDateTime(strTime, AppTimeFormat);
                strTime = dt.ToString();
            }
            return (string)strTime;
        } //End function
        public static string getUserDateFormat()
        {
            System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            return USER_TERMINAL_DATE_FORMAT.ShortDatePattern.ToString();
        }
        public static string getUserDateSeparator()
        {
            System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            return USER_TERMINAL_DATE_FORMAT.DateSeparator.ToString();
        }
        #endregion


        #region DataType Checking
        public static bool IsDateOK(string strdate)
        {
            try
            {
                if (strdate.Length != 11)
                {
                    return false;
                }
                if (strdate.Substring(2, 1) != "-" && strdate.Substring(6, 1) != "-")
                {
                    return false;
                }
                System.DateTime myDt = System.Convert.ToDateTime(strdate);
                return true;
            }
            catch (System.Exception ex)
            {
                return false;
            }
            finally
            {
                //
            }
        }// end function



        public static bool IsNumeric(string strNumber)
        {
            Double d;
            System.Globalization.NumberFormatInfo n = new System.Globalization.NumberFormatInfo();
            if (strNumber.Length == 0)
            {
                return false;
            }
            return Double.TryParse(strNumber, System.Globalization.NumberStyles.Float, n, out d);
        } // End Function
        public static string GetNumData(string strNumber)
        {
            double d;
            System.Globalization.NumberFormatInfo n = new System.Globalization.NumberFormatInfo();
            if (strNumber.Trim() == "")
            { return "0"; }
            else if (System.Double.TryParse(strNumber, System.Globalization.NumberStyles.Float, n, out d) == true)
            {
                return strNumber;
            }
            else
            {
                return "0";
            }
        }// end function

        public static string ChkDBNull(ref String FldValue)
        {
            string s;
            if (Convert.IsDBNull(FldValue) == true)
            {
                s = "";
            }
            else
            {
                s = (string)FldValue;
            }
            return s;
        } // End Function

        #endregion

        #region CheckNumeric_Lenchk
        public static bool ChkEntryNumeric(string str, char keyP)
        {
            int x = 0;
            int i;
            int KeyAssci;
            KeyAssci = Convert.ToInt32(keyP);
            if (KeyAssci == 8 || KeyAssci == 13)
            {
                return false;
            }
            if (KeyAssci == 46)
            {
                for (i = 1; i <= str.Length; ++i)
                {
                    x = String.CompareOrdinal(str, i, ".", 0, 1);
                    if (x == 0)
                    {
                        return true;
                    }
                }
            }
            if ((KeyAssci < 48 || KeyAssci > 57) && (KeyAssci != 46))
            {
                return true;
            }

            return false;


        }
        public static bool LenChk(String str, int lenStr, char KeyP)
        {
            if (Convert.ToInt32(KeyP) == 8)
            {
                return false;
            }
            if (str.Length >= lenStr)
            {
                return true;
            }
            return false;
        }
        #endregion

        #region Support function for Ultra

        //Sample use
        //		private void LoadStaticInfo()
        //		{
        //			
        //			string[] EntityTypes = {"CutNo","Pattern","Color","Size"};
        //			BS.clsStylePlan objPLan;
        //			try
        //			{
        //				objPLan = new BS.clsStylePlan();				
        //				ulCboOrder.DataSource = CreateNewTableForArray("Order by", EntityTypes );
        //				
        //				ulCboCritea.DataSource=CreateDataTableForCriteria();
        //			}
        //			catch(System.Exception ex)
        //			{
        //				MessageBox.Show(this, ex.ToString(),"System",MessageBoxButtons.OK, MessageBoxIcon.Error);
        //			}
        //		}//End function



        public static System.Data.DataTable CreateNewTableForArray(string ColumnName, string[] arr_Value)
        {
            System.Data.DataTable dtOut;
            try
            {
                System.Data.DataColumn col;
                dtOut = new System.Data.DataTable();
                col = new System.Data.DataColumn();
                col.DataType = typeof(string);
                col.ColumnName = ColumnName;
                dtOut.Columns.Add(col);

                System.Data.DataRow newrow;
                foreach (string strValue in arr_Value)
                {
                    newrow = dtOut.NewRow();
                    newrow[ColumnName] = strValue;
                    dtOut.Rows.Add(newrow);
                }
                return dtOut;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                dtOut = null;
            }
        }//End function

        //ultraGridFinPlan.DataSource=BS.clsUltraUIManager.CreateNewVirtualTable( dsLocal.Tables[0],"FileEntryID","FileNo","Patern","CmbOrShade","Size","FoldeNo","QtyBook");

        public static System.Data.DataTable CreateNewVirtualTable(System.Data.DataTable dtINPut, params string[] ColumnNames)
        {
            System.Data.DataTable dtOut;
            try
            {
                System.Data.DataColumn col;
                dtOut = new System.Data.DataTable();

                foreach (string colname in ColumnNames)
                {
                    col = new System.Data.DataColumn();
                    col.DataType = dtINPut.Columns[colname].DataType;
                    col.ColumnName = dtINPut.Columns[colname].ColumnName;
                    col.AllowDBNull = dtINPut.Columns[colname].AllowDBNull;
                    dtOut.Columns.Add(col);
                    col = null;
                }

                System.Data.DataRow newrow;
                foreach (System.Data.DataRow row in dtINPut.Rows)
                {
                    newrow = dtOut.NewRow();
                    foreach (string colname in ColumnNames)
                    {
                        newrow[colname] = row[colname];
                    }
                    dtOut.Rows.Add(newrow);
                }

                return dtOut;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                dtOut = null;
            }
        }//End function
        public static System.Data.DataTable CreateNewVirtualTable(params string[] ColumnNames)
        {
            System.Data.DataTable dtOut;
            int i = 0;
            try
            {
                System.Data.DataColumn col;
                dtOut = new System.Data.DataTable();

                foreach (string colname in ColumnNames)
                {
                    col = new System.Data.DataColumn();
                    col.DataType = typeof(System.String);
                    col.MaxLength = 50;
                    col.ColumnName = colname;
                    col.AllowDBNull = true;
                    dtOut.Columns.Add(col);
                    col = null;
                }
                i = dtOut.Columns.Count;
                return dtOut;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                dtOut = null;
            }
        }//End function
        #endregion

        #region Conversion
        public static string convertToDoller(object objInstance)
        {
            double d = 0.0;
            string strValue = "";
            try
            {
                d = Convert.ToDouble(GetNumData(objInstance.ToString().Trim()));
                strValue = String.Format("{0:$#,##0.00}", d);
                return strValue;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                d = 0.0;
                strValue = "";
            }
        }// end function
        #endregion

        #region Ultra settings

        #region WebCombo Setting
        //			public static void WebComboSetting(Infragistics.WebUI.WebCombo.WebCombo webCombo)
        //			{
        //
        //				//webCombo.ExpandEffects.Type = Infragistics.WebUI.WebCombo.ExpandEffectType.Fade;
        //				webCombo.ExpandEffects.Type = Infragistics.WebUI.WebCombo.ExpandEffectType.NotSet;
        //				//webCombo.Width = 300;
        //				webCombo.DropDownLayout.DropdownWidth = 300;
        //				webCombo.DropDownLayout.DropdownHeight = 120;
        //				webCombo.DropDownLayout.AllowColSizing=Infragistics.WebUI.UltraWebGrid.AllowSizing.Free;
        //				webCombo.DropDownLayout.ColWidthDefault=50;
        //				webCombo.DropDownLayout.HeaderStyle.BackgroundImage = "./BlueExplorer.gif";
        //
        //				webCombo.Font.Name="Tahoma";
        //				webCombo.Font.Size=System.Web.UI.WebControls.FontUnit.Point(8);
        //
        //				webCombo.Editable=true;
        //				webCombo.Enabled=true;
        //			
        //				webCombo.JavaScriptFileName= "InfraSup/ig_common/WebGrid3/ig_webcombo3_1.js";
        //				webCombo.JavaScriptFileNameCommon= "InfraSup/ig_common/Scripts/ig_csom.js";
        //				webCombo.DropImage1="InfraSup/ig_common/WebGrid3/ig_cmboDown1.bmp";
        //				webCombo.DropImage2="InfraSup/ig_common/WebGrid3/ig_cmboDown2.bmp";
        //				webCombo.DropImageXP1="InfraSup/ig_common/WebGrid3/ig_cmboDownXP1.bmp";
        //				webCombo.DropImageXP2="InfraSup/ig_common/WebGrid3/ig_cmboDownXP2.bmp";
        //				webCombo.DropDownLayout.JavaScriptFileName="InfraSup/ig_common/WebGrid3/ig_WebGrid.js";
        //				webCombo.DropDownLayout.ImageUrls.ImageDirectory="InfraSup/ig_common/WebGrid3/";
        //			}
        #endregion

        #region WebDate Setting
        //			public static void WebDateChooserSetting(Infragistics.WebUI.WebSchedule.WebDateChooser webDateChooser)
        //			{
        //				//if (!IsPostBack)
        //					webDateChooser.ExpandEffects.Type = Infragistics.WebUI.WebDropDown.ExpandEffectType.Fade;
        //
        //				webDateChooser.AllowNull = false;
        //				webDateChooser.Height= 10;
        //				webDateChooser.Width = 100;
        //				
        //				webDateChooser.DropButton.ImageUrl1="InfraSup/ig_common/webschedule1/igsch_xpblueup.gif";
        //				webDateChooser.DropButton.ImageUrl2="InfraSup/ig_common/webschedule1/igsch_xpbluedn.gif";
        //				webDateChooser.CalendarLayout.FooterFormat = "Today: {00:MM/dd/yyyy}";
        //
        //				webDateChooser.CalendarLayout.DayHeaderStyle.BackgroundImage = "./BlueExplorer.gif";
        //				webDateChooser.CalendarLayout.DayHeaderStyle.BorderStyle =System.Web.UI.WebControls.BorderStyle.Inset;
        //				webDateChooser.CalendarLayout.DayHeaderStyle.Font.Bold = true;
        //
        //				webDateChooser.CalendarLayout.CalendarStyle.BorderColor =System.Drawing.Color.Gainsboro;
        //
        //				webDateChooser.CalendarLayout.TitleStyle.BackgroundImage = "./BlueExplorer.gif";
        //				webDateChooser.CalendarLayout.TitleStyle.Font.Bold = true;
        //
        //				webDateChooser.CalendarLayout.ShowYearDropDown = false;
        //				webDateChooser.CalendarLayout.ShowMonthDropDown = false;
        //
        //				webDateChooser.CalendarLayout.NextPrevStyle.BackgroundImage = "./BlueExplorer.gif";
        //				webDateChooser.CalendarLayout.NextMonthImageUrl = "./btnNext.gif";
        //				webDateChooser.CalendarLayout.PrevMonthImageUrl = "./btnPrev.gif";
        //				webDateChooser.CalendarLayout.NextPrevStyle.BorderColor =System.Drawing.Color.Gainsboro;
        //				webDateChooser.CalendarLayout.NextPrevStyle.Font.Bold=true;
        //				webDateChooser.CalendarLayout.NextPrevStyle.Width =30;
        //
        //				webDateChooser.CalendarLayout.OtherMonthDayStyle.ForeColor =System.Drawing.Color.LightGray;
        //
        //				webDateChooser.CalendarLayout.FooterStyle.BackgroundImage = "./BlueExplorer.gif";
        //				webDateChooser.CalendarLayout.FooterStyle.BorderStyle = System.Web.UI.WebControls.BorderStyle.Inset;
        //				webDateChooser.CalendarLayout.FooterStyle.Font.Italic = true;
        //				webDateChooser.CalendarLayout.FooterStyle.Font.Bold = true;
        //				webDateChooser.CalendarLayout.FooterStyle.ForeColor =System.Drawing.Color.Indigo;
        //
        //				webDateChooser.CalendarLayout.WeekendDayStyle.ForeColor =System.Drawing.Color.Red;
        //				
        //				webDateChooser.CalendarLayout.TodayDayStyle.Font.Italic = true;
        //				webDateChooser.CalendarLayout.TodayDayStyle.Font.Bold = true;
        //				webDateChooser.CalendarLayout.TodayDayStyle.ForeColor = System.Drawing.Color.Indigo;
        //
        //				webDateChooser.Font.Name="Tahoma";
        //				webDateChooser.Font.Size=System.Web.UI.WebControls.FontUnit.Point(8);
        //			
        //				webDateChooser.EditStyle.Font.Name="Tahoma";
        //				webDateChooser.EditStyle.Font.Size=System.Web.UI.WebControls.FontUnit.Point(8);
        //
        //				webDateChooser.CalendarLayout.Calendar.Font.Name="Tahoma";
        //				webDateChooser.CalendarLayout.Calendar.Font.Size=System.Web.UI.WebControls.FontUnit.Point(8);
        //			
        //
        //				webDateChooser.JavaScriptFileName="InfraSup/ig_common/webschedule1/ig_webdropdown.js";
        //				webDateChooser.JavaScriptFileNameCommon="InfraSup/ig_common/scripts/ig_csom.js";
        //				webDateChooser.CalendarJavaScriptFileName="InfraSup/ig_common/webschedule1/ig_calendar.js";
        //	
        //			}
        #endregion

        #region Web Grid Setting
        //public static void SetUltraWebGrid(Infragistics.WebUI.UltraWebGrid.UltraWebGrid UltrWebGrid, bool blnAddNew, bool blnUpdate, bool blnDelete)
        //{
        //    if (blnAddNew == true)
        //    {
        //        UltrWebGrid.DisplayLayout.AllowAddNewDefault = Infragistics.WebUI.UltraWebGrid.AllowAddNew.Yes;
        //        UltrWebGrid.DisplayLayout.AddNewBox.Hidden = false;
        //        //UltrWebGrid.DisplayLayout.AddNewBox.
        //    }
        //    if (blnUpdate == true)
        //    {
        //        UltrWebGrid.DisplayLayout.AllowUpdateDefault = Infragistics.WebUI.UltraWebGrid.AllowUpdate.Yes;
        //    }
        //    if (blnDelete == true)
        //    {
        //        UltrWebGrid.DisplayLayout.AllowDeleteDefault = Infragistics.WebUI.UltraWebGrid.AllowDelete.Yes;
        //    }
        //    UltrWebGrid.Font.Name = "Verdana";
        //    UltrWebGrid.Font.Size = 8;
        //    //new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
        //    UltrWebGrid.DisplayLayout.Bands[0].AllowColumnMoving = Infragistics.WebUI.UltraWebGrid.AllowColumnMoving.NotSet;
        //    UltrWebGrid.DisplayLayout.Bands[0].FooterStyle.BackColor = System.Drawing.Color.White;
        //    UltrWebGrid.DisplayLayout.AddNewBox.ButtonStyle.BackColor = System.Drawing.Color.CornflowerBlue;
        //    UltrWebGrid.DisplayLayout.AddNewBox.Style.BackColor = System.Drawing.Color.WhiteSmoke;
        //    UltrWebGrid.DisplayLayout.AddNewBox.ButtonStyle.Cursor = Infragistics.WebUI.Shared.Cursors.Hand;
        //    UltrWebGrid.DisplayLayout.Bands[0].AddButtonCaption = "Add New";
        //    //UltrWebGrid.DisplayLayout.Bands[0].HeaderStyle.BackgroundImage=@".\Picture\GridLooks.jpg";
        //    UltrWebGrid.DisplayLayout.Bands[0].HeaderStyle.BackColor = System.Drawing.Color.RoyalBlue;
        //    UltrWebGrid.DisplayLayout.Bands[0].HeaderStyle.ForeColor = System.Drawing.Color.White;

        //    UltrWebGrid.JavaScriptFileName = "InfraSup/ig_common/WebGrid3/ig_WebGrid.js";
        //    UltrWebGrid.JavaScriptFileNameCommon = "InfraSup/ig_common/Scripts/ig_csom.js";
        //} //end function
        #endregion

        #endregion
    }

    public class BPWebFormsFriendlyUrlResolver : Microsoft.AspNet.FriendlyUrls.Resolvers.WebFormsFriendlyUrlResolver
    {
        protected override bool TrySetMobileMasterPage(HttpContextBase httpContext, System.Web.UI.Page page, String mobileSuffix)
        {
            if (mobileSuffix == "Mobile")
            {
                return false;
            }
            else
            {
                return base.TrySetMobileMasterPage(httpContext, page, mobileSuffix);
            }
        }
    }
}