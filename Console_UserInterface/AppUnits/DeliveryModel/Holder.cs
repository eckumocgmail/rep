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
using pickpoint_delivery_service;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Builder;

namespace Console_BlazorApp.AppUnits.DeliveryModel
{

    /// <summary>
    /// Постамат/Устройтво хранения
    /// </summary>
    public class Holder : BaseEntity
    {


        /// <summary>
        /// Номер постомата
        /// </summary>
        [SerialNumber("XXXX-XXX")]
        public string HolderSerial { get; set; }

        public bool HolderIsActive { get; set; } = false;
        public string HolderLocation { get; set; } = "";
        public string HolderToken { get; set; } = "";

        public int UserId { get; set; } // ид-пользователя, хозяина склада

        [JsonIgnore]
        public List<Order> HolderOrders { get; set; }

        [JsonIgnore]
        public List<ProductsInStock> ProductsInStock { get; set; }
    }
}
