using BookingModel.ServiceDataModel;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingModel.ServiceServiceModel
{
    /// <summary>
    /// Создаёт слоты согласно расписанию
    /// </summary>
    
    public class BookingSlotsFactory: IDisposable
    {
        private readonly ServiceDbContext serviceDbContext;

        public BookingSlotsFactory(ServiceDbContext serviceDbContext)
        {
            this.serviceDbContext = serviceDbContext;
        }


        [HttpGet()]
        [Route("/api/services/{serviceDepartmentId}/{dateOfWork}")]
        public List<BookingSlot> GetAvailableBookingSlots([FromRoute]int serviceDepartmentId, [FromRoute] string dateOfWork, [FromQuery] List<int> workIds)
        {
            var all = GetOrCreateBookingSlots(serviceDepartmentId, dateOfWork, workIds);
            return all.Where(p => p.CustomerId is null).ToList();
        }

        [NonAction]
        protected List<BookingSlot> GetOrCreateBookingSlots(int serviceDepartmentId, string dateOfWork, List<int> workIds)
        {
            List<BookingSlot> all = new();
            var department = this.serviceDbContext.ServiceDepartments.Include(sd => sd.PriceList).FirstOrDefault(p => p.Id == serviceDepartmentId);
            int start = GetStartOfWork(department, dateOfWork);
            int end = GetEndOfWork(department, dateOfWork);
            foreach(var workId in workIds)
            {
                var price = department.PriceList.FirstOrDefault(price => price.ServiceWorkId == workId);
                if (price is null)
                    continue;

                var work = this.serviceDbContext.ServiceWorks.Find(price.ServiceWorkId);
                var slots = this.serviceDbContext.BookingSlots.Include(slot => slot.ServicePrice).Include(slot => slot.ServicePrice.ServiceDepartment).Where(slot => slot.ServicePrice.ServiceDepartmentId == serviceDepartmentId && slot.WorkDate == dateOfWork && slot.ServicePriceId == price.Id).ToList();
                double i = start;
                while (i < end)
                {

                    if (slots.Any(s => s.WorkTime == $"{i}:00.000Z"))
                    {
                        i += work.WorkTime;
                        continue;
                    }
                    BookingSlot p = null;
                    this.serviceDbContext.BookingSlots.Add(p = new BookingSlot
                    {
                        WorkDate = dateOfWork,
                        ServicePriceId = price.Id,
                        WorkTime = $"{i}:00.000Z"
                    });
                    slots.Add(p);
                    this.serviceDbContext.SaveChanges();
                    i += work.WorkTime;
                }
                all.AddRange(slots);
            }
            
            
            return all;
        }


        [HttpGet()]
        [Route("/api/services/{serviceDepartmentId}/{dateOfWork}/slots")]
        public List<BookingSlot> GetOrCreateBookingSlots( int serviceDepartmentId, string dateOfWork )
        {
            List<BookingSlot> all = new();
            var department = this.serviceDbContext.ServiceDepartments.Include(sd => sd.PriceList).FirstOrDefault(p => p.Id == serviceDepartmentId);
            int start = GetStartOfWork(department, dateOfWork);
            int end = GetEndOfWork(department, dateOfWork);
            int n = 0;
            using(var serviceDbContext = new ServiceDbContext())
            {
                foreach (var price in department.PriceList)
                {
                    n++;
                    Console.WriteLine($"\t\t{n.ToString()}/{department.PriceList.Count()}");
                    var work = serviceDbContext.ServiceWorks.Find(price.ServiceWorkId);
                    var slots = serviceDbContext.BookingSlots.Include(slot => slot.ServicePrice).Include(slot => slot.ServicePrice.ServiceDepartment).Where(slot => slot.ServicePrice.ServiceDepartmentId == serviceDepartmentId && slot.WorkDate == dateOfWork && slot.ServicePriceId == price.Id).ToList();
                    double i = start;
                    while (i < end)
                    {
                        if (slots.Any(s => s.WorkTime == $"{i}:00.000Z"))
                        {
                            i += work.WorkTime;
                            continue;
                        }
                        BookingSlot p = null;
                        serviceDbContext.BookingSlots.Add(p = new BookingSlot
                        {
                            WorkDate = dateOfWork,
                            ServicePriceId = price.Id,
                            WorkTime = $"{i}:00.000Z"
                        });
                        slots.Add(p);
                        serviceDbContext.SaveChanges();
                        i += work.WorkTime;
                    }
                    all.AddRange(slots);
                }
                return all;
            }
            
        }

        private int GetEndOfWork(ServiceDepartment? department, string dateOfWork)
        {
            return 21;
        }

        private int GetStartOfWork(ServiceDepartment? department, string dateOfWork)
        {
            return 9;
        }

        [NonAction]
        public virtual void Dispose()
        {
            this.serviceDbContext.Dispose();
        }
    }
}
