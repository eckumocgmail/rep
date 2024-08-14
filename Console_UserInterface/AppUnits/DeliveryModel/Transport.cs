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
    /// Тот кто осуществляет транспортировку,
    /// напр. сотрудник курьерской службы
    /// </summary>
    public class Transport : BaseEntity
    {
        [NotNullNotEmpty]
        [Label("Широта")]
        public float Latitude { get; set; }

        [NotNullNotEmpty]
        [Label("Долгота")]
        public float Longitude { get; set; }

        [NotNullNotEmpty]
        [Label("Пользователь")]
        public int UserId { get; set; }
    }

}
