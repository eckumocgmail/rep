using AngleSharp.Dom;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.EntityFrameworkCore;

using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    public class HolderProductsFasade : EntityFasade<Product>
    {
        private readonly Holder _holder;

        public HolderProductsFasade(DeliveryDbContext context, Holder holder) : base(context)
        {
            _holder = holder;
        }
        protected override IQueryable<Product> GetDbSet() => _context.Set<Product>().Where(product => ((DeliveryDbContext)_context).ProductsInStock.Where(instock => instock.HolderId == _holder.Id).Select(instock => instock.ProductId).Contains(product.Id));

    }
}
