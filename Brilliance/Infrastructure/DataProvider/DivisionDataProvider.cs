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
    public class DivisionDataProvider : BaseDataProvider, IDivisionDataProvider
    {
        public ServiceResponse GetDivision(Guid DivisionID)
        {
            DivisionViewModel DivisionViewModel = new DivisionViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "DivisionID" ,Value = Convert.ToString(DivisionID)}
                };

                DivisionViewModel = GetMultipleEntity<DivisionViewModel>("GetDivisiondata", searchList);
                response.Data = DivisionViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public DivisionListModel DivisionList()
        {
            var divisiontListModel = new DivisionListModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<Division> clients = GetEntityList<Division>("GetDivisionList", searchList);//Constants.GetUserGroupList
                if (clients.Count > 0)
                {
                    divisiontListModel.Divisionlist = clients;
                    divisiontListModel.Response.IsSuccess = true;
                }
                else
                {
                    divisiontListModel.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return divisiontListModel;
        }
        public ServiceResponse SaveDivision(DivisionViewModel Divisionmodel)
        {
            var response = new ServiceResponse();
            try
            {


                if (Divisionmodel.division.IsEdit == false)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@DivisionID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", Divisionmodel.division.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Name", Divisionmodel.division.Name).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", Divisionmodel.division.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CompanyID", Divisionmodel.division.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", Divisionmodel.division.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn",DateTime.Now ).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedOn",null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy",SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedBy",null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Divisionmodel.division.IsEdit).SqlDbType = SqlDbType.Bit;


                    DataSet ds = null;
                    ds = BulkInsert("Save_Division", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@DivisionID", Divisionmodel.division.DivisionID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", Divisionmodel.division.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Name", Divisionmodel.division.Name).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", Divisionmodel.division.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CompanyID", Divisionmodel.division.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", Divisionmodel.division.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Divisionmodel.division.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Division", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Updated Successfully.";
                }

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
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
                objnew.GetScalar("DeleteDivision", searchList);
                response.IsSuccess = true;
                response.Message = "Record Deleted Successfully.";
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public ServiceResponse GetCompanyByClient(Guid ClientID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });
                List<SelectListItem> States = GetEntityList<SelectListItem>("GetcompanyListByClient", searchvaluedata);
                //if (HoldingCompanies != null && HoldingCompanies.Count > 0)
                //{
                //    response.Data = HoldingCompanies;
                //}
                response.Data = States;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }
    }
}