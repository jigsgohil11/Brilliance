using System;
using System.Collections.Generic;
using System.Linq;
using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Brilliance.Infrastructure.DataProvider
{
    public class GeneralSetupDataProvider : BaseDataProvider, IGeneralSetupDataProvider
    {
        public ProjectTermListByCategory TermlistbyCategory()
        {
            var ProjecttermList = new ProjectTermListByCategory();
            try
            {
                var searchList = new List<SearchValueData>();

                ProjecttermList = GetMultipleEntity<ProjectTermListByCategory>("ProjectTermListByCategory");//Constants.GetUserGroupList
                if (ProjecttermList.CategoryList.Count > 0)
                {
                    ProjecttermList.Response.IsSuccess = true;
                }
                else
                {
                    ProjecttermList.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                ProjecttermList.Response.Message = "Internal server error";
            }
            return ProjecttermList;
        }
        public ProjectTermListByCategory CategoryList(Guid ProjectTermCategoryID)
        {
            var ProjecttermList = new ProjectTermListByCategory();
            try
            {
                var searchList = new List<SearchValueData>();
                searchList.Add(new SearchValueData { Name = "ProjectTermCategoryID", Value = Convert.ToString(ProjectTermCategoryID) });

                List<ProjectTermCategoryList> categorylist = GetEntityList<ProjectTermCategoryList>("ProjectTermCategoryList", searchList);
                ProjecttermList.projecttermcategory.TermCategory = categorylist[0].DisplayName;
                ProjecttermList.projecttermcategory.ProjectTermcategoryID = ProjectTermCategoryID;
                if (categorylist.Count > 0)
                {
                    ProjecttermList.Projecttermcategorylist = categorylist;
                    ProjecttermList.Response.IsSuccess = true;
                }
                else
                {
                    ProjecttermList.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                ProjecttermList.Response.Message = "Internal server error";
            }
            return ProjecttermList;
        }

        public ProjectTermListByCategory AddGeneralsetupPopup(Guid Categoryid, string CategoryName)
        {
            ProjectTermListByCategory Projecttermlist = new ProjectTermListByCategory();
            var searchList = new List<SearchValueData>();
            List<SelectListItem> SectortypeList = new List<SelectListItem>();
            try
            {

                searchList.Add(new SearchValueData { Name = "ProjectTermID", Value = Convert.ToString(Guid.Empty) });
                Projecttermlist = GetMultipleEntity<ProjectTermListByCategory>("Setup_GetGeneralSetup", searchList);
                //Projecttermlist.SectorList = SectortypeList;
                Projecttermlist.projecttermcategory = new ProjectTermCategory();
                Projecttermlist.projecttermcategory.ProjectTermcategoryID = Categoryid;
                Projecttermlist.projecttermcategory.CategoryName = CategoryName;
                Projecttermlist.Response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                Projecttermlist.Response.Message = "Server Error";
            }

            return Projecttermlist;
        }

        public ProjectTermListByCategory GeneralsetupPopup(Guid ProjectTermID)
        {
            var ProjecttermList = new ProjectTermListByCategory();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();
                searchList.Add(new SearchValueData { Name = "ProjectTermID", Value = Convert.ToString(ProjectTermID) });
                ProjecttermList = GetMultipleEntity<ProjectTermListByCategory>("Setup_GetGeneralSetup", searchList);
                response.Data = ProjecttermList;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                ProjecttermList.Response.Message = "Internal server error";
            }

            return ProjecttermList;
        }

        public ServiceResponse SaveGeneralSetup1(ProjectTermListByCategory ProjectCategoryModel)
        {
            var response = new ServiceResponse();
            try
            {
                if (ProjectCategoryModel.projecttermcategory != null)
                {
                    GeneralSetupDataProvider obj = new GeneralSetupDataProvider();

                    if (ProjectCategoryModel.projecttermcategory.CategoryName.ToUpper() != "ASSIGNMENT ROLE")
                    {
                        string strChkDataDuplication = obj.CheckDataDuplicationForProjectTerms("TermID", "mst_ProjectTerm", null, ProjectCategoryModel.projecttermcategory.TermID, "Term", ProjectCategoryModel.projecttermcategory.Term.Trim(), "ProjectTermcategoryID", ProjectCategoryModel.projecttermcategory.ProjectTermcategoryID.ToString());

                        var Term = ProjectCategoryModel.projecttermcategory.CategoryName;
                        if (!string.IsNullOrEmpty(strChkDataDuplication))
                        {
                            if (strChkDataDuplication.Contains("Term"))
                            {
                                strChkDataDuplication = strChkDataDuplication.Replace("Term ", Term + "Name ");
                            }
                            //if (strChkDataDuplication.Contains("TermCode"))
                            //{
                            //    strChkDataDuplication = strChkDataDuplication.Replace("TermCode", Term + "Code");
                            //}
                            if (strChkDataDuplication.Contains("ProjectTermCategoryId"))
                            {
                                strChkDataDuplication = strChkDataDuplication.Replace("ProjectTermCategoryId and", "");
                            }
                            response.IsSuccess = false;
                            response.Message = string.Format("{0} {1}", strChkDataDuplication, "Already Exists");
                            return response;
                        }
                    }
                }
                if (ProjectCategoryModel.projecttermcategory.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    Guid ProjectTermID1 = Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@ProjectTermID", ProjectTermID1).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ProjectCategoryID", ProjectCategoryModel.projecttermcategory.ProjectTermcategoryID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TermCode", ProjectCategoryModel.projecttermcategory.TermCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Term", ProjectCategoryModel.projecttermcategory.Term).SqlDbType = SqlDbType.NVarChar;
                    //cmd.Parameters.AddWithValue("@DefaultValue2", ProjectCategoryModel.projecttermcategory.DefaultValue2).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsEdit", ProjectCategoryModel.projecttermcategory.IsEdit).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@Category", ProjectCategoryModel.projecttermcategory.CategoryName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RefTermID", (ProjectCategoryModel.projecttermcategory.RefTermID)).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", ProjectCategoryModel.projecttermcategory.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;

                    DataSet ds = null;
                    ds = BulkInsert("Crm_SaveGeneralSetup", cmd);

                    response.IsSuccess = true;
                    response.Message = "Record Saved Successfully.";
                    //ProjectCategoryModel.projecttermcategory.TermID = ProjectTermID1;
                    //ProjectTermCategory newmodel = GetEntity<ProjectTermCategory>(ProjectCategoryModel.projecttermcategory.TermID);
                    //Common.ActionLog(Constants.Insert, ProjectCategoryModel.projecttermcategory.CategoryName, "SaveGeneralSetup1", null, Common.SerializeObject(newmodel));
                }
                else
                {
                    //ProjectTermCategory oldmodel = GetEntity<ProjectTermCategory>(ProjectCategoryModel.projecttermcategory.ProjectTermID);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@ProjectTermID", ProjectCategoryModel.projecttermcategory.TermID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ProjectCategoryID", ProjectCategoryModel.projecttermcategory.ProjectTermcategoryID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TermCode", ProjectCategoryModel.projecttermcategory.TermCode).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Term", ProjectCategoryModel.projecttermcategory.Term).SqlDbType = SqlDbType.NVarChar;
                    //cmd.Parameters.AddWithValue("@DefaultValue2", ProjectCategoryModel.projecttermcategory.DefaultValue2).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsEdit", ProjectCategoryModel.projecttermcategory.IsEdit).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@Category", ProjectCategoryModel.projecttermcategory.CategoryName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@RefTermID", ProjectCategoryModel.projecttermcategory.RefTermID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClientID", ProjectCategoryModel.projecttermcategory.ClientID).SqlDbType = SqlDbType.UniqueIdentifier;
                    DataSet ds = null;
                    ds = BulkInsert("Crm_SaveGeneralSetup", cmd);
                    response.IsSuccess = true;
                    response.Message = "Record Updated Successfully.";
                    //ProjectTermCategory newmodel = GetEntity<ProjectTermCategory>(ProjectCategoryModel.projecttermcategory.ProjectTermID);
                    //Common.ActionLog(Constants.Update, ProjectCategoryModel.projecttermcategory.CategoryName, "SaveGeneralSetup1", Common.SerializeObject(oldmodel), Common.SerializeObject(newmodel));
                }

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }
        public ServiceResponse DeleteSetup(Guid ProjectTermID)
        {
            var response = new ServiceResponse();
            var searchList = new List<SearchValueData>();
            if (ProjectTermID != Guid.Empty)
            {
                ProjectTermListByCategory ProjectCategoryModel = new ProjectTermListByCategory();
                ProjectCategoryModel.projecttermcategory.TermID = ProjectTermID;
                ProjectTermCategory oldmodel = GetEntity<ProjectTermCategory>(ProjectCategoryModel.projecttermcategory.TermID);
                ProjectTermListByCategory ProjectTermList = GeneralsetupPopup(ProjectTermID);
                //Common.ActionLog(Constants.Delete, ProjectTermList.projecttermcategory.CategoryName, "DeleteSetup", Common.SerializeObject(oldmodel), null);

                var searchValueData = new SearchValueData { Name = "ProjectTermID", Value = Convert.ToString(ProjectTermID) };
                searchList.Add(searchValueData);
                BaseDataProvider objnew = new BaseDataProvider();
                objnew = new BaseDataProvider();
                objnew.GetScalar("DeleteSetupByProjectTermID", searchList);
                response.IsSuccess = true;
            }
            else
            {
                response.IsSuccess = false;
            }
            return response;
        }

        public string CheckDataDuplicationForProjectTerms(string PrimaryKey, string TableName, Guid? ClientID, Guid? PrimaryKeyValue, string FieldName1, string Value1, string FieldName2 = null, string Value2 = null, string FieldName3 = null, string Value3 = null, string FieldName4 = null, string Value4 = null)
        {
            string[] CheckDataDuplicationToreturn = null;
            BaseDataProvider obj = new BaseDataProvider();
            var searchList = new List<SearchValueData>();
            List<DuplicateDataCheck> duplicateDataCheckList = new List<DuplicateDataCheck>();

            try
            {
                if (!string.IsNullOrEmpty(PrimaryKey) && !string.IsNullOrEmpty(FieldName1) && !string.IsNullOrEmpty(Value1) && !string.IsNullOrEmpty(TableName))
                {
                    var searchValueData = new SearchValueData { Name = "PrimaryKey", Value = PrimaryKey };
                    searchList.Add(searchValueData);

                    var searchValueData1 = new SearchValueData { Name = "TableName", Value = TableName };
                    searchList.Add(searchValueData1);

                    var searchValueData2 = new SearchValueData { Name = "FieldName1", Value = FieldName1 };
                    searchList.Add(searchValueData2);

                    var searchValueData3 = new SearchValueData { Name = "Value1", Value = Value1 };
                    searchList.Add(searchValueData3);

                    var searchValueData4 = new SearchValueData { Name = "FieldName2", Value = FieldName2 };
                    searchList.Add(searchValueData4);

                    var searchValueData5 = new SearchValueData { Name = "Value2", Value = Value2 };
                    searchList.Add(searchValueData5);

                    var searchValueData6 = new SearchValueData { Name = "FieldName3", Value = FieldName3 };
                    searchList.Add(searchValueData6);

                    var searchValueData7 = new SearchValueData { Name = "Value3", Value = Value3 };
                    searchList.Add(searchValueData7);

                    var searchValueData8 = new SearchValueData { Name = "FieldName4", Value = FieldName4 };
                    searchList.Add(searchValueData8);

                    var searchValueData9 = new SearchValueData { Name = "Value4", Value = Value4 };
                    searchList.Add(searchValueData9);

                    var searchValueData10 = new SearchValueData { Name = "ClientID", Value = (ClientID != null ? Convert.ToString(ClientID) : null) };
                    searchList.Add(searchValueData10);

                    duplicateDataCheckList = obj.GetEntityList<DuplicateDataCheck>("mst_CHECK_FOR_DUPLICATATION_PROJECTTERM", searchList);
                }
            }
            catch (Exception ex)
            {

            }

            var query = from c in duplicateDataCheckList
                        where (c.ColumnName == FieldName1 && c.PrimariKey.ToUpper() != Convert.ToString(PrimaryKeyValue).ToUpper()) || (c.ColumnName == FieldName2 && c.PrimariKey.ToUpper() != Convert.ToString(PrimaryKeyValue).ToUpper()) || (c.ColumnName == FieldName3 && c.PrimariKey.ToUpper() != Convert.ToString(PrimaryKeyValue).ToUpper()) || (c.ColumnName == FieldName4 && c.PrimariKey.ToUpper() != Convert.ToString(PrimaryKeyValue).ToUpper())
                        select c.ColumnName;
            CheckDataDuplicationToreturn = query.ToArray();

            return (CheckDataDuplicationToreturn != null && CheckDataDuplicationToreturn.Length > 0) ? string.Join(" and ", CheckDataDuplicationToreturn) : "";
        }
    }
}