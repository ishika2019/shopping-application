using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace project.Entities
{
    public class BasketItem
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int itemId { get; set; }
        public string ProductName { get; set; }
        public decimal price { get; set; }

        public int Quantity { get; set; }

        public string PictureUrl { get; set; }
        public string Brand { get; set; }
        public string Type { get; set; }

   
        public string CustomerBasketId { get; set; }

    }
}