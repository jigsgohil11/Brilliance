using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Infrastructure.DataProvider
{
    public class TemplatemasterDataProvider : BaseDataProvider, ITemplateMaster
    {
        public ServiceResponse SaveTemplateMaster(TemplateMaster _model)
        {
            var response = new ServiceResponse();
            try
            {


                if (_model.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@TemplateId", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateName", _model.TemplateName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Datecreated", _model.Datecreated).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedByID", _model.CreatedByID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DateModfied", _model.DateModfied).SqlDbType = SqlDbType.DateTime;

                    cmd.Parameters.AddWithValue("@ModifybyId", _model.ModifybyId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit",false).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsDeleted", false).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_TemplateMaster", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@TemplateId", _model.TemplateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateName", _model.TemplateName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Datecreated", _model.Datecreated).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedByID", _model.CreatedByID).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ModifybyId", _model.ModifybyId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DateModfied", _model.DateModfied).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsEdit", true).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsDeleted",false).SqlDbType = SqlDbType.Bit;


                    DataSet ds = null;
                    ds = BulkInsert("Save_TemplateMaster", cmd);
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


        public TemplateViewModel TemplateMasterList()
        {
            var _TemplateMasterList = new TemplateViewModel();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                };

                List<TemplateMaster> _TemplateMaster = GetEntityList<TemplateMaster>("_GetTemplates", searchList);//Constants.GetUserGroupList
                if (_TemplateMaster.Count > 0)
                {
                    _TemplateMasterList.TemplateMaster = _TemplateMaster;
                    _TemplateMasterList.Response.IsSuccess = true;
                }
                else
                {
                    _TemplateMasterList.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _TemplateMasterList;
        }

        public TemplateMaster GetTemplateById(Guid Id)
        {
            var TemplateMaster = new TemplateMaster();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "TemplateId" ,Value = Convert.ToString(Id)}
                };
                TemplateMaster = GetEntity<TemplateMaster>("_GetTemplateById", searchList);//Constants.GetUserGroupList
                if (TemplateMaster!=null)
                {
                    return TemplateMaster;


                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return TemplateMaster;
        }

        public List<SelectListItem> GetTemplatemaster()
        {
            var TCFQuestionGroup = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();

                List<SelectListItem> _TCFQuestionGroup = GetEntityList<SelectListItem>("_GetTemplatemaster", searchList);//Constants.GetUserGroupList
                if (_TCFQuestionGroup != null)
                {
                    TCFQuestionGroup = _TCFQuestionGroup;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return TCFQuestionGroup;
        }


        public int MasterCont(Guid TemplateId)
        {
            int count = 0;

            try
            {
                var searchList = new List<SearchValueData>()
                {

                         new SearchValueData { Name = "TemplateId" ,Value = Convert.ToString(TemplateId)}
                };
                var _count = GetScalar("_PrcTemplateCount", searchList);
                if (_count != null)
                {
                    count = Convert.ToInt32(_count);
                }

            }
            catch (Exception ex)
            {
                count = 0;
            }
            return count;
        }

        public string Prcoutcometitle(Guid TemplateId)
        {
            string title = string.Empty;

            try
            {
                var searchList = new List<SearchValueData>()
                {

                         new SearchValueData { Name = "TemplateId" ,Value = Convert.ToString(TemplateId)}
                };
                var _title = GetScalar("_Prctemplateoutcometitle", searchList);
                if (_title != null)
                {
                    title = _title.ToString();
                }

            }
            catch (Exception ex)
            {
                title = string.Empty;
            }
            return title;
        }
        public ServiceResponse  Addoutcomemastertemplate(TemplateOutcomemaster _model)
        {
            var response = new ServiceResponse();
            try
            {
                if (_model.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@TemplateOutcomemasterId", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateId", _model.TemplateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code",_model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Title",_model.Title).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description",_model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreateDate",_model.Createddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@Createdby", _model.Createdby).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Isedit", _model.IsEdit).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsVisible", _model.IsVisible).SqlDbType = SqlDbType.Bit;



                    DataSet ds = null;
                    ds = BulkInsert("_Savetemplateoutcomemaster", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";

                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@TemplateOutcomemasterId", _model.TemplateOutcomemasterId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateId", _model.TemplateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Title", _model.Title).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreateDate", _model.Createddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@Createdby", _model.Createdby).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Isedit", _model.IsEdit).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@IsVisible", _model.IsVisible).SqlDbType = SqlDbType.Bit;


                    DataSet ds = null;
                    ds = BulkInsert("_Savetemplateoutcomemaster", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Update Successfully.";

                }
            }
            catch(Exception ex)
            {

            }


            return response;
        }

        public TemplateViewModel TemplateOutcomeList()
        {
            var TemplateOutcomemaster = new TemplateViewModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<TemplateOutcomemaster> _TemplateOutcomemaster = GetEntityList<TemplateOutcomemaster>("_GetTemplateoutcomelist", searchList);//Constants.GetUserGroupList
                if (_TemplateOutcomemaster.Count > 0)
                {
                    TemplateOutcomemaster._TemplateOutcomemaster = _TemplateOutcomemaster;
                    TemplateOutcomemaster.Response.IsSuccess = true;
                }
                else
                {
                    TemplateOutcomemaster.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return TemplateOutcomemaster;
        }

        public TemplateOutcomemaster GetTemplateOutcomeById(Guid Id)
        {
            var _TemplateOutcomemaster = new TemplateOutcomemaster();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "TemplateId" ,Value = Convert.ToString(Id)}
                };
                _TemplateOutcomemaster = GetEntity<TemplateOutcomemaster>("GetTemplateOutcomeById", searchList);//Constants.GetUserGroupList
                if (_TemplateOutcomemaster != null)
                {
                    return _TemplateOutcomemaster;


                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _TemplateOutcomemaster;
        }
        public List<SelectListItem> GetTemplateGroupcode()
        {
            var _Bindclient = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();

                List<SelectListItem> client = GetEntityList<SelectListItem>("GetTemplateGroupcode", searchList);//Constants.GetUserGroupList
                if (client != null)
                {
                    _Bindclient = client;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _Bindclient;
        }
        public int GrupcntByEntity(int Id, Guid TemplateId)
        {
            int count = 0;
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "gId" ,Value = Convert.ToString(Id )},
                         new SearchValueData { Name = "TemplateId" ,Value = Convert.ToString(TemplateId)}
                };

                var _count = GetScalar("Grouptemplatecount", searchList);
                if (_count != null)
                {
                    count = Convert.ToInt32(_count);
                }
            }
            catch (Exception ex)
            {

            }
            return count;
        }

        public string GetGroupSubCode(int count)
        {
            string cde = string.Empty;
            List<Groupcode> st = new List<Groupcode>();
            st.Add(new Groupcode { index = 0, gpno = "a" });
            st.Add(new Groupcode { index = 1, gpno = "b" });
            st.Add(new Groupcode { index = 2, gpno = "c" });
            st.Add(new Groupcode { index = 3, gpno = "d" });
            st.Add(new Groupcode { index = 4, gpno = "e" });
            st.Add(new Groupcode { index = 5, gpno = "f" });
            st.Add(new Groupcode { index = 6, gpno = "g" });
            st.Add(new Groupcode { index = 7, gpno = "h" });
            st.Add(new Groupcode { index = 8, gpno = "i" });
            st.Add(new Groupcode { index = 9, gpno = "j" });
            st.Add(new Groupcode { index = 10, gpno = "k" });
            st.Add(new Groupcode { index = 11, gpno = "l" });
            cde = st.Where(x => x.index == count).Select(x => x.gpno).FirstOrDefault();
            return cde;
        }

        public List<SelectListItem> GetQuiztype()
        {
            var _Bindclient = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();

                List<SelectListItem> client = GetEntityList<SelectListItem>("GetQuiztype", searchList);//Constants.GetUserGroupList
                if (client != null)
                {
                    _Bindclient = client;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _Bindclient;
        }

        public List<SelectListItem> Bindgroup(Guid TemplateId)
        {
            var _Bindclient = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "TemplateId " ,Value = Convert.ToString(TemplateId )}
                };


                List<SelectListItem> client = GetEntityList<SelectListItem>("Bindtemplategroup", searchList);//Constants.GetUserGroupList
                if (client != null)
                {
                    _Bindclient = client;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _Bindclient;
        }


        public string GetSuboutcomeCode(Guid Id)
        {
            string code = string.Empty;

            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "Code" ,Value = Convert.ToString(Id)}
                };
                var _count = GetScalar("GetSuboutcomeCodemaster", searchList);
                if (_count != null)
                {
                    code = _count.ToString();
                }

            }
            catch (Exception ex)
            {
                code = ex.Message;
            }
            return code;
        }

        public ServiceResponse SavetemplateSuboutcomemaster(TemplateSuboutcomeMaster _model)
        {
            var response = new ServiceResponse();
            try
            {
                if (_model.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateId", _model.TemplateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateOutcomemasterId",_model.TemplateOutcomemasterId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionTypeId", _model.TCFQuestionTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@GuidanceTitle", _model.GuidanceTitle).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@GuidanceDescription", _model.GuidanceDescription).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Createdby", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedAt", _model.CreatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;

                    cmd.Parameters.AddWithValue("@Isedit", _model.IsEdit).SqlDbType = SqlDbType.Bit;



                    DataSet ds = null;
                    ds = BulkInsert("SavetemplateSuboutcomemaster", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";

                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Id",_model.Id).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateId", _model.TemplateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TemplateOutcomemasterId", _model.TemplateOutcomemasterId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionTypeId", _model.TCFQuestionTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@GuidanceTitle", _model.GuidanceTitle).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@GuidanceDescription", _model.GuidanceDescription).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Createdby", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@CreatedAt", _model.CreatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;

                    cmd.Parameters.AddWithValue("@Isedit", _model.IsEdit).SqlDbType = SqlDbType.Bit;



                    DataSet ds = null;
                    ds = BulkInsert("SavetemplateSuboutcomemaster", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Update Successfully.";

                }
            }
            catch (Exception ex)
            {

            }


            return response;
        }

        public TemplateViewModel GetTemplatesuboutcomelist()
        {
            var TemplateOutcomemaster = new TemplateViewModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<TemplateSuboutcomeMaster> _TemplateSuboutcomeMaster = GetEntityList<TemplateSuboutcomeMaster>("_GetTemplatesuboutcomelist", searchList);//Constants.GetUserGroupList
                if (_TemplateSuboutcomeMaster.Count > 0)
                {
                    TemplateOutcomemaster._TemplateSuboutcomeMaster = _TemplateSuboutcomeMaster;
                    TemplateOutcomemaster.Response.IsSuccess = true;
                }
                else
                {
                    TemplateOutcomemaster.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return TemplateOutcomemaster;
        }

        public TemplateSuboutcomeMaster GetTemplateSuboutcomeMasterById(Guid Id)
        {
            var _TemplateSuboutcomeMaster = new TemplateSuboutcomeMaster();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "Id" ,Value = Convert.ToString(Id)}
                };
                _TemplateSuboutcomeMaster = GetEntity<TemplateSuboutcomeMaster>("GetTemplatesubOutcomeById", searchList);//Constants.GetUserGroupList
                if (_TemplateSuboutcomeMaster != null)
                {
                    return _TemplateSuboutcomeMaster;


                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _TemplateSuboutcomeMaster;
        }

        public List<SelectListItem> Bindclient()
        {
            var _Bindclient = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();

                List<SelectListItem> client = GetEntityList<SelectListItem>("Bindclient", searchList);//Constants.GetUserGroupList
                if (client != null)
                {
                    _Bindclient = client;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _Bindclient;
        }
        public List<SelectListItem> BindDivionByCompany(Guid CompanyID)
        {
            var _division = new List<SelectListItem>();
            try
            {

                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "CompanyID" ,Value = Convert.ToString(CompanyID)}

                };

                List<SelectListItem> division = GetEntityList<SelectListItem>("BindDivionByCompany", searchList);//Constants.GetUserGroupList
                if (division != null)
                {
                    _division = division;
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return _division;
        }

        public ServiceResponse AttachStandard(StandardConduct _model)
        {
            var response = new ServiceResponse();
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.AddWithValue("@Id", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@TemplateId", _model.TemplateId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CompanyId", _model.CompanyId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@DivisionId", _model.DivisonId).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@CreatedDate", _model.CreatedDate).SqlDbType = SqlDbType.DateTime;
               



                DataSet ds = null;
                ds = BulkInsert("_StandardAttachtocompany", cmd);

                response.IsSuccess = true;

                response.Message = "Record Saved Successfully.";

            }
            catch (Exception ex)
            {

            }


            return response;
        }

    }
}