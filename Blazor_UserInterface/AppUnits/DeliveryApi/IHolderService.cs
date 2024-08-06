using Console_BlazorApp.AppUnits.DeliveryModel;

namespace Console_BlazorApp.AppUnits.DeliveryApi
{
    public interface IHolderService
    {

        //оплатить доставку по заказу
        int PaymentDelivery(int orderId);

        int GetProductCount(int holderId, int productId);
        IEnumerable<ProductsInStock> GetProductOffer(int holderId);
        List<Product> GetProducts(int holderId);

        List<Order> GetOrderInDelivery(int holderId);
        Dictionary<int, int> GetProductsCountsInStock(int holderId);

        /// <summary>
        /// Личный сведения по клиенту
        /// </summary>
        UserPerson GetCustomerPerson(int id);

        /// <summary>
        /// Получение кол-ва доступного товара (с учётом резерва)
        /// </summary>        
        /// <returns>Справочник [ИД-товара,Кол-во с учётом резерва]</returns>
        Dictionary<int, int> GetProductsCounts(int holderId);

        /// <summary>
        /// Получить данные для оформления заказ очередной поставки необходимого товара
        /// </summary>
        Dictionary<int, int> CreateOrder(int holderId, Dictionary<int, int> targetProductsCount);

        void SetOrderStored(int holderId, Order order);


        /// <summary>
        /// Свеления по товару в наличии
        /// </summary>
        IEnumerable<ProductsInStock> GetProductsInStock(int Id);


        /// <summary>
        /// Сведления по товару на складе
        /// </summary>
        ProductsInStock GetProductInStockInfo(int HolderId, int ProductId);

    }
}