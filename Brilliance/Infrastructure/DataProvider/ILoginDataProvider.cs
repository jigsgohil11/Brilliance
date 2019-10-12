using Brilliance.Models;

namespace Brilliance.Infrastructure.DataProvider
{
    interface ILoginDataProvider
    {
        ServiceResponse Login(string UserName, string Password);

    }
}
