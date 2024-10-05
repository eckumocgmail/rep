using BookingModel.ServiceDataModel;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingModel
{
    internal class BookingUnit
    {
        public static void Test()
        {
            using (var controller = new ServiceController())
            {
                Console.Clear(); 
                var dep = controller.GetServiceDepartments().First();
                var work = controller.GetWorkList(dep.Id).First();
                var customer = controller.GetCustomers().First();
                controller.CreateBooking(dep.Id, DateTime.Now.ToString("d"), 12, new List<int>() { work.Id }, customer.Id);
               
                var all = controller.GetBookings(dep.Id, DateTime.Now.ToString("d"));
                Console.WriteLine("Записей: "+ all.Count());
                Console.WriteLine("Записей: "+ System.Text.Json.JsonSerializer.Serialize(all));
            }
            
        }
    }
}

