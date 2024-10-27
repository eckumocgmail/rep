using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingModel.ServiceDataModel
{
    /// <summary>
    /// Услуга предоставляемая автосервисом
    /// </summary>
    public class ServicePrice: BaseEntity
    {
  
        [Required]
        public int ServiceDepartmentId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public ServiceDepartment ServiceDepartment { get; set; }

        [Required]
        public int ServiceWorkId { get; set; }
        public ServiceWork ServiceWork { get; set; }
        public double WorkPrice { get; set; }

        public List<BookingSlot> BookingSlots { get; set; }
    }
}
