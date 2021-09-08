using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BicycleStore.Models;

namespace BicycleStore.Models
{
    public class Order
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "- Name is required! -")]
        public string Name { get; set; }
        [Required(ErrorMessage = "- Surname is required! -")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "- Phone Number is required! -")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "- Email is required! -")]
        public string Email { get; set; }
        [Required(ErrorMessage = "- Postcode is required! -")]
        public string Postcode { get; set; }

        public int BicycleId { get; set; }
        public Bicycle bicycle { get; set; }
    }
}
