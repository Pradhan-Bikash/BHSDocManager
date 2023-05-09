using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace BPWEBAccessControl
{
    public class clsSecurityControl
    {
        public clsSecurityControl()
        {
        }//eof

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

        } // End Function	

        #region User Access
        public void searchModules(out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * From ApplicationModule order by moduleid";
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
        public void searchModules(string searchKey, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * From ApplicationModule";
                strSQl += " where " + searchKey.Trim();
                strSQl += " order by moduleid";
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
        public void GetUserAccManagerInfo(out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";

            try
            {

                strSql = "Select DISTINCT(UserID) as USERID from tblUser ORDER BY USERID";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }// End function
        public void GetUserAccManagerInfo(string UserCode, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";

            try
            {

                strSql = "Select * from USER_ACC_MANAGER where USERID='" + UserCode.Trim() + "' ORDER BY ModuleName";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }// End function
        public void DeleteSiteAccData(string UserID)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from USER_ACC_IN_SITE where USERID='" + UserID.Trim() + "'", true, "1");
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
        }	// end function
        public void DeleteUserAccData(string UserID)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from USER_ACC_MANAGER where USERID='" + UserID.Trim() + "'", true, "1");
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
        }	// end function
        public void DeleteUserModule(string strUserID, string strModule)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from USER_ACC_MANAGER where (USERID='" + strUserID.Trim() + "') AND (MODULENAME LIKE '" + strModule.Trim() + "%')", true, "1");
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
        }	// end function
        #endregion

        #region Creating User
        public void GetUserDetails(string UserCode, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";

            try
            {

                strSql = "Select * from tblUser where USERID='" + UserCode.Trim() + "' ORDER BY USERID";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }// End function
        public void SearchUserData(string fromDate, string todate, string strKey, out System.Data.DataSet dsRef)
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

                    strSql = "Select UserID,Password,UserGroup,UserLocation,convert(varchar(20),UpdateOn,106) as UpdateOn from tblUser where UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by UpdateOn desc,UserID asc";

                }
                else
                {
                    strSql = "Select UserID,Password,UserGroup,UserLocation,convert(varchar(20),UpdateOn,106) as UpdateOn from tblUser where " + strKey + " order by UpdateOn desc,UserID asc";
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
        public void DeleteUserData(string UserID)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from tblUser where USERID='" + UserID.Trim() + "'", true, "1");
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
        }// end function

        public bool checkPassword(string strPWD, out string strResultStatus)
        {
            char[] chrSpecialChar = new char[] { '!', '"', '#', '$', '%', '&', '\'', '(', ')', '*', '+', ',', '-', '.', '/', ':', ';', '<', '=', '>', '?', '@', '[', '\\', ']', '^', '_', '`', '{', '|', '}', '~' };
            char[] chrAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            strResultStatus = "";

            bool result = false;
            int iSplChar = 0;
            int iAlpha = 0;
            int iNumeric = 0;
            try
            {
                //strPWD = String.Concat(strPWD.Where(c => char.IsWhiteSpace(c) == false)).ToUpper();
                strPWD = strPWD.ToUpper();
                if (string.IsNullOrWhiteSpace(strPWD) == true || strPWD.Trim().Length > 10)
                {
                    strResultStatus = "Password must contain maximum 10 Characters........[Must Contain : 1 Capital Alpha Char, 1 Special Char, 1 Numeric Char]";
                }
                else
                {
                    char[] chrPWD = strPWD.ToCharArray();
                    foreach (char eachChar in chrPWD)
                    {
                        if (iSplChar == 0 && Array.Exists(chrSpecialChar, element => element == eachChar))
                        {
                            iSplChar = 1;
                        }
                        if (iAlpha == 0 && Array.Exists(chrAlpha, element => element == eachChar))
                        {
                            iAlpha = 1;
                        }
                        if (iNumeric == 0 && char.IsNumber(eachChar))
                        {
                            iNumeric = 1;
                        }
                    }

                    strResultStatus = "";
                    if (iSplChar == 0)
                    {
                        strResultStatus = "<br/>Password must contain at least 1 Special Character";
                        result = false;
                    }
                    else
                    {
                        result = true;
                    }
                    if (iAlpha == 0)
                    {
                        strResultStatus = strResultStatus.Trim().Length > 0 ? strResultStatus + "<br/>" : "";
                        strResultStatus += "Password must contain at least 1 Alpha Character";
                        result = false;
                    }
                    else
                    {
                        result = result == true ? true : false;
                    }
                    if (iNumeric == 0)
                    {
                        strResultStatus = strResultStatus.Trim().Length > 0 ? strResultStatus + "<br/>" : "";
                        strResultStatus += "Password must contain at least 1 Numeric Character";
                        result = false;
                    }
                    else
                    {
                        result = result == true ? true : false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error in Password Checking :<br/>" + ex.ToString());
            }
            return result;
        }//eof
        public bool CheckEmailPatern(string strInputEmail)
        {
            try
            {
                string pattern = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
                if (Regex.IsMatch(strInputEmail.ToLower(), pattern) == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                return false;

            }
        }//eof

        public void getSystemModules(string strEntities, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";

            try
            {

                strSql = "select * from tblUserMgtSetting where Entities='" + strEntities.Trim() + "' ORDER BY EntryValues";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }// End function
        #endregion

        #region Copy User Access
        public string CopySystemAccess(string srcUser, string TrgetUser)
        {
            System.Data.DataSet dsTarget = null;
            System.Data.DataSet dsSource = null;
            string modules = "";
            System.Data.DataRow drTarget = null;

            try
            {

                GetUserAccManagerInfo(srcUser, out dsSource);
                if (dsSource.Tables[0].Rows.Count > 0)
                {
                    DeleteUserAccData(TrgetUser);
                    GetUserAccManagerInfo(TrgetUser, out dsTarget);
                    for (int POS = 0; POS < dsSource.Tables[0].Rows.Count; POS++)
                    {
                        drTarget = dsTarget.Tables[0].NewRow();
                        drTarget["USERID"] = TrgetUser.Trim();
                        drTarget["MODULENAME"] = dsSource.Tables[0].Rows[POS]["MODULENAME"];
                        dsTarget.Tables[0].Rows.Add(drTarget);
                        modules += dsSource.Tables[0].Rows[POS]["MODULENAME"].ToString().Trim() + ".........done! \n";
                    }
                    SaveData(ref dsTarget);
                }
                else
                {
                    modules += "Can not get any module to copy............";
                }

                return modules;

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsTarget = null;
                dsSource = null;
            }
        } // end function 
        #endregion

        #region System User
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
        public void DeleteSysUserData(string UserID)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from USER_INFO where USERID='" + UserID.Trim() + "'", true, "1");
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
        }	// end function
        #endregion

        #region User Site Access
        public void searchSites(out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * From USER_SITE";
                strSQl += " order by SITE_GROUP,SITEID";
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
        public void GetUserSitesInfo(string UserCode, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";

            try
            {

                strSql = "Select * from USER_ACC_IN_SITE where USERID='" + UserCode.Trim() + "' ORDER BY SITEID";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }// End function
        public void GetSitesInfo(string UserCode, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";

            try
            {

                strSql = "Select * from USER_ACC_IN_SITE where USERID='" + UserCode.Trim() + "' ORDER BY SITEID";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }// End function
        public void DeleteSiteModule(string strUserID)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from USER_ACC_IN_SITE where USERID='" + strUserID.Trim() + "'", true, "1");
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
        }   // end function
        #endregion

        #region Reset Password
        public int checkLoginStatus(string strExistingUserID, string strExistingPassWd)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            int result = 0;
            string strPWD = "";
            System.Data.DataSet dsRef = new System.Data.DataSet();
            try
            {
                if (!string.IsNullOrWhiteSpace(strExistingUserID))
                {
                    strSql = "SELECT * FROM tblUser Users WHERE Users.UserID='" + strExistingUserID.Trim() + "'";
                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
                    if (dsRef.Tables[0].Rows.Count > 0)
                    {
                        strPWD = BPEnCodeDecodeLib.clsEnCodeDeCode.DecodeHI(dsRef.Tables[0].Rows[0]["Password"].ToString().Trim(), "bapps");
                        result = string.Compare(strPWD, strExistingPassWd) == 0 ? 1 : 0;

                        if (result != 0 && string.IsNullOrWhiteSpace(dsRef.Tables[0].Rows[0]["UserEmailID"].ToString()))
                        {
                            result = 2;
                        }

                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsRef = null;
                objCon = null;
            }
        }//eof
        public bool checkMatchedWithPasswordHistory(string strExistingUserID, string strNewPassWd)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            System.Data.DataSet dsRef = new System.Data.DataSet();
            bool result = false;
            string strExistingPwd = "";
            try
            {

                strSql = "SELECT TOP 5 PWDHistory.[Password] FROM tblPWDHistory PWDHistory where PWDHistory.UserID='" + strExistingUserID.Trim() + "' ORDER BY PWDHistory.LastUpdate DESC";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
                if (dsRef.Tables[0].Rows.Count > 0)
                {
                    foreach (System.Data.DataRow dr in dsRef.Tables[0].Rows)
                    {
                        strExistingPwd = BPEnCodeDecodeLib.clsEnCodeDeCode.DecodeHI(dr["Password"].ToString().Trim(), "bapps");
                        if (string.Compare(strExistingPwd.Trim(), strNewPassWd.Trim(), true) == 0)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsRef = null;
                objCon = null;
            }
            return result;
        }//eof
        public void getBlankDSOfPasswordHistory(out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";

            try
            {

                strSql = "Select * from tblPWDHistory where EntryId='RETURN_BLANK_DS'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSql, out dsRef, false, false, "", "1");
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//eof
        public void updateUserInformation(string strUserID, string strNewPassword, string strEmail="NA")
        {
            ConnectionManager.DAL.ConManager objCon = null;
            string strSql = "";

            try
            {
                strNewPassword = bplib.clsWebLib.RetValidLen(BPEnCodeDecodeLib.clsEnCodeDeCode.EncodeHI(strNewPassword.Trim(), "bapps"), 100).ToString();
                if (string.Compare(strEmail,"NA",true)==0)
                {
                    strSql = "update tblUser set [Password]='" + strNewPassword.Trim() + "', LastUpdate='" + System.DateTime.Today.ToString("yyyy-MM-dd").Trim() + "' where UserID='" + strUserID.Trim() + "'";
                }
                else
                {
                    strSql = "update tblUser set [Password]='" + strNewPassword.Trim() + "', UserEmailID='" + strEmail.Trim() + "', LastUpdate='" + System.DateTime.Today.ToString("yyyy-MM-dd").Trim() + "' where UserID='" + strUserID.Trim() + "'";
                }
                
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper(strSql, true, "1");
                objCon.CommitTransaction();
                objCon.CloseConnection();

            }
            catch (Exception ex)
            {
                //objCon.RollBack();
                throw (ex);
            }
            finally
            {
                //objCon.CloseConnection();
                objCon = null;
            }
        }//eof 
        #endregion
    }
}