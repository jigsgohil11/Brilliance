using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Brilliance.Models.Entity
{
    public class ChangePassword
    {
        [Display(Name = "Current Password")]
        [Required(ErrorMessage = "Current Password is required.")]
        [Remote("IsCurrentPasswordIsTrue", "User", ErrorMessage = "Current Password is incorrect.")]
        public string CurrentPassword { get; set; }

        [Display(Name = "New Password")]
        [Required(ErrorMessage = "New Password is required.")]
        public string NewPassword { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Confirm Password is required.")]
        [System.ComponentModel.DataAnnotations.Compare("NewPassword", ErrorMessage = "New Password and Confirm Password must match.")]
        public string ConfirmPassword { get; set; }


        public string EncryptedUserID { get; set; }
        public bool IsActive { get; set; }
        public bool IsVerified { get; set; }
    }
}