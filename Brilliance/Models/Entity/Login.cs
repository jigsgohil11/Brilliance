using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    public class Login
    {
        [DisplayName("User Name")]
        [Required(ErrorMessage = "User Name is Required.")]
        public string UserName { get; set; }

        [DisplayName("Password")]
        [Required(ErrorMessage = "Password is Required.")]
        public string Password { get; set; }

        [MaxLength(100)]
        [Display(Name = "EmailID")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "Enter Valid Email Address")]
        [Required(ErrorMessage = "Email Id is required.")]
        public string SecondaryEmailID { get; set; }
        [DisplayName("OTP")]
        [Required(ErrorMessage = "Enter OTP.")]
        public string OTPCode { get; set; }
    }
}