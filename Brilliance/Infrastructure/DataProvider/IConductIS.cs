using Brilliance.Models;
using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IConductIS
    {
        ConductDataViewModel TCFQuestionGroupList(Guid userid);

        List<SelectListItem> BindOutcome();
        List<SelectListItem> BindOutcomeRating();
        List<SelectListItem> Bindclient();
        List<SelectListItem> GetGroupcode();
        ServiceResponse SaveOutcome(TCFQuestionGroup outcome);
        int GroupCount();
        int MasterCont(Guid EntityId);
        string Prcoutcometitle(Guid EntityId);
        string GetSuboutcomeCode(Guid Id);
        string GetGroupSubCode(int count);
        int GrupcntByEntity(int Id, Guid entityId);
        ConductDataViewModel TCFOutcomeList();
        TCFQuestionGroup GetoutcomeById(Guid GroupId);
        List<SelectListItem> GetQuiztype();
        List<SelectListItem> Bindgroup(Guid EntityId);

        ServiceResponse SaveSuboutcomes(TCFQuestion outcome);
        List<TCFQuestionViewmodel> TCFGetQuestionList();

        TCFQuestion GetTCFQuestionById(Guid Id);
        ServiceResponse DeleteoutcomeMaster(Guid Id);
        List<SelectListItem> BindUsers();
        ServiceResponse SaveTCFForm(TCFForm form);


        TCFSubOutCome _GetSuboutcomeById(Guid Id);
        int RemoveTcfImages(TCFFormEvidence ev);
        ServiceResponse SaveTcfImage(TCFFormEvidence _TCFFormEvidence);
        ServiceResponse UpdateUserSuboutcome(TCFSubOutCome _model);
        List<TCFFormEvidence> _TCFFormEvidence(Guid Id,Guid TCFQuestionId);
        List<TCFNotes> _GetNotes(Guid Id, Guid TCFQuestionId);
        List<TCFTask> _GetTask(Guid Id, Guid TCFQuestionId);
        int RemoveTcfTask(TCFTask ev);
        int RemoveTcfNotes(TCFNotes ev);
        ServiceResponse SaveTCFTask(TCFTask _model);
        ServiceResponse SaveTCFNotes(TCFNotes _model);
    }
}
