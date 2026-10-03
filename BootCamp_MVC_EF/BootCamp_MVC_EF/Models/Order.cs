using System.ComponentModel.DataAnnotations;

namespace BootCamp_MVC_EF.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer is required")]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Quantity is required")]
        public int Quantity { get; set; }

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public decimal TotalPrice { get; set; }
    }
}
