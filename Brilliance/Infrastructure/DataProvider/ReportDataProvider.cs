﻿using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Brilliance.Infrastructure.DataProvider
{
    public class ReportDataProvider : BaseDataProvider, IReportDataProvider
    {

        public DataSet GetComplaintExcelReport(string Type, Guid CompanyID, Guid DivisionID)
        {
            var response = new ServiceResponse();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                //cmd.Parameters.AddWithValue("@Year", Convert.ToString(Year)).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@Month", Convert.ToString(Month)).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@ClientID", SessionHelper.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                if (CompanyID != Guid.Empty && DivisionID != Guid.Empty)
                {
                    cmd.Parameters.AddWithValue("@CompanyID", CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DivisionID", DivisionID).SqlDbType = SqlDbType.UniqueIdentifier;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CompanyID", SessionHelper.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DivisionID", SessionHelper.DivisionID).SqlDbType = SqlDbType.UniqueIdentifier;
                }
                cmd.Parameters.AddWithValue("@Type", Type).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@EmpUserIds", Convert.ToString(EmpUserIds)).SqlDbType = SqlDbType.NVarChar;
                ds = BulkInsert("GetComplaintExcelReport", cmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetLevel2ComplaintExcelReport(string Type, Guid CompanyID)
        {
            var response = new ServiceResponse();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                //cmd.Parameters.AddWithValue("@Year", Convert.ToString(Year)).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@Month", Convert.ToString(Month)).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@ClientID", SessionHelper.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                if (CompanyID != Guid.Empty)
                {
                    cmd.Parameters.AddWithValue("@CompanyID", CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                }
                else
                {
                    cmd.Parameters.AddWithValue("@CompanyID", SessionHelper.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                }

                cmd.Parameters.AddWithValue("@DivisionID", SessionHelper.DivisionID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@Type", Type).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@EmpUserIds", Convert.ToString(EmpUserIds)).SqlDbType = SqlDbType.NVarChar;
                ds = BulkInsert("GetLevel2ComplaintExcelReport", cmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }
        public DataSet GetLevel1ComplaintExcelReport(string Type)
        {
            var response = new ServiceResponse();
            SqlCommand cmd = new SqlCommand();
            DataSet ds = new DataSet();

            try
            {
                //cmd.Parameters.AddWithValue("@Year", Convert.ToString(Year)).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@Month", Convert.ToString(Month)).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@ClientID", SessionHelper.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CompanyID", SessionHelper.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@DivisionID", SessionHelper.DivisionID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@Type", Type).SqlDbType = SqlDbType.NVarChar;
                //cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                //cmd.Parameters.AddWithValue("@EmpUserIds", Convert.ToString(EmpUserIds)).SqlDbType = SqlDbType.NVarChar;
                ds = BulkInsert("GetLevel1ComplaintExcelReport", cmd);
            }
            catch (Exception ex)
            {

            }
            return ds;
        }

    }
}