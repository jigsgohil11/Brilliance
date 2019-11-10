using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("mst_CRTNotes")]
    [PrimaryKey("NoteID")]
    [Sort("SeqNo", "ASC")]
    public class CRTNotes
    {
        [Key]
        [Required(ErrorMessage = "Note ID is required")]
        public Guid NoteID { get; set; }
        public Guid? ComplaintID { get; set; }
        [MaxLength(50)]
        public string FileNoteID { get; set; }
        public DateTime? FileNoteDate { get; set; }
        [MaxLength]
        public string NoteText { get; set; }
        [MaxLength]
        public string FilePath { get; set; }
        [ResultColumn]
        public string Attachment { get; set; }
        [ResultColumn]
        public string Term { get; set; }
        [ResultColumn]
        public string FileName { get; set; }
        [MaxLength(67)]
        public string Author { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        
    }
}