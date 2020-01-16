using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Brilliance.Infrastructure.DataProvider
{
    public class AdmCrtDataProvider : BaseDataProvider, IAdmCrtDataProvider
    {
        public ServiceResponse GetCrtSetup()
        {
            CrtAdminViewmodel crtViewModel = new CrtAdminViewmodel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                crtViewModel = GetMultipleEntity<CrtAdminViewmodel>("Adm_GetCRTsetupdata", searchList);
                response.Data = crtViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ServiceResponse Savelabelconfig(CrtAdminViewmodel CRTAdminVM)
        {
            var response = new ServiceResponse();
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@LabelconfigID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@ClientID", CRTAdminVM.labelconfig.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@instanceName", CRTAdminVM.labelconfig.InstanceName).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Tier2", CRTAdminVM.labelconfig.Tier2).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Tier3", CRTAdminVM.labelconfig.Tier3).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Accnumber", CRTAdminVM.labelconfig.Accnumber).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Idnumber", CRTAdminVM.labelconfig.Idnumber).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@contactno", CRTAdminVM.labelconfig.contactno).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@emailaddress", CRTAdminVM.labelconfig.emailaddress).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@productcategory", CRTAdminVM.labelconfig.productcategory).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Producttype", CRTAdminVM.labelconfig.Producttype).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Compcategory", CRTAdminVM.labelconfig.Compcategory).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Nature", CRTAdminVM.labelconfig.Nature).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@TCF", CRTAdminVM.labelconfig.TCF).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@incidentdate", CRTAdminVM.labelconfig.incidentdate).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@policystatus", CRTAdminVM.labelconfig.policystatus).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Rootcause", CRTAdminVM.labelconfig.Rootcause).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@howcomreceived", CRTAdminVM.labelconfig.howcomreceived).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Compregulatory", CRTAdminVM.labelconfig.Compregulatory).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@feedbackregulatory", CRTAdminVM.labelconfig.feedbackregulatory).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Overalloutcome", CRTAdminVM.labelconfig.Overalloutcome).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Compensation", CRTAdminVM.labelconfig.Compensation).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Regulatedcost", CRTAdminVM.labelconfig.Regulatedcost).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Value", CRTAdminVM.labelconfig.Value).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@DissatisfactionLevel", CRTAdminVM.labelconfig.DissatisfactionLevel).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@SatisfactionResolution", CRTAdminVM.labelconfig.SatisfactionResolution).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@IsTemplate", CRTAdminVM.labelconfig.IsTemplate).SqlDbType = SqlDbType.Bit;
                cmd.Parameters.AddWithValue("@CreatedBy", CRTAdminVM.labelconfig.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CreatedOn", CRTAdminVM.labelconfig.CreatedOn).SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.AddWithValue("@IsEdit", CRTAdminVM.labelconfig.IsEdit).SqlDbType = SqlDbType.Bit;

                DataSet ds = null;
                ds = BulkInsert("SaveCRT_Labelconfig", cmd);
                response.IsSuccess = true;
                response.Message = "Record Saved Successfully.";

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }

    }
}