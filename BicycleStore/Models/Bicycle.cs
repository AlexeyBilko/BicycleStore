using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BicycleStore.Models
{
    public class Bicycle
    {
        public int BicycleId { get; set; }
        [Required]
        public string Manufacturer { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public int WeelsRadius { get; set; }
        [Required]
        public int Brakes { get; set; }
        [Required]
        public string Type { get; set; }

        public string PictureBase64 { get; set; }
    }
}
