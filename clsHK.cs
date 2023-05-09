using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace PWOMS
{
    public class clsHK
    {
        public clsHK()
        {
        }//initialization
        public void SaveData(ref System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.SaveDataSetThroughAdapter(ref dsRef, false, "1");
                //updateing ...
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

        #region Update Unit
        public void GetPopUpUnit(string str, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                if (str == null)
                {
                    strSQL = "Select A.UnitSetDesc,B.UnitDesc from tblSMSUnitSetMaster A,tblSMSUnit B where A.UnitSet=B.UnitSet";
                }
                else
                {
                    strSQL = "Select A.UnitSetDesc,B.UnitDesc from tblSMSUnitSetMaster A,tblSMSUnit B where A.UnitSet=B.UnitSet and B.UnitCode='" + str + "'";
                }
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetPopUpUnitDesc(out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select UnitCode,UnitDesc from tblSMSUnit";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function

        public void GetUnitSets(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT * FROM tblSMSUnitSetMaster  WHERE SiteId='" + strSiteId + "' order by UnitSetDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetUnits(string strUnitSetDesc, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select A.UnitSet,A.UnitSetDesc,B.* " +
                        "from tblSMSUnitSetMaster A, tblSMSUnit B " +
                        "where A.UnitSet=B.UnitSet and A.UnitSet='" + strUnitSetDesc.Trim() + "' AND A.SiteId='" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetUnit(string strUnitSet, string strUnitCode, out System.Data.DataSet dsRef)
        {
            string strSQL = null;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from tblSMSUnit where UnitSet='" + strUnitSet.Trim() + "' and UnitCode='" + strUnitCode.Trim() + "'";// AND SiteId='" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetUnitCode(string strSiteId, string strUnitSet, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from tblSMSUnit where UnitSet='" + strUnitSet.Trim() + "' AND SiteId='" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetUnitCodeForGroupItem(string strGroupItemID,string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT A.UnitCode,A.UnitShortCode " +
                         "FROM tblSMSUnit A "+
                         "LEFT OUTER JOIN tblSMSMaterialGroup B ON A.UnitSet=B.UnitSet "+
                         "WHERE GroupItemID='" + strGroupItemID + "' AND A.SiteID='" + strSiteId + "'";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetUnitCodeRelatedFactor(string strUnitCode, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT UnitCode,UnitDesc,RelativeFactor " +
                         "FROM tblSMSUnit  " +
                         "WHERE UnitCode='" + strUnitCode + "' AND SiteID='" + strSiteId + "'";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void SaveUnits(string strSiteId, ref System.Data.DataSet dsRef)
        {
            string strSQL = null;
            string strUnitSetDesc = null;
            string strUnitSet = null;
            string strUnitCode = null;
            //string strUnitShortCode = null;

            bplib.clsGenID objGenID = null;

            System.Data.DataSet dsUnitSetMaster;
            System.Data.DataTable dtUnitSetMaster;
            System.Data.DataRow drUnitSetMaster;
            System.Data.DataView dvUnitSetMaster;

            System.Data.DataSet dsUnit;
            System.Data.DataTable dtUnit;
            System.Data.DataRow drUnit;
            System.Data.DataView dvUnit;

            ConnectionManager.DAL.ConManager objCon = null;

            try
            {
                if (dsRef.Tables[0].Rows.Count > 0)
                {
                    strUnitSetDesc = dsRef.Tables[0].Rows[0]["UnitSetDesc"].ToString().Trim();

                    objCon = new ConnectionManager.DAL.ConManager("1");
                    objCon.OpenConnection("1");
                    objCon.BeginTransaction();


                    strSQL = "SELECT * FROM tblSMSUnitSetMaster where UnitSetDesc='" + strUnitSetDesc.Trim() + "' AND SiteId='" + strSiteId + "'";
                    objCon.OpenDataSetThroughAdapter(strSQL, out dsUnitSetMaster, true, "1");

                    dtUnitSetMaster = dsUnitSetMaster.Tables[0];
                    dvUnitSetMaster = new DataView();
                    dvUnitSetMaster.Table = dtUnitSetMaster;

                    if (dsUnitSetMaster.Tables[0].Rows.Count == 0)
                    {
                        objGenID = new bplib.clsGenID();
                        objGenID.GenID(System.DateTime.Now.ToShortDateString().ToString(), "UNITSET", out strUnitSet);
                        strUnitSet = "US" + strUnitSet;
                        drUnitSetMaster = dtUnitSetMaster.NewRow();

                        drUnitSetMaster["UnitSet"] = bplib.clsWebLib.RetValidLen(strUnitSet.Trim(), 20);
                        drUnitSetMaster["SiteId"] = bplib.clsWebLib.RetValidLen(strSiteId.Trim(), 20);
                        drUnitSetMaster["UnitSetDesc"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UnitSetDesc"].ToString().Trim(), 100);
                        drUnitSetMaster["AddedBy"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["AddedBy"].ToString().Trim(), 20);
                        drUnitSetMaster["DateAdded"] = bplib.clsWebLib.DateData_AppToDB(dsRef.Tables[0].Rows[0]["DateAdded"].ToString().Trim(), bplib.clsWebLib.DB_DATE_FORMAT);
                        drUnitSetMaster["UpdatedBy"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UpdatedBy"].ToString().Trim(), 20);
                        drUnitSetMaster["DateUpdated"] = bplib.clsWebLib.DateData_AppToDB(dsRef.Tables[0].Rows[0]["DateUpdated"].ToString().Trim(), bplib.clsWebLib.DB_DATE_FORMAT);
                        dtUnitSetMaster.Rows.Add(drUnitSetMaster);
                    }
                    else
                    {
                        strUnitSet = dsUnitSetMaster.Tables[0].Rows[0]["UnitSet"].ToString().Trim();
                        drUnitSetMaster = dtUnitSetMaster.Rows[0];
                        drUnitSetMaster.BeginEdit();
                        drUnitSetMaster["UnitSetDesc"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UnitSetDesc"].ToString().Trim(), 100);
                        drUnitSetMaster["UpdatedBy"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UpdatedBy"].ToString().Trim(), 20);
                        drUnitSetMaster["DateUpdated"] = bplib.clsWebLib.DateData_AppToDB(dsRef.Tables[0].Rows[0]["DateUpdated"].ToString().Trim(), bplib.clsWebLib.DB_DATE_FORMAT);
                        drUnitSetMaster.EndEdit();
                    }

                    #region Unit

                    strUnitCode = dsRef.Tables[0].Rows[0]["UnitCode"].ToString().Trim();
                    //strUnitShortCode = dsRef.Tables[0].Rows[0]["UnitShortCode"].ToString().Trim();

                    strSQL = "SELECT * FROM tblSMSUnit where UnitCode='" + strUnitCode.Trim() + "'";
                    objCon.OpenDataSetThroughAdapter(strSQL, out dsUnit, true, "1");

                    dtUnit = dsUnit.Tables[0];
                    dvUnit = new DataView();
                    dvUnit.Table = dtUnit;

                    if (dsUnit.Tables[0].Rows.Count == 0)
                    {
                        objGenID = new bplib.clsGenID();
                        objGenID.GenID(System.DateTime.Now.ToShortDateString().ToString(), "UNIT", out strUnitCode);
                        strUnitCode = "U" + strUnitCode;
                        drUnit = dtUnit.NewRow();

                        drUnit["UnitCode"] = bplib.clsWebLib.RetValidLen(strUnitCode.Trim(), 10);
                        drUnit["UnitSet"] = bplib.clsWebLib.RetValidLen(strUnitSet.Trim(), 20);
                        drUnit["SiteId"] = bplib.clsWebLib.RetValidLen(strSiteId.Trim(), 20);
                        drUnit["UnitShortCode"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UnitShortCode"].ToString().Trim(), 10);
                        drUnit["UnitDesc"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UnitDesc"].ToString().Trim(), 50);
                        drUnit["RelativeFactor"] = bplib.clsWebLib.GetNumData(dsRef.Tables[0].Rows[0]["RelativeFactor"].ToString().Trim());
                        drUnit["AddedBy"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["AddedBy"].ToString().Trim(), 20);
                        drUnit["DateAdded"] = bplib.clsWebLib.DateData_AppToDB(dsRef.Tables[0].Rows[0]["DateAdded"].ToString().Trim(), bplib.clsWebLib.DB_DATE_FORMAT);
                        drUnit["UpdatedBy"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UpdatedBy"].ToString().Trim(), 20);
                        drUnit["DateUpdated"] = bplib.clsWebLib.DateData_AppToDB(dsRef.Tables[0].Rows[0]["DateUpdated"].ToString().Trim(), bplib.clsWebLib.DB_DATE_FORMAT);
                        dtUnit.Rows.Add(drUnit);
                    }
                    else
                    {
                        drUnit = dtUnit.Rows[0];
                        drUnit.BeginEdit();
                        drUnit["UnitShortCode"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UnitShortCode"].ToString().Trim(), 10);
                        drUnit["UnitDesc"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UnitDesc"].ToString().Trim(), 50);
                        drUnit["RelativeFactor"] = bplib.clsWebLib.GetNumData(dsRef.Tables[0].Rows[0]["RelativeFactor"].ToString().Trim());
                        drUnit["UpdatedBy"] = bplib.clsWebLib.RetValidLen(dsRef.Tables[0].Rows[0]["UpdatedBy"].ToString().Trim(), 20);
                        drUnit["DateUpdated"] = bplib.clsWebLib.DateData_AppToDB(dsRef.Tables[0].Rows[0]["DateUpdated"].ToString().Trim(), bplib.clsWebLib.DB_DATE_FORMAT);
                        drUnit.EndEdit();
                    }
                    #endregion

                    objCon.SaveDataSetThroughAdapter(ref dsUnitSetMaster, true, "1");
                    objCon.SaveDataSetThroughAdapter(ref dsUnit, true, "1");

                    objCon.CommitTransaction();

                    //objCon = new ConnectionManager.DAL.ConManager("1");
                    //objCon.SaveDataSetThroughAdapter(ref dsRef, false, "1");
                }
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

        } //End Function	
        public void DeleteUnit(string strUnitCode)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from tblSMSUnit where UnitCode='" + strUnitCode + "'", true, "1");
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
        }//End of function
        public void DeleteUnitSet(string strUnitSet)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from tblSMSUnitSetMaster where UnitSet='" + strUnitSet + "'", true, "1");
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
        }//End of function
        public void GetUnitInformation(string strUnitCode, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from tblSMSUnit where UnitCode='" + strUnitCode.Trim() + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetAllUnit(out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from tblSMSUnit";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        //Start Edit By SHOHEL
        public void GetUnitCount(string strUnitShortCode, string strUnitDesc, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT UnitCode, UnitSet, UnitShortCode, UnitDesc, RelativeFactor, AddedBy, DateAdded, UpdatedBy, DateUpdated, msrepl_tran_version " +
                         "FROM dbo.tblSMSUnit " +
                         "WHERE SiteId='" + strSiteId + "' AND ((UnitShortCode ='" + strUnitShortCode + "') OR (UnitDesc ='" + strUnitDesc + "')) ";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void SmallUnitConversion(double Qty, string UnitCode, double Rate, out double SmallQty, out string SmallUnitCode, out double SmallUnitFactor, out double SmallRate)
        {
            System.Data.DataSet dsUnit = null;
            System.Data.DataSet dsSmallUnit = null;

            string strSQL;
            ConnectionManager.DAL.ConManager objCon;

            try
            {
                strSQL = "Select * from tblSMSUnit where UnitCode='" + UnitCode + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsUnit, false, "1");

                strSQL = "SELECT UnitCode, UnitSet, UnitDesc, RelativeFactor AS MinFactor " +
                         " FROM         dbo.tblSMSUnit " +
                         " WHERE     (UnitSet = '" + dsUnit.Tables[0].Rows[0]["UnitSet"].ToString() + "') " +
                         " GROUP BY UnitCode, UnitSet, UnitDesc,RelativeFactor " +
                         " ORDER BY MinFactor";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsSmallUnit, false, "1");

                if (dsSmallUnit.Tables[0].Rows[0]["MinFactor"].ToString() != "0")
                {
                    SmallQty = (Qty * Convert.ToDouble(dsUnit.Tables[0].Rows[0]["RelativeFactor"].ToString())) / Convert.ToDouble(dsSmallUnit.Tables[0].Rows[0]["MinFactor"].ToString());
                    SmallUnitCode = dsSmallUnit.Tables[0].Rows[0]["UnitCode"].ToString();
                }
                else
                {
                    Exception ex = new Exception(dsSmallUnit.Tables[0].Rows[0]["UnitDesc"].ToString() + " Unit Factor Cann't Be Zero");
                    throw (ex);
                }

                if (dsUnit.Tables[0].Rows[0]["RelativeFactor"].ToString() != "0")
                {
                    SmallRate = (Rate * Convert.ToDouble(dsSmallUnit.Tables[0].Rows[0]["MinFactor"].ToString())) / Convert.ToDouble(dsUnit.Tables[0].Rows[0]["RelativeFactor"].ToString());
                }
                else
                {
                    Exception ex = new Exception(dsUnit.Tables[0].Rows[0]["UnitDesc"].ToString() + " Unit Factor Cann't Be Zero");
                    throw (ex);
                }

                SmallUnitFactor = Convert.ToDouble(dsSmallUnit.Tables[0].Rows[0]["MinFactor"].ToString());

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsUnit = null;
                dsSmallUnit = null;
            }


        }

        //End Edit By SHOHEL
        //Added By ATIQ
        public void SmallUnitConversionReverse(double SmallQty, string SmallUnitCode, double SmallRate, out double ReQty, string ReUnitCode, out double ReRate)
        {
            System.Data.DataSet dsUnit = null;
            System.Data.DataSet dsSmallUnit = null;

            string strSQL;
            ConnectionManager.DAL.ConManager objCon;

            try
            {
                strSQL = "Select * from tblSMSUnit where UnitCode='" + SmallUnitCode + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsSmallUnit, false, "1");

                strSQL = "Select * from tblSMSUnit where UnitCode='" + ReUnitCode + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsUnit, false, "1");

                if (dsUnit.Tables[0].Rows[0]["RelativeFactor"].ToString() != "0")
                {
                    ReQty = (SmallQty * Convert.ToDouble(dsSmallUnit.Tables[0].Rows[0]["RelativeFactor"].ToString())) / Convert.ToDouble(dsUnit.Tables[0].Rows[0]["RelativeFactor"].ToString());
                    //SmallUnitCode = dsSmallUnit.Tables[0].Rows[0]["UnitCode"].ToString();
                }
                else
                {
                    Exception ex = new Exception(dsSmallUnit.Tables[0].Rows[0]["UnitDesc"].ToString() + " Unit Factor Cann't Be Zero");
                    throw (ex);
                }

                if (dsSmallUnit.Tables[0].Rows[0]["RelativeFactor"].ToString() != "0")
                {
                    ReRate = (SmallRate * Convert.ToDouble(dsSmallUnit.Tables[0].Rows[0]["RelativeFactor"].ToString())) / Convert.ToDouble(dsUnit.Tables[0].Rows[0]["RelativeFactor"].ToString());
                }
                else
                {
                    Exception ex = new Exception(dsUnit.Tables[0].Rows[0]["UnitDesc"].ToString() + " Unit Factor Cann't Be Zero");
                    throw (ex);
                }

                //SmallUnitFactor = Convert.ToDouble(dsSmallUnit.Tables[0].Rows[0]["MinFactor"].ToString());

            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                dsUnit = null;
                dsSmallUnit = null;
            }


        }

        public void GetUnitSetForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select UnitSet,UnitSetDesc from tblSMSUnitSetMaster Where Siteid='"+ strSiteId + "' Order By UnitSet";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
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
        public void GetDataOftblSMSUnit(string ID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "SELECT * FROM tblSMSUnit WHERE UnitCode='" + ID.Trim() + "' AND SiteId='" + strSiteId + "'";
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
        public void GetDataOftblSMSUnitByCode(string ID, string strCode, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                if (ID.Trim() == "")
                {
                    strSQl = "SELECT * FROM tblSMSUnit WHERE UnitShortCode='" + strCode.Trim() + "' AND SiteId='" + strSiteId + "'";
                }
                else
                {
                    strSQl = "SELECT * FROM tblSMSUnit WHERE UnitCode!='" + ID + "' AND UnitShortCode='" + strCode.Trim() + "' AND SiteId='" + strSiteId + "'";
                }
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF
        #endregion

        #region Update Item Group
        public void GetItemGroup(bool Fabric, bool Trims, bool Consumable, bool Chemical, bool Capital, out System.Data.DataSet dsRef)
        {
            string strSQL = "";
            string ItemNature = "";

            ConnectionManager.DAL.ConManager objCon;
            try
            {

                if (Fabric == true)
                    ItemNature = ItemNature + "'FABRIC',";
                if (Trims == true)
                    ItemNature = ItemNature + "'TRIM',";
                if (Consumable == true)
                    ItemNature = ItemNature + "'CONSUMABLE',";
                if (Chemical == true)
                    ItemNature = ItemNature + "'CHEMICAL',";
                if (Capital == true)
                    ItemNature = ItemNature + "'CAPITAL',";


                if (ItemNature.ToString().Length > 0)
                {
                    if (ItemNature.EndsWith(","))
                        ItemNature = ItemNature.Substring(0, ItemNature.Length - 1);
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where Type in (" + ItemNature + ")  Order By GroupItemDesc";
                }
                else
                    strSQL = "SELECT * FROM tblSMSMaterialGroup  Order By GroupItemDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end function

        public void GetItemGroups(out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT * FROM tblSMSMaterialGroup order by GroupItemDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end function

        public void GetDistinctItemNature(out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT Distinct Type FROM tblSMSMaterialGroup order by Type ASC";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end function

        /// <summary>
        /// Get Group Info for Item/Store
        /// Create By Atiqur
        /// If Parameter is Item/Store then output specfic Data other wise returns all group
        /// Parameter also CAPITAL/SERVICE/OTHERS To return This Type of Nature Group
        /// </summary>
        /// <param name="dsflag">Item/Store</param>
        /// <param name="dsRef"></param>
        public void GetItemGroups(string dsflag, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                if (dsflag == "Item")
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where ItemDetailRequire='True' order by GroupItemDesc";
                else if (dsflag == "Store")
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where IsCapital='False' order by GroupItemDesc";
                else if (dsflag == "CAPITAL")
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where Type ='CAPITAL' order by GroupItemDesc";
                else if (dsflag == "SERVICE")
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where Type ='SERVICE' order by GroupItemDesc";
                else if (dsflag == "OTHERS")
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where Type <>'SERVICE' and Type <>'CAPITAL' order by GroupItemDesc";
                else
                    strSQL = "SELECT * FROM tblSMSMaterialGroup order by GroupItemDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end function

        public void GetItemGroupsForReceive(string strGroupType, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = " SELECT TOP (100) PERCENT GroupItemID, DisplayItemCodePrefix, GroupItemDesc, UnitSet, UnitCode, Type, AddedBy, DateAdded, UpdatedBy, DateUpdated FROM tblSMSMaterialGroup " +
                         " WHERE (IsCapital = 'False') GROUP BY GroupItemID, DisplayItemCodePrefix, GroupItemDesc, UnitSet, UnitCode, Type, AddedBy, DateAdded, UpdatedBy, DateUpdated HAVING (Type = '" + strGroupType + "') ORDER BY GroupItemDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end function

        public void GetItemGroups(bool strGrouplevel, string dsflag, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                if (strGrouplevel)
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where ItemDetailRequire='False' and IsCapital='False' order by GroupItemDesc";
                else
                    strSQL = "SELECT * FROM tblSMSMaterialGroup where ItemDetailRequire='True' and IsCapital='False' order by GroupItemDesc";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//end function

        public void GetItemGroup(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select A.GroupItemID,A.DisplayItemCodePrefix,A.GroupItemDesc,A.ItemDetailRequire,A.Type,B.UnitSetDesc as UnitSet,C.UnitDesc as UnitCode " +
                         "from tblSMSMaterialGroup A,tblSMSUnitSetMaster B, tblSMSUnit C " +
                         "where A.UnitSet=B.UnitSet And A.UnitCode=C.UnitCode AND A.SiteId='" + strSiteId + "' order by A.GroupItemDesc ASC";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function

        public void GetItemGroupCapital(out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select A.GroupItemID,A.DisplayItemCodePrefix,A.GroupItemDesc,A.ItemDetailRequire,A.Type,B.UnitSetDesc as UnitSet,C.UnitDesc as UnitCode from tblSMSMaterialGroup A,tblSMSUnitSetMaster B, Unit C where A.UnitSet=B.UnitSet And A.UnitCode=C.UnitCode And A.IsCapital='True' order by A.GroupItemDesc ASC";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function

        public void GetUniqueItemGroup(string strItemGroupID, out System.Data.DataSet dsRef)
        {
            string strSQL = null;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from tblSMSMaterialGroup where GroupItemID='" + strItemGroupID.Trim() + "' order by GroupItemDesc ASC";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function

        public void PrefixCheck(string strprefix, string stritemgroupdes, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL = null;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from tblSMSMaterialGroup where SiteId='" + strSiteId + "' AND (DisplayItemCodePrefix='" + strprefix.Trim() + "' or GroupItemDesc='" + stritemgroupdes.Trim() + "') order by GroupItemDesc ASC";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//End of function

        public void DeleteItemGroupData(string ID)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from tblSMSMaterialGroup where GroupItemID='" + ID + "'", true, "1");
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

        public void GetAllUnit(string strGroupItemID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT  U.UnitCode,U.UnitDesc,U.RelativeFactor,GI.UnitCode as UnitCodeDefault,GI.CommercialUnitCode from tblSMSMaterialGroup as GI, " +
                    "tblSMSUnitSetMaster as UM,Unit as U where GI.UnitSet=UM.UnitSet and UM.UnitSet=U.UnitSet" +
                    " and GI.GroupItemID='" + strGroupItemID + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }///end function

        public void GetUnit(string strGroupItemID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT  U.UnitCode,U.UnitDesc,U.RelativeFactor from tblSMSMaterialGroup as GI, tblSMSUnitSetMaster as UM,Unit as U where GI.UnitSet=UM.UnitSet and UM.UnitSet=U.UnitSet";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }///end function

        public void GetTrimGroupItem(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                //strSQL = "Select * from GroupItem Where Type!='FABRIC' AND SiteId='" + strSiteId + "' Order By GroupItemDesc";
                strSQL = "Select * from tblSMSMaterialGroup Where Type ='TRIM' AND SiteId='" + strSiteId + "' Order By GroupItemDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }//EOF
        public void GetUnitOfGroupItem(string strUnitSet, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT  DISTINCT U.UnitCode,U.UnitDesc,U.RelativeFactor " +
                         "FROM tblSMSMaterialGroup GI " +
                         "LEFT OUTER JOIN Unit U ON GI.UnitSet=U.UnitSet " +
                         "WHERE GI.SiteId='" + strSiteId + "' AND GI.UnitSet='" + strUnitSet + "'";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }///EOF
        public void GetGroupItemUnitSet(string strGroupItemID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQL = "SELECT UnitSet FROM tblSMSMaterialGroup " +
                         "WHERE GroupItemID='" + strGroupItemID + "'";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }///EOF
        public void GetRelativeFactor(string strUnitCode, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "SELECT RelativeFactor FROM Unit " +
                         "WHERE UnitCode='" + strUnitCode + "'";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }///EOF
        ///
        #region For HK
        public void GetAllGroupByFileRef(string strFileRefID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = " SELECT     dbo.BOMItemAttribute.FileRefID, dbo.tblSMSMaterialGroup.GroupItemID, dbo.tblSMSMaterialGroup.GroupItemDesc " +
                         " FROM         dbo.BOMItemAttribute INNER JOIN dbo.tblSMSMaterialGroup ON dbo.BOMItemAttribute.GroupItemID = dbo.tblSMSMaterialGroup.GroupItemID" +
                         " WHERE dbo.BOMItemAttribute.FileRefID='" + strFileRefID + "'" +
                         " GROUP BY  dbo.BOMItemAttribute.FileRefID, dbo.tblSMSMaterialGroup.GroupItemID, dbo.GrotblSMSMaterialGroupupItem.GroupItemDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQL, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            {
                throw (ex);
            }
            finally
            {
                objCon = null;
            }
        }///end function
        #endregion

        #endregion

        #region Update Currency

        public void GetDataOftblSMSCurrency(string ID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCurrency where CurrencyCode='" + ID.Trim() + "' AND SiteID='" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void DupicateCurrencyDescCheck(string CurrencyCode, string CurrencyDesc, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCurrency where CurrencyDesc='" + CurrencyDesc.Trim() + "' AND SiteID='" + strSiteId + "' and CurrencyCode <>'" + CurrencyCode + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetDataOftblSMSCurrencyForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCurrency  Where SiteID='" + strSiteId + "' Order By CurrencyDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void DeleteDataOftblSMSCurrency(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where CurrencyCode='" + ID + "'", true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                objCon.RollBack();
                if (ex.Number == 547)
                {
                    throw new Exception("Data cannot be deleted as it is being used by other tables..");
                }
                else
                {
                    throw (ex);
                }
            }
            catch (Exception aex)
            {
                throw (aex);
            }
            finally
            {
                objCon.CloseConnection();
                objCon = null;
            }
        }	// end function
        public void SearchCurrencyData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = "Select CurrencyCode,CurrencyDesc,CurrencyLongDesc,ConvertFactor,convert(varchar(20),DateUpdated,105) as DateUpdated from tblSMSCurrency";
                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";
                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and DateUpdated between '" + strfromdate + "' and '" + strtodate + "' order by DateUpdated desc,CurrencyCode asc";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by DateUpdated desc";
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

        public void GetDataOfBseCurrencyForCombo(string strSiteId,out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCurrency Where CurrencyDesc='USD' AND SiteID = '"+strSiteId+"'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetDataOfCustSupCurrencyForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCurrency Where CurrencyDesc<>'USD' AND SiteID = '" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function
        public void GetDataOfCurrencyForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCurrency Where SiteID = '" + strSiteId + "' Order By CurrencyDesc";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function
        #endregion

        #region Update Day Wise Rate Chart
        public void GetDataOftblSMSCurrencyRate(string ID, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCurrencyRate where EntryID='" + ID.Trim() + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End function
        public void DeleteDataOftblSMSCurrencyRate(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where EntryID='" + ID + "'", true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                objCon.RollBack();
                if (ex.Number == 547)
                {
                    throw new Exception("Data cannot be deleted as it is being used by other tables..");
                }
                else
                {
                    throw (ex);
                }
            }
            catch (Exception aex)
            {
                throw (aex);
            }
            finally
            {
                objCon.CloseConnection();
                objCon = null;
            }
        }//End function
        public void GetDataOfDayWiseRateChartForGrid(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQl = "Select A.EntryID,Convert(Varchar(20),A.EntryDate,106)EntryDate,A.ConvertFactor, " +
                         "B.BaseCurrencyCode,C.CustSupCurrencyCode " +
                         "FROM tblSMSCurrencyRate A " +
                         "Left Outer Join (Select CurrencyCode,CurrencyDesc As BaseCurrencyCode From tblSMSCurrency) B ON A.BaseCurrencyCode=B.CurrencyCode " +
                         "Left Outer Join (Select CurrencyCode,CurrencyDesc As CustSupCurrencyCode From tblSMSCurrency) C ON A.CustSupCurrencyCode=C.CurrencyCode " +
                         "Where A.SiteId='" + strSiteId + "'Order By C.CustSupCurrencyCode,A.EntryDate Desc,A.EntryID Desc";

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

        public double GetUpdateExchangeRate(string strCurrencyCode)
        {
            System.Data.DataSet dsLocal = null;
            ConnectionManager.DAL.ConManager objCon = null;
            double dblExcRate = 0;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();

                string strSQl = "Select ConvertFactor From tblSMSCurrencyRate " +
                                "Where CustSupCurrencyCode='" + strCurrencyCode + "' AND EntryDate In (Select MAX(EntryDate)From tblSMSCurrencyRate Where CustSupCurrencyCode='" + strCurrencyCode + "') ";

                objCon.OpenDataSetThroughAdapter(strSQl, out dsLocal, true, "1");



                if (dsLocal.Tables[0].Rows.Count > 0)
                    dblExcRate = Convert.ToDouble(bplib.clsWebLib.GetNumData(dsLocal.Tables[0].Rows[0]["ConvertFactor"].ToString()));

                //objCon.CommitTransaction();
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
            return dblExcRate;
        }//EOF
        #endregion

        #region Update SMSItemMaster
        public void GetDataOfSMSItemMaster(string ID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSItemMaster where ItemCode='" + ID.Trim() + "' AND SiteId='" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function
        #endregion

        #region Update Customer/ Supplier Info
        public void GetDataOfCustomerSupplier(string ID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCustSup where SysRefID='" + ID.Trim() + "'AND SiteId='" + strSiteId + "'";
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
        public void GetDataOfCustomerSupplierByCode(string ID, string strCode, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                if (ID.Trim() == "")
                {
                    strSQl = "SELECT * FROM tblSMSCustSup WHERE Code='" + strCode.Trim() + "'AND SiteId='" + strSiteId + "'";
                }
                else
                {
                    strSQl = "SELECT * FROM tblSMSCustSup WHERE SysRefID!='" + ID + "' AND Code='" + strCode.Trim() + "'AND SiteId='" + strSiteId + "'";
                }
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF
        public void DeleteDataOfCustomerSupplier(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where SysRefID='" + ID + "'", true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                objCon.RollBack();
                if (ex.Number == 547)
                {
                    throw new Exception("Data cannot be deleted as it is being used by other tables..");
                }
                else
                {
                    throw (ex);
                }
            }
            catch (Exception aex)
            {
                throw (aex);
            }
            finally
            {
                objCon.CloseConnection();
                objCon = null;
            }
        }	//eof
        public void SearchCustomerSupplierData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = "Select SysRefID,Code,Type,CompanyName,ContactName,ContactPhone,ContactFax,ContactEmail,Status,convert(varchar(20),DateUpdated,105) as DateUpdated from tblSMSCustSup ";
                strSql = strSql + " where  SiteId='" + strSiteId.Trim() + "'";
                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and DateUpdated between '" + strfromdate + "' and '" + strtodate + "' order by DateUpdated desc,SysRefID asc";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by DateUpdated desc";
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
        }//eof
        public void SearchCustomerData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = "Select SysRefID,Code,CompanyName,ContactName,ContactPhone,ContactFax,ContactEmail,Status,convert(varchar(20),DateUpdated,105) as DateUpdated from tblSMSCustSup ";
                strSql = strSql + " where Type='CUSTOMER' AND SiteId='" + strSiteId.Trim() + "'";
                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and DateUpdated between '" + strfromdate + "' and '" + strtodate + "' order by DateUpdated desc,SysRefID asc";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by DateUpdated desc";
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
        }//eof
        public void SearchSupplierData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = "Select SysRefID,Code,CompanyName,ContactName,ContactPhone,ContactFax,ContactEmail,Status,convert(varchar(20),DateUpdated,105) as DateUpdated from tblSMSCustSup ";
                strSql = strSql + " where Type='SUPPLIER' AND SiteId='" + strSiteId.Trim() + "'";
                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and DateUpdated between '" + strfromdate + "' and '" + strtodate + "' order by DateUpdated desc,SysRefID asc";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by DateUpdated desc";
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
        }//eof

        public void GetDataOfCustomerForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCustSup where Type='CUSTOMER' AND SiteId='" + strSiteId + "' Order By CompanyName";
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
        #endregion

        #region Update SMSMarketier
        public void GetDataOfSMSMarketierForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select upper(userId) as userID,upper(username) as username from tblSMSMarketier where SiteId='" + strSiteId + "'";
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
        #endregion

        #region Update SMSMaterialGroup
        public void GetDataOfSMSMaterialGroup(string strGroupItemID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select GroupItemID,GroupItemDesc from tblSMSMaterialGroup where GroupItemID='" + strGroupItemID + "' AND SiteId='" + strSiteId + "'";
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
        public void GetDataOfSMSMaterialGroupForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select GroupItemID,DisplayItemCodePrefix from tblSMSMaterialGroup where SiteId='" + strSiteId + "'";
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
        #endregion

        #region Update SMSUnit
        public void GetUnitSet(string ID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSUnitSetMaster where UnitSet='" + ID.Trim() + "' AND SiteID='" + strSiteId + "'";
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
        public void DeleteDataOftblSMSUnitSetMaster(string ID)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from tblSMSUnitSetMaster where UnitSet='" + ID + "'", true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                objCon.RollBack();
                if (ex.Number == 547)
                {
                    throw new Exception("Data cannot be deleted as it is being used by other tables..");
                }
                else
                {
                    throw (ex);
                }
            }
            catch (Exception aex)
            {
                throw (aex);
            }
            finally
            {
                objCon.CloseConnection();
                objCon = null;
            }
        }	// end function
        public void DupicateUnitSetDescCheck(string CurrencyCode, string CurrencyDesc, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSUnitSetMaster where UnitSetDesc='" + CurrencyDesc.Trim() + "' AND SiteID='" + strSiteId + "' and UnitSet <>'" + CurrencyCode + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//End of function        
        public void SearchUnitSetData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT distinct UnitSet,UnitSetDesc,AddedBy,
                IIF(convert(varchar(20), DateAdded, 105) = '01-01-1901', NULL, convert(varchar(20), DateAdded, 105)) as DateAdded,
                UpdatedBy,
                IIF(convert(varchar(20), DateUpdated, 105) = '01-01-1901', NULL, convert(varchar(20), DateUpdated, 105)) as DateUpdated
                FROM tblSMSUnitSetMaster";
                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";
                if (strKey == "")
                {
                    //strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    //strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " order by UnitSet";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by UnitSet";
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
        public void GetDataOfSMSUnitForCombo(string strGroupItemID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "Select A.UnitCode,A.UnitShortCode From tblSMSUnit A "+
                         "Left Outer Join tblSMSMaterialGroup B ON A.UnitSet=B.UnitSet "+
                         "Where B.GroupItemID='" + strGroupItemID + "' AND A.SiteID='" + strSiteId + "'";

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
        #endregion

        #region Update Company Info
        public void GetDataOfCompanyInfo(string ID, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSCompanyInfo where CompanyCode='" + ID.Trim() + "'AND SiteId='" + strSiteId + "'";
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

        public void DeleteDataOfCompanyInfo(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where CompanyCode='" + ID + "'", true, "1");
                objCon.CommitTransaction();
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                objCon.RollBack();
                if (ex.Number == 547)
                {
                    throw new Exception("Data cannot be deleted as it is being used by other tables..");
                }
                else
                {
                    throw (ex);
                }
            }
            catch (Exception aex)
            {
                throw (aex);
            }
            finally
            {
                objCon.CloseConnection();
                objCon = null;
            }
        }	//eof
        #endregion

        #region Update Marketers Info
        public void GetDataOfMarketers(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblSMSMarketier where UserId='" + strUserId.Trim() + "' AND SiteId='" + strSiteId + "'";
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
        public void GetDataOfMarketersForCombo(string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select upper(userId) as userID,upper(username) as username from tblSMSMarketier where SiteId='" + strSiteId + "'";
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
        public void DeleteDataOfMarketers(string ID, string strSiteId, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where UserId='" + ID + "' AND SiteId='" + strSiteId + "'", true, "1");
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
        }	//eof
        public void SearchMarketersData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT distinct UserId,UserName,UserCompany,UserUnit,UserDepartment,UserDesignation,HOD,ROD1,UserFlag,SITEID,
                IIF(convert(varchar(20), UpdateOn, 105)='01-01-1901',NULL,convert(varchar(20), UpdateOn, 105)) as UpdateOn
                FROM  tblSMSMarketier";

                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by UserId";
                    
                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by UserId";
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
        }//eof

        public bool CheckMarketingAccount_In_tblSMSOrderMaster(string strMarketingAccount, string strSiteId, string tblName)
        {
            string strSQl;
            bool fileExist = false;
            System.Data.DataSet dsRef;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "SELECT * FROM " + tblName + " WHERE MarketingAccount='" + strMarketingAccount.Trim() + "' AND SiteId='" + strSiteId + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");

                if (dsRef.Tables[0].Rows.Count > 0)
                {
                    fileExist = true;
                }

            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
                dsRef = null;
            }
            return fileExist;
        }//EOF

        #endregion

        #region Country Master
        public void GetCountryMasterDataforCombo(out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select Country,ISO,ISO3,ISONumeric,PhoneCode,IsProhibited from tblCountryMaster Order By Country";

                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, false, "", "1");
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//EOF

        #endregion
    }
}