using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;
namespace Brilliance.Models.Entity
{
    [TableName("mst_Company")]
    [PrimaryKey("CompanyID")]
    [Sort("SeqNo", "ASC")]
    public class Company
    {
        [Key]
        [Required]
        public Guid CompanyID { get; set; }
        [MaxLength(17)]
        public string CompanyCode { get; set; }
        [MaxLength(370)]
        public string CompanyName { get; set; }
        [MaxLength(373)]
        public string Address { get; set; }
        [MaxLength(67)]
        public string City { get; set; }
        [MaxLength(67)]
        public string State { get; set; }
        [MaxLength(67)]
        public string Country { get; set; }
        [MaxLength(137)]
        public string GeoLocation { get; set; }
        [MaxLength(19)]
        public string ZipCode { get; set; }
        [MaxLength(167)]
        public string CompanyLogo { get; set; }
        [MaxLength(367)]
        public string ContactPersonName { get; set; }
        [MaxLength(19)]
        public string PhoneNo { get; set; }
        [MaxLength(19)]
        public string ExtensionNo { get; set; }
        [MaxLength(19)]
        public string MobileNo { get; set; }
        [MaxLength(167)]
        public string Email { get; set; }
        [MaxLength(167)]
        public string WebURL { get; set; }
        [MaxLength]
        public string AboutCompany { get; set; }
        public DateTime? DateOfRegistration { get; set; }
        public Guid? RegisteredBy { get; set; }
        public long? TotalRooms { get; set; }
        [MaxLength(167)]
        public string SecondaryContactName { get; set; }
        [MaxLength(19)]
        public string SecondaryContactNo { get; set; }
        [MaxLength(19)]
        public string SecondaryExtNo { get; set; }
        [MaxLength(97)]
        public string SecondaryEmail { get; set; }
        [MaxLength(19)]
        public string SecondaryMobNo { get; set; }
        public Guid? ClientID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBlock { get; set; }
       
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? Supervisor { get; set; }
    
        public Guid? WhiteLabelTemplateID { get; set; }
        public bool? IsWhiteLabel { get; set; }
        public bool? IsSync { get; set; }
        [MaxLength]
        public string Description { get; set; }
        public Guid? SectorID { get; set; }
        public Guid? IndustryID { get; set; }
        [Ignore]
        public string EncryptedCompanyID { get { return CompanyID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(CompanyID)) : null; } }

        [Ignore]
        public bool IsEdit { get { return CompanyID != Guid.Empty ? true : false; } }

    }
}