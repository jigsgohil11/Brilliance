using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;
using System.Web;
using System.IO;

namespace Brilliance.Infrastructure.DataProvider
{
    public class ClientSetupDataProvider : BaseDataProvider, IClientSetupDataProvider
    {
        public ServiceResponse GetClient(Guid ClientID)
        {
            ClientSetupViewModel ClientViewModel = new ClientSetupViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(ClientID)}
                };

                ClientViewModel = GetMultipleEntity<ClientSetupViewModel>("GetClientdata_new", searchList);
                response.Data = ClientViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public ServiceResponse SaveClient(ClientSetupViewModel Clients,HttpPostedFileBase orglogo)
        {
            var response = new ServiceResponse();
            try
            {

                var SavePathInDB = (orglogo != null ? Clients.client.ClientID + Path.GetExtension(orglogo.FileName) : Clients.client.Logo);

                if (!string.IsNullOrEmpty(Convert.ToString(Clients.client.ClientID)) && Clients.client.ClientID != Guid.Empty)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ClientID", Clients.client.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@OrganizationName", Clients.client.OrganizationName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Industry", Clients.client.Industry).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ContactPersonName", Clients.client.ContactPersonName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ContactPersonSurName", Clients.client.ContactPersonSurName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@TelephoneNo", Clients.client.TelephoneNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@MobileNo", Clients.client.MobileNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ContactPersonEmail", Clients.client.ContactPersonEmail).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@VATNo", Clients.client.VATNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@AddressLine1", Clients.client.AddressLine1).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@AddressLine2", Clients.client.AddressLine2).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@AddressLine3", Clients.client.AddressLine3).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@City", Clients.client.City).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Postcode", Clients.client.Postcode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Country", Clients.client.Country).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@EntityNo", Clients.client.EntityNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@FSPNo", Clients.client.FSPNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@URLDetails", Clients.client.URLDetails).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Logo", SavePathInDB).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Sitepullthrough", Clients.client.Sitepullthrough).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Skincolours", Clients.client.Skincolours).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@buttonsclr", Clients.client.buttonsclr).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@headsclr", Clients.client.headsclr).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@subheadsclr", Clients.client.subheadsclr).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsEdit", Clients.client.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Client_New", cmd);
                    response.IsSuccess = true;
                    if (Clients.client.IsEdit == true)
                    {
                        response.Message = "Record Updated Successfully.";
                    }
                    else
                    {
                        response.Message = "Record Created Successfully.";
                    }
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Server Error";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
    }
}