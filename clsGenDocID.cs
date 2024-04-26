using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bplib

{
	public class clsGenDocID
	{
        #region Gen Document ID
        public void GenDOCIdTest(string strEntryDate, string strFieldName, out string strID)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            decimal LastNumber = 0;
            char[] stringArray;

            try
            {
                strEntryDate = bplib.clsWebLib.AppDateConvert(strEntryDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                strSql = "Select * from Signature where Field ='" + strFieldName.Trim() + "' and Dates = '" + strEntryDate + "'";

                SB = new System.Text.StringBuilder(strEntryDate);
                strID = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();
                stringArray = strID.ToCharArray();
                strID = new string(stringArray);
                objCoManager = new ConnectionManager.DAL.ConManager("1");
                objCoManager.OpenDataSetThroughAdapter(strSql, out dsLocal, false, false, "", "1");
                dtLocal = dsLocal.Tables[0];
                dvLocal = new System.Data.DataView();
                dvLocal.Table = dtLocal;
                dvLocal.RowFilter = "Field ='" + strFieldName.Trim() + "'and Dates = '" + strEntryDate + "'";
                if (dvLocal.Count == 0)
                {// Add data
                    drLocal = dtLocal.NewRow();
                    drLocal["Field"] = bplib.clsWebLib.RetValidLen(strFieldName, 50);
                    drLocal["Dates"] = strEntryDate.Trim();
                    drLocal["LastNumber"] = 1;
                    LastNumber = 1;
                    dtLocal.Rows.Add(drLocal);
                }
                else if (dvLocal.Count == 1)
                {
                    drLocal = dvLocal[0].Row;

                    LastNumber = System.Convert.ToDecimal(bplib.clsWebLib.GetNumData(("" + drLocal["LastNumber"].ToString())));
                    LastNumber = LastNumber + 1;

                    drLocal.BeginEdit();
                    drLocal["LastNumber"] = LastNumber;
                    drLocal.EndEdit();
                }
                objCoManager.SaveDataSetThroughAdapter(ref dsLocal, false, "1");
                string lstNmb = string.Format("{0:00}", LastNumber);
                strID = strID + "-" + lstNmb;
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dtLocal = null;
                dvLocal = null;
                drLocal = null;
            }
        }//eof

        #endregion


    }
}