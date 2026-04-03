using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Restaraunt_Lobanov.Models
{
    public class OrderDish
    {
        [Key]
        public int DishId { get; set; }
        public int Count { get; set; }
    }
}