using DocsVision.Platform.WebClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkTripServerSE.Models;

namespace WorkTripServerSE.Service
{
    //смена состояния
    public class ChangeState : IChangeState
    {
        private readonly IServiceProvider _serviceProvider;
        public ChangeState(IServiceProvider serviceProvider) {

            _serviceProvider = serviceProvider;
        }

        public CustomChangeStateData GoChangeState(SessionContext context) {

            QueryObject query = new QueryObject(StatesOperation.DefaultNameProperty.Name, "OnApproval");
            CustomChangeStateData model = new CustomChangeStateData();
            StatesOperation SO = context.ObjectContext.FindObject<StatesOperation>(query);
            model.NeedState = SO.GetObjectId().ToString();
            return model; 
        }
    }
}



namespace WorkTripServerSE.Service
{
    //сумма командировочных
    public class CustomAllowanceService : ICustomAllowanceService
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomAllowanceService(IServiceProvider serviceProvider) { _serviceProvider = serviceProvider; }
        public CustomAllowanceData GetAllowanceData(SessionContext context, Guid cityId)
        {
            BaseUniversalItem baseUniversal = context.ObjectContext.GetObject<BaseUniversalItem>(cityId);
            var mainInfo = baseUniversal.ItemCard.GetSection(new Guid("{F5641A7E-83AF-4C20-9C60-EA2973C4F135}"));
            BaseCardSectionRow firstRow = (BaseCardSectionRow)mainInfo[0];
            CustomAllowanceData model = new CustomAllowanceData();
            model.DailyAllowance = decimal.Parse(firstRow["Money"].ToString(), CultureInfo.InvariantCulture);
            return model;    
        }
    }
}


namespace WorkTripServerSE.Service
{ 
    //Получение данных сотрудника
    public class CustomEmployeeService : ICustomEmployeeService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceHelper _serviceHelper;

        public CustomEmployeeService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _serviceHelper = new ServiceHelper(_serviceProvider);
        }
        public CustomEmployeeData GetEmployeeData(SessionContext context, Guid employeeId)
        {
            StaffEmployee employee = context.ObjectContext.GetObject<StaffEmployee>(employeeId);
            
            if (employee == null) 
            { return null; }

            CustomEmployeeData model = new CustomEmployeeData();
            model.Phone = employee.Phone;
            StaffEmployee manager;

            if (employee.Manager != null) 
            { manager = employee.Manager; }

            else { manager = employee.Unit.Manager; }

            if (manager != null) 
            {  model.Manager = _serviceHelper.EmployeeService.GetEmployee(context, manager.GetObjectId());}

            return model;
        }
    }
}



namespace WorkTripServerSE.Service
{
    //сотрудники из группы
    public class CustomGroupService : ICustomGroupService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ServiceHelper _serviceHelper;

        public CustomGroupService(IServiceProvider serviceProvider) {

            _serviceProvider = serviceProvider;
            _serviceHelper = new ServiceHelper(_serviceProvider);
        }
        public CustomGroupData GetGroupData(SessionContext context)
        {
            QueryObject queryObject = new QueryObject(StaffGroup.NameProperty.Name, "Секретарь");
            StaffGroup group = context.ObjectContext.FindObject<StaffGroup>(queryObject);
            CustomGroupData model = new CustomGroupData();
            Guid[] EmplID = group.EmployeesIds.ToArray();
            model.GroupEmployees = new EmployeeModel[EmplID.Length];
            for (int i = 0; i < EmplID.Length; i++)
            {
                model.GroupEmployees[i] = _serviceHelper.EmployeeService.GetEmployee(context, EmplID[i]);
            }
            
            return model;
        }
    }
}



namespace WorkTripServerSE.Service
{
    public class TicketCostService : ITicketCostService
    {
        private readonly IServiceProvider _serviceProvider;
        public TicketCostService(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }
        public CustomTicketData GetTicketCost(SessionContext context, Guid cityID, DateTime d1, DateTime d2) // получение цен на билеты и их суммы
        {
            BaseUniversalItem CityData = context.ObjectContext.GetObject<BaseUniversalItem>(cityID);
            var CityDataDest = CityData.ItemCard.GetSection(new Guid("{F5641A7E-83AF-4C20-9C60-EA2973C4F135}"));
            BaseCardSectionRow FirstRow = (BaseCardSectionRow)CityDataDest[0];
            string _Destination = FirstRow["AirID"].ToString();
            CustomTicketData model = new CustomTicketData();

            DateTime Date1 = (DateTime)d1;
            DateTime Date2 = (DateTime)d2;
            ExtensionMethod method = context.Session.ExtensionManager.GetExtensionMethod("TicketCost", "GetInforationTickets");
            method.Parameters.AddNew("desID", ParameterValueType.String, _Destination);
            method.Parameters.AddNew("d1", ParameterValueType.DateTime, Date1);
            method.Parameters.AddNew("d2", ParameterValueType.DateTime, Date2);
            var result = method.Execute();
            model.TotalValue = Decimal.Parse(result.ToString(), CultureInfo.InvariantCulture);
            return model;
        }
    }
}
