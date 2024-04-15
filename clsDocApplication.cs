using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWOMS
{
	public class clsDocApplication
	{
        public clsDocApplication()
		{
		}
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

        } //eof
        #region Update Customer
        public void GetDataOfDOC(string strUserId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblDOCMgt where EntryID='" + strUserId.Trim() + "' ";
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
        public void SearchDocument(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.EntryID,X.EntryDateTime,X.Documents_Group,X.DocumentName,X.DocumentDescription,x.VersionNo,x.BuildNo FROM (SELECT  EntryID,Documents_Group,DocumentName,
DocumentDescription,VersionNo,BuildNo,
                         IIF(convert(varchar(20), EntryDateTime, 105)='01-01-1901',NULL,convert(varchar(20), EntryDateTime, 105)) as EntryDateTime
                         FROM  tblDOCMgt) X";

                //strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " where EntryDateTime between '" + strfromdate + "' and '" + strtodate + "' order by EntryID";

                }
                else
                {
                    strSql = strSql + " where " + strKey + " order by EntryID";
                }

                //strSql = strSql + "order by EmployeeId";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, "1");            // Get the data table as well as data set name
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//eof
        public void DeleteDataOfDOC(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where EntryID='" + ID + "' ", true, "1");
                objCon.CommitTransaction();
            }
            catch (Exception ex)
            {
                objCon.RollBack();
                throw (ex);
            }
            finally
            {
                objCon.CloseConnection();
                objCon = null;
            }
        }   //eof

        #endregion
    }
}