using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brilliance.Infrastructure;
using PetaPoco;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("user_LoginLog")]
    [PrimaryKey("LoginLogID")]
    [Sort("LoginLogID", "ASC")]

    public class LoginLog
    {
        public Guid LoginLogID { get; set; }

        public DateTime? LoginDate { get; set; }

        public Guid? ClientiD { get; set; }

        public string SessionID { get; set; }

        public string LoginSource { get; set; }

        [ResultColumn]
        public long SeqNo { get; set; }

        public DateTime? LogoutDate { get; set; }

        [ResultColumn]
        public byte[] UpdateLog { get; set; }
        public Guid? UserID { get; set; }

        [Ignore]
        public string EncryptedLoginLogID { get { return LoginLogID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(LoginLogID)) : null; } }

        [ResultColumn]
        public string UserName { get; set; }
    }
}