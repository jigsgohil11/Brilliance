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
    public class AdmCrtDataProvider : BaseDataProvider, IAdmCrtDataProvider
    {
        public ServiceResponse GetCrtSetup(Guid ClientID)
        {
            CrtAdminViewmodel crtViewModel = new CrtAdminViewmodel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();

                searchList.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });
                crtViewModel = GetMultipleEntity<CrtAdminViewmodel>("Adm_GetCRTsetupdata", searchList);
                response.Data = crtViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

                 
            }
            return response;
        }
        public CrtAdminViewmodel GetLabelConfig(Guid TemplateID, Guid ClientID,string OrgName, bool? IsEdit)
        {
            var CrtVM = new CrtAdminViewmodel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                searchList.Add(new SearchValueData { Name = "TemplateID", Value = Convert.ToString(TemplateID) });
                searchList.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });
                searchList.Add(new SearchValueData { Name = "IsEdit", Value = Convert.ToString(IsEdit) });
                LabelConfiguration labelconfig = GetEntity<LabelConfiguration>("Crt_GetLabelsfromTemplate", searchList);
                CrtVM.labelconfig = labelconfig;
                CrtVM.labelconfig.IsEdit = IsEdit;
                CrtVM.labelconfig.ClientID = ClientID;
                CrtVM.labelconfig.OrganizationName = OrgName;
                CrtVM.labelconfig.TemplateID = TemplateID;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return CrtVM;
        }
        public ServiceResponse Savelabelconfig(string TemplateName, string Tier2, string incidentdate, string Tier3, string policystatus,
                                            string Accnumber, string Rootcause, string Idnumber, string howcomreceived, string contactno,
                                            string Compregulatory, string emailaddress, string feedbackregulatory, string productcategory, string Overalloutcome,
                                            string Producttype, string Compensation, string Compcategory, string Regulatedcost, string Nature,
                                            string Value, string TCF, string DissatisfactionLevel, string SatisfactionResolution, bool? IsTemplate, bool? IsEdit, Guid? ClientID, Guid? RefTempID)
        {
            var response = new ServiceResponse();
            try
            {

                if (IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@LabelconfigID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@RefTemplateID", RefTempID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateName", TemplateName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier2", Tier2).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier3", Tier3).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Accnumber", Accnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Idnumber", Idnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@contactno", contactno).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@emailaddress", emailaddress).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@productcategory", productcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Producttype", Producttype).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compcategory", Compcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Nature", Nature).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@TCF", TCF).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@incidentdate", incidentdate).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@policystatus", policystatus).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Rootcause", Rootcause).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@howcomreceived", howcomreceived).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compregulatory", Compregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@feedbackregulatory", feedbackregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Overalloutcome", Overalloutcome).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compensation", Compensation).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Regulatedcost", Regulatedcost).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Value", Value).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DissatisfactionLevel", DissatisfactionLevel).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SatisfactionResolution", SatisfactionResolution).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsTemplate", IsTemplate).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsEdit", IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("SaveCRT_Labelconfig", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";

                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                   cmd.Parameters.AddWithValue("@LabelconfigID", RefTempID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@RefTemplateID", RefTempID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateName", TemplateName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier2", Tier2).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier3", Tier3).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Accnumber", Accnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Idnumber", Idnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@contactno", contactno).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@emailaddress", emailaddress).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@productcategory", productcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Producttype", Producttype).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compcategory", Compcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Nature", Nature).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@TCF", TCF).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@incidentdate", incidentdate).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@policystatus", policystatus).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Rootcause", Rootcause).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@howcomreceived", howcomreceived).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compregulatory", Compregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@feedbackregulatory", feedbackregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Overalloutcome", Overalloutcome).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compensation", Compensation).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Regulatedcost", Regulatedcost).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Value", Value).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DissatisfactionLevel", DissatisfactionLevel).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SatisfactionResolution", SatisfactionResolution).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsTemplate", IsTemplate).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsEdit", IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("SaveCRT_Labelconfig", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }

        public CrtAdminViewmodel labelconfiglist(Guid ClientID)
        {
            var CrtVM = new CrtAdminViewmodel();
            try
            {
                var searchList = new List<SearchValueData>();
                searchList.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });

                List<LabelConfiguration> labellist = GetEntityList<LabelConfiguration>("GetLabelConfigList", searchList);
                //ProjecttermList.projecttermcategory.CategoryName = categorylist[0].DisplayName;
                //ProjecttermList.projecttermcategory.ProjectTermCategoryID = ProjectTermCategoryID;
                if (labellist.Count > 0)
                {
                    //CrtVM.labelconfiglist = labellist;
                }
            }
            catch (Exception ex)
            {

            }
            return CrtVM;
        }

        public CrtAdminViewmodel EditLabelConfig(Guid LabelconfigID)
        {
            var CrtVM = new CrtAdminViewmodel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                searchList.Add(new SearchValueData { Name = "LabelconfigID", Value = Convert.ToString(LabelconfigID) });
                CrtVM = GetMultipleEntity<CrtAdminViewmodel>("Crt_GetLabelConfigData", searchList);
                //CrtVM.labelconfig.IsEdit = true;
                //CrtVM.labelconfig.LabelconfigID = LabelconfigID;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return CrtVM;
        }


        public DropSelectViewmodel DropselectgList(Guid ClientID)
        {
            var CrtVM = new DropSelectViewmodel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                searchList.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });

                CrtVM = GetMultipleEntity<DropSelectViewmodel>("GetDropselectList", searchList);
                //ProjecttermList.projecttermcategory.CategoryName = categorylist[0].DisplayName;
                //ProjecttermList.projecttermcategory.ProjectTermCategoryID = ProjectTermCategoryID;
                CrtVM.dropselect.ClientID = ClientID;
                response.Data = CrtVM;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return CrtVM;
        }

        public DropselectModel AddDropSelect(string Category, Guid ClientID)
        {
            var CrtVM = new DropselectModel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                searchList.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });
                searchList.Add(new SearchValueData { Name = "Category", Value = Category });
                CrtVM = GetMultipleEntity<DropselectModel>("Crt_GetDropselectConfigData", searchList);
                CrtVM.dropselectconfig = new dropselectconfig();
                CrtVM.dropselectconfig.ClientID = ClientID;
                CrtVM.dropselectconfig.ProjecttermCategoryName = Category;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return CrtVM;
        }

        public ServiceResponse Savedropselectconfig(Guid TermId, Guid ClientID, Guid? Refid, Guid? Refid1, string name, string desc, string category, bool isedit)
        {
            var response = new ServiceResponse();
            try
            {
                if (isedit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProjecttermID", TermId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Name", name).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Desc", desc).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ProjecttermCategoryName", category).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RefTermID", Refid).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@RefTermID1", Refid1).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedOn", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsEdit", isedit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Savedropselectconfig", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProjecttermID", TermId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Name", name).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Desc", desc).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ProjecttermCategoryName", category).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RefTermID", Refid).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@RefTermID1", Refid1).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsEdit", isedit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Savedropselectconfig", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                }



            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
        public ServiceResponse Deletedropselectconfig(Guid TermId)
        {
            var response = new ServiceResponse();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ProjecttermID", TermId).SqlDbType = SqlDbType.UniqueIdentifier;

                DataSet ds = null;
                ds = BulkInsert("Deletedropselectconfig", cmd);
                response.IsSuccess = true;
                response.Message = "Record Saved Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }

        public ServiceResponse SaveCRTconfig(CrtAdminViewmodel crtadminVM)
        {
            var response = new ServiceResponse();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientID", crtadminVM.client.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@IsPortal", crtadminVM.client.IsPortal).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@ShowComplaintNumber", crtadminVM.client.ShowComplaintNumber).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@URL", crtadminVM.client.URL).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@IsShowDeclaration", crtadminVM.client.IsShowDeclaration).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@IsUploadattachment", crtadminVM.client.IsUploadattachment).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@IsTurnaroundTimer", crtadminVM.client.IsTurnaroundTimer).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Turnaround_hours", crtadminVM.client.Turnaround_hours).SqlDbType = SqlDbType.Int;
                cmd.Parameters.AddWithValue("@Turnaround_times", crtadminVM.client.Turnaround_times).SqlDbType = SqlDbType.Int;
                cmd.Parameters.AddWithValue("@Turnaround_email", crtadminVM.client.Turnaround_email).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@IsWebturnaroundTimer", crtadminVM.client.IsWebturnaroundTimer).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@WebTurnaround_hours", crtadminVM.client.WebTurnaround_hours).SqlDbType = SqlDbType.Int;
                cmd.Parameters.AddWithValue("@WebTurnaround_times", crtadminVM.client.WebTurnaround_times).SqlDbType = SqlDbType.Int;
                cmd.Parameters.AddWithValue("@WebTurnaround_email", crtadminVM.client.WebTurnaround_email).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@IsViewDissatisfationLevel", crtadminVM.client.IsViewDissatisfationLevel).SqlDbType = SqlDbType.Bit;
                cmd.Parameters.AddWithValue("@IsRequireDissatisfationLevel", crtadminVM.client.IsRequireDissatisfationLevel).SqlDbType = SqlDbType.Bit;
                cmd.Parameters.AddWithValue("@IsViewSatisfationLevel", crtadminVM.client.IsViewSatisfationLevel).SqlDbType = SqlDbType.Bit;
                cmd.Parameters.AddWithValue("@IsRequireSatisfationLevel", crtadminVM.client.IsRequireSatisfationLevel).SqlDbType = SqlDbType.Bit;

                DataSet ds = null;
                ds = BulkInsert("SaveCRTconfig", cmd);
                response.IsSuccess = true;
                response.Message = "Record Saved Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }

        public ServiceResponse SavedropselectInTemplate(Guid ClientID, Guid TemplateID)
        {
            var CrtVM = new CrtAdminViewmodel();
            var response = new ServiceResponse();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ClientID", ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@TemplateID", TemplateID).SqlDbType = SqlDbType.UniqueIdentifier;
                DataSet ds = null;
                ds = BulkInsert("SavedropselectInTemplate", cmd);
                response.IsSuccess = true;
                response.Message = "Record Saved Successfully.";
            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }
            return response;
        }

        public ServiceResponse GetComplaintReasonList(Guid ComplaintTypeID, Guid ClientID)
        {
            var response = new ServiceResponse();
            var searchvaluedata = new List<SearchValueData>();
            try
            {
                searchvaluedata.Add(new SearchValueData { Name = "ComplaintTypeID", Value = Convert.ToString(ComplaintTypeID) });
                searchvaluedata.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });
                List<SelectListItem> NatureOfComplaints = GetEntityList<SelectListItem>("GetCustomerReasonList", searchvaluedata);
                response.Data = NatureOfComplaints;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.Message = "Internal Server Error";
            }
            return response;
        }

        public CrtAdminViewmodel GetTemplateList()
        {
            var response = new ServiceResponse();
            var CrtVM = new CrtAdminViewmodel();
            try
            {
                var searchList = new List<SearchValueData>();
               
                //searchList.Add(new SearchValueData { Name = "ClientID", Value = Convert.ToString(ClientID) });
                CrtVM = GetMultipleEntity<CrtAdminViewmodel>("GetTemplateList", searchList);
                response.Data = CrtVM;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return CrtVM;
        }
    }
}