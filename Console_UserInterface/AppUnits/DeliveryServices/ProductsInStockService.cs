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
            var products = new Dictionary<int, int>(order.OrderItems.Select(item => new KeyValuePair<int,int>(item.ProductID,item.ProductCount)));
            List<int> results = new List<int>();
            var instock = _deliveryDbContext.ProductsInStock.Where(instock =>
                products.Keys.Contains(instock.ProductID) &&
                order.OrderItems.FirstOrDefault(item => item.ProductID == instock.ProductID).ProductCount <= instock.ProductCount
            ).ToList();
            foreach(var id in instock.Select(p => p.HolderID).ToHashSet())
            {
                var store = instock.Where(p => p.HolderID == id);
                foreach(var item in order.OrderItems)
                {
                    var p = store.FirstOrDefault(s => s.ProductID == item.ProductID);
                    if ((p is not null && item.ProductCount<= p.ProductCount))
                    {
                        results.Add(id);
                    }
                }
            }


            /*foreach (var holder in _deliveryDbContext.Holders.ToList())           
            {
                var holderId = holder.Id;
                if(order.OrderItems.Any(item => _deliveryDbContext.ProductsInStock.Where(p => p.HolderID == holderId && item.ProductID == p.ProductID && item.ProductCount <= p.ProductCount).Count() != 1)==false)
                {
                    results.Add(holderId);
                }
            }*/
            return results;            
        }

        public IEnumerable<Holder> GetReservationStocks(int orderId)
        {
            var ids = GetReservationStocksIds(orderId);
            return _deliveryDbContext.Holders.Where(holder => ids.Contains(holder.Id)).ToList();
        }

        public bool HasProducts(int holderId, Dictionary<int, int> products)
        {
            var ids = products.Keys.ToList();
            var stock = _deliveryDbContext.ProductsInStock.Where(p => p.HolderID == holderId && ids.Contains(p.ProductID)).ToList();
            if (stock.Count() != products.Count())
            {
                return false;
            }
            else
            {
                Dictionary<int, int> stored = new Dictionary<int, int>(stock.Select(p => new KeyValuePair<int, int>(p.ProductID, p.ProductCount)).ToList());
                foreach(var kv in products)
                {
                    if( stored[kv.Key]<kv.Value )
                    {
                        return false;
                    }
                }
                return true;
            }
        }
    }
}
