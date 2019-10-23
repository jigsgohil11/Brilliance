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
        public Client _Client { get; set; }
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

        [Ignore]
        public bool IsEdit { get { return GroupId != Guid.Empty ? true : false; } }

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
        public int Id { get; set; }
        public int ClusterId { get; set; }
        public int TCFPeriodId { get; set; }
        public int TCFQuestionId { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public byte FileAttachment { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ExpiresAt { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class TCFQuestion
    {
        public TCFQuestion()
        {
            CreatedAt = DateTime.Now;

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

        public Guid? UpdatedBy { get; set; }
        public bool ? IsDeleted { get; set; }
        [Ignore]
        public bool IsEdit { get { return Id != Guid.Empty ? true : false; } }

    }


    public class TCFQuestionGroupViewmodel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Qgroup { get; set; }
        public string Description { get; set; }
        public string Qdesc { get; set; }
        public int Status { get; set; }
        public Guid GroupId { get; set; }
    }
    public class TCFForm
    {
        public TCFForm()
        {
           _TCFSubOutCome = new List<TCFSubOutCome>();
            TCFNotes = new List<TCFNotes>();
            TCFTask= new List<TCFTask>();
        }
        public Guid Id { get; set; }
        public Guid ClusterId { get; set; }
        public Guid TCFPeriodId { get; set; }
        public Guid TCFQuestionId { get; set; }

        public Guid TCFFormRatingId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DueDate { get; set; }

        public string ReasonNotApplicable { get; set; }

        public string ReasonPartially { get; set; }
        public string ReasonFully { get; set; }
        public DateTime ? Reminder1 { get; set; }
        public DateTime? Reminder2 { get; set; }
        public DateTime? Reminder3 { get; set; }

        public int ? Reminder1IsSent { get; set; }
        public int? Reminder2IsSent { get; set; }
        public int? Reminder3IsSent { get; set; }
        public int? Reminder4IsSent { get; set; }

        public int ? NotificationNRPIsSent { get; set; }
        public DateTime CreatedAt { get; set; }

        public int CreatedBy { get; set; }

        public DateTime ? UpdatedAt { get; set; }

        public int ? UpdatedBy { get; set; }
        public int IsDeleted { get; set; }

        public int? TempId { get; set; }


        public virtual IList<TCFNotes> TCFNotes { get; set; }
        public virtual IList<TCFTask> TCFTask { get; set; }
        public virtual IList<TCFSubOutCome> _TCFSubOutCome { get; set; }

    }
    public  class TCFSubOutCome
    {
        public Guid TCFFormId { set; get; }
        public Guid TCFoutcomeId { set; get; }
        public Guid RateId { set; get; }
        public string Note { set; get; }
        public Guid ? AssignUserId { get; set; }
        [ForeignKey("AssignUserId")]
        public virtual User  _User { get; set; }
        public DateTime DueDate { set; get; }
    }
    public class TCFNotes
    {
        public Guid Id { get; set; }
        public Guid TcfId { get; set; }
        public Guid AddedById { get; set; }
        public DateTime AddedAt { get; set; }
        public string Note { get; set; }
        public string NoteId { get; set; }
        public string Author { get; set; }
    }
    public class TCFTask
    {
        public Guid Id { get; set; }
        public Guid TcfId { get; set; }
        public Guid AddedById { get; set; }

        public Guid AssignUserId { get; set; }
        public DateTime AddedAt { get; set; }
        public string Task { get; set; }
    }
    public class TCFQuestionViewmodel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public Guid Id { get; set; }
    }

}