using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface ICustomerService
    {
        /*void AddItemToOrder(Product first, int orderId);
        Order CreateOrder(int customerId, IDictionary<Product, int> productCounts, int holderId);
        List<Order> GetOrders(int customerId);
        void OrderCheckout(Order order);
        Dictionary<int, int> SearchProductHolders(int productId, int count);
        IEnumerable<Product> SearchProducts(string query);
        int SetProductReserved(int productId, int count, int stockId);
        CustomerContext GetCustomerByPhone(string phoneNumber);*/

   
        /// <summary>
        /// Добавление товара в корзину заказа
        /// </summary>
        /// <param name="first"></param>
        /// <param name="orderId"></param>
        void AddItemToOrder(Product first, int orderId);
        int CreateCustomer(string firstName, string lastName, string phoneNumber);
        Order CreateOrder(int customerId);
        Order CreateOrder(int customerId, IDictionary<Product, int> productCounts, int holderId);
        CustomerContext GetCustomerByPhone(string phoneNumber);
        List<Holder> GetHolders();
        List<Order> GetOrders(int customerId);
        void OrderCheckout(Order order);
        Dictionary<int, int> SearchProductHolders(int productId, int count);
        IEnumerable<Product> SearchProducts(string query);
        IEnumerable<Product> SearchProducts(string query, double maxPrice);
        IEnumerable<Product> SearchProducts(string query, List<string> categories);
        IEnumerable<Product> SearchProducts(string query, List<string> categories, double maxPrice);
        int SetProductReserved(int productId, int count, int stockId);
    }
}