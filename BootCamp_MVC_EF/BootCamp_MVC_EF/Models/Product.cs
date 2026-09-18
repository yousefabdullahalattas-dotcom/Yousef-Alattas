using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootCamp_MVC_EF.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Product Name")]
        [Required(ErrorMessage = "Name is required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Power is required")]
        public int PowerWatt { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }
        public int StockQuantity { get; set; } = 0;

    }
}

