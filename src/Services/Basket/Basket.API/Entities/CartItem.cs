using System.ComponentModel.DataAnnotations;

namespace Basket.API.Entities
{
    public class CartItem
    {
        [Required]
        [Range(1, double.PositiveInfinity, ErrorMessage = "The feild {0} must be >= {1}")]

        public int Quantity { get; set; }

        [Required]
        [Range(0.1, double.PositiveInfinity, ErrorMessage = "The feild {0} must be >= {1}")]
        public decimal ProductPrice { get; set; }

        [Required]
        public string ProductNo { get; set; }

        [Required]
        public string ProductName { get; set; }

        public int AvailableQuantity { get; set; }
    }
}
