using Console_BlazorApp.AppUnits.DeliveryModel;
using pickpoint_delivery_service;

namespace Console_BlazorApp.AppUnits.DeliveryServices
{
    /// <summary>
    /// Функции добавления комментариев к товару
    /// </summary>
    public class ProductCommentsService
    {
        private readonly DeliveryDbContext deliveryDbContext;

        public ProductCommentsService(DeliveryDbContext deliveryDbContext)
        {
            this.deliveryDbContext = deliveryDbContext;
        }

        /// <summary>
        /// Добавить комментарий к товару
        /// </summary>
        /// <param name="productId">ид-товара</param>
        /// <param name="mark">оценка от 0 до 10</param>
        /// <param name="text">тест комментария</param>
        /// <returns>ид-комментария</returns>
        public int Add(int productId, int mark, string text)
        {
            deliveryDbContext.ProductComments.Add(new() { 
                 CommentMark = mark,
                 CommentText = text,
                 CreatedTime = DateTime.Now,
                 ProductId = productId
            });
            return deliveryDbContext.SaveChanges(); 
        }

        /// <summary>
        /// Получение комментариев по товару
        /// </summary>
        /// <param name="productId">ид-товара</param>
        /// <returns>список комментариев</returns>
        public List<ProductComment> GetComments(int productId)
        {
            return deliveryDbContext.ProductComments.OrderByDescending(c => c.CreatedTime).Where(comment => comment.ProductId == productId).Take(10).ToList();            
        }
    }
}
