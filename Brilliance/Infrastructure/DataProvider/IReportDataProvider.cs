using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Data;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IReportDataProvider
    {

        DataSet GetComplaintExcelReport(string Type, Guid CompanyID, Guid DivisionID);
        DataSet GetLevel2ComplaintExcelReport(string Type,Guid CompanyID);
        DataSet GetLevel1ComplaintExcelReport(string Type);

    }
}
