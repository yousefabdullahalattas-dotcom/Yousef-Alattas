using BootCamp_MVC_EF.Data;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootCamp_MVC_EF.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Customer Name")]
        [Required(ErrorMessage = "Name is required")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        [DisplayName("Customer Type")]
        public string CustomerType { get; set; } = "";
    }



}

