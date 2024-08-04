using Console_BlazorApp.AppUnits.DeliveryModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc_Apteka.Entities
{
    /// <summary>
    /// Сведения о продукции на складе
    /// </summary>
    public class ProductInfo: BaseEntity
    {
    
        public int? ProductCatalogID { get; set; }

        [Display(Name = "Наименование продукции")]
        [Required(ErrorMessage = "Необходимо указать наименование продукции")]
        public string ProductName { get; set; }
        [Display( Name = "Наименование продукции") ]
        [Required( ErrorMessage = "Необходимо указать наименование продукции")]
        public string ProductIndicatorsJson { get; set; }
        
        [Display(Name = "В наличии на складе")]
        [Required(ErrorMessage = "Необходимо указать кол-во продукции на складе")]
        public int ProductCount { get; set; }

        [Display(Name = "Цена за ед.")]
        [Required(ErrorMessage = "Необходимо указать цену за ед. продукции в рублях")]
        public float ProductPrice { get; set; }


        public ICollection<ProductImage> ProductImages { get; set; }

    }
}
