using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
  
    [TableName("mst_Client")]
    [PrimaryKey("ClientID")]
    [Sort("SeqNo", "ASC")]
    public class Client
    {
        [Key]
       // [Required]
        public Guid ClientID { get; set; }
        [MaxLength(7)]
        //[Required(ErrorMessage ="Code is required.")]
        public string ClientCode { get; set; }
        [MaxLength(167)]
       // [Required(ErrorMessage = "Organization name is required.")]
        public string OrganizationName { get; set; }
        [MaxLength(167)]
        public string TotalNumberofUser { get; set; }
        [MaxLength(67)]
        //[Required(ErrorMessage = "Contact person name is required.")]
        public string ContactPersonName { get; set; }
        [MaxLength(31)]
        //[Required(ErrorMessage = "Email is required.")]
        public string ContactPersonEmail { get; set; }
        [MaxLength(19)]
        //[Required(ErrorMessage = "Mobile No. is required.")]
        public string MobileNo { get; set; }
        public Guid? AssociatePartnerID { get; set; }
        [MaxLength(167)]
        public string Country { get; set; }
        [MaxLength(167)]
        public string State { get; set; }
        [MaxLength(167)]
        public string City { get; set; }
        public bool? IsAcceptedTermsServices { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBlock { get; set; }
        public DateTime? DateOfRegister { get; set; }
        public DateTime? DateOfActivation { get; set; }
        public DateTime? DateOfExpiry { get; set; }
        [MaxLength(167)]
        public string CurrentStatus_Term { get; set; }
        [MaxLength(189)]
        public string WebURL { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        
        public string Description { get; set; }
       
        public string Address { get; set; }
       
        [Ignore]
        public string EncryptedClientID { get { return ClientID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ClientID)) : null; } }

        [Ignore]
        public bool IsEdit { get { return ClientID != Guid.Empty ? true : false; } }
    }
}