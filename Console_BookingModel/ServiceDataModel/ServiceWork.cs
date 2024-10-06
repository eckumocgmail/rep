namespace BookingModel.ServiceDataModel
{
    /// <summary>
    /// Услуга по ремонту авто
    /// </summary>
    public class ServiceWork: BaseEntity
    {
     
        public string WorkName { get; set; }
        public double WorkTime { get; set; }

        public string JsonParams { get; set; }
    }
}