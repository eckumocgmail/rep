using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using pickpoint_delivery_service;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using static System.Net.Mime.MediaTypeNames;

namespace Console_BlazorApp.AppUnits.DeliveryControllers
{

    /// <summary>
    /// Процесс оформления заказа
    /// </summary>
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
        public IActionResult Back()
        {
            return Redirect("/");
        }

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
                CustomerID = customer.Id
            };
            order.HolderID = holder_id;
            db.Add(order);
            db.SaveChanges();
            var items = signin.GetFromSession<List<Product>>("selected-products").Select(product => new OrderItem()
            {
                ProductID = product.Id,
                ProductCount = 1,
                OrderID = order.Id
            }).ToList();
            foreach (var item in items)
            {
                db.Add(item);
                db.SaveChanges();
            }            
            return View(order);
        }


        [HttpGet()]
        public IActionResult Submit() 
            => View(signin.GetFromSession<List<Product>>("selected-products"));

        [HttpGet()]
        public IActionResult Checkout() 
            => View(signin.GetFromSession<List<Product>>("selected-products"));

        [HttpGet()]
        [Microsoft.AspNetCore.Mvc.Route("/Order/Checkout/{OrderId}")]
        public IActionResult CheckoutOrder([FromServices] DeliveryDbContext db, [FromRoute] int OrderId)
        {
            var order = db.Orders.Include(o => o.OrderItems).First(o => o.Id == OrderId);
            return View("Checkout", order.OrderItems.Select(item => db.Products.Find(item.ProductID)).ToList());
        }

        [HttpGet]
        public async Task ProductImage([FromServices] DeliveryDbContext db, int image_id)
        {
            var image = db.ProductImages.FirstOrDefault(img => img.Id == image_id);
            image = image == null ? db.ProductImages.First() : image;
            Response.ContentType = image.ContentType;
            byte[] data = image.ImageData;
            await Response.Body.WriteAsync(data, 0, data.Length);

        }

        /*public override SearchModel<Product> GetModel()
        {
            return signin.GetFromSession<SearchModel<Product>>(GetType().GetTypeName());            
        }

        public override IEnumerable<string> GetOptions(string Query)
        {
            var fasade = new EntityFasade<Product>(this._deliveryDbContext);
            return fasade.GetOptions(Query);            
        }*/
    }
}
