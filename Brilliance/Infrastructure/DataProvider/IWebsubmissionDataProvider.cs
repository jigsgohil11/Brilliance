using Brilliance.Models;
using Brilliance.Models.ViewModel;

namespace Brilliance.Infrastructure.DataProvider
{
    interface IWebsubmissionDataProvider
    {
        ServiceResponse GetComplaint();
        ServiceResponse SaveComplaints(ComplaintViewModel Complaintmodel);
    }
}