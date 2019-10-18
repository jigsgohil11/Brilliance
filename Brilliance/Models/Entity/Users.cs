using System;
using System.ComponentModel.DataAnnotations;
using Brilliance.Infrastructure;
using PetaPoco;

namespace Brilliance.Models.Entity
{
    [TableName("usr_User")]
    [PrimaryKey("UserID")]
    [Sort("UserID", "ASC")]
    public class Users
    {
        [Key]
        public Guid UserID { get; set; }

        [Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Resource.Resource))]
        [StringLength(267, ErrorMessageResourceName = "NameMaxLength", ErrorMessageResourceType = typeof(Resource.Resource))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Resource.Resource))]
        [StringLength(367, ErrorMessageResourceName = "NameMaxLength", ErrorMessageResourceType = typeof(Resource.Resource))]
        public string Password { get; set; }

        [StringLength(17, ErrorMessageResourceName = "CodeMaxLength", ErrorMessageResourceType = typeof(Resource.Resource))]
        public string ContactNo { get; set; }
        [Ignore]
        public Guid? RegionsID { get; set; }

        [Required(ErrorMessageResourceName = "CodeRequired", ErrorMessageResourceType = typeof(Resource.Resource))]
        [Ignore]
        public Guid? RoleID { get; set; }
        public bool IsVerified { get; set; }

        [ResultColumn]
        public string RegionsIDs { get; set; }
        [ResultColumn]
        public string ConstituencieIDs { get; set; }
        [ResultColumn]
        public string LocationIDs { get; set; }

        public Guid? ConstituencieID { get; set; }
        public Guid? LocationID { get; set; }

        [Required(ErrorMessageResourceName = "CodeRequired", ErrorMessageResourceType = typeof(Resource.Resource))]
        [StringLength(7, ErrorMessageResourceName = "CodeMaxLength", ErrorMessageResourceType = typeof(Resource.Resource))]
        public string UserCode { get; set; }

        public bool IsActive { get; set; }

        [ResultColumn]
        public long SeqNo { get; set; }

        public long OrderNo { get; set; }

        [ResultColumn]
        public Guid UpdatedBy { get; set; }

        public DateTime UpdateOn { get; set; }

        [ResultColumn]
        public byte[] UpdateLog { get; set; }

        public Guid ClientID { get; set; }

        public string PhotoPath { get; set; }

        [RegularExpression(Constants.RegxEmail, ErrorMessageResourceName = "EmailInvalid", ErrorMessageResourceType = typeof(Resource.Resource))]
        [StringLength(267, ErrorMessageResourceName = "EmailMaxLength", ErrorMessageResourceType = typeof(Resource.Resource))]
        public string EmailID { get; set; }

        [Ignore]
        public string EncryptedUserID { get { return UserID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(UserID)) : null; } }

        [ResultColumn]
        public bool? IsChangePass { get; set; }

        [ResultColumn]
        public string RegionName { get; set; }


        [ResultColumn]
        public string ConstituenciesName { get; set; }

        [ResultColumn]
        public string LocationName { get; set; }

        public Guid? UserGroupID { get; set; }

        public string ProgrammeID { get; set; }
        [ResultColumn]
        public string ProgramName { get; set; }
        [ResultColumn]
        public string ProgramIDs { get; set; }
    }
}