using Brilliance.Models.Entity;
using Brilliance.Models.ViewModel;
using Brilliance.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace Brilliance.Infrastructure.DataProvider
{
    public class AdmCrtDataProvider : BaseDataProvider, IAdmCrtDataProvider
    {
        public ServiceResponse GetCrtSetup()
        {
            CrtAdminViewmodel crtViewModel = new CrtAdminViewmodel();
            var response = new ServiceResponse();
            try
            {
                var searchList = new List<SearchValueData>();

                crtViewModel = GetMultipleEntity<CrtAdminViewmodel>("Adm_GetCRTsetupdata", searchList);
                response.Data = crtViewModel;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {

            }
            return response;
        }

    }
}