using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("usr_User")]
    [PrimaryKey("UserID")]
    [Sort("UserID", "ASC")]
    public class User
    {
        public Guid UserID { get; set; }
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
        public string EncryptionKey { get; set; }
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        public string UserImage { get; set; }
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "EmailID is required.")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Contact no. is required.")]
        public string PhoneNumber { get; set; }
        public Guid? TimeZone { get; set; }
        public string UserType_Term { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public bool? IsSingleSigningOn { get; set; }
        public Guid? Position { get; set; }
        public Guid? AssociationID { get; set; }
        public string AssociationType_Term { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsSuperAdmin { get; set; }
        public bool? IsBlock { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime? VerifiedOn { get; set; }
        public string SourceOfVarificationTerm { get; set; }
        public Guid? ClientID { get; set; }
        public long SeqNo { get; set; }
        public byte[] UpdateLog { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [Ignore]
        public string EncryptedUserID => UserID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(UserID)) : null;
        [Ignore]
        public bool IsEdit { get { return UserID != Guid.Empty ? true : false; } }
    }
}