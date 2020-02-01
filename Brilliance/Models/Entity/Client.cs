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
        [Required]
        public Guid ClientID { get; set; }
        
        public string ClientCode { get; set; }
        
        [Required(ErrorMessage = "Organization name is required.")]
        public string OrganizationName { get; set; }
        [MaxLength(167)]
        public string TotalNumberofUser { get; set; }
        
        public string ContactPersonName { get; set; }
        public string ContactPersonSurName { get; set; }
      
        public string ContactPersonEmail { get; set; }
        
        [RegularExpression(@"^(\+*)[0-9]*$", ErrorMessage = "Mobile No.should be + and digit.")]
        public string TelephoneNo { get; set; }
        
        [RegularExpression(@"^(\+*)[0-9]*$", ErrorMessage = "Mobile No.should be + and digit.")]
        public string MobileNo { get; set; }
        public string Industry { get; set; }
        public Guid? AssociatePartnerID { get; set; }
        public string VATNo { get; set; }
       
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }

        
        public Guid? Country { get; set; }
        
        public Guid? State { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        [Required(ErrorMessage = "Entity number is required.")]
        public string EntityNo { get; set; }
        public string FSPNo { get; set; }
        [Required(ErrorMessage = "URL is required.")]
        public string URLDetails { get; set; }
        public string Logo { get; set; }
        public string Skincolours { get; set; }
        public string Sitepullthrough { get; set; }
        public string buttonsclr { get; set; }
        public string headsclr { get; set; }
        public string subheadsclr { get; set; }
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
        
        public string Complex { get; set; }
        public string StreetNo { get; set; }
        public string Street { get; set; }
        public bool IsCrtSubscribe { get; set; }
        public bool IsCISSubscribe { get; set; }

        [ResultColumn]
        public string countorganisation { get; set; }
        [ResultColumn]
        public bool IsCompanyExist { get; set; }
        [ResultColumn]
        public bool OrganisationID { get; set; }

        [Ignore]
        public string EncryptedClientID { get { return ClientID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ClientID)) : null; } }

        //[Ignore]
        //public bool IsEdit { get { return ClientID != Guid.Empty ? true : false; } }

        [Ignore]
        public bool IsEdit { get; set; }
    }
}