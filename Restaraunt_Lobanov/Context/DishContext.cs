using Microsoft.EntityFrameworkCore;
using Restaraunt_Lobanov.Models;

namespace Restaraunt_Lobanov.Context
{
    public class DishContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDish> orderDishes { get; set; }

        public DishContext()
        {
            Database.EnsureCreated();
            User.Load();
            Dishes.Load();
            Orders.Load();
            orderDishes.Load();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3307;database=Restaraunt;uid=root;pwd=;", new MySqlServerVersion(new Version(8, 0, 11)));
        }
    }
}