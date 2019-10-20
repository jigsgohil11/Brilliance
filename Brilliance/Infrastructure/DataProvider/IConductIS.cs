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
        ConductDataViewModel TCFQuestionGroupList();

        List<SelectListItem> BindOutcome();
        List<SelectListItem> BindOutcomeRating();
        List<SelectListItem> Bindclient();
        List<SelectListItem> GetGroupcode();
        ServiceResponse SaveOutcome(TCFQuestionGroup outcome);
        int GroupCount();
        ConductDataViewModel TCFOutcomeList();
        TCFQuestionGroup GetoutcomeById(Guid GroupId);
        List<SelectListItem> GetQuiztype();
        List<SelectListItem> Bindgroup(Guid EntityId);

        ServiceResponse SaveSuboutcomes(TCFQuestion outcome);
        List<TCFQuestionViewmodel> TCFGetQuestionList();

        TCFQuestion GetTCFQuestionById(Guid Id);
        ServiceResponse DeleteoutcomeMaster(Guid Id);
    }
}
