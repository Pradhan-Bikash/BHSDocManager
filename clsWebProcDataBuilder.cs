using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System
{
    /// <summary>
    /// Summary description for clsWebProcDataBuilder
    /// </summary>
    public class clsWebProcDataBuilder
    {
        ConnectionManager.DAL.ConManager appConn;
        private string mdbPath = "";

        private clsWebProcDataBuilder()
        {
            //
            // TODO: Add constructor logic here
            //
        }//eof

        #region Cutomized Function About ProcLock
        public static string CheckAndLock_PROC(string procid, string userid)
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
        public static void RemoveLock_PROC(string procid)
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

        public static double toDouble(object objNumbers, string format = "Default")
        {
            double dblValue = 0;
            try
            {
                if (string.IsNullOrWhiteSpace(format.Trim()) == true || string.Compare(format.Trim(), "Default", true) == 0)
                {
                    format = "{0:0.###0}";
                }
                if (Double.TryParse(objNumbers.ToString().Trim(), out dblValue))
                {
                    dblValue = Convert.ToDouble(string.Format(format, dblValue));
                }
            }
            catch (Exception ex)
            {
                dblValue = 0;
            }
            finally
            {
            }
            return dblValue;
        }//eof
        public static string removeProhibitedChars(string strInput, int iNoOfChar = 0, string strReplaceBy = "_")
        {
            string strOutput = "";
            try
            {
                strInput = strInput.Replace("<", strReplaceBy);
                strInput = strInput.Replace(">", strReplaceBy);
                strInput = strInput.Replace(":", strReplaceBy);
                strInput = strInput.Replace("\"", strReplaceBy);
                strInput = strInput.Replace("/", strReplaceBy);
                strInput = strInput.Replace("\\", strReplaceBy);
                strInput = strInput.Replace("|", strReplaceBy);
                strInput = strInput.Replace("*", strReplaceBy);
                strInput = strInput.Replace("?", strReplaceBy);
                strInput = strInput.Replace("'", strReplaceBy);
                strInput = strInput.Replace("\r", strReplaceBy);
                strInput = strInput.Replace("\n", strReplaceBy);

                if (iNoOfChar == 0)
                {
                    strOutput = strInput.ToString().Trim();
                }
                else
                {
                    if (iNoOfChar > strInput.ToString().Trim().Length)
                    {
                        strOutput = strInput.ToString().Trim();
                    }
                    else
                    {
                        iNoOfChar = iNoOfChar - 1;
                        strOutput = strInput.Substring(0, iNoOfChar).Trim();

                    }
                }
            }
            catch (System.Exception ex)
            {
                strOutput = "";
            }
            finally
            {
                //
            }
            return strOutput;
        }// end function
        public static DateTime convertToDateTime(string inputDateValue, CheckDateFormat checkDateFormat = CheckDateFormat.NOCHECK)
        {
            System.DateTime dtResult = default(DateTime);
            System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            //System.Globalization.DateTimeFormatInfo USER_TERMINAL_DATE_FORMAT = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat;
            System.Globalization.DateTimeStyles styles = System.Globalization.DateTimeStyles.None;
            try
            {
                if (checkDateFormat == CheckDateFormat.CHECK)
                {
                    if (!System.DateTime.TryParseExact(inputDateValue, "dd-MMM-yyyy", culture, styles, out dtResult))
                    {
                        dtResult = new DateTime(1901, 1, 1);
                    }
                }
                else
                {
                    if (!System.DateTime.TryParse(inputDateValue, culture, styles, out dtResult))
                    {
                        dtResult = new DateTime(1901, 1, 1);
                    }
                }
            }
            catch (Exception)
            {
                dtResult = new DateTime(1901, 1, 1);
            }

            return dtResult;
        }//eof
        public enum CheckDateFormat
        {
            CHECK,
            NOCHECK
        }//eof

        private static System.Data.DataTable GetTable()
        {
            // Here we create a DataTable with four columns.
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("Dosage", typeof(int));
            table.Columns.Add("Drug", typeof(string));
            table.Columns.Add("Patient", typeof(string));
            table.Columns.Add("Date", typeof(DateTime));

            // Here we add five DataRows.
            table.Rows.Add(25, "Indocin", "David", DateTime.Now);
            table.Rows.Add(50, "Enebrel", "Sam", DateTime.Now);
            table.Rows.Add(10, "Hydralazine", "Christoff", DateTime.Now);
            table.Rows.Add(21, "Combivent", "Janet", DateTime.Now);
            table.Rows.Add(100, "Dilantin", "Melanie", DateTime.Now);
            return table;
        }//eof
        public static int getWeekNumber(object dateTime)
        {
            int weekNum = 0;
            DateTime convertedDateTime = new DateTime(1901, 01, 01);
            try
            {
                if (!string.IsNullOrWhiteSpace(dateTime.ToString().Trim()))
                {
                    convertedDateTime = convertToDateTime(dateTime.ToString().Trim());
                }
                if (convertedDateTime.Year != 1901)
                {
                    System.Globalization.CultureInfo ciCurr = System.Globalization.CultureInfo.GetCultureInfo("en-US");
                    weekNum = ciCurr.Calendar.GetWeekOfYear(convertedDateTime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, System.DayOfWeek.Monday);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                weekNum = 0;
            }
            catch (Exception)
            {
                weekNum = 0;
            }

            return weekNum;
        }//eof

        private string genProcessRef()
        {
            string strSQl;
            System.Data.DataSet dsRef = null;
            string strProcessRef = "";
            ConnectionManager.DAL.ConManager objCon;
            string strToday = System.DateTime.Now.ToString("yyyy-MM-dd").Trim();
            try
            {
                strSQl = @"SELECT 
	COUNTERS=(ISNULL(MAX(CONVERT(INT, REPLACE(RIGHT(LCSCProcReq.ProcessRef,CHARINDEX('-',REVERSE(LCSCProcReq.ProcessRef))),'-',''))),0))+1 
FROM 
	tblLCSCProcReq LCSCProcReq 
WHERE 
	CHARINDEX('-',REVERSE(LCSCProcReq.ProcessRef))!=0 
	AND LCSCProcReq.updateOn='" + strToday + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
                strToday = strToday.Replace("-", string.Empty);
                if (dsRef.Tables[0].Rows.Count > 0)
                {

                    strProcessRef = strToday + "-" + dsRef.Tables[0].Rows[0]["COUNTERS"].ToString().Trim();
                }
                else
                {
                    strProcessRef = strToday + "-1";
                }
                return strProcessRef;
            }
            catch (Exception ex)
            {

                throw new Exception("Error in Process Ref Generation : " + ex.Message.ToString());
            }
        }//CODE HERE

    }
}