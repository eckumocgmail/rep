using System.ComponentModel.DataAnnotations;

namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    [Label("Сведения по автомобилю клиента")]
    public class CustomerCar: BaseEntity
    {

        [Label("Использовать по-умолчанию")]
        public bool Active { get; set; } = false;

        [Label("Произодитель")]
        [StringLength(32)]
        [MinLength(4)]
        [NotNullNotEmpty()]
        public string Manufacturer { get; set; } = "";

        [Label("Модель")]
        [StringLength(32)]
        [MinLength(4)]
        [NotNullNotEmpty()]
        public string Model { get; set; }

        [Label("Год выпуска")]
        public string Year { get; set; }

        [Label("ВИН")]
        [StringLength(16)]
        [MinLength(16)]
        [NotNullNotEmpty()]
        public string Vin { get; set; }

        [Label("Модель")]
        [StringLength(10)]
        [MinLength(10)]
        [NotNullNotEmpty()]
        public string RegNumber { get; set; }

        [Label("Клиент")]
        [InputDictionary(nameof(Customer) + ",Name")]
        public int CustomerId { get; set; }

        [Label("Клиент")]
        public CustomerContext Customer { get; set; }

    }
}
