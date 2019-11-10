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
    public class ComplaintDataProvider : BaseDataProvider, IComplaintDataProvider
    {
        public ServiceResponse GetComplaint(Guid ComplaintID)
        {
            ComplaintViewModel Complaintmodel = new ComplaintViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ComplaintID" ,Value = Convert.ToString(ComplaintID)}
                };

                Complaintmodel = GetMultipleEntity<ComplaintViewModel>("GetComplaintdata", searchList);
                response.Data = Complaintmodel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ComplaintListModel ComplaintList()
        {
            var complaintlistVM = new ComplaintListModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<Complaint> complaints = GetEntityList<Complaint>("GetComplaintList", searchList);//Constants.GetUserGroupList
                if (complaints.Count > 0)
                {
                    complaintlistVM.Complaintlist = complaints;
                    complaintlistVM.Response.IsSuccess = true;
                }
                else
                {
                    complaintlistVM.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return complaintlistVM;
        }
        public ServiceResponse SaveComplaints(ComplaintViewModel Complaintmodel)
        {
            var response = new ServiceResponse();
            try
            {


                if (Complaintmodel.complaint.IsEdit == false)
                {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ComplaintID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompanyId", Complaintmodel.complaint.CompanyId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DivisionId", Complaintmodel.complaint.DivisionId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@PolicyNumber", Complaintmodel.complaint.PolicyNumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsClient", Complaintmodel.complaint.IsClient).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@ClientIdentificationNumber", Complaintmodel.complaint.ClientIdentificationNumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@FirstName", Complaintmodel.complaint.FirstName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@LastName", Complaintmodel.complaint.LastName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@PhoneNo", Complaintmodel.complaint.PhoneNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Email", Complaintmodel.complaint.Email).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SatisfactionLevel", Complaintmodel.complaint.SatisfactionLevel).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsComplaint", Complaintmodel.complaint.IsComplaint).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Isclaimpaid", Complaintmodel.complaint.Isclaimpaid).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@claimpaymentdate", Complaintmodel.complaint.claimpaymentdate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@claimpaidamount_exclVAT", Complaintmodel.complaint.claimpaidamount_exclVAT).SqlDbType = SqlDbType.Decimal;
                    cmd.Parameters.AddWithValue("@claimpaidamount_inclVAT", Complaintmodel.complaint.claimpaidamount_inclVAT).SqlDbType = SqlDbType.Decimal;
                    cmd.Parameters.AddWithValue("@IsVATregistered", Complaintmodel.complaint.IsVATregistered).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@VAT_number", Complaintmodel.complaint.VAT_number).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsPayee", Complaintmodel.complaint.IsPayee).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Panel_Attorneys", Complaintmodel.complaint.Panel_Attorneys).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CaseFileStage", Complaintmodel.complaint.CaseFileStage).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintProductId", Complaintmodel.complaint.ComplaintProductId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintProductTypeId", Complaintmodel.complaint.ComplaintProductTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintCategoryId", Complaintmodel.complaint.ComplaintCategoryId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintNatureOfId", Complaintmodel.complaint.ComplaintNatureOfId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DateReceived", Complaintmodel.complaint.DateReceived).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@DateIncident", Complaintmodel.complaint.DateIncident).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsResolved", Complaintmodel.complaint.IsResolved).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DateResolved", Complaintmodel.complaint.DateResolved).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ComplaintPolicyStatusId", Complaintmodel.complaint.ComplaintPolicyStatusId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintRootCauseId", Complaintmodel.complaint.ComplaintRootCauseId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintReceivedId", Complaintmodel.complaint.ComplaintReceivedId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Satisfactionlevel_resolution", Complaintmodel.complaint.Satisfactionlevel_resolution).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintReceivedRegulatoryId", Complaintmodel.complaint.ComplaintReceivedRegulatoryId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintReceivedRegulatoryFeedbackId", Complaintmodel.complaint.ComplaintReceivedRegulatoryFeedbackId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintOverallOutcomeId", Complaintmodel.complaint.ComplaintOverallOutcomeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintCompensationId", Complaintmodel.complaint.ComplaintCompensationId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintRegulatedCostId", Complaintmodel.complaint.ComplaintRegulatedCostId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompensationValue", Complaintmodel.complaint.CompensationValue).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Complaintmodel.complaint.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Complaint", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ComplaintID", Complaintmodel.complaint.ComplaintID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompanyId", Complaintmodel.complaint.CompanyId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DivisionId", Complaintmodel.complaint.DivisionId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@PolicyNumber", Complaintmodel.complaint.PolicyNumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsClient", Complaintmodel.complaint.IsClient).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@ClientIdentificationNumber", Complaintmodel.complaint.ClientIdentificationNumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@FirstName", Complaintmodel.complaint.FirstName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@LastName", Complaintmodel.complaint.LastName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@PhoneNo", Complaintmodel.complaint.PhoneNo).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Email", Complaintmodel.complaint.Email).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SatisfactionLevel", Complaintmodel.complaint.SatisfactionLevel).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsComplaint", Complaintmodel.complaint.IsComplaint).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Isclaimpaid", Complaintmodel.complaint.Isclaimpaid).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@claimpaymentdate", Complaintmodel.complaint.claimpaymentdate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@claimpaidamount_exclVAT", Complaintmodel.complaint.claimpaidamount_exclVAT).SqlDbType = SqlDbType.Decimal;
                    cmd.Parameters.AddWithValue("@claimpaidamount_inclVAT", Complaintmodel.complaint.claimpaidamount_inclVAT).SqlDbType = SqlDbType.Decimal;
                    cmd.Parameters.AddWithValue("@IsVATregistered", Complaintmodel.complaint.IsVATregistered).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@VAT_number", Complaintmodel.complaint.VAT_number).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsPayee", Complaintmodel.complaint.IsPayee).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Panel_Attorneys", Complaintmodel.complaint.Panel_Attorneys).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CaseFileStage", Complaintmodel.complaint.CaseFileStage).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintProductId", Complaintmodel.complaint.ComplaintProductId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintProductTypeId", Complaintmodel.complaint.ComplaintProductTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintCategoryId", Complaintmodel.complaint.ComplaintCategoryId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintNatureOfId", Complaintmodel.complaint.ComplaintNatureOfId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DateReceived", Complaintmodel.complaint.DateReceived).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@DateIncident", Complaintmodel.complaint.DateIncident).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsResolved", Complaintmodel.complaint.IsResolved).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DateResolved", Complaintmodel.complaint.DateResolved).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ComplaintPolicyStatusId", Complaintmodel.complaint.ComplaintPolicyStatusId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintRootCauseId", Complaintmodel.complaint.ComplaintRootCauseId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintReceivedId", Complaintmodel.complaint.ComplaintReceivedId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Satisfactionlevel_resolution", Complaintmodel.complaint.Satisfactionlevel_resolution).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintReceivedRegulatoryId", Complaintmodel.complaint.ComplaintReceivedRegulatoryId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintReceivedRegulatoryFeedbackId", Complaintmodel.complaint.ComplaintReceivedRegulatoryFeedbackId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintOverallOutcomeId", Complaintmodel.complaint.ComplaintOverallOutcomeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintCompensationId", Complaintmodel.complaint.ComplaintCompensationId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ComplaintRegulatedCostId", Complaintmodel.complaint.ComplaintRegulatedCostId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CompensationValue", Complaintmodel.complaint.CompensationValue).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Complaintmodel.complaint.IsEdit).SqlDbType = SqlDbType.Bit;


                    DataSet ds = null;
                    ds = BulkInsert("Save_Complaint", cmd);
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
        public ServiceResponse DeleteComplaint(Guid ComplaintID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (ComplaintID != Guid.Empty)
            {
                var searchValueData = new SearchValueData { Name = "ComplaintID", Value = Convert.ToString(ComplaintID) };
                searchList.Add(searchValueData);
                BaseDataProvider objnew = new BaseDataProvider();
                objnew = new BaseDataProvider();
                objnew.GetScalar("DeleteComplaint", searchList);
                response.IsSuccess = true;
                response.Message = "Record Deleted Successfully.";
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public ServiceResponse GetCompanyCustomRules(Guid CompanyID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                Company company = GetEntity<Company>(CompanyID);

                response.Data = company;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }

        public ServiceResponse GetDivisionByCompany(Guid CompanyID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "CompanyID", Value = Convert.ToString(CompanyID) });
                List<SelectListItem> Divisions = GetEntityList<SelectListItem>("GetDivisionListByCompany", searchvaluedata);

                response.Data = Divisions;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }
        public ServiceResponse GetProductByCompany(Guid CompanyID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "CompanyID", Value = Convert.ToString(CompanyID) });
                List<SelectListItem> Products = GetEntityList<SelectListItem>("GetProductListByCompany", searchvaluedata);
                response.Data = Products;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }
        public ServiceResponse GetProductByProductCategory(Guid ProductCategoryID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "ProductCategoryID", Value = Convert.ToString(ProductCategoryID) });
                List<SelectListItem> Products = GetEntityList<SelectListItem>("GetProductListByProductCategory", searchvaluedata);
                response.Data = Products;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }
        public ServiceResponse GetNatureOfComplaintList(Guid ComplaintCategoryID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "ComplaintCategoryID", Value = Convert.ToString(ComplaintCategoryID) });
                List<SelectListItem> NatureOfComplaints = GetEntityList<SelectListItem>("GetNatureOfComplaintList", searchvaluedata);
                response.Data = NatureOfComplaints;
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