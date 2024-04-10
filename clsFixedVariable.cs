using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bplib
{
    /// <summary>
    /// Summary description for clsFixedVariable.
    /// </summary>
    public class clsFixedVariable
    {
        public clsFixedVariable()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Customized Function to get information from Fixed Variable
        public void GetEntityType(out System.Data.DataTable dtRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select distinct(EntityType) from EntityFixedVariables";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataTableThroughAdapter(strSQl, out dtRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//eof

        public void DeleteFixedVaraibleInfo(string EntityType, string Code, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where EntityType='" + EntityType + "' and Code='" + Code + "'", true, "1");
                objCon.CommitTransaction();
                objCon.CloseConnection();
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

        public void GetEntityFixedVariablesDesc(out System.Data.DataSet dsRef, string EntityType)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select Code as iNo, Description as CODE from EntityFixedVariables";
                if (EntityType.Trim() != "")
                {
                    strSQl = strSQl + " where EntityType = '" + EntityType.Trim() + "'";
                }
                strSQl = strSQl + " Order by iNo";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//eof
        //return fixed entity set by MIS deptt

        public void GetFixedValiablesInformation(out System.Data.DataSet dsRef, string sEntityType, string sCode, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "select * from " + tblName + "";
                if (sEntityType.Trim() != "" && sCode.Trim() != "")
                {

                    strSql = strSql + " where EntityType = '" + sEntityType.Trim() + "'" +
                        " and Code='" + sCode.Trim() + "'";
                }
                strSql = strSql + " Order by Code";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");


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


        public void GetWebComboFixedVariable(out System.Data.DataSet dsRef, string tblName, string sortorder)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select distinct EntityType from " + tblName + " order by " + sortorder;
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
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

        public void GetEntityFixedValiables(out System.Data.DataSet dsRef, string EntityType)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select EntityType,Code,Description,Value from EntityFixedVariables";
                if (EntityType.Trim() != "")
                {
                    strSQl = strSQl + " where EntityType = '" + EntityType.Trim() + "'";
                }
                strSQl = strSQl + " Order by Code";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//eof

        public void SaveFixedVaraible(ref System.Data.DataSet dsRef)
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

        public void GetFileOwnedByfromFixedValiables(out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select CODE,Description=CODE+' - '+Description from EntityFixedVariables";
                strSQl = strSQl + " where EntityType = 'FILE_OWNED_BY'";
                strSQl = strSQl + " Order by Code";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//eof

        public void GetEntityFixedValiablesDesc(out System.Data.DataSet dsRef, string EntityType)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
                   {
                strSQl = "select Description as CODE from EntityFixedVariables";
                if (EntityType.Trim() != "")
                {
                    strSQl = strSQl + " where EntityType = '" + EntityType.Trim() + "'";
                }
                strSQl = strSQl + " Order by Code";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//eof
        #endregion
    }
}