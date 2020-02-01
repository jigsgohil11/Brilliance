using Brilliance.Models.Entity;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class AppSubscriptionViewModel
    {
        public AppSubscriptionViewModel()
        {
            instance = new Instance();
            Templatelist = new List<SelectListItem>();
            instancelist = new List<Instance>();
        }
        public Instance instance { get; set; }
        public List<SelectListItem> Templatelist { get; set; }
        public List<Instance> instancelist { get; set; }
    }
}