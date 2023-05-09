using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace bplib
{

    /// <summary>
    /// Summary description for clsAppSeq.
    /// </summary>
    public class clsAppSeq
    {
        public clsAppSeq()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Normal user validation - Under Dataadmin update
        public static bool CheckUserAccess(string userID, string ModuleName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            System.Data.DataTable dtLocal = null;
            string strSql = "";
            bool blnStatus = false;
            try
            {
                ModuleName = ModuleName.Trim().ToUpper();
                userID = userID.Trim().ToUpper();
                if (ModuleName.Trim() == "ACCESS ADMIN")
                {
                    strSql = "Select * from USER_ACC_MANAGER where MODULENAME='" + ModuleName.Trim() + "' and USERID='" + userID.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count > 0)
                    {
                        blnStatus = true;
                    }
                    else
                    {
                        blnStatus = false;
                        string ss = String.Format("Sorry Access denied for Module Name - {0} UserID  - {1}", ModuleName.Trim(), userID.Trim());
                        System.Exception ex = new Exception(ss);
                        throw (ex);
                    }
                    //}
                }
                else
                {
                    strSql = "Select * from USER_ACC_MANAGER where MODULENAME='" + ModuleName.Trim() + "' and USERID='" + userID.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count > 0)
                    {
                        blnStatus = true;
                    }
                    else
                    {
                        blnStatus = false;
                        string ss = String.Format("Sorry Access denied for Module Name - {0} UserID  - {1}", ModuleName.Trim(), userID.Trim());
                        System.Exception ex = new Exception(ss);
                        throw (ex);
                    }
                }
                return blnStatus;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }
        public static bool CheckUserAccessForSQL(string userID, string ModuleName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            System.Data.DataTable dtLocal = null;
            string strSql = "";
            bool blnStatus = false;
            try
            {
                ModuleName = ModuleName.Trim().ToUpper();
                userID = userID.Trim().ToUpper();
                if (ModuleName.Trim() == "ACCESS ADMIN")
                {
                    strSql = "Select * from USER_ACC_MANAGER where MODULENAME='" + ModuleName.Trim() + "' and USERID='" + userID.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count > 0)
                    {
                        blnStatus = true;
                    }
                    else
                    {
                        blnStatus = false;
                      
                    }
                    //}
                }
                else
                {
                    strSql = "Select * from USER_ACC_MANAGER where MODULENAME='" + ModuleName.Trim() + "' and USERID='" + userID.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count > 0)
                    {
                        blnStatus = true;
                    }
                    else
                    {
                        blnStatus = false;
                       
                    }
                }
                return blnStatus;
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                objCon = null;
            }
        }
        public bool CheckNormalUser(string userID, string userPWD, ref int LOGIN_STATUS, ref string Message, ref string Group)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            System.Data.DataTable dtLocal = null;
            string strSql = "";
            string strpwd = "";
            bool blnStatus = false;
            LOGIN_STATUS = 0;
            try
            {
                userID = userID.Trim().ToUpper();
                userPWD = userPWD.Trim().ToUpper();
                strSql = "Select * from tblUser";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                if (dtLocal.Rows.Count <= 0)
                {
                    System.Exception ex = new System.Exception("There is no user profile in the system.\n Please contact system admin and create user profile first. ------- Thank you");
                    blnStatus = false;
                    throw (ex);
                    return blnStatus;
                }
                else
                {
                    strSql = "Select * from tblUser where UserID='" + userID.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count <= 0)
                    {
                        blnStatus = false;
                    }
                    else
                    {
                        strpwd = "" + dtLocal.Rows[0][1].ToString();
                        if (strpwd == "")
                        {
                            LOGIN_STATUS = 2;
                            blnStatus = true;
                        }
                        else
                        {
                            
                            strpwd= BPEnCodeDecodeLib.clsEnCodeDeCode.DecodeHI(strpwd, "bapps");
                            if (String.Compare(strpwd, userPWD, true).ToString() != "0")
                            {
                                blnStatus = false;
                            }
                            else
                            {
                                LOGIN_STATUS = 2;
                                blnStatus = true;
                            }
                        }
                    }
                }
                Group = "" + dtLocal.Rows[0][2].ToString();
                return blnStatus;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }

        }//end of function
        public bool CheckUserID(string userID, string userPWD)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            System.Data.DataTable dtLocal = null;
            string strSql = "";
            string strpwd = "";
            bool blnStatus = false;

            try
            {
                userID = userID.Trim().ToUpper();
                userPWD = userPWD.Trim().ToUpper();

                strSql = "Select * from USER_INFO";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                if (dtLocal.Rows.Count <= 0)
                {
                    //	Message="There is no user profile in the system.\n Please go to USER Account Manager and create admin user profile first. Assign the module wise access.\n\n                                                   ------- Thank you";
                    blnStatus = true;
                    return blnStatus;
                }
                else
                {
                    strSql = "Select * from USER_INFO where UserID='" + userID.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count <= 0)
                    {
                        blnStatus = false;
                        //mess
                    }
                    else
                    {
                        strpwd = "" + dtLocal.Rows[0][1].ToString();
                        if (strpwd == "")
                        {
                            blnStatus = true;
                        }
                        else
                        {
                            strpwd= BPEnCodeDecodeLib.clsEnCodeDeCode.DecodeHI(strpwd, "bapps");
                            if (String.Compare(strpwd, userPWD, true).ToString() != "0")
                            {
                                blnStatus = false;

                            }
                            else
                            {

                                blnStatus = true;
                            }
                        }
                    }
                }
                return blnStatus;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }

        }//end of function



        public bool CheckUserAccToSite(string userID, string userSiteID, ref int LOGIN_STATUS, ref string Message)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            System.Data.DataTable dtLocal = null;
            string strSql = "";
            string strSite = "";
            bool blnStatus = false;
            LOGIN_STATUS = 0;
            try
            {
                userID = userID.Trim().ToUpper();
                userSiteID = userSiteID.Trim().ToUpper();

                strSql = "Select * from USER_ACC_IN_SITE";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                if (dtLocal.Rows.Count <= 0)
                {
                    System.Exception ex = new System.Exception("There is no site access updated in the system. Please contact system admin. ------- Thank you");
                    blnStatus = false;
                    throw (ex);
                    return blnStatus;
                }
                else
                {
                    strSql = "Select * from USER_ACC_IN_SITE where UserID='" + userID.Trim() + "' and SITEID='" + userSiteID + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count <= 0)
                    {
                        Message = "Error - User has not authorized site access.";
                        blnStatus = false;
                    }
                    else
                    {
                        strSite = "" + dtLocal.Rows[0][1].ToString().ToUpper();
                        if (String.Compare(strSite, userSiteID, true).ToString() != "0")
                        {
                            Message = "Error - User is not authorized to access the selected site.";
                            blnStatus = false;
                        }
                        else
                        {
                            LOGIN_STATUS = 2;
                            blnStatus = true;
                        }
                    }
                }
                return blnStatus;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }

        }//end of function
        #endregion

        #region Custimized function To create / save the user details
        public void GetDataUserSearch(string fromDate, string todate, string strKey, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    //strSql = "select ReqID,convert(varchar(20),EntryDate,101) as EntryDate,PINo,ToName,CommInvoiceNo,AWBNo,SupplierInvNo,convert(varchar(20),UpdateBy,101) as UpdateBy from tblAirReq where EntryDate between '" + strfromdate + "' and '" +  strtodate + "' order by UpdateBy desc";
                    strSql = "select UserID,UserGroup,UserLocation from tbluser where UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by UpdateOn desc";
                }
                else
                {
                    strSql = "select UserID,UserGroup,UserLocation from tbluser where " + strKey + " order by USERID";
                }
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end of function

        public void GetUserList(string strUserID, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                if (strUserID.Trim() == "")
                {
                    strSql = "Select * from tblUser order by UserID";
                }
                else
                {
                    strSql = "Select * from tblUser where UserID='" + strUserID.Trim() + "'";
                }
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
        }//end of function
        public void GetUserID(out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select UserID from tblUser";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end of function

        public void SaveUserInfoByDataSet(ref System.Data.DataSet dsRef)
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

        } // End Function

        public void DeleteUSERInfo(string strUserID)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSQL = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                strSQL = "Delete from tblUser where USERID='" + strUserID.Trim() + "'";
                objCon.ExecuteNonQueryWrapper(strSQL, true, "1");
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
        } //end function
        #endregion
    }

}