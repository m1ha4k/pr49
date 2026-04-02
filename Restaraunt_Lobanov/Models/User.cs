using System.ComponentModel.DataAnnotations;

namespace Restaraunt_Lobanov.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
