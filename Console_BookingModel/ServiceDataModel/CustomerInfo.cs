namespace BookingModel.ServiceDataModel
{
    public class CustomerInfo : BaseEntity
    {
        public int Id { get; set; }
        public string Phone { get; internal set; }
    }
}