using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Web;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IAdmCrtDataProvider
    {
        ServiceResponse GetCrtSetup(Guid ClientID);
        CrtAdminViewmodel GetLabelConfig(Guid TemplateID, Guid ClientID,string OrgName, bool? IsEdit);
        ServiceResponse Savelabelconfig(string TemplateName, string Tier2, string incidentdate, string Tier3, string policystatus,
                                            string Accnumber, string Rootcause, string Idnumber, string howcomreceived, string contactno,
                                            string Compregulatory, string emailaddress, string feedbackregulatory, string productcategory, string Overalloutcome,
                                            string Producttype, string Compensation, string Compcategory, string Regulatedcost, string Nature,
                                            string Value, string TCF, string DissatisfactionLevel, string SatisfactionResolution, bool? IsTemplate, bool? IsEdit, Guid? ClientID,
                                            Guid? RefTempID);
        CrtAdminViewmodel labelconfiglist(Guid ClientID);
        CrtAdminViewmodel EditLabelConfig(Guid ClientID);
        DropSelectViewmodel DropselectgList(Guid ClientID);
        DropselectModel AddDropSelect(string Category, Guid ClientID);
        ServiceResponse Savedropselectconfig(Guid TermId, Guid ClientID, Guid? Refid, Guid? Refid1, string name, string desc, string category, bool isedit);
        ServiceResponse Deletedropselectconfig(Guid TermId);
        ServiceResponse SaveCRTconfig(CrtAdminViewmodel crtadminVM);
        ServiceResponse SavedropselectInTemplate(Guid ClientID, Guid TemplateID);
        ServiceResponse GetComplaintReasonList(Guid ComplaintTypeID, Guid ClientID);
        CrtAdminViewmodel GetTemplateList();

    }
}


