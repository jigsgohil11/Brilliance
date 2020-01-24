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
        public ServiceResponse GetCrtSetup()
        {
            CrtAdminViewmodel crtViewModel = new CrtAdminViewmodel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                crtViewModel = GetMultipleEntity<CrtAdminViewmodel>("Adm_GetCRTsetupdata", searchList);
               
                response.Data = crtViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }
        public ServiceResponse Savelabelconfig(CrtAdminViewmodel CRTAdminVM)
        {
            var response = new ServiceResponse();
            try
            {

                if (CRTAdminVM.labelconfig.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@LabelconfigID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", CRTAdminVM.labelconfig.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@instanceName", CRTAdminVM.labelconfig.InstanceName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier2", CRTAdminVM.labelconfig.Tier2).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier3", CRTAdminVM.labelconfig.Tier3).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Accnumber", CRTAdminVM.labelconfig.Accnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Idnumber", CRTAdminVM.labelconfig.Idnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@contactno", CRTAdminVM.labelconfig.contactno).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@emailaddress", CRTAdminVM.labelconfig.emailaddress).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@productcategory", CRTAdminVM.labelconfig.productcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Producttype", CRTAdminVM.labelconfig.Producttype).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compcategory", CRTAdminVM.labelconfig.Compcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Nature", CRTAdminVM.labelconfig.Nature).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@TCF", CRTAdminVM.labelconfig.TCF).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@incidentdate", CRTAdminVM.labelconfig.incidentdate).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@policystatus", CRTAdminVM.labelconfig.policystatus).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Rootcause", CRTAdminVM.labelconfig.Rootcause).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@howcomreceived", CRTAdminVM.labelconfig.howcomreceived).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compregulatory", CRTAdminVM.labelconfig.Compregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@feedbackregulatory", CRTAdminVM.labelconfig.feedbackregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Overalloutcome", CRTAdminVM.labelconfig.Overalloutcome).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compensation", CRTAdminVM.labelconfig.Compensation).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Regulatedcost", CRTAdminVM.labelconfig.Regulatedcost).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Value", CRTAdminVM.labelconfig.Value).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DissatisfactionLevel", CRTAdminVM.labelconfig.DissatisfactionLevel).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SatisfactionResolution", CRTAdminVM.labelconfig.SatisfactionResolution).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsTemplate", CRTAdminVM.labelconfig.IsTemplate).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@CreatedBy", CRTAdminVM.labelconfig.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", CRTAdminVM.labelconfig.CreatedOn).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsEdit", CRTAdminVM.labelconfig.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("SaveCRT_Labelconfig", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";

                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@LabelconfigID", CRTAdminVM.labelconfig.LabelconfigID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", CRTAdminVM.labelconfig.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@instanceName", CRTAdminVM.labelconfig.InstanceName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier2", CRTAdminVM.labelconfig.Tier2).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Tier3", CRTAdminVM.labelconfig.Tier3).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Accnumber", CRTAdminVM.labelconfig.Accnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Idnumber", CRTAdminVM.labelconfig.Idnumber).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@contactno", CRTAdminVM.labelconfig.contactno).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@emailaddress", CRTAdminVM.labelconfig.emailaddress).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@productcategory", CRTAdminVM.labelconfig.productcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Producttype", CRTAdminVM.labelconfig.Producttype).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compcategory", CRTAdminVM.labelconfig.Compcategory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Nature", CRTAdminVM.labelconfig.Nature).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@TCF", CRTAdminVM.labelconfig.TCF).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@incidentdate", CRTAdminVM.labelconfig.incidentdate).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@policystatus", CRTAdminVM.labelconfig.policystatus).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Rootcause", CRTAdminVM.labelconfig.Rootcause).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@howcomreceived", CRTAdminVM.labelconfig.howcomreceived).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compregulatory", CRTAdminVM.labelconfig.Compregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@feedbackregulatory", CRTAdminVM.labelconfig.feedbackregulatory).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Overalloutcome", CRTAdminVM.labelconfig.Overalloutcome).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Compensation", CRTAdminVM.labelconfig.Compensation).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Regulatedcost", CRTAdminVM.labelconfig.Regulatedcost).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Value", CRTAdminVM.labelconfig.Value).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DissatisfactionLevel", CRTAdminVM.labelconfig.DissatisfactionLevel).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@SatisfactionResolution", CRTAdminVM.labelconfig.SatisfactionResolution).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsTemplate", CRTAdminVM.labelconfig.IsTemplate).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@CreatedBy", CRTAdminVM.labelconfig.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedOn", CRTAdminVM.labelconfig.CreatedOn).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsEdit", CRTAdminVM.labelconfig.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("SaveCRT_Labelconfig", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record updated Successfully.";
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
                    CrtVM.labelconfiglist = labellist;
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
                CrtVM.labelconfig.IsEdit = true;
                CrtVM.labelconfig.LabelconfigID = LabelconfigID;
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

        public ServiceResponse Savedropselectconfig(Guid ClientID, Guid? Refid, Guid? Refid1, string name, string desc, string category)
        {
            var response = new ServiceResponse();
            try
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@ProjecttermID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@ClientID", ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@Name", name).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@Desc", desc).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@ProjecttermCategoryName", category).SqlDbType = SqlDbType.NVarChar;
                cmd.Parameters.AddWithValue("@RefTermID", Refid).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@RefTermID1", Refid1).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CreatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CreatedOn", DateTime.Now).SqlDbType = SqlDbType.DateTime;
                //cmd.Parameters.AddWithValue("@IsEdit", CRTAdminVM.labelconfig.IsEdit).SqlDbType = SqlDbType.Bit;

                DataSet ds = null;
                ds = BulkInsert("Savedropselectconfig", cmd);
                response.IsSuccess = true;
                response.Message = "Record Saved Successfully.";

                //if (CRTAdminVM.labelconfig.IsEdit == false)
                //{
                //    SqlCommand cmd = new SqlCommand();
                //    cmd.Parameters.AddWithValue("@LabelconfigID", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                //    cmd.Parameters.AddWithValue("@ClientID", CRTAdminVM.labelconfig.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                //    cmd.Parameters.AddWithValue("@IsEdit", CRTAdminVM.labelconfig.IsEdit).SqlDbType = SqlDbType.Bit;

                //    DataSet ds = null;
                //    ds = BulkInsert("SaveCRT_Labelconfig", cmd);
                //    response.IsSuccess = true;
                //    response.Message = "Record Saved Successfully.";

                //}
                //else
                //{
                //    SqlCommand cmd = new SqlCommand();
                //    cmd.Parameters.AddWithValue("@LabelconfigID", CRTAdminVM.labelconfig.LabelconfigID).SqlDbType = SqlDbType.UniqueIdentifier;
                //    cmd.Parameters.AddWithValue("@ClientID", CRTAdminVM.labelconfig.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                //    cmd.Parameters.AddWithValue("@instanceName", CRTAdminVM.labelconfig.InstanceName).SqlDbType = SqlDbType.NVarChar;
                //    cmd.Parameters.AddWithValue("@IsEdit", CRTAdminVM.labelconfig.IsEdit).SqlDbType = SqlDbType.Bit;

                //    DataSet ds = null;
                //    ds = BulkInsert("SaveCRT_Labelconfig", cmd);
                //    response.IsSuccess = true;
                //    response.Message = "Record updated Successfully.";
                //}




            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
    }
}