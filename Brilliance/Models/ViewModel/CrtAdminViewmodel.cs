using Brilliance.Infrastructure;
using Brilliance.Models.Entity;
using PetaPoco;
using System;
using System.Collections.Generic;
using System.Web.Mvc;


namespace Brilliance.Models.ViewModel
{
    public class CrtAdminViewmodel
    {
        public CrtAdminViewmodel()
        {
            client = new Client();
            clientList = new List<SelectListItem>();
            labelconfig = new LabelConfiguration();
            labelconfiglist = new List<LabelConfiguration>();
        }
        public Client client { get; set; }
        public List<SelectListItem> clientList { get; set; }
        public LabelConfiguration labelconfig { get; set; }
        public List<LabelConfiguration> labelconfiglist { get; set; }
    }

    //public class CrtAdminLabelConfigViewmodel
    //{
    //    public CrtAdminLabelConfigViewmodel()
    //    {
    //        labelconfig = new LabelConfiguration();
    //    }
    //    public LabelConfiguration labelconfig { get; set; }
       
    //}
}