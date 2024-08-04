using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static System.Console;
using static Newtonsoft.Json.JsonConvert;
using Newtonsoft.Json.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using Console_AuthModel;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;
using System.ComponentModel.DataAnnotations.Schema;
using Mvc_Apteka.Entities;

namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    public class Product : BaseEntity
    {

        public int? ProductCatalogId { get; set; }

        [JsonIgnore]
        public ProductCatalog ProductCatalog { get; set; }
        
        public bool IsNew { get; set; }

        [NotNullNotEmpty]
        public float? ProductCost { get; set; } = 0;
        public float ProductRate { get; set; } = 0;

        [NotNullNotEmpty]
        [InputRusText()]        
        public string ProductName { get; set; }

        [Newtonsoft.Json.JsonIgnore()]
        [InputRusText()]
        public string ProductDesc { get; set; }

        [Newtonsoft.Json.JsonIgnore()]
        public string ProductIndicatorsJson { get; set; } = "{}";

        [Newtonsoft.Json.JsonIgnore()]
        [NotMapped]
        public int ImageIndex { get; set; } = 0;
        public int IncImageIndex()
        {
            ImageIndex++;
            if(ProductImages is not null && ProductImages.Count() ==  ImageIndex)
                ImageIndex = 0;
            return ImageIndex;
        }





        public long NumberOfSales { get; set; } = 0;

        [JsonIgnore]
        public IList<ProductComment> ProductComments { get; set; } = new List<ProductComment>();

        [JsonIgnore]
        public List<ProductImage> ProductImages { get; set; }
    }

}
