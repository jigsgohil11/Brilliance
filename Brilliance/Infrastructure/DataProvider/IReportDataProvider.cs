using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Data;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IReportDataProvider
    {
        DataSet GetComplaintExcelReport(Guid ClientID,string Type);
    }
}
