using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTripServerSE.Models
{
    //командировочные
    public class CustomAllowanceData
    {
        public decimal DailyAllowance { get; set; }
    }
}


using DocsVision.BackOffice.ObjectModel;
using DocsVision.BackOffice.WebClient.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTripServerSE.Models
{
    //модель полученного состояния
    public class CustomChangeStateData
    {
        public string NeedState;
    }
}


using DocsVision.BackOffice.WebClient.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTripServerSE.Models
{
    public class CustomEmployeeData
    { 
        // модель данных, которые нам нужны от сотрудника
        public string Phone { get; set; }
        public EmployeeModel Manager { get; set; }
    }
}


using DocsVision.BackOffice.WebClient.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTripServerSE.Models
{ 
    //модель данных работников группы
    public class CustomGroupData
    {
        public EmployeeModel[] GroupEmployees { get; set; }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkTripServerSE.Models
{ // стоимость билетов
    public class CustomTicketData
    {
        public decimal TotalValue { get; set; }
    }
}


namespace WorkTripServerSE.Models
{ //модель данных для билетов
    public class Tickets
    {
        public string Destination { get; set; }
        public decimal Value { get; set; }
    }
}
