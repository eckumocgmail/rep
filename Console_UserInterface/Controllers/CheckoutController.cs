using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pickpoint_delivery_service;

using System.ComponentModel.DataAnnotations;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Console_BlazorApp.AppUnits.DeliveryControllers
{

    /// <summary>
    /// Процесс оформления заказа
    /// </summary>
    /// /api/Checkout/SessionImage?key=
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("/api/[controller]/[action]")]
    public class CheckoutController : Controller

         
    {
        private readonly SigninUser signin;


        public CheckoutController(SigninUser signin) //: base(deliveryDbContext, env, products, images)
        {
            this.signin = signin;
        }

        [HttpGet()]
        public IActionResult Submit()
            => View(signin.GetFromSession<List<Product>>("selected-products"));

        [HttpGet()]
        public IActionResult Checkout()
            => View(signin.GetFromSession<List<Product>>("selected-products"));

        [HttpGet()]
        public IActionResult Back()
            => Redirect("/");


        [HttpGet()]
        [Microsoft.AspNetCore.Mvc.Route("[controller]/[action]/{holder_id:int}")]
        public IActionResult Complete([FromServices] SigninUser signin, [FromServices] DeliveryDbContext db, [FromRoute] int holder_id)
        {
            var user = signin.Verify();
            var customer = new CustomerContext()
            {
                FirstName = user.Id.ToString(),
                LastName = "Kest",
                PhoneNumber = "79043341124"
            };
            db.Add(customer);
            db.SaveChanges();
            var order = new Order()
            {
                CustomerId = customer.Id
            };
            order.HolderId = holder_id;
            db.Add(order);
            db.SaveChanges();
            var items = signin.GetFromSession<List<Product>>("selected-products").Select(product => new OrderItem()
            {
                ProductId = product.Id,
                ProductCount = 1,
                OrderId = order.Id
            }).ToList();
            foreach (var item in items)
            {
                db.Add(item);
                db.SaveChanges();
            }            
            return View(order);
        }
        

        [HttpGet()]
        [Microsoft.AspNetCore.Mvc.Route("/Order/Checkout/{OrderId}")]
        public IActionResult CheckoutOrder([FromServices] DeliveryDbContext db, [FromRoute] int OrderId)
        {
            var order = db.Orders.Include(o => o.OrderItems).First(o => o.Id == OrderId);
            return View("Checkout", order.OrderItems.Select(item => db.Products.Find(item.ProductId)).ToList());
        }

        [HttpGet]
        public async Task ProductImage([FromServices] DeliveryDbContext db, int image_id)
        {
            var image = db.ProductImages.FirstOrDefault(img => img.Id == image_id);
            image = image == null ? db.ProductImages.First() : image;
            Response.ContentType = image.ContentType;
            byte[] data = image.ImageData;
            Response.ContentType = "image/json";
            await Response.Body.WriteAsync(data, 0, data.Length);
        }

        [HttpGet]
        public async Task SessionImage([FromServices] Console_UserInterface.Services.ISessionService sessionServices, [FromQuery]string id)
        {
            
            byte[] value = sessionServices.GetValue<byte[]>(id);
            await Response.Body.WriteAsync(value, 0, value.Length);
        }
        public SearchModel<Product> GetModel()
        {
            return signin.GetFromSession<SearchModel<Product>>(GetType().GetTypeName());            
        }

        public IEnumerable<string> GetOptions([FromServices] DeliveryDbContext _deliveryDbContext, string Query)
        {
            var fasade = new EfEntityFasade<Product>(_deliveryDbContext);
            return fasade.GetOptions(Query);            
        }
    }
}
