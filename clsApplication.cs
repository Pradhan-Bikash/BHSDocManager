using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using Microsoft.Ajax.Utilities;
using System.IO;
using System.Drawing.Imaging;
using bplib;
using System.Drawing;

namespace PWOMS
{
    public class clsApplication
    {
        public clsApplication()
        {
        }//initialization
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

        #region TelData
        public void GetDataOfTeledata(string ID, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblTeleData where entryId='" + ID.Trim() + "'";
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
        public void DeleteDataOfTeleData(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where entryId='" + ID + "'", true, "1");
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
        public bool CheckFloor(string strFloorNo,string strSiteId)
        {
            System.Data.DataSet dsRef = null;
            string strSQl;
            bool blnStatus = false;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "Select *  from tblDeptloc_Fax where floorNo='" + strFloorNo + "' and SiteId='" + strSiteId.Trim() + "'";
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenDataSetThroughAdapter(strSQl, out dsRef, false, "1");
                if (dsRef.Tables[0].Rows.Count == 0)
                {
                    blnStatus = true;
                }
                else
                {
                    blnStatus = false;
                }
                return blnStatus;
            }
            catch (System.Exception ex)
            { throw (ex); }
            finally
            {
                objCon = null;
            }
        }//eof
        public void SearchTeleData(string fromDate, string todate, string strKey,string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = "Select entryId,department,staffLocation,deptGroup,extNo,mobileNo,convert(varchar(20),UpdateOn,105) as UpdateOn from tblTeleData";
                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";
                if (strKey == "")
                {
                    //strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    //strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    //strSql = strSql + " and UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by UpdateOn desc,entryId asc";
                    strSql = strSql + " and entryId='NONEEDTOLOAD'";
                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by UpdateOn desc";
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

        #endregion

        #region Update Employee Master
        public void GetDataOfEmployee(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblEmployeeMaster where EmployeeCode='" + strUserId.Trim() + "' AND SiteId='" + strSiteId + "'";
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
        public void DeleteDataOfEmployee(string ID, string strSiteId, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where EmployeeCode='" + ID + "' AND SiteId='" + strSiteId + "'", true, "1");
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
        public void SearchEmployeeData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.EmployeeCode,X.Name,X.Gender,X.DateOfBirth,X.DateOfJoin,X.Location,X.Department,
                X.Designation,X.EmailAddress,X.PhoneNumber,X.UpdateBy,
                IIF(convert(varchar(20), X.UpdateOn, 105)='01-01-1901',NULL,convert(varchar(20), X.UpdateOn, 105)) as UpdateOn
                FROM (SELECT  EmployeeCode,(isnull(NameTitle, '') + ' ' + isnull(FirstName, '') + ' ' + 
                isnull(MiddleName, '') + ' ' + isnull(LastName, '')) Name,Gender,
                IIF(convert(varchar(20), DateOfBirth, 105)='01-01-1901',NULL,convert(varchar(20), DateOfBirth, 105)) as DateOfBirth,
                IIF(convert(varchar(20), DateOfJoin, 105)='01-01-1901',NULL,convert(varchar(20), DateOfJoin, 105)) as DateOfJoin,
                Location,Department,Designation,EmailAddress,PhoneNumber,SiteId,UpdateBy,
                UpdateOn
                FROM  tblEmployeeMaster) X";

                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by EmployeeCode";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by EmployeeCode";
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

        #endregion

        #region Update employee

        public void GetDataOfEmp(string strUserId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblEmployee where EmployeeId='" + strUserId.Trim() + "' ";
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

        public void SearchEmpData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.EmployeeId,X.EmployeeName,X.Department,X.DOJ FROM (SELECT  EmployeeId, EmployeeName, Department ,
                         IIF(convert(varchar(20), DOJ, 105)='01-01-1901',NULL,convert(varchar(20), DOJ, 105)) as DOJ
                         FROM  tblEmployee) X";

                //strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " where DOJ between '" + strfromdate + "' and '" + strtodate + "' order by EmployeeId";

                }
                else
                {
                    strSql = strSql + " where " + strKey + " order by EmployeeId";
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

        public void DeleteDataOfEmp(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where EmployeeId='" + ID + "' ", true, "1");
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
        #endregion

        #region Update Bank Info

        public void GetDataOfBank(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblBankData where SystemID='" + strUserId.Trim() + "' AND SiteId='" + strSiteId + "'";
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
        public void GetBankDataForLoad(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = @"SELECT A.SystemID,A.BankName,A.BankAccountName,A.Location,A.BankAddress,A.BankCode,
                A.BranchCode,A.AccountCode,A.SWIFTCode,A.SORTCode,A.IBANsCode,A.BSBCode,A.CorrespondentBankName,
                A.CorrespendentBankAddress,A.AuthorisedSignatoryID,A.Remarks,(isnull(B.NameTitle, '') + ' ' + isnull(B.FirstName, '') + ' ' +
                isnull(B.MiddleName, '') + ' ' + isnull(B.LastName, '')) EmpName
                From tblBankData A
                Left Outer Join tblEmployeeMaster B On A.AuthorisedSignatoryID = B.EmployeeCode where A.SystemID='" + strUserId.Trim() + "' AND A.SiteId='" + strSiteId + "'";
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
        public void DeleteDataOfBank(string ID, string strSiteId, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where SystemID='" + ID + "' AND SiteId='" + strSiteId + "'", true, "1");
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
        public void SearchBankData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.SystemID,X.BankName,X.BankAccountName,X.Location,X.BankAddress,X.BankCode,X.BranchCode,X.AccountCode,
                  X.SWIFTCode,X.SORTCode,X.IBANsCode,X.BSBCode, X.UpdateBy,
                  IIF(convert(varchar(20), X.UpdateOn, 105)='01-01-1901',NULL,convert(varchar(20), X.UpdateOn, 105)) as UpdateOn
                  FROM (
                  SELECT  SystemID,BankName,BankAccountName,Location,BankAddress,BankCode,BranchCode,AccountCode,
                  SWIFTCode,SORTCode,IBANsCode,BSBCode, UpdateBy,UpdateOn,SiteId
                  FROM  tblBankData) X";

                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by SystemID";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by SystemID";
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

        #endregion

        #region Update Lease Agreement

        public void GetDataOfLeaseAgreement(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblLeaseAgreement where SystemID='" + strUserId.Trim() + "' AND SiteId='" + strSiteId + "'";
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
        public void GetDataLeaseAgreementForLoad(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = @"SELECT A.SystemID,A.Location,A.OfficeAddress,A.LettableAreaSQM,A.MonthlyRentalFee,A.MonthlyServiceFee,
                A.LeaseStartDate,A.LeaseEndDate,A.LeaseMinDurationMonth,A.AgentName,A.AgentEmail,A.AgentPhone,A.LandlordName,
                A.LandlordEmail,A.LandlordPhone,A.OtherTermsConditions,A.EmpInchargeID,(isnull(B.NameTitle, '') + ' ' + isnull(B.FirstName, '') + ' ' + 
                isnull(B.MiddleName, '') + ' ' + isnull(B.LastName, '')) EmpName
                From tblLeaseAgreement A
                Left Outer Join tblEmployeeMaster B On A.EmpInchargeID = B.EmployeeCode 
                where A.SystemID='" + strUserId.Trim() + "' AND A.SiteId='" + strSiteId + "'";

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
        public void DeleteDataOfLeaseAgreement(string ID, string strSiteId, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where SystemID='" + ID + "' AND SiteId='" + strSiteId + "'", true, "1");
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
        public void SearchLeaseAgreementData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.SystemID,X.Location,X.Address,X.AreaSQM,X.MonthlyRental,X.MonthlyService,
                IIF(convert(varchar(20), X.LeaseStartDate, 105)='01-01-1901',NULL,convert(varchar(20), X.LeaseStartDate, 105)) as LeaseStartDate,
                IIF(convert(varchar(20), X.LeaseEndDate, 105)='01-01-1901',NULL,convert(varchar(20), X.LeaseEndDate, 105)) as LeaseEndDate,
                X.LeaseMinDuration,X.AgentName,X.AgentEmail,X.AgentPhone,X.LandlordName,X.LandlordEmail,X.LandlordPhone,X.UpdateBy,
                IIF(convert(varchar(20), X.UpdateOn, 105)='01-01-1901',NULL,convert(varchar(20), X.UpdateOn, 105)) as UpdateOn
                FROM (
                SELECT  SystemID,Location,OfficeAddress Address,LettableAreaSQM AreaSQM,MonthlyRentalFee MonthlyRental,
                MonthlyServiceFee MonthlyService,LeaseStartDate,LeaseEndDate,LeaseMinDurationMonth LeaseMinDuration,
                AgentName,AgentEmail,AgentPhone,LandlordName,LandlordEmail,LandlordPhone,UpdateBy,UpdateOn,SiteId
                FROM  tblLeaseAgreement) X";

                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by SystemID";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by SystemID";
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

        #endregion

        #region Update Funding Info

        public void GetDataOfFundingInfo(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblFundingInfo where SystemID='" + strUserId.Trim() + "' AND SiteId='" + strSiteId + "'";
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
        public void GetDataFundingInfoForLoad(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = @"SELECT A.SystemID,A.Location,A.SchemeName,A.GovtDeptName,A.GovtOfficialEmail,A.GovtOfficialPhone,
                A.ApplicationDate,A.CurrentStatus,A.StatusUpdateDate,A.EmpInchargeID,
                (isnull(B.NameTitle, '') + ' ' + isnull(B.FirstName, '') + ' ' + 
                isnull(B.MiddleName, '') + ' ' + isnull(B.LastName, '')) EmpName
                From tblFundingInfo A
                Left Outer Join tblEmployeeMaster B On A.EmpInchargeID = B.EmployeeCode where A.SystemID='" + strUserId.Trim() + "' AND A.SiteId='" + strSiteId + "'";

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
        public void DeleteDataOfFundingInfo(string ID, string strSiteId, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where SystemID='" + ID + "' AND SiteId='" + strSiteId + "'", true, "1");
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
        public void SearchFundingInfoData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.SystemID,X.Location,SchemeName,X.GovtDeptName,X.GovtOfficialEmail,X.GovtOfficialPhone,
                IIF(convert(varchar(20), X.ApplicationDate, 105)='01-01-1901',NULL,convert(varchar(20), X.ApplicationDate, 105)) as ApplicationDate,
                X.CurrentStatus,
                IIF(convert(varchar(20), X.StatusUpdateDate, 105)='01-01-1901',NULL,convert(varchar(20), X.StatusUpdateDate, 105)) as StatusUpdateDate,
                X.UpdateBy,
                IIF(convert(varchar(20), X.UpdateOn, 105)='01-01-1901',NULL,convert(varchar(20), X.UpdateOn, 105)) as UpdateOn
                FROM (
                SELECT  SystemID,Location,SchemeName,GovtDeptName,GovtOfficialEmail,GovtOfficialPhone,ApplicationDate,
                CurrentStatus,StatusUpdateDate,UpdateBy,UpdateOn,SiteId
                FROM  tblFundingInfo) X";

                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by SystemID";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by SystemID";
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

        #endregion

        #region Update Biz Registration Info

        public void GetDataOfBizRegInfo(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = "select * from tblBizRegInfo where SystemID='" + strUserId.Trim() + "' AND SiteId='" + strSiteId + "'";
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
        public void GetDataBizRegInfoForLoad(string strUserId, string strSiteId, out System.Data.DataSet dsRef)
        {
            string strSQl;
            ConnectionManager.DAL.ConManager objCon;
            try
            {
                strSQl = @"SELECT A.SystemID,A.Location,A.BRNumber,A.ExpiryDate,A.RegisteredAddress,
                A.EmpInchargeID,
                (isnull(B.NameTitle, '') + ' ' + isnull(B.FirstName, '') + ' ' + 
                isnull(B.MiddleName, '') + ' ' + isnull(B.LastName, '')) EmpName
                From tblBizRegInfo A
                Left Outer Join tblEmployeeMaster B On A.EmpInchargeID = B.EmployeeCode where A.SystemID='" + strUserId.Trim() + "' AND A.SiteId='" + strSiteId + "'";

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
        public void DeleteDataOfBizRegInfo(string ID, string strSiteId, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where SystemID='" + ID + "' AND SiteId='" + strSiteId + "'", true, "1");
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
        public void SearchBizRegInfoData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.SystemID,X.BRNumber,X.Location,X.RegisteredAddress,
                IIF(convert(varchar(20), X.ExpiryDate, 105)='01-01-1901',NULL,convert(varchar(20), X.ExpiryDate, 105)) as ExpiryDate,
                X.UpdateBy,
                IIF(convert(varchar(20), X.UpdateOn, 105)='01-01-1901',NULL,convert(varchar(20), X.UpdateOn, 105)) as UpdateOn
                FROM (
                SELECT  SystemID,Location,BRNumber,ExpiryDate,RegisteredAddress,UpdateBy,UpdateOn,SiteId
                FROM  tblBizRegInfo) X";

                strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " and UpdateOn between '" + strfromdate + "' and '" + strtodate + "' order by SystemID";

                }
                else
                {
                    strSql = strSql + " and " + strKey + " order by SystemID";
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

        #endregion

        #region Update Customer
        public void GetDataOfCustomer(string strUserId, out System.Data.DataSet dsRef)
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

        public void SearchCustomerData(string fromDate, string todate, string strKey, string strSiteId, out System.Data.DataSet dsRef)
        {
            ConnectionManager.DAL.ConManager objCon;
            string strSql = "";
            string strfromdate = "";
            string strtodate = "";
            try
            {
                strSql = @"SELECT X.CustomerId,X.CustomerName,X.CustomerType,X.DOB,x.Phone FROM (SELECT  CustomerId, CustomerName, CustomerType ,
                         IIF(convert(varchar(20), DOB, 105)='01-01-1901',NULL,convert(varchar(20), DOB, 105)) as DOB,Phone
                         FROM  TBL_Customer) X";

                //strSql = strSql + " where SiteId='" + strSiteId.Trim() + "'";

                if (strKey == "")
                {
                    strfromdate = bplib.clsWebLib.AppDateConvert(fromDate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");
                    strtodate = bplib.clsWebLib.AppDateConvert(todate, bplib.clsWebLib.getUserDateFormat(), "MM/dd/yyyy").ToString("MM/dd/yyyy");

                    strSql = strSql + " where DOB between '" + strfromdate + "' and '" + strtodate + "' order by CustomerId";

                }
                else
                {
                    strSql = strSql + " where " + strKey + " order by CustomerId";
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

        public void DeleteDataOfCustomer(string ID, string tblName)
        {
            ConnectionManager.DAL.ConManager objCon = null;
            try
            {
                objCon = new ConnectionManager.DAL.ConManager("1");
                objCon.OpenConnection("1");
                objCon.BeginTransaction();
                objCon.ExecuteNonQueryWrapper("Delete from " + tblName + " where CustomerId='" + ID + "' ", true, "1");
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