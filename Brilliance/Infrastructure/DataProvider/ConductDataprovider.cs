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
    public class ConductDataprovider : BaseDataProvider, IConductIS
    {
        public ServiceResponse SaveOutcome(TCFQuestionGroup _model)
        {
            var response = new ServiceResponse();
            try
            {


                if (_model.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@GroupId",Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@HoldingEntityId", _model.HoldingEntityId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Title", _model.Title).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsVisible", _model.IsVisible).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@SortingWeight", _model.SortingWeight).SqlDbType = SqlDbType.Int;
                    cmd.Parameters.AddWithValue("@IsDeleted",_model.IsDeleted).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@Createddate", _model.Createddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Modifieddate",_model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", _model.IsEdit).SqlDbType = SqlDbType.Bit;
                    


                    DataSet ds = null;
                    ds = BulkInsert("Save_Outcome", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@GroupId", _model.GroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@HoldingEntityId", _model.HoldingEntityId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Title", _model.Title).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsVisible", _model.IsVisible).SqlDbType = SqlDbType.Int;
                    cmd.Parameters.AddWithValue("@SortingWeight", _model.SortingWeight).SqlDbType = SqlDbType.Int;
                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@Createddate", null).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", null).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", _model.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_Outcome", cmd);
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
        public ConductDataViewModel TCFQuestionGroupList()
        {
            var TCFQuestionGroup = new ConductDataViewModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<TCFQuestionGroupViewmodel> _TCFQuestionGroup = GetEntityList<TCFQuestionGroupViewmodel>("_GetTCFQuestion", searchList);//Constants.GetUserGroupList
                if (_TCFQuestionGroup.Count > 0)
                {
                    TCFQuestionGroup.TCFQuestionGrouplist = _TCFQuestionGroup;
                    TCFQuestionGroup.Response.IsSuccess = true;
                }
                else
                {
                    TCFQuestionGroup.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return TCFQuestionGroup;
        }

        

        public List<SelectListItem> BindOutcome()
        {
            var TCFQuestionGroup = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();

                List<SelectListItem> _TCFQuestionGroup = GetEntityList<SelectListItem>("_PROCGETQUIZGROUP", searchList);//Constants.GetUserGroupList
                if (_TCFQuestionGroup!=null)
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
        public List<SelectListItem> BindOutcomeRating()
        {
            var TCFQuestionGroup = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();

                List<SelectListItem> _TCFQuestionGroup = GetEntityList<SelectListItem>("_PROCRating", searchList);//Constants.GetUserGroupList
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

        public int GroupCount()
        {
            int count = 0;

            try
            {
              var  _count = GetScalar("_Groupcount");
                if(_count!=null)
                {
                    count =Convert.ToInt32(_count);
                }

            }
            catch(Exception ex)
            {
                count = 0;
            }
           return count;
        }

        public List<SelectListItem> GetGroupcode()
        {
            var _Bindclient = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();

                List<SelectListItem> client = GetEntityList<SelectListItem>("GetGroupcode", searchList);//Constants.GetUserGroupList
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

        public ConductDataViewModel TCFOutcomeList()
        {
            var TCFQuestionGroup = new ConductDataViewModel();
            try
            {
                var searchList = new List<SearchValueData>();

                List<TCFQuestionGroupViewmodel> _TCFQuestionGroup = GetEntityList<TCFQuestionGroupViewmodel>("_Getoutcomelist", searchList);//Constants.GetUserGroupList
                if (_TCFQuestionGroup.Count > 0)
                {
                    TCFQuestionGroup.TCFQuestionGrouplist = _TCFQuestionGroup;
                    TCFQuestionGroup.Response.IsSuccess = true;
                }
                else
                {
                    TCFQuestionGroup.Response.IsSuccess = false;
                }
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return TCFQuestionGroup;
        }

        public TCFQuestionGroup GetoutcomeById(Guid GroupId)
        {
            TCFQuestionGroup vm = new TCFQuestionGroup();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "GroupId " ,Value = Convert.ToString(GroupId )}
                };

                vm = GetEntity<TCFQuestionGroup>("_PRCGetoutcomeById", searchList);
                response.Data = vm;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return vm;
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

        public List<SelectListItem> Bindgroup(Guid EntityId)
        {
            var _Bindclient = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "EntityId " ,Value = Convert.ToString(EntityId )}
                };


                List<SelectListItem> client = GetEntityList<SelectListItem>("Bindgrp", searchList);//Constants.GetUserGroupList
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

        public ServiceResponse SaveSuboutcomes(TCFQuestion _model)
        {
            var response = new ServiceResponse();
            try
            {


                if (_model.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Id", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionTypeId", _model.TCFQuestionTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@BinderHolderTypeId", _model.BinderHolderTypeId).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@GuidanceDescription", _model.GuidanceDescription).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsStandard", _model.IsStandard).SqlDbType = SqlDbType.Bit;

                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@CreatedAt", _model.CreatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", _model.IsEdit).SqlDbType = SqlDbType.Bit;



                    DataSet ds = null;
                    ds = BulkInsert("Save_TCFQuestion", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Id", _model.Id).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionTypeId", _model.TCFQuestionTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@BinderHolderTypeId", _model.BinderHolderTypeId).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@GuidanceDescription", _model.GuidanceDescription).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsStandard", _model.IsStandard).SqlDbType = SqlDbType.Bit;

                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@CreatedAt", _model.CreatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", _model.IsEdit).SqlDbType = SqlDbType.Bit;

                    DataSet ds = null;
                    ds = BulkInsert("Save_TCFQuestion", cmd);
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


        public List<TCFQuestionViewmodel> TCFGetQuestionList()
        {
            List<TCFQuestionViewmodel> TCFQuestion = new List<TCFQuestionViewmodel>();
            try
            {
                var searchList = new List<SearchValueData>();

                TCFQuestion = GetEntityList<TCFQuestionViewmodel>("prc_GetQuestion", searchList);//Constants.GetUserGroupList
                
            }
            catch (Exception ex)
            {
                //userGroupListModel.Response.Message = Resource.Resource.ServerError;
            }
            return TCFQuestion;
        }


        public TCFQuestion GetTCFQuestionById(Guid Id)
        {
            TCFQuestion vm = new TCFQuestion();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "Id " ,Value = Convert.ToString(Id )}
                };

                vm = GetEntity<TCFQuestion>("GetTCFQuestionById", searchList);
                response.Data = vm;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return vm;
        }

        public ServiceResponse DeleteoutcomeMaster(Guid Id)
        {
            var response = new ServiceResponse();
            
            try
            {
                
                var searchList = new List<SearchValueData>();
                if (Id != Guid.Empty)
                {
                    var searchValueData = new SearchValueData { Name = "Id", Value = Convert.ToString(Id) };
                    searchList.Add(searchValueData);
                    BaseDataProvider objnew = new BaseDataProvider();
                    objnew = new BaseDataProvider();
                    var _msg =Convert.ToInt32(objnew.GetScalar("_PrcDeleteoutcomeMaster", searchList));
                    if(_msg==0)
                    {
                        response.Message ="Can't delete item in use";
                        response.IsSuccess = false;
                    }
                    else if (_msg > 0)
                    {
                        response.Message = "Delete successfully";
                        response.IsSuccess = true;
                    }
                      
                    
                }
                else
                {
                    response.IsSuccess = false;
                }
                return response;
            }
            catch(Exception ex)
            {

            }
            return response;
        }
    }
}