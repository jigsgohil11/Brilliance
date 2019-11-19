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
    interface ITemplateMaster
    {
        ServiceResponse SaveTemplateMaster(TemplateMaster template);
        TemplateViewModel TemplateMasterList();
        TemplateMaster GetTemplateById(Guid Id);
        List<SelectListItem> GetTemplatemaster();
        ServiceResponse Addoutcomemastertemplate(TemplateOutcomemaster _model);
        int MasterCont(Guid TemplateId);
        string Prcoutcometitle(Guid TemplateId);
        TemplateViewModel TemplateOutcomeList();

        TemplateOutcomemaster GetTemplateOutcomeById(Guid Id);
        List<SelectListItem> GetTemplateGroupcode();
        int GrupcntByEntity(int Id, Guid TemplateId);
        string GetGroupSubCode(int count);
        List<SelectListItem> GetQuiztype();
        List<SelectListItem> Bindgroup(Guid TemplateId);
        string GetSuboutcomeCode(Guid Id);
        ServiceResponse SavetemplateSuboutcomemaster(TemplateSuboutcomeMaster _model);
        TemplateViewModel GetTemplatesuboutcomelist();

        TemplateSuboutcomeMaster GetTemplateSuboutcomeMasterById(Guid Id);
        List<SelectListItem> Bindclient();
        List<SelectListItem> BindDivionByCompany(Guid CompanyID);

        ServiceResponse AttachStandard(StandardConduct _model);

    }
}
