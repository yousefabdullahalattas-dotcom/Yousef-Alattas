using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootCamp_MVC_EF.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Employee Name")]
        [Required(ErrorMessage = "Name is required")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Position is required")]
        public string Position { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }
    }
}

