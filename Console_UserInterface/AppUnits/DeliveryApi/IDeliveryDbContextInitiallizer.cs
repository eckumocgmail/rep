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

namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface IdeliveryDbContextInitiallizer
    {

        /// <summary>
        /// Возвращает статистику типа ключ-значение
        /// ключ-наименование сущности
        /// ключ-кол-во выполненных операций
        /// </summary>
        /// <param name="context">Контекст данных с проверенной структурой</param>
        /// <param name="contentPath">Путь к директории ресурсов</param>
        /// <returns></returns>
        public IDictionary<string, int> Init(DeliveryDbContext context, string contentPath);
        public int InitCustomers(DeliveryDbContext context, int customersCount);
        public int InitHolders(DeliveryDbContext context);
        public int InitProducts(DeliveryDbContext context, string contentPath);
    }

}
