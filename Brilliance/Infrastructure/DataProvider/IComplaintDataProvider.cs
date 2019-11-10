using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IComplaintDataProvider
    {
        ComplaintListModel ComplaintList();
        ServiceResponse GetComplaint(Guid ComplaintID);
        ServiceResponse SaveComplaints(ComplaintViewModel Complaintmodel);
        ServiceResponse DeleteComplaint(Guid ComplaintID);
        ServiceResponse GetCompanyCustomRules(Guid CompanyID);
        ServiceResponse GetDivisionByCompany(Guid CompanyID);
        ServiceResponse GetProductByCompany(Guid CompanyID);
        ServiceResponse GetProductByProductCategory(Guid ProductCategoryID);
        ServiceResponse GetNatureOfComplaintList(Guid ComplaintCategoryID);
    }
}
