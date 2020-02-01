using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("mst_Company")]
    [PrimaryKey("CompanyID")]
    [Sort("SeqNo", "ASC")]
    public class Instance
    {
        public Guid InstanceID { get; set; }
        public Guid? ClientID { get; set; }
        [MaxLength(65)]
        public string TemplateName { get; set; }
        [Required(ErrorMessage ="Template is required.")]
        public Guid? TemplateID { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string PrefferedName { get; set; }
        [Required(ErrorMessage = "start date is required.")]
        public DateTime? Initialstartdate { get; set; }
        [Required(ErrorMessage = "This filed is required.")]
        public DateTime? PeriodFrom { get; set; }
        [Required(ErrorMessage = "This filed is required.")]
        public DateTime? PeriodTo { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsActive { get; set; }
        [Ignore]
        public string EncryptedClientID { get { return ClientID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ClientID)) : null; } }
        [ResultColumn]
        public string Template { get; set; }
    }
}