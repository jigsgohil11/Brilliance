﻿using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("mst_Division")]
    [PrimaryKey("DivisionID")]
    [Sort("SeqNo", "ASC")]
    public class Division
    {

        [Key]
        public Guid DivisionID { get; set; }

        //[RegularExpression(@"^.{1,50}$", ErrorMessage = "only 5 character")]
        public string Code { get; set; }
        [MaxLength(167)]
        [Required(ErrorMessage = "Division Name is required.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage = "Company is required.")]
        public Guid? CompanyID { get; set; }
        [Required(ErrorMessage = "Client is required.")]
        public Guid? ClientID { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsBlock { get; set; }
       
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? DivisionType { get; set; }

        [ResultColumn]
        public string Company { get; set; }

        [ResultColumn]
        public string Type { get; set; }
        [ResultColumn]

        public string Client { get; set; }

        [ResultColumn]
        public string OrganizationName { get; set; }

        [Ignore]
        public string EncryptedDivisionID { get { return DivisionID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(DivisionID)) : null; } }
        [Ignore]
        public string EncryptedClientID { get { return ClientID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ClientID)) : null; } }
        [Ignore]
        public string EncryptedCompanyID { get { return CompanyID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(CompanyID)) : null; } }

        //[Ignore]
        //public bool IsEdit { get { return DivisionID != Guid.Empty ? true : false; } }
        [ResultColumn]
        public bool IsEdit { get; set; }

    }
}