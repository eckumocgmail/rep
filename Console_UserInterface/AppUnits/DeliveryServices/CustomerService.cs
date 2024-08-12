using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.EntityFrameworkCore;

using pickpoint_delivery_service;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    /// <summary>
    /// Операции: 
    ///     запись на СТО, 
    ///     покупка запчастей
    /// </summary>
    public class CustomerService : ICustomerService
    {
        private readonly DeliveryDbContext _deliveryUnit;

        public CustomerService(DeliveryDbContext deliveryUnit)
        {
            _deliveryUnit = deliveryUnit;
        }

        /// <summary>
        /// Список заказов клиента
        /// </summary>
        /// <param name="customerId">ид-клиента</param>        
        public List<Order> GetOrders(int customerId)
        {
            return _deliveryUnit.Orders.Include(o => o.OrderItems).Where(o => o.CustomerID == customerId).ToList();
        }

        /// <summary>
        /// Создать заказ 
        /// </summary>
        /// <param name="customerId">ид-клиента который создаёт заказ</param>
        /// <param name="productCounts">товары</param>
        /// <param name="holderId">точка вывоза</param>        
        public Order CreateOrder(int customerId, IDictionary<Product, int> productCounts, int holderId)
        {
            var order = new Order() { CustomerID = customerId };
            order.OnOrderCreated();
            _deliveryUnit.Orders.Add(order);
            _deliveryUnit.SaveChanges();
            foreach (var products in productCounts)
            {
                var item = new OrderItem()
                {
                    Product = products.Key,
                    ProductCount = products.Value,
                    OrderID = order.Id
                };
                _deliveryUnit.OrderItems.Add(item);
                order.OrderItems.Add(item);
            }
            _deliveryUnit.SaveChanges();

            order.HolderID = holderId;
            _deliveryUnit.SaveChanges();
            order.Holder = _deliveryUnit.Holders.FirstOrDefault(holder => holder.Id == holderId);
            return order;
        }

        public IEnumerable<Product> SearchProducts(string query, double maxPrice)
        {
            var fasade = new EntityFasade<Product>(_deliveryUnit);
            var products = fasade.Search(query);
            return products.Where(product => product.ProductCost <= maxPrice).ToList();
        }

        public IEnumerable<Product> SearchProducts(string query, List<string> categories, double maxPrice)
        {

            //TODO
            var fasade = new EntityFasade<Product>(_deliveryUnit);
            var products = fasade.Search(query);

            return products;
        }

        public IEnumerable<Product> SearchProducts(string query)
        {
            var fasade = new EntityFasade<Product>(_deliveryUnit);
            return fasade.Search(query);
        }

        public IEnumerable<Product> SearchProducts(string query, List<string> categories)
        {
            var fasade = new EntityFasade<Product>(_deliveryUnit);
            return fasade.Search(query);
        }

        public void AddItemToOrder(Product first, int orderId)
        {
            try
            {
                var order = _deliveryUnit.Orders.Include(o => o.OrderItems).FirstOrDefault(o => o.Id == orderId);
                if (order == null)
                {
                    throw new ArgumentException("orderId");
                }
                order.OrderItems.Add(new OrderItem()
                {
                    ProductID = first.Id,
                    ProductCount = 1,
                    OrderID = orderId
                });
                _deliveryUnit.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine("Ошибка при добавлении товара в заказ", ex);
            }
        }

        public void OrderCheckout(Order order)
        {
           
            var current = _deliveryUnit.Orders.Find(order.Id);
            current.SetProperties(order);
            current.OnOrderCreated();
            _deliveryUnit.SaveChanges();
             
            
        }


        /// <summary>
        /// Получение сведений о наличии товара в заданном кол-ве
        /// Возвращает справочник типа <HolderId,Count>
        /// </summary>
        public Dictionary<int, int> SearchProductHolders(int productId, int count)
        {
            var instocks = _deliveryUnit.ProductsInStock.Where(instock => instock.ProductID == productId && instock.ProductCount - instock.ProductsInReserve >= count);
            var result = instocks.Select(instock => new KeyValuePair<int, int>(instock.HolderID, instock.ProductCount - instock.ProductsInReserve)).ToList();
            return new(result);
        }

        public int SetProductReserved(int productId, int count, int stockId)
        {
            ProductsInStock instock = _deliveryUnit.ProductsInStock.FirstOrDefault(instock => instock.HolderID == stockId && productId == productId);
            /*if (instock.ProductsInReserve + count > instock.ProductCount)
            {
                throw new ArgumentException("count");
            }*/
            instock.ProductsInReserve += count;
            _deliveryUnit.SaveChanges();
            return instock.ProductsInReserve;
        }

        public CustomerContext GetCustomerByPhone(string phoneNumber)
        {
            return _deliveryUnit.Customers.FirstOrDefault(customer => customer.PhoneNumber == phoneNumber);
        }

        public int CreateCustomer(string firstName, string lastName, string phoneNumber)
        {
            var customer = new CustomerContext()
            {
                FirstName = firstName,
                LastName = lastName,
                PhoneNumber = phoneNumber
            };
            _deliveryUnit.Customers.Add(customer);
            _deliveryUnit.SaveChanges();
            return customer.Id;
        }

        public List<Holder> GetHolders()
        {
            return _deliveryUnit.Holders.ToList();
        }

        public Order CreateOrder(int customerId)
        {
            var order = new Order() {  CustomerID = customerId };
            _deliveryUnit.Orders.Add(order);
            _deliveryUnit.SaveChanges();
            var customer = _deliveryUnit.Customers.Find(customerId);
            customer.CurrentOrderId = order.Id;
            _deliveryUnit.SaveChanges();
            return order;
        }
    }
}