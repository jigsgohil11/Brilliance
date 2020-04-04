using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Data;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IReportDataProvider
    {

        DataSet GetComplaintExcelReport(string Type);
        DataSet GetLevel2ComplaintExcelReport(string Type);
        DataSet GetLevel1ComplaintExcelReport(string Type);

    }
}
