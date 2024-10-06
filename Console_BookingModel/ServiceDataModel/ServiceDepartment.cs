using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingModel.ServiceDataModel
{
    /// <summary>
    /// Отделение автосервиса
    /// </summary>
    public class ServiceDepartment: BaseEntity
    {
      

        [Required]
        [StringLength(40)]
        public string ServiceName { get; set; }

        [Required]
        [StringLength(255)]
        public string ServiceDescription { get; set; }

        [Required]
        [StringLength(80)]
        public string WorkingHours { get; set; }

        [Required]
        [StringLength(11)]
        public string ServicePhone { get; set; }


        [Required]
        public List<ServicePrice> PriceList { get; set; } = new();
    }
}
