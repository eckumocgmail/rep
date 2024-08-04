using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Mvc_Apteka.Entities
{
    /// <summary>
    /// Группировка продукции ( 1 ко многим )   
    /// </summary>
    [Index(nameof(ProductCatalogName))]
    public class ProductCatalog
    {
        
        [Key]
        public int ID { get; set; }
 
        public int? ParentId { get; set; }
        public ProductCatalog Parent { get; set; }

        [Required]
        public string ProductCatalogName { get; set; }
        public virtual ICollection<ProductInfo> Products { get; set; }

        public ProductCatalog()
        {
            this.Products = new List<ProductInfo>();
        }
    }
}
