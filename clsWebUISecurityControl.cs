using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace bplib
{
    public class clsWebUISecurityControl
    {

        /// <summary>
        /// Summary description for clsWebUISecurityControl.
        /// </summary>
        public clsWebUISecurityControl()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        #region Customized function - Log In
        public void CheckDefaultConnection(string strUser, string strPassword, string strDefaultConID, string strPath)
        {

            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager(strDefaultConID);
                objCon.LoginApplication(strUser, strPassword, strDefaultConID,
                    strPath);
            }
            catch (System.Exception ex)
            { throw ex; }
            finally
            {
                objCon = null;
            }
        }//end function

        public bool ValidateUser(string userID, string userPWD, ref int LOGIN_STATUS, ref string Message)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            System.Data.DataTable dtLocal = null;
            string strSql = "";
            string strpwd = "";
            bool blnStatus = false;
            LOGIN_STATUS = 0;
            Message = "";
            try
            {
                userID = userID.Trim().ToUpper();
                userPWD = userPWD.Trim().ToUpper();
                if (userID == "MISADMIN" && userPWD == "MISADMIN")
                {
                    Message = "Welcome System Developer. You have only access on user account manager.\n Set System ADMIN ID and log off. And use the admin ID from next time.";
                    LOGIN_STATUS = 1;
                    blnStatus = true;
                    return blnStatus;
                }
                strSql = "Select * from USER_INFO";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                if (dtLocal.Rows.Count <= 0)
                {
                    Message = "There is no user profile in the system.\n Please go to USER Account Manager and create admin user profile first. Assign the module wise access.\n\n                                                   ------- Thank you";
                    LOGIN_STATUS = 2;
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
                        System.Exception ex = new Exception("INVALID USER ID...(secondary log in check)");
                        throw (ex);
                    }
                    else
                    {
                        strpwd = "" + dtLocal.Rows[0][1].ToString();
                        if (strpwd == "")
                        {
                            blnStatus = true;
                            //LOGIN_STATUS=2;
                            LOGIN_STATUS = 0;
                        }
                        else
                        {                            
                            strpwd = BPEnCodeDecodeLib.clsEnCodeDeCode.DecodeHI(strpwd, "bapps");
                            if (String.Compare(strpwd, userPWD, true).ToString() != "0")
                            {
                                blnStatus = false;
                                System.Exception ex = new Exception("INVALID PASSWORD...(secondary log in check)");
                                throw (ex);
                            }
                            else
                            {
                                LOGIN_STATUS = 2;
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
        }// end function


        public static bool CheckUserAccess(string userID, string ModuleName, ref string Message)
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
                    strSql = "Select * from USER_ACC_MANAGER where MODULENAME='" + ModuleName.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataTableThroughAdapter(strSql, out dtLocal, false, "1");
                    if (dtLocal.Rows.Count <= 0)
                    {
                        blnStatus = true;
                        return blnStatus;
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
                            string ss = String.Format("Sorry Access denied for ...\n Module Name : {0}\n UserID  : {1}", ModuleName.Trim(), userID.Trim());
                            Message = ss;
                            blnStatus = false;
                        }
                    }
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
                        string ss = String.Format("Sorry Access denied for ...\n Module Name : {0}\n UserID  : {1}", ModuleName.Trim(), userID.Trim());
                        Message = ss;
                        blnStatus = false;
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
        }//end function

        public void getSites(out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                strSql = "Select SITEID,SITE_GROUP from USER_SITE order by SITE_GROUP";
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
        #endregion

        #region Custimized function To create / save the user details
        public void GetUserList(string strUserID, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            try
            {
                if (strUserID.Trim() == "")
                {
                    strSql = "Select * from USER_INFO order by USERID";
                }
                else
                {
                    strSql = "Select * from USER_INFO where USERID='" + strUserID.Trim() + "'";
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
                strSQL = "Delete from USER_INFO where USERID='" + strUserID.Trim() + "'";
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