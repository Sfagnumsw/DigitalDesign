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
