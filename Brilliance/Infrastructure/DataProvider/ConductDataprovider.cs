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
                    cmd.Parameters.AddWithValue("@GroupId", Guid.NewGuid()).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@HoldingEntityId", _model.HoldingEntityId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Code", _model.Code).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Title", _model.Title).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@IsVisible", _model.IsVisible).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@SortingWeight", _model.SortingWeight).SqlDbType = SqlDbType.Int;
                    cmd.Parameters.AddWithValue("@IsDeleted", _model.IsDeleted).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@Createddate", _model.Createddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Modifieddate", _model.Modifieddate).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsEdit", _model.IsEdit).SqlDbType = SqlDbType.Bit;
                    cmd.Parameters.AddWithValue("@AdditionalDescription", _model.AdditionalDescription).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Filename", _model.Filename).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DivisionId", _model.DivisionId).SqlDbType = SqlDbType.UniqueIdentifier;



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
                    cmd.Parameters.AddWithValue("@AdditionalDescription", _model.AdditionalDescription).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Filename", _model.Filename).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DivisionId", _model.DivisionId).SqlDbType = SqlDbType.UniqueIdentifier;


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
        public ConductDataViewModel TCFQuestionGroupList(Guid userid)
        {
            var TCFQuestionGroup = new ConductDataViewModel();
            try
            {
                var client = SessionHelper.ClientID;
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "EntityId " ,Value = Convert.ToString(client)}
                };

                List<TCFQuestionGroupViewmodel> _TCFQuestionGroup = GetEntityList<TCFQuestionGroupViewmodel>("prc_GetQuestion", searchList);//Constants.GetUserGroupList
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
                var _count = GetScalar("_Groupcount");
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
                    cmd.Parameters.AddWithValue("@RateId", _model.RateId).SqlDbType = SqlDbType.UniqueIdentifier;

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
                    cmd.Parameters.AddWithValue("@RateId", _model.RateId).SqlDbType = SqlDbType.UniqueIdentifier;
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
                var client = SessionHelper.ClientID;
                Guid userid = new Guid();
                userid = SessionHelper.UserId;
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "EntityId " ,Value = Convert.ToString(client)},
                        new SearchValueData { Name = "UserId " ,Value = Convert.ToString(userid)}
                };

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
                    var _msg = Convert.ToInt32(objnew.GetScalar("_PrcDeleteoutcomeMaster", searchList));
                    if (_msg == 0)
                    {
                        response.Message = "Can't delete item in use";
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
            catch (Exception ex)
            {

            }
            return response;
        }


        public int MasterCont( Guid EntityId)
        {
            int count = 0;

            try
            {
                var searchList = new List<SearchValueData>()
                {
                        
                         new SearchValueData { Name = "EntityId" ,Value = Convert.ToString(EntityId)}
                };
                var _count = GetScalar("_PrcMasterCont", searchList);
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

        public string Prcoutcometitle(Guid EntityId)
        {
            string title = string.Empty;

            try
            {
                var searchList = new List<SearchValueData>()
                {

                         new SearchValueData { Name = "EntityId" ,Value = Convert.ToString(EntityId)}
                };
                var _title = GetScalar("_Prcoutcometitle", searchList);
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

        public string GetSuboutcomeCode(Guid Id)
        {
            string code = string.Empty;

            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "Code" ,Value = Convert.ToString(Id)}
                };
                var _count = GetScalar("GetSuboutcomeCode", searchList);
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


        public int GrupcntByEntity(int Id, Guid entityId)
        {
            int count = 0;
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "gId" ,Value = Convert.ToString(Id )},
                         new SearchValueData { Name = "EntityId" ,Value = Convert.ToString(entityId)}
                };

                var _count = GetScalar("GrupcntByEntity", searchList);
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

        public List<SelectListItem> BindUsers(Guid Id)
        {
            var _Bindclient = new List<SelectListItem>();
            try
            {
                var searchList = new List<SearchValueData>();


                List<SelectListItem> client = GetEntityList<SelectListItem>("Proc_BindUsers", searchList);//Constants.GetUserGroupList
                if (client != null)
                {
                    foreach(var item in client)
                    {
                        if(item.Value==Id.ToString().ToUpper())
                        {
                            _Bindclient.Add(new SelectListItem { Text = item.Text, Value = item.Value, Selected = true });
                        }
                        else
                        {
                            _Bindclient.Add(new SelectListItem { Text = item.Text, Value = item.Value});
                        }
                    }
                    /////_Bindclient = client;
                }
               
            }
            catch (Exception ex)
            {
            }
            return _Bindclient;
        }

        public ServiceResponse SaveTCFForm(TCFForm _model)
        {
            var response = new ServiceResponse();
            try
            {


                if (_model.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();
                    Guid Id = Guid.NewGuid();
                    cmd.Parameters.AddWithValue("@Id", Id).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClusterId", _model.ClusterId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFPeriodId", _model.TCFPeriodId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@BinderHolderTypeId", _model.BinderHolderTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreatedAt", _model.CreatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedAt", _model.UpdatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", _model.UpdatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsDeleted", false).SqlDbType = SqlDbType.Bit;

                    cmd.Parameters.AddWithValue("@IsEdit",false).SqlDbType = SqlDbType.Bit;
                    DataSet ds = null;
                    ds = BulkInsert("Save_TCFForm", cmd);



                    try
                    {
                        Guid UID=new Guid();
                        string RecordId = string.Empty;
                        string msg = string.Empty;
                        int count = 0;
                        if(ds.Tables[0].Rows.Count>0)
                        {
                            if(ds.Tables[0].Rows[0].ToString()!=null)
                            {
                                count = Convert.ToInt32(ds.Tables[0].Rows[0]["CountRows"]);
                                RecordId =ds.Tables[0].Rows[0]["RecordId"].ToString();
                            }
                        }
                        if(count>0)
                        {
                            UID = new Guid(RecordId);
                            Guid _UID = new Guid();
                            _UID = Guid.NewGuid();
                            SqlCommand _cmd = new SqlCommand();
                            _cmd.Parameters.AddWithValue("@Id", _UID).SqlDbType = SqlDbType.UniqueIdentifier;
                            _cmd.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFOutCome.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                            _cmd.Parameters.AddWithValue("@AddedBy", _model.TCFOutCome.AddedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                            _cmd.Parameters.AddWithValue("@AddedDate", _model.TCFOutCome.AddedDate).SqlDbType = SqlDbType.DateTime;
                            _cmd.Parameters.AddWithValue("@TCFoutcomeId", UID).SqlDbType = SqlDbType.UniqueIdentifier;
                            _cmd.Parameters.AddWithValue("@IsEdit", false).SqlDbType = SqlDbType.Bit;
                            DataSet _ds = null;
                            _ds = BulkInsert("Save_TCFOutcome", _cmd);
                            if (_ds.Tables[0].Rows.Count > 0)
                            {
                                msg = _ds.Tables[0].Rows[0]["Msg"].ToString();
                                if (msg == "1")
                                {
                                    SqlCommand _cmdsuboutcome = new SqlCommand();
                                    _cmdsuboutcome.Parameters.AddWithValue("@TCFFormId", UID).SqlDbType = SqlDbType.UniqueIdentifier;

                                    _cmdsuboutcome.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFOutCome.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@AddedBy", _model.TCFOutCome.AddedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@OutcomeId", _UID).SqlDbType = SqlDbType.UniqueIdentifier;

                                    DataSet _dssuboutcome = null;
                                    _dssuboutcome = BulkInsert("_SaveTCFSuboutcome", _cmdsuboutcome);
                                }
                              else  if (msg == "0")
                                {
                                    SqlCommand _cmdsuboutcome = new SqlCommand();
                                    _cmdsuboutcome.Parameters.AddWithValue("@TCFFormId", UID).SqlDbType = SqlDbType.UniqueIdentifier;
                                   _cmdsuboutcome.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFOutCome.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@AddedBy", _model.TCFOutCome.AddedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@OutcomeId", _UID).SqlDbType = SqlDbType.UniqueIdentifier;

                                    DataSet _dssuboutcome = null;
                                    _dssuboutcome = BulkInsert("_SaveTCFSuboutcome", _cmdsuboutcome);
                                }
                            }
                            response.IsSuccess = true;

                            response.Message = "Record Saved Successfully.";
                        }
                        else if(count==0)
                        {
                            Guid _UID = new Guid();
                            _UID = Guid.NewGuid();
                            SqlCommand _cmd = new SqlCommand();
                            _cmd.Parameters.AddWithValue("@Id", _UID).SqlDbType = SqlDbType.UniqueIdentifier;
                            _cmd.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFOutCome.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                            _cmd.Parameters.AddWithValue("@AddedBy", _model.TCFOutCome.AddedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                            _cmd.Parameters.AddWithValue("@AddedDate", _model.TCFOutCome.AddedDate).SqlDbType = SqlDbType.DateTime;
                            _cmd.Parameters.AddWithValue("@TCFoutcomeId", Id).SqlDbType = SqlDbType.UniqueIdentifier;
                            _cmd.Parameters.AddWithValue("@IsEdit", false).SqlDbType = SqlDbType.Bit;
                            DataSet _ds = null;
                            _ds = BulkInsert("Save_TCFOutcome", _cmd);
                            if(_ds.Tables[0].Rows.Count>0)
                            {
                                msg = _ds.Tables[0].Rows[0]["Msg"].ToString();
                                if(msg=="1")
                                {
                                    SqlCommand _cmdsuboutcome = new SqlCommand();
                                    _cmdsuboutcome.Parameters.AddWithValue("@TCFFormId", Id).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFOutCome.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@AddedBy", _model.TCFOutCome.AddedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@OutcomeId", _UID).SqlDbType = SqlDbType.UniqueIdentifier;

                                    DataSet _dssuboutcome = null;
                                    _dssuboutcome = BulkInsert("_SaveTCFSuboutcome", _cmdsuboutcome);
                                }
                                else if (msg == "0")
                                {
                                    SqlCommand _cmdsuboutcome = new SqlCommand();
                                    _cmdsuboutcome.Parameters.AddWithValue("@TCFFormId", Id).SqlDbType = SqlDbType.UniqueIdentifier;

                                  _cmdsuboutcome.Parameters.AddWithValue("@TCFQuestionGroupId", _model.TCFOutCome.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@AddedBy", _model.TCFOutCome.AddedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                                    _cmdsuboutcome.Parameters.AddWithValue("@OutcomeId", _UID).SqlDbType = SqlDbType.UniqueIdentifier;

                                    DataSet _dssuboutcome = null;
                                    _dssuboutcome = BulkInsert("_SaveTCFSuboutcome", _cmdsuboutcome);
                                }
                            }
                            response.IsSuccess = true;

                            response.Message = "Record Saved Successfully.";
                        }
                       

                    }
                    catch(Exception ex)
                    {
                        response.IsSuccess = false;

                        response.Message = "Server Issue..";
                    }

                   
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Id", _model.Id).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ClusterId", _model.ClusterId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFPeriodId", _model.TCFPeriodId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@BinderHolderTypeId", _model.BinderHolderTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Description", _model.Description).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreatedAt", _model.CreatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@UpdatedAt", _model.UpdatedAt).SqlDbType = SqlDbType.DateTime;
                    cmd.Parameters.AddWithValue("@UpdatedBy", _model.UpdatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@IsDeleted", false).SqlDbType = SqlDbType.Bit;

                    cmd.Parameters.AddWithValue("@IsEdit", false).SqlDbType = SqlDbType.Bit;

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


        public TCFQuestion _GetSuboutcomeById(Guid Id)
        {
            TCFQuestion vm = new TCFQuestion();
            Guid userid = new Guid();
            userid = SessionHelper.UserId;
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "Id" ,Value = Convert.ToString(Id)},

                        new SearchValueData { Name = "UserId" ,Value = Convert.ToString(userid)}
                };

                vm = GetEntity<TCFQuestion>("_GetSuboutcomeById", searchList);
                response.Data = vm;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return vm;

        }

        public List<TCFFormEvidence> _TCFFormEvidence(Guid TCFQuestionId)
        {
            List<TCFFormEvidence> vm = new List<TCFFormEvidence>();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "TCFQuestionId" ,Value = Convert.ToString(TCFQuestionId)}
                };

                vm = GetEntityList<TCFFormEvidence>("_GetTCFEvidence", searchList);
                response.Data = vm;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return vm;

        }
        public List<TCFNotes> _GetNotes(Guid TCFQuestionId)
        {
            List<TCFNotes> vm = new List<TCFNotes>();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                         new SearchValueData { Name = "TCFQuestionId " ,Value = Convert.ToString(TCFQuestionId)}
                };

                vm = GetEntityList<TCFNotes>("_GetTCFNotes", searchList);
                response.Data = vm;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return vm;

        }
        public List<TCFTask> _GetTask(Guid TCFQuestionId)
        {
            List<TCFTask> vm = new List<TCFTask>();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "TCFQuestionId" ,Value = Convert.ToString(TCFQuestionId)}
                };

                vm = GetEntityList<TCFTask>("_GetTCFTask", searchList);
                response.Data = vm;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return vm;

        }


        public int RemoveTcfImages(TCFFormEvidence ev)
        {
            int count = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = null;
                cmd.Parameters.AddWithValue("@AddedById", ev.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@TCFQuestionId", ev.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                ds = BulkInsert("RemoveTcfImages", cmd);
            }
            catch (Exception ex)
            {

            }
            return count;
        }

        public int RemoveTcfTask(TCFTask ev)
        {
            int count = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = null;
                cmd.Parameters.AddWithValue("@AddedBy", ev.AddedById).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@TCFQuestionId", ev.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                ds = BulkInsert("_procDeleteTaks", cmd);
            }
            catch (Exception ex)
            {

            }
            return count;
        }

        public int RemoveTcfNotes(TCFNotes ev)
        {
            int count = 0;
            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet ds = null;
                cmd.Parameters.AddWithValue("@AddedBy", ev.AddedById).SqlDbType = SqlDbType.UniqueIdentifier;
                cmd.Parameters.AddWithValue("@TCFQuestionId", ev.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                ds = BulkInsert("_procDeleteNote", cmd);
            }
            catch (Exception ex)
            {

            }
            return count;
        }

        public ServiceResponse SaveTcfImage(TCFFormEvidence _model)
        {
            var response = new ServiceResponse();
            try
            {


                if (_model.IsEdit == false)
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Parameters.AddWithValue("@ClusterId", _model.ClusterId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFPeriodId", _model.TCFPeriodId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionId", _model.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@FileName", _model.FileName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@FileExtension", _model.FileExtension).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@IsEdit", _model.IsEdit).SqlDbType = SqlDbType.Bit;



                    DataSet ds = null;
                    ds = BulkInsert("ProcessTcfImages", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                }
                else
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.Parameters.AddWithValue("@ClusterId", _model.ClusterId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFPeriodId", _model.TCFPeriodId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionId", _model.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@FileName", _model.FileName).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@FileExtension", _model.FileExtension).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@CreatedBy", _model.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;

                    cmd.Parameters.AddWithValue("@IsEdit", _model.IsEdit).SqlDbType = SqlDbType.Bit;



                    DataSet ds = null;
                    ds = BulkInsert("ProcessTcfImages", cmd);

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

        public ServiceResponse UpdateUserSuboutcome(TCFSubOutCome _model)
        {
            var response = new ServiceResponse();
            try
            {

                    SqlCommand cmd = new SqlCommand();
                    cmd.Parameters.AddWithValue("@Id", _model.Id).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFQuestionId", _model.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@RateId", _model.RateId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@TCFSubOutComeUserId", _model.TCFSubOutComeUserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@ReasonNotYet", _model.ReasonNotYet).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Reasonmostly", _model.Reasonmostly).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ReasonNotApplicable", _model.ReasonNotApplicable).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ReasonPartially", _model.ReasonPartially).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ReasonFully", _model.ReasonFully).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@ModifiedBy", _model.ModifiedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@DueDate", _model.DueDate).SqlDbType = SqlDbType.DateTime;



                DataSet ds = null;
                    ds = BulkInsert("Proc_UpdateSuboutcome", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Update Successfully.";
                
                

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }


        public ServiceResponse SaveTCFTask(TCFTask _model)
        {
            var response = new ServiceResponse();
            try
            {


                SqlCommand cmd = new SqlCommand();

                    cmd.Parameters.AddWithValue("@TCFQuestionId", _model.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@AddedById", _model.AddedById).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@AssignUserId", _model.AssignUserId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Task", _model.Task).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Status", _model.Status).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@DueDate", _model.DueDate).SqlDbType = SqlDbType.NVarChar;



                DataSet ds = null;
                    ds = BulkInsert("_ProcessTcfTask", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                
                

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }

        public ServiceResponse SaveTCFNotes(TCFNotes _model)
        {
            var response = new ServiceResponse();
            try
            {

                    SqlCommand cmd = new SqlCommand();

                    cmd.Parameters.AddWithValue("@TCFQuestionId", _model.TCFQuestionId).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@AddedById", _model.AddedById).SqlDbType = SqlDbType.UniqueIdentifier;
                    cmd.Parameters.AddWithValue("@Author", _model.Author).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@Note", _model.Note).SqlDbType = SqlDbType.NVarChar;
                    cmd.Parameters.AddWithValue("@NoteId", _model.NoteId).SqlDbType = SqlDbType.NVarChar;

                     DataSet ds = null;
                     ds = BulkInsert("_ProcessTcfNote", cmd);

                    response.IsSuccess = true;

                    response.Message = "Record Saved Successfully.";
                
                

            }
            catch (Exception ex)
            {
                response.Message = "Internal server error";
            }

            return response;
        }

        public string _GetEntityByUser(Guid Id)
        {
            string code = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet _ds = null;
                cmd.Parameters.AddWithValue("@UserId", Id).SqlDbType = SqlDbType.UniqueIdentifier;
                _ds = BulkInsert("_GetEntityByUser", cmd);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    code = _ds.Tables[0].Rows[0]["code"].ToString();
                }

            }
            catch (Exception ex)
            {
             
            }
            return code;
        }

        public string _GetUserName(Guid Id)
        {
            string TCFNoteuser = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet _ds = null;
                cmd.Parameters.AddWithValue("@UserId", Id).SqlDbType = SqlDbType.UniqueIdentifier;
                _ds = BulkInsert("_GetUserName", cmd);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    TCFNoteuser = _ds.Tables[0].Rows[0]["TCFNoteuser"].ToString();
                }

            }
            catch (Exception ex)
            {

            }
            return TCFNoteuser;
        }




        public ConductDataViewModel TCFQuestionEntityUserWise(Guid EntityId,Guid UserId, int IsAdmin, Guid Division)
        {
            var TCFQuestionGroup = new ConductDataViewModel();
            try
            {
                var client = SessionHelper.ClientID;
                var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "EntityId" ,Value = Convert.ToString(EntityId)},
                        new SearchValueData { Name = "UserId" ,Value = Convert.ToString(UserId)},
                         new SearchValueData { Name = "IsAdmin" ,Value = Convert.ToString(IsAdmin)},
                          new SearchValueData { Name = "DivisionId" ,Value = Convert.ToString(Division)}
                };

                List<TCFQuestionGroupViewmodel> _TCFQuestionGroup = GetEntityList<TCFQuestionGroupViewmodel>("TCFQuestionEntityUserWise", searchList);//Constants.GetUserGroupList
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

        public ServiceResponse _UpdateTcfFormEntryByUser(TCFQuestion quiz)
        {
            var response = new ServiceResponse();
            try
            {
                
                SqlCommand _cmd = new SqlCommand();
                DataSet _ds = null;
                _cmd.Parameters.AddWithValue("@CreatedBy", quiz.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@TCFQuestionGroupId", quiz.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@Id", quiz.Id).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@RateId", quiz.RateId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@TCFQuestionTypeId", quiz.TCFQuestionTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@BinderHolderTypeId", quiz.BinderHolderTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@Code", quiz.Code).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@Description", quiz.Description).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@GuidanceTitle", quiz.GuidanceTitle).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@GuidanceDescription", quiz.GuidanceDescription).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@IsStandard", true).SqlDbType = SqlDbType.Bit;
                _cmd.Parameters.AddWithValue("@ReasonNotYet", quiz.ReasonNotYet).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@Reasonmostly", quiz.Reasonmostly).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@ReasonNotApplicable", quiz.ReasonNotApplicable).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@ReasonPartially", quiz.ReasonPartially).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@ReasonFully", quiz.ReasonFully).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@TCFSubOutComeUserId", quiz.TCFSubOutComeUserId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@UpdatedBy", SessionHelper.UserId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@DueDate", quiz.DueDate).SqlDbType = SqlDbType.DateTime;
                _ds = BulkInsert("_TcfFormEntryByUser", _cmd);

                string message = string.Empty;
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    message = _ds.Tables[0].Rows[0]["Msg"].ToString();
                    if (message.ToString() == "1")
                    {
                        response.Message = "Saved Sucessfully..";
                    }
                    else
                    {
                        response.Message = "Update Sucessfully..";
                    }
                }
            }
            catch(Exception ex)
            {


            }
            return response;
        }

        public ServiceResponse _NewTcfEntry(TCFQuestion quiz)
        {
            var response = new ServiceResponse();
            try
            {

                SqlCommand _cmd = new SqlCommand();
                DataSet _ds = null;
                _cmd.Parameters.AddWithValue("@Id", quiz.Id).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@RateId", quiz.RateId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@TCFQuestionGroupId", quiz.TCFQuestionGroupId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@BinderHolderTypeId", quiz.BinderHolderTypeId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@Code", quiz.Code).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@Description", quiz.Description).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@GuidanceDescription", quiz.GuidanceDescription).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@IsStandard", true).SqlDbType = SqlDbType.Bit;
                _cmd.Parameters.AddWithValue("@CreatedAt", quiz.CreatedAt).SqlDbType = SqlDbType.DateTime;
                _cmd.Parameters.AddWithValue("@CreatedBy", quiz.CreatedBy).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@Isdelete", quiz.IsDeleted).SqlDbType = SqlDbType.Bit;

                _cmd.Parameters.AddWithValue("@ReasonNotYet", quiz.ReasonNotYet).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@Reasonmostly", quiz.Reasonmostly).SqlDbType = SqlDbType.NVarChar;

                _cmd.Parameters.AddWithValue("@ReasonNotApplicable", quiz.ReasonNotApplicable).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@ReasonPartially", quiz.ReasonPartially).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@ReasonFully", quiz.ReasonFully).SqlDbType = SqlDbType.NVarChar;
                _cmd.Parameters.AddWithValue("@TCFSubOutComeUserId", quiz.TCFSubOutComeUserId).SqlDbType = SqlDbType.UniqueIdentifier;
                _cmd.Parameters.AddWithValue("@DueDate", quiz.DueDate).SqlDbType = SqlDbType.DateTime;
                _ds = BulkInsert("_NewTcfEntry", _cmd);

                string message = string.Empty;
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    message = _ds.Tables[0].Rows[0]["Msg"].ToString();
                    if (message.ToString() == "1")
                    {
                        response.Message = "Saved Sucessfully..";
                    }
                    else
                    {
                        response.Message = "Update Sucessfully..";
                    }
                }
            }
            catch (Exception ex)
            {


            }
            return response;
        }

        public List<TCFQuestionViewmodel> _TCFQuestionForUser(Guid CreatedBy)
        {
            List<TCFQuestionViewmodel> vm = new List<TCFQuestionViewmodel>();

            var searchList = new List<SearchValueData>()
                {
                        new SearchValueData { Name = "CreatedBy " ,Value = Convert.ToString(CreatedBy)}
                       
                };
            vm = GetEntityList<TCFQuestionViewmodel>("_TCFQuestionForUser", searchList);//Constants.GetUserGroupList
            return vm;
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

        public string _GetUserDivision(Guid Id)
        {
            string TCFNoteuser = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand();
                DataSet _ds = null;
                cmd.Parameters.AddWithValue("@UserId", Id).SqlDbType = SqlDbType.UniqueIdentifier;
                _ds = BulkInsert("_GetUserDivision", cmd);
                if (_ds.Tables[0].Rows.Count > 0)
                {
                    TCFNoteuser = _ds.Tables[0].Rows[0]["Position"].ToString();
                }
                else
                {
                    TCFNoteuser = null;
                }

            }
            catch (Exception ex)
            {

            }
            return TCFNoteuser;
        }


    }
}