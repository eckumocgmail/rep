using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingModel.ServiceDataModel
{
    /// <summary>
    /// Слот для записи
    /// </summary>
    public class BookingSlot
    {
        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public int ServicePriceId { get; set; }
        [JsonIgnore]
        public ServicePrice ServicePrice { get; set; }
        [JsonIgnore]
        public CustomerInfo Customer { get; set; }

        [JsonIgnore]
        [NotMapped]
        public List<ServicePrice> ServicePrices { get; set; } = new();
        public string WorkDate { get; set; }
        public double WorkTime { get; set; }

        
    }
}
