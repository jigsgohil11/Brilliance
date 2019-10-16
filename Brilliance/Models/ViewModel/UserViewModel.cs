using Brilliance.Models.Entity;

namespace Brilliance.Models.ViewModel
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Users = new Users();           
        }
        public Users Users { get; set; }
    }    
}