using Brilliance.Infrastructure;
using PetaPoco;
using System;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("usr_Role")]
    [PrimaryKey("RoleID")]
    [Sort("RoleID", "ASC")]
    public class Role
    {
        [Key]
        public Guid RoleID { get; set; }
        public string RoleCode { get; set; }
        public string RoleName { get; set; }
        public string AboutRole { get; set; }
        public Guid? ClientID { get; set; }
        public bool? IsDefault { get; set; }
        public bool? IsActive { get; set; }
        public byte[] UpdateLog { get; set; }
        public bool? IsBlock { get; set; }
        public int? OrderNo { get; set; }
        public long SeqNo { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? ModuleID { get; set; }
        [Ignore]
        public string EncryptedRoleID { get { return RoleID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(RoleID)) : null; } }

        [Ignore]
        public bool IsEdit { get { return RoleID != Guid.Empty ? true : false; } }
    }
}