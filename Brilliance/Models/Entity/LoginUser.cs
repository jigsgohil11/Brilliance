using Brilliance.Infrastructure;
using PetaPoco;
using System;

namespace Brilliance.Models.Entity
{
    public class LoginUser
    {
        public Guid UserID { get; set; }
        public Guid ClientID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string DisplayName { get; set; }
        public string ProfileImage { get; set; }
        public string SecondaryEmailID { get; set; }
        public Guid TimeZoneID { get; set; }
        public Guid EmployeeID { get; set; }
        public bool IsSuperAdmin { get; set; }
        public bool IsDefault { get; set; }
        public bool IsVerified { get; set; }
        public bool IsActive { get; set; }
        public string RoleName { get; set; }
        public Guid? DefaultDropboxID { get; set; }
        public string DefaultDropboxName { get; set; }
        public string DefaultLandingPageURL { get; set; }

        [ResultColumn]
        public string FullName { get; set; }

        [Ignore]
        public string EncryptedDefaultDropboxID { get { return DefaultDropboxID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(DefaultDropboxID)) : null; } }

        public string DOJStr { get; set; }
    }
}