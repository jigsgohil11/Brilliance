using Brilliance.Models.Entity;
using Brilliance.Models;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Brilliance.Models.ViewModel
{
    public class ClientViewModel
    {
        public ClientViewModel()
        {
            client = new Client();
        }
        public Client client { get; set; }
    }
    public class ClientListModel
    {
        public ClientListModel()
        {
            Clientlist = new List<Client>();
            Response = new ServiceResponse();
        }
        public List<Client> Clientlist { get; set; }
        public ServiceResponse Response { get; set; }
    }
}