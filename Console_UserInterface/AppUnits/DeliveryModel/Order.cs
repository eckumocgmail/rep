using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations.Schema;

namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    public class Order : BaseEntity
    {
        public Order()
        {
            
        }

        [NotMapped]
        public string LastUpdated { get; set; } = DateTime.Now.ToString("T");
        public int CustomerID { get; set; }


        [JsonIgnore]
        public CustomerContext Customer { get; set; }
        public int? HolderID { get; set; }


        [JsonIgnore]
        public Holder Holder { get; set; }
        public int? TransportID { get; set; }


        [JsonIgnore]
        public Transport Transport { get; set; }


        public DateTime? OrderCreated { get; set; } = DateTime.Now;
        public DateTime? OrderUpdated { get; set; }
        public int UpdateCounter { get; set; } = 0;


        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();


        /// <summary>
        /// Вычисление общей суммы заказа
        /// </summary>            
        public float GetOrderPrice()
        {
            float price = 0;
            foreach (var item in OrderItems)
            {
                price += item.ProductCount * item.ProductCount;
            }
            return price;
        }


        /// <summary>
        /// Выставляем статус при регистрации изделия
        /// </summary>
        public void OnOrderCreated()
        {
            OrderStatus = 1;
            OrderUpdated = DateTime.Now;
            UpdateCounter++;
        }

        /// <summary>
        /// Выставляем статус при поступлении на склад
        /// </summary>
        public void OnOrderStored()
        {
            OrderStatus = 2;
            OrderUpdated = DateTime.Now;
            UpdateCounter++;

        }

        /// <summary>
        /// Выставляем статус при передачи курьеру
        /// </summary>
        public void OnOrderIsDelivering()
        {
            OrderStatus = 3;
            OrderUpdated = DateTime.Now;
            UpdateCounter++;
        }

        /// <summary>
        /// Выставляем статус при передачи курьеру
        /// </summary>
        public void OnOrderDelivered()
        {
            OrderStatus = 4;
            OrderUpdated = DateTime.Now;
            UpdateCounter++;
        }


        /// <summary>
        /// Выставляем статус при передачи курьеру
        /// </summary>
        public void OnOrderReceived()
        {
            OrderStatus = 5;
            OrderUpdated = DateTime.Now;
            UpdateCounter++;
        }


        /// <summary>
        /// Выставляем статус при передачи курьеру
        /// </summary>
        public void OnOrderCanceled()
        {
            OrderStatus = 6;
            OrderUpdated = DateTime.Now;
            UpdateCounter++;
        }

        /// <summary>
        /// Выставляем статус при передачи курьеру
        /// </summary>
        public void OnOrderCompleted()
        {
            OrderStatus = 5;
            OrderUpdated = DateTime.Now;
            UpdateCounter++;
        }



        /***
         * 1-зарегистрирован
         * 2-на складе
         * 3-у курьера
         * 4-в постамате
         * 5-добавлен получателю
         * 6-отменён
         * 7-завершен
         */
        public int OrderStatus { get; private set; } = 0;

        
        public int DeliveryStatus { get; private set; } = 0;
        public string GetDeliveryStatusText()
        {
            switch (DeliveryStatus)
            {
                case 0: return "готовиться к перевозке";
                case 1: return "готов к выдачи курьеру";
                case 2: return "получен курьером";
                case 3: return "доставлен курьером";
                case 4: return "проверен получателем";
                case 5: return "ожидает клиента"; 
                default: return $"неправельный статуc: {DeliveryStatus}";
            }
        }

        public string GetStatusText()
        {
            switch (OrderStatus)
            {
                case 0: return "неустановлен";
                case 1: return "зарегистрирован";
                case 2: return "на складе";
                case 3: return "у курьера";
                case 4: return "в постамате";
                case 5: return "выдан покупателю";
                case -1: return "отменён";
                default: return $"неправельный статуc: {OrderStatus}";
            }
        }

        public static List<int> GetOrderStatused() => new List<int> { 1, 2, 3, 4, 5, 6}; 
    }

}
