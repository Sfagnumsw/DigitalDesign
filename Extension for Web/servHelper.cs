using DocsVision.BackOffice.ObjectModel;
using DocsVision.Platform.WebClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkTripServerSE.Service;

namespace WorkTripServerSE.Halper
{
    public class ServiceHelper : DocsVision.BackOffice.WebClient.Helpers.ServiceHelper
    { //создание своего хелпера для возможности его использования с сервисе
        public ServiceHelper(IServiceProvider serviceProvider) : base(serviceProvider) { } // проталкиваем провайдер в базовый хелпер

        public ICustomEmployeeService CustomEmployeeService 
        {    //сервис 1 в хелпер , чтобы его можно было дергать из контроллера
            get { return ServiceUtil.GetService<ICustomEmployeeService>(serviceProvider); }
        }

        public ICustomGroupService CustomGroupService 
        {   //сервис 2 в хелпер , чтобы его можно было дергать из контроллера
            get { return ServiceUtil.GetService<ICustomGroupService>(serviceProvider); }
        }

        public ICustomAllowanceService CustomAllowanceService 
        {  //сервис 3 в хелпер , чтобы его можно было дергать из контроллера
            get { return ServiceUtil.GetService<ICustomAllowanceService>(serviceProvider); }
        }

        public IChangeState GoChangeState 
        {  //сервис 4 в хелпер , чтобы его можно было дергать из контроллера
            get { return ServiceUtil.GetService<IChangeState>(serviceProvider); }
        }

        public ITicketCostService TicketCostService
        { //сервис 4 в хелпер , чтобы его можно было дергать из контроллера
            get { return ServiceUtil.GetService<ITicketCostService>(serviceProvider); }
        }
    }
}
