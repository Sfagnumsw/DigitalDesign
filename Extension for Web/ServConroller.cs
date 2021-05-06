using DocsVision.Platform.WebClient;
using DocsVision.BackOffice.WebClient.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkTripServerSE.Halper;
using ServiceHelper = WorkTripServerSE.Halper.ServiceHelper;
using WorkTripServerSE.Models;
using DocsVision.Platform.WebClient.Models.Generic;

namespace WorkTripServerSE.Controllers
{
    /// <summary>
    /// Первый контроллер
    /// </summary>
    public class OneController : Controller
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceHelper _serviceHelper;
        public OneController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceHelper = new ServiceHelper(_serviceProvider);
        }

        // тестовый 
        public ActionResult Test()
        {
            return Content(DocsVision.Platform.WebClient.Helpers.JsonHelper.SerializeToJson("Привет"), "application/json");
        }

        // данные сотрудника
        public ActionResult GetEmployeeData(Guid employeeId)
        {
            SessionContext context = _serviceHelper.CurrentObjectContextProvider.GetOrCreateCurrentSessionContext();
            CustomEmployeeData model = _serviceHelper.CustomEmployeeService.GetEmployeeData(context, employeeId);
            CommonResponse<CustomEmployeeData> response = new CommonResponse<CustomEmployeeData>();
            response.InitializeSuccess(model);
            return Content(DocsVision.Platform.WebClient.Helpers.JsonHelper.SerializeToJson(response), "application/json");
        }

        // данные группы сотрудников
        public ActionResult GetGroupData()
        {
            SessionContext context = _serviceHelper.CurrentObjectContextProvider.GetOrCreateCurrentSessionContext();
            CustomGroupData model = _serviceHelper.CustomGroupService.GetGroupData(context);
            CommonResponse<CustomGroupData> response = new CommonResponse<CustomGroupData>();
            response.InitializeSuccess(model);
            return Content(DocsVision.Platform.WebClient.Helpers.JsonHelper.SerializeToJson(response), "application/json");
        }

        // командировочные
        public ActionResult GetAllowanceData(Guid cityId)
        {
            SessionContext context = _serviceHelper.CurrentObjectContextProvider.GetOrCreateCurrentSessionContext();
            CustomAllowanceData model = _serviceHelper.CustomAllowanceService.GetAllowanceData(context, cityId);
            CommonResponse<CustomAllowanceData> response = new CommonResponse<CustomAllowanceData>();
            response.InitializeSuccess(model);
            return Content(DocsVision.Platform.WebClient.Helpers.JsonHelper.SerializeToJson(response), "application/json");
        }

        // смена состояния(на согласование)
        public ActionResult ChangeState()
        {
            SessionContext context = _serviceHelper.CurrentObjectContextProvider.GetOrCreateCurrentSessionContext();
            CustomChangeStateData model = _serviceHelper.GoChangeState.GoChangeState(context);
            CommonResponse<CustomChangeStateData> response = new CommonResponse<CustomChangeStateData>();
            response.InitializeSuccess(model);
            return Content(DocsVision.Platform.WebClient.Helpers.JsonHelper.SerializeToJson(response), "application/json");
        }

        // получение стоимости билетов
        public ActionResult GetTicketCost(Guid cityID, DateTime d1, DateTime d2) 
        {
            SessionContext context = _serviceHelper.CurrentObjectContextProvider.GetOrCreateCurrentSessionContext();
            CustomTicketData model = _serviceHelper.TicketCostService.GetTicketCost(context, cityID, d1, d2);
            CommonResponse<CustomTicketData> response = new CommonResponse<CustomTicketData>();
            response.InitializeSuccess(model);
            return Content(DocsVision.Platform.WebClient.Helpers.JsonHelper.SerializeToJson(response), "application/json");
        }
    }
}
