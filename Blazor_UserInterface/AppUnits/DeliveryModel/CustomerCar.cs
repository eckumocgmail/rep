namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    public class CustomerCar
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public CustomerContext Customer { get; set; }
        public bool Active { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string Vin { get; set; }
        public string RegNumber { get; set; }
    }
}
