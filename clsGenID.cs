using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



/// <summary>
/// Summary description for clsGenID.
/// </summary>


///back end table structure

///TableName	Script	LastUpdatedDate	Remarks
///Signature	"

//Drop table Signature
//GO

//CREATE TABLE Signature
//(
// Field			varchar  (50) NOT NULL , 
// Dates			datetime  NOT NULL ,
// LastNumber		numeric(10,2) NULL ,
// primary key (Field,Dates)
//)  
//GO

///"	March 12,2002	B.Sinha
///

namespace bplib
{
    public class clsGenID
    {
        public clsGenID()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region Gen ID
        public void GenID(string strEntryDate, string strFieldName, out string strID)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            //  int						lngRecCount=0;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            decimal LastNumber = 0;

            try
            {
                //strEntryDate=bplib.clsutilib.AppDateConvert(strEntryDate,"MM/dd/yyyy",bplib.clsutilib.getUserDateFormat()).ToShortDateString();
                strEntryDate = bplib.clsWebLib.AppDateConvert(strEntryDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                strSql = "Select * from Signature where Field ='" + strFieldName.Trim() + "' and Dates = '" + strEntryDate + "'";

                SB = new System.Text.StringBuilder(strEntryDate);
                strID = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();

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

                //strID = strID + "-" + (int)LastNumber;
                strID = strID + "" + lstNmb;
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
        }

        public void GenAssetID(string strEntryDate, string strFieldName, out string strID)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            //  int						lngRecCount=0;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            decimal LastNumber = 0;
            

            try
            {
                //strEntryDate=bplib.clsutilib.AppDateConvert(strEntryDate,"MM/dd/yyyy",bplib.clsutilib.getUserDateFormat()).ToShortDateString();
                strEntryDate = bplib.clsWebLib.AppDateConvert(strEntryDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                strSql = "Select * from Signature where Field ='" + strFieldName.Trim() + "' and Dates = '" + strEntryDate + "'";

                SB = new System.Text.StringBuilder(strEntryDate);
                strID = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();

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

                string lstNmb = string.Format("{0:0000}", LastNumber);

                //strID = strID + "-" + (int)LastNumber;
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

        public void GenDeptCodMaxNum(string strPreFix, out string strID)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            //  int						lngRecCount=0;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            decimal LastNumber = 0;


            try
            {

                strSql = "Select Max(Convert(Numeric(18,0),Substring(DeptCode,5,4))) MaxNum From dbo.tblAMSAssetRegister Where Substring(DeptCode,0,4) = '" + strPreFix.Trim() + "'";

                //strID = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();

                objCoManager = new ConnectionManager.DAL.ConManager("1");
                objCoManager.OpenDataSetThroughAdapter(strSql, out dsLocal, false, false, "", "1");
                dtLocal = dsLocal.Tables[0];
                dvLocal = new System.Data.DataView();
                dvLocal.Table = dtLocal;

                if (dvLocal.Count == 0)
                {// Add data
                    LastNumber = 1;
                }
                else if (dvLocal.Count == 1)
                {
                    drLocal = dvLocal[0].Row;

                    LastNumber = System.Convert.ToDecimal(bplib.clsWebLib.GetNumData(("" + drLocal["MaxNum"].ToString())));
                    LastNumber = LastNumber + 1;
                }
                objCoManager.SaveDataSetThroughAdapter(ref dsLocal, false, "1");

                string lstNmb = string.Format("{0:0000}", LastNumber);

                //strID = strID + "-" + (int)LastNumber;
                strID = strPreFix + "-" + lstNmb;
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

        public void GenLocationID(string strEntryDate, string strFieldName, out string strID)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            //  int						lngRecCount=0;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            decimal LastNumber = 0;


            try
            {
                //strEntryDate=bplib.clsutilib.AppDateConvert(strEntryDate,"MM/dd/yyyy",bplib.clsutilib.getUserDateFormat()).ToShortDateString();
                strEntryDate = bplib.clsWebLib.AppDateConvert(strEntryDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                strSql = "Select * from Signature where Field ='" + strFieldName.Trim() + "' and Dates = '" + strEntryDate + "'";

                SB = new System.Text.StringBuilder(strEntryDate);
                strID = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();

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

                string lstNmb = string.Format("{0:0000}", LastNumber);

                //strID = strID + "-" + (int)LastNumber;
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

        public void GenEmpCode(string strEntryDate, string strFieldName, out string strID)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            decimal LastNumber = 0;

            try
            {
                strEntryDate = bplib.clsWebLib.AppDateConvert(strEntryDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                strSql = "Select * from Signature where Field ='" + strFieldName.Trim() + "' and Dates = '" + strEntryDate + "'";

                SB = new System.Text.StringBuilder(strEntryDate);
                strID = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();
                //strID = strID.Substring(0, strID.Length - 2);

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

                //strID = strID + "-" + (int)LastNumber;
                strID = strID + "" + lstNmb;
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

        #region GetLastNumber
        public void GetLastNumber(string strEntryDate, string strFieldName, out string strID)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            //	int						lngRecCount=0;
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            decimal LastNumber = 0;

            try
            {
                strEntryDate = bplib.clsWebLib.AppDateConvert(strEntryDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                strSql = "Select * from Signature where Field ='" + strFieldName.Trim() + "' and Dates = '" + strEntryDate + "'";

                SB = new System.Text.StringBuilder(strEntryDate);
                strID = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();

                objCoManager = new ConnectionManager.DAL.ConManager("1");
                objCoManager.OpenDataSetThroughAdapter(strSql, out dsLocal, false, false, "", "1");
                //------------------------------------------------------
                if ((dsLocal.Tables[0].Rows.Count < 0) || (dsLocal.Tables[0].Rows.Count == 0))
                {
                    LastNumber = 0;
                }
                else
                {
                    LastNumber = Math.Round(System.Convert.ToDecimal(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[0]["LastNumber"].ToString().Trim())));
                }
                strID = System.Convert.ToString(LastNumber + 1);
                //------------------------------

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

                //strID=strID+"-"+(int)LastNumber;
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
        }
        #endregion

        #region Folder
        public void MakeFolderID(string strEntryDate, string strFieldName, ref string strCode, ref double lastNo, double BlockNo)
        {
            ConnectionManager.DAL.ConManager objCoManager;
            string strSql = "";
            System.Data.DataSet dsLocal = null;
            System.Data.DataTable dtLocal = null;
            System.Data.DataRow drLocal = null;
            System.Data.DataView dvLocal = null;

            System.Text.StringBuilder SB = null;
            try
            {
                objCoManager = new ConnectionManager.DAL.ConManager("1");
                lastNo = 0;

                //strEntryDate = bplib.clsutilib.AppDateConvert(strEntryDate,"MM/dd/yyyy",bplib.clsutilib.getUserDateFormat()).ToShortDateString();
                strEntryDate = bplib.clsWebLib.AppDateConvert(strEntryDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                strSql = "Select * from Signature where Field ='" + strFieldName.Trim() + "' and Dates = '" + strEntryDate + "'";
                objCoManager.OpenDataSetThroughAdapter(strSql, out dsLocal, false, "1");

                SB = new System.Text.StringBuilder(strEntryDate);
                strCode = SB.Replace(bplib.clsWebLib.getUserDateSeparator().ToString(), "").ToString();

                strCode = strCode.Substring(strCode.Length - 2, 2) + strCode.Substring(0, 2) + strCode.Substring(2, 2);
                dtLocal = dsLocal.Tables[0];
                dvLocal = new System.Data.DataView();
                dvLocal.Table = dtLocal;
                dvLocal.RowFilter = "Field ='" + strFieldName.Trim() + "'and Dates = '" + strEntryDate + "'";
                if (dvLocal.Count == 0)
                {// Add data
                    drLocal = dtLocal.NewRow();
                    drLocal["Field"] = bplib.clsWebLib.RetValidLen(strFieldName, 50);
                    drLocal["Dates"] = strEntryDate.Trim();
                    //						drLocal["LastNumber"]=BlockNo + 1;
                    //						lastNo = BlockNo + 1;
                    drLocal["LastNumber"] = BlockNo;
                    lastNo = 1;

                    dtLocal.Rows.Add(drLocal);
                }
                else if (dvLocal.Count == 1)
                {
                    drLocal = dvLocal[0].Row;

                    lastNo = System.Convert.ToDouble(bplib.clsWebLib.GetNumData(("" + drLocal["LastNumber"].ToString())));
                    drLocal.BeginEdit();
                    //drLocal["LastNumber"]=lastNo + BlockNo +1;
                    drLocal["LastNumber"] = lastNo + BlockNo;

                    lastNo = lastNo + 1;
                    drLocal.EndEdit();
                }
                objCoManager.SaveDataSetThroughAdapter(ref dsLocal, false, "1");
                strCode = strCode + (int)lastNo;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                dsLocal = null;
                dtLocal = null;
                dvLocal = null;
            }

        }
        #endregion

    }
}