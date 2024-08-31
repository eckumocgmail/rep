using Console_BlazorApp.AppUnits.DeliveryApi;
using Console_BlazorApp.AppUnits.DeliveryModel;
using Microsoft.EntityFrameworkCore;

namespace pickpoint_delivery_service
{

   
    public class OrdersService : IOrdersService
    {
        private readonly DeliveryDbContext _unit;
        private readonly SigninUser _signin;

        public OrdersService(DeliveryDbContext unit, SigninUser signin)
        {
            _unit = unit;
            _signin = signin;
        }

        public Order GetOrder(int id)
        {
            var order = _unit.Orders.Include(o => o.Holder).FirstOrDefault(o => o.Id == id);
            order.OrderItems = _unit.OrderItems.Include(o => o.Product).Include(o => o.Product.ProductImages).Where(item => item.OrderId == id).ToList();
            return order;
        }

        public Order CreateOrder(int customerId)
        {
            var result = new Order() { CustomerId = customerId };
            _unit.Orders.Add(result);
            CustomerContext customer = _unit.Customers.FirstOrDefault(customer => customer.Id == customerId);
            if (customer is null)
                throw new ArgumentException("custoemrId", "Клиента с таким идентификатором не найдено");
            _unit.SaveChanges();
            customer.CurrentOrderId = result.Id;
            _unit.SaveChanges();
            return result;
        }

        public void Clear()
        {
            foreach (var o in _unit.Orders.Select(o => o.Id).ToList())
            {
                _unit.Remove(_unit.Orders.Find(o));
            }
            _unit.SaveChanges();
        }

        

        public CustomerContext GetCustomerByUser(UserContext user)
        {
            CustomerContext customer = _unit.Customers.FirstOrDefault(customer => customer.UserId == user.Id);
            if (customer == null)
            {
                _unit.Customers.Add(customer = new CustomerContext()
                {
                    FirstName = user.Person.FirstName,
                    LastName = user.Person.LastName,
                    PhoneNumber = user.Person.Tel,
                    UserId = user.Id
                });
                _unit.SaveChanges();
            }
            return customer;
        }


        public IEnumerable<Order> GetOrders()
        {

            if (_signin.IsSignin() == false)
            {
                throw new UnauthorizedAccessException("Метод используется только для авторизованных пользвателей");
            }
            var user = _signin.Verify();
            var customer = GetCustomerByUser(user);
            return _unit.Orders.Include(o => o.Holder).Include(o => o.OrderItems).Where(order => order.CustomerId == customer.Id).OrderByDescending(o => o.OrderCreated).ToList();
        }

        public IEnumerable<OrderItem> GetOrderItems(int orderId)
        {
            return _unit.OrderItems.Include(o => o.Product).Include(o => o.Product.ProductImages).Where(item => item.OrderId == orderId);
        }

        public void CancellOrder(int orderId)
        {
            _unit.Orders.Find(orderId)?.OnOrderCanceled();
            _unit.SaveChanges();

        }

        public void CompleteOrder(int orderId)
        {
            var order = _unit.Orders.Find(orderId);
            order?.OnOrderCompleted();
            _unit.SaveChanges();
            foreach (var orderItem in _unit.OrderItems.Include(o => o.Product).Where(item => item.OrderId == orderId))
            {
                var instock = _unit.ProductsInStock.First(ins => ins.HolderId == order.HolderId && ins.ProductId == orderItem.ProductId);
                instock.ProductsInReserve -= orderItem.ProductCount;
                instock.ProductCount -= orderItem.ProductCount;
            }
            _unit.SaveChanges();
        }

        public void CreateReservationOrder(int holderId, Dictionary<int, int> products, int customerId)
        {
            throw new NotImplementedException();
        }

        public void CreateDeliveryOrder(int holderId, Dictionary<int, int> products, int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
