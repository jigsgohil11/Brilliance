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
    public class AdmDivisionDataProvider : BaseDataProvider, IAdmDivisionDataProvider
    {

        public ServiceResponse GetDivision(Guid ClientID, Guid CompanyID)
        {
            AdmDivisionViewModel DivisionViewModel = new AdmDivisionViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(ClientID)},
                        new SearchValueData { Name = "CompanyID" ,Value = Convert.ToString(CompanyID)}
                };

                DivisionViewModel = GetMultipleEntity<AdmDivisionViewModel>("Adm_GetDivisiondata", searchList);
                DivisionViewModel.division.ClientID = ClientID;
                DivisionViewModel.division.CompanyID = CompanyID;
                response.Data = DivisionViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ServiceResponse SaveDivision(AdmDivisionViewModel divisionModel)
        {
            var response = new ServiceResponse();
            try
            {
                DataTable dtdivision = new DataTable();
                dtdivision = Common.ToDataTable(divisionModel.divisionList);


                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientID", divisionModel.division.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CompanyID", divisionModel.division.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@dtdivisions", dtdivision).SqlDbType = SqlDbType.Structured;

                DataSet ds = null;
                ds = BulkInsert("Save_division_new", cmd);
                response.IsSuccess = true;
                //response.IsClient = divisionModel.division.IsClient = true ? true : false;
                response.Message = "Record Saved Successfully.";

            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }

        public ServiceResponse DeleteDivision(Guid DivisionID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (DivisionID != Guid.Empty)
            {
                var searchValueData = new SearchValueData { Name = "DivisionID", Value = Convert.ToString(DivisionID) };
                searchList.Add(searchValueData);
                BaseDataProvider objnew = new BaseDataProvider();
                objnew = new BaseDataProvider();
                objnew.GetScalar("Pms_DeleteRequisitionRequest", searchList);
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }


    }
}