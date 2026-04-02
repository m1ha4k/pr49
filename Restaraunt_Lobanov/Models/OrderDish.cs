using System.ComponentModel.DataAnnotations;

namespace Restaraunt_Lobanov.Models
{
    public class OrderDish
    {
        [Key]
        public int DishId { get; set; }
        public int Count { get; set; }
    }
}