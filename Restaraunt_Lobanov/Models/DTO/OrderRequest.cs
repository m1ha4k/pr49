namespace Restaraunt_Lobanov.Models.DTO
{
    public class OrderRequest
    {
        public string Address { get; set; }
        public string Date { get; set; }
        public List<OrderDishRequest> Dishes { get; set; }
    }
}