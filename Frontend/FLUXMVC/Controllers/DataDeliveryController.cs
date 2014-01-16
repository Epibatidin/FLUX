﻿using System.Web.Mvc;
using FLUXMVC.ViewModels;

namespace FLUXMVC.Controllers
{
    public class DataDeliveryController : ProcessorCoupledController
    {
        private DataStoreMVCAdapter GetAdapter()
        {
             return new DataStoreMVCAdapter(GetProcessor().DataStore);
        } 


        public PartialViewResult Index()
        {
            return null;
            //return PartialView("MainDataTable", GetAdapter().LayerData);
        }
        
    }
}
