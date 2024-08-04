using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.EntityFrameworkCore;

using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    /// <summary>
    /// 
    /// </summary>
    public class ProductsInStockService: IProductsInStockService
    {
        private readonly DeliveryDbContext _deliveryDbContext;

        public ProductsInStockService(DeliveryDbContext deliveryDbContext)
        {
            _deliveryDbContext = deliveryDbContext;
        }

        public IEnumerable<int> GetReservationStocksIds(int orderId)
        {
            var order = _deliveryDbContext.Orders.Include(order => order.OrderItems).FirstOrDefault(order => order.Id == orderId);
            if (order is null)
                throw new ArgumentException("orderId");
            var counts = new Dictionary<int, int>();
            order.OrderItems.ToList().ForEach(item => counts[item.ProductID]=item.ProductCount );

            var res = new List<int>();
            var stocks = _deliveryDbContext.ProductsInStock.Where(instock => counts.Keys.ToList().Contains(instock.ProductID) && instock.ProductCount >= counts[instock.ProductID]).ToList();
            foreach(var p in stocks.Select(p => p.HolderID).ToList())
            {
                var products = stocks.Where(instock => instock.HolderID == p).Select(instock => instock.ProductID).ToHashSet();
                var ex = products.Except(counts.Keys.ToHashSet());
                if(counts.Keys.ToHashSet().All(k => products.Contains(k)))
                {
                    res.Add(p);
                }
            }


            return res;            
        }

        public IEnumerable<Holder> GetReservationStocks(int orderId)
        {
            var ids = GetReservationStocksIds(orderId);
            return _deliveryDbContext.Holders.Where(holder => ids.Contains(holder.Id)).ToList();
        }
    }
}
