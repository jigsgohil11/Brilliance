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
    public class CompanyDataProvider: BaseDataProvider, ICompanyDataProvider
    {
        public ServiceResponse GetCompany(Guid CompanyID)
        {
            CompanyViewModel CompanyVM = new CompanyViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "CompanyID" ,Value = Convert.ToString(CompanyID)}
                };

                CompanyVM = GetMultipleEntity<CompanyViewModel>("GetCompanydata", searchList);
                response.Data = CompanyVM;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public CompanyListModel CompanyList()
        {
            var companyListModel = new CompanyListModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<Company> companies = GetEntityList<Company>("GetCompanyList", searchList);//Constants.GetUserGroupList
                if (companies.Count > 0)
                {
                    companyListModel.Companylist = companies;
                    companyListModel.Response.IsSuccess = true;
                }
                else
                {
                    companyListModel.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return companyListModel;
        }
        public ServiceResponse SaveCompanies(CompanyViewModel companymodel)
        {
            var response = new ServiceResponse();
            try
            {

                if (companymodel.company.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@CompanyID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompanyCode", companymodel.company.CompanyCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CompanyName", companymodel.company.CompanyName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ClientID", companymodel.company.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Description", companymodel.company.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Address", companymodel.company.Address).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@UnitNo", companymodel.company.UnitNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Complex", companymodel.company.Complex).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Street", companymodel.company.Street).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@StreetNo", companymodel.company.StreetNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Country", companymodel.company.Country).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@State", companymodel.company.State).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@City", companymodel.company.City).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ContactPersonName", companymodel.company.ContactPersonName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@MobileNo", companymodel.company.MobileNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Email", companymodel.company.Email).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SecondaryContactName", companymodel.company.SecondaryContactName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SecondaryContactNo", companymodel.company.SecondaryContactNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SecondaryEmail", companymodel.company.SecondaryEmail).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@WebURL", companymodel.company.WebURL).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IndustryID", companymodel.company.IndustryID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@SectorID", companymodel.company.SectorID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsViewDissatisfactionLevel", companymodel.company.IsViewDissatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsRequireDissatisfactionLevel", companymodel.company.IsRequireDissatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsViewSatisfactionLevel", companymodel.company.IsViewSatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsRequireSatisfactionLevel", companymodel.company.IsRequireSatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsEdit", companymodel.company.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Company", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@CompanyID", companymodel.company.CompanyID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompanyCode", companymodel.company.CompanyCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CompanyName", companymodel.company.CompanyName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ClientID", companymodel.company.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Description", companymodel.company.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Address", companymodel.company.Address).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@UnitNo", companymodel.company.UnitNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Complex", companymodel.company.Complex).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Street", companymodel.company.Street).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@StreetNo", companymodel.company.StreetNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Country", companymodel.company.Country).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@State", companymodel.company.State).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@City", companymodel.company.City).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ContactPersonName", companymodel.company.ContactPersonName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@MobileNo", companymodel.company.MobileNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Email", companymodel.company.Email).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SecondaryContactName", companymodel.company.SecondaryContactName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SecondaryContactNo", companymodel.company.SecondaryContactNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SecondaryEmail", companymodel.company.SecondaryEmail).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@WebURL", companymodel.company.WebURL).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IndustryID", companymodel.company.IndustryID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@SectorID", companymodel.company.SectorID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsViewDissatisfactionLevel", companymodel.company.IsViewDissatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsRequireDissatisfactionLevel", companymodel.company.IsRequireDissatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsViewSatisfactionLevel", companymodel.company.IsViewSatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsRequireSatisfactionLevel", companymodel.company.IsRequireSatisfationLevel).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsEdit", companymodel.company.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Company", cmd);
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
        public ServiceResponse DeleteCompany(Guid CompanyID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (CompanyID != Guid.Empty)
            {
                var searchValueData = new SearchValueData { Name = "CompanyID", Value = Convert.ToString(CompanyID) };
                searchList.Add(searchValueData);
                BaseDataProvider objnew = new BaseDataProvider();
                objnew = new BaseDataProvider();
                objnew.GetScalar("DeleteCompany", searchList);
                response.IsSuccess = true;
                response.Message = "Record Deleted Successfully.";
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public ServiceResponse GetSectorByIndustry(Guid IndustryID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "IndustryID", Value = Convert.ToString(IndustryID) });
                List<SelectListItem> Sectors = GetEntityList<SelectListItem>("GetSectorListByIndustry", searchvaluedata);
                //if (HoldingCompanies != null && HoldingCompanies.Count > 0)
                //{
                //    response.Data = HoldingCompanies;
                //}
                response.Data = Sectors;
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