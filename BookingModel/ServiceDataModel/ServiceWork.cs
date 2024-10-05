namespace BookingModel.ServiceDataModel
{
    /// <summary>
    /// Услуга по ремонту авто
    /// </summary>
    public class ServiceWork
    {
        public int Id { get; set; }
        public string WorkName { get; set; }
        public double WorkTime { get; set; }
    }
}