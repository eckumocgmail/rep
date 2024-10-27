using BookingModel.ServiceDataModel;
using BookingModel.ServiceServiceModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

[ApiController]
public class ServiceController: BookingSlotsFactory
{
    private readonly ServiceDbContext serviceDbContext;

    public static ServiceController Create() => new ServiceController(new ServiceDbContext());

    public ServiceController(ServiceDbContext serviceDbContext): base(serviceDbContext)
    {
        this.serviceDbContext = serviceDbContext;
    }


    [HttpGet()]
    [Route("/api/services/model")]
    public List<CityGroup> GetModel()
    {
        var controller = this;
        var cities = controller.GetCities();
        foreach (var city in cities)
        {
            foreach (var dep in city.ServiceDepartments)
            {
                dep.PriceList = controller.GetPriceList(dep.Id).Select(price => { price.ServiceDepartment = null; return price; }).ToList();
            }
        }
        return cities;
    }


    [HttpGet()]
    [Route("/api/services/bookings/{phone}")]
    public List<BookingSlot> GetBookingsByPhone([FromRoute]string phone)
    {
        return serviceDbContext.BookingSlots.Include(s => s.Customer).Where(p => p.Customer.Phone == phone ).ToList();
    }


    [HttpGet()]
    [Route("/api/services/{serviceDepartmentId}/bookings/{date}")]
    public List<BookingSlot> GetBookings([FromRoute] int serviceDepartmentId, [FromRoute] string date)
    {
        return GetOrCreateBookingSlots(serviceDepartmentId, date).Where(p => p.ServicePrice.ServiceDepartmentId == serviceDepartmentId && p.WorkDate == date).ToList();
    }


    [HttpDelete()]
    [Route("/api/bookings/{bookingId}")]
    public int CancelBooking(int bookingId)
    {
        var slot = this.serviceDbContext.BookingSlots.FirstOrDefault(item => item.Id == bookingId);
        slot.CustomerId = null;
        slot.CustomerId = null;
        return serviceDbContext.SaveChanges();
    }


    [HttpPost()]
    [Route("/api/services/{serviceDepartmentId}/bookings/{date}/{time}/{clientId}")]
    public int CreateBooking([FromRoute] int serviceDepartmentId, [FromRoute] string date, [FromRoute] string time, [FromQuery]List<int> works, int clientId)
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
        serviceDbContext.SaveChanges();
        return slot.Id;
    }

    [NonAction]
    //[HttpGet()]
    //[Route("/api/services/works/{id}/params/form")]
    public InputFormModel GetAdditionalParamsForm([FromRoute]int id)
    {
        ServiceWork work = serviceDbContext.ServiceWorks.Find(id);
        var model = new InputFormModel(System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(work.JsonParams));
        return model;        
    }


    [HttpGet()]
    [Route("/api/services/works/{id}/params")]
    public Dictionary<string, object> GetAdditionalParams([FromRoute] int id)
    {
        ServiceWork work = serviceDbContext.ServiceWorks.Find(id);
        return System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(work.JsonParams);
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


    [HttpGet()]
    [Route("/api/services/prices/{id}")]
    public ServicePrice GetServicePrice([FromRoute()]int id)
    {
        return this.serviceDbContext.ServicePrices.Include(p => p.ServiceWork).Include(p => p.ServiceDepartment).FirstOrDefault(p => p.Id==id);
    }


    [HttpGet()]
    [Route("/api/services")]
    public List<ServiceDepartment> GetServiceDepartments()
    {
        return this.serviceDbContext.ServiceDepartments.ToList();
    }


    [HttpPost()]
    [Route("/api/services")]
    public int CreateServiceDepartment(int cityId, [Required][StringLength(40)] string ServiceName, [Required][StringLength(255)] string ServiceDescription, [Required] [StringLength(80)] string WorkingHours, [Required][StringLength(11)] string ServicePhone)
    {
        ServiceDepartment p = null;
        this.serviceDbContext.ServiceDepartments.Add(p = new()
        {
            ServiceDescription = ServiceDescription,
            ServiceName = ServiceName,
            ServicePhone = ServicePhone,
            WorkingHours = WorkingHours,
            CityGroupId = cityId
        });
        this.serviceDbContext.SaveChanges();
        return p.Id;
    }


    [HttpPatch()]
    [Route("/api/services/{serviceDepartmentId}")]
    public int SetPriceList([FromRoute]int serviceDepartmentId, [FromQuery] Dictionary<int, double> workPrices)
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


    [HttpGet()]
    [Route("/api/services/{serviceDepartmentId}/prices")]
    public List<ServicePrice> GetPriceList([FromRoute]int serviceDepartmentId)
    {
        return serviceDbContext.ServicePrices.Include(s => s.ServiceDepartment).Include(s => s.ServiceWork).Where(price => price.ServiceDepartmentId == serviceDepartmentId).ToList();
    }


    [HttpGet()]
    [Route("/api/services/{serviceDepartmentId}/works")]
    public List<ServiceWork> GetWorkList(int serviceDepartmentId)
    {
        var works = serviceDbContext.ServicePrices.Include(s => s.ServiceWork).Where(price => price.ServiceDepartmentId == serviceDepartmentId).Select(p => p.ServiceWork).ToList();
        return works;
    }


    [HttpGet()]
    [Route("/api/services/works")]
    public List<ServiceWork> GetWorkList( )
    {
        return serviceDbContext.ServiceWorks.ToList();
    }


    [HttpGet()]
    [Route("/api/services/customers")]
    public List<CustomerInfo> GetCustomers()
    {
        return serviceDbContext.CustomerInfos.ToList();
    }


    [HttpGet()]
    [Route("/api/services/cities")]
    public List<CityGroup> GetCities()
    {
        return serviceDbContext.CityGroups.Include(city => city.ServiceDepartments).ToList();
    }
}