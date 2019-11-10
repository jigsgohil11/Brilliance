using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Brilliance.Models.Entity
{
    [TableName("mst_ProjectTerm")]
    [PrimaryKey("TermID")]
    public class ProjectTermCategory
    {
        [Key]
        [Required(ErrorMessage = "Term ID is required")]
        public Guid TermID { get; set; }
        public Guid ProjectTermcategoryID { get; set; }
        [MaxLength(17)]
        public string TermCode { get; set; }
        [MaxLength(167)]
        [Required(ErrorMessage ="Term is required")]
        public string Term { get; set; }
        [MaxLength(167)]
        public string TermCategory { get; set; }
        public int? OrderNo { get; set; }
        [MaxLength(167)]
        public string AboutTerm { get; set; }
        [MaxLength(167)]
        public string IconPath { get; set; }
        [MaxLength(167)]
        public string BackColor { get; set; }
        [MaxLength(167)]
        public string ForeColor { get; set; }
        
        public bool? IsDefault { get; set; }
       
        public bool? IsActive { get; set; }
        public bool? IsBlock { get; set; }
        public Guid? ClientID { get; set; }
        public Guid? RefTermID { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        [MaxLength(167)]
        public string DefaultValue { get; set; }
        [Ignore]
        public bool IsEdit { get { return TermID != Guid.Empty ? true : false; } }
        [Ignore]
        public string EncryptedProjectTermID { get { return TermID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(TermID)) : null; } }
        [ResultColumn]
        public string CategoryName { get; set; }

    }
}