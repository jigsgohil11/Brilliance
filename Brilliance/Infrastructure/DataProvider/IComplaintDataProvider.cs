﻿using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Data;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IComplaintDataProvider
    {
        ComplaintListModel ComplaintList();
        ServiceResponse GetComplaint(Guid ComplaintID);
        ServiceResponse SaveComplaints(ComplaintViewModel Complaintmodel,DataTable complaint_note);
        ServiceResponse DeleteComplaint(Guid ComplaintID);
        ServiceResponse GetCompanyCustomRules(Guid CompanyID);
        ServiceResponse GetDivisionByCompany(Guid CompanyID);
        ServiceResponse GetProductByCompany(Guid CompanyID);
        ServiceResponse GetProductByProductCategory(Guid ProductCategoryID);
        ServiceResponse GetNatureOfComplaintList(Guid ComplaintCategoryID);
        ServiceResponse GetTCFOutcome(Guid ComplaintCategoryID, Guid NatureID);
        ServiceResponse GetReport();
        ServiceResponse GetCompanyReport();
        ServiceResponse GetOrganisationReport();
        ServiceResponse GetDivisionReport();
        ServiceResponse GetDivbyCompany(Guid CompanyID);

    }
}
