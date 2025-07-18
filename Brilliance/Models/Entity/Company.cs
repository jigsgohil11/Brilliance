﻿using Brilliance.Infrastructure;
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
        public Guid CompanyID { get; set; }
        
        [StringLength(5, ErrorMessage = "Maximum length is 5")]
        public string CompanyCode { get; set; }
        [MaxLength(370)]
        [Required(ErrorMessage = "Company name is required.")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        [StringLength(5, ErrorMessage = "Maximum length is 5")]
        public string FspNo { get; set; }
        public string Complex { get; set; }
        public string StreetNo { get; set; }
        public string Street { get; set; }

        [Required(ErrorMessage = "City is required.")]
        public Guid? City { get; set; }

        [Required(ErrorMessage = "State is required.")]
        public Guid? State { get; set; }

        [Required(ErrorMessage = "Country is required.")]
        public Guid? Country { get; set; }
        [MaxLength(137)]
        public string GeoLocation { get; set; }
        [MaxLength(19)]
        public string ZipCode { get; set; }
        [MaxLength(167)]
        public string CompanyLogo { get; set; }
        [MaxLength(367)]
        [Required(ErrorMessage = "Contact person name is required.")]
        public string ContactPersonName { get; set; }
        [MaxLength(19)]
        public string PhoneNo { get; set; }
        [MaxLength(19)]
        public string ExtensionNo { get; set; }
        [MaxLength(15)]
        [Required(ErrorMessage = "Contact No is required.")]
        [RegularExpression(@"^(\+*)[0-9]*$", ErrorMessage = "Contact No.should be + and digit.")]
        public string MobileNo { get; set; }
        [MaxLength(167)]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        public string WebURL { get; set; }
        [MaxLength]
        public string AboutCompany { get; set; }
        public DateTime? DateOfRegistration { get; set; }
        public Guid? RegisteredBy { get; set; }
        public long? TotalRooms { get; set; }
        [MaxLength(167)]
        public string SecondaryContactName { get; set; }

        [RegularExpression(@"^(\+*)[0-9]*$", ErrorMessage = "Contact No. should be + and digit.")]
        public string SecondaryContactNo { get; set; }
        [MaxLength(19)]
        public string SecondaryExtNo { get; set; }

        //[RegularExpression(@"\b[A-Z0-9._%-]+@[A-Z0-9.-]+\.[A-Z]{2,4}\b", ErrorMessage = "Please enter a valid e-mail adress")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string SecondaryEmail { get; set; }
        [MaxLength(19)]
        public string SecondaryMobNo { get; set; }
        [Required(ErrorMessage = "Organization is required.")]
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
        public bool IsViewDissatisfationLevel { get; set; }
        public bool IsRequireDissatisfationLevel { get; set; }
        public bool IsViewSatisfationLevel { get; set; }
        public bool IsRequireSatisfationLevel { get; set; }

        public string Description { get; set; }
        public Guid? SectorID { get; set; }
        public Guid? IndustryID { get; set; }

        [ResultColumn]
        public string OrganizationName { get; set; }
        [Ignore]
        public string EncryptedCompanyID { get { return CompanyID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(CompanyID)) : null; } }
        [ResultColumn]
        public string EncryptedClientID { get { return ClientID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ClientID)) : null; } }

        [Ignore]
        public bool IsEdit { get { return CompanyID != Guid.Empty ? true : false; } }

        [ResultColumn]
        public bool IsInsert { get; set; }
    }
}