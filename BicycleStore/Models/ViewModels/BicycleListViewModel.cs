using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BicycleStore.Models.ViewModels
{
    public class BicycleListViewModel
    {
        public int? PageNumber { get; set; }
        public int TotalPages { get; set; }
        public List<Bicycle> Bicycles { get; set; }
        public SelectList Companies { get; set; }
    }
}
