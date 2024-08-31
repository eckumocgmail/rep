using Console_AuthModel;
using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;

using Microsoft.EntityFrameworkCore;

using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    public class HolderService : IHolderService
    {
        private readonly DeliveryDbContext _deliveryDbContext;
        private readonly DbContextUser _userDbContext;

        public HolderService(DeliveryDbContext deliveryDbContext, DbContextUser userDbContext)
        {
            _deliveryDbContext = deliveryDbContext;
            _userDbContext = userDbContext;
        }

        /// <summary>
        /// Получить список товаров на складе
        /// </summary>
        /// <param name="holderId"></param>
        /// <returns></returns>
        public List<Product> GetProducts(int holderId)
        {
            var holdersInstock = _deliveryDbContext.ProductsInStock.Where(instock => instock.HolderId == holderId);
            var products = holdersInstock.Select(instock => _deliveryDbContext.Products.FirstOrDefault(product => product.Id == instock.ProductId)).ToList();
            return products;
        }

        /// <summary>
        /// Получение списка необходимых товаров для закупки
        /// </summary>
        /// <param name="targetProductsCount">кол-во товара которое должно иметься в наличие всего</param>
        /// <returns>кол-во которого не хватает</returns>
        public Dictionary<int, int> CreateOrder(int holderId)
        {
            var kvlist = _deliveryDbContext.ProductsInStock.Where(instock => instock.HolderId == holderId).Select(instock => new KeyValuePair<int, int>(instock.ProductId, instock.StoreSize)).ToList();
            return new Dictionary<int, int>(kvlist);
        }
        public Dictionary<int, int> CreateOrder(int holderId, Dictionary<int, int> targetProductsCount)
        {
            Dictionary<int, int> result = new();
            var currentProductsCount = GetProductsCounts(holderId);
            foreach (var productId in targetProductsCount.Keys.ToHashSet().Except(currentProductsCount.Keys.ToHashSet()))
            {
                result[productId] = targetProductsCount[productId];
            }
            foreach (var productId in targetProductsCount.Keys.ToHashSet().Intersect(currentProductsCount.Keys.ToHashSet()))
            {
                if (targetProductsCount[productId] - currentProductsCount[productId] > 0)
                {
                    result[productId] = targetProductsCount[productId] - currentProductsCount[productId];
                }
            }
            return result;
        }

        /// <summary>
        /// Кол-во товара на складе
        /// </summary>
        public Dictionary<int, int> GetProductsCounts(int holderId)
        {
            Dictionary<int, int> result = new Dictionary<int, int>();
            var products = GetProducts(holderId);
            foreach (var product in products)
            {
                result[product.Id] = GetProductCount(holderId, product.Id);
            }
            return result;
        }

        /// <summary>
        /// Кол-во товара на складе
        /// </summary>
        public int GetProductCount(int holderId, int productId)
        {
            var holdersInstock = _deliveryDbContext.ProductsInStock.Where(instock => instock.HolderId == holderId && instock.ProductId == productId);
            if (holdersInstock.Count() == 0)
                throw new ArgumentException("productId", "Товар не найден на этом складе");
            if (holdersInstock.Count() > 1)
                throw new ArgumentException("productId", "Ошибка в данных");
            return holdersInstock.First().ProductCount;
        }

        public IEnumerable<ProductsInStock> GetProductOffer(int holderId)
        {
            return _deliveryDbContext.ProductsInStock.Include(pis => pis.Product).Include(pis => pis.Product.ProductComments).Include(pis => pis.Product.ProductImages).Where(instock => instock.HolderId == holderId).ToList();
        }

        /// <summary>
        /// Получение кол-во которое должно быть на складе или магазине
        /// </summary>
        /// <param name="holderId"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetProductsCountsInStock(int holderId)
        {
            var result = new Dictionary<int, int>( 
                _deliveryDbContext.ProductsInStock
                .Include(pis => pis.Product)
                .Include(pis => pis.Product.ProductComments)
                .Include(pis => pis.Product.ProductImages)
                .Where(instock => instock.HolderId == holderId)
                .Select( ins => new KeyValuePair<int,int>(ins.ProductId, ins.ProductCount-ins.ProductsInReserve)).ToList() );
            return result;
        }

        public void SetOrderStored(int holderId, Order order)
        {            
            _deliveryDbContext.Orders.Find(order.Id).HolderId = holderId;
            _deliveryDbContext.Orders.Find(order.Id).OnOrderStored();
            _deliveryDbContext.SaveChanges();
        }


        /// <summary>
        /// Оплатить доставку по заказу
        /// </summary>     
        public int PaymentDelivery(int orderId)
        {
            var order = _deliveryDbContext.Orders.First(o => o.Id == orderId);
            order.OrderItems = _deliveryDbContext.OrderItems.Include(item => item.Product).Where(o => o.OrderId == orderId).ToList();
            var holder = order.HolderId;
            var transport = order.TransportId;

            var holderUserId = _deliveryDbContext.Holders.First(t => t.Id == holder).UserId;
            var holderUser = _userDbContext.UserContexts_.First(u => u.Id == holderUserId);
            var holderUserWallet = _userDbContext.UserWallets_.First(u => u.UserId == holderUserId);
            var transportUserId = _deliveryDbContext.Transports.First(t => t.Id == transport).UserId;
            var transportUser = _userDbContext.UserContexts_.First(u => u.Id == transportUserId);
            var transportUserWallet = _userDbContext.UserWallets_.First(u => u.UserId == transportUserId);
             
            var amount = GetDeliveryPrice(order.OrderItems.Select(item => new ProductDeliveryPriceParams()
            {
                Count = item.ProductCount,
                Size = item.ProductSize,
                Weight = item.ProductWeight
            }));
            holderUserWallet.Amount -= amount;
            transportUserWallet.Amount += amount;

            _userDbContext.TransferTransactions_.Add(new() {
                SourceWalletId = holderUserWallet.Id,
                TargetWalletId = transportUserWallet.Id,
                TransactionAmount = amount
            });
            return _userDbContext.SaveChanges();
        }

        public class ProductDeliveryPriceParams
        {
            public int Count { get; set; }
            public string Size { get; set; }
            public int Weight { get; set; }
        }

        private double GetDeliveryPrice(IEnumerable<ProductDeliveryPriceParams> products)
        {
            double price = 1000;
            if (products.Sum(p => p.Weight) > 5000)
                price += 1000;
            if (products.Sum(p => (p.Count * p.Size[0] * p.Size[1] * p.Size[2])) > 5000)
                price += 1000;
            return price;            
        }

        public IEnumerable<ProductsInStock> GetProductsInStock(int Id)
        {
            return _deliveryDbContext.ProductsInStock.Where(instock => instock.HolderId == Id).ToList();
        }

        public ProductsInStock GetProductInStockInfo(int HolderId, int ProductId)
        {
            return _deliveryDbContext.ProductsInStock.Include(instock => instock.Product).FirstOrDefault(instock => instock.HolderId == HolderId && instock.ProductId == ProductId);
        }

        public UserPerson GetCustomerPerson(int id)
        {
            var customer = _deliveryDbContext.Customers.Find(id);
            if (customer is null)
                throw new ArgumentException("id","Идентификатор клиента задан неверно, ничего не найдено.");
            var user = _userDbContext.UserContexts_.Include(u => u.Person).FirstOrDefault(u => u.Id==customer.UserId);
            if (user is null)
            {
                throw new NullReferenceException("Идентификатор пользователя у клиента {id} зне привязан к DbContextUser.Users");
            }             
            return user.Person;
        }

        public List<Order> GetOrderInDelivery(int holderId)
        {
            return _deliveryDbContext.Orders.Where(order => order.HolderId == holderId && 3 == order.OrderStatus).ToList();
        }
    }
}
