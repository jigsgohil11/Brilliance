using Brilliance.Models;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Brilliance.Infrastructure.DataProvider
{
    public class WebsubmissionDataProvider : BaseDataProvider, IWebsubmissionDataProvider
    {
        public ServiceResponse GetComplaint()
        {
            ComplaintViewModel Complaintmodel = new ComplaintViewModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "ComplaintID" ,Value = Convert.ToString(Guid.Empty) },
                        new SearchValueData { Name = "ClientID" ,Value = "B3E5F363-8293-4069-8239-4220AA40CA26"}
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

        public ServiceResponse SaveComplaints(ComplaintViewModel Complaintmodel)
        {
            var response = new ServiceResponse();
            try
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
                cmd.Parameters.AddWithValue("@DateReceived",DateTime.Now).SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.AddWithValue("@DateIncident", Complaintmodel.complaint.DateIncident).SqlDbType = SqlDbType.DateTime;
                cmd.Parameters.AddWithValue("@IsResolved", "New").SqlDbType = SqlDbType.NVarChar;
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
                cmd.Parameters.AddWithValue("@Description", Complaintmodel.complaint.Notes).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@ComplaintType", "Web Submission").SqlDbType = SqlDbType.NVarChar;
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
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
    }
}