using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Brilliance.Models.ViewModel
{
    public class ConductDataViewModel
    {
        public ConductDataViewModel()
        {
            TCFQuestionGrouplist = new List<TCFQuestionGroupViewmodel>();
            TCFQuestion = new List<TCFQuestion>();
            Response = new ServiceResponse();
        }
        public List<TCFQuestionGroupViewmodel> TCFQuestionGrouplist { get; set; }
        public List<TCFQuestion> TCFQuestion { get; set; }
        public ServiceResponse Response { get; set; }
    }
}