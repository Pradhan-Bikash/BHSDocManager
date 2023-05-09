using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


namespace application
{
    public class clsUnitConversion
    {
        public clsUnitConversion()
        {
        }//eof


        #region unit+rate conversion
        public void GetGroupBaseUnit(string groupID, string matrialGroupID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"select * FROM MaterialGroupUOM mgu WHERE mgu.MaterialGroupSystemID='" + matrialGroupID + "' AND mgu.GroupID='" + groupID + "'";
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
        public void GetConversionFactorByMaterialGroup(string groupID, string materialGroupID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT mgu.SystemID AS SystemID,mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                                'BASE' AS ValuationType
                                FROM MaterialGroupUOM mgu WHERE mgu.MaterialGroupSystemID='" + materialGroupID + "' AND mgu.GroupID='" + groupID + "' " + @"

                                UNION ALL

 
                                SELECT mgau.SystemID,mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                                mgau.baseUOMfactor,
                                CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                                CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                                FROM MaterialGroupAlternativeUOM mgau WHERE mgau.MaterialGroupSystemID='" + materialGroupID + "' AND mgau.GroupID='" + groupID + "'";
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
        public void GetMaterialGroupBuyingSelling(string groupID, string materialGroupID, string SalesOrganizationID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT MS.*,UOM.Name FROM MaterialGroupBibleSellingPrice MS 
                                left outer join uom on uom.SystemID=ms.UOMsystemIDSelling
                                WHERE MS.MaterialGroupSystemID='" + materialGroupID + "'  AND MS.GroupID='" + groupID + "' AND MS.SalesOrganizationID='" + SalesOrganizationID + "'";

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
        public void GetMaterialGroupBuyingSelling(string groupID, string materialGroupID, string SalesOrganizationID, string customerID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT MS.*,UOM.Name FROM MaterialGroupCustomerSellingPrice MS 
                            left outer join uom on uom.SystemID=ms.UOMsystemIDSelling
                            WHERE MS.MaterialGroupSystemID='" + materialGroupID + "'  AND MS.GroupID='" + groupID + "' AND MS.SalesOrganizationID='" + SalesOrganizationID + "' AND (ISNULL(MS.ContactAsCustomer,'')='" + customerID.Trim() + @"' OR ISNULL(MS.CompanyAsCustomer,'')='" + customerID.Trim() + @"')";


                // and MS.ContactID='" + customerID + "'


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
        public void getAllMaterialUnit(string groupID, string materialGroupID, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;

            try
            {
                string strSql = @" SELECT mg.MaterialGroupSystemID, mg.MaterialGroupCodeSystemID, mg.UOMSystemID,o.Code, o.Name,
                                    o.[Description], '1' AS baseUOMFactor, '1' AS AlternativeUOMfactor
                                    FROM MaterialGroupUOM MG
                                    LEFT OUTER JOIN uom AS O ON o.SystemID=mg.UOMSystemID

                                    WHERE mg.GroupID='" + groupID + "' AND MG.MaterialGroupSystemID='" + materialGroupID + "'" + @"

                                    UNION ALL

                                    SELECT mg.MaterialGroupSystemID, mg.MaterialGroupCodeSystemID, mg.AlternativeUOMSystemID,o.Code, o.Name,
                                    o.[Description], mg.baseUOMFactor, mg.AlternativeUOMfactor
                                    FROM MaterialGroupAlternativeUOM MG
                                    LEFT OUTER JOIN uom AS O ON o.SystemID=mg.AlternativeUOMSystemID

                                    WHERE mg.GroupID='" + groupID + "' AND MG.MaterialGroupSystemID='" + materialGroupID + "'";

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
        public void GetAllUOMForFabric(out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT distinct CON.TargetGroupUnitID AS UomSystemID,U.Name AS UOM FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  WHERE mgu.packingForm='roll'

                            UNION ALL

                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau
                            INNER JOIN MaterialMaster AS mm ON mm.SystemID=mgau.MaterialMasterSystemID
                               where mm.packingForm='roll') AS CON
                            LEFT OUTER JOIN UOM U ON U.SystemID=CON.TargetGroupUnitID ";
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

        public double convertedUOM(string sourceUnitID, double amount, double transactionFactor, string targetUnitID, double targetFactor, DataView dvFactorRelations)
        {

            DataView dvLocal = new DataView();
            dvLocal.Table = dvFactorRelations.ToTable();


            sourceUnitID = sourceUnitID.ToUpper();
            targetUnitID = targetUnitID.ToUpper();

            if (sourceUnitID == targetUnitID) //here usd=usd or bdt=bdt
                return amount;//transation Group and target currcency are same. no need to convert further


            if (dvLocal.Count == 0)
                return 0;


            dvLocal.RowFilter = "ValuationType='Base'";
            if (dvLocal.Count == 0)
                return 0; //no base Group found

            dvLocal.RowFilter = null;
            #region Transaction Group to base
            //now usd (or any Group) >> base
            dvLocal.RowFilter = "TargetGroupUnitID='" + sourceUnitID + "'";


            if (dvLocal.Count > 0)
            {


                if (dvLocal[0]["ValuationType"].ToString().ToUpper() == "OV")
                {
                    if (transactionFactor > 0)
                    {
                        //convert the Group based on user input rate
                        amount = amount * Convert.ToDouble(transactionFactor);
                    }
                    else
                    {
                        //buy or sell rate in the database
                        amount = amount * Convert.ToDouble(bplib.clsWebLib.GetNumData(dvLocal[0]["UnitFactor"].ToString()));

                    }
                }
                else if (dvLocal[0]["ValuationType"].ToString().ToUpper() == "UV")
                {
                    if (transactionFactor > 0)
                    {
                        //convert the Group based on user input rate
                        amount = amount / Convert.ToDouble(transactionFactor);
                    }
                    else
                    {
                        //buy or sell rate in the database
                        amount = amount / Convert.ToDouble(bplib.clsWebLib.GetNumData(dvLocal[0]["UnitFactor"].ToString()));

                    }
                }

                //checking whether the target Group is base Group
                dvLocal.RowFilter = "TargetGroupUnitID='" + targetUnitID + "'";
                if (dvLocal.Count > 0)
                {
                    if (dvLocal[0]["ValuationType"].ToString().ToUpper() == "BASE")
                    {
                        //target Group is base Group 
                        //so no need to convert it anymore
                        return amount;
                    }
                }
            }
            else
            {

                return 0;
            }
            #endregion Transaction Group to base

            #region base to target Group
            dvLocal.RowFilter = "TargetGroupUnitID='" + targetUnitID + "'";
            if (dvLocal.Count > 0)
            {



                if (dvLocal[0]["ValuationType"].ToString().ToUpper() == "OV")
                {
                    if (targetFactor > 0)
                    {
                        //convert the Group based on user input rate
                        amount = amount / Convert.ToDouble(targetFactor);
                    }
                    else
                    {
                        //buy or sell rate in the database
                        amount = amount / Convert.ToDouble(bplib.clsWebLib.GetNumData(dvLocal[0]["UnitFactor"].ToString()));

                    }
                }
                else if (dvLocal[0]["ValuationType"].ToString().ToUpper() == "UV")
                {
                    if (targetFactor > 0)
                    {
                        //convert the Group based on user input rate
                        amount = amount * Convert.ToDouble(targetFactor);
                    }
                    else
                    {
                        //buy or sell rate in the database
                        amount = amount * Convert.ToDouble(bplib.clsWebLib.GetNumData(dvLocal[0]["UnitFactor"].ToString()));

                    }
                }

            }
            else
            {


                return 0;
            }


            #endregion base to targetGroup

            return amount;
        }

        #endregion unit+rate conversion

        #region currency Conversion
        public void GetAllCurrencies(out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from Currency Order By CurrencyDesc";
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
        public void GetCurrencyDesc(string strCurrencyDesc, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from Currency where CurrencyDesc='" + strCurrencyDesc.ToUpper() + "'";
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
        }//End Function
        public void GetDuplicateName(string strCurrencyDesc, string strCurrencyCode, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"SELECT * FROM Currency WHERE CurrencyDesc='" + strCurrencyDesc.Trim() + "' AND CurrencyCode<>'" + strCurrencyCode.Trim() + "'";
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
        public void GetCurrencies(string strCurrencyCode, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from Currency where CurrencyCode='" + strCurrencyCode.Trim() + "'";
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
        public void DeleteCurrency(string strCurrencyCode)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from Currency where CurrencyCode='" + strCurrencyCode + "'", true, "1");
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


        public void GetExchangerateDateWise(string CompanyID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = "Select * from ExchangerateDateWise where SystemID=''";
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
        public void GetCompanyCurrency(string CompanyID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"SELECT distinct c2.* FROM CurrencyAssignment AS C  
                            LEFT OUTER JOIN Currency c2 ON c.CurrencyCode=c2.CurrencyCode
                            WHERE CompanyID='" + CompanyID + "' AND CurrencyType='Company Currency'";
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
        public void GetGroupCurrency(string GroupID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"SELECT distinct c2.* FROM CurrencyAssignment AS C  
                            LEFT OUTER JOIN Currency c2 ON c.CurrencyCode=c2.CurrencyCode
                            WHERE C.GroupID='" + GroupID + "' AND CurrencyType='Group Currency'";
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

        public void GetDateWiseExchangeRate(string date, string CompanyID, string screenNo, string InvoiceSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT P.SystemID AS SystemID,P.CurrencyCode AS Currency,'01-jan-1900' AS FromDate,  
							'1' AS [Buyrate],'1' AS [Sellrate],'1' AS [AverageRate], 'BASE' AS Factor, 
							'1' AS ExchangeBaseBuyRate, '1' AS ExchangeBaseSellRate,'1' AS ExchangeBaseAverageRate,  '1' AS ExchangeTransactionRate  FROM CurrencyAssignment P 
							WHERE P.CompanyID='" + CompanyID + "'  AND P.CurrencyType='Company Currency' " + @"

							UNION

							select Parallel.SystemID, Parallel.FromCurrencyCode, Parallel.FromDate,
							Parallel.Buyrate,Parallel.Sellrate,Parallel.[Averagerate],
							Parallel.Factor,
							ExchangeBaseBuyRate,ExchangeBaseSellRate,ExchangeBaseAverageRate, ExchangeTransactionRate
							from (SELECT edw.SystemID,EDW.FromCurrencyCode,left(replace(upper(convert(varchar,edw.FromDate,113)),' ','-'),11) AS FromDate,

							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN edw.ToCurrencyBankBuying/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankBuying END AS [Buyrate],
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankSelling THEN edw.ToCurrencyBankSelling/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankSelling END AS [Sellrate],
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN edw.ToCurrencyAverage/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyAverage END AS [Averagerate],

            
							CASE WHEN isnull(edw.ToCurrencyBankBuying,0)>0 THEN 
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN 'OV' ELSE 'UV'  END
							ELSE
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN 'OV' ELSE 'UV'  END
							END AS Factor,
                                                   
							edw.ToCurrencyBankBuying AS ExchangeBaseBuyRate,
							edw.ToCurrencyBankSelling AS ExchangeBaseSellRate,
							edw.ToCurrencyAverage AS ExchangeBaseAverageRate, 
							EDW.FromCurrencyUnit AS ExchangeTransactionRate,
                                                         
							RANK() OVER (PARTITION BY edw.FromCurrencyCode ORDER BY edw.FromDate desc) AS Seq  " +

                            @" FROM ExchangerateDateWise EDW    WHERE edw.FromCurrencyCode NOT IN (
                                                                                            SELECT FromCurrencyCode FROM ExchangerateReferenceWise EDW    
                                                                                            WHERE edw.ScreenNo='" + screenNo + "' AND edw.ReferenceNo='" + InvoiceSystemID + "' AND edw.CompanyID='" + CompanyID + "' ) " +
                            " AND EDW.FromDate<='" + date + "' AND edw.CompanyID='" + CompanyID + "') AS Parallel where seq=1 ";



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
        public void GetDateWiseExchangeRateForDropDown(string date, string CompanyID, string screenNo, string InvoiceSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {

                strSQL = @"SELECT P.SystemID AS SystemID,P.CurrencyCode AS Currency,'01-jan-1900' AS FromDate,  
                                    '1' AS [Buyrate],'1' AS [Sellrate],'1' AS [AverageRate], 'BASE' AS Factor, 
                                    '1' AS ExchangeBaseBuyRate, '1' AS ExchangeBaseSellRate,'1' AS ExchangeBaseAverageRate,  '1' AS ExchangeTransactionRate  FROM CurrencyAssignment P 
                                    WHERE P.CompanyID='" + CompanyID + "'  AND P.CurrencyType='Company Currency' " + @"

                                    UNION  

                                    select Parallel.SystemID, Parallel.FromCurrencyCode, Parallel.FromDate,
                                    Parallel.Buyrate,Parallel.Sellrate,Parallel.[Averagerate],
                                    Parallel.Factor,
                                    ExchangeBaseBuyRate,ExchangeBaseSellRate,ExchangeBaseAverageRate, ExchangeTransactionRate
                                    from (SELECT edw.SystemID,EDW.FromCurrencyCode,left(replace(upper(convert(varchar,edw.FromDate,113)),' ','-'),11) AS FromDate,

                                    CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN edw.ToCurrencyBankBuying/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankBuying END AS [Buyrate],
                                    CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankSelling THEN edw.ToCurrencyBankSelling/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankSelling END AS [Sellrate],
                                    CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN edw.ToCurrencyAverage/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyAverage END AS [Averagerate],


                                    CASE WHEN isnull(edw.ToCurrencyBankBuying,0)>0 THEN 
                                    CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN 'OV' ELSE 'UV'  END
                                    ELSE
                                    CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN 'OV' ELSE 'UV'  END
                                    END AS Factor,
                                                           
                                    edw.ToCurrencyBankBuying AS ExchangeBaseBuyRate,
                                    edw.ToCurrencyBankSelling AS ExchangeBaseSellRate,
                                    edw.ToCurrencyAverage AS ExchangeBaseAverageRate, 
                                    EDW.FromCurrencyUnit AS ExchangeTransactionRate,
                                                                 
                                    RANK() OVER (PARTITION BY edw.FromCurrencyCode ORDER BY edw.FromDate desc) AS Seq   

                                     FROM ExchangerateDateWise EDW    WHERE edw.FromCurrencyCode NOT IN (
                                                                                                SELECT FromCurrencyCode FROM ExchangerateReferenceWise EDW    
                                                                                                WHERE edw.ScreenNo='" + screenNo + "' AND edw.ReferenceNo='" + InvoiceSystemID + "' AND edw.CompanyID='" + CompanyID + "' ) " +
                                                                " AND EDW.FromDate<='" + date + "' AND edw.CompanyID='" + CompanyID + "') AS Parallel where seq=1" +

                                                                @" UNION 
                                    SELECT edw.SystemID,EDW.FromCurrencyCode,left(replace(upper(convert(varchar,edw.FromDate,113)),' ','-'),11) AS FromDate,

                                    CASE WHEN ISNULL(edw.ToCurrencyBankBuying,0)>=1 THEN 
                                    CASE WHEN  EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN edw.ToCurrencyBankBuying/EDW.FromCurrencyUnit 
                                    ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankBuying END 
                                    ELSE 0
                                    END AS [Buyrate],

                                    CASE WHEN ISNULL(edw.ToCurrencyBankSelling,0)>=1 THEN 
                                    CASE WHEN  EDW.FromCurrencyUnit<edw.ToCurrencyBankSelling THEN edw.ToCurrencyBankSelling/EDW.FromCurrencyUnit 
                                    ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankSelling END  
                                    ELSE 0
                                    END AS [Sellrate],

                                    CASE WHEN ISNULL(edw.ToCurrencyAverage,0)>=1 THEN 
                                    CASE WHEN  EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN edw.ToCurrencyAverage/EDW.FromCurrencyUnit 
                                    ELSE edw.FromCurrencyUnit/EDW.ToCurrencyAverage END  
                                    ELSE 0
                                    END AS [Averagerate],

	                                    CASE WHEN isnull(edw.ToCurrencyBankBuying,0)>0 THEN 
							                                    CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN 'OV' ELSE 'UV'  END
							                                    ELSE
							                                    CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN 'OV' ELSE 'UV'  END
							                                    END AS Factor,

                                    edw.ToCurrencyBankBuying AS ExchangeBaseBuyRate, edw.ToCurrencyBankSelling AS ExchangeBaseSellRate,edw.ToCurrencyAverage AS ExchangeBaseAverageRate, 
                                    EDW.FromCurrencyUnit AS ExchangeTransactionRate  
                                    FROM ExchangerateReferenceWise EDW    WHERE edw.ScreenNo='" + screenNo + "' AND edw.ReferenceNo='" + InvoiceSystemID + "' AND edw.CompanyID='" + CompanyID + "' ";
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

        public void GetCompanyCurrencies(string CompanyID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"SELECT distinct c2.* FROM transactionCurrency AS C  
                            LEFT OUTER JOIN Currency c2 ON c.CurrencyCode=c2.CurrencyCode
                            WHERE C.CompanyID='" + CompanyID + "' ";
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



        public void GetParallelCurrencies(string CompanyID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"SELECT c.*, pc.CurrencyType FROM ParallelCurrency pc
                            LEFT OUTER JOIN Currency c ON c.CurrencyCode=pc.CurrencyCode
                            WHERE pc.CompanyID='" + CompanyID + "'";
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
        public void GetExchangerateInvoiceWise(string screenNo, string ReferenceNo, string CompanyID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"SELECT * FROM ExchangerateReferenceWise WHERE ScreenNo='" + screenNo + "' AND ReferenceNo='" + ReferenceNo + "' AND CompanyID='" + CompanyID + "'";
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
        #endregion currency Conversion

        #region budget fiscalyearWise Exchange Rate
        public void GetFiscalYearWiseExchangeRate_PO(string date, string CompanyID, string groupID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"SELECT P.SystemID AS SystemID,P.CurrencyCode AS Currency,'01-jan-1900' AS FromDate,  
							'1' AS [Buyrate],'1' AS [Sellrate],'1' AS [AverageRate], 'BASE' AS Factor, 
							'1' AS ExchangeBaseBuyRate, '1' AS ExchangeBaseSellRate,'1' AS ExchangeBaseAverageRate,  '1' AS ExchangeTransactionRate  FROM CurrencyAssignment P 
							WHERE P.GroupID='" + groupID + "'  AND P.CurrencyType='Company Currency' AND p.CompanyID='" + CompanyID + "' " + @"

							UNION

							select Parallel.SystemID, Parallel.FromCurrencyCode, Parallel.FromDate,
							Parallel.Buyrate,Parallel.Sellrate,Parallel.[Averagerate],
							Parallel.Factor,
							ExchangeBaseBuyRate,ExchangeBaseSellRate,ExchangeBaseAverageRate, ExchangeTransactionRate
							from (SELECT edw.SystemID,EDW.FromCurrencyCode,left(replace(upper(convert(varchar,fy.StartDate,113)),' ','-'),11) AS FromDate,

							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN edw.ToCurrencyBankBuying/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankBuying END AS [Buyrate],
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankSelling THEN edw.ToCurrencyBankSelling/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyBankSelling END AS [Sellrate],
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN edw.ToCurrencyAverage/EDW.FromCurrencyUnit ELSE edw.FromCurrencyUnit/EDW.ToCurrencyAverage END AS [Averagerate],

            
							CASE WHEN isnull(edw.ToCurrencyBankBuying,0)>0 THEN 
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyBankBuying THEN 'OV' ELSE 'UV'  END
							ELSE
							CASE WHEN EDW.FromCurrencyUnit<edw.ToCurrencyAverage THEN 'OV' ELSE 'UV'  END
							END AS Factor,
                                                   
							edw.ToCurrencyBankBuying AS ExchangeBaseBuyRate,
							edw.ToCurrencyBankSelling AS ExchangeBaseSellRate,
							edw.ToCurrencyAverage AS ExchangeBaseAverageRate, 
							EDW.FromCurrencyUnit AS ExchangeTransactionRate,
							 RANK() OVER (PARTITION BY edw.FromCurrencyCode ORDER BY fy.StartDate desc) AS Seq 

                            FROM ExchangeRateFiscalYearWise EDW   
INNER JOIN FiscalYear fy ON FY.FiscalYearID=EDW.FiscalYearID
INNER JOIN FiscalYearPeriodAndCompanyAssignment FS ON FS.FiscalYearID=FY.FiscalYearID AND EDW.CompanyID=FS.CompanyID
WHERE fy.StartDate<='" + date + "'  AND edw.CompanyID='" + CompanyID + "') AS Parallel  where seq=1";
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
        #endregion budget fiscalyearWise Exchange Rate

        #region UOM Related Queries
        public void GetGroupBaseUnitForAll(string groupID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"select * FROM MaterialGroupUOM mgu WHERE mgu.GroupID='" + groupID + "'";
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
        public void GetConversionFactorForAllMaterialGroup(string groupID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT mgu.SystemID AS SystemID,mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                                'BASE' AS ValuationType
                                FROM MaterialGroupUOM mgu WHERE mgu.GroupID='" + groupID + "' " + @"

                                UNION ALL

 
                                SELECT mgau.SystemID,mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                                mgau.baseUOMfactor,
                                CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                                CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                                FROM MaterialGroupAlternativeUOM mgau WHERE mgau.GroupID='" + groupID + "'";
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
        public void GetMaterialGroupBuyingSellingForAllGroup(string groupID, string SalesOrganizationID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT MS.*,UOM.Name FROM MaterialGroupBibleSellingPrice MS 
                                left outer join uom on uom.SystemID=ms.UOMsystemIDSelling
                                WHERE MS.GroupID='" + groupID + "' AND MS.SalesOrganizationID='" + SalesOrganizationID + "'";

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
        public void GetMaterialGroupBuyingSellingForAllGroup(string groupID, string SalesOrganizationID, string customerID, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT MS.*,UOM.Name FROM MaterialGroupCustomerSellingPrice MS 
                            left outer join uom on uom.SystemID=ms.UOMsystemIDSelling
                            WHERE MS.GroupID='" + groupID + "' AND MS.SalesOrganizationID='" + SalesOrganizationID + "' AND (ISNULL(MS.ContactAsCustomer,'')='" + customerID.Trim() + @"' OR ISNULL(MS.CompanyAsCustomer,'')='" + customerID.Trim() + @"')";


                // and MS.ContactID='" + customerID + "'


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

        public void GetConversionFactorByRMCode(string RMSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" select distinct MM.* from (SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
'BASE' AS ValuationType
FROM MaterialMaster mgu  

UNION ALL

 
SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
mgau.baseUOMfactor,
CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
FROM MaterialMasterAlternativeUOM  mgau ) AS MM
INNER JOIN BOMandSOWiseRM bsr ON bsr.MaterialMasterSystemID=mm.MaterialMasterSystemID

WHERE bsr.SystemID='" + RMSystemID + "'";
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
        public void GetConversionFactorByMaterialMasterID(string MaterialMasterSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  where mgu.SystemID='" + MaterialMasterSystemID + "' " + @"

                            UNION ALL

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   where mgau.MaterialMasterSystemID='" + MaterialMasterSystemID + "') AS CON ";
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
        public void GetConversionFactorByMaterialMasterIDs(string MaterialMasterSystemIDList, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                if (string.IsNullOrEmpty(MaterialMasterSystemIDList.Trim()) == true)
                {
                    MaterialMasterSystemIDList = "''";
                }


                strSQL = @" SELECT * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  where mgu.SystemID IN (" + MaterialMasterSystemIDList + ") " + @"

                            UNION ALL

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   where mgau.MaterialMasterSystemID IN(" + MaterialMasterSystemIDList + ")) AS CON ";
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

        public void GetConversionFactorByMaterialGroupID(string MaterialGroupSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
                            INNER JOIN MaterialGroup mg ON mg.SystemID=mgu.materialGroupID
                            where mg.SystemID='" + MaterialGroupSystemID + "' " + @"


                            UNION ALL

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                             INNER JOIN  MaterialMaster mgu   ON mgu.SystemID=mgau.MaterialMasterSystemID
                            INNER JOIN MaterialGroup mg ON mg.SystemID=mgu.materialGroupID
                            where mg.SystemID='" + MaterialGroupSystemID + "') AS CON ";
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
        public void GetConversionFactorByPOONSO(string POOnSOSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"  SELECT * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
							inner join PurchaseOrderOnSOItems pos on pos.MaterialMasterSystemID=mgu.SystemID
                            where pos.PurchaseOrderMasterOnSOSystemID='" + POOnSOSystemID + "' " + @"


                            UNION 

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                             INNER JOIN  MaterialMaster mgu   ON mgu.SystemID=mgau.MaterialMasterSystemID
                           inner join PurchaseOrderOnSOItems pos on pos.MaterialMasterSystemID=mgu.SystemID
                           where pos.PurchaseOrderMasterOnSOSystemID='" + POOnSOSystemID + "' ) AS CON ";
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
        public void GetConversionFactorByPOONSOs(string POOnSOSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"  SELECT * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
							inner join PurchaseOrderOnSOItems pos on pos.MaterialMasterSystemID=mgu.SystemID
                            where pos.PurchaseOrderMasterOnSOSystemID IN (" + POOnSOSystemID + ") " + @"


                            UNION 

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                             INNER JOIN  MaterialMaster mgu   ON mgu.SystemID=mgau.MaterialMasterSystemID
                           inner join PurchaseOrderOnSOItems pos on pos.MaterialMasterSystemID=mgu.SystemID
                           where pos.PurchaseOrderMasterOnSOSystemID IN (" + POOnSOSystemID + ") ) AS CON ";
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
        public void GetConversionFactorByPOONSOByPI(string ProformaInvoiceMasterId, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"  SELECT distinct * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
							inner join PurchaseOrderOnSODetail pos on pos.MaterialMasterSystemID=mgu.SystemID
                            inner join PurchaseOrderMasterOnSO pom on pos.PurchaseOrderMasterOnSOSystemID=pom.systemID
                            where pom.ProformaInvoiceMasterId='" + ProformaInvoiceMasterId + "' " + @"


                            UNION 

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                             INNER JOIN  MaterialMaster mgu   ON mgu.SystemID=mgau.MaterialMasterSystemID
                           inner join PurchaseOrderOnSODetail pos on pos.MaterialMasterSystemID=mgu.SystemID
                            inner join PurchaseOrderMasterOnSO pom on pos.PurchaseOrderMasterOnSOSystemID=pom.systemID
                           where pom.ProformaInvoiceMasterId='" + ProformaInvoiceMasterId + "' ) AS CON ";
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

        public void GetConversionFactorByPOonRMRequisition(string POonRMRequisitionSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"  SELECT distinct * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
							inner join PurchaseOrderonRMRequisitionItems pos on pos.MaterialMasterSystemID=mgu.SystemID
                            where pos.PurchaseOrderMasteronRMRequisitionSystemID='" + POonRMRequisitionSystemID + "' " + @"


                            UNION 

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                             INNER JOIN  MaterialMaster mgu   ON mgu.SystemID=mgau.MaterialMasterSystemID
                           inner join PurchaseOrderonRMRequisitionItems pos on pos.MaterialMasterSystemID=mgu.SystemID
                           where pos.PurchaseOrderMasteronRMRequisitionSystemID='" + POonRMRequisitionSystemID + "' ) AS CON ";
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
        public void GetConversionFactorByCartonChemicalPO(string GeneralPOMasterSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"  SELECT distinct * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
							inner join GeneralPOMaterialDetail pos on pos.MaterialMasterSystemID=mgu.SystemID
                            where pos.GeneralPOMasterSystemID='" + GeneralPOMasterSystemID + "' " + @"


                            UNION 

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                             INNER JOIN  MaterialMaster mgu   ON mgu.SystemID=mgau.MaterialMasterSystemID
                           inner join GeneralPOMaterialDetail pos on pos.MaterialMasterSystemID=mgu.SystemID
                           where pos.GeneralPOMasterSystemID='" + GeneralPOMasterSystemID + "' ) AS CON ";
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
        public void GetConversionFactorByCommercialInvoice(string CommercialInvoiceSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"  SELECT * FROM (  

SELECT distinct mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                           FROM
                            CIDetailMaster CI
inner join CIDetailAndPackingList ca on ci.CommercialInvoiceMasterId=ca.CommercialInvoiceMasterId
left outer join DetailPackingListPackagingDetailOnSOMaterial co on ca.DetailPackingListMasterSystemID=co.DetailPackingListMasterSystemID
left outer join BOMandSOWiseRM bsr on bsr.SystemID=co.BOMandSOwiseRMSystemID
left outer join MaterialMaster mgu  on bsr.MaterialMasterSystemID=mgu.SystemID
where ci.CommercialInvoiceMasterId='" + CommercialInvoiceSystemID + "' " + @"

                            UNION 

 
                           SELECT distinct mgau.SystemID,mgau.MaterialMasterSystemID, 
mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
mgau.baseUOMfactor,
CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                           
FROM
CIDetailMaster CI
inner join CIDetailAndPackingList ca on ci.CommercialInvoiceMasterId=ca.CommercialInvoiceMasterId
left outer join DetailPackingListPackagingDetailOnSOMaterial co on ca.DetailPackingListMasterSystemID=co.DetailPackingListMasterSystemID
left outer join BOMandSOWiseRM bsr on bsr.SystemID=co.BOMandSOwiseRMSystemID
left outer join MaterialMaster mgu  on bsr.MaterialMasterSystemID=mgu.SystemID
INNER JOIN  MaterialMasterAlternativeUOM mgau ON mgu.SystemID=mgau.MaterialMasterSystemID
where ci.CommercialInvoiceMasterId='" + CommercialInvoiceSystemID + "' " + @"

) AS CON ";
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


        public void GetConversionFactorByRMSO(string RMSoSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
                            inner join SalesOrderOnTradingMaterialDetail SO ON SO.MaterialMasterSystemID=mgu.SystemID
                            where SO.SalesOrderMasterOnTradingMaterialSystemID='" + RMSoSystemID + "' " + @"
                            UNION ALL

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                            inner join SalesOrderOnTradingMaterialDetail SO ON SO.MaterialMasterSystemID=mgau.MaterialMasterSystemID
                            where SO.SalesOrderMasterOnTradingMaterialSystemID='" + RMSoSystemID + "') AS CON ";
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
        public void GetConversionFactorBySalesOrderRM(string SalesOrderMasterSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT distinct * FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
                            inner join BOMandSOWiseRM SO ON SO.MaterialMasterSystemID=mgu.SystemID
                            where SO.SalesOrderMasterSystemID='" + SalesOrderMasterSystemID + "' " + @"
                            UNION ALL

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                            inner join BOMandSOWiseRM SO ON SO.MaterialMasterSystemID=mgau.MaterialMasterSystemID
                            where SO.SalesOrderMasterSystemID='" + SalesOrderMasterSystemID + "') AS CON ";
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

        public void GetConversionFactorBySalesOrder(string SalesOrderMasterSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT distinct CON.*,u.Name AS UOM FROM (
                            SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
                            mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
                            'BASE' AS ValuationType
                            FROM MaterialMaster mgu  
                            inner join BOMandSOWiseRM SO ON SO.MaterialMasterSystemID=mgu.SystemID
                            where SO.SalesOrderMasterSystemID='" + SalesOrderMasterSystemID + "' " + @"
                            UNION ALL

 
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOM  mgau   
                            inner join BOMandSOWiseRM SO ON SO.MaterialMasterSystemID=mgau.MaterialMasterSystemID
                            where SO.SalesOrderMasterSystemID='" + SalesOrderMasterSystemID + @"') AS CON  
                        INNER JOIN UOM AS u ON u.SystemID=con.TargetGroupUnitID";
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


        public void GetConversionFactorByRMCodeSelected(string RMSystemIDs, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" select distinct MM.* from (SELECT mgu.SystemID AS SystemID,mgu.SystemID AS MaterialMasterSystemID, 
mgu.UOMSystemID AS TargetGroupUnitID,'1' AS targetValue,'1' AS BaseValue, '1' AS UnitFactor, 
'BASE' AS ValuationType
FROM MaterialMaster mgu  

UNION ALL

 
SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
mgau.baseUOMfactor,
CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
FROM MaterialMasterAlternativeUOM  mgau ) AS MM
INNER JOIN BOMandSOWiseRM bsr ON bsr.MaterialMasterSystemID=mm.MaterialMasterSystemID

WHERE bsr.SystemID IN (" + RMSystemIDs + ")";
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

        public void GetConversionFactorForAlternativeUOMRefWise(string RefSystemID, string materialMasterSystemID, string AlternativeUOMSystemID, out System.Data.DataSet dsRef)
        {
            string strSQL = ""; ;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @" SELECT * FROM (
                           
                            SELECT mgau.SystemID,mgau.MaterialMasterSystemID, 
                            mgau.AlternativeUOMSystemID,mgau.AlternativeUOMfactor,
                            mgau.baseUOMfactor,
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN mgau.baseUOMfactor/mgau.AlternativeUOMfactor ELSE mgau.AlternativeUOMfactor/mgau.baseUOMfactor END AS [UnitRate],
                            CASE WHEN mgau.AlternativeUOMfactor<mgau.baseUOMfactor THEN 'OV' ELSE 'UV' END AS Factor
 
                            FROM MaterialMasterAlternativeUOMRefWise  mgau   
                         
                            where mgau.RefSystemID='" + RefSystemID + "' AND mgau.materialMasterSystemID='" + materialMasterSystemID + "' AND mgau.AlternativeUOMSystemID='" + AlternativeUOMSystemID + "') AS CON ";
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

        #endregion UOM Related Queries

        #region Currency Amount Related Queries
        public void GetConvertedAmount(string ReferenceMasterSystemID, string ReferenceType, out System.Data.DataSet dsRef)
        {
            string strSQL;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQL = @"select * FROM ConvertedAmount mgu WHERE mgu.ReferenceType='" + ReferenceType + "' AND mgu.ReferenceMasterSystemID='" + ReferenceMasterSystemID + "'";
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

        #endregion Currency Amount Related Queries
    }
}