using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;


namespace Brilliance.Models.ViewModel
{
    public class ActionLoginLogModel
    {
        public ActionLoginLogModel()
        {
            ActionLogList = new List<ActionLog>();
            LoginLogList = new List<LoginLog>();
            Response = new ServiceResponse();
        }
        public List<ActionLog> ActionLogList { get; set; }
        public List<LoginLog> LoginLogList { get; set; }
        public ServiceResponse Response { get; set; }
    }
}