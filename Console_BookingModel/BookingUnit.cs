using BookingModel.ServiceDataModel;

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingModel
{
    internal class BookingUnit: TestingUnit
    {
        public static void RunTest()
        {
            using (var controller = ServiceController.Create())
            {
                controller.Info
                (
                    controller.GetType().GetMethod("CreateBooking").GetParameters().Select(field => field.Name).ToList().ToJsonOnScreen()
                );
                controller.Info
                (
                    controller.GetType().GetInputForm("CreateBooking").FormFields.Select(field => field.Name).ToList().ToJsonOnScreen()
                );


                CreateBookingSlots(controller);
                CreateBookingTest(controller);
                CancelBookingTest(controller);
                GetModelTest(controller);

                /*var all = controller.GetBookings(controller.GetServiceDepartments().First().Id, DateTime.Now.ToString("d"));
                Console.WriteLine("Записей: " + all.Count());
                Console.WriteLine("Записей: " + System.Text.Json.JsonSerializer.Serialize(all));*/
            }

        }




        private static void CreateBookingSlots(ServiceController controller)
        {
            controller.GetAvailableBookingSlots(1, "2024-10-10", new() { 1, 2, 3 }).ToJsonOnScreen().WriteToConsole();
        }

        private static void GetModelTest(ServiceController controller)
        {
            
            var cities = controller.GetCities();
            foreach (var city in cities)
            {
                foreach(var dep in city.ServiceDepartments)
                {
                    dep.PriceList = controller.GetPriceList(dep.Id).Select( price => { price.ServiceDepartment = null; return price; }).ToList();
                }
            }
            controller.Info(cities.ToJsonOnScreen());
        }

        private static ServiceDepartment CancelBookingTest(ServiceController controller)
        {
            var dep = controller.GetServiceDepartments().First();
            var work = controller.GetWorkList(dep.Id).First();
            var customer = controller.GetCustomers().First();
            var booking_id = controller.CreateBooking(dep.Id, DateTime.Now.ToString("d"), "12", new List<int>() { work.Id }, customer.Id);
            controller.CancelBooking(booking_id);
            return dep;
        }
        private static ServiceDepartment CreateBookingTest(ServiceController controller)
        {
            var dep = controller.GetServiceDepartments().First();
            var work = controller.GetWorkList(dep.Id).First();
            var customer = controller.GetCustomers().First();
            var booking_id = controller.CreateBooking(dep.Id, DateTime.Now.ToString("d"), "12", new List<int>() { work.Id }, customer.Id);
            controller.CancelBooking(booking_id);
            return dep;
        }
    }
}

