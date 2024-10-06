using BookingModel.ServiceDataModel;
using BookingModel.ServiceServiceModel;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static System.Runtime.InteropServices.JavaScript.JSType;

public class ServiceController: BookingSlotsFactory
{
    private readonly ServiceDbContext serviceDbContext;

    public ServiceController() : this(new ServiceDbContext()) { }
    public ServiceController(ServiceDbContext serviceDbContext): base(serviceDbContext)
    {
        this.serviceDbContext = serviceDbContext;
    }
    public List<BookingSlot> GetBookings(int serviceDepartmentId, string date)
    {
        return GetOrCreateBookingSlots(serviceDepartmentId, date).Where(p => p.ServicePrice.ServiceDepartmentId == serviceDepartmentId && p.WorkDate == date ).ToList();
    }

    public int CreateBooking(int serviceDepartmentId, string date, double time, List<int> works, int clientId)
    {
        var slot = GetOrCreateBookingSlots(serviceDepartmentId, date, works).FirstOrDefault(p => p.ServicePrice.ServiceDepartmentId == serviceDepartmentId && p.WorkDate==date && p.WorkTime == $"{time}:00.000Z" );
        if (slot == null)
            throw new ArgumentException("не найден слот");
        if(slot.CustomerId != null)
        {
            throw new Exception("Время занято");
        }
        slot.CustomerId = clientId;
        slot.ServicePrices = GetPriceList(serviceDepartmentId).Where(price => price.ServiceWorkId == clientId).ToList();
        serviceDbContext.Update(slot);
        return serviceDbContext.SaveChanges();
    }

    public InputFormModel GetAdditionalParams(ServiceWork work)
    {
        var model = new InputFormModel(System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(work.JsonParams));

        return model;
        
    }

    private object CreateFormControl(string key, object value)
    {
        
        switch (value.GetType().Name)
        {
            case nameof(System.Int32): return new InputControl(InputTypes.Number);
            case nameof(System.Boolean): return new CheckboxModel();
            case nameof(System.String): return new InputControl(InputTypes.Text);
            case nameof(System.DateTime): return new InputControl(InputTypes.DateTime);
            case nameof(System.Double): return new InputControl(InputTypes.Number);
            default: throw new NotImplementedException(value.GetType().Name);
       }        
    }

    public ServicePrice GetServicePrice(int id)
    {
        return this.serviceDbContext.ServicePrices.Include(p => p.ServiceWork).Include(p => p.ServiceDepartment).FirstOrDefault(p => p.Id==id);
    }

    public List<ServiceDepartment> GetServiceDepartments()
    {
        return this.serviceDbContext.ServiceDepartments.ToList();
    }

    public int CreateServiceDepartment([Required][StringLength(40)] string ServiceName, [Required][StringLength(255)] string ServiceDescription, [Required] [StringLength(80)] string WorkingHours, [Required][StringLength(11)] string ServicePhone)
    {
        ServiceDepartment p = null;
        this.serviceDbContext.ServiceDepartments.Add(p = new()
        {
            ServiceDescription = ServiceDescription,
            ServiceName = ServiceName,
            ServicePhone = ServicePhone,
            WorkingHours = WorkingHours,
        });
        this.serviceDbContext.SaveChanges();
        return p.Id;
    }

    public int SetPriceList(int serviceDepartmentId, Dictionary<int, double> workPrices)
    {
        GetPriceList(serviceDepartmentId).ForEach(item => serviceDbContext.Remove<ServicePrice>(item));
        serviceDbContext.SaveChanges();
        foreach (var kv in workPrices)
        {
            serviceDbContext.ServicePrices.Add(new ServicePrice
            {
                ServiceDepartmentId = serviceDepartmentId,                  
                ServiceWorkId = kv.Key,
                WorkPrice = kv.Value
            });
            serviceDbContext.SaveChanges();
        }
        return workPrices.Count();
    }

    public List<ServicePrice> GetPriceList(int serviceDepartmentId)
    {
        return serviceDbContext.ServicePrices.Include(s => s.ServiceDepartment).Include(s => s.ServiceWork).Where(price => price.ServiceDepartmentId == serviceDepartmentId).ToList();
    }

    public List<ServiceWork> GetWorkList(int serviceDepartmentId)
    {
        return serviceDbContext.ServicePrices.Where(price => price.ServiceDepartmentId == serviceDepartmentId).Select(p => p.ServiceWork).ToList();
    }
    public List<ServiceWork> GetWorkList( )
    {
        return serviceDbContext.ServiceWorks.ToList();
    }

    public List<CustomerInfo> GetCustomers()
    {
        return serviceDbContext.CustomerInfos.ToList();
    }
}