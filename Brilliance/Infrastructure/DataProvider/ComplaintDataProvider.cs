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
                        new SearchValueData { Name = "ComplaintID" ,Value = Convert.ToString(ComplaintID)},
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(SessionHelper.ClientID)},
                        new SearchValueData { Name = "CompanyID" ,Value = Convert.ToString(SessionHelper.CompanyID)},
                        new SearchValueData { Name = "DivisionID" ,Value = Convert.ToString(SessionHelper.DivisionID)},
                        new SearchValueData { Name = "RoleName" ,Value = Convert.ToString(SessionHelper.RoleName)}
                };

                Complaintmodel = GetMultipleEntity<ComplaintViewModel>("GetComplaintdata", searchList);
                //Complaintmodel.complaint.ClientId = SessionHelper.ClientID;
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
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "UserID" ,Value = Convert.ToString(SessionHelper.UserId)},
                        new SearchValueData { Name = "RoleName" ,Value = Convert.ToString(SessionHelper.RoleName)}
                };

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
        public ServiceResponse SaveComplaints(ComplaintViewModel Complaintmodel, DataTable complaint_note)
        {
            var response = new ServiceResponse();
            try
            {

                if (Complaintmodel.complaint.IsEdit == false)
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
                    if (Complaintmodel.complaint.IsComplaint != null && Complaintmodel.complaint.IsComplaint.ToUpper() == "NO")
                        cmd.Parameters.AddWithValue("@IsComplaint", "No").SqlDbType = SqlDbType.NVarChar;
                    else
                        cmd.Parameters.AddWithValue("@IsComplaint", "Yes").SqlDbType = SqlDbType.NVarChar;
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
                    cmd.Parameters.AddWithValue("@DateReceived", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@DateIncident", Complaintmodel.complaint.DateIncident).SqlDbType = SqlDbType.DateTime;
                    if (Complaintmodel.complaint.IsComplaint != null && Complaintmodel.complaint.IsComplaint.ToUpper() == "NO")
                    {
                        cmd.Parameters.AddWithValue("@IsResolved", "Non-Complaint").SqlDbType = SqlDbType.NVarChar;

                    }
                    else
                    {
                        if (Complaintmodel.complaint.IsResolved != null && Complaintmodel.complaint.IsResolved.ToUpper() == "YES")
                            cmd.Parameters.AddWithValue("@IsResolved", "Resolved").SqlDbType = SqlDbType.NVarChar;
                        else
                            cmd.Parameters.AddWithValue("@IsResolved", "Not yet").SqlDbType = SqlDbType.NVarChar;
                    }

                    if (Complaintmodel.complaint.IsComplaint != null && Complaintmodel.complaint.IsComplaint.ToUpper() == "NO")
                        cmd.Parameters.AddWithValue("@DateResolved", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    else
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
                    cmd.Parameters.AddWithValue("@ComplaintType", "Manual").SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@TCFOutcome", Complaintmodel.complaint.TCFOutcome).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Eventtype", Complaintmodel.complaint.Eventtype).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Descofloss", Complaintmodel.complaint.Descofloss).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@AssignTo", Complaintmodel.complaint.AssignTo).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Complaintmodel.complaint.IsEdit).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@dtcomplaintNotes", complaint_note).SqlDbType = SqlDbType.Structured;


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
                    if (Complaintmodel.complaint.IsComplaint != null && Complaintmodel.complaint.IsComplaint.ToUpper() == "NO")
                        cmd.Parameters.AddWithValue("@IsComplaint", "No").SqlDbType = SqlDbType.NVarChar;
                    else
                        cmd.Parameters.AddWithValue("@IsComplaint", "Yes").SqlDbType = SqlDbType.NVarChar;
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
                    if (Complaintmodel.complaint.IsComplaint != null && Complaintmodel.complaint.IsComplaint.ToUpper() == "NO")
                    {
                        cmd.Parameters.AddWithValue("@IsResolved", "Non-Complaint").SqlDbType = SqlDbType.NVarChar;

                    }
                    else
                    {
                        if (Complaintmodel.complaint.IsResolved != null && Complaintmodel.complaint.IsResolved.ToUpper() == "YES")
                            cmd.Parameters.AddWithValue("@IsResolved", "Resolved").SqlDbType = SqlDbType.NVarChar;
                        else
                            cmd.Parameters.AddWithValue("@IsResolved", "Not yet").SqlDbType = SqlDbType.NVarChar;
                    }

                    if (Complaintmodel.complaint.IsComplaint != null && Complaintmodel.complaint.IsComplaint.ToUpper() == "NO")
                        cmd.Parameters.AddWithValue("@DateResolved", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    else
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
                    cmd.Parameters.AddWithValue("@TCFOutcome", Complaintmodel.complaint.TCFOutcome).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Eventtype", Complaintmodel.complaint.Eventtype).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Descofloss", Complaintmodel.complaint.Descofloss).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@AssignTo", Complaintmodel.complaint.AssignTo).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", Complaintmodel.complaint.IsEdit).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@dtcomplaintNotes", complaint_note).SqlDbType = SqlDbType.Structured;


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
                searchvaluedata.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(SessionHelper.ClientID) });
                List<SelectListItem> Products = GetEntityList<SelectListItem>("GetProductTypeList", searchvaluedata);
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
                searchvaluedata.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(SessionHelper.ClientID) });
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

        public ServiceResponse GetTCFOutcome(Guid ComplaintCategoryID, Guid NatureID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "ComplaintCategoryID", Value = Convert.ToString(ComplaintCategoryID) });
                searchvaluedata.Add(new SearchValueData { Name = "NatureID", Value = Convert.ToString(NatureID) });
                searchvaluedata.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(SessionHelper.ClientID) });
                projectterm projectterm = GetEntity<projectterm>("GetTCFOutcome", searchvaluedata);
                response.Data = projectterm;
                if (response.Data == null)
                {
                    projectterm projectterm1 = new projectterm();
                    response.Data = projectterm1;
                }
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }

        public ServiceResponse GetReport()
        {
            ComplaintReportViewModel reportmodel = new ComplaintReportViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "RoleName" ,Value = Convert.ToString(SessionHelper.RoleName)},
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(SessionHelper.ClientID)}
                       // new SearchValueData { Name = "RoleName" ,Value = "Level 3"}
                };

                reportmodel = GetMultipleEntity<ComplaintReportViewModel>("GetReportType", searchList);
                response.Data = reportmodel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public ServiceResponse GetCompanyReport()
        {
            ComplaintReportModel reportmodel = new ComplaintReportModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(SessionHelper.ClientID)}
                       // new SearchValueData { Name = "RoleName" ,Value = "Level 3"}
                };

                reportmodel = GetMultipleEntity<ComplaintReportModel>("GetCompanyReport", searchList);
                response.Data = reportmodel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public ServiceResponse GetOrganisationReport()
        {
            ComplaintReportModel reportmodel = new ComplaintReportModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                       new SearchValueData { Name = "RoleName" ,Value = Convert.ToString(SessionHelper.RoleName)}
                };

                reportmodel = GetMultipleEntity<ComplaintReportModel>("GetOrgReport", searchList);
                response.Data = reportmodel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public ServiceResponse GetDivisionReport()
        {
            ComplaintReportModel reportmodel = new ComplaintReportModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(SessionHelper.ClientID)}
                       // new SearchValueData { Name = "RoleName" ,Value = "Level 3"}
                };

                reportmodel = GetMultipleEntity<ComplaintReportModel>("GetDivisionReport", searchList);
                response.Data = reportmodel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ServiceResponse GetDivbyCompany(Guid CompanyID)
        {
            
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "CompanyID" ,Value = Convert.ToString(CompanyID)},
                        new SearchValueData { Name = "ClientID" ,Value = Convert.ToString(SessionHelper.ClientID)}
                };

                List<SelectListItem> divList = GetEntityList<SelectListItem>("GetDivisionByCompany", searchList);
                response.Data = divList;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        //public ServiceResponse GetCRTDataPdf()
        //{
        //    var response = new ServiceResponse();
        //   // ApplicantReportViewModel ApplicantReportVM = new ApplicantReportViewModel();
        //    var searchList = new List<SearchValueData>();
        //    try
        //    {
        //        var searchValueData = new SearchValueData { Name = "CandidateID", Value = Convert.ToString(CandidateID) };
        //        searchList.Add(searchValueData);
        //        var searchValueData1 = new SearchValueData { Name = "RequisitionDetailID", Value = Convert.ToString(RequisitionDetailID) };
        //        searchList.Add(searchValueData1);

        //        ApplicantReportVM = GetMultipleEntity<ApplicantReportViewModel>("Hr_GetApplicantDetailsForReport", searchList);

        //        response.Data = ApplicantReportVM;
        //    }
        //    catch (Exception ex)
        //    {
        //        response.Message = "Server Error";
        //    }

        //    return response;
        //}



    }
}