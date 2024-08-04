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

namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    /// <summary>
    /// Свеедния о продукции имеющейся в наличии на складе
    /// </summary>
    public class ProductsInStock : BaseEntity
    {

        public int HolderID { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
        public Holder Holder { get; set; }
        public int ProductCount { get; set; }

        /// <summary>
        /// Допустимое количество
        /// </summary>
        public int StoreSize { get; set; } = 0;
        public int ProductsInReserve { get; set; } = 0;
    }



   

}
