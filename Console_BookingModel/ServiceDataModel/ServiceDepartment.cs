using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookingModel.ServiceDataModel
{
    
    public class CityGroup : BaseEntity
    {
        [Required]
        [StringLength(40)]
        public string CityName { get; set; }

        public List<ServiceDepartment> ServiceDepartments { get; set; }
    }


    /// <summary>
    /// Отделение автосервиса
    /// </summary>
    public class ServiceDepartment: BaseEntity
    {
      
        public int CityGroupId { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        [JsonIgnore]
        public CityGroup CityGroup { get; set; }

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
