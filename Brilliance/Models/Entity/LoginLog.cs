using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brilliance.Infrastructure;
using PetaPoco;
using System.ComponentModel.DataAnnotations;

namespace Brilliance.Models.Entity
{
    [TableName("usr_LoginLog")]
    [PrimaryKey("LoginLogID")]
    [Sort("LoginLogID", "ASC")]

    public class LoginLog
    {
        public Guid LoginLogID { get; set; }
        public Guid? ClientID { get; set; }
        public Guid? CompanyID { get; set; }
        public Guid? UserID { get; set; }
        public DateTime? LoginDateTime { get; set; }
        public string SessionID { get; set; }
        public DateTime? LogoutDateTime { get; set; }
        public string TokenID { get; set; }
        public string SourceOfLogin_Term { get; set; }
        public string IPLocation { get; set; }
        public string WebSource_Term { get; set; }
        public string DeviceSource_Term { get; set; }
        public string CurrentStatus_Term { get; set; }
        public Guid? DeviceID { get; set; }
        public Guid? ModuleID { get; set; }
        [ResultColumn]
        public long Seqno { get; set; }

        [Ignore]
        public string EncryptedLoginLogID { get { return LoginLogID != Guid.Empty ? Crypto.Encrypt(Convert.ToString(LoginLogID)) : null; } }
    }
}