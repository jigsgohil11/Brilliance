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
    public class AdmCompanyDataProvider : BaseDataProvider, IAdmCompanyDataProvider
    {
        public ServiceResponse GetCompany(Guid ClientID)
        {
            AdmCompanyViewModel CompanyViewModel = new AdmCompanyViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(ClientID)}
                };

                CompanyViewModel = GetMultipleEntity<AdmCompanyViewModel>("Adm_GetCompanydata", searchList);
                CompanyViewModel.company.ClientID = ClientID;
                response.Data = CompanyViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ServiceResponse SaveCompany(AdmCompanyViewModel companyModel)
        {
            var response = new ServiceResponse();
            try
            {
                DataTable dtcompany = new DataTable();
                dtcompany = Common.ToDataTable(companyModel.companyList);


                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientID", companyModel.company.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@dtcompanies", dtcompany).SqlDbType = SqlDbType.Structured;

                DataSet ds = null;
                ds = BulkInsert("Save_company_new", cmd);
                response.IsSuccess = true;
                response.Message = "Record Saved Successfully.";


            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }
        public ServiceResponse DeleteCompany(Guid CompanyID)
        {
            var response = new ServiceResponse();
            SqlCommand cmd = new SqlCommand();
            //var searchList = new List<SearchValueData>();
            if (CompanyID != Guid.Empty)
            {
                cmd.Parameters.AddWithValue("@CompanyID", CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                DataSet ds = null;
                ds = BulkInsert("chk_divisions", cmd);
                bool Isdivisionexist;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    Isdivisionexist = Convert.ToBoolean(ds.Tables[0].Rows[0]["Isdivisionexist"]);
                    if (Isdivisionexist == true)
                    {
                        response.IsSuccess = true;
                    }
                    else
                    {
                        response.IsSuccess = false;
                    }
                }
                else
                {
                    response.IsSuccess = false;
                }
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}