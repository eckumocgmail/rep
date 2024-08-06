using Microsoft.AspNetCore.Mvc;

using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryControllers
{
    [ApiController]
    public class OrderController : Controller
    {
        [HttpGet]
        [Route("/Order/Remove/{OrderId}")]
        public IActionResult Remove([FromServices] DeliveryDbContext db, [FromRoute] int OrderId, [FromQuery] string return_url)
        {
            db.Orders.Remove(db.Orders.Find(OrderId));
            db.SaveChanges();
            return Redirect(return_url);

        }
    }
}
