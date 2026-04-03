using System.ComponentModel.DataAnnotations;

namespace Restaraunt_Lobanov.Models
{
    public class OrderDish
    {
        public int OrderId { get; set; }
        [Key]
        public int DishId { get; set; }
        public int Count { get; set; }
    }
}