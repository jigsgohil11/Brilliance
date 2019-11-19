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
    public class ClientDataProvider : BaseDataProvider, IClientDataProvider
    {
        public ServiceResponse GetClient(Guid ClientID)
        {
            ClientViewModel ClientViewModel = new ClientViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(ClientID)}
                };

                ClientViewModel = GetMultipleEntity<ClientViewModel>("GetClientdata", searchList);
                response.Data = ClientViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ClientListModel ClientList()
        {
            var clientListModel = new ClientListModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<Client> clients = GetEntityList<Client>("GetClientList", searchList);//Constants.GetUserGroupList
                if (clients.Count > 0)
                {
                    clientListModel.Clientlist = clients;
                    clientListModel.Response.IsSuccess = true;
                }
                else
                {
                    clientListModel.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return clientListModel;
        }
        public ServiceResponse SaveClients(ClientViewModel Clients)
        {
            var response = new ServiceResponse();
            try
            {


                if (Clients.client.IsEdit == false)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ClientID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientCode", Clients.client.ClientCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@OrganizationName", Clients.client.OrganizationName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ContactPersonName", Clients.client.ContactPersonName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ContactPersonEmail", Clients.client.ContactPersonEmail).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@MobileNo", Clients.client.MobileNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", Clients.client.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Address", Clients.client.Address).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@UnitNo", Clients.client.UnitNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Complex", Clients.client.Complex).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Street", Clients.client.Street).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@StreetNo", Clients.client.StreetNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@City", Clients.client.City).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@State", Clients.client.State).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Country", Clients.client.Country).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Clients.client.IsEdit).SqlDbType = SqlDbType.Bit;


                    DataSet ds = null;
                    ds = BulkInsert("Save_Client", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ClientID", Clients.client.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientCode", Clients.client.ClientCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@OrganizationName", Clients.client.OrganizationName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ContactPersonName", Clients.client.ContactPersonName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ContactPersonEmail", Clients.client.ContactPersonEmail).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@MobileNo", Clients.client.MobileNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", Clients.client.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Address", Clients.client.Address).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@UnitNo", Clients.client.UnitNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Complex", Clients.client.Complex).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Street", Clients.client.Street).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@StreetNo", Clients.client.StreetNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@City", Clients.client.City).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@State", Clients.client.State).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Country", Clients.client.Country).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Clients.client.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Client", cmd);
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
        public ServiceResponse DeleteClient(Guid ClientID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (ClientID != Guid.Empty)
            {
                var searchValueData = new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) };
                searchList.Add(searchValueData);
                if (Convert.ToBoolean(GetScalar("DeleteClient", searchList)))
                {
                    response.IsSuccess = true;
                    response.Message = "Record Deleted Successfully.";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Organization is assigned to Company!!";
                }
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }
        public ServiceResponse GetStateByCountry(Guid CountryID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "CountryID", Value = Convert.ToString(CountryID) });
                List<SelectListItem> States = GetEntityList<SelectListItem>("GetStateListByCountry", searchvaluedata);
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

        public ServiceResponse GetCityByState(Guid StateID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "StateID", Value = Convert.ToString(StateID) });
                List<SelectListItem> States = GetEntityList<SelectListItem>("GetCityListByState", searchvaluedata);
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