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
using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface IOrdersService
    {
        public Order GetOrder(int id);
        public IEnumerable<Order> GetOrders();
        public IEnumerable<OrderItem> GetOrderItems(int orderId);
        public void CancellOrder(int orderId);
        public void CompleteOrder(int orderId);
    }

}
