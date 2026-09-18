using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootCamp_MVC_EF.Models
{
    public class Supplier
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Supplier Name")]
        [Required(ErrorMessage = "Name is required")]
        public string SupplierName { get; set; }

        [DisplayName("Material Type")]
        [Required(ErrorMessage = "Material type is required")]
        public string MaterialType { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }
    }
}
