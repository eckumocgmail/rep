using System.ComponentModel.DataAnnotations;

namespace Console_BlazorApp.AppUnits.DeliveryModel
{
    public class ProductComment : BaseEntity
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
     
        public Product Product { get; set; }

        [MinLength(10)]
        [NotNullNotEmpty]
        [InputRusText]
        public string CommentText { get; set; }
        [Range(0,10)]
        public int CommentMark { get; set; }
        public DateTime CreatedTime
        {
            get; set;
        }
    }
}