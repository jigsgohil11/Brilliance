using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("CRT_Template")]
    [PrimaryKey("TemplateID")]
    [Sort("SeqNo", "ASC")]
    public class CrtTemplate
    {
        [Key]
        public Guid TemplateID { get; set; }
        public Guid? ClientID { get; set; }
        public Guid? InstanceID { get; set; }
        public string TemplateName { get; set; }
        public string Tier2 { get; set; }
        public string Tier3 { get; set; }
        public string incidentdate { get; set; }
        public string policystatus { get; set; }
        public string Accnumber { get; set; }
        public string Rootcause { get; set; }
        public string Idnumber { get; set; }
        public string howcomreceived { get; set; }
        public string contactno { get; set; }
        public string Compregulatory { get; set; }
        public string emailaddress { get; set; }
        public string feedbackregulatory { get; set; }
        public string productcategory { get; set; }
        public string Overalloutcome { get; set; }
        public string Producttype { get; set; }
        public string Compensation { get; set; }
        public string Compcategory { get; set; }
        public string Regulatedcost { get; set; }
        public string Nature { get; set; }
        public string Value { get; set; }
        public string TCF { get; set; }
        public string DissatisfactionLevel { get; set; }
        public string SatisfactionResolution { get; set; }
        public bool? IsTemplate { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}