using PetaPoco;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Brilliance.Models.Entity
{
    public class TCFQuestionGroup
    {
        public TCFQuestionGroup()
        {
           
            TCFQuestion = new List<TCFQuestion>();
        }
        [Key]
        public Guid GroupId { get; set; }
        [Required(ErrorMessage ="Please select Entity")]
        public Guid HoldingEntityId { get; set; }
        [ForeignKey("HoldingEntityId")]
        public Company _Company { get; set; }

        [Required(ErrorMessage = "Please select Division")]
        public Guid DivisionId { get; set; }
        [ForeignKey("DivisionId")]
        public Division _Division { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "Please provide Description")]
        public string Description { get; set; }

        public bool IsVisible { get; set; }
        public int SortingWeight { get; set; }
        public bool IsDeleted { get; set; }

        public Guid CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public Users _FKUsercreate { get; set; }
        public DateTime Createddate { get; set; }

        public DateTime ? Modifieddate { get; set; }
        public Guid ? ModifiedBy { get; set; }
        [ForeignKey("ModifiedBy")]
        public Users _fkUsersmodify { get; set; }
        public DateTime? CurrentDueDate { get; set; }
        public string AdditionalDescription { get; set; }
        public string Filename { get; set; }
        [Ignore]
        public bool IsEdit { get { return GroupId != Guid.Empty ? true : false; } }

        public HttpPostedFileBase file { get; set; }

        [Ignore]
        public string coderef { get; set; }
        public virtual ICollection<TCFQuestion> TCFQuestion { get; set; }
    }
    public class TCFQuestionType
    {
      
        public Guid Id { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
    }
    public class TCFFormEvidence
    {
        [Key]
        public Guid Id { get; set; }
     
        public Guid ClusterId { get; set; }
        public Guid TCFPeriodId { get; set; }
        public Guid TCFQuestionId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte FileAttachment { get; set; }

        public DateTime CreatedAt { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsDeleted { get; set; }
        [Ignore]
        public IEnumerable<HttpPostedFileBase> files { get; set; }
        [Ignore]
        public bool IsEdit { get { return Id != Guid.Empty ? true : false; } }
    }

    public class TCFQuestion
    {
        public TCFQuestion()
        {
            CreatedAt = DateTime.Now;
            TCFNotes = new List<TCFNotes>();
            TCFTask = new List<TCFTask>();
            TCFFormEvidence = new List<TCFFormEvidence>();
        }
        
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Please select Group")]
        public Guid TCFQuestionGroupId { get; set; }
        [Required(ErrorMessage = "Please select Types")]
        public Guid TCFQuestionTypeId { get; set; }
        [Required(ErrorMessage = "Please select Entity")]
        public Guid BinderHolderTypeId { get; set; }
        [Required(ErrorMessage = "Please provide code")]

        public string Code { get; set; }
        [Required(ErrorMessage = "Please provide Description")]
        public string Description { get; set; }
        public string GuidanceTitle { get; set; }
        [Required(ErrorMessage = "Please provide Guidance Description")]

        public string GuidanceDescription { get; set; }
        public bool IsStandard { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTime? Modifieddate { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime ? UpdatedAt { get; set; }

        public DateTime? DueDate { get; set; }
        public Guid RateId { set; get; }

        public string ReasonNotYet { get; set; }

        public string Reasonmostly { get; set; }
        public string ReasonNotApplicable { get; set; }

        public string ReasonPartially { get; set; }
        public string ReasonFully { get; set; }

        public Guid? UpdatedBy { get; set; }
        public Guid? TCFSubOutComeUserId { get; set; }

        public virtual IList<TCFNotes> TCFNotes { get; set; }
        public virtual IList<TCFTask> TCFTask { get; set; }
        public virtual IList<TCFFormEvidence> TCFFormEvidence { get; set; }
        public bool  IsDeleted { get; set; }
        [Ignore]
        public bool IsEdit { get { return Id != Guid.Empty ? true : false; } }

    }


    public class TCFQuestionGroupViewmodel
    {
        public Guid Id { get; set; }
        public Guid OutcomeId { get; set; }
        public Guid TCFSubOutComeId { get; set; }

        public string Code { get; set; }
        public string Description { get; set; }

        public string Title { get; set; }
        public string Qgroup { get; set; }
        public string Qdesc { get; set; }
        public int Status { get; set; }
        public int ? PreviousRating { get; set; }
        public int? CurrentRating { get; set; }

        public string DueDateDes { get; set; }
        public Guid GroupId { get; set; }
    }
    [PrimaryKey("Id", autoIncrement = true)]
    public class TCFForm
    {

        public Guid Id { get; set; }
        public Guid ClusterId { get; set; }
        public Guid TCFPeriodId { get; set; }
        public Guid BinderHolderTypeId { get; set; }
        public string Description { get; set; }
        
        public DateTime CreatedAt { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime ? UpdatedAt { get; set; }

        public Guid  ? UpdatedBy { get; set; }
        public int  IsDeleted { get; set; }


     
        public virtual TCFOutCome  TCFOutCome { get; set; }
        [Ignore]
        public bool IsEdit { get { return Id != Guid.Empty ? true : false; } }
        [Ignore]
        public string images { get; set; }
        /// public virtual User  _User { get; set; }

    }
    [PrimaryKey("Id")]
    public class TCFOutCome
    {
        public Guid Id { get; set; }
        [ForeignKey("TCFForm")]
        public Guid TCFoutcomeId { set; get; }
        public  virtual TCFForm TCFForm { get; set; }

        [ForeignKey("TCFForm")]
        public Guid TCFQuestionGroupId { set; get; }
        public virtual TCFQuestionGroup TCFQuestionGroup { get; set; }

        public Guid AddedBy { set; get; }
        public DateTime AddedDate { set; get; }
        public virtual TCFSubOutCome TCFSubOutCome { get; set; }
        [Ignore]
        public bool IsEdit { get { return Id != Guid.Empty ? true : false; } }
    }
    [PrimaryKey("Id")]
    public  class TCFSubOutCome
    {
        public TCFSubOutCome()
        {
            TCFNotes = new List<TCFNotes>();
            TCFTask = new List<TCFTask>();
            TCFFormEvidence = new List<TCFFormEvidence>();
        }
        public Guid Id { set; get; }
        [ForeignKey("TCFSubOutComeId")]
        public Guid TCFSubOutComeId { set; get; }
        public virtual TCFOutCome _TCFOutCome { get; set; }
      
        public Guid TCFQuestionId { set; get; }
        [ForeignKey("TCFQuestionId")]
        public TCFQuestion TCFQuestion { get; set; }
        public Guid RateId { set; get; }
        public Guid TCFFormId { set; get; }

        
        public Guid ? TCFSubOutComeUserId { get; set; }
        [ForeignKey("TCFSubOutComeUserId")]
        public virtual User  _User { get; set; }
       

        public string ReasonNotYet { get; set; }

        public string Reasonmostly { get; set; }
        public string ReasonNotApplicable { get; set; }

        public string ReasonPartially { get; set; }
        public string ReasonFully { get; set; }
        public Guid AddedBy { set; get; }
        public DateTime AddedDate { set; get; }

        public Guid ? ModifiedBy { set; get; }

        public DateTime ? ModifiedDate { set; get; }
        public DateTime ? DueDate { set; get; }

        public virtual IList<TCFNotes> TCFNotes { get; set; }
        public virtual IList<TCFTask> TCFTask { get; set; }
        public virtual IList<TCFFormEvidence> TCFFormEvidence { get; set; }
        [Ignore]
        public bool IsEdit { get { return TCFSubOutComeUserId != Guid.Empty ? true : false; } }
      
    }
    public class TCFNotes
    {
        public Guid Id { get; set; }
       
        public Guid TCFQuestionId { get; set; }
        public Guid AddedById { get; set; }
        public DateTime AddedAt { get; set; }
        public string Note { get; set; }
        
        public string NoteId { get; set; }
        public string Author { get; set; }
        public Guid ? ModifiedById { get; set; }
        public DateTime ? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
       
    }
    public class TCFTask
    {
        public TCFTask()
        {
            _TaskStatus = new List<TaskStatus>();
        }
        public Guid Id { get; set; }
      
        public Guid TCFQuestionId { get; set; }
    

        public Guid AddedById { get; set; }

        public Guid AssignUserId { get; set; }

        public DateTime? DueDate { get; set; }

        public DateTime AddedAt { get; set; }
        public string Task { get; set; }
        public string Status { get; set; }
        public Guid ? ModifiedById { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public List<TaskStatus> _TaskStatus { get; set; }
    }
    public class TCFQuestionViewmodel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public Guid Id { get; set; }
    }
    public class TaskStatus
    {
        public string Id { get; set; }
        public string Value { get; set; }

    }
}