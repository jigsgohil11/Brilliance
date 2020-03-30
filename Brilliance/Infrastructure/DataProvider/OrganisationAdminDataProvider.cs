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

    public class OrganisationAdminDataProvider : BaseDataProvider, IOrganisationAdminDataProvider
    {

        public OrganisationListModel OrganisationList()
        {
            var organisationListModel = new OrganisationListModel();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "RoleName" ,Value = Convert.ToString(SessionHelper.RoleName)},
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(SessionHelper.ClientID)}
                };


                List<Client> clients = GetEntityList<Client>("GetOrganisationList", searchList);//Constants.GetUserGroupList
                if (clients.Count > 0)
                {
                    organisationListModel.Organisationlist = clients;
                    organisationListModel.Response.IsSuccess = true;
                }
                else
                {
                    organisationListModel.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return organisationListModel;
        }
        public ServiceResponse GetCompany(Guid ClientID)
        {
            CompanyAdminModel Companymodel = new CompanyAdminModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(ClientID)}
                };

                Companymodel = GetMultipleEntity<CompanyAdminModel>("GetCompanydata_New", searchList);
                Companymodel.company.ClientID = ClientID;
                response.Data = Companymodel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ServiceResponse SaveCompany(CompanyAdminModel companymodel)
        {
            var response = new ServiceResponse();
            try
            {
                DataTable company = new DataTable();
                company.TableName = "mst_Company";
                company.Columns.Add("CompanyID", typeof(Guid));
                company.Columns.Add("CompanyCode", typeof(string));
                company.Columns.Add("CompanyName", typeof(string));
                company.Columns.Add("FspNo", typeof(string));
                company.Columns.Add("ClientID", typeof(Guid));
                company.Columns.Add("IsActive", typeof(bool));
                company.Columns.Add("CreatedBy", typeof(Guid));
                company.Columns.Add("CreatedOn", typeof(DateTime));
                company.Columns.Add("IsInsert", typeof(bool));

                if (companymodel != null && companymodel.Companylist.Count > 0)
                {
                    foreach (var items in companymodel.Companylist)
                    {
                        if (items.CompanyName != null && items.CompanyName != "" && items.CompanyID == Guid.Empty)
                        {
                            DataRow dr = null;
                            dr = company.NewRow();
                            dr["CompanyID"] = Guid.NewGuid();
                            dr["CompanyCode"] = items.CompanyCode;
                            dr["CompanyName"] = items.CompanyName;
                            dr["FspNo"] = items.FspNo;
                            dr["ClientID"] = companymodel.company.ClientID;
                            dr["IsActive"] = true;
                            dr["CreatedBy"] = SessionHelper.UserId;
                            dr["CreatedOn"] = DateTime.Now;
                            dr["UpdatedOn"] = null;
                            dr["UpdatedBy"] = null;
                            dr["IsInsert"] = true;
                            company.Rows.Add(dr);
                        }
                        else if (items.CompanyName != null && items.CompanyName != "" && items.CompanyID != Guid.Empty)
                        {
                            DataRow dr = null;
                            dr = company.NewRow();
                            dr["CompanyID"] = items.CompanyID;
                            dr["CompanyCode"] = items.CompanyCode;
                            dr["CompanyName"] = items.CompanyName;
                            dr["FspNo"] = items.FspNo;
                            dr["ClientID"] = companymodel.company.ClientID;
                            dr["IsActive"] = true;
                            dr["CreatedBy"] = null;
                            dr["CreatedOn"] = null;
                            dr["UpdatedOn"] = DateTime.Now;
                            dr["UpdatedBy"] = SessionHelper.UserId;
                            dr["IsInsert"] = false;
                            company.Rows.Add(dr);
                        }
                    }
                }

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientID", companymodel.company.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@dtcompanies", company).SqlDbType = SqlDbType.Structured;

                DataSet ds = null;
                ds = BulkInsert("SaveCompany_New", cmd);
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