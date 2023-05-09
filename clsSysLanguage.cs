using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PWOMS
{
    public static class clsSysLanguage
    {
        public static System.Data.DataTable sysLanguage(string strFlag = "NA")
        {
            System.Data.DataTable tblLang = null;
            //The appSettings element is a NameValueCollection collection of strings
            System.Configuration.Configuration rootWebConfig = null;
            System.Configuration.KeyValueConfigurationElement languageSetting = null;
            try
            {
                tblLang = HttpContext.Current.Cache["LANGUAGE"] as System.Data.DataTable;
                if (tblLang == null || strFlag == "Default")
                {
                    //To obtain configuration settings for the root-level Web configuration, Request.ApplicationPath is passed to the OpenWebConfiguration method.
                    rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.HttpContext.Current.Request.ApplicationPath);//"/TradingOrder"
                    if (rootWebConfig.AppSettings.Settings.Count > 0)
                    {
                        languageSetting = rootWebConfig.AppSettings.Settings["LanguageSettings"];
                        if (languageSetting != null)
                        {
                            if (languageSetting.Value.ToString().Trim() != "" && languageSetting.Value.ToString() != "NA")
                            {
                                getDataOfLanguage(languageSetting.Value.ToString().Trim(), out tblLang);
                                if (tblLang.Rows.Count > 0)
                                {
                                    HttpContext.Current.Cache.Insert("LANGUAGE", tblLang, null, DateTime.Now.AddHours(1), System.Web.Caching.Cache.NoSlidingExpiration);
                                }
                                else
                                {
                                    tblLang = null;
                                }
                            }

                        }
                    }
                }
                return tblLang;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                //
            }
        }//eof
        private static void getDataOfLanguage(string strFLAG, out System.Data.DataTable dtRef)
        {
            string strSQl = "";
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                if (string.Compare(strFLAG, "NA", true) != 0)
                {
                    strSQl = "select ScreenName,CtrlID,ISNULL(LnFRN,(ISNULL(LnEng,'MISSING'))) as LNG from tblAppLanguage";
                }

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataTableThroughAdapter(strSQl, out dtRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function

        #region Message Language management
        public static string AppMessageText(string code, string LANG_APP_SWITCH)
        {
            string strMsg = "";
            switch (LANG_APP_SWITCH)
            {
                case "TC":
                    //strMsg = TC_Message(code);
                    break;
                case "SC":
                    //strMsg = SC_Message(code);
                    break;
                case "JP":
                    //strMsg = JP_Message(code);
                    break;
                default:
                    strMsg = EN_Message(code);
                    break;
            }
            return strMsg;
        }
        private static string EN_Message(string code)
        {
            string strMsg = "";
            switch (code)
            {
                #region Common Message

                case "M0001":
                    strMsg = "Data save sucessfully.";
                    break;

                #endregion

                #region Master Data

                case "AA10001":
                    strMsg = "Define the Entry ID";
                    break;
                #endregion

                #region Fixed Entiry Var Manager

                case "AB10001":
                    strMsg = "Define the Entry Type";
                    break;
                case "AB10002":
                    strMsg = "Define the Code .... (max length 150)";
                    break;
                case "AB10003":
                    strMsg = "Define the Decription .... (max length 150)";
                    break;
                case "AB10004":
                    strMsg = "The Value Code Cannot be text";
                    break;
                case "AB10005":
                    strMsg = "Select the Code";
                    break;

                #endregion

                default:
                    break;
            }
            return strMsg;
        }//eof

        #endregion
    }
}