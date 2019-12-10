using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("mst_Complaint")]
    [PrimaryKey("ComplaintID")]
    [Sort("SeqNo", "ASC")]
    public class Complaint
    {

        public Guid ComplaintID { get; set; }
        [Required(ErrorMessage = "Company is required.")]
        public Guid? CompanyId { get; set; }
        [Required(ErrorMessage = "Division is required.")]
        public Guid? DivisionId { get; set; }
        [Required(ErrorMessage = "Policy number is required.")]
        [MaxLength(50)]
        public string PolicyNumber { get; set; }
        public bool IsClient { get; set; }
        [MaxLength(50)]
        public string ClientIdentificationNumber { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Last name is required.")]
        public string LastName { get; set; }
        [MaxLength(19)]
        [Required(ErrorMessage = "Contact no. is required.")]
        public string PhoneNo { get; set; }
        [MaxLength(50)]
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
        public Guid? ComplaintProductId { get; set; }
        public Guid? ComplaintProductTypeId { get; set; }
        [Required(ErrorMessage = "Complaint category is required.")]
        public Guid? ComplaintCategoryId { get; set; }
        [Required(ErrorMessage = "Nature of complaint is required.")]
        public Guid? ComplaintNatureOfId { get; set; }
        [Required(ErrorMessage = "Received date is required.")]
        public DateTime? DateReceived { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = Constants.DateFormatString, ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Incident date is required.")]
        public DateTime? DateIncident { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Notes { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string IsResolved { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = Constants.DateFormatString, ApplyFormatInEditMode = true)]
        public DateTime? DateResolved { get; set; }
        [Required(ErrorMessage = "Policy status is required.")]
        public Guid? ComplaintPolicyStatusId { get; set; }
        public Guid? ComplaintRootCauseId { get; set; }
        public Guid? ComplaintReceivedId { get; set; }
        [Required(ErrorMessage = "Received Regulatory is required.")]
        public Guid? ComplaintReceivedRegulatoryId { get; set; }
        [Required(ErrorMessage = "Received Regulatory Feedback is required.")]
        public Guid? ComplaintReceivedRegulatoryFeedbackId { get; set; }
        [MaxLength(50)]
        public string ComplaintReceivedRegulatoryReference { get; set; }
        public Guid? ComplaintOverallOutcomeId { get; set; }
        public Guid? ComplaintCompensationId { get; set; }
        public Guid? ComplaintRegulatedCostId { get; set; }
        [Required(ErrorMessage = "Satisfaction Level is required.")]
        public Guid? SatisfactionLevel { get; set; }
        [Required(ErrorMessage = "Satisfaction level is required.")]
        public Guid? Satisfactionlevel_resolution { get; set; }
        public string IsComplaint { get; set; }
        public long? CompensationValue { get; set; }
        public string Isclaimpaid { get; set; }
        public string IsVATregistered { get; set; }
        public string IsPayee { get; set; }
        public DateTime? claimpaymentdate { get; set; }
        public decimal claimpaidamount_exclVAT { get; set; }
        public decimal claimpaidamount_inclVAT { get; set; }
        public string VAT_number { get; set; }
        public string Panel_Attorneys { get; set; }
        [Required(ErrorMessage = "Case File Stage is required.")]
        public Guid? CaseFileStage { get; set; }
        [Required(ErrorMessage = "Event type is required.")]
        public Guid? Eventtype { get; set; }
        [Required(ErrorMessage = "Description of loss is required.")]
        public Guid? Descofloss { get; set; }
        public bool? IsActive { get; set; }
        public string ComplaintType { get; set; }


        public DateTime? CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }

        [ResultColumn]
        public string company { get; set; }
        [ResultColumn]
        public string division { get; set; }
        [ResultColumn]
        public string productcategory { get; set; }
        [ResultColumn]
        public string natureofcomplaint { get; set; }
        [ResultColumn]
        public string ReceivedDate { get; set; }
        [ResultColumn]
        public string ResolvedDate { get; set; }

        [ResultColumn]
        public bool IsEdit { get; set; }


        [Ignore]
        public string EncryptedComplaintID { get { return ComplaintID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ComplaintID)) : null; } }

        [Ignore]
        public bool IsEditforNotes { get { return ComplaintID != Guid.Empty ? true : false; } }

    }
}