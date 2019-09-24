using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brilliance.Infrastructure;
using PetaPoco;
using System.ComponentModel.DataAnnotations;


namespace Brilliance.Models.Entity
{
    [TableName("user_ActionLog")]
    [PrimaryKey("ActionLogID")]
    [Sort("ActionLogID", "ASC")]

    public class ActionLog
    {
        public Guid ActionLogID { get; set; }
        public Guid? UserID { get; set; }
        public Guid? ClientID { get; set; }
        public Guid? LoginLogID { get; set; }
        public DateTime? ActionDate { get; set; }
        public string ActionObject { get; set; }
        public string ActionFrom { get; set; }
        public string OperationType { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }

        [ResultColumn]
        public long SeqNo { get; set; }

        [ResultColumn]
        public byte[] UpdateLog { get; set; }

        [Ignore]
        public string EncryptedActionLogID { get { return ActionLogID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(ActionLogID)) : null; } }

        [ResultColumn]
        public string UserName { get; set; }
    }
}