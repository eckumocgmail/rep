using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.EntityFrameworkCore;

using pickpoint_delivery_service;

using System.Reflection;

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
            try
            {
                var order = _deliveryDbContext.Orders.Include(order => order.OrderItems).FirstOrDefault(order => order.Id == orderId);
                if (order is null)
                    throw new ArgumentException("orderId");
                var products = new Dictionary<int, int>(order.OrderItems.Select(item => new KeyValuePair<int, int>(item.ProductId, item.ProductCount)));
                List<int> results = new List<int>();
                var instock = _deliveryDbContext.ProductsInStock.Where(instock =>
                    _deliveryDbContext.Orders.Include(order => order.OrderItems).FirstOrDefault(order => order.Id == orderId).OrderItems.Select(item => item.ProductId).Contains(instock.ProductId)
                //products.Keys.Contains(instock.ProductId) &&
                //order.OrderItems.FirstOrDefault(item => item.ProductId == instock.ProductId).ProductCount <= instock.ProductCount
                ).ToList();
                foreach (var id in instock.Select(p => p.HolderId).ToHashSet())
                {
                    var store = instock.Where(p => p.HolderId == id);
                    foreach (var item in order.OrderItems)
                    {
                        var p = store.FirstOrDefault(s => s.ProductId == item.ProductId);
                        if ((p is not null && item.ProductCount <= p.ProductCount))
                        {
                            results.Add(id);
                        }
                    }
                }


                /*foreach (var holder in _deliveryDbContext.Holders.ToList())           
                {
                    var holderId = holder.Id;
                    if(order.OrderItems.Any(item => _deliveryDbContext.ProductsInStock.Where(p => p.HolderId == holderId && item.ProductId == p.ProductId && item.ProductCount <= p.ProductCount).Count() != 1)==false)
                    {
                        results.Add(holderId);
                    }
                }*/
                return results;
            }           
            catch(Exception ex)
            {
                this.Error(MethodInfo.GetCurrentMethod().Name + " "+ex.Message);
                throw;
            }
        }

        public IEnumerable<Holder> GetReservationStocks(int orderId)
        {
            var ids = GetReservationStocksIds(orderId);
            return _deliveryDbContext.Holders.Where(holder => ids.Contains(holder.Id)).ToList();
        }

        public bool HasProducts(int holderId, Dictionary<int, int> products)
        {
            var ids = products.Keys.ToList();
            var stock = _deliveryDbContext.ProductsInStock.Where(p => p.HolderId == holderId && ids.Contains(p.ProductId)).ToList();
            if (stock.Count() != products.Count())
            {
                return false;
            }
            else
            {
                Dictionary<int, int> stored = new Dictionary<int, int>(stock.Select(p => new KeyValuePair<int, int>(p.ProductId, p.ProductCount)).ToList());
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
