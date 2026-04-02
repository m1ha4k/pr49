using System.ComponentModel.DataAnnotations;

namespace Restaraunt_Lobanov.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        public List<OrderDish> Dishes { get; set; }
    }
}